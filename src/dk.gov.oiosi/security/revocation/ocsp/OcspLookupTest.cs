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
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.security.revocation.ocsp {

    /// <summary>
    /// Implementation of the IRevocationLookup interface for offline testing.
    /// You may configure the RevocationLookupFactory to instantiate classes of this type.
    /// </summary>
    public class OcspLookupTest : IRevocationLookup {
        private OcspConfig _config;
        private OcspLookupTestConfig _testConfig;


        #region IRevocationLookup Members

        /// <summary>
        /// Returns the status of the certificate. In this offline test implementation of the
        /// IRevocationLookup interface, the response can be set in the configuration file
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>Returns a revocation status</returns>
        public RevocationResponse CheckCertificate(X509Certificate2 certificate)
        {
            RevocationResponse response = new RevocationResponse();
            response.IsValid = _testConfig.ReturnPositiveResponse;
            response.NextUpdate = DateTime.MaxValue;
            return response;
        }

        /// <summary>
        /// Return OCSP configuration
        /// </summary>
        public OcspConfig Configuration {
            get { return _config; }
        }

        /// <summary>
        /// Default constructor. Attempts to load configuration from file.
        /// </summary>
        public OcspLookupTest() {
            // 1. Load normal OCSP config
            _config = ConfigurationHandler.GetConfigurationSection<OcspConfig>();

            // 2. Load OcspLookupTest-specific configuration:
            _testConfig = ConfigurationHandler.GetConfigurationSection<OcspLookupTestConfig>();
        }

        /// <summary>
        /// Constructor. Does not rely on configuration files.
        /// </summary>
        /// <param name="ocspConfig">The OCSP configuration</param>
        /// <param name="testConfig">Configuration specific for this class</param>
        public OcspLookupTest(OcspConfig ocspConfig, OcspLookupTestConfig testConfig) {
            _config = ocspConfig;
            _testConfig = testConfig;
        }

        #endregion
    }
}