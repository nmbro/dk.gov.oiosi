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

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.ocsp;

namespace dk.gov.oiosi.raspProfile
{
    /// <summary>
    /// Default revocation values class
    /// </summary>
    public abstract class DefaultRevocationConfig
    {
        /// <summary>
        /// set revocation
        /// </summary>
        public abstract void SetRevocationLookupFactoryConfig();

        /// <summary>
        /// set test
        /// </summary>
        public abstract void SetTestRevocationLookupFactoryConfig();

        /// <summary>
        /// Use default live factory as default
        /// </summary>
        public virtual void SetIfNotExistsRevocationLookupFactoryConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<RevocationLookupFactoryConfig>())
                return;
            SetRevocationLookupFactoryConfig();
        }

        /// <summary>
        /// Use default test factory as default
        /// </summary>
        public virtual void SetIfNotExistsTestRevocationLookupFactoryConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<RevocationLookupFactoryConfig>())
                return;
            SetTestRevocationLookupFactoryConfig();
        }

        /// <summary>
        /// Set default test config values
        /// </summary>
        public virtual void SetTestCertificatesOscpConfig()
        {
            // Test certificates here
            OcspConfig ocspConfig = ConfigurationHandler.GetConfigurationSection<OcspConfig>();
            ocspConfig.DefaultTimeoutMsec = 20000;
        }

        /// <summary>
        /// Set default live config values
        /// </summary>
        public virtual void SetOscpConfig()
        {
            // Live certificates here
            OcspConfig ocspConfig = ConfigurationHandler.GetConfigurationSection<OcspConfig>();
            ocspConfig.DefaultTimeoutMsec = 20000;
        }

        /// <summary>
        /// Use test lookup
        /// </summary>
        public virtual void SetTestOscpConfig()
        {
            OcspLookupTestConfig ocspTestConfig = ConfigurationHandler.GetConfigurationSection<OcspLookupTestConfig>();
            ocspTestConfig.ReturnPositiveResponse = true;
        }

        /// <summary>
        /// Use test Ocsp server as default
        /// </summary>
        public virtual void SetIfNotExistsTestCertificatesOscpConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<OcspConfig>())
                return;
            SetTestCertificatesOscpConfig();
        }

        /// <summary>
        /// Set live Ocsp server as default
        /// </summary>
        public virtual void SetIfNotExistsOscpConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<OcspConfig>())
                return;
            SetOscpConfig();
        }

        /// <summary>
        /// Use test lookup Ocsp
        /// </summary>
        public virtual void SetIfNotExistsTestOscpConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<OcspLookupTestConfig>())
                return;
            SetTestOscpConfig();
        }

        /// <summary>
        /// Sets ocsp test server url
        /// </summary>
        public virtual void SetOcspConfigToUseTestOcspServer()
        {
            var ocspConfig = ConfigurationHandler.GetConfigurationSection<OcspConfig>();
            ocspConfig.ServerUrl = "http://test.ocsp.certifikat.dk/ocsp/status";
        }
    }
}