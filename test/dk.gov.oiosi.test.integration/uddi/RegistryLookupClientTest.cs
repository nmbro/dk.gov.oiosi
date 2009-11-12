﻿using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.addressing;

namespace dk.gov.oiosi.test.integration.uddi {
    [TestFixture]
    public class RegistryLookupClientTest {

        [Test]
        public void LookingUpWhatProfilesAnIdentifierCanSupport() {
            var identifier = new IdentifierEan(TestConstants.TESTEAN);
            var orderService = new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a", true);
            var acceptedProtocols = new List<EndpointAddressTypeCode>();
            acceptedProtocols.Add(EndpointAddressTypeCode.http);
            RegistryLookupClientFactory rlcf = new RegistryLookupClientFactory();
            IUddiLookupClient ulc = rlcf.CreateUddiLookupClient();
            LookupParameters lookupParameters = new LookupParameters(identifier, orderService, acceptedProtocols);
            List<UddiLookupResponse> results = ulc.Lookup(lookupParameters);

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            //There is still support for getting the process roles like the Process
            //Assert.IsNotNull(results[0].Process);
            Assert.IsNotNull(results[0].ProcessRoles);
            //There is still support for getting the process roles via. the Process
            //IEnumerator<ProcessRoleDefinition> enumerator = results[0].Process.GetEnumerator();
            IEnumerator<ProcessRoleDefinition> enumerator = results[0].ProcessRoles.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            //The name of the process role defintion
            Assert.AreEqual("Procurement-OrdAdv-BilSim-1.0 SellerParty", enumerator.Current.Name);
            //The id of the process definition
            Assert.AreEqual("uddi:88fbd6d5-6a25-4c08-91cc-5344c73c4d69", enumerator.Current.ProcessDefinitionId.ID);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void GetProcessDefinitions() {
            List<UddiId> uddiIds = new List<UddiId>();
            uddiIds.Add(new UddiStringId("uddi:AEE8B6DE-298F-4cbc-A96D-9AE8AED0AC31", true));
            uddiIds.Add(new UddiStringId("uddi:c001daa0-8ba3-11dd-894e-770465b08940", true));

            RegistryLookupClientFactory rlcf = new RegistryLookupClientFactory();
            IUddiLookupClient ulc = rlcf.CreateUddiLookupClient();

            List<ProcessDefinition> processes = ulc.GetProcessDefinitions(uddiIds);
            Assert.AreEqual(uddiIds.Count, processes.Count);
        }
    }
}
