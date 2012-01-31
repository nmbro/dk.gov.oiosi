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
    /// It only supports two channel types; the reply channel and the request 
    /// channel. There are no interceptors in other channel types.
    /// </summary>
    /// <typeparam name="TChannel">parameter name</typeparam>
    public class ChannelListener<TChannel> : ChannelListenerBase<TChannel> where TChannel : class, IChannel {
        private BindingContext _context;
        private IChannelListener<TChannel> _innerChannelListener;
        private IChannelInterceptor _channelInterceptor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">binding context</param>
        /// <param name="ChannelInterceptor">channel interceptor</param>
        public ChannelListener(BindingContext context, IChannelInterceptor ChannelInterceptor) {
            _context = context;
            _innerChannelListener = context.BuildInnerChannelListener<TChannel>();
            _channelInterceptor = ChannelInterceptor;
        }

        #region ChannelListenerBase Overrides

        /// <summary>
        /// Gets a property
        /// </summary>
        /// <typeparam name="T">parameter name</typeparam>
        /// <returns>property value</returns>
        public override T GetProperty<T>() {
            T baseProperty = base.GetProperty<T>();
            if (baseProperty != null) return baseProperty;
            return _innerChannelListener.GetProperty<T>();
        }

        /// <summary>
        /// OnAcceptChannel event
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected override TChannel OnAcceptChannel(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener accepts channel");
            TChannel innerChannel = _innerChannelListener.AcceptChannel(timeout);
            return WrapChannel(innerChannel);
        }

        /// <summary>
        /// OnBeginAcceptChannel event
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener begins accept channel");
            return _innerChannelListener.BeginAcceptChannel(timeout, callback, state);
        }

        /// <summary>
        /// OnEndAcceptChannel event
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override TChannel OnEndAcceptChannel(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener ends accept channel");
            TChannel innerChannel = _innerChannelListener.EndAcceptChannel(result);
            return WrapChannel(innerChannel);
        }

        /// <summary>
        /// OnBeginWaitForChannel event
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener begins wait for channel");
            return _innerChannelListener.BeginWaitForChannel(timeout, callback, state);
        }

        /// <summary>
        /// OnEndWaitForChannel
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override bool OnEndWaitForChannel(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener ends wait for channel");
            return _innerChannelListener.EndWaitForChannel(result);
        }

        /// <summary>
        /// OnWaitForChannel
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected override bool OnWaitForChannel(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener waits for channel");
            return _innerChannelListener.WaitForChannel(timeout);
        }

        /// <summary>
        /// Override Uri returning uri for channellistener
        /// </summary>
        public override Uri Uri {
            get { return _innerChannelListener.Uri; }
        }

        /// <summary>
        /// OnAbort event
        /// </summary>
        protected override void OnAbort() {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener aborts");
            _innerChannelListener.Abort();
        }

        /// <summary>
        /// OnBeginClose event
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state) {
            return _innerChannelListener.BeginClose(timeout, callback, state);
        }

        /// <summary>
        /// OnBeginOpen event
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener begins open");
            return _innerChannelListener.BeginOpen(timeout, callback, state);
        }

        /// <summary>
        /// OnClose event
        /// </summary>
        /// <param name="timeout"></param>
        protected override void OnClose(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener closes");
            _innerChannelListener.Close(timeout);
        }

        /// <summary>
        /// OnEndClose event
        /// </summary>
        /// <param name="result"></param>
        protected override void OnEndClose(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener ends close");
            _innerChannelListener.EndClose(result);
        }

        /// <summary>
        /// OnEndOpen event
        /// </summary>
        /// <param name="result"></param>
        protected override void OnEndOpen(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener ends open");
            _innerChannelListener.EndOpen(result);
        }

        /// <summary>
        /// OnOpen event
        /// </summary>
        /// <param name="timeout"></param>
        protected override void OnOpen(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "ChannelListener opens");
            _innerChannelListener.Open(timeout);
        }
        #endregion

        private TChannel WrapChannel(TChannel innerChannel) 
        {
            TChannel channel;

            if (innerChannel == null)
            {
                channel  = null;
            }
            else if (_channelInterceptor != null && typeof(TChannel) == typeof(IReplyChannel))
            {
                InterceptorReplyChannel interceptorReplyChannel = new InterceptorReplyChannel(this, (IReplyChannel)innerChannel, _channelInterceptor);
                //interceptorReplyChannel.Faulted += new EventHandler(interceptorReplyChannel_Faulted);
                channel = (TChannel)(IChannel)interceptorReplyChannel;
            }
            else if (_channelInterceptor != null && typeof(TChannel) == typeof(IReplySessionChannel))
            {
                InterceptorReplySessionChannel interceptorReplySessionChannel = new InterceptorReplySessionChannel(this, (IReplySessionChannel)innerChannel, _channelInterceptor);
                //interceptorReplySessionChannel.Faulted += new EventHandler(interceptorReplySessionChannel_Faulted);
                channel = (TChannel)(IChannel)interceptorReplySessionChannel;
            }
            else
            {
                throw new UnsupportedChannelTypeException(typeof(TChannel));
            }

            return channel;
        }
    }
}