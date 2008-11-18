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
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;
using NUnit.Framework;

namespace dk.gov.oiosi.test.nunit.library.uddi{
    [TestFixture]
    public class RegistryLookupTest{

    	private Uri firstRegistry = new Uri("http://test1.com");
		private Uri firstFallback = new Uri("http://fallback1.com");
		private Uri secondRegistry = new Uri("http://test2.com");
		private Uri thirdRegistry = new Uri("http://test3.com");
		private Uri fourthRegistry = new Uri("http://test4.com");
		private Uri fourthFallback1 = new Uri("http://fallback41.com");
		private Uri fourthFallback2 = new Uri("http://fallback42.com");


		private IdentifierEan endpointInNoRegistry = new IdentifierEan("5700000000000");
    	private IdentifierEan endpointInFirstRegistry = new IdentifierEan("5700000000001");
    	private IdentifierEan endpointInSecondRegistry = new IdentifierEan("5700000000002");
		private IdentifierEan endpointInFourthRegistry = new IdentifierEan("5700000000004");
    	

    	[SetUp]
        public void SetUp(){
            ConfigurationHandler.ConfigFilePath = "..\\..\\Resources\\RaspConfiguration.xml";
        }

		private LookupParameters CreateParams(IdentifierEan ean){
			return new LookupParameters(
				ean, new EndpointKeytype(EndpointKeyTypeCode.ean),
				null,
				PreferredEndpointType.http,
				LookupReturnOptionEnum.firstResult,
				null,
				new BusinessProcessRoleIdentifierType(),
				new BusinessProcessRoleIdentifier(), new UddiId[0]);
		}

		private AdvancedUddiDummyClientConfig GetClearDummyConfig(){
			// Clears the dummy so that all calls return a result
			AdvancedUddiDummyClientConfig dummyConfig = ConfigurationHandler.GetConfigurationSection<AdvancedUddiDummyClientConfig>();
			dummyConfig.NonExistingRegistrations.Clear();
			dummyConfig.ErroneousEndpoints.Clear();
			return dummyConfig;
		}

        [Test]
        public void _01_TestSuccesfulOnFirstLookup(){
        	GetClearDummyConfig();
        	IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
			List<UddiLookupResponse>result = client.Lookup(CreateParams(endpointInFirstRegistry));
			Assert.IsNotNull(result);
			Assert.IsNotEmpty(result);
        }

        [Test]
        public void _02_TestSuccesfulOnFirstFallback(){
			AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
			config.ErroneousEndpoints.Add(firstRegistry);
			IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
			List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInFirstRegistry));
			Assert.IsNotNull(result);
			Assert.IsNotEmpty(result);
        }

		[Test,ExpectedException(typeof(UddiLookupException))]
		public void _03_TestFailureOnFirstRegistry() {
			AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
			config.ErroneousEndpoints.Add(firstRegistry);
			config.ErroneousEndpoints.Add(firstFallback);
			IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
			List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInFirstRegistry));
			Assert.IsEmpty(result);
		}

        [Test]
        public void _04_TestSuccesfulOnSecondRegistry(){
			AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
			config.NonExistingRegistrations.Add(firstRegistry, new List<IIdentifier>{endpointInSecondRegistry});
			IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
			List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInSecondRegistry));
			Assert.IsNotNull(result);
			Assert.IsNotEmpty(result);
        }

		[Test]
		public void _05_TestSuccesfulOnLastFallback() {
			AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
			config.NonExistingRegistrations.Add(firstRegistry, new List<IIdentifier> { endpointInFourthRegistry});
			config.NonExistingRegistrations.Add(secondRegistry, new List<IIdentifier> { endpointInFourthRegistry });
			config.NonExistingRegistrations.Add(thirdRegistry, new List<IIdentifier> { endpointInFourthRegistry });
			config.ErroneousEndpoints.Add(fourthRegistry);
			config.ErroneousEndpoints.Add(fourthFallback1);

			IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
			List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInFourthRegistry));
			Assert.IsNotNull(result);
			Assert.IsNotEmpty(result);
		}

        [Test]
        public void _05_TestUnsuccesfulOnAllRegistries(){
			AdvancedUddiDummyClientConfig config = GetClearDummyConfig();
			config.NonExistingRegistrations.Add(firstRegistry, new List<IIdentifier> { endpointInNoRegistry });
			config.NonExistingRegistrations.Add(secondRegistry, new List<IIdentifier> { endpointInNoRegistry });
			config.NonExistingRegistrations.Add(thirdRegistry, new List<IIdentifier> { endpointInNoRegistry });
			config.NonExistingRegistrations.Add(fourthRegistry, new List<IIdentifier> { endpointInNoRegistry });

			IUddiLookupClient client = new RegistryLookupClientFactory().CreateUddiLookupClient();
			List<UddiLookupResponse> result = client.Lookup(CreateParams(endpointInNoRegistry));
			Assert.IsEmpty(result);
        }

    }
}