using System.IO;
using System.Xml;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.raspProfile.communication;
using NUnit.Framework;
using System;
using System.Reflection;
using dk.gov.oiosi.security.oces;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.raspProfile;

namespace dk.gov.oiosi.test.unit.raspProfile.communication {
    
    [TestFixture]
    public class RaspRequestTest {

        [SetUp]
        public void Setup() {
            var configFile = Settings.CreateRandomPath("RaspConfig.xml");
            Directory.CreateDirectory(configFile.Directory.FullName);

            ConfigurationHandler.ConfigFilePath = configFile.FullName;
            ConfigurationHandler.Reset();

            ConfigurationHandler.RegisterConfigurationSection<DocumentTypeCollectionConfig>();
            ConfigurationHandler.RegisterConfigurationSection<OcesX509CertificateConfig>();
            ConfigurationHandler.PreloadRegisteredConfigurationSections();

            DocumentTypeCollectionConfig documentTypeCollectionConfig = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            documentTypeCollectionConfig.AddDocumentType(new DefaultDocumentTypes().GetOioUblInvoice());
            new DefaultOcesCertificate().SetIfNotExistsOcesCertificateConfig();

            ConfigurationHandler.SaveToFile();
        }

        [Test]
        public void DocumentIdMustBeAddedAsCustomHeader() {
            var documentId = "678";
            OiosiMessage oiosiMessage = GetInvoiceOiosiMessage();

            // Call private method
            Type raspRequestType = typeof(RaspRequest);
            MethodInfo addCustomHeadersMethod = raspRequestType.GetMethod("AddCustomHeaders", BindingFlags.NonPublic | BindingFlags.Instance);
            OcesX509Certificate client = new OcesX509Certificate(new X509Certificate2(TestConstants.PATH_CERTIFICATE_EMPLOYEE, TestConstants.PASSWORD_CERTIFICATE_EMPLOYEE));
            OcesX509Certificate server = new OcesX509Certificate(new X509Certificate2(TestConstants.PATH_CERTIFICATE_EMPLOYEE, TestConstants.PASSWORD_CERTIFICATE_EMPLOYEE));
            Credentials c1 = new Credentials(client, server);
            RaspRequest raspRequest = new RaspRequest(new Request(new Uri("http://test.dk"), c1));
            addCustomHeadersMethod.Invoke(raspRequest, new object[] { oiosiMessage, documentId });

            bool headerValueAdded = false;
            foreach (var messageHeader in oiosiMessage.MessageHeaders) {
                var headerValue = messageHeader.Value.ToString();
                if (headerValue.Contains(documentId)) headerValueAdded = true;
            }
            Assert.IsTrue(headerValueAdded, "DocumentId not found in header.");
        }

        private OiosiMessage GetInvoiceOiosiMessage() {
            var invoiceSourcePath = "Resources/Documents/Examples/OIOUBL_Invoice_identifier_ean_v2p1.xml";
            var invoiceFile = Settings.CreateRandomPath("invoice.xml");
            Directory.CreateDirectory(invoiceFile.DirectoryName);
            File.Copy(invoiceSourcePath, invoiceFile.FullName);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(invoiceFile.FullName);

            return new OiosiMessage(xmlDocument);
        }
    }
}