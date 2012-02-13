using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using NUnit.Framework;
using uddiorg.api_v3;
using System.ServiceModel;

namespace dk.gov.oiosi.test.integration.uddi {

    [TestFixture]
    public class UddiLookupClientTest {

        private Uri uddiServerUri;
        private readonly Identifier eanIdentifier = new IdentifierEan(TestConstants.TESTEAN);
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
        public void SetupUddi() 
        {
            ConfigurationUtil.SetupConfiguration();
            UddiConfig config = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            uddiServerUri = new Uri(config.LookupRegistryFallbackConfig.PrioritizedRegistryList[0].Endpoints[0]);
        }


        [Test]
        public void CacheTest() 
        {
            List<UddiId> profileIds = new List<UddiId>() { new UddiGuidId("uddi:88fbd6d5-6a25-4c08-91cc-5344c73c4d69", true) };
            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);

            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);

            List<UddiId> profileIds2 = new List<UddiId>() { new UddiGuidId("uddi:88fbd6d5-6a25-4c08-91cc-5344c73c4d69", true) };
            Identifier eanIdentifier2 = new IdentifierEan("5798009811578");
            UddiId orderServiceId2 = new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a", true);

            var lookupParameters2 = new LookupParameters(eanIdentifier2, orderServiceId2, profileIds2, acceptHttpProtocol);

            List<UddiLookupResponse> lookupResponses2 = GetEndpointsWithProfileFromUddi(lookupParameters2);
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

            var expectedCertificateSubjectString = "OID.2.5.4.5=CVR:26769388-FID:1272375084431 + CN=Test NemHandelservice (funktionscertifikat), O=IT- og Telestyrelsen // CVR:26769388, C=DK";
            var actualCertificateSubjectString = lookupResponses[0].CertificateSubjectSerialNumber.SubjectString;
            Assert.AreEqual(expectedCertificateSubjectString, actualCertificateSubjectString);
        }

        [Test, ExpectedException(typeof(FaultException<DispositionReport>))]
        public void LookingUpNonExistingServiceShouldReturnFault() {
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
        public void LookingUpExistingServiceProvidingNonExistingRoleMustReturnEmptyResult() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileUddiId };

            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol, nonExistingRoleIdentifier);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            
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
            List<UddiId> profileIds = new List<UddiId> { nesublProfilesProfile5 };

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

        [Test]
        public void LookingUpAllRegistrationsOnEanNumber() {
            var lookupParameters = new LookupParameters(eanIdentifier, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.Greater(lookupResponses.Count, 0);
        }

        [Test]
        public void GetProcessDefinitions() {
            List<UddiId> uddiIds = new List<UddiId>();
            uddiIds.Add(new UddiStringId("uddi:AEE8B6DE-298F-4cbc-A96D-9AE8AED0AC31", true));
            uddiIds.Add(new UddiStringId("uddi:c001daa0-8ba3-11dd-894e-770465b08940", true));
            UddiLookupClient lookupClient = new UddiLookupClient(uddiServerUri);

            List<ProcessDefinition> processes = lookupClient.GetProcessDefinitions(uddiIds);
            Assert.AreEqual(uddiIds.Count, processes.Count);
        }

        # region Helper methods

        private void AssertReponsePropertiesAreSetCorrectly(UddiLookupResponse response) {
            var expectedActivationDate = new DateTime(2010, 9, 9, 0, 0, 0);
            Assert.AreEqual(expectedActivationDate, response.ActivationDate);

            var expectedEndpoint = "http://testservice.nemhandel.gov.dk/integration/RASPNET/1.2.3/receiver.svc";
            Assert.AreEqual(expectedEndpoint, response.EndpointAddress.GetAsUri().AbsoluteUri);

            var expectedIdentifierActual = TestConstants.TESTEAN;
            Assert.AreEqual(expectedIdentifierActual, response.EndpointIdentifierActual.GetAsString());

            var expectedExpirationDate = new DateTime(2030, 9, 9, 0, 0, 0);
            Assert.AreEqual(expectedExpirationDate, response.ExpirationDate);

            Assert.AreEqual(false, response.HasNewerVersion);
            Assert.AreEqual(null, response.NewerVersionReference);

            var processRoles = new HashSet<string>();

            foreach (var processRoleDefinition in response.ProcessRoles) {
                processRoles.Add(processRoleDefinition.Name);
            }
            Assert.IsTrue(processRoles.Contains("Procurement-OrdAdv-BilSim-1.0 SellerParty"));

            Assert.IsNotNull(response.ServiceContactEmail);
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
