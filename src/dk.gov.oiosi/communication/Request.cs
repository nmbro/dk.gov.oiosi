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
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
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

namespace dk.gov.oiosi.communication {

    /// <summary>
    /// Represents a request to an OIOSI http or email endpoint.
    /// </summary>
    public class Request : dk.gov.oiosi.communication.IRequest {
        
        // The proxy used for service calls
        private ClientProxy _proxy;

        // The delegate used to call our synchronous sending method asynchronously
        private delegate Response AsyncGetResponse(OiosiMessage message);

        /// <summary>
        /// Constant for http endpoint configuration name
        /// </summary>
        public const string HttpEndpointConfigurationName = "OiosiHttpEndpoint";
        
        /// <summary>
        /// Constant for smtp endpoint configuration name
        /// </summary>
        public const string MailtoEndpointConfigurationName = "OiosiEmailEndpoint";

        /// <summary>
        /// The name of the WCF endpoint configuration
        /// </summary>
        protected string pEndpointConfigurationName;

        Credentials _credentials;
        private Uri _requestUri;
        SendPolicy _policy = new SendPolicy();

        #region public methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpointAddress"></param>
        public Request(Uri endpointAddress) {
            // Test that a valid endpoint address was given
            TestEndpointAddressCompability(endpointAddress);
            _requestUri = endpointAddress;
        }

        /// <summary>
        /// Request
        /// </summary>
        /// <param name="endpointAddress">The endpoint address</param>
        /// <param name="credentials">Credentials</param>
        public Request(Uri endpointAddress, Credentials credentials)
            : this(endpointAddress) {
            _credentials = credentials;
        }

        /// <summary>
        /// Creates a new Request instance bound to the specific endpoint. During construction, 
        /// the type of endpoint is inferred from the Uri scheme. After construction, additional 
        /// properties may be set for the request, such as credentials
        /// </summary>
        /// <param name="credentials">Overrides the credentials set in config</param>
        /// <param name="endpointAddress"></param>
        /// <param name="sendPolicy">The send policy of the request</param>
        public Request(Uri endpointAddress, Credentials credentials, SendPolicy sendPolicy) : this(endpointAddress, credentials) {
            _policy = sendPolicy;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpointConfigurationName">The name of the endpoint configuration</param>
        public Request(string endpointConfigurationName) {
            if (endpointConfigurationName == null || endpointConfigurationName == "")
                throw new NoEndpointGivenException();
            pEndpointConfigurationName = endpointConfigurationName;
        }

        /// <summary>
        /// Tests that and endpoint address is compatible with Request
        /// </summary>
        protected void TestEndpointAddressCompability(Uri endpointAddress) {

            if (endpointAddress == null)
                throw new NoEndpointGivenException();

            // We only support 'http' and 'mailto'
            if (endpointAddress.Scheme != "mailto" && endpointAddress.Scheme != "http")
                throw new NotSupportedSchemeException(endpointAddress.Scheme);
        }

        /// <summary>
        /// Creates the proxy object
        /// </summary>
        protected void CreateProxy() {
            logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Proxy being created");

            // Try to create a proxy
            try {
                // Is an endpoint in configuration given?
                if (pEndpointConfigurationName != null) {
                    _proxy = new ClientProxy(pEndpointConfigurationName);
                    _requestUri = _proxy.Endpoint.Address.Uri;
                    logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "Endpoint configuration section '" + pEndpointConfigurationName  + "' used to create proxy object");
                }
                // ... if not, infer the endpoint config from the URI type
                else {
                    switch (_requestUri.Scheme) {

                        case "mailto":
                            logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "Mail transport inferred from request URI '" + RequestUri + "'");
                            _proxy = new ClientProxy(MailtoEndpointConfigurationName);
                            logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "Default mail endpoint configuration section '" + MailtoEndpointConfigurationName + "' used to create proxy object");
                            break;

                        case "http":
                            logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "HTTP transport inferred from request URI '" + RequestUri + "'");
                            _proxy = new ClientProxy(HttpEndpointConfigurationName);
                            logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "Default HTTP endpoint configuration section '" + HttpEndpointConfigurationName + "' used to create proxy object");
                            break;

