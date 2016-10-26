/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;
using dk.gov.oiosi.communication.client;
using dk.gov.oiosi.extension.wcf.Behavior;
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.extension.wcf.Interceptor.UbiquitousProperties;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.communication.handlers.email;

namespace dk.gov.oiosi.communication
{

    /// <summary>
    /// Represents a request to an OIOSI http or email endpoint.
    /// </summary>
    public class Request : dk.gov.oiosi.communication.IRequest
    {

        // The proxy used for service calls
        private ClientProxy proxy;

        // The delegate used to call our synchronous sending method asynchronously
        private delegate void AsyncGetResponse(OiosiMessage message, out Response response);

        /// <summary>
        /// Constant for http endpoint configuration name
        /// </summary>
        public const string HttpEndpointConfigurationName = "OiosiHttpEndpoint";

        /// <summary>
        /// The name of the WCF endpoint configuration
        /// </summary>
        protected string endpointConfigurationName;

        private Credentials credentials;
        private Uri requestUri;
        private SendPolicy policy = new SendPolicy();

        #region public methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpointAddress"></param>
        public Request(Uri endpointAddress)
        {
            // Test that a valid endpoint address was given
            this.TestEndpointAddressCompability(endpointAddress);
            this.requestUri = endpointAddress;
        }

        /// <summary>
        /// Request
        /// </summary>
        /// <param name="endpointAddress">The endpoint address</param>
        /// <param name="credentials">Credentials</param>
        public Request(Uri endpointAddress, Credentials credentials)
            : this(endpointAddress)
        {
            this.credentials = credentials;
        }

        /// <summary>
        /// Creates a new Request instance bound to the specific endpoint. During construction, 
        /// the type of endpoint is inferred from the Uri scheme. After construction, additional 
        /// properties may be set for the request, such as credentials
        /// </summary>
        /// <param name="credentials">Overrides the credentials set in config</param>
        /// <param name="endpointAddress"></param>
        /// <param name="sendPolicy">The send policy of the request</param>
        public Request(Uri endpointAddress, Credentials credentials, SendPolicy sendPolicy)
            : this(endpointAddress, credentials)
        {
            this.policy = sendPolicy;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint configuration</param>
        public Request(string endpointConfigurationName)
        {
            if (string.IsNullOrEmpty(endpointConfigurationName))
            {
                throw new NoEndpointGivenException();
            }

            this.endpointConfigurationName = endpointConfigurationName;
        }

        /// <summary>
        /// Tests that and endpoint address is compatible with Request
        /// </summary>
        protected void TestEndpointAddressCompability(Uri endpointAddress)
        {
            if (endpointAddress == null)
            {
                throw new NoEndpointGivenException();
            }

            // We only support 'http' and 'mailto'
            if (endpointAddress.Scheme != "mailto" && endpointAddress.Scheme != "http")
            {
                throw new NotSupportedSchemeException(endpointAddress.Scheme);
            }
        }

        /// <summary>
        /// Creates the proxy object
        /// </summary>
        protected void CreateProxy()
        {
            logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Proxy being created");

            // Try to create a proxy
            try
            {
                // Is an endpoint in configuration given?
                if (!string.IsNullOrEmpty(endpointConfigurationName))// != null)
                {
                    this.proxy = new ClientProxy(endpointConfigurationName);
                    this.requestUri = proxy.Endpoint.Address.Uri;
                    logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "Endpoint configuration section '" + endpointConfigurationName + "' used to create proxy object");
                }
                // ... if not, infer the endpoint config from the URI type
                else
                {
                    switch (this.requestUri.Scheme)
                    {
                        case "http":
                            {
                                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "HTTP transport inferred from request URI '" + RequestUri + "'");
                                this.proxy = new ClientProxy(HttpEndpointConfigurationName);
                                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "Default HTTP endpoint configuration section '" + HttpEndpointConfigurationName + "' used to create proxy object");
                                break;
                            }

                        default:
                            {
                                throw new NotSupportedSchemeException(this.requestUri.Scheme);
                            }
                    }
                }

                // Add the behavior that will encrypt the bodies of WS-RM messages
                this.proxy.Endpoint.Behaviors.Add(new EncryptRmBodiesBehavior());
                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "Behavior to encrypt the body of RM messages added to proxy");


