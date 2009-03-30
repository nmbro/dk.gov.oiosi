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
using System.Xml;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.Header {

    /// <summary>
    /// Represents a sequence header
    /// </summary>
    public class SequenceHeader : Header {
        private string _sequenceId;
        private long _messageNumber;
        private bool _isLastMessage;
        private MessageHeader _sequenceHeader;

        /// <summary>
        /// Constructor. Takes a message sequence header.
        /// </summary>
        /// <param name="sequenceHeader">The sequence header</param>
        public SequenceHeader(MessageHeader sequenceHeader) {
            _sequenceHeader = sequenceHeader;

            XmlDocument headerDocument = new XmlDocument();
            headerDocument.LoadXml(_sequenceHeader.ToString());

            _sequenceId = GetElementValueFromTagName(headerDocument, "Identifier");
            
            string messageNumberString = GetElementValueFromTagName(headerDocument, "MessageNumber");
            _messageNumber = long.Parse(messageNumberString);

            _isLastMessage = ContainsElementFromTagName(headerDocument, "LastMessage");
        }

        /// <summary>
        /// Gets the sequence ID
        /// </summary>
        public string SequenceId {
            get { return _sequenceId; }
        }

        /// <summary>
        /// Gets the message number
        /// </summary>
        public long MessageNumber {
            get { return _messageNumber; }
        }

        /// <summary>
        /// True if this message represents the last message
        /// </summary>
        public bool IsLastMessage {
            get { return _isLastMessage; }
        }
    }
}
