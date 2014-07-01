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
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.ldap;

namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// A default LDAP connection configuration
    /// </summary>
    public class DefaultLdapConfig {

        /// <summary>
        /// Use default (live) Ldap config factory
        /// </summary>
        public void SetLdapLookupFactoryConfig() {
            LdapLookupFactoryConfig ldapFactoryConfig = ConfigurationHandler.GetConfigurationSection<LdapLookupFactoryConfig>();
            ldapFactoryConfig.ImplementationAssembly = "dk.gov.oiosi.library";
            ldapFactoryConfig.ImplementationNamespaceClass = "dk.gov.oiosi.security.ldap.LdapCertificateLookup";
        }

        /// <summary>
        /// Use test Ldap config factory
        /// </summary>
        public void SetTestLdapLookupFactoryConfig() {
            LdapLookupFactoryConfig ldapFactoryConfig = ConfigurationHandler.GetConfigurationSection<LdapLookupFactoryConfig>();
            ldapFactoryConfig.ImplementationAssembly = "dk.gov.oiosi.library";
            ldapFactoryConfig.ImplementationNamespaceClass = "dk.gov.oiosi.security.ldap.LdapCertificateLookupTest";
        }

        /// <summary>
        /// Use the dafult, live factory
        /// </summary>
        public void SetIfNotExistsLdapLookupFactoryConfig() {
            if (ConfigurationHandler.HasConfigurationSection<LdapLookupFactoryConfig>())
                return;
            SetLdapLookupFactoryConfig();
        }

        /// <summary>
        /// Use test factory
        /// </summary>
        public void SetIfNotExistsTestLdapLookupFactoryConfig() {
            if (ConfigurationHandler.HasConfigurationSection<LdapLookupFactoryConfig>())
                return;
            SetTestLdapLookupFactoryConfig();
        }

        /// <summary>
        /// Fill configuration section with default live values
        /// </summary>
        public void SetDefaultLdapConfig() {
            LdapSettings ldapSettings = ConfigurationHandler.GetConfigurationSection<LdapSettings>();
            // Lookup for live OCES certificates
            // ldapSettings.Host = "dir.certifikat.dk";
            ldapSettings.Host = "crtdir.certifikat.dk";
            ldapSettings.MaxResults = 1;
            ldapSettings.Port = 389;
            ldapSettings.ConnectionTimeoutMsec = 5000;
            ldapSettings.SearchClientTimeoutMsec = 5000;
            ldapSettings.SearchServerTimeoutMsec = 5000;
        }

        /// <summary>
        /// Fill configuration section with default test values
        /// </summary>
        public void SetTestCertificateDefaultLdapConfig() {
            LdapSettings ldapSettings = ConfigurationHandler.GetConfigurationSection<LdapSettings>();
            // Lookup for test OCES certificates
            // ldapSettings.Host = "fenris.certifikat.dk";
            ldapSettings.Host = "crtdir.pp.certifikat.dk";
            ldapSettings.MaxResults = 1;
            ldapSettings.Port = 389;
            ldapSettings.ConnectionTimeoutMsec = 5000;
            ldapSettings.SearchClientTimeoutMsec = 5000;
            ldapSettings.SearchServerTimeoutMsec = 5000;
        }

        /// <summary>
        /// Set default, test certificate root location
        /// </summary>
        public void SetDefaultLdapConfigTest() {
            LdapCertificateLookupTestConfig ldapTestConfig = ConfigurationHandler.GetConfigurationSection<LdapCertificateLookupTestConfig>();
            ldapTestConfig.StoreLocation = StoreLocation.LocalMachine;
            ldapTestConfig.StoreName = StoreName.Root;
        }

        /// <summary>
        /// Use default, live values
        /// </summary>
        public void SetIfNotExistsDefaultLdapConfig() {
            if (ConfigurationHandler.HasConfigurationSection<LdapSettings>())
                return;
            SetDefaultLdapConfig();
        }

    }
}
