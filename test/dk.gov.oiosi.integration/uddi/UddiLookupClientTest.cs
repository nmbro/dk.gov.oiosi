using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.category;
using NUnit.Framework;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.integration.uddi {
    
    [TestFixture]
    [Ignore]
    public class UddiLookupClientTest {

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
