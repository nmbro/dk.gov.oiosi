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
using System.ServiceModel.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels {
    
    /// <summary>
    /// Interceptor request session channel
    /// </summary>
    class InterceptorRequestSessionChannel : InterceptorRequestChannel, IRequestSessionChannel {
        private IRequestSessionChannel _innerChannel;
        public InterceptorRequestSessionChannel(ChannelManagerBase manager, IRequestSessionChannel innerChannel, IChannelInterceptor channelInterceptor)
            : base(manager, innerChannel, channelInterceptor) {
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
            if (State == System.ServiceModel.CommunicationState.Faulted) return;
            Fault();
        }
    }
}