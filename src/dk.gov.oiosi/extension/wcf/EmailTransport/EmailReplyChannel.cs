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
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.handlers.email;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {

    /// <summary>
    /// Reply channel used by the Email transport. Receives an incoming message and creates a RequestContext, 
    /// that other layers of the stack can use for replying to the sender.
    /// </summary>
    public class EmailReplyChannel: CommonChannelBase, IReplyChannel {
        IMailHandler _mailHandler;

        // The delegate used to call our synchronous opening method asynchronously
        private delegate bool AsyncTryReceiveRequest(TimeSpan timeout, out RequestContext context);
        private AsyncTryReceiveRequest _asyncTryReceiveRequest;
        private RequestContext _asyncTryReceiveRequestOutContext;

        private EmailBindingElement _bindingElement;
        private MailSoap12TransportBinding _msg;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channelManager">channel handler</param>
        /// <param name="bindingElement">The binding element</param>
        /// <param name="mailHandler">The mail handler</param>
        /// <param name="msg">The message</param>
        public EmailReplyChannel(ChannelManagerBase channelManager, IMailHandler mailHandler, MailSoap12TransportBinding msg, EmailBindingElement bindingElement)
            : base(channelManager)
        {
            _msg = msg;
            _mailHandler = mailHandler;
            _asyncTryReceiveRequest = new AsyncTryReceiveRequest(TryReceiveRequest);
            _bindingElement = bindingElement;
        }


        #region IReplyChannel Members

        /// <summary>
        /// Begins an asynchronous operation to receive an available request with a specified
        /// timeout
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies the interval of time to wait for the reception
        /// of an available request</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// receive that a request operation completes</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous receive of a request operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous reception of the
        /// request</returns>
        public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state) {
            throw new NotImplementedException("RaspEmailReplyChannel.BeginRecieveRequest() has not been implemented. Check RaspEmailReplyChannel.cs in the code base or contact the developers.");
        }

        /// <summary>
        /// Begins an asynchronous operation to receive an available request with a default
        /// timeout
        /// </summary>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// receive that a request operation completes</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous receive of a request operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous reception of the
        ///  request</returns>
        public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state) {
            throw new NotImplementedException("RaspEmailReplyChannel.BeginRecieveRequest() has not been implemented. Check RaspEmailReplyChannel.cs in the code base or contact the developers.");
        }

        /// <summary>
        ///  Completes an asynchronous operation to receive an available request
        /// </summary>
        /// <param name="result">The System.IAsyncResult returned by a call to the System.ServiceModel.Channels.IInputChannel.BeginReceive()
        /// method</param>
        /// <returns>The System.ServiceModel.Channels.RequestContext used to construct a reply
        /// to the request</returns>
        public RequestContext EndReceiveRequest(IAsyncResult result) {
            throw new NotImplementedException("RaspEmailReplyChannel.EndRecieveRequest() has not been implemented. Check RaspEmailReplyChannel.cs in the code base or contact the developers.");
        }

        /// <summary>
        /// Receives a request with a timeout
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies the interval of time to wait for the reception
        /// of an available request</param>
        /// <returns>a request context</returns>
        public RequestContext ReceiveRequest(TimeSpan timeout) {
            throw new NotImplementedException("RaspEmailReplyChannel.RecieveRequest() has not been implemented. Check RaspEmailReplyChannel.cs in the code base or contact the developers.");
        }

        /// <summary>
        /// Receives a request
        /// </summary>
        /// <returns>a request context</returns>
        public RequestContext ReceiveRequest() {
            throw new NotImplementedException("RaspEmailReplyChannel.RecieveRequest() has not been implemented. Check RaspEmailReplyChannel.cs in the code base or contact the developers.");
        }

        /// <summary>
        /// Begins an asynchronous operation to receive a request message that has a
        /// specified time out and state object associated with it
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies how long the receive request operation
        /// has to complete before timing out and returning false</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// receive that a request operation completes</param>
        /// <param name="state">An object, specified by the application, that contains state information
        ///  associated with the asynchronous receive of a request operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous receive request
        /// operation</returns>
        public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state) {
            return _asyncTryReceiveRequest.BeginInvoke(timeout,out _asyncTryReceiveRequestOutContext ,callback, state);
        }

        /// <summary>
        /// Completes the specified asynchronous operation to receive a request message
        /// </summary>
        /// <param name="result">The System.IAsyncResult returned by a call to the System.ServiceModel.Channels.IReplyChannel.BeginTryReceiveRequest()
        /// method</param>
        /// <param name="context">The System.ServiceModel.Channels.RequestContext received</param>
        /// <returns>The System.IAsyncResult returned by a call to the System.ServiceModel.Channels.IReplyChannel.BeginTryReceiveRequest()
        /// method</returns>
        public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context) {
            
            if (_asyncTryReceiveRequest != null){
                bool successful = _asyncTryReceiveRequest.EndInvoke(out _asyncTryReceiveRequestOutContext, result);
                context = _asyncTryReceiveRequestOutContext;
                return successful;
            }
            else{
                throw new EmailTransportException();
            }
        }

        /// <summary>
        /// Returns a value that indicates whether a request is received before a specified
        /// interval of time elapses
        /// </summary>
        /// <param name="timeout">The System.TimeSpan that specifies how long the receive of a request operation
        /// has to complete before timing out and returning false</param>
        /// <param name="context">The System.ServiceModel.Channels.RequestContext received</param>
        /// <returns>true if a request message is received before the specified interval of time
        /// elapses; otherwise false</returns>
        public bool TryReceiveRequest(TimeSpan timeout, out RequestContext context) {
            context = null;

            try {
                if (_msg != null) {
                    context = new EmailRequestContext(_msg, _mailHandler, _bindingElement);
                    _msg = null;
                    return true;
                }
                WCFLogger.Write(System.Diagnostics.TraceEventType.Information, "Received an incoming mail");
            }
            catch (TimeoutException) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Begins an asynchronous request operation that has a specified time out and
        /// state object associated with it
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies the interval of time to wait for the reception
        /// of an available request</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// receive that a request operation completes</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous receive of a request operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous operation to wait
        /// for a request message to arrive</returns>
        public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state) {
            throw new NotImplementedException("RaspEmailReplyChannel.BeginWaitForRequest() has not been implemented. Check RaspEmailReplyChannel.cs in the code base or contact the developers.");
        }

        /// <summary>
        /// Completes the specified asynchronous wait-for-a-request message operation
        /// </summary>
        /// <param name="result">The System.IAsyncResult that identifies the System.ServiceModel.Channels.IReplyChannel.BeginWaitForRequest()
        /// operation to finish, and from which to retrieve an end result</param>
        /// <returns>true if a request is received before the specified interval of time elapses;
        /// otherwise false</returns>
        public bool EndWaitForRequest(IAsyncResult result) {
            throw new NotImplementedException("RaspEmailReplyChannel.EndWaitForRequest() has not been implemented. Check RaspEmailReplyChannel.cs in the code base or contact the developers.");
        }

        /// <summary>
        /// Returns a value that indicates whether a request message is received before
        /// a specified interval of time elapses
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies how long a request operation has to complete
        /// before timing out and returning false</param>
        /// <returns>true if a request is received before the specified interval of time elapses;
        /// otherwise false</returns>
        public bool WaitForRequest(TimeSpan timeout) {
            throw new NotImplementedException("RaspEmailReplyChannel.WaitForRequest() has not been implemented. Check RaspEmailReplyChannel.cs in the code base or contact the developers.");
        }

        /// <summary>
        /// Gets the address on which this reply channel receives messages
        /// </summary>
        public System.ServiceModel.EndpointAddress LocalAddress {
            get { return new System.ServiceModel.EndpointAddress(_mailHandler.InboxServerConfiguration.ReplyAddress); }
        }


        #endregion
    }
}