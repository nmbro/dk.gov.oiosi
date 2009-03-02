using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using NUnit.Framework;

namespace dk.gov.oiosi.test.integration.uddi {

    [TestFixture]
    public class UddiLookupClientTest {

        private Uri uddiServerUri;
        private string clientEndpointName = "OiosiClientEndpointInquiry";

        private readonly Identifier eanIdentifier = new IdentifierEan("5798009811578");
        private readonly Identifier dunsIdentifier = new IdentifierDuns("1234567890");

        private readonly UddiId nonExistingServiceId = new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26fa1111", true);
        private readonly UddiId orderServiceId = new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a", true);
        private readonly UddiId invoiceServiceId = new UddiGuidId("uddi:2e0b402a-7a5e-476b-8686-b33f54fd1f47", true);

        private readonly UddiId nonExistingProfileUddiId = new UddiGuidId("uddi:88fbd6d5-6a25-4c08-91cc-5344c73c1111", true);
        private readonly UddiId procurementOrdAdvBilSimProfileUddiId = new UddiGuidId("uddi:88fbd6d5-6a25-4c08-91cc-5344c73c4d69", true);
        private readonly UddiId nesublProfilesProfile5 = new UddiGuidId("uddi:AEE8B6DE-298F-4cbc-A96D-9AE8AED0AC31", true);

        private const string nonExistingRoleIdentifier = "NonExistingSellerParty";
        private const string sellerPartyRoleIdentifier = "SellerParty";

        private readonly List<EndpointAddressTypeCode> acceptHttpProtocol = new List<EndpointAddressTypeCode>() { EndpointAddressTypeCode.http };
        private readonly List<EndpointAddressTypeCode> acceptSmtpProtocol = new List<EndpointAddressTypeCode>() { EndpointAddressTypeCode.email };
        
        [TestFixtureSetUp]
        public void SetupUddi() {
            ConfigurationUtil.SetupConfiguration();
            UddiConfig config = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            uddiServerUri = new Uri(config.LookupRegistryFallbackConfig.PrioritizedRegistryList[0].Endpoints[0]);
        }

        [Test]
        public void LookingUpExistingServiceMustReturnResponseWithValidProperties() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };
            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);

            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count, "Exactly 1 endpoint expected.");

            var response = lookupResponses[0];
            AssertReponsePropertiesAreSetCorrectly(response);
        }

        [Test]
        public void LookingUpExistingServiceMustReturnCertificateSubjectString() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };
            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);
            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            
            Assert.Greater(lookupResponses.Count, 0);

            var expectedCertificateSubjectString = "CN=NemHandel test service (funktionscertifikat) + SERIALNUMBER=CVR:26769388-FID:1200406941690 + O=IT- og Telestyrelsen // CVR:26769388 + C=DK";
            var actualCertificateSubjectString = lookupResponses[0].CertificateSubjectSerialNumber.SubjectString;
            Assert.AreEqual(expectedCertificateSubjectString, actualCertificateSubjectString);
        }

        [Test]
        public void LookingUpNonExistingServiceShouldReturnEmptyResponse() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };
            
            var lookupParameters = new LookupParameters(eanIdentifier, nonExistingServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithoutProfileMustReturnEmptyResponseList() {
            List<UddiId> profileIds = new List<UddiId>() { nonExistingProfileUddiId };

            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceProvidingNonExistingRoleMustReturnEmptyResponseList() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };

            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol, nonExistingRoleIdentifier);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpServiceProvidingExistingRoleMustReturnResponse() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };

            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol, sellerPartyRoleIdentifier);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithTwoProfilesThatBothExistMustReturnResponse() {
            List<UddiId> profileIds = new List<UddiId> { procurementOrdAdvBilSimProfileUddiId, nesublProfilesProfile5 };

            var lookupParameters = new LookupParameters(eanIdentifier, invoiceServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithTwoProfilesWhereOnlyOneExistsMustReturnResponse() {
            List<UddiId> profileIds = new List<UddiId> { nonExistingProfileUddiId, nesublProfilesProfile5 };

            var lookupParameters = new LookupParameters(eanIdentifier, invoiceServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpServiceProvidingNoProfilesWhereServiceHasProfileMustReturnResponse() {
            var lookupParameters = new LookupParameters(eanIdentifier, invoiceServiceId, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithDunsIdentifierShouldReturnResponse() {
            List<UddiId> profileIds = new List<UddiId> { procurementOrdAdvBilSimProfileUddiId };

            var lookupParameters = new LookupParameters(dunsIdentifier, invoiceServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpHttpOnlyServiceWithAcceptedProtocolTypeSetToSmtpShouldReturnEmptyResponse() {
            List<UddiId> profileIds = new List<UddiId> { procurementOrdAdvBilSimProfileUddiId };

            var lookupParameters = new LookupParameters(eanIdentifier, invoiceServiceId, profileIds, acceptSmtpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        # region Helper methods

        private void AssertReponsePropertiesAreSetCorrectly(UddiLookupResponse response) {
            var expectedActivationDate = new DateTime(2008, 1, 19, 16, 0, 42);
            Assert.AreEqual(expectedActivationDate, response.ActivationDate);

            var expectedEndpoint = "http://193.163.141.141/TestEndpoint/OiosiOmniEndpointA.svc";
            Assert.AreEqual(expectedEndpoint, response.EndpointAddress.GetAsUri().AbsoluteUri);

            var expectedIdentifierActual = "5798009811578";
            Assert.AreEqual(expectedIdentifierActual, response.EndpointIdentifierActual.GetAsString());

            var expectedExpirationDate = new DateTime(2018, 01, 21, 16, 0, 42);
            Assert.AreEqual(expectedExpirationDate, response.ExpirationDate);

            Assert.AreEqual(false, response.HasNewerVersion);

            // TODO: This property is not present on any of the services in uddi and has not been tested
            Assert.AreEqual(null, response.NewerVersionReference);

            foreach (var processRoleDefinition in response.Processes) {
                Assert.AreEqual("Procurement-OrdAdv-BilSim-1.0 SellerParty", processRoleDefinition.Name);
                break;
            }

            // TODO: This property is not present on any of the services in uddi and has not been tested
            Assert.AreEqual(null, response.ServiceContactEmail);

            // TODO: This property is not present on any of the services in uddi and has not been tested
            Assert.AreEqual(null, response.TermsOfUseUrl);

            var expectedVersion = new Version(1, 0, 0);
            Assert.AreEqual(expectedVersion, response.Version);
        }

        private List<UddiLookupResponse> GetEndpointsWithProfileFromUddi(LookupParameters lookupParameters) {
            UddiLookupClient lookupClient = new UddiLookupClient(uddiServerUri);
            return lookupClient.Lookup(lookupParameters);
        }

        # endregion

    }
}
