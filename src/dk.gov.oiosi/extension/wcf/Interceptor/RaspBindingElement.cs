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
using System.Text;

using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor {
    /// <summary>
    /// Implementation of the basic features needed by a binding element.
    /// </summary>
    public abstract class RaspBindingElement : BindingElement, IChannelInterceptor {
        /// <summary>
        /// Returns a typed object requested, if present, from the appropriate layer in the binding stack
        /// </summary>
        /// <typeparam name="T">The typed object T</typeparam>
        /// <param name="context">The System.ServiceModel.Channels.BindingContext for the binding element</param>
        /// <returns>The typed object T requested if it is present or null if it is not</returns>
        public override T GetProperty<T>(BindingContext context) {
            return context.GetInnerProperty<T>();
        }

        /// <summary>
        /// Returns a value that indicates whether the binding element can build a channel
        /// factory for a specific type of channel
        /// </summary>
        /// <typeparam name="TChannel">channel to build</typeparam>
        /// <param name="context">The System.ServiceModel.Channels.BindingContext that provides context for
        /// the binding element</param>
        /// <returns>true if the System.ServiceModel.Channels.IChannelFactory&lt;TChannel&gt; of type
        /// TChannel can be built by the binding element; otherwise, false</returns>
        public override bool CanBuildChannelFactory<TChannel>(BindingContext context) {
            return context.CanBuildInnerChannelFactory<TChannel>();
        }

        /// <summary>
        /// Returns a value that indicates whether the binding element can build a channel
        /// listener for a specific type of channel
        /// </summary>
        /// <typeparam name="TChannel">channel to build</typeparam>
        /// <param name="context">The System.ServiceModel.Channels.BindingContext that provides context for
        /// the binding element</param>
        /// <returns>true if the System.ServiceModel.Channels.IChannelFactory&lt;TChannel&gt; of type
        /// TChannel can be built by the binding element; otherwise, false</returns>
        public override bool CanBuildChannelListener<TChannel>(BindingContext context) {
            return context.CanBuildInnerChannelListener<TChannel>();
        }

        /// <summary>
        /// IChannelFactory override
        /// </summary>
        /// <typeparam name="TChannel">channel to build</typeparam>
        /// <param name="context">The System.ServiceModel.Channels.BindingContext that provides context for
        /// the binding element</param>
        /// <returns>The factory</returns>
        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context) {
            return new InterceptorChannelFactory<TChannel>(context, this);
        }

        /// <summary>
        /// Returns the relevant channel listener
        /// </summary>
        /// <typeparam name="TChannel">channel to build</typeparam>
        /// <param name="context">The System.ServiceModel.Channels.BindingContext that provides context for
        /// the binding element</param>
        /// <returns>Returns the relevant channel listener</returns>
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context) {
            return new ChannelListener<TChannel>(context, this);
        }

        #region IChannelInterceptor Members
        /// <summary>
        /// Validating interceptors must implement this interface. interceptorMessage is the
        /// wrapped intercepted message from the request.
        /// </summary>
        /// <param name="interceptorMessage">Wrapped intercepted message</param>
        public abstract void InterceptRequest(InterceptorMessage interceptorMessage);
        /// <summary>
        /// Validating interceptors must implement this interface. interceptorMessage is the
        /// wrapped intercepted message from the response. Not in use in the asynchronous profile
        /// </summary>
        /// <param name="interceptorMessage">Wrapped intercepted message</param>
        public abstract void InterceptResponse(InterceptorMessage interceptorMessage);
        /// <summary>
        /// True if configured to intercept the request
        /// </summary>
        public abstract bool DoesRequestIntercept { get; }
        /// <summary>
        /// True if configured to intercept the response
        /// </summary>
        public abstract bool DoesResponseIntercept { get; }
        /// <summary>
        /// True if interceptor should return a fault upon validation errors
        /// </summary>
        public abstract bool DoesFaultOnRequestException { get; }

        #endregion
    }
}
