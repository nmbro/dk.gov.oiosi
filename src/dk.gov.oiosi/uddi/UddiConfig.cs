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

using System.Xml.Serialization;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {
    
    /// <summary>
    /// UDDI configuration for the user.settings file.
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class UddiConfig {

        private LookupRegistryFallbackConfig lookupRegistryFallbackConfig;

        private string _profileConformanceClaim = "";
        private string _registrationConformanceClaim = "";
        
        private string _uddiPublishEndpointURL = "";
        private string _uddiPublishInquiryEndpointURL = "";
        private string _uddiSecurityEndpointURL = "";

        private int _fallbackTimeoutMinutes = 0;
        private bool _tryOtherHostsOnFailure;

        /// <summary>
        /// Gets or sets the default service profile claim used when performing
        /// a lookup
        /// </summary>
        public string ProfileConformanceClaim {
            get { return _profileConformanceClaim; }
            set { _profileConformanceClaim = value; }
        }

        /// <summary>
        /// Gets or sets the default UDDI profile claim used when performing
        /// a lookup
        /// </summary>
        public string RegistrationConformanceClaim {
            get { return _registrationConformanceClaim; }
            set { _registrationConformanceClaim = value; }
        }


        /// <summary>
        /// Gets or sets the default UDDI publish service endpoint
        /// </summary>
        public string PublishEndpoint {
            get { return _uddiPublishEndpointURL; }
            set { _uddiPublishEndpointURL = value; }
        }

        /// <summary>
        /// Gets or sets the default UDDI publish inquiry endpoint
        /// </summary>
        public string PublishInquiryEndpoint {
            get { return _uddiPublishInquiryEndpointURL ; }
            set { _uddiPublishInquiryEndpointURL = value; }
        }

        /// <summary>
        /// Gets or sets the default UDDI security endpoint
        /// </summary>
        public string SecurityEndpoint {
            get { return _uddiSecurityEndpointURL; }
            set { _uddiSecurityEndpointURL = value; }
        }

        /// <summary>
        /// Gets or sets the fallback timeout in minutes
        /// </summary>
        public int FallbackTimeoutMinutes {
            get { return _fallbackTimeoutMinutes; }
            set { _fallbackTimeoutMinutes = value; }
        }

        /// <summary>
        /// Gets or sets the list of registries to try lookup with
        /// </summary>
        public LookupRegistryFallbackConfig LookupRegistryFallbackConfig {
            get { return lookupRegistryFallbackConfig; }
            set { lookupRegistryFallbackConfig = value; }
        }

        /// <summary>
        /// Should other hosts be tried on failure
        /// </summary>
        public bool TryOtherHostsOnFailure {
            get { return _tryOtherHostsOnFailure; }
            set { _tryOtherHostsOnFailure = value; }
        }
    }
}