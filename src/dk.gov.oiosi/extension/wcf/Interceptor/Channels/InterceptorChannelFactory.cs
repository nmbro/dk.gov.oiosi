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
    /// Factory for interceptor channels
    /// </summary>
    /// <typeparam name="TChannel">channel</typeparam>
    public class InterceptorChannelFactory<TChannel> : ChannelFactoryBase<TChannel> {
        private IChannelFactory<TChannel> _innerChannelFactory;
        private IChannelInterceptor _channelInterceptor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">binding context</param>
        /// <param name="channelInterceptor">channel interceptor</param>
        public InterceptorChannelFactory(BindingContext context, IChannelInterceptor channelInterceptor) {
            _innerChannelFactory = context.BuildInnerChannelFactory<TChannel>();
            _channelInterceptor = channelInterceptor;
        }

        /// <summary>
        /// Gets a property
        /// </summary>
        /// <typeparam name="T">parameter</typeparam>
        /// <returns>property</returns>
        public override T GetProperty<T>() {
            T baseProperty = base.GetProperty<T>();
            if (baseProperty != null) return baseProperty;
            return _innerChannelFactory.GetProperty<T>();
        }

        /// <summary>
        /// Creates the channel.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="via"></param>
        /// <returns></returns>
        protected override TChannel OnCreateChannel(System.ServiceModel.EndpointAddress address, Uri via) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelFactory creates channel");
            TChannel innerChannel = _innerChannelFactory.CreateChannel(address, via);
            if (innerChannel == null) return default(TChannel);
            if (_channelInterceptor == null) return innerChannel;
            if (typeof(TChannel) == typeof(IRequestChannel))
                return (TChannel)(IChannel)new InterceptorRequestChannel(this, (IRequestChannel)innerChannel, _channelInterceptor);
            if (typeof(TChannel) == typeof(IRequestSessionChannel))
                return (TChannel)(IChannel)new InterceptorRequestSessionChannel(this, (IRequestSessionChannel)innerChannel, _channelInterceptor);
            throw new UnsupportedChannelTypeException(typeof(TChannel));
        }

        /// <summary>
        /// Overriden, let the inner channel factory handle the call.
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelFactory begins open");
            return _innerChannelFactory.BeginOpen(timeout, callback, state);
        }

        /// <summary>
        /// Overriden, let the inner channel factory handle the call.
        /// </summary>
        /// <param name="result"></param>
        protected override void OnEndOpen(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelFactory ends open");
            _innerChannelFactory.EndOpen(result);
        }

        /// <summary>
        /// Overridden, let the inner channel factory handle the call.
        /// </summary>
        /// <param name="timeout"></param>
        protected override void OnOpen(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelFactory opens");
            _innerChannelFactory.Open(timeout);
        }
    }
}