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
using System.Xml.Serialization;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.security {
    
    /// <summary>
    /// User-level certificate configuration
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class CertificateConfig {
        /// <summary>
        /// Certificate used for sending documents
        /// </summary>
        [XmlElement("CertificateForSending")]
        public CertifikateLoopkupInformation CertificateForSending { get { return _certificateForSending; } set { _certificateForSending = value; } }
        private CertifikateLoopkupInformation _certificateForSending = new CertifikateLoopkupInformation();

        /// <summary>
        /// Certificate usd for receiving documents
        /// </summary>
        [XmlElement("CertificateForReceiving")]
        public CertifikateLoopkupInformation CertificateForReceiving { get { return _certificateForReceiving; } set { _certificateForReceiving = value; } }
        private CertifikateLoopkupInformation _certificateForReceiving = new CertifikateLoopkupInformation();
    }
}
