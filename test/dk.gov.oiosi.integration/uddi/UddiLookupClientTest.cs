﻿using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.category;
using NUnit.Framework;

namespace dk.gov.oiosi.integration.uddi {
    [TestFixture]
    public class UddiLookupClientTest {

        [TestFixture]
        public class Processes {
            public const string SERVICEIDENTIFIER = "5798009811578";
            public const string ORDERSERVICETYPEID = "uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a";
            public const string SIMPLEBILLINGPROCESSID = "uddi:98070e14-ee30-4b10-84ef-986cde3b8116";
            public const string SIMPLEORDERPROCESSID = "uddi:1e1b209e-7b8d-4f1e-8f2a-f3d9a94d1086";
            public const string NESPROFILE5PROCESSID = "uddi:aee8b6de-298f-4cbc-a96d-9ae8aed0ac31";
            
            [Test]
            public void _01_OneProcessNoResultsLookup() {
                IIdentifier identifier = new IdentifierEan(SERVICEIDENTIFIER);
                UddiId serviceDefinitionId = new UddiGuidId(ORDERSERVICETYPEID, true);
                List<UddiId> processDefinitionIds = new List<UddiId>();
                processDefinitionIds.Add(new UddiGuidId(SIMPLEBILLINGPROCESSID, true));
                List<UddiLookupResponse> responses = Lookup(identifier, serviceDefinitionId, processDefinitionIds);
                Assert.AreEqual(0, responses.Count);
            }

            [Test]
            public void _02_OneProcessOneResultLookup() {
                IIdentifier identifier = new IdentifierEan(SERVICEIDENTIFIER);
                UddiId serviceDefinitionId = new UddiGuidId(ORDERSERVICETYPEID, true);
                List<UddiId> processDefinitionIds = new List<UddiId>();
                processDefinitionIds.Add(new UddiGuidId(SIMPLEORDERPROCESSID, true));
                List<UddiLookupResponse> responses = Lookup(identifier, serviceDefinitionId, processDefinitionIds);
                Assert.AreEqual(1, responses.Count);
            }

            [Test]
            public void _03_MultipleProcessesNoResultsLookup() {
                IIdentifier identifier = new IdentifierEan(SERVICEIDENTIFIER);
                UddiId serviceDefinitionId = new UddiGuidId(ORDERSERVICETYPEID, true);
                List<UddiId> processDefinitionIds = new List<UddiId>();
                processDefinitionIds.Add(new UddiGuidId(SIMPLEBILLINGPROCESSID, true));
                processDefinitionIds.Add(new UddiGuidId(NESPROFILE5PROCESSID, true));
                List<UddiLookupResponse> responses = Lookup(identifier, serviceDefinitionId, processDefinitionIds);
                Assert.AreEqual(0, responses.Count);
            }

            [Test]
            public void _04_MultipleProcessesOneResultLookup() {
                IIdentifier identifier = new IdentifierEan(SERVICEIDENTIFIER);
                UddiId serviceDefinitionId = new UddiGuidId(ORDERSERVICETYPEID, true);
                List<UddiId> processDefinitionIds = new List<UddiId>();
                processDefinitionIds.Add(new UddiGuidId(SIMPLEBILLINGPROCESSID, true));
                processDefinitionIds.Add(new UddiGuidId(SIMPLEORDERPROCESSID, true));
                List<UddiLookupResponse> responses = Lookup(identifier, serviceDefinitionId, processDefinitionIds);
                Assert.AreEqual(1, responses.Count);
            }


            private List<UddiLookupResponse> Lookup(IIdentifier identifier, UddiId serviceDefinitionId, IEnumerable<UddiId> processDefinitionIds) {
                UddiLookupClient lookupClient = new UddiLookupClient();
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
                LookupReturnOptionEnum lookupReturnOptionEnum = LookupReturnOptionEnum.allResults;
                LookupParameters parameters = new LookupParameters(identifier, identifierType, adressTypeFilter,
                                                                   preferredEndpointType, lookupReturnOptionEnum,
                                                                   serviceDefinitionId, null, null, processDefinitionIds);
                return lookupClient.Lookup(parameters);
            }
        }
    }
}
