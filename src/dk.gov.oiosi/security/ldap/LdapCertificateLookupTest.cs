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
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.security.ldap {

    /// <summary>
    /// Dummy test class. Use for offline LDAP lookup tests. Returns a selected certificate based on 
    /// configuration.
    /// </summary>
    public class LdapCertificateLookupTest : ICertificateLookup {
        private LdapCertificateLookupTestConfig _config;

        /// <summary>
        /// Returns a selected certificate based on configuration.
        /// </summary>
        /// <param name="certificateSubject">The subject serial number of the certificate</param>
        /// <returns>Returns a selected certificate based on configuration.</returns>
        public X509Certificate2 GetCertificate(CertificateSubject certificateSubject) {
            switch (_config.Action) {
                case LdapCertificateLookupTestConfig.LookupAction.FindCertificate:
                    // 1. Attempt to load the certificate from store:
                    return CertificateLoader.GetCertificateFromStoreWithSSN(
                        certificateSubject.SerialNumberValue,
                        _config.StoreLocation,
                        _config.StoreName
                    );
                case LdapCertificateLookupTestConfig.LookupAction.ConnectionFailed:
                    LdapSettings settings = ConfigurationHandler.GetConfigurationSection<LdapSettings>();
                    throw new ConnectingToLdapServerFailedException(settings, new Exception(this.ToString()));
                case LdapCertificateLookupTestConfig.LookupAction.SearchFailed:
                    throw new SearchFailedException(new Exception(this.ToString()));
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Default constructor. Attempts to load configuration from configuration file.
        /// </summary>
        public LdapCertificateLookupTest() {
            _config = ConfigurationHandler.GetConfigurationSection<LdapCertificateLookupTestConfig>();
        }
    }
}