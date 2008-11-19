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
using System.Configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.addressing;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Policies for the ARS lookup client, e.g. addresses of UDDI interfaces and fallback parameters.
    /// </summary>
    public class UddiLookupClientPolicy {

        /// <summary>
        /// Return config section
        /// </summary>
        private static UddiConfig config = ConfigurationHandler.GetConfigurationSection<UddiConfig>();

        private static UddiLookupClientPolicy _default;

        /// <summary>
        /// Returns the default lookup client policy. The default is read from configuration.
        /// </summary>
        public static UddiLookupClientPolicy Default {
            get {
                if (_default == null) {
                    _default = new UddiLookupClientPolicy();
                }
                return _default;
            }
        }
        
        /// <summary>
        /// If true, the lookup client will attempt the next UDDI endpoint in the list of hosts, 
        /// if an attempt to connect to a UDDI fails.
        /// </summary>
        private bool _tryOtherHostsOnFailure = config.TryOtherHostsOnFailure;

        /// <summary>
        /// 
        /// </summary>
        public bool TryOtherHostsOnFailure {
            get { return _tryOtherHostsOnFailure; }
            set { _tryOtherHostsOnFailure = value; }
        }


        /// <summary>
        /// A list of possible UDDI endpoints that may be used for translation.
        /// In case of failure, the next UDDI endpoint in the list is tried.
        /// First endpoint in the list is alwys tried first.
        /// </summary>
        private List<UddiEndpoint> _uddiEndpoints = new List<UddiEndpoint>();
        /// <summary>
        /// 
        /// </summary>
        public List<UddiEndpoint> UddiEndpoints {
            get { return _uddiEndpoints; }
            //set { _uddiEndpoints = value; }
        }

    }
}