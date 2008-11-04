using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using NUnit.Framework;

using dk.gov.oiosi;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.raspProfile;

namespace dk.gov.oiosi.test.nunit.library.xml.documentType {

    [TestFixture]
    public class DocumentTypeConfigSearcherTest {
        private DocumentTypeConfigSearcher _searcher;

        public DocumentTypeConfigSearcherTest() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            _searcher = new DocumentTypeConfigSearcher();
        }

        [Test]
        public void SearchForApplicationResponseTest() {
            Print.Started("SearchForApplicationResponseTest");
            string path = TestConstants.PATH_APPLICATIONRESPONSE_XML;
            DocumentTypeConfig documentType = SearchForDocument(path);
            Assert.AreEqual("Applikationsmeddelse", documentType.FriendlyName);
            Print.Completed("SearchForApplicationResponseTest");
        }

        [Test]
        public void SearchForCreditNoteTest() {
            Print.Started("SearchForCreditNoteTest");
            string path = TestConstants.PATH_CREDITNOTE_XML;
            DocumentTypeConfig documentType = SearchForDocument(path);
            Assert.AreEqual("Kreditnota", documentType.FriendlyName);
            Print.Completed("SearchForCreditNoteTest");
        }

        [Test]
        public void SearchForInvoiceTest() {
            Print.Started("SearchForInvoiceTest");
            string path = TestConstants.PATH_INVOICE_XML;
            DocumentTypeConfig documentType = SearchForDocument(path);
            Assert.AreEqual("Faktura", documentType.FriendlyName);
            Print.Completed("SearchForInvoiceTest");
        }

        public void SearchForOrderTest() {
            Print.Started("SearchForOrderTest");
            string path = TestConstants.PATH_ORDER_XML;
            DocumentTypeConfig documentType = SearchForDocument(path);
            Assert.AreEqual("Order", documentType.FriendlyName);
            Print.Completed("SearchForOrderTest");
        }

        [Test]
        public void SearchForOrderResponseSimpleTest() {
            Print.Started("SearchForOrderResponseSimpleTest");
            string path = TestConstants.PATH_ORDERRESPONSESIMPLE_XML;
            DocumentTypeConfig documentType = SearchForDocument(path);
            Assert.AreEqual("Simpel ordrebekræftelse", documentType.FriendlyName);
            Print.Completed("SearchForOrderResponseSimpleTest");
        }

        [Test]
        public void SearchForReminderTest() {
            Print.Started("SearchForReminderTest");
            string path = TestConstants.PATH_REMINDER_XML;
            DocumentTypeConfig documentType = SearchForDocument(path);
            Assert.AreEqual("Rykker", documentType.FriendlyName);
            Print.Completed("SearchForReminderTest");
        }

        [Test]
        public void SearchForInvoice07Test() {
            Print.Started("SearchForInvoice07Test");
            string path = TestConstants.PATH_INVOICE07_XML;
            DocumentTypeConfig documentType = SearchForDocument(path);
            Assert.AreEqual("Faktura v0.7", documentType.FriendlyName);
            Print.Completed("SearchForInvoice07Test");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.xml.documentType.NoDocumentTypeFoundFromXmlDocumentException))]
        public void SearchForUnkownTypeTest() {
            string path = TestConstants.PATH_UNKNOWNTYPE_XML;
            DocumentTypeConfig documentType = SearchForDocument(path);
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.exception.NullArgumentException))]
        public void NullArguementTest() {
            _searcher.FindUniqueDocumentType(null);
        }

        private DocumentTypeConfig SearchForDocument(string path) {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            return documentType;
        }
    }
}
