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
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.communication.fault;
using dk.gov.oiosi.communication.listener;
using dk.gov.oiosi.extension.wcf;


using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.xml.documentType;

namespace dk.gov.oiosi.communication.service {

    /// <summary>
    /// The implementation of our service contract for use when self hosting services
    /// </summary>
    public class ServiceImplementation: IServiceContract
    {

        /// <summary>
        /// The delegate used to call our synchronous opening method asynchronously
        /// </summary>
        /// <param name="request">The request message</param>
        /// <returns>Returns the response message</returns>
        private delegate Message AsyncRequestRespond(Message request);
        private AsyncRequestRespond _asyncRequestRespond;


        #region IServiceProxyContract Members

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceImplementation() {
            _asyncRequestRespond = new AsyncRequestRespond(RequestRespond);
        }

        /// <summary>
        /// Handles request and respond
        /// </summary>
        /// <param name="request">the message</param>
        /// <returns>Message</returns>
        public virtual Message RequestRespond(Message request) 
        {
            // The incoming message + metadata
            ListenerRequest listenerReq = new ListenerRequest(new OiosiMessage(request));


            try {
                // If any properties with the attribute MessageProperty were sent with the message
                // they should be attached to the ListenerRequest message as well
                foreach (object o in request.Properties.Values) {
                    if (o is InterceptorChannelExceptionCollection) {
                        listenerReq.AddProperty(o);
                    }
                    else {
                        object[] attributes = o.GetType().GetCustomAttributes(typeof(OiosiMessagePropertyAttribute), false);
                        if (attributes.Length > 0) {
                            listenerReq.AddProperty(o);
                        }
                    }
                }
                foreach (MessageHeader h in request.Headers) {
                    listenerReq.RequestMessage.MessageHeaders.Add(new XmlQualifiedName(h.Name, h.Namespace), h);
                }
            }
            catch (Exception e) {
                dk.gov.oiosi.logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Error, "Exception occurred in service implementation: " + e);
            }


            // Trigger the message receive event
            try {
                Listener.TriggerMessageReceiveEvent(listenerReq, MessageProcessStatus.messageReceiveOk);
            }
            catch (Exception exception) {
                return System.ServiceModel.Channels.Message.CreateMessage(MessageVersion.Soap12WSAddressing10,
                    new OiosiMessageFault(exception, OiosiFaultCode.Receiver, OiosiInnerFaultCode.MessagePersistencyFault),
                    common.Definitions.DefaultOiosiNamespace2007 + OiosiInnerFaultCode.MessagePersistencyFault.ToString());
            }

            var typeSearcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig docTypeConfig = typeSearcher.FindUniqueDocumentType(listenerReq.RequestMessage.MessageXml);

            // Reply with an empty message
            return Message.CreateMessage(MessageVersion.Soap12WSAddressing10, docTypeConfig.EndpointType.ReplyAction);
        }

        /// <summary>
        /// ASync begin and end respond
        /// </summary>
        /// <param name="request">the request</param>
        /// <param name="callback">the async callback object</param>
        /// <param name="asyncState">the async state</param>
        /// <returns>result object</returns>
        public IAsyncResult BeginRequestRespond(Message request, AsyncCallback callback, object asyncState) {
            return _asyncRequestRespond.BeginInvoke(request, callback, asyncState);
        }

        /// <summary>
        /// Ends a requestrespond
        /// </summary>
        /// <param name="result">result object</param>
        /// <returns>the message</returns>
        public Message EndRequestRespond(IAsyncResult result) {
            if (_asyncRequestRespond != null)
                return _asyncRequestRespond.EndInvoke(result);
            else return null;
        }

        #endregion
    }
}