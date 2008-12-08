using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using NUnit.Framework;

using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.raspProfile;

namespace dk.gov.oiosi.test.nunit.library.xml.xpath {
    [TestFixture]
    public class XPathTest {
        private DocumentTypeConfigSearcher _searcher;

        [TestFixtureSetUp]
        public void Start() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            _searcher = new DocumentTypeConfigSearcher();
        }

        [Test]
        public void ApplicationResponseTest() {
            IIdentifier identifier = Test(TestConstants.PATH_APPLICATIONRESPONSE_XML);
            Assert.AreEqual("DK16356706", identifier.GetAsString());
        }

        [Test]
        public void CreditNoteTest() {
            IIdentifier identifier = Test(TestConstants.PATH_CREDITNOTE_XML);
            Assert.AreEqual("5798000416604", identifier.GetAsString());
        }

        [Test]
        public void InvoiceTest() {
            IIdentifier identifier = Test(TestConstants.PATH_INVOICE_XML);
            Assert.AreEqual("5798000416604", identifier.GetAsString());
        }

        [Test]
        public void OrderTest() {
            IIdentifier identifier = Test(TestConstants.PATH_ORDER_XML);
            Assert.AreEqual("DK16356706", identifier.GetAsString());
        }

        [Test]
        public void OrderResponseSimpleTest() {
            IIdentifier identifier = Test(TestConstants.PATH_ORDERRESPONSESIMPLE_XML);
            Assert.AreEqual("5798000416604", identifier.GetAsString());
        }

        [Test]
        public void ReminderTest() {
            IIdentifier identifier = Test(TestConstants.PATH_REMINDER_XML);
            Assert.AreEqual("5798009811585", identifier.GetAsString());
        }

        private IIdentifier Test(string path) {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            DocumentTypeConfig config = _searcher.FindUniqueDocumentType(document);
            string keyXpath = config.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = config.Namespaces;

            EndpointKeyTypeCode code = EndpointKeyTypeCode.ean;
            IIdentifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            return identifier;
        }
    }
}
