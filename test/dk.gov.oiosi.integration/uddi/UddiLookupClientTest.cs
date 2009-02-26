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

        [TestFixtureSetUp]
        public void SetupUddi() {
            ConfigurationUtil.SetupConfiguration();
            UddiConfig config = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            uddiServerUri = new Uri(config.LookupRegistryFallbackConfig.PrioritizedRegistryList[0].Endpoints[0]);
        }

        [Test]
        public void LookupExistingServiceMustReturnEndpoint() {
            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddiWithDefaultValues();
            Assert.AreEqual(1, lookupResponses.Count, "Exactly 1 endpoint expected.");
            
            var expectedEndpoint = "http://193.163.141.141/TestEndpoint/OiosiOmniEndpointA.svc";
            var actualEndpoint = lookupResponses[0].EndpointAddress.GetAsUri().AbsoluteUri;
            Assert.AreEqual(expectedEndpoint, actualEndpoint);
        }
        
        [Test]
        public void LookingUpExistingServiceMustReturnCertificateSubjectString() {
            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddiWithDefaultValues();
            
            Assert.Greater(lookupResponses.Count, 0);

            var expectedCertificateSubjectString = "CN=NemHandel test service (funktionscertifikat) + SERIALNUMBER=CVR:26769388-FID:1200406941690 + O=IT- og Telestyrelsen // CVR:26769388 + C=DK";
            var actualCertificateSubjectString = lookupResponses[0].CertificateSubjectSerialNumber.SubjectString;
            Assert.AreEqual(expectedCertificateSubjectString, actualCertificateSubjectString);
        }

        [Test]
        public void LookingUpNonExistingServiceShouldReturnEmptyResponse() {
            string eanIdentifier = "5798009811578";
            string nonExistingServiceId = "uddi:b138dc71-d301-42d1-8c2e-2c3a26fa1111";
            string procurementOrdAdvBilSimProfileUddiId = "uddi:88fbd6d5-6a25-4c08-91cc-5344c73c4d69";
            string sellerPartyRoleIdentifier = "SellerParty";

            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(eanIdentifier, nonExistingServiceId, procurementOrdAdvBilSimProfileUddiId, sellerPartyRoleIdentifier);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithoutProfileMustReturnEmptyResponseList() {
            string eanIdentifier = "5798009811578";
            string orderServiceId = "uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a";
            string nonExistingProfileUddiId = "uddi:88fbd6d5-6a25-4c08-91cc-5344c73c1111";
            string sellerPartyRoleIdentifier = "SellerParty";

            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(eanIdentifier, orderServiceId, nonExistingProfileUddiId, sellerPartyRoleIdentifier);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithoutRoleMustReturnEmptyResponseList() {
            string eanIdentifier = "5798009811578";
            string orderServiceId = "uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a";
            string procurementOrdAdvBilSimProfileUddiId = "uddi:88fbd6d5-6a25-4c08-91cc-5344c73c4d69";
            string nonExistingRoleIdentifier = "NonExistingSellerParty";

            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(eanIdentifier, orderServiceId, procurementOrdAdvBilSimProfileUddiId, nonExistingRoleIdentifier);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        private List<UddiLookupResponse> GetEndpointsWithProfileFromUddiWithDefaultValues() {
            string eanIdentifier = "5798009811578";
            string orderServiceId = "uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a";
            string procurementOrdAdvBilSimProfileUddiId = "uddi:88fbd6d5-6a25-4c08-91cc-5344c73c4d69";
            string sellerPartyRoleIdentifier = "SellerParty";

            return GetEndpointsWithProfileFromUddi(eanIdentifier, orderServiceId, procurementOrdAdvBilSimProfileUddiId, sellerPartyRoleIdentifier);
        }

        private List<UddiLookupResponse> GetEndpointsWithProfileFromUddi(string identifierId, string serviceId, string profileId, string profileRoleIdentifier) {

            UddiId procurementOrdAdvBilSimProfileUddiId = new UddiGuidId(profileId, true);

            IIdentifier identifier = new IdentifierEan(identifierId);
            UddiId existingServiceUddiId = IdentifierUtility.GetUddiIDFromString(serviceId);

            UddiLookupClient lookupClient = new UddiLookupClient(uddiServerUri);
            return lookupClient.Lookup(identifier, existingServiceUddiId, procurementOrdAdvBilSimProfileUddiId, profileRoleIdentifier);
        }


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