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
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.test.integration.communication
{
    public abstract class AbstractIntegrationRaspRequestTest    
    {

        private X509Certificate2 clientCertificate = null;

        /// <summary>
        /// Get or set the client certificate
        /// </summary>
        public X509Certificate2 ClientCertificate
        {
            get
            {
                return this.clientCertificate;
            }

            set
            {
                this.clientCertificate = value;
            }
        }


        protected void AssertSendable(string path)
        {
            FileInfo oioublFile = new FileInfo(path);
            
            Response response = null;
            bool disableTest = false;
            if (disableTest)
            {
                // disable the test
                Assert.IsNull(response);
            }
            else
            {
                // perform the test
                response = SendRequestAndGetResponse(oioublFile);
                Assert.IsNotNull(response);
            }
        }

        protected Response SendRequestAndGetResponse(FileInfo file)
        {
            string documentId = "TEST:" + Guid.NewGuid();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(file.FullName);
            OiosiMessage oiosiMessage = new OiosiMessage(xmlDocument);
            RaspRequest raspRequest = this.GetRaspRequest(oiosiMessage);
            Response response;
            raspRequest.GetResponse(oiosiMessage, documentId, out response);
            return response;
        }

        protected RaspRequest GetRaspRequest(OiosiMessage oiosiMessage)
        {
            DocumentTypeConfigSearcher documentTypeConfigSearcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig documentTypeConfig = documentTypeConfigSearcher.FindUniqueDocumentType(oiosiMessage.MessageXml);
            LookupParameters messageParameters = this.GetMessageParameters(oiosiMessage, documentTypeConfig);
            UddiLookupResponse uddiResponse = this.PerformUddiLookup(messageParameters);
            Uri endpointAddressUri = uddiResponse.EndpointAddress.GetAsUri();

            OcesX509Certificate endpointCertificate = this.GetEndpointCertificateFromLdap(uddiResponse.CertificateSubjectSerialNumber);
            this.ValidateEndpointCertificate(endpointCertificate);
            //X509Certificate2 clientCertificate = CertificateUtil.InstallAndGetFunctionCertificateFromCertificateStore();
            X509Certificate2 clientCertificate = this.ClientCertificate;

            Credentials credentials = new Credentials(new OcesX509Certificate(clientCertificate), endpointCertificate);
            Request request = new Request(endpointAddressUri, credentials);
            RaspRequest raspRequest = new RaspRequest(request);
            return raspRequest;
        }

        protected void ValidateEndpointCertificate(OcesX509Certificate endpointOcesCertificate)
        {
            RevocationLookupFactory ocspLookupFactory = new RevocationLookupFactory();
            IRevocationLookup ocspClient = ocspLookupFactory.CreateRevocationLookupClient();

            RevocationResponse ocspStatus = endpointOcesCertificate.CheckRevocationStatus(ocspClient);

            switch (ocspStatus.RevocationCheckStatus)
            {
                case RevocationCheckStatus.AllChecksPassed:
                    {
                        // all okay
                        break;
                    }
                case RevocationCheckStatus.CertificateRevoked:
                    {
                        throw new Exception("Certificate validation error - CertificateRevoked");
                        break;
                    }
                case RevocationCheckStatus.NotChecked:
                    {
                        throw new Exception("Certificate validation error - NotChecked");
                        break;
                    }
                case RevocationCheckStatus.UnknownIssue:
                    {
                        throw new Exception("Certificate validation error - UnknownIssue");
                        break;
                    }
                default:
                    {
                        throw new Exception("Certificate validation error");
                        break;
                    }
            }
        }

        protected OcesX509Certificate GetEndpointCertificateFromLdap(CertificateSubject certificateSubject)
        {
            LdapLookupFactory ldapClientFactory = new LdapLookupFactory();
            ICertificateLookup ldapClient = ldapClientFactory.CreateLdapLookupClient();
            X509Certificate2 endpointCertificate = ldapClient.GetCertificate(certificateSubject);
            OcesX509Certificate endpointOcesCertificate = new OcesX509Certificate(endpointCertificate);
            return endpointOcesCertificate;
        }

        protected LookupParameters GetMessageParameters(OiosiMessage message, DocumentTypeConfig docTypeConfig)
        {
            string endpointKeyTypeCode = Utilities.GetEndpointKeyTypeCode(message, docTypeConfig);

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
            if (profileTModelId == null)
            {
                uddiLookupParameters = new LookupParameters(
                    endpointKey,
                    serviceContractTModel,
                    new List<EndpointAddressTypeCode>() { EndpointAddressTypeCode.http });
            }
            else
            {
                uddiLookupParameters = new LookupParameters(
                    endpointKey,
                    serviceContractTModel,
                    new List<UddiId>() { profileTModelId },
                    new List<EndpointAddressTypeCode>() { EndpointAddressTypeCode.http });
            }

            return uddiLookupParameters;
        }

        protected UddiId GetProfileTModelId(OiosiMessage message, DocumentTypeConfig docTypeConfig)
        {
            UddiId uddiId;
            // If doctype does't have a XPath expression to extract the document Profile 
            // then we assume that the current document type does operate with OIOUBL profiles
            if (docTypeConfig.ProfileIdXPath == null)
            {
                uddiId = null;
            }
            else if (docTypeConfig.ProfileIdXPath.XPath == null)
            {
                uddiId = null;
            }
            else if (docTypeConfig.ProfileIdXPath.XPath.Equals(""))
            {
                uddiId = null;
            }
            else
            {

                // Fetch the OIOUBL profile name
                string profileName = DocumentXPathResolver.GetElementValueByXpath(
                        message.MessageXml,
                        docTypeConfig.ProfileIdXPath.XPath,
                        docTypeConfig.Namespaces);

                ProfileMappingCollectionConfig config = ConfigurationHandler.GetConfigurationSection<ProfileMappingCollectionConfig>();
                if (config.ContainsProfileMappingByName(profileName))
                {
                    ProfileMapping profileMapping = config.GetMapping(profileName);
                    string profileTModelGuid = profileMapping.TModelGuid;
                    uddiId = IdentifierUtility.GetUddiIDFromString(profileTModelGuid);
                }
                else
                {
                    throw new Exception("GetProfileTModelId failed for : " + profileName);
                }
            }

            return uddiId;
        }

        /// <summary>
        /// Performs a lookup in the UDDI and validates the response.
        /// </summary>
        /// <returns>Returns the UDDI response</returns>
        protected UddiLookupResponse PerformUddiLookup(LookupParameters uddiLookupParameters)
        {
            RegistryLookupClientFactory uddiClientFactory = new RegistryLookupClientFactory();
            IUddiLookupClient uddiClient = uddiClientFactory.CreateUddiLookupClient();
            List<UddiLookupResponse> uddiResponses = uddiClient.Lookup(uddiLookupParameters);
            Assert.AreEqual(1, uddiResponses.Count, "Unexcpected number of uddi results.");
            UddiLookupResponse selectedUddiResponse = uddiResponses[0];
            return selectedUddiResponse;
        }
    }
}