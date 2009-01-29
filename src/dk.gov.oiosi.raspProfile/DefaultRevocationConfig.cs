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

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.ocsp;

namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// Default Ocsp values class
    /// </summary>
    public class DefaultRevocationConfig {

        /// <summary>
        /// Set default, live Ocsp factory 
        /// </summary>
        public void SetRevocationLookupFactoryConfig() {
            RevocationLookupFactoryConfig ocspFactoryConfig = ConfigurationHandler.GetConfigurationSection<RevocationLookupFactoryConfig>();
            ocspFactoryConfig.ImplementationAssembly = "dk.gov.oiosi.library";
            ocspFactoryConfig.ImplementationNamespaceClass = "dk.gov.oiosi.security.revocation.ocsp.OcspLookup";
        }

        /// <summary>
        /// Set default, test Ocsp factory
        /// </summary>
        public void SetTestRevocationLookupFactoryConfig() {
            RevocationLookupFactoryConfig ocspFactoryConfig = ConfigurationHandler.GetConfigurationSection<RevocationLookupFactoryConfig>();
            ocspFactoryConfig.ImplementationAssembly = "dk.gov.oiosi.library";
            ocspFactoryConfig.ImplementationNamespaceClass = "dk.gov.oiosi.security.revocation.ocsp.OcspLookupTest";
        }

        /// <summary>
        /// Use default live factory as default 
        /// </summary>
        public void SetIfNotExistsRevocationLookupFactoryConfig() {
            if (ConfigurationHandler.HasConfigurationSection<RevocationLookupFactoryConfig>())
                return;
            SetRevocationLookupFactoryConfig();
        }

        /// <summary>
        /// Use default test factory as default
        /// </summary>
        public void SetIfNotExistsTestRevocationLookupFactoryConfig() {
            if (ConfigurationHandler.HasConfigurationSection<RevocationLookupFactoryConfig>())
                return;
            SetTestRevocationLookupFactoryConfig();
        }

        /// <summary>
        /// Set default test config values
        /// </summary>
        public void SetTestCertificatesOscpConfig() {
            // Test certificates here
            OcspConfig ocspConfig = ConfigurationHandler.GetConfigurationSection<OcspConfig>();
            ocspConfig.DefaultTimeoutMsec = 10000;
        }

        /// <summary>
        /// Set default live config values
        /// </summary>
        public void SetOscpConfig() {
            // Live certificates here
            OcspConfig ocspConfig = ConfigurationHandler.GetConfigurationSection<OcspConfig>();
            ocspConfig.DefaultTimeoutMsec = 10000;
        }

        /// <summary>
        /// Use test lookup
        /// </summary>
        public void SetTestOscpConfig() {
            OcspLookupTestConfig ocspTestConfig = ConfigurationHandler.GetConfigurationSection<OcspLookupTestConfig>();
            ocspTestConfig.ReturnPositiveResponse = true;
        }

        /// <summary>
        /// Use test Ocsp server as default
        /// </summary>
        public void SetIfNotExistsTestCertificatesOscpConfig() {
            if (ConfigurationHandler.HasConfigurationSection<OcspConfig>())
                return;
            SetTestCertificatesOscpConfig();
        }

        /// <summary>
        /// Set live Ocsp server as default
        /// </summary>
        public void SetIfNotExistsOscpConfig() {
            if (ConfigurationHandler.HasConfigurationSection<OcspConfig>())
                return;
            SetOscpConfig();
        }

        /// <summary>
        /// Use test lookup Ocsp
        /// </summary>
        public void SetIfNotExistsTestOscpConfig() {
            if (ConfigurationHandler.HasConfigurationSection<OcspLookupTestConfig>())
                return;
            SetTestOscpConfig();
        }
    }
}