                this.SetCredentials();
                this.SetMailConfig();

                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Proxy finished being created");
            }
            catch (Exception e)
            {
                throw new ProxyGenerationException(e);
            }
        }

        private void SetCredentials()
        {

            string dnsId = null;

            // Do we have programatically set credentials?
            if (this.credentials != null)
            {

                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Credentials added programatically. Starting to override proxy settings.");

                // Override client cert
                if (this.credentials.ClientCertificate != null)
                {
                    this.proxy.ClientCredentials.ClientCertificate.Certificate = this.credentials.ClientCertificate.Certificate;
                    logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Client certificate overridden with '" + this.credentials.ClientCertificate.Certificate.FriendlyName + "'");
                }

                // Override server cert
                if (this.credentials.ServerCertificate != null)
                {
                    this.proxy.ClientCredentials.ServiceCertificate.DefaultCertificate = this.credentials.ServerCertificate.Certificate;
                    logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Server certificate overridden with '" + this.credentials.ServerCertificate.Certificate.FriendlyName + "'");
                }

                // Get the endpoint DNS Identity
                dnsId = this.proxy.ClientCredentials.ServiceCertificate.DefaultCertificate.GetNameInfo(X509NameType.DnsName, false);

                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Finished overriding credentials.");
            }
            // The credentials were not set programatically...
            // If client credentials have been set by config, set the local property to point to them
            else if (proxy.ClientCredentials != null)
            {

                X509Certificate2 clientCert = null;
                X509Certificate2 serverCert = null;

                if (this.proxy.ClientCredentials.ClientCertificate != null)
                {
                    clientCert = proxy.ClientCredentials.ClientCertificate.Certificate;
                }
                if (proxy.ClientCredentials.ServiceCertificate != null)
                {
                    serverCert = proxy.ClientCredentials.ServiceCertificate.DefaultCertificate;
                }

                // Local credentials should be set to same as the ones gotten from app.config
                this.credentials = new Credentials(
                    new OcesX509Certificate(clientCert),
                    new OcesX509Certificate(serverCert));

                // Get the endpoint DNS Identity
                dnsId = this.proxy.ClientCredentials.ServiceCertificate.DefaultCertificate.GetNameInfo(X509NameType.DnsName, false);
            }

            // Set the endpoint address
            if (dnsId != null)
            {
                this.proxy.Endpoint.Address = new EndpointAddress(this.requestUri, new DnsEndpointIdentity(dnsId), new AddressHeaderCollection());
            }
            else
            {
                proxy.Endpoint.Address = new EndpointAddress(this.requestUri);
            }
        }

        private void SetMailConfig()
        {
            // Do we have dynamically set mail config?
            if (this.policy != null && this.proxy.Endpoint.Binding.GetType() == typeof(CustomBinding))
            {
                EmailBindingElement binding = ((CustomBinding)this.proxy.Endpoint.Binding).Elements.Find<EmailBindingElement>();
                // If we had an email binding element
                if (binding != null)
                {
                    IMailServerConfiguration inboxConfiguration = this.policy.InboxMailConfiguration;
                    IMailServerConfiguration outboxConfiguration = this.policy.OutboxMailConfiguration;
                    if (inboxConfiguration != null)
                    {
                        binding.ReceivingServerAddress = inboxConfiguration.ServerAddress;
                        binding.ReceivingUserName = inboxConfiguration.UserName;
                        binding.ReceivingPassword = inboxConfiguration.Password;
                        binding.ReceivingPort = inboxConfiguration.ConnectionPolicy.Port;
                        binding.ReceivingAuthenticationMode = inboxConfiguration.ConnectionPolicy.AuthenticationMode;
                    }
                    if (this.policy.OutboxMailConfiguration != null)
                    {
                        binding.SendingServerAddress = outboxConfiguration.ServerAddress;
                        binding.SendingUserName = outboxConfiguration.UserName;
                        binding.SendingPassword = outboxConfiguration.Password;
                        binding.ReplyAddress = outboxConfiguration.ReplyAddress;
                        binding.SendingPort = outboxConfiguration.ConnectionPolicy.Port;
                        binding.SendingAuthenticationMode = outboxConfiguration.ConnectionPolicy.AuthenticationMode;
                    }
                }
            }
        }

        /// <summary>
        /// Synchronously sends a request and gets a response
        /// </summary>
        /// <param name="message">Request message</param>
        /// <returns>Response message</returns>
        [Obsolete("void GetResponse(OiosiMessage message, Response response) should be used instead", false)]
        public Response GetResponse(OiosiMessage message)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now + " " + this.ToString() + ".GetResponse()");
            Response response = null;
            this.OpenProxy();

            try
            {
                this.SendMessage(message, out response);
            }
            catch
            {
                throw;
            }
            finally
            {
                this.CloseProxy();
            }
            return response;
        }

        /// <summary>
        /// Synchronously sends a request and gets a response
        /// </summary>
        /// <example>
        /// Response response;
        /// try{
        ///    GetResponse(request, out response);
        /// }
        /// catch(RequestShutdownException){
        ///    // Sending went well, and we can continue even though we didn't get a neat shutdown
        /// }
        /// catch(Exception e){
        ///     // Sending did not go well
        ///     trow;
        /// }
        /// </example>
        /// <param name="response">The response. If this parameter is set the sending went well and the response is safe to use</param>
        /// <param name="request">Request message</param>
        public void GetResponse(OiosiMessage request, out Response response)
        {
            response = null;
            this.OpenProxy();

            try
            {
                this.SendMessage(request, out response);
            }
            catch
            {
                throw;
            }
            finally
            {
                this.CloseProxy();
            }
        }

        /// <summary>
        /// Opens a proxy connection to the remote endpoint 
        /// </summary>
        private void OpenProxy()
        {
            this.CreateProxy();

            // Test that an inner channel was created
            if (proxy.InnerChannel == null)
            {
                throw new CreatingCommunicationChannelFailedException();
            }
        }

        /// <summary>
        /// Closes the proxy connection to the remote endpoint
        /// </summary>
        private void CloseProxy()
        {
            if (this.proxy != null && this.proxy.State != CommunicationState.Faulted)
            {
                try
                {
                    this.proxy.Close();
                }
                catch (CommunicationObjectAbortedException)
                {
                    // The Communication Object hass already been aborted, so we can not close it
                    // Exception should no be handled.
                }
                catch (Exception ex)
                {
                    throw new RequestShutdownException(ex);
                }
            }
        }

        /// <summary>
        /// Converts the message to a wcf message and sends it via the proxy
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private void SendMessage(OiosiMessage message, out Response response)
        {
            response = null;
            try
            {
                // Convert to WCF message
                Message wcfMessage = null;
                if (message.HasBody)
                {
                    XmlReader xmlBody = message.GetMessageXmlReader();
                    wcfMessage = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, message.RequestAction, xmlBody);
                }
                else
                {
                    wcfMessage = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, message.RequestAction);
                }

                // Adds properties from the Message to the WCF message
                foreach (KeyValuePair<string, object> p in message.Properties)
                {
                    wcfMessage.Properties.Add(p.Key, p.Value);
                }

                // Do we have any properties that should be added to WS-RM messages as well?
                if (message.UbiquitousProperties.Count > 0)
                {
                    try
                    {
                        UbiquitousPropertiesBindingElement interceptor = (((CustomBinding)proxy.ChannelFactory.Endpoint.Binding).Elements.Find<UbiquitousPropertiesBindingElement>());
                        interceptor.SetProperties(message.UbiquitousProperties);
                    }
                    catch (NullReferenceException e)
                    {
                        throw new MissingStackElementException("UbiquitousPropertiesBindingElement", e);
                    }
                }

                // Adds custom headers 
                foreach (KeyValuePair<XmlQualifiedName, MessageHeader> header in message.MessageHeaders)
                {
                    wcfMessage.Headers.Add(header.Value);
                }

                // Sends
                Message wcfMessageResponse = this.proxy.RequestRespond(wcfMessage);

                // Make sure we dind't receive a fault
                if (wcfMessageResponse.IsFault)
                {
                    throw this.CreateFaultWasReceivedException(new FaultException(MessageFault.CreateFault(wcfMessageResponse, int.MaxValue)));
                }

                // Convert back to oiosi message
                response = new Response(wcfMessageResponse);

                // If any properties with the attribute MessageProperty were sent with the message
                // they should be attached to the ListenerRequest message as well
                foreach (object o in wcfMessageResponse.Properties.Values)
                {
                    object[] attributes = o.GetType().GetCustomAttributes(typeof(dk.gov.oiosi.extension.wcf.OiosiMessagePropertyAttribute), false);
                    if (attributes.Length > 0)
                    {
                        response.AddProperty(o);
                    }
                }
            }
            catch (ProtocolException e)
            {
                // Minor hack to fix interop problems with the Java/Axis2 1.2 NemHandel stack
                // SOAP faults might be returned with a http code 400 (Bad request),
                // if that is the case we need to manually get the SOAP fault from the WebException
                if (e.InnerException is System.Net.WebException)
                {
                    throw this.GetSoapFaultFromHttpException(e.InnerException as System.Net.WebException);
                }
                else
                {
                    throw new ProtocolMismatchException(e);
                }
            }
            catch (MessageSecurityException e)
            {
                // If the execption was not due to a fault
                if (!(e.InnerException is FaultException))
                {
                    throw new ProtocolMismatchException(e);
                }
                else
                {
                    throw this.CreateFaultWasReceivedException((FaultException)e.InnerException);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Minor hack to fix interop problems with the Java/Axis2 1.2 NemHandel stack
        /// SOAP faults might be returned with a http code 400 (Bad request),
        /// if that is the case we need to manually get the SOAP fault  from the WebException
        /// </summary>
        private Exception GetSoapFaultFromHttpException(System.Net.WebException e)
        {
            // ToDo : should this string builder has ha max size? ~ new StringBuilder("", 65536);
            StringBuilder sb = new StringBuilder();
            Stream s = e.Response.GetResponseStream();
            //e.
            // Try to read the fault
            try
            {

                byte[] readBuffer = new byte[1000];
                int count = 0;
                bool finish = false;

                while (!finish)
                {
                    count = s.Read(readBuffer, 0, readBuffer.Length);

                    if (count == 0)
                    {
                        // EOF
                        finish = true;
                    }
                    else
                    {
                        sb.Append(System.Text.Encoding.UTF8.GetString(readBuffer, 0, count));
                    }
                }
            }
            catch
            {
                return e;
            }
            finally
            {
                s.Close();
            }

            // Try to make it a SOAP faultobject
            // move declarion here, for easy debugging
            MemoryStream memStream = null;
            Message message;
            MessageFault msgFault;
            Exception exception;
            try
            {
                ;
                using (memStream = new MemoryStream(Encoding.Default.GetBytes(sb.ToString())))
                {
                    XmlTextReader xmlReader = new XmlTextReader(memStream);
                    message = Message.CreateMessage(xmlReader, int.MaxValue, MessageVersion.Soap12WSAddressing10);
                }

                //OiosiMessage oio = new OiosiMessage(message);
                msgFault = MessageFault.CreateFault(message, int.MaxValue);
                exception = CreateFaultWasReceivedException(new FaultException(msgFault));
            }
            catch (Exception)
            {
                return new Exception(sb.ToString());
            }            

            return exception;
        }

        /// <summary>
        /// Creates the appropriate exception when a fault was received
        /// </summary>
        private Exception CreateFaultWasReceivedException(FaultException e)
        {
            // Time for blaming. Who's fault was it?
            if (e.Code.IsSenderFault)
            {
                return new FaultReturnedException(e, "dig");
            }
            else
            {
                return new FaultReturnedException(e, "serveren");
            }
        }

        /// <summary>
        /// Asynchronously starts sending a request
        /// </summary>
        /// <param name="message">Request message</param>
        /// <param name="callback">The asynchronous callback</param>
        /// <returns>Returns an IAsyncResult object</returns>
        public IAsyncResult BeginGetResponse(OiosiMessage message, out Response response, AsyncCallback callback)
        {
            AsyncGetResponse asyncGetResponse = new AsyncGetResponse(GetResponse);
            IAsyncResult result = asyncGetResponse.BeginInvoke(message, out response, callback, asyncGetResponse);
            return result;
        }


        /// <summary>
        /// Asynchronously ends sending a request
        /// </summary>
        /// <returns>Response message</returns>
        public void EndGetResponse(IAsyncResult asyncResult, out Response response)
        {
            //Response r;
            try
            {
                ((AsyncGetResponse)asyncResult.AsyncState).EndInvoke(out response, asyncResult);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            //return r;
        }

        #region properties

        /// <summary>
        /// Property for credentials
        /// </summary>
        public Credentials Credentials
        {
            get { return this.credentials; }
            set { this.credentials = value; }
        }

        /// <summary>
        /// Property for the request uri
        /// </summary>
        public Uri RequestUri
        {
            get { return this.requestUri; }
        }

        /// <summary>
        /// Property for the send policy
        /// </summary>
        public SendPolicy Policy
        {
            get { return this.policy; }
            set { this.policy = value; }
        }

        #endregion

        /// <summary>
        /// Closes the request
        /// </summary>
        public void Close()
        {
            this.CloseProxy();
        }

        /// <summary>
        /// Aborts the request
        /// </summary>
        public void Abort()
        {
            if (this.proxy != null)
            {
                proxy.Abort();
            }
        }

        #endregion
    }
}