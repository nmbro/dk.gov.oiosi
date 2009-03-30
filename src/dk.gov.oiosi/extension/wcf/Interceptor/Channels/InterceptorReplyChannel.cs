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
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System;
using System.Diagnostics;
using System.ServiceModel.Channels;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels {

    /// <summary>
    /// Custom reply channel implementation where the interceptor will be called.
    /// It inherits the ChannelBase that can hold the inner channel and expose standard
    /// functionality.
    /// 
    /// All functions call the innerchannel except the EndReceiveRequest and the 
    /// EndTryReceiveRequest where the interception can be done.
    /// </summary>
    /// <remarks>In case an interceptor has been added UNDER the security layer a copy of the Message object should never be done - seeing how this leads to the To header not being signed.</remarks>
    class InterceptorReplyChannel : InterceptorChannelBase<IReplyChannel>, IReplyChannel {
        private IChannelInterceptor _channelInterceptor;

        /// <summary>
        /// The constructor that takes three parameters; the channel manager, the inner channel and 
        /// the interceptor. The channel manager and the inner channel is given to the base 
        /// constructor of the ChannelBase. The interceptor is used intercept the replies.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="innerChannel"></param>
        /// <param name="channelInterceptor"></param>
        public InterceptorReplyChannel(ChannelManagerBase manager, IReplyChannel innerChannel, IChannelInterceptor channelInterceptor) : base(manager, innerChannel) {
            _channelInterceptor = channelInterceptor;
        }

        #region IReplyChannel Members
        /// <summary>
        /// Implements the BeginRecieveRequest method of the IReplyChannel interface. It bridges the
        /// call to the innner channel located in the channel base.
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies how long the receive request operation
        /// has to complete before timing out and returning false</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// receive that a request operation completes</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous receive of a request operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous receive request
        /// operation</returns>
        public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel begins recieve requet");
            return InnerChannel.BeginReceiveRequest(timeout, callback, state);
        }

        /// <summary>
        /// Implements the BeginRecieveRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base.
        /// </summary>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// receive that a request operation completes</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous receive of a request operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous reception of the
        /// request</returns>
        public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel begins recieve request");
            return InnerChannel.BeginReceiveRequest(callback, state);
        }

        /// <summary>
        /// Implements the BeginTryRecieveRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base.
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies how long the receive request operation
        /// has to complete before timing out and returning false</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// receive that a request operation completes</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous receive of a request operation</param>
        /// <returns></returns>
        public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel begins try recieve request");
            return InnerChannel.BeginTryReceiveRequest(timeout, callback, state);
        }

        /// <summary>
        /// Implements the BeginWaitForRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base.
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies the interval of time to wait for the reception
        /// of an available request</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// receive that a request operation completes</param>
        /// <param name="state">An object, specified by the application, that contains state information
        ///     associated with the asynchronous receive of a request operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous operation to wait
        /// for a request message to arrive</returns>
        public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel begins wait for request");
            return InnerChannel.BeginWaitForRequest(timeout, callback, state);
        }

        /// <summary>
        /// Implements the EndBeginRecieveRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base. Then it calls the reply interceptor
        /// so it can do whatever with the request context.
        /// </summary>
        /// <param name="result">The System.IAsyncResult returned by a call to the System.ServiceModel.Channels.IInputChannel.BeginReceive()
        /// method</param>
        /// <returns>The System.ServiceModel.Channels.RequestContext used to construct a reply
        /// to the request</returns>
        public RequestContext EndReceiveRequest(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel ends receive request");
            RequestContext innerContext = InnerChannel.EndReceiveRequest(result);
            return EndReceiveRequest(innerContext);
        }

        /// <summary>
        /// Implements the EndTryBeginRecieveRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base. Then it calls the reply interceptor
        /// so it can do whatever with the request context.
        /// </summary>
        /// <param name="result">The System.IAsyncResult returned by a call to the System.ServiceModel.Channels.IReplyChannel.BeginTryReceiveRequest()
        /// method</param>
        /// <param name="context">The System.ServiceModel.Channels.RequestContext received</param>
        /// <returns>true if a request message is received before the specified interval of time
        /// elapses; otherwise false</returns>
        public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel ends try receive request");
            bool success = InnerChannel.EndTryReceiveRequest(result, out context);
            if (context == null) return success;
            context = EndReceiveRequest(context);
            return context != null;
        }

        /// <summary>
        /// Implements the EndWaitForRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base.
        /// </summary>
        /// <param name="result">The System.IAsyncResult that identifies the System.ServiceModel.Channels.IReplyChannel.BeginWaitForRequest()
        /// operation to finish, and from which to retrieve an end result</param>
        /// <returns>true if a request is received before the specified interval of time elapses;
        /// otherwise false</returns>
        public bool EndWaitForRequest(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel ends wait for request");
            return InnerChannel.EndWaitForRequest(result);
        }

        /// <summary>
        /// Implements the LocalAddress get property of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base.
        /// </summary>
        public System.ServiceModel.EndpointAddress LocalAddress {
            get { return InnerChannel.LocalAddress; }
        }

        /// <summary>
        /// Implements the RecieveRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base.
        /// </summary>
        /// <param name="timeout">The System.TimeSpan that specifies how long the receive of a request operation
        /// has to complete before timing out and returning false</param>
        /// <returns>The System.ServiceModel.Channels.RequestContext used to construct replies</returns>
        public RequestContext ReceiveRequest(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel receives request");
            return InnerChannel.ReceiveRequest(timeout);
        }

        /// <summary>
        /// Implements the RecieveRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base.
        /// </summary>
        /// <returns>The System.ServiceModel.Channels.RequestContext used to construct replies</returns>
        public RequestContext ReceiveRequest() {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel receives request");
            return InnerChannel.ReceiveRequest();
        }

        /// <summary>
        /// Implements the TryRecieveRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base.
        /// </summary>
        /// <param name="timeout">The System.TimeSpan that specifies how long the receive of a request operation
        /// has to complete before timing out and returning false</param>
        /// <param name="context">The System.ServiceModel.Channels.RequestContext received</param>
        /// <returns>true if a request message is received before the specified interval of time
        /// elapses; otherwise false</returns>
        public bool TryReceiveRequest(TimeSpan timeout, out RequestContext context) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel tries to receive request");
            return InnerChannel.TryReceiveRequest(timeout, out context);
        }

        /// <summary>
        /// Implements the WaitForRequest method of the IReplyChannel interface. It bridges the
        /// call to the inner channel located in the channel base.
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies how long a request operation has to complete
        /// before timing out and returning false</param>
        /// <returns>true if a request is received before the specified interval of time elapses;
        /// otherwise false</returns>
        public bool WaitForRequest(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel waits for request");
            return InnerChannel.WaitForRequest(timeout);
        }

        #endregion

        private RequestContext EndReceiveRequest(RequestContext innerContext) {
            if (innerContext == null) return null;
            if (innerContext.RequestMessage == null) return innerContext;
            if (!_channelInterceptor.DoesRequestIntercept) return innerContext;
            
            Message wcfMessage = innerContext.RequestMessage;
            RequestContext wrappedContext;
            InterceptorMessage message = new InterceptorMessage(wcfMessage);
            try {
                _channelInterceptor.InterceptRequest(message);
                wrappedContext = new InterceptorRequestContext(innerContext, message.GetMessage(), _channelInterceptor);
                return wrappedContext;
            }
            catch (InterceptorChannelException iifex) {
                WCFLogger.Write(System.Diagnostics.TraceEventType.Warning, "Exception occurred in interceptor");
                return HandleException(iifex, innerContext, message);
            }
            catch (Exception ex) {
                WCFLogger.Write(System.Diagnostics.TraceEventType.Warning, "Exception occurred in interceptor");
                InterceptorInternalFailureException iifex = new InterceptorInternalFailureException(ex);
                return HandleException(iifex, innerContext, message);
            }
        }

        private RequestContext HandleException(InterceptorChannelException icex, RequestContext innerContext, InterceptorMessage message) {
            WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Interceptor is starting to handle a thrown exception...");
            try {
                if (_channelInterceptor.DoesFaultOnRequestException) {
                    SendSoapFault(icex, innerContext, message);
                    return null;
                }

                RequestContext wrappedContext = new InterceptorRequestContext(innerContext, message.GetMessage(), _channelInterceptor);
                object exceptions = null;
                InterceptorChannelExceptionCollection exceptionCollection = null;
                if (!wrappedContext.RequestMessage.Properties.TryGetValue("Exceptions", out exceptions)) {
                    exceptions = new InterceptorChannelExceptionCollection();
                    wrappedContext.RequestMessage.Properties.Add("Exceptions", exceptions);
                }
                exceptionCollection = (InterceptorChannelExceptionCollection)exceptions;
                exceptionCollection.Add(icex);
                WCFLogger.Write(System.Diagnostics.TraceEventType.Information, "Interceptor added an exception to the message: " + icex.Message);
                WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Interceptor is finished handling the thrown exception.");
                return wrappedContext;
            } 
            catch (Exception ex) {

                WCFLogger.Write(System.Diagnostics.TraceEventType.Error, "An error occurred when trying to handle an exception in the Interceptor: " + ex);
                WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Interceptor is finished handling the thrown exception.");
                return null;
            }
        }

        private void SendSoapFault(InterceptorChannelException ex, RequestContext innerContext, InterceptorMessage message) {
            WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Interceptor is sending a SOAP fault...");        
            MessageFault messageFault = ex.GetMessageFault();
            Message wcfMessage = Message.CreateMessage(MessageVersion.Default, messageFault, common.Definitions.DefaultOiosiNamespace2007 + messageFault.Code.SubCode.Name);
            innerContext.Reply(wcfMessage);
            WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Interceptor finished sending the SOAP fault.");        
        }
    }
}