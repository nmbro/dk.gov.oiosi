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
    /// Interceptor request channel
    /// </summary>
    /// <remarks>In case an interceptor has been added UNDER the security layer a copy of the Message object should never be done - seeing how this leads to the To header not being signed.</remarks>
    class InterceptorRequestChannel : InterceptorChannelBase<IRequestChannel>, IRequestChannel
    {
        private IChannelInterceptor channelInterceptor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">channel manager</param>
        /// <param name="innerChannel">inner channel</param>
        /// <param name="channelInterceptor">channel interceptor</param>
        public InterceptorRequestChannel(ChannelManagerBase manager, IRequestChannel innerChannel, IChannelInterceptor channelInterceptor)
            : base(manager, innerChannel)
        {
            this.channelInterceptor = channelInterceptor;
        }


        #region IRequestChannel Members

        /// <summary>
        /// BeginRequest implementation of IRequest channel
        /// </summary>
        /// <param name="message">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <param name="timeout">The System.TimeSpan that specifies the interval of time within which a response
        /// must be received</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the completion
        ///     of the asynchronous operation transmitting a request message</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous operation transmitting a request message</param>
        /// <returns>The System.IAsyncResult that references the asynchronous message transmission</returns>
        public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorReplyChannel begins request");
            ThrowIfDisposedOrNotOpen();
            try {
                Message interceptedMessage = InterceptRequest(message);
                return InnerChannel.BeginRequest(interceptedMessage, timeout, callback, state);
            }
            catch (Exception ex) {
                WCFLogger.Write(TraceEventType.Error, "InterceptorReplyChannel exception thrown " + ex);
                HandleException(message);
                throw ex;
            }
        }

        /// <summary>
        /// Begins an asynchronous operation to transmit a request message to the reply-side
        /// of a request-reply message exchange
        /// </summary>
        /// <param name="message">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the completion
        ///     of the asynchronous operation transmitting a request message</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous operation transmitting a request message</param>
        /// <returns>The System.IAsyncResult that references the asynchronous message transmission</returns>
        public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state) 
        {
            base.ThrowIfDisposedOrNotOpen();
            IAsyncResult asyncResult = null;
            try
            {
                WCFLogger.Write(TraceEventType.Start, "Beginning to intercept request");
                Message interceptedMessage = InterceptRequest(message);
                WCFLogger.Write(TraceEventType.Stop, "Finished intercepting request");
                asyncResult = InnerChannel.BeginRequest(interceptedMessage, callback, state);
            }
            catch (Exception ex) {
                HandleException(message);
                WCFLogger.Write(TraceEventType.Error, "Exception occurred while intercepting: " + ex);
                throw ex;
            }

            return asyncResult;
        }


        /// <summary>
        /// Completes an asynchronous operation to return a message-based response to
        /// a transmitted request
        /// </summary>
        /// <param name="result">The System.IAsyncResult returned by a call to the System.ServiceModel.Channels.IInputChannel.BeginRequest()
        /// method</param>
        /// <returns>The System.ServiceModel.Channels.Message received in response to the request</returns>
        public Message EndRequest(IAsyncResult result)
        {
            Message innerMessage = InnerChannel.EndRequest(result);
            WCFLogger.Write(TraceEventType.Start, "Beginning to intercept response");
            Message interceptedMessage = InterceptResponse(innerMessage);
            WCFLogger.Write(TraceEventType.Stop, "Finished intercepting response");
            return interceptedMessage;
        }

        /// <summary>
        /// Gets the remote address to which the request channel sends messages
        /// </summary>
        public System.ServiceModel.EndpointAddress RemoteAddress {
            get { return InnerChannel.RemoteAddress; }
        }

        /// <summary>
        ///  Sends a message-based request and returns the correlated message-based response
        ///  within a specified interval of time
        /// </summary>
        /// <param name="message">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <param name="timeout">The System.TimeSpan that specifies the interval of time within which a response
        /// must be received</param>
        /// <returns>The System.ServiceModel.Channels.Message received in response to the request</returns>
        public Message Request(Message message, TimeSpan timeout) 
        {
            WCFLogger.Write(TraceEventType.Start, "Beginning to intercept request");
            base.ThrowIfDisposedOrNotOpen();

            Message interceptedResponseMessage = null;
            try 
            {
                Message interceptedRequestMessage = InterceptRequest(message);
                WCFLogger.Write(TraceEventType.Verbose, "Interceptor Requesting");
                Message innerMessage = InnerChannel.Request(interceptedRequestMessage, timeout);
                interceptedResponseMessage = InterceptResponse(innerMessage);
                WCFLogger.Write(TraceEventType.Stop, "Finished intercepting request");
            }
            catch (Exception ex)
            {
                HandleException(message);
                WCFLogger.Write(TraceEventType.Error, "Exception occurred while intercepting: " + ex);
                throw ex;
            }

            return interceptedResponseMessage;
        }

        /// <summary>
        /// Sends a message-based request and returns the correlated message-based response
        /// </summary>
        /// <param name="message">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <returns>The System.ServiceModel.Channels.Message received in response to the request</returns>
        public Message Request(Message message)
        {
            WCFLogger.Write(TraceEventType.Start, "Beginning to intercept request");
            base.ThrowIfDisposedOrNotOpen();
            Message interceptedResponseMessage = null;
            try
            {
                Message interceptedRequestMessage = InterceptRequest(message);
                Message innerMessage = InnerChannel.Request(interceptedRequestMessage);
                interceptedResponseMessage = InterceptResponse(innerMessage);
                WCFLogger.Write(TraceEventType.Stop, "Finished intercepting request");
            }
            catch (Exception ex)
            {
                HandleException(message);
                WCFLogger.Write(TraceEventType.Error, "Exception occurred while intercepting: " + ex);
                WCFLogger.Write(TraceEventType.Stop, "Finished intercepting request");
                throw ex;
            }

            return interceptedResponseMessage;
        }

        /// <summary>
        /// Gets the transport address to which the request is send
        /// </summary>
        public Uri Via 
        {
            get 
            {
                return InnerChannel.Via;
            }
        }

        protected override void OnAbort() 
        {
            base.OnAbort();
        }

        protected override void OnFaulted()
        {
            WCFLogger.Write(TraceEventType.Warning, "Interceptor going into faulted state");
            base.OnFaulted();
        }

        #endregion

        protected virtual void HandleException(Message message)
        {
            message.Close();
            base.Fault();
           
        }

        /// <summary>
        /// If an exception is thrown it will not be caugth by the method but it will
        /// propegate back and stop the sending of the request.
        /// </summary>
        /// <param name="wcfMessage">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <returns>the intercepted message</returns>
        private Message InterceptRequest(Message wcfMessage)
        {
            Message interceptedWcfMessage = null;
            if (wcfMessage == null)
            {
                interceptedWcfMessage = null;
            }
            else if (!this.channelInterceptor.DoesRequestIntercept)
            {
                WCFLogger.Write(TraceEventType.Information, "Interceptor ignored request");
                interceptedWcfMessage = wcfMessage;
            }
            else
            {
                InterceptorMessage message = new InterceptorMessage(wcfMessage);
                this.channelInterceptor.InterceptRequest(message);
                interceptedWcfMessage = message.GetMessage();
            }

            return interceptedWcfMessage;
        }

        /// <summary>
        /// The method is not functinal right now becuase there is no
        /// need for response interception in the current model
        /// </summary>
        /// <param name="wcfMessage">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <returns>response interception</returns>
        private Message InterceptResponse(Message wcfMessage)
        {
            Message message;

            if (wcfMessage == null)
            {
                message = null;
            }
            else if (!this.channelInterceptor.DoesResponseIntercept)
            {
                WCFLogger.Write(TraceEventType.Information, "Interceptor ignored response");
                message = wcfMessage;
            }
            else
            {
                InterceptorMessage interceptorMessage = new InterceptorMessage(wcfMessage);
                this.channelInterceptor.InterceptResponse(interceptorMessage);
                message = interceptorMessage.GetMessage();
            }

            return message;
        }
    }
}