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
using System.ServiceModel.Channels;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.exception;
using dk.gov.oiosi.xml;
using System.IO;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.communication {
    
    /// <summary>
    /// Represents an outbound message (xml + metadata)
    /// </summary>
    public class OiosiMessage {

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xml">the message</param>
        public OiosiMessage(XmlDocument xml) 
        {   _messageXml = xml;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="wcfMessage">the message</param>
        public OiosiMessage(Message wcfMessage) {
            _messageXml = Utilities.GetMessageBodyAsXmlDocument(wcfMessage, true);
        }

        #endregion

        /// <summary>
        /// A list of cumstom headers
        /// </summary>
        public Dictionary<XmlQualifiedName, MessageHeader> MessageHeaders {
            get { return _messageHeaders; }
        }
        private Dictionary<XmlQualifiedName, MessageHeader> _messageHeaders = new Dictionary<XmlQualifiedName, MessageHeader>();

        /// <summary>
        /// Custom properties
        /// </summary>
        public Dictionary<string, object> Properties {
            get { return _properties; }
        }
        private Dictionary<string, object> _properties = new Dictionary<string,object>();

        /// <summary>
        /// Custom properties
        /// </summary>
        public Dictionary<string, object> UbiquitousProperties {
            get { return _ubiquitousProperties; }
        }
        private Dictionary<string, object> _ubiquitousProperties = new Dictionary<string, object>();


        private string _requestAction = "*";

        /// <summary>
        /// Gets or sets the request action of the message
        /// </summary>
        public string RequestAction {
            get { return _requestAction; }
            set { _requestAction = value; }
        }


        private string _replyAction = "*";

        /// <summary>
        /// Gets or sets the reply action for the message
        /// </summary>
        public string ReplyAction {
            get { return _replyAction; }
            set { _replyAction = value; }
        }

        /// <summary>
        /// Property for the message
        /// </summary>
        public XmlDocument MessageXml {
            get { return _messageXml; }
        }
        private XmlDocument _messageXml;

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