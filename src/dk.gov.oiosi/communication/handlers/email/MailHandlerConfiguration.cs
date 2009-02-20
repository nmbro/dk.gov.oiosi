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

namespace dk.gov.oiosi.communication.handlers.email {

    /// <summary>
    /// Mail handler configuration
    /// </summary>
    public class MailHandlerConfiguration : IMailHandlerConfiguration {
        private Type _outBoxImplementationType;
        private Type _inBoxImplementationType;
        private IMailServerConfiguration _sendingServerConfiguration;
        private IMailServerConfiguration _recievingServerConfiguration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outBoxImplementationType">The type of the outbox implementation</param>
        /// <param name="inBoxImplementationType">The type of the inbox implementation</param>
        /// <param name="sendingSeverConfiguration">The configuration of the sending server</param>
        /// <param name="recievingServerConfiguration">The configuration of the receiving server</param>
        public MailHandlerConfiguration
                (Type outBoxImplementationType,
                 Type inBoxImplementationType,
                 IMailServerConfiguration sendingSeverConfiguration,
                 IMailServerConfiguration recievingServerConfiguration) {
            _outBoxImplementationType = outBoxImplementationType;
            _inBoxImplementationType = inBoxImplementationType;
            _sendingServerConfiguration = sendingSeverConfiguration;
            _recievingServerConfiguration = recievingServerConfiguration;
        }

        #region IMailHandlerConfiguration Members

        /// <summary>
        /// The type of the oubox implementation
        /// </summary>
        public Type OutBoxImplementationType {
            get { return _outBoxImplementationType; }
            set { _outBoxImplementationType = value; }
        }

        /// <summary>
        /// The type of the inbox implementation
        /// </summary>
        public Type InBoxImplementationType {
            get { return _inBoxImplementationType; }
            set { _inBoxImplementationType = value; }
        }

        /// <summary>
        /// The configuration of the sending server
        /// </summary>
        public IMailServerConfiguration SendingServerConfiguration {
            get { return _sendingServerConfiguration; }
            set { _sendingServerConfiguration = value; }
        }

        /// <summary>
        /// The configuration of the receiving server
        /// </summary>
        public IMailServerConfiguration RecievingServerConfiguration {
            get { return _recievingServerConfiguration; }
            set { _recievingServerConfiguration = value; }
        }

        #endregion
    }
}
