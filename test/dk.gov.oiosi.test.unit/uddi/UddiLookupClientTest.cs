using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using NUnit.Framework;
using uddiorg.api_v3;
using System.ServiceModel;
using dk.gov.oiosi.test.unit;

namespace dk.gov.oiosi.test.integration.uddi {

    [TestFixture]
    public class UddiLookupClientTest {

        private Uri uddiServerUri;
        private readonly Identifier eanIdentifier = new Identifier(TestConstants.EAN, TestConstants.TESTEAN);
        private readonly Identifier dunsIdentifier = new Identifier("duns", "1234567890");

        private readonly UddiId nonExistingServiceId = new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26fa1111", true);
        private readonly UddiId orderServiceId = new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a", true);
        private readonly UddiId invoiceServiceId = new UddiGuidId("uddi:2e0b402a-7a5e-476b-8686-b33f54fd1f47", true);

        private readonly UddiId oioxmlInvoiceServiceId =        new UddiGuidId("uddi:bc99bb01-80f9-4f52-89dc-edf7732c56f9", true);
        private readonly UddiId oioxmlInvoiceLaesIndServiceId = new UddiGuidId("uddi:1867d8b0-a893-11dc-a813-bfc65441a808", true);
        private readonly UddiId oioxmlCreditNoteServiceId =     new UddiGuidId("uddi:3bbc9cf0-3c4c-11dc-98be-6976502198bd", true);

        private readonly UddiId nonExistingProfileProfileId = new UddiGuidId("uddi:88fbd6d5-6a25-4c08-91cc-5344c73c1111", true);
        private readonly UddiId procurementOrdAdvBilSimProfileProfileId = new UddiGuidId("uddi:88fbd6d5-6a25-4c08-91cc-5344c73c4d69", true);
        private readonly UddiId nesublProfilesProfile5ProfileId = new UddiGuidId("uddi:AEE8B6DE-298F-4cbc-A96D-9AE8AED0AC31", true);
        private readonly UddiId oioxmlElektroniskHandelProfileId = new UddiGuidId("uddi:c001daa0-8ba3-11dd-894e-770465b08940", true);
        private readonly UddiId oioxmlElektroniskHandelLaesIndProfileId = new UddiGuidId("uddi:cac79330-8ba3-11dd-894e-770465b08940", true);

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
            Identifier eanIdentifier2 = new Identifier(TestConstants.EAN, "5798009811578");
            UddiId orderServiceId2 = new UddiGuidId("uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a", true);

            var lookupParameters2 = new LookupParameters(eanIdentifier2, orderServiceId2, profileIds2, acceptHttpProtocol);

            List<UddiLookupResponse> lookupResponses2 = GetEndpointsWithProfileFromUddi(lookupParameters2);
        }

