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
using System;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Configuration for the UddiLookupClientTest implementation of the IUddiLookup interface.
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class UddiLookupClientTestConfig {

        private string _endpointAddress = "";

        /// <summary>
        /// The endpoint addresse returned
        /// </summary>
        public string EndpointAddress {
            get { return _endpointAddress; }
            set { _endpointAddress = value; }
        }

        private DateTime _activationDate;

        /// <summary>
        /// The activation date returned
        /// </summary>
        public DateTime ActivationDate {
            get { return _activationDate; }
            set { _activationDate = value; }
        }


        private DateTime _expirationDate;

        /// <summary>
        /// The expiration date returned
        /// </summary>
        public DateTime ExpirationDate {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }

        private string _certificateSubjectSerialNumber="";

        /// <summary>
        /// The certificate subject serial number returned
        /// </summary>
        public string CertificateSubjectSerialNumber {
            get { return _certificateSubjectSerialNumber; }
            set { _certificateSubjectSerialNumber = value; }
        }

        private string _termsOfUseUrl = "";

        /// <summary>
        /// The url to the terms of use document returned
        /// </summary>
        public string TermsOfUseUrl {
            get { return _termsOfUseUrl; }
            set { _termsOfUseUrl = value; }
        }

        private string _serviceContactEmail = "";

        /// <summary>
        /// The service contract email returned
        /// </summary>
        public string ServiceContactEmail {
            get { return _serviceContactEmail; }
            set { _serviceContactEmail = value; }
        }

        private string _version = "";

        /// <summary>
        /// The version returned
        /// </summary>
        public string Version {
            get { return _version; }
            set { _version = value; }
        }

        private string _newerVersionReference = "";

        /// <summary>
        /// An (optional) reference to a newer version of the endpoint (UDDI serviceKey reference)
        /// </summary>
        public string NewerVersionReference {
            get { return _newerVersionReference; }
            set { _newerVersionReference = value; }
        }

        private bool _hasNewerVersion = false;

        /// <summary>
        /// True if a newer version of the service exists. If this is set to true, the
        /// NewerVersionReference field MUST be set.
        /// </summary>
        public bool HasNewerVersion {
            get { return _hasNewerVersion; }
            set { _hasNewerVersion = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UddiLookupClientTestConfig() {
        }
    }
}