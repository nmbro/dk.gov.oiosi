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
        public void OioxmlInvoiceMustBeSendableByRaspRequest() {
            var oioxmlInvoiceFile = new FileInfo("Resources/Documents/Examples/OIOXML_Invoice_v0.7.xml");
            var response = SendRequestAndGetResponse(oioxmlInvoiceFile);
            Assert.IsNotNull(response);
        }

        [Test]
        public void OioublInvoiceMustBeSendableByRaspRequest() {
            var oioublInvoiceFile = new FileInfo("Resources/Documents/Examples/OIOUBL_Invoice_v2p1.xml");
            var response = SendRequestAndGetResponse(oioublInvoiceFile);
            Assert.IsNotNull(response);
        }

        [Test]
        public void OioublApplicationResponse202MustBeSendableByRaspRequest() {
            var oioublFile = new FileInfo("Resources/Documents/Examples/OIOUBL_ApplicationResponse_v2p2.xml");
            var response = SendRequestAndGetResponse(oioublFile);
            Assert.IsNotNull(response);
        }

        [Test]
        public void OioublCatalogue202MustBeSendableByRaspRequest() {
            var oioublFile = new FileInfo("Resources/Documents/Examples/OIOUBL_Catalogue_v2p2.xml");
            var response = SendRequestAndGetResponse(oioublFile);
            Assert.IsNotNull(response);
        }

        [Test]
        public void AllExampleDocumentsMustBeSendableByRaspRequest() {
            bool errorsFound = false;
            var documentsToSendDirectory = new DirectoryInfo("Resources/Documents/Examples");
            foreach (var file in documentsToSendDirectory.GetFiles()) {
                Response response;
                try {
                    response = SendRequestAndGetResponse(file);
                    if (response != null) Console.WriteLine("Success: " + file.Name);
                    if (response == null) {
                        errorsFound = true;
                        Console.WriteLine("Failure: " + file.Name);
                    }
                }
                catch {
                    errorsFound = true;
                    Console.WriteLine("Failure: " + file.Name);
                }
            }
            Assert.IsFalse(errorsFound, "One or more documents had failures.");
        }
        
        # region Private methods

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
            throw new Exception("GetProfileTModelId");
        }

        /// <summary>
        /// Performs a lookup in the UDDI and validates the response.
        /// </summary>
        /// <returns>Returns the UDDI response</returns>
        private UddiLookupResponse PerformUddiLookup(LookupParameters uddiLookupParameters) {
            var uddiClientFactory = new RegistryLookupClientFactory();
            var uddiClient = uddiClientFactory.CreateUddiLookupClient();
            var uddiResponses = uddiClient.Lookup(uddiLookupParameters);
            if (uddiResponses.Count != 1)
            {
                throw new Exception("An uddi entry could not be found. Identifier: " + uddiLookupParameters.Identifier);
            }
            var selectedUddiResponse = uddiResponses[0];
            return selectedUddiResponse;
        }

        # endregion

    }
}