        [Test]
        public void LookingUpExistingServiceMustReturnResponseWithValidProperties() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileProfileId };
            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);

            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count, "Exactly 1 endpoint expected.");

            var response = lookupResponses[0];
            AssertReponsePropertiesAreSetCorrectlyOrdAdvBilSim(response);
        }

        [Test]
        public void LookingUpExistingServiceMustReturnCertificateSubjectString() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileProfileId };
            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);
            List<UddiLookupResponse> lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            
            Assert.Greater(lookupResponses.Count, 0);

            var expectedCertificateSubjectString = "OID.2.5.4.5=CVR:34051178-FID:55310689 + CN=Digst Demo Endpoint Foces2 (funktionscertifikat), O=Digitaliseringsstyrelsen // CVR:34051178, C=DK";            
            var actualCertificateSubjectString = lookupResponses[0].CertificateSubjectSerialNumber.SubjectString;
            Assert.AreEqual(expectedCertificateSubjectString, actualCertificateSubjectString);
        }

        [Test]
        public void LookingUpNonExistingServiceShouldReturnZeroResponse()
        {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileProfileId };
            
            var lookupParameters = new LookupParameters(eanIdentifier, nonExistingServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithoutProfileMustReturnEmptyResponseList() {
            List<UddiId> profileIds = new List<UddiId>() { nonExistingProfileProfileId };

            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceProvidingNonExistingRoleMustReturnEmptyResult() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileProfileId };

            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol, nonExistingRoleIdentifier);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            
        }

        [Test]
        public void LookingUpServiceProvidingExistingRoleMustReturnResponse() {
            List<UddiId> profileIds = new List<UddiId>() { procurementOrdAdvBilSimProfileProfileId };

            var lookupParameters = new LookupParameters(eanIdentifier, orderServiceId, profileIds, acceptHttpProtocol, sellerPartyRoleIdentifier);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithTwoProfilesThatBothExistMustReturnResponse() {
            List<UddiId> profileIds = new List<UddiId> { procurementOrdAdvBilSimProfileProfileId, nesublProfilesProfile5ProfileId };

            var lookupParameters = new LookupParameters(eanIdentifier, invoiceServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpExistingServiceWithTwoProfilesWhereOnlyOneExistsMustReturnResponse() {
            List<UddiId> profileIds = new List<UddiId> { nonExistingProfileProfileId, nesublProfilesProfile5ProfileId };

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

//        [Test] Temp. made comment, because there exists no DUNS data i NHR at the moment.
        public void LookingUpExistingServiceWithDunsIdentifierShouldReturnResponse() {
            List<UddiId> profileIds = new List<UddiId> { nesublProfilesProfile5ProfileId };

            var lookupParameters = new LookupParameters(dunsIdentifier, invoiceServiceId, profileIds, acceptHttpProtocol);
            var lookupResponses = GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count);
        }

        [Test]
        public void LookingUpHttpOnlyServiceWithAcceptedProtocolTypeSetToSmtpShouldReturnEmptyResponse() {
            List<UddiId> profileIds = new List<UddiId> { procurementOrdAdvBilSimProfileProfileId };

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

        [Test]
        public void LookOioXmlWithProfile()
        {
            List<UddiId> profileIds = new List<UddiId>() { this.oioxmlElektroniskHandelProfileId };
            LookupParameters lookupParameters = new LookupParameters(eanIdentifier, this.oioxmlInvoiceServiceId, profileIds, acceptHttpProtocol);

            List<UddiLookupResponse> lookupResponses = this.GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count, "Exactly 1 endpoint expected.");

            UddiLookupResponse response = lookupResponses[0];
            AssertReponsePropertiesAreSetCorrectly(response, "OIOXML elektronisk handel BuyerParty");
        }

        [Test]
        public void LookOioXmlLaesIndWithProfile()
        {
            List<UddiId> profileIds = new List<UddiId>() { this.oioxmlElektroniskHandelLaesIndProfileId };
            LookupParameters lookupParameters = new LookupParameters(eanIdentifier, this.oioxmlInvoiceServiceId, profileIds, acceptHttpProtocol);

            List<UddiLookupResponse> lookupResponses = this.GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(0, lookupResponses.Count, "No 'Læs Ind' should have been registrated.");
        }

        [Test]
        public void LookOioXmlWithoutProfile()
        {            
            LookupParameters lookupParameters = new LookupParameters(eanIdentifier, this.oioxmlInvoiceServiceId, acceptHttpProtocol);

            List<UddiLookupResponse> lookupResponses = this.GetEndpointsWithProfileFromUddi(lookupParameters);
            Assert.AreEqual(1, lookupResponses.Count, "Exactly 1 endpoint expected.");

            UddiLookupResponse response = lookupResponses[0];
            this.AssertReponsePropertiesAreSetCorrectly(response, "OIOXML elektronisk handel BuyerParty");
            this.AssertReponsePropertiesAreSetCorrectlyProfile(response, "IOXML elektronisk handel - læs ind BuyerParty", false);
        }

        # region Helper methods
               

        private void AssertReponsePropertiesAreSetCorrectlyOrdAdvBilSim(UddiLookupResponse response) 
        {
            this.AssertReponsePropertiesAreSetCorrectly(response, "Procurement-OrdAdv-BilSim-1.0 SellerParty");
        }

        private void AssertReponsePropertiesAreSetCorrectly(UddiLookupResponse response, string profile)
        {
            this.AssertReponsePropertiesAreSetCorrectlyDate(response);
            this.AssertReponsePropertiesAreSetCorrectlyEndpoint(response);
            this.AssertReponsePropertiesAreSetCorrectlyVersion(response);
            this.AssertReponsePropertiesAreSetCorrectlyInfo(response);
            this.AssertReponsePropertiesAreSetCorrectlyProfile(response, profile, true);
        }

        private void AssertReponsePropertiesAreSetCorrectlyDate(UddiLookupResponse response)
        {
            DateTime expectedActivationDate = new DateTime(2000, 1, 1, 0, 59, 0);
            Assert.AreEqual(expectedActivationDate, response.ActivationDate);

            DateTime expectedExpirationDate = new DateTime(2039, 12, 31, 0, 59, 0);
            Assert.AreEqual(expectedExpirationDate, response.ExpirationDate);
        }

        private void AssertReponsePropertiesAreSetCorrectlyEndpoint(UddiLookupResponse response)
        {
            string expectedEndpoint = "http://demo.nemhandel.dk/RaspNet/2.1.0/TestService.svc";
            Assert.AreEqual(expectedEndpoint, response.EndpointAddress.GetAsUri().AbsoluteUri);

            string expectedIdentifierActual = TestConstants.TESTEAN;
            Assert.AreEqual(expectedIdentifierActual, response.EndpointIdentifierActual.GetAsString());
        }

        private void AssertReponsePropertiesAreSetCorrectlyVersion(UddiLookupResponse response)
        {
            Assert.AreEqual(false, response.HasNewerVersion);
            Assert.AreEqual(null, response.NewerVersionReference);

            Version expectedVersion = new Version(1, 0, 0);
            Assert.AreEqual(expectedVersion, response.Version);
        }

        private void AssertReponsePropertiesAreSetCorrectlyInfo(UddiLookupResponse response)
        {
            Assert.IsNotNull(response.ServiceContactEmail);
            Assert.AreEqual(null, response.TermsOfUseUrl);
        }

        private void AssertReponsePropertiesAreSetCorrectlyProfile(UddiLookupResponse response, string profile, bool included)
        {
            var processRoles = new HashSet<string>();

            foreach (ProcessRoleDefinition processRoleDefinition in response.ProcessRoles)
            {
                processRoles.Add(processRoleDefinition.Name);
            }

            if (included)
            {
                Assert.IsTrue(processRoles.Contains(profile));
            }
            else
            {
                Assert.IsFalse(processRoles.Contains(profile));
            }
        }

        private List<UddiLookupResponse> GetEndpointsWithProfileFromUddi(LookupParameters lookupParameters) {
            UddiLookupClient lookupClient = new UddiLookupClient(uddiServerUri);
            return lookupClient.Lookup(lookupParameters);
        }

        # endregion

    }
}
