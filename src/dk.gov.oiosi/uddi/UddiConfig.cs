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

using dk.gov.oiosi.common;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {
    
    /// <summary>
    /// UDDI configuration for the user.settings file.
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class UddiConfig {
        private string _profileConformanceClaim = "";
        private string _registrationConformanceClaim = "";
        private string _uddiInquireEndpointURL = "";
        private string _uddiInquireEndpointURLFallback = "";
        private string _uddiPublishEndpointURL = "";
        private string _uddiSecurityEndpointURL = "";
        private int _fallbackTimeoutMinutes = 0;
        private LookupReturnOptionEnum _lookupReturnOptions = LookupReturnOptionEnum.allResults;
        private bool _tryOtherHostsOnFailure = true;
        private ranges.GatewayRange _gatewayRange = null;

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
        /// Gets or sets the default UDDI inquiry service endpoint
        /// </summary>
        public string UddiInquireEndpointURL {
            get { return _uddiInquireEndpointURL; }
            set { _uddiInquireEndpointURL = value; }
        }

        /// <summary>
        /// Gets or sets the fallback UDDI inquiry service endpoint. This endpoint is used
        /// if the main inquiry endpoint fails
        /// </summary>
        public string UddiInquireEndpointURLFallback {
            get { return _uddiInquireEndpointURLFallback; }
            set { _uddiInquireEndpointURLFallback = value; }
        }

        /// <summary>
        /// Gets or sets the default UDDI publish service endpoint
        /// </summary>
        public string UddiPublishEndpointURL {
            get { return _uddiPublishEndpointURL; }
            set { _uddiPublishEndpointURL = value; }
        }

        /// <summary>
        /// Gets or sets the default UDDI security endpoint
        /// </summary>
        public string UddiSecurityEndpointURL {
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
        /// Gets or sets the lookup return options
        /// </summary>
        public LookupReturnOptionEnum LookupReturnOptions {
            get { return _lookupReturnOptions; }
            set { _lookupReturnOptions = value; }
        }

        /// <summary>
        /// Gets or sets the 'try other hosts on failure' flag
        /// </summary>
        public bool TryOtherHostsOnFailure {
            get { return _tryOtherHostsOnFailure; }
            set { _tryOtherHostsOnFailure = value; }
        }


        /// <summary>
        /// Gets or sets the gateway range configuration
        /// </summary>
        public ranges.GatewayRange GatewayRange {
            get { return _gatewayRange; }
            set { _gatewayRange = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UddiConfig() {
        }
    }
}