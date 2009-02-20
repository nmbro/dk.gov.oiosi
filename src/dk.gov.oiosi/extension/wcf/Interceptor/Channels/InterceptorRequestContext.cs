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
using System.Diagnostics;
using System.ServiceModel.Channels;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels {
    
    /// <summary>
    /// Provides a reply that is correlated to an incoming request
    /// </summary>
    public class InterceptorRequestContext : RequestContext {
        private RequestContext _innerContext;
        private Message _message;
        private IChannelInterceptor _channelInterceptor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="innerContext">inner context</param>
        /// <param name="message">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        /// <param name="channelInterceptor">channel interceptor</param>
        public InterceptorRequestContext(RequestContext innerContext, Message message, IChannelInterceptor channelInterceptor) {
            _innerContext = innerContext;
            _message = message;
            _channelInterceptor = channelInterceptor;
        }

        /// <summary>
        /// Aborts processing the request associated with the context
        /// </summary>
        public override void Abort() {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorRequestContext abort");
            _innerContext.Abort();
        }

        /// <summary>
        /// begins an asynchronous operation to reply to the request associated with the current 
        /// context within a specified interval of time
        /// </summary>
        /// <param name="message">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        /// <param name="timeout">The System.Timespan that specifies the interval of time to wait for the reply
        /// to an available request</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// reply operation completion</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous reply operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous reply operation</returns>
        public override IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorRequestContext begins reply");
            Message interceptedMessage = InterceptResponse(message);
            return _innerContext.BeginReply(interceptedMessage, timeout, callback, state);
        }

        /// <summary>
        ///  Begins an asynchronous operation to reply to the request associated with the current context
        /// </summary>
        /// <param name="message">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// reply operation completion</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous reply operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous reply operation</returns>
        public override IAsyncResult BeginReply(Message message, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorRequestContext begins reply");
            Message interceptedMessage = InterceptResponse(message); 
            return _innerContext.BeginReply(interceptedMessage, callback, state);
        }

        /// <summary>
        /// Closes the operation that is replying to the request context associated with the 
        /// current context within a specified interval of time
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies the interval of time within which the
        /// reply operation associated with the current context must close</param>
        public override void Close(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorRequestContext closes");
            _innerContext.Close(timeout);
        }

        /// <summary>
        /// closes the operation that is replying to the request context associated with the 
        /// current context
        /// </summary>
        public override void Close() {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorRequestContext closes");
            _innerContext.Close();
        }

        /// <summary>
        /// Completes an asynchronous operation to reply to a request message
        /// </summary>
        /// <param name="result">The System.IAsyncResult returned by a call to one of the 
        /// Overload:System.ServiceModel.Channels.IRequestContext.BeginReply methods</param>
        public override void EndReply(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorRequestContext ends reply");
            _innerContext.EndReply(result);
        }

        /// <summary>
        /// Replies to a request message within a specified interval of time
        /// </summary>
        /// <param name="message">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        /// <param name="timeout">The System.Timespan that specifies the interval of time to wait for the reply
        /// to a request</param>
        public override void Reply(Message message, TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorRequestContext replies");
            Message interceptedMessage = InterceptResponse(message); 
            _innerContext.Reply(interceptedMessage, timeout);
        }

        /// <summary>
        /// Replies to a request message
        /// </summary>
        /// <param name="message">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        public override void Reply(Message message) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorRequestContext replies");
            Message interceptedMessage = InterceptResponse(message); 
            _innerContext.Reply(interceptedMessage);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public override Message RequestMessage {
            get { return _message; }
        }

        /// <summary>
        /// The method is nor operational right now because there is no need
        /// for a response interception on the response side in the current model.
        /// </summary>
        /// <param name="wcfMessage">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        /// <returns></returns>
        private Message InterceptResponse(Message wcfMessage) {
            if (wcfMessage == null) return wcfMessage;
            if (!_channelInterceptor.DoesResponseIntercept) return wcfMessage;
            InterceptorMessage message = new InterceptorMessage(wcfMessage);
            _channelInterceptor.InterceptResponse(message);
            return message.GetMessage();
        }
    }
}