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
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace dk.gov.oiosi.raspProfile.extension.wcf.Interceptor.CustomHeader {
    
    /// <summary>
    /// Used to configure the PartyIdentifier headers run-time. 
    /// Should be added as a property on the Message object before sending it down the communication stack
    /// </summary>
    public class PartyIdentifierHeaderSettings {

        /// <summary>
        /// Message property key string
        /// </summary>
        public const string MessagePropertyKey = "partyIdentifierHeaderSettings";

        private string _senderPartyHeaderValue;
        private string _receiverPartyHeaderValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="senderPartyHeaderValue">The value of the Sender party ID header</param>
        /// <param name="receiverPartyHeaderValue">The value of the Receiver party ID header</param>
        public PartyIdentifierHeaderSettings(string senderPartyHeaderValue, string receiverPartyHeaderValue) {
            _senderPartyHeaderValue = senderPartyHeaderValue;
            _receiverPartyHeaderValue = receiverPartyHeaderValue;
        }

        /// <summary>
        /// The value of the Sender party ID header
        /// </summary>
        public string SenderPartyHeaderValue {
            get { return _senderPartyHeaderValue; }
            set { _senderPartyHeaderValue = value; }
        }

        /// <summary>
        /// The value of the Receiver party ID header
        /// </summary>
        public string ReceiverPartyHeaderValue {
            get { return _receiverPartyHeaderValue; }
            set { _receiverPartyHeaderValue = value; }
        }
    }
}
