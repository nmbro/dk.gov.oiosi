using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.category;
using NUnit.Framework;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.test.integration.uddi {
    [TestFixture]
    [Ignore]
    public class UddiLookupClientTest {

        private Uri uddiServerUri;

        private readonly IIdentifier eanIdentifier = new IdentifierEan("5798009811578");
        private readonly IIdentifier dunsIdentifier = new IdentifierDuns("1234567890");

        private readonly UddiId nonExistingServiceId = new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26fa1111", true);
        private readonly UddiId orderServiceId = new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a", true);
        private readonly UddiId invoiceServiceId = new UddiGuidId("uddi:2e0b402a-7a5e-476b-8686-b33f54fd1f47", true);

        private readonly UddiId nonExistingProfileUddiId = new UddiGuidId("uddi:88fbd6d5-6a25-4c08-91cc-5344c73c1111", true);
        private readonly UddiId procurementOrdAdvBilSimProfileUddiId = new UddiGuidId("uddi:88fbd6d5-6a25-4c08-91cc-5344c73c4d69", true);
        private readonly UddiId nesublProfilesProfile5 = new UddiGuidId("uddi:AEE8B6DE-298F-4cbc-A96D-9AE8AED0AC31", true);

        private const string nonExistingRoleIdentifier = "NonExistingSellerParty";
        private const string sellerPartyRoleIdentifier = "SellerParty";
        private const string buyerPartyRoleIdentifier = "BuyerParty";

        private List<EndpointAddressTypeCode> acceptHttpProtocol = new List<EndpointAddressTypeCode>() { EndpointAddressTypeCode.http };
        private List<EndpointAddressTypeCode> acceptSmtpProtocol = new List<EndpointAddressTypeCode>() { EndpointAddressTypeCode.email };
        
        [TestFixtureSetUp]
        public void SetupUddi() {
            ConfigurationUtil.SetupConfiguration();
            UddiConfig config = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            uddiServerUri = new Uri(config.LookupRegistryFallbackConfig.PrioritizedRegistryList[0].Endpoints[0]);
        }

        [Test]
        public void LookupExistingServiceMustReturnEndpoint() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };
            var lookupParameters = new UddiLookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);

            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count, "Exactly 1 endpoint expected.");
            
            var expectedEndpoint = "http://193.163.141.141/TestEndpoint/OiosiOmniEndpointA.svc";
            var actualEndpoint = lookupResponses[0].EndpointAddress.GetAsUri().AbsoluteUri;
            Assert.AreEqual(expectedEndpoint, actualEndpoint);
        }
        
        [Test]
        public void LookingUpExistingServiceMustReturnCertificateSubjectString() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };
            var lookupParameters = new UddiLookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);
            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            
            Assert.Greater(lookupResponses.Count, 0);

            var expectedCertificateSubjectString = "CN=NemHandel test service (funktionscertifikat) + SERIALNUMBER=CVR:26769388-FID:1200406941690 + O=IT- og Telestyrelsen // CVR:26769388 + C=DK";
            var actualCertificateSubjectString = lookupResponses[0].CertificateSubjectSerialNumber.SubjectString;
            Assert.AreEqual(expectedCertificateSubjectString, actualCertificateSubjectString);
        }

        [Test]
        public void LookingUpNonExistingServiceShouldReturnEmptyResponse() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };
            
            var lookupParameters = new UddiLookupParameters(eanIdentifier, nonExistingServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithoutProfileMustReturnEmptyResponseList() {
            List<UddiId> profileIds = new List<UddiId>() { nonExistingProfileUddiId };

            var lookupParameters = new UddiLookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithoutRoleMustReturnEmptyResponseList() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };

            var lookupParameters = new UddiLookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol, nonExistingRoleIdentifier);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceTwoProfilesBothExistingMustReturnResponse() {
            List<UddiId> profileIds = new List<UddiId> { procurementOrdAdvBilSimProfileUddiId, nesublProfilesProfile5 };

            var lookupParameters = new UddiLookupParameters(eanIdentifier, invoiceServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceTwoProfilesOneExistingMustReturnResponse() {
            List<UddiId> profileIds = new List<UddiId> { nonExistingProfileUddiId, nesublProfilesProfile5 };

            var lookupParameters = new UddiLookupParameters(eanIdentifier, invoiceServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithDunsIdentifierShouldReturnResponse() {
            List<UddiId> profileIds = new List<UddiId> { procurementOrdAdvBilSimProfileUddiId };

            var lookupParameters = new UddiLookupParameters(dunsIdentifier, invoiceServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpHttpOnlyServiceWithAcceptedProtocolTypeSetToSmtpShouldReturnEmptyResponse() {
            List<UddiId> profileIds = new List<UddiId> { procurementOrdAdvBilSimProfileUddiId };

            var lookupParameters = new UddiLookupParameters(eanIdentifier, invoiceServiceId, profileIds, acceptSmtpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        # region Helper methods

        private List<UddiLookupResponse> GetEndpointsWithProfileFromUddi(UddiLookupParameters lookupParameters) {
            UddiLookupClient lookupClient = new UddiLookupClient(uddiServerUri);
            return lookupClient.Lookup(lookupParameters);
        }

        # endregion

        //        [Test]
//        public void LookingUpNonexistingServiceShouldReturnEmptyResponse()
//        {
//            ConfigurationUtil.SetupConfiguration();
//
//            string eanIdentifier = "5798009811578";
//            string nonExistingServiceId = "uddi:b138dc71-d301-42d1-8c2e-2c3a26fa1111";
//            //string ordAdvBilSimSellerParty = "uddi:f4c46f00-1146-11dd-a56f-32872391a563";
//
//            IIdentifier identifier = new IdentifierEan(eanIdentifier);
//            UddiId nonExistingServiceUddiId = IdentifierUtility.GetUddiIDFromString(nonExistingServiceId);
//
//            // TODO: Maybe use RegistrationClient
//            UddiConfig config = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
//            Uri uddiServerUri = new Uri(config.LookupRegistryFallbackConfig.PrioritizedRegistryList[0].Endpoints[0]);
//            UddiLookupClient lookupClient = new UddiLookupClient(uddiServerUri);
//            //LookupParameters lookupParameters = new LookupParameters(identifier, nonExistingServiceUddiId);
//            List<string> endpoints = lookupClient.Lookup(identifier, nonExistingServiceUddiId);
//
//            //List<EndpointAddressTypeCode> adressTypeFilter = new List<EndpointAddressTypeCode>();
//            //PreferredEndpointType preferredEndpointType = PreferredEndpointType.http;
//            //LookupReturnOption lookupReturnOption = LookupReturnOption.allResults;
//            //LookupParameters parameters = new LookupParameters(identifier, identifierType, adressTypeFilter,
//            //                                                   preferredEndpointType, lookupReturnOption,
//            //                                                   serviceDefinitionId, null, null, processDefinitionIds, new TimedNullCache<LookupKey, List<UddiLookupResponse>>());
//
//
//            //List<UddiLookupResponse> responses = Lookup(identifier, serviceDefinitionId, processDefinitionIds);
//            Assert.AreEqual(0, endpoints.Count);
//
//        }

        [TestFixture]
        [Ignore]
        public class Processes {
            public const string SERVICEIDENTIFIER = "5798009811578";
            public const string ORDERSERVICETYPEID = "uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a";
            public const string SIMPLEBILLINGPROCESSID = "uddi:98070e14-ee30-4b10-84ef-986cde3b8116";
            public const string SIMPLEORDERPROCESSID = "uddi:142c4188-3d53-440d-a64f-68d7c3b9a59b";
            public const string NESPROFILE5PROCESSID = "uddi:aee8b6de-298f-4cbc-a96d-9ae8aed0ac31";
            

            [Test]
            public void _01_OneProcessNoResultsLookup() {
                IIdentifier identifier = new IdentifierEan(SERVICEIDENTIFIER);
                UddiId serviceDefinitionId = IdentifierUtility.GetUddiIDFromString(ORDERSERVICETYPEID);
                List<UddiId> processDefinitionIds = new List<UddiId>();
                processDefinitionIds.Add(IdentifierUtility.GetUddiIDFromString(SIMPLEBILLINGPROCESSID));
                List<UddiLookupResponse> responses = Lookup(identifier, serviceDefinitionId, processDefinitionIds);
                Assert.AreEqual(0, responses.Count);
            }

            [Test]
            public void _02_OneProcessOneResultLookup() {
                IIdentifier identifier = new IdentifierEan(SERVICEIDENTIFIER);
                UddiId serviceDefinitionId = IdentifierUtility.GetUddiIDFromString(ORDERSERVICETYPEID);
                List<UddiId> processDefinitionIds = new List<UddiId>();
                processDefinitionIds.Add(IdentifierUtility.GetUddiIDFromString(SIMPLEORDERPROCESSID));
                List<UddiLookupResponse> responses = Lookup(identifier, serviceDefinitionId, processDefinitionIds);
                Assert.AreEqual(1, responses.Count);
                bool foundProcessInformation = false;
                foreach (UddiProcessInformation process in responses[0].Processes) {
                    if (process.ProcessDefinitionId.ID != SIMPLEORDERPROCESSID) continue;
                    foundProcessInformation = true;
                }
                Assert.IsTrue(foundProcessInformation);
            }

            [Test]
            public void _03_MultipleProcessesNoResultsLookup() {
                IIdentifier identifier = new IdentifierEan(SERVICEIDENTIFIER);
                UddiId serviceDefinitionId = IdentifierUtility.GetUddiIDFromString(ORDERSERVICETYPEID);
                List<UddiId> processDefinitionIds = new List<UddiId>();
                processDefinitionIds.Add(IdentifierUtility.GetUddiIDFromString(SIMPLEBILLINGPROCESSID));
                processDefinitionIds.Add(IdentifierUtility.GetUddiIDFromString(NESPROFILE5PROCESSID));
                List<UddiLookupResponse> responses = Lookup(identifier, serviceDefinitionId, processDefinitionIds);
                Assert.AreEqual(0, responses.Count);
            }

            [Test]
            public void _04_MultipleProcessesOneResultLookup() {
                IIdentifier identifier = new IdentifierEan(SERVICEIDENTIFIER);
                UddiId serviceDefinitionId = IdentifierUtility.GetUddiIDFromString(ORDERSERVICETYPEID);
                List<UddiId> processDefinitionIds = new List<UddiId>();
                processDefinitionIds.Add(IdentifierUtility.GetUddiIDFromString(SIMPLEBILLINGPROCESSID));
                processDefinitionIds.Add(IdentifierUtility.GetUddiIDFromString(SIMPLEORDERPROCESSID));
                List<UddiLookupResponse> responses = Lookup(identifier, serviceDefinitionId, processDefinitionIds);
                Assert.AreEqual(1, responses.Count);
                bool foundProcessInformation = false;
                foreach (UddiProcessInformation process in responses[0].Processes) {
                    if (process.ProcessDefinitionId.ID != SIMPLEORDERPROCESSID) continue;
                    foundProcessInformation = true;
                }
                Assert.IsTrue(foundProcessInformation);
            }


            private List<UddiLookupResponse> Lookup(IIdentifier identifier, UddiId serviceDefinitionId, IEnumerable<UddiId> processDefinitionIds) {
                UddiConfig config = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
                UddiLookupClient lookupClient = new UddiLookupClient(new Uri(config.LookupRegistryFallbackConfig.PrioritizedRegistryList[0].Endpoints[0]));
                EndpointKeytype identifierType;

                if (identifier is IdentifierEan) {
                    identifierType = new EndpointKeytype(EndpointKeyTypeCode.ean);
                } 
                else if (identifier is IdentifierOvt) {
                    identifierType = new EndpointKeytype(EndpointKeyTypeCode.ovt);
                }
                else if (identifier is IdentifierCvr) {
                    identifierType = new EndpointKeytype(EndpointKeyTypeCode.cvr);
                }
                else {
                    identifierType = new EndpointKeytype(EndpointKeyTypeCode.other);
                }
                
                List<EndpointAddressTypeCode> adressTypeFilter = new List<EndpointAddressTypeCode>();
                PreferredEndpointType preferredEndpointType = PreferredEndpointType.http;
                LookupReturnOption lookupReturnOption = LookupReturnOption.allResults;
                LookupParameters parameters = new LookupParameters(identifier, identifierType, adressTypeFilter,
                                                                   preferredEndpointType, lookupReturnOption,
                                                                   serviceDefinitionId, null, null, processDefinitionIds, new TimedNullCache<LookupKey, List<UddiLookupResponse>>());
                return lookupClient.Lookup(parameters);
            }
        }
    }
}