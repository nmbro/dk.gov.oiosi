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
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using NUnit.Framework;
using dk.gov.oiosi.common.cache;

namespace dk.gov.oiosi.test.unit.uddi{
    [TestFixture]
    public class RegistryLookupTest {
        // The fake registries (and fallback servers)
    	private Uri firstRegistry = new Uri("http://test1.com");
		private Uri firstFallback = new Uri("http://fallback1.com");
        private Uri secondRegistry = new Uri("http://test2.com");
        private Uri secondFallback = new Uri("http://fallback2.com");
        private Uri thirdRegistry = new Uri("http://test3.com");
		private Uri fourthRegistry = new Uri("http://test4.com");
		private Uri fourthFallback1 = new Uri("http://fallback41.com");
		private Uri fourthFallback2 = new Uri("http://fallback42.com");

        // The fake endpoints
		private IdentifierEan endpointInNoRegistry = new IdentifierEan("5700000000000");
    	private IdentifierEan endpointInFirstRegistry = new IdentifierEan("5700000000001");
    	private IdentifierEan endpointInSecondRegistry = new IdentifierEan("5700000000002");
		private IdentifierEan endpointInFourthRegistry = new IdentifierEan("5700000000004");


        [SetUp]
        public void SetUp() {
            ConfigurationHandler.ConfigFilePath = "Resources\\RaspConfigurationUddi.xml";
            ConfigurationHandler.Reset();
            SetUpConfiguration();
        }

        [Test]
        public void _01_TestSuccesfulOnFirstLookup() {
            GetClearDummyConfig();
            IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
            List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInFirstRegistry));
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(firstRegistry.ToString(), result[0].EndpointAddress.GetKeyAsString());
        }

        [Test]
        public void _02_TestSuccesfulOnFirstFallback() {
            AdvancedUddiDummyClient.AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
            config.ErroneousEndpoints.Add(firstRegistry);
            IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
            List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInFirstRegistry));
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(firstFallback.ToString(), result[0].EndpointAddress.GetKeyAsString());
        }

        [Test, ExpectedException(typeof(UddiException))]
        public void _03_TestFailureOnFirstRegistry() {
            AdvancedUddiDummyClient.AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
            config.ErroneousEndpoints.Add(firstRegistry);
            config.ErroneousEndpoints.Add(firstFallback);
            IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
            List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInFirstRegistry));
            Assert.IsEmpty(result);
        }

        [Test]
        public void _04_TestSuccesfulOnSecondRegistry() {
            AdvancedUddiDummyClient.AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
            config.NonExistingRegistrations.Add(firstRegistry, new List<Identifier> { endpointInSecondRegistry });
            IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
            List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInSecondRegistry));
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(secondRegistry.ToString(), result[0].EndpointAddress.GetKeyAsString());
        }

        [Test]
        public void _05_TestSuccesfulOnLastFallback() {
            AdvancedUddiDummyClient.AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
            config.NonExistingRegistrations.Add(firstRegistry, new List<Identifier> { endpointInFourthRegistry });
            config.NonExistingRegistrations.Add(secondRegistry, new List<Identifier> { endpointInFourthRegistry });
            config.NonExistingRegistrations.Add(thirdRegistry, new List<Identifier> { endpointInFourthRegistry });
            config.ErroneousEndpoints.Add(fourthRegistry);
            config.ErroneousEndpoints.Add(fourthFallback1);

            IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
            List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInFourthRegistry));
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(fourthFallback2.ToString(), result[0].EndpointAddress.GetKeyAsString());
        }

        [Test]
        public void _06_TestUnsuccesfulOnAllRegistries() {
            AdvancedUddiDummyClient.AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
            config.NonExistingRegistrations.Add(firstRegistry, new List<Identifier> { endpointInNoRegistry });
            config.NonExistingRegistrations.Add(secondRegistry, new List<Identifier> { endpointInNoRegistry });
            config.NonExistingRegistrations.Add(thirdRegistry, new List<Identifier> { endpointInNoRegistry });
            config.NonExistingRegistrations.Add(fourthRegistry, new List<Identifier> { endpointInNoRegistry });

            IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
            List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInNoRegistry));
            Assert.IsEmpty(result);
        }

        private LookupParameters CreateParams(IdentifierEan ean) {
            return new LookupParameters(
                ean,
                new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26fa1111", true),
                new List<EndpointAddressTypeCode>() {EndpointAddressTypeCode.http});
        }

        private AdvancedUddiDummyClient.AdvancedUddiDummyClientConfig GetClearDummyConfig() {
            // Clears the dummy so that all calls return a result
            AdvancedUddiDummyClient.AdvancedUddiDummyClientConfig dummyConfig = ConfigurationHandler.GetConfigurationSection<AdvancedUddiDummyClient.AdvancedUddiDummyClientConfig>();
            dummyConfig.NonExistingRegistrations.Clear();
            dummyConfig.ErroneousEndpoints.Clear();
            return dummyConfig;
        }

        private void SetUpConfiguration(){
            // Configures fallback sequence
            UddiConfig uddiConfig = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            uddiConfig.FallbackTimeoutMinutes = 1;
            uddiConfig.PublishEndpoint = uddiConfig.SecurityEndpoint = "http://a.com";
            uddiConfig.LookupRegistryFallbackConfig = new LookupRegistryFallbackConfig();
            uddiConfig.LookupRegistryFallbackConfig.PrioritizedRegistryList.Add(
                new Registry(
                    new List<string>(){
                        firstRegistry.ToString(), 
                        firstFallback.ToString()}));

            uddiConfig.LookupRegistryFallbackConfig.PrioritizedRegistryList.Add(
                new Registry(
                    new List<string>(){
                        secondRegistry.ToString(), 
                        secondFallback.ToString()}));

            uddiConfig.LookupRegistryFallbackConfig.PrioritizedRegistryList.Add(
                new Registry(
                    new List<string>(){
                        thirdRegistry.ToString()}));

            uddiConfig.LookupRegistryFallbackConfig.PrioritizedRegistryList.Add(
                new Registry(
                    new List<string>(){
                        fourthRegistry.ToString(), 
                        fourthFallback1.ToString(),
                        fourthFallback2.ToString()}));

            // Configures factories for UDDI lookup clients used
            RegistryLookupClientFactoryConfig registryLookupClientFactoryConfig = ConfigurationHandler.GetConfigurationSection<RegistryLookupClientFactoryConfig>();
            registryLookupClientFactoryConfig.ImplementationNamespaceClass = typeof (RegistryLookupClient).FullName;
            registryLookupClientFactoryConfig.ImplementationAssembly = typeof(RegistryLookupClient).Assembly.FullName;
            UddiLookupClientFactoryConfig uddiLookupClientFactoryConfig = ConfigurationHandler.GetConfigurationSection<UddiLookupClientFactoryConfig>();
            uddiLookupClientFactoryConfig.ImplementationNamespaceClass = typeof(AdvancedUddiDummyClient).FullName;
            uddiLookupClientFactoryConfig.ImplementationAssembly = typeof(AdvancedUddiDummyClient).Assembly.FullName;
        }
    }
}