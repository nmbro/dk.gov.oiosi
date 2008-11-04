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
using System.Xml.Serialization;

using dk.gov.oiosi.uddi.category;

namespace dk.gov.oiosi.communication.configuration {
    /// <summary>
    /// Definition of a RASP endpoint type
    /// </summary>
    public class DocumentEndpointInformation {
        private string _requestAction = "";
        private string _replyAction = "";
        private ServiceEndpointFriendlyName _endpointFriendlyName = new ServiceEndpointFriendlyName();
        private ServiceEndpointFriendlyName _senderFriendlyName = new ServiceEndpointFriendlyName();
        private ServiceEndpointKey _key = new ServiceEndpointKey();
        private ServiceEndpointKey _senderKey = new ServiceEndpointKey();

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentEndpointInformation() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestAction">The SOAP action associated with requests to endpoints of this type</param>
        /// <param name="replyAction">The SOAP action expected to be returned by endpoints of this type</param>
        /// <param name="friendlyNameXpath">Xpath to the human reader friendly name of this endpoint (e.g. "Ministry of Science, Denmark")</param>
        /// <param name="endpointKey">Information about the endpoint unique ID, used for UDDI lookups</param>
        /// <param name="senderFriendlyNameXpath">The xpath to the human reader friendly name of the sender</param>
        /// <param name="senderKey">The endpoint key of the sender</param>
        public DocumentEndpointInformation(
            string requestAction,
            string replyAction,
            ServiceEndpointFriendlyName friendlyNameXpath,
            ServiceEndpointKey endpointKey,
            ServiceEndpointFriendlyName senderFriendlyNameXpath,
            ServiceEndpointKey senderKey) {
            _requestAction = requestAction;
            _replyAction = replyAction;
            _endpointFriendlyName = friendlyNameXpath;
            _key = endpointKey;
            _senderFriendlyName = senderFriendlyNameXpath;
            _senderKey = senderKey;
        }

        /// <summary>
        /// The SOAP action associated with endpoints of this type
        /// </summary>
        [XmlElement("RequestAction")]
        public string RequestAction { 
            get { return _requestAction; } 
            set { _requestAction = value; } 
        }

        /// <summary>
        /// The SOAP action returned by endpoints of this type
        /// </summary>
        [XmlElement("ReplyAction")]
        public string ReplyAction { 
            get { return _replyAction; } 
            set { _replyAction = value; } 
        }

        /// <summary>
        /// The xpath to the human reader friendly name of this endpoint (e.g. "Ministry of Science, Denmark")
        /// </summary>
        [XmlElement("EndpointFriendlyName")]
        public ServiceEndpointFriendlyName EndpointFriendlyName { 
            get { return _endpointFriendlyName; } 
            set { _endpointFriendlyName = value; } 
        }
        
        /// <summary>
        /// The xpath to the human reader friendly name of the sender (e.g. "Ministry of Science, Denmark")
        /// </summary>
        [XmlElement("SenderFriendlyName")]
        public ServiceEndpointFriendlyName SenderFriendlyName { 
            get { return _senderFriendlyName; } 
            set { _senderFriendlyName = value; } 
        }

        /// <summary>
        /// Information about the endpoint unique ID, used for UDDI lookups
        /// </summary>
        [XmlElement("Key")]
        public ServiceEndpointKey Key { 
            get { return _key; } 
            set { _key = value; } 
        }
        
        /// <summary>
        /// Information about the sending endpoint unique ID
        /// </summary>
        [XmlElement("SenderKey")]
        public ServiceEndpointKey SenderKey { 
            get { return _senderKey; } 
            set { _senderKey = value; } 
        }
    }
}