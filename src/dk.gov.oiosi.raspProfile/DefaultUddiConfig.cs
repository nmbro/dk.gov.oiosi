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
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// Creates the default UDDI configuration
    /// </summary>
    public class DefaultUddiConfig {

        /// <summary>
        /// Sets the default UDDI lookup factory configuration
        /// </summary>
        public void SetUddiLookupFactoryConfig() {
            UddiLookupClientFactoryConfig uddiFactoryConfig = ConfigurationHandler.GetConfigurationSection<UddiLookupClientFactoryConfig>();
            uddiFactoryConfig.ImplementationAssembly = "dk.gov.oiosi.library";
            uddiFactoryConfig.ImplementationNamespaceClass = "dk.gov.oiosi.uddi.UddiLookupClient";

            RegistryLookupClientFactoryConfig regFactoryConfig = ConfigurationHandler.GetConfigurationSection<RegistryLookupClientFactoryConfig>();
            regFactoryConfig.ImplementationAssembly = "dk.gov.oiosi.library";
            regFactoryConfig.ImplementationNamespaceClass = "dk.gov.oiosi.uddi.RegistryLookupClient";
        }

        /// <summary>
        /// Sets the test UDDI lookup factory configuration
        /// </summary>
        public void SetTestUddiLookupFactoryConfig() {
            UddiLookupClientFactoryConfig uddiFactoryConfig = ConfigurationHandler.GetConfigurationSection<UddiLookupClientFactoryConfig>();
            uddiFactoryConfig.ImplementationAssembly = "dk.gov.oiosi.library";
            uddiFactoryConfig.ImplementationNamespaceClass = "dk.gov.oiosi.uddi.UddiLookupClientTest";

            RegistryLookupClientFactoryConfig regFactoryConfig = ConfigurationHandler.GetConfigurationSection<RegistryLookupClientFactoryConfig>();
            regFactoryConfig.ImplementationAssembly = "dk.gov.oiosi.library";
            uddiFactoryConfig.ImplementationNamespaceClass = "dk.gov.oiosi.uddi.RegistryLookupClient";
        }

        /// <summary>
        /// Sets the lookup factory configuration if it does not exist
        /// </summary>
        public void SetIfNotExistsUddiLookupFactoryConfig() {
            if (ConfigurationHandler.HasConfigurationSection<UddiLookupClientFactoryConfig>())
                return;
            SetUddiLookupFactoryConfig();
        }

        /// <summary>
        /// Sets the test uddi lookup factory configuration if it does not exist
        /// </summary>
        public void SetIfNotExistsTestUddiLookupFactoryConfig() {
            if (ConfigurationHandler.HasConfigurationSection<UddiLookupClientFactoryConfig>())
                return;
            SetTestUddiLookupFactoryConfig();
        }

        /// <summary>
        /// Sets the default uddi configuration
        /// </summary>
        public void SetDefaultUddiConfig() {
            UddiConfig uddiConfig = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            uddiConfig.TryOtherHostsOnFailure = true;
            uddiConfig.PublishEndpoint = "https://publish.uddi.ehandel.gov.dk/UDDIProxy/UDDIProxy.svc";
            uddiConfig.PublishInquiryEndpoint = "http://publish.uddi.ehandel.gov.dk/registry/uddi/inquiry";                                                 
            uddiConfig.SecurityEndpoint = "http://publish.uddi.ehandel.gov.dk/registry/uddi/security";
            uddiConfig.FallbackTimeoutMinutes = 15;
            uddiConfig.LookupRegistryFallbackConfig = new LookupRegistryFallbackConfig();
            uddiConfig.LookupRegistryFallbackConfig.PrioritizedRegistryList.Add(new Registry(new List<string>() { "http://discoverypublic.uddi.ehandel.gov.dk/registry/uddi/inquiry", "http://discoverybackup.uddi.ehandel.gov.dk/registry/uddi/inquiry" }));
            uddiConfig.LookupRegistryFallbackConfig.PrioritizedRegistryList.Add(new Registry(new List<string>() { "http://discoverygateway.uddi.ehandel.gov.dk/UddiAdapterService/2008/10/27/UddiAdapterService.svc" }));
        }

        private void SetDefaultUddiConfigTest() {
            // Set UDDI test config:
            UddiLookupClientTestConfig uddiTestConfig = ConfigurationHandler.GetConfigurationSection<UddiLookupClientTestConfig>();
            // The oiosi test VOCES certificate:
            uddiTestConfig.CertificateSubjectSerialNumber =
               "CN=NemHandel Test 2 + SERIALNUMBER=CVR:26769388-DID:00000002, O=IT- og Telestyrelsen // CVR:26769388, C=DK";

            // Example of a revoked endpoint certificate:
            /*uddiTestConfig.CertificateSubjectSerialNumber =
               "OID.2.5.4.5=CVR:26769388-UID:1157365203353 + CN=IT- og Telestyrelsen - OIOSI-Test, O=IT- og Telestyrelsen // CVR:26769388, C=DK"; */

            uddiTestConfig.EndpointAddress = "http://oiositest.dk/interoptest/oiosiOmniEndpointB.svc";
            uddiTestConfig.ActivationDate = new DateTime(2007, 1, 1);
            uddiTestConfig.ExpirationDate = new DateTime(2012, 6, 1);
            uddiTestConfig.HasNewerVersion = false;
            uddiTestConfig.NewerVersionReference = "";
            uddiTestConfig.ServiceContactEmail = "test@test.com";
            uddiTestConfig.TermsOfUseUrl = "http://test.dk/termsOfUse.html";
            uddiTestConfig.Version = "1.0.3";
        }

        /// <summary>
        /// Sets the default uddi configuration if it does not exist
        /// </summary>
        public void SetIfNotExistsDefaultUddiConfig() {
            if (ConfigurationHandler.HasConfigurationSection<UddiConfig>())
                return;
            SetDefaultUddiConfig();
        }

        /// <summary>
        /// Sets the default test uddi configuration if it does not exist
        /// </summary>
        public void SetIfNotExistsDefaultUddiConfigTest() {
            if (ConfigurationHandler.HasConfigurationSection<UddiLookupClientTestConfig>())
                return;
            SetDefaultUddiConfigTest();
        }
    }
}
