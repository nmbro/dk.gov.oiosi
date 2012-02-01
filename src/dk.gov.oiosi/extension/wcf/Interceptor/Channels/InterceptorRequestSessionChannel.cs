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
  *   Dennis S�gaard, Accenture
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
    /// Interceptor request session channel
    /// </summary>
    class InterceptorRequestSessionChannel : InterceptorRequestChannel, IRequestSessionChannel {
        private IRequestSessionChannel _innerChannel;
        public InterceptorRequestSessionChannel(ChannelManagerBase manager, IRequestSessionChannel innerChannel, IChannelInterceptor channelInterceptor)
            : base(manager, innerChannel, channelInterceptor)
        {
            _innerChannel = innerChannel;
        }

        #region ISessionChannel<IOutputSession> Members

        /// <summary>
        /// Property for session
        /// </summary>
        public IOutputSession Session {
            get { return _innerChannel.Session; }
        }

        #endregion

        protected override void HandleException(Message message) {
            base.HandleException(message);
            if (State == System.ServiceModel.CommunicationState.Faulted)
            {
                // The base class has handle the exception
            }
            else
            {
                base.Fault();
            }
        }
    }
}