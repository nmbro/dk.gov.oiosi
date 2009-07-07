using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile.communication;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.ldap;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.xpath;
using NUnit.Framework;

namespace dk.gov.oiosi.test.integration.communication {
    
    [TestFixture]
    public class RaspRequestTest {

        [TestFixtureSetUp]
        public void Setup() {
            ConfigurationUtil.SetupConfiguration();
        }
        [Test]
        public void OioublApplicationResponse201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_ApplicationResponse_v2p1.xml");
        }

        [Test]
        public void OioublApplicationResponse202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_ApplicationResponse_v2p2.xml");
        }

        [Test]
        public void OioublCatalogue201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Catalogue_v2p1.xml");
        }

        [Test]
        public void OioublCatalogue202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Catalogue_v2p2.xml");
        }

        [Test]
        public void OioublCatalogueDeletion201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CatalogueDeletion_v2p1.xml");
        }

        [Test]
        public void OioublCatalogueDeletion202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CatalogueDeletion_v2p2.xml");
        }

        [Test]
        public void OioublCatalogueItemSpecificationUpdate201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CatalogueItemSpecificationUpdate_v2p1.xml");
        }

        [Test]
        public void OioublCatalogueItemSpecificationUpdate202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CatalogueItemSpecificationUpdate_v2p2.xml");
        }

        [Test]
        public void OioublCataloguePricingUpdate201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CataloguePricingUpdate_v2p1.xml");
        }

        [Test]
        public void OioublCataloguePricingUpdate202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CataloguePricingUpdate_v2p2.xml");
        }

        [Test]
        public void OioublCatalogueRequest201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CatalogueRequest_v2p1.xml");
        }

        [Test]
        public void OioublCatalogueRequest202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CatalogueRequest_v2p2.xml");
        }

        [Test]
        public void OioublCreditNote201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CreditNote_v2p1.xml");
        }

        [Test]
        public void OioublCreditNote202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_CreditNote_v2p2.xml");
        }

        [Test]
        public void OioublInvoice201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Invoice_v2p1.xml");
        }

        [Test]
        public void OioublInvoice202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Invoice_v2p2.xml");
        }

        [Test]
        public void OioublOrder201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Order_v2p1.xml");
        }

        [Test]
        public void OioublOrder202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Order_v2p2.xml");
        }

        [Test]
        public void OioublOrderCancellation201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_OrderCancellation_v2p1.xml");
        }

        [Test]
        public void OioublOrderCancellation202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_OrderCancellation_v2p2.xml");
        }

        [Test]
        public void OioublOrderChange201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_OrderChange_v2p1.xml");
        }

        [Test]
        public void OioublOrderChange202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_OrderChange_v2p2.xml");
        }

        [Test]
        public void OioublOrderResponseSimple201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_OrderResponseSimple_v2p1.xml");
        }

        [Test]
        public void OioublOrderResponseSimple202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_OrderResponseSimple_v2p2.xml");
        }

        [Test]
        public void OioublOrderResponse201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_OrderResponse_v2p1.xml");
        }

        [Test]
        public void OioublOrderResponse202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_OrderResponse_v2p2.xml");
        }

        [Test]
        public void OioublReminder201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Reminder_v2p1.xml");
        }

        [Test]
        public void OioublReminder202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Reminder_v2p2.xml");
        }

        [Test]
        public void OioublStatement201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Statement_v2p1.xml");
        }

        [Test]
        public void OioublStatement202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_Statement_v2p2.xml");
        }

        [Test]
        public void OioxmlCreditNoteMustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOXML_CreditNote_v0.7.xml");
        }

        [Test]
        public void OioxmlInvoiceMustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOXML_Invoice_v0.7.xml");
        }

        # region Private methods

        private void AssertSendable(string path) {
            var oioublFile = new FileInfo(path);
            var response = SendRequestAndGetResponse(oioublFile);
            Assert.IsNotNull(response);

        }

        private Response SendRequestAndGetResponse(FileInfo file) {
            var documentId = "TEST:" + Guid.NewGuid();
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(file.FullName);
            var oiosiMessage = new OiosiMessage(xmlDocument);
            var raspRequest = GetRaspRequest(oiosiMessage);
            Response response;
            raspRequest.GetResponse(oiosiMessage, documentId, out response);
            return response;
        }

        private RaspRequest GetRaspRequest(OiosiMessage oiosiMessage) {
            var documentTypeConfigSearcher = new DocumentTypeConfigSearcher();
            var documentTypeConfig = documentTypeConfigSearcher.FindUniqueDocumentType(oiosiMessage.MessageXml);
            var messageParameters = GetMessageParameters(oiosiMessage, documentTypeConfig);
            var uddiResponse = PerformUddiLookup(messageParameters);
            var endpointAddressUri = uddiResponse.EndpointAddress.GetAsUri();

            var endpointCertificate = GetEndpointCertificateFromLdap(uddiResponse.CertificateSubjectSerialNumber);
            ValidateEndpointCertificate(endpointCertificate);
            var clientCertificate = CertificateUtil.InstallAndGetFunctionCertificateFromCertificateStore();

            var credentials = new Credentials(new OcesX509Certificate(clientCertificate), endpointCertificate);
            var request = new Request(endpointAddressUri, credentials);
            var raspRequest = new RaspRequest(request);
            return raspRequest;
        }

        private void ValidateEndpointCertificate(OcesX509Certificate endpointOcesCertificate) {
            var ocspLookupFactory = new RevocationLookupFactory();
            IRevocationLookup ocspClient = ocspLookupFactory.CreateRevocationLookupClient();
            
            RevocationCheckStatus ocspStatus = endpointOcesCertificate.CheckRevocationStatus(ocspClient);
            if (ocspStatus == RevocationCheckStatus.AllChecksPassed) return;

            throw new Exception("Certificate validation error");

        }

        private OcesX509Certificate GetEndpointCertificateFromLdap(CertificateSubject certificateSubject) {
            var ldapClientFactory = new LdapLookupFactory();
            var ldapClient = ldapClientFactory.CreateLdapLookupClient();
            X509Certificate2 endpointCertificate = ldapClient.GetCertificate(certificateSubject);
            var endpointOcesCertificate = new OcesX509Certificate(endpointCertificate);
            return endpointOcesCertificate;
        }

        private LookupParameters GetMessageParameters(OiosiMessage message, DocumentTypeConfig docTypeConfig) {
            EndpointKeyTypeCode endpointKeyTypeCode = Utilities.GetEndpointKeyTypeCode(message, docTypeConfig);

            Identifier endpointKey = Utilities.GetEndpointKeyByXpath(
                message.MessageXml,
                docTypeConfig.EndpointType.Key.XPath,
                docTypeConfig.Namespaces,
                endpointKeyTypeCode
                );

            UddiId profileTModelId = GetProfileTModelId(message, docTypeConfig);

            // 2. Build MessageParameters class:
            UddiId serviceContractTModel;
            serviceContractTModel = IdentifierUtility.GetUddiIDFromString(docTypeConfig.ServiceContractTModel);

            LookupParameters uddiLookupParameters;
            if (profileTModelId == null) {
                uddiLookupParameters = new LookupParameters(
                    endpointKey,
                    serviceContractTModel,
                    new List<EndpointAddressTypeCode>() {EndpointAddressTypeCode.http});
            } else {
                uddiLookupParameters = new LookupParameters(
                    endpointKey,
                    serviceContractTModel,
                    new List<UddiId>() { profileTModelId },
                    new List<EndpointAddressTypeCode>() { EndpointAddressTypeCode.http });
            }

            return uddiLookupParameters;
        }

        private UddiId GetProfileTModelId(OiosiMessage message, DocumentTypeConfig docTypeConfig) {
            // If doctype does't have a XPath expression to extract the document Profile 
            // then we assume that the current document type does operate with OIOUBL profiles
            if (docTypeConfig.ProfileIdXPath == null) return null;
            if (docTypeConfig.ProfileIdXPath.XPath == null) return null;
            if (docTypeConfig.ProfileIdXPath.XPath.Equals("")) return null;

            // Fetch the OIOUBL profile name
            string profileName = DocumentXPathResolver.GetElementValueByXpath(
                    message.MessageXml,
                    docTypeConfig.ProfileIdXPath.XPath,
                    docTypeConfig.Namespaces);

            var config = ConfigurationHandler.GetConfigurationSection<ProfileMappingCollectionConfig>();
            if (config.ContainsProfileMappingByName(profileName)) {
                ProfileMapping profileMapping = config.GetMapping(profileName);
                string profileTModelGuid = profileMapping.TModelGuid;
                return IdentifierUtility.GetUddiIDFromString(profileTModelGuid);
            }
            throw new Exception("GetProfileTModelId failed for : " + profileName);
        }

        /// <summary>
        /// Performs a lookup in the UDDI and validates the response.
        /// </summary>
        /// <returns>Returns the UDDI response</returns>
        private UddiLookupResponse PerformUddiLookup(LookupParameters uddiLookupParameters) {
            var uddiClientFactory = new RegistryLookupClientFactory();
            var uddiClient = uddiClientFactory.CreateUddiLookupClient();
            var uddiResponses = uddiClient.Lookup(uddiLookupParameters);
            Assert.AreEqual(1, uddiResponses.Count, "Unexcpected number of uddi results.");
            var selectedUddiResponse = uddiResponses[0];
            return selectedUddiResponse;
        }

        # endregion

    }
}