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
using System.Security.Cryptography.X509Certificates;
using System.Text;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security;


namespace dk.gov.oiosi.raspProfile {
    /// <summary>
    /// Default configuration for the root certificate
    /// </summary>
    public class DefaultRootCertificateConfig {

        /// <summary>
        /// Test default root certificate configuration
        /// </summary>
        public void SetTestDefaultRootCertificateConfig() {
            RootCertificateConfig rootCertificateConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateConfig>();
            rootCertificateConfig.RootCertificateLocation.SerialNumber = "403617FC";
            rootCertificateConfig.RootCertificateLocation.StoreLocation = StoreLocation.LocalMachine;
            rootCertificateConfig.RootCertificateLocation.StoreName = StoreName.Root;
        }

        /// <summary>
        /// Liver certificate default root certificate configuration 
        /// </summary>
        public void SetProductionDefaultRootCertificateConfig() {
            RootCertificateConfig rootCertificateConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateConfig>();
            rootCertificateConfig.RootCertificateLocation.SerialNumber = "3E48BDC4";
            rootCertificateConfig.RootCertificateLocation.StoreLocation = StoreLocation.LocalMachine;
            rootCertificateConfig.RootCertificateLocation.StoreName = StoreName.Root;
        }

        /// <summary>
        /// Sets the test default root certificate configuration if it does not exist in configuration
        /// </summary>
        public void SetIfNotExistsTestDefaultRootCertificateConfig() {
            if (ConfigurationHandler.HasConfigurationSection<RootCertificateConfig>())
                return;
            SetTestDefaultRootCertificateConfig();
        }

        /// <summary>
        /// Sets the default live root certificate configuration if it does not exist in configuration
        /// </summary>
        public void SetIfNotExistsProductionDefaultRootCertificateConfig() {
            if (ConfigurationHandler.HasConfigurationSection<RootCertificateConfig>())
                return;
            SetProductionDefaultRootCertificateConfig();
        }
    }
}
