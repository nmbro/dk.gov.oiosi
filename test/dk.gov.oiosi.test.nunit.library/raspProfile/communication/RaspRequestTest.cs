using System.IO;
using System.Xml;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.raspProfile.communication;
using NUnit.Framework;
using System;
using System.Reflection;
using dk.gov.oiosi.security.oces;
using System.Security.Cryptography.X509Certificates;

namespace dk.gov.oiosi.test.nunit.library.raspProfile.communication {
    
    [TestFixture]
    public class RaspRequestTest {
        
        [Test]
        public void DocumentIdMustBeAddedAsCustomHeader() {
            var documentId = "678";
            OiosiMessage oiosiMessage = GetInvoiceOiosiMessage();

            // Call private method
            Type raspRequestType = typeof(RaspRequest);
            MethodInfo addCustomHeadersMethod = raspRequestType.GetMethod("AddCustomHeaders", BindingFlags.NonPublic | BindingFlags.Instance);
            RaspRequest raspRequest = new RaspRequest(new Request(new Uri("http://test.dk"), new Credentials(new OcesX509Certificate(new X509Certificate2(TestConstants.PATH_CERTIFICATE_EMPLOYEE)), new OcesX509Certificate(new X509Certificate2(TestConstants.PATH_CERTIFICATE_EMPLOYEE)))));
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