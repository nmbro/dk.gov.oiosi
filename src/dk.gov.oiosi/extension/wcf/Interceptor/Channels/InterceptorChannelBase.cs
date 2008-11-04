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
using System.ServiceModel;
using System.ServiceModel.Channels;

using dk.gov.oiosi.exception;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels {
    /// <summary>
    /// Base channel class that inherits the ChannelBase of WCF. It stores the inner
    /// channel and implements the needed functions from the ChannelBase of WCF.
    /// </summary>
    public class InterceptorChannelBase<TChannel> : ChannelBase where TChannel : class, IChannel {
        private TChannel _innerChannel;

        /// <summary>
        /// Constructor that takes a channel manager and an inner channel as parameter. The manager
        /// is used by its base class ans the inner channel is as a class variable..
        /// </summary>
        /// <param name="manager">channel manager</param>
        /// <param name="innerChannel">inner channel</param>
        protected InterceptorChannelBase(ChannelManagerBase manager, TChannel innerChannel)
            : base(manager) {
            if (innerChannel == null) throw new NullArgumentException("innerChannel");
            _innerChannel = innerChannel;
        }

        /// <summary>
        /// Gets the inner channel of the channel base.
        /// </summary>
        public TChannel InnerChannel {
            get { return _innerChannel; }
        }

        #region ChannelBase implementation

        /// <summary>
        /// Overides the GetProperty function of the BaseChannel of WCF. It bridges the call to the
        /// inner channel.
        /// </summary>
        /// <typeparam name="T">typed object</typeparam>
        /// <returns>The typed object T requested if it is present or null if it is not</returns>
        public override T GetProperty<T>() {
            T baseProperty = base.GetProperty<T>();
            if (baseProperty != null) return baseProperty;
            return _innerChannel.GetProperty<T>();
        }

        /// <summary>
        /// Overides the OnAbort function of the BaseChannel of WCF. It bridges the call to the
        /// inner channel.
        /// </summary>
        protected override void OnAbort() {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelBase was aborted");
            _innerChannel.Abort();
        }

        /// <summary>
        /// Overides the OnBeginClose function of the BaseChannel of WCF. It bridges the call to the
        /// inner channel.
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelBase begins close");
            return _innerChannel.BeginClose(timeout, callback, state);
        }

        /// <summary>
        /// Overides the OnBeginOpen function of the BaseChannel of WCF. It bridges the call to the
        /// inner channel.
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelBase begins open");
            return _innerChannel.BeginOpen(timeout, callback, state);
        }

        /// <summary>
        /// Overides the OnClose function of the BaseChannel of WCF. It bridges the call to the
        /// inner channel.
        /// </summary>
        /// <param name="timeout"></param>
        protected override void OnClose(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelBase closes");
            _innerChannel.Close(timeout);
        }

        /// <summary>
        /// Overides the OnEndClose function of the BaseChannel of WCF. It bridges the call to the
        /// inner channel.
        /// </summary>
        /// <param name="result"></param>
        protected override void OnEndClose(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelBase ends close");
            _innerChannel.EndClose(result);
        }

        /// <summary>
        /// Overides the OnEndOpen function of the BaseChannel of WCF. It bridges the call to the
        /// inner channel.
        /// </summary>
        /// <param name="result"></param>
        protected override void OnEndOpen(IAsyncResult result) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelBase ends open");
            _innerChannel.EndOpen(result);
        }

        /// <summary>
        /// Overides the OnOpen function of the BaseChannel of WCF. It bridges the call to the
        /// inner channel.
        /// </summary>
        /// <param name="timeout"></param>
        protected override void OnOpen(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Verbose, "InterceptorChannelBase opens");
            _innerChannel.Open(timeout);
        }
        #endregion
    }
}