                        default:
                            throw new NotSupportedSchemeException(_requestUri.Scheme);
                    }
                }

                // Add the behavior that will encrypt the bodies of WS-RM messages
                _proxy.Endpoint.Behaviors.Add(new EncryptRmBodiesBehavior());
                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "Behavior to encrypt the body of RM messages added to proxy");


                SetCredentials();
                SetMailConfig();

                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Proxy finished being created");
            }
            catch (Exception e) {
                throw new ProxyGenerationException(e);
            }
        }

        private void SetCredentials() {

            string dnsId = null;

            // Do we have programatically set credentials?
            if (_credentials != null) {

                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Credentials added programatically. Starting to override proxy settings.");

                // Override client cert
                if (_credentials.ClientCertificate == null)
                    throw new MissingCredentialsException();
                _proxy.ClientCredentials.ClientCertificate.Certificate = _credentials.ClientCertificate.Certificate;
                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Client certificate overridden with '" + _credentials.ClientCertificate.Certificate.FriendlyName + "'");

                // Override server cert
                if (_credentials.ServerCertificate == null)
                    throw new MissingCredentialsException();
                _proxy.ClientCredentials.ServiceCertificate.DefaultCertificate = _credentials.ServerCertificate.Certificate;
                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Server certificate overridden with '" + _credentials.ServerCertificate.Certificate.FriendlyName + "'");

                // Get the endpoint DNS Identity
                dnsId = _proxy.ClientCredentials.ServiceCertificate.DefaultCertificate.GetNameInfo(X509NameType.DnsName, false);

                logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Finished overriding credentials.");
            }
            // The credentials were not set programatically...
            // If client credentials have been set by config, set the local property to point to them
            else if (_proxy.ClientCredentials != null){
                
                X509Certificate2 clientCert = null;
                X509Certificate2 serverCert = null;

                if(_proxy.ClientCredentials.ClientCertificate != null)
                    clientCert = _proxy.ClientCredentials.ClientCertificate.Certificate;
                if(_proxy.ClientCredentials.ServiceCertificate != null) 
                    serverCert = _proxy.ClientCredentials.ServiceCertificate.DefaultCertificate;

                // Local credentials should be set to same as the ones gotten from app.config
                _credentials = new Credentials(
                    new OcesX509Certificate(clientCert),
                    new OcesX509Certificate(serverCert));

                // Get the endpoint DNS Identity
                dnsId = _proxy.ClientCredentials.ServiceCertificate.DefaultCertificate.GetNameInfo(X509NameType.DnsName, false);
            }

            // Set the endpoint address
            if (dnsId != null)
                _proxy.Endpoint.Address = new EndpointAddress(_requestUri, new DnsEndpointIdentity(dnsId), new AddressHeaderCollection());
            else
                _proxy.Endpoint.Address = new EndpointAddress(_requestUri);

        }

        private void SetMailConfig() {
            // Do we have dynamically set mail config?
            if (_policy != null && _proxy.Endpoint.Binding.GetType() == typeof(CustomBinding)) {
                EmailBindingElement binding = ((CustomBinding)_proxy.Endpoint.Binding).Elements.Find<EmailBindingElement>();
                // If we had an email binding element
                if (binding != null) {
                    if (_policy.InboxMailConfiguration != null) {
                        binding.ReceivingServerAddress = _policy.InboxMailConfiguration.ServerAddress;
                        binding.ReceivingUserName = _policy.InboxMailConfiguration.UserName;
                        binding.ReceivingPassword = _policy.InboxMailConfiguration.Password;
                        binding.ReplyAddress = _policy.OutboxMailConfiguration.ReplyAddress;
                    }
                    if (_policy.OutboxMailConfiguration != null) {
                        binding.SendingServerAddress = _policy.OutboxMailConfiguration.ServerAddress;
                        binding.SendingUserName = _policy.OutboxMailConfiguration.UserName;
                        binding.SendingPassword = _policy.OutboxMailConfiguration.Password;
                        binding.ReplyAddress = _policy.OutboxMailConfiguration.ReplyAddress;
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
        public Response GetResponse(OiosiMessage message) {
            System.Diagnostics.Debug.WriteLine(DateTime.Now + " " + this.ToString() + ".GetResponse()");
            Response response = null;
            OpenProxy();

            try {
                response = SendMessage(message);
            }
            catch  {
                throw;
            }
            finally {
                CloseProxy();
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
        public void GetResponse(OiosiMessage request, out Response response) {

            response = null;
            OpenProxy();

            try {
                response = SendMessage(request);
            }
            catch {
                throw;
            }
            finally {
                CloseProxy();
            }
        }

        /// <summary>
        /// Opens a proxy connection to the remote endpoint 
        /// </summary>
        private void OpenProxy() {
            CreateProxy();

            // Test that an inner channel was created
            if (_proxy.InnerChannel == null) {
                throw new CreatingCommunicationChannelFailedException();
            }
        }

        /// <summary>
        /// Closes the proxy connection to the remote endpoint
        /// </summary>
        private void CloseProxy() {
            if (_proxy != null && _proxy.State != CommunicationState.Faulted) {
                try {
                    _proxy.Close();
                }
                catch (CommunicationObjectAbortedException) { }
                catch (Exception ex) {
                    throw new RequestShutdownException(ex);
                }
            }
        }

        /// <summary>
        /// Converts the message to a wcf message and sends it via the proxy
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private Response SendMessage(OiosiMessage message) {
            Response response = null;
            try {
                // Convert to WCF message
                Message wcfMessage = null;
                if (message.HasBody) {
                    XmlReader xmlBody = message.GetMessageXmlReader();
                    wcfMessage = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, message.RequestAction, xmlBody);
                }
                else {
                    wcfMessage = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, message.RequestAction);
                }

                // Adds properties from the Message to the WCF message
                foreach (KeyValuePair<string, object> p in message.Properties)
                    wcfMessage.Properties.Add(p.Key, p.Value);

                // Do we have any properties that should be added to WS-RM messages as well?
                if (message.UbiquitousProperties.Count > 0) {
                    try {
                        UbiquitousPropertiesBindingElement interceptor = (((CustomBinding)_proxy.ChannelFactory.Endpoint.Binding).Elements.Find<UbiquitousPropertiesBindingElement>());
                        interceptor.SetProperties(message.UbiquitousProperties);
                    }
                    catch (NullReferenceException e) {
                        throw new MissingStackElementException("UbiquitousPropertiesBindingElement", e);
                    }
                }

                // Adds custom headers 
                foreach (KeyValuePair<XmlQualifiedName,MessageHeader> header in message.MessageHeaders) {
                    wcfMessage.Headers.Add(header.Value);
                }

                // Sends
                Message wcfMessageResponse = _proxy.RequestRespond(wcfMessage);
                
                // Make sure we dind't receive a fault
                if (wcfMessageResponse.IsFault)
                    throw CreateFaultWasReceivedException(new FaultException(MessageFault.CreateFault(wcfMessageResponse, int.MaxValue)));

                // Convert back to oiosi message
                response = new Response(wcfMessageResponse);

                // If any properties with the attribute MessageProperty were sent with the message
                // they should be attached to the ListenerRequest message as well
                foreach (object o in wcfMessageResponse.Properties.Values) {
                    object[] attributes = o.GetType().GetCustomAttributes(typeof(dk.gov.oiosi.extension.wcf.OiosiMessagePropertyAttribute), false);
                    if (attributes.Length > 0) {
                        response.AddProperty(o);
                    }
                }
            }
            catch (ProtocolException e) {

                // Minor hack to fix interop problems with the Java/Axis2 1.2 NemHandel stack
                // SOAP faults might be returned with a http code 400 (Bad request),
                // if that is the case we need to manually get the SOAP fault from the WebException
                if (e.InnerException is System.Net.WebException)
                    throw GetSoapFaultFromHttpException(e.InnerException as System.Net.WebException);
                else
                    throw new ProtocolMismatchException(e);
            }
            catch (MessageSecurityException e) {
                // If the execption was not due to a fault
                if (!(e.InnerException is FaultException))
                    throw new ProtocolMismatchException(e);
                else {
                    throw CreateFaultWasReceivedException((FaultException)e.InnerException);
                }
            }
            catch {
                throw ;
            }
            //4. Return
            return response;
        }

        /// <summary>
        /// Minor hack to fix interop problems with the Java/Axis2 1.2 NemHandel stack
        /// SOAP faults might be returned with a http code 400 (Bad request),
        /// if that is the case we need to manually get the SOAP fault  from the WebException
        /// </summary>
        private Exception GetSoapFaultFromHttpException(System.Net.WebException e){
            
            StringBuilder sb = new StringBuilder("", 65536);
            Stream s = e.Response.GetResponseStream();

            // Try to read the fault
            try {
                
                byte[] readBuffer = new byte[1000];
                int count = 0;

                for (; ; ) {
                    count = s.Read(readBuffer, 0, readBuffer.Length);

                    if (count == 0) {
                        // EOF
                        break;
                    }

                    sb.Append(System.Text.Encoding.UTF8.GetString(readBuffer, 0, count));
                }

            }
            catch {
                return e;
            }
            finally {
                s.Close();
            }

            // Try to make it a SOAP faultobject
            MemoryStream memStream = null;
            try {
                memStream = new MemoryStream(Encoding.Default.GetBytes(sb.ToString()));
                XmlTextReader xmlReader = new XmlTextReader(memStream);
                Message msg = Message.CreateMessage(xmlReader, int.MaxValue, MessageVersion.Soap12WSAddressing10);
                MessageFault msgFault = MessageFault.CreateFault(msg, int.MaxValue);
                return CreateFaultWasReceivedException(new FaultException(msgFault));
            }
            catch {
                return new Exception(sb.ToString());
            }
            finally {
                if (memStream != null)
                    memStream.Close();
            }
        }

        /// <summary>
        /// Creates the appropriate exception when a fault was received
        /// </summary>
        private Exception CreateFaultWasReceivedException(FaultException e) { 
            // Time for blaming. Who's fault was it?
            if (e.Code.IsSenderFault) {
                return new FaultReturnedException(e, "dig");

            }
            else {
                return new FaultReturnedException(e, "serveren");
            }
        }

        /// <summary>
        /// Asynchronously starts sending a request
        /// </summary>
        /// <param name="message">Request message</param>
        /// <param name="callback">The asynchronous callback</param>
        /// <returns>Returns an IAsyncResult object</returns>
        public IAsyncResult BeginGetResponse(OiosiMessage message, AsyncCallback callback) {
            AsyncGetResponse asyncGetResponse = new AsyncGetResponse(GetResponse);
            IAsyncResult result = asyncGetResponse.BeginInvoke(message, callback, asyncGetResponse);
            return result;
        }


        /// <summary>
        /// Asynchronously ends sending a request
        /// </summary>
        /// <returns>Response message</returns>
        public Response EndGetResponse(IAsyncResult asyncResult) {
            Response r;
            try {
                r = ((AsyncGetResponse)asyncResult.AsyncState).EndInvoke(asyncResult);
            }
            catch (InvalidOperationException) {
                throw;
            }
            return r;
        }
      
        #region properties
        
        /// <summary>
        /// Property for credentials
        /// </summary>
        public Credentials Credentials { 
            get { return _credentials; } 
            set { _credentials = value; } 
        }

        /// <summary>
        /// Property for the request uri
        /// </summary>
        public Uri RequestUri { 
            get { return _requestUri; } 
        }

        /// <summary>
        /// Property for the send policy
        /// </summary>
        public SendPolicy Policy { 
            get { return _policy; }
            set { _policy = value; }
        }

        #endregion

        /// <summary>
        /// Closes the request
        /// </summary>
        public void Close() {
            CloseProxy();
        }

        /// <summary>
        /// Aborts the request
        /// </summary>
        public void Abort() {
            if (_proxy != null)
                _proxy.Abort();
        }

        #endregion
    }
}