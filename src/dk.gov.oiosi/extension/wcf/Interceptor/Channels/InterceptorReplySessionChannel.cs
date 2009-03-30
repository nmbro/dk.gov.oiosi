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
using System.ServiceModel.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels {
    /// <summary>
    /// Custom reply session channel implementation where the interceptor will be called.
    /// It inherits the custom reply session to gain the same interception functionality and
    /// it uses the inner channel to hold the input session.
    /// </summary>
    class InterceptorReplySessionChannel : InterceptorReplyChannel, IReplySessionChannel {
        private IReplySessionChannel _innerChannel;

        /// <summary>
        /// The constructor that takes a manager, an inner channel and an interceptor as 
        /// parameters. It uses the base contructor of the custom reply channel. Further 
        /// it uses the inner channel to gain knowledge about the session.
        /// </summary>
        /// <param name="manager">channel  manager</param>
        /// <param name="innerChannel">inner session channel</param>
        /// <param name="channelInterceptor">channel interceptor</param>
        public InterceptorReplySessionChannel(ChannelManagerBase manager, IReplySessionChannel innerChannel, IChannelInterceptor channelInterceptor)
            : base(manager, innerChannel, channelInterceptor) {
            _innerChannel = innerChannel;
        }

        #region ISessionChannel<IInputSession> Members

        /// <summary>
        /// Implementation of the IReplySessionChannel, where it returns the current
        /// session.
        /// </summary>
        public IInputSession Session {
            get { return _innerChannel.Session; }
        }

        #endregion
    }
}