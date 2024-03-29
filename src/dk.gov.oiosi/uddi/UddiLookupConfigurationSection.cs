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
using System;
using System.Configuration;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Configuration section attributes
    /// </summary>
    public class UddiLookupUddiInquireEndpointURLConfigurationSection : ConfigurationSection {
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public UddiLookupUddiInquireEndpointURLConfigurationSection() {
        }

        /// <summary>
        /// UDDI inquire endpoint 
        /// </summary>
        [ConfigurationProperty("uddiInquireEndpointURL",
                    IsRequired = true, IsKey = false)]
        public Uri UddiEndpointURI {
            get { return (Uri)this["uddiInquireEndpointURL"]; }
            set { this["uddiInquireEndpointURL"] = value; }
        }
    }

    /// <summary>
    /// Configuration section attributes
    /// </summary>
    public class UddiLookupUddiPublishEndpointURLConfigurationSection : ConfigurationSection {
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public UddiLookupUddiPublishEndpointURLConfigurationSection() {
        }

        /// <summary>
        /// UDDI publish endpoint 
        /// </summary>
        [ConfigurationProperty("uddiPublishEndpointURL",
                    IsRequired = true, IsKey = false)]
        public Uri UddiEndpointURI {
            get { return (Uri)this["uddiPublishEndpointURL"]; }
            set { this["uddiPublishEndpointURL"] = value; }
        }
    }

    /// <summary>
    /// Configuration section attributes
    /// </summary>
    public class UddiLookupUddiSecurityEndpointURLConfigurationSection : ConfigurationSection {
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public UddiLookupUddiSecurityEndpointURLConfigurationSection() {
        }

        /// <summary>
        /// UDDI security endpoint 
        /// </summary>
        [ConfigurationProperty("uddiSecurityEndpointURL",
                    IsRequired = true, IsKey = false)]
        public Uri UddiEndpointURI {
            get { return (Uri)this["uddiSecurityEndpointURL"]; }
            set { this["uddiSecurityEndpointURL"] = value; }
        }
    }

    /// <summary>
    /// Configuration section attributes
    /// </summary>
    public class UddiLookupClientPolicyConfigurationSection : ConfigurationSection {
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public UddiLookupClientPolicyConfigurationSection() {
        }

        /// <summary>
        /// True if other UDDI hosts should be contacted if a UDDI connection failure has 
        /// occurred
        /// </summary>
        [ConfigurationProperty("tryOtherHostsOnFailure",
                    IsRequired = true, IsKey = false)]
        public bool TryOtherHostsOnFailure {
            get { return (bool)this["tryOtherHostsOnFailure"]; }
            set { this["tryOtherHostsOnFailure"] = value; }
        }


    }
}