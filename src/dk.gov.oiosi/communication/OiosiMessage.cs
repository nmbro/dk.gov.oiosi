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
using dk.gov.oiosi.common;
using System;

namespace dk.gov.oiosi.communication {
    
    /// <summary>
    /// Represents an outbound message (xml + metadata)
    /// </summary>
    public class OiosiMessage {
        private const string DEFAULTREQUESTACTION = "*";
        private const string DEFAULTREPLYACTION = "*";
        //private Dictionary<string, object> _properties = new Dictionary<string, object>();
        //private Dictionary<XmlQualifiedName, MessageHeader> _messageHeaders = new Dictionary<XmlQualifiedName, MessageHeader>();
        //TODO: private Dictionary<string, object> _ubiquitousProperties = new Dictionary<string, object>();
        //TODO: private string _requestAction = "*";
        //TODO: private string _replyAction = "*";
        private XmlDocument _messageXml;

        #region Constructors

        private OiosiMessage() {
            Properties = new Dictionary<string, object>();
            UbiquitousProperties = new Dictionary<string, object>();
            MessageHeaders = new Dictionary<XmlQualifiedName, MessageHeader>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xml">the message</param>
        public OiosiMessage(XmlDocument xml) : this() {   
            _messageXml = xml;
            RequestAction = DEFAULTREQUESTACTION;
            ReplyAction = DEFAULTREPLYACTION;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="wcfMessage">the message</param>
        public OiosiMessage(Message wcfMessage) : this() {
            try {
                _messageXml = Utilities.GetMessageBodyAsXmlDocument(wcfMessage, true);
                //Adding the headers
                foreach (MessageHeader header in wcfMessage.Headers) {
                    XmlQualifiedName qName = new XmlQualifiedName(header.Name, header.Namespace);
                    MessageHeaders.Add(qName, header);
                }
                foreach (KeyValuePair<string, object> keyValuePair in wcfMessage.Properties) {
                    
                }
            }
            catch (Exception e) {
                dk.gov.oiosi.logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Error, "Exception occurred in OiosiMessage: " + e);
            }
        }

        #endregion

        /// <summary>
        /// A list of cumstom headers
        /// </summary>
        public Dictionary<XmlQualifiedName, MessageHeader> MessageHeaders { get; private set; }

        /// <summary>
        /// Custom properties
        /// </summary>
        public Dictionary<string, object> Properties { get; private set; }

        /// <summary>
        /// Custom properties
        /// </summary>
        public Dictionary<string, object> UbiquitousProperties { get; private set; }

        /// <summary>
        /// Gets or sets the request action of the message
        /// </summary>
        public string RequestAction { get; set; }

        /// <summary>
        /// Gets or sets the reply action for the message
        /// </summary>
        public string ReplyAction { get; set; }

        /// <summary>
        /// Property for the message
        /// </summary>
        public XmlDocument MessageXml {
            get { return _messageXml; }
        }

        /// <summary>
        /// Do we have a message body?
        /// </summary>
        public bool HasBody {
            get { return (_messageXml != null && _messageXml.DocumentElement != null); }
        }
        
        /// <summary>
        /// Returns an XmlReader that can read the message xml
        /// </summary>
        /// <returns>XmlReader that can read the message xml</returns>
        public XmlReader GetMessageXmlReader() {
            XmlNodeReader xnr = new XmlNodeReader(_messageXml.DocumentElement);
            return (XmlReader)xnr;
        }
    }
}