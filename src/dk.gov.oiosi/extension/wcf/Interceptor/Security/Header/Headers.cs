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
using System.Xml;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.Header {
    /// <summary>
    /// Represents known and easy to access headers of an wcf message headers.
    /// </summary>
    public class Headers {
        private UniqueId _messageId;
        private UniqueId _relatesTo;
        private SequenceHeader _sequenceHeader;
        private SequenceAcknowledgementHeader _sequenceAcknowledgementHeader;
        private SecurityHeader _securityHeader;
        private bool _isCreateSequenceResponse;
        private bool _isCreateSequence;

        /// <summary>
        /// Constructor that takes the interceptor message as parameter.
        /// </summary>
        /// <param name="message"></param>
        public Headers(InterceptorMessage message) {
            MessageHeaders headers = message.GetHeaders();
            _messageId = headers.MessageId;
            _relatesTo = headers.RelatesTo;
            _isCreateSequence = headers.Action.Contains("CreateSequence");
            _isCreateSequenceResponse = headers.Action.Contains("CreateSequenceResponse");
            SetHeaders(headers);
        }

        private void SetHeaders(MessageHeaders headers) {
            foreach (MessageHeader currentHeader in headers) {
                switch (currentHeader.Name) {
                    case "Sequence":
                        _sequenceHeader = new SequenceHeader(currentHeader);
                        break;
                    case "SequenceAcknowledgement":
                        _sequenceAcknowledgementHeader = new SequenceAcknowledgementHeader(currentHeader);
                        break;
                    case "Security":
                        _securityHeader = new SecurityHeader(currentHeader);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the message id.
        /// </summary>
        public UniqueId MessageId {
            get { return _messageId; }
        }

        /// <summary>
        /// Gets the relates to id.
        /// </summary>
        public UniqueId RelatesTo {
            get { return _relatesTo; }
        }

        /// <summary>
        /// Gets whether the message is a reliable messaging create sequence.
        /// </summary>
        public bool IsCreateSequence {
            get { return _isCreateSequence; }
        }

        /// <summary>
        /// Gets whether the message is a reliable messaging create sequence response.
        /// </summary>
        public bool IsCreateSequenceResponse {
            get { return _isCreateSequenceResponse; }
        }

        /// <summary>
        /// Gets the sequence header. If none exists null is returned.
        /// </summary>
        public SequenceHeader SequenceHeader {
            get { return _sequenceHeader; }
        }

        /// <summary>
        /// Gets the sequence acknowledgement header. If none exists null is returned.
        /// </summary>
        public SequenceAcknowledgementHeader SequenceAcknowledgement {
            get { return _sequenceAcknowledgementHeader; }
        }

        /// <summary>
        /// Gets the security header. If none exists null is returned.
        /// </summary>
        public SecurityHeader SecurityHeader {
            get { return _securityHeader; }
        }
    }
}
