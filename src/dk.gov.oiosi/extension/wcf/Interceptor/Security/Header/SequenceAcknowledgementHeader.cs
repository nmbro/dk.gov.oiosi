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
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Xml;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.Header {
    /// <summary>
    /// Represents the sequence acknowledgement header used in reliable messaging.
    /// </summary>
    public class SequenceAcknowledgementHeader : Header {
        private MessageHeader _sequenceAcknowledgementHeader;
        private string _sequenceId;
        private List<SequenceAcknowledgementRange> _ackRanges;

        /// <summary>
        /// Constructor that takes the wcf message header that represent the message 
        /// sequence acknowledgement header.
        /// </summary>
        /// <param name="sequenceAcknowledgementHeader"></param>
        public SequenceAcknowledgementHeader(MessageHeader sequenceAcknowledgementHeader) {
            _sequenceAcknowledgementHeader = sequenceAcknowledgementHeader;

            XmlDocument headerDocument = new XmlDocument();
            headerDocument.LoadXml(_sequenceAcknowledgementHeader.ToString());

            _sequenceId = GetElementValueFromTagName(headerDocument, "Identifier");

            _ackRanges = new List<SequenceAcknowledgementRange>();
            string ackRangeName = "AcknowledgementRange";
            IEnumerable<XmlNode> ackRangeNodes = GetElementsFromTagName(headerDocument, ackRangeName);
            foreach (XmlNode ackRangeNode in ackRangeNodes) {
                string upperString = GetAttributeValueFromTagName(ackRangeNode, "Upper");
                string lowerString = GetAttributeValueFromTagName(ackRangeNode, "Lower");
                long upper = long.Parse(upperString);
                long lower = long.Parse(lowerString);
                SequenceAcknowledgementRange ackRange = new SequenceAcknowledgementRange(upper, lower);
                _ackRanges.Add(ackRange);
            }
            if (_ackRanges.Count < 1) throw new NoElementsFoundException(ackRangeName);
        }

        /// <summary>
        /// Gets the sequence id.
        /// </summary>
        public string SequenceId {
            get { return _sequenceId.ToString(); }
        }

        /// <summary>
        /// Gets the acknowledgement range.
        /// </summary>
        public IEnumerable<SequenceAcknowledgementRange> AcknowledgementRanges {
            get { return _ackRanges; }
        }

        /// <summary>
        /// Returns whether a given message number is within range.
        /// </summary>
        /// <param name="messageNumber"></param>
        /// <returns></returns>
        public bool IsMessageNumberWithinRange(long messageNumber) {
            foreach (SequenceAcknowledgementRange range in _ackRanges) {
                if (range.IsWithinRange(messageNumber)) return true;
            }
            return false;
        }
    }
}
