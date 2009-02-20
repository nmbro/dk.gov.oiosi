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
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace dk.gov.oiosi
{
    /// <summary>
    /// Contains information needed to look a certificate up in a Windows certificate store
    /// </summary>
    [XmlType("RaspCertifikateLoopkupInformation")]
    public class CertifikateLoopkupInformation
    {
        /// <summary>
        /// The store location of the certificate
        /// </summary>
        [XmlElement("StoreLocation")]
        public StoreLocation StoreLocation { get { return _storeLocation; } set { _storeLocation = value; } }
        private StoreLocation _storeLocation = StoreLocation.CurrentUser;

        /// <summary>
        /// The store where the certificate can be found
        /// </summary>
        [XmlElement("StoreName")]
        public StoreName StoreName { get { return _storeName; } set { _storeName = value; } }
        private StoreName _storeName = StoreName.My;

        /// <summary>
        /// The certificate's serial number
        /// </summary>
        [XmlElement("SerialNumber")]
        public string SerialNumber { get { return _serialNumber; } set { _serialNumber = value; } }
        private string _serialNumber = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public CertifikateLoopkupInformation(){}
    }
}