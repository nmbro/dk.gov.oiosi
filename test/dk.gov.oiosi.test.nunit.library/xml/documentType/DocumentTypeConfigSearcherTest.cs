using System.Xml;

using NUnit.Framework;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.raspProfile;

namespace dk.gov.oiosi.test.nunit.library.xml.documentType {

    [TestFixture]
    public class DocumentTypeConfigSearcherTest {
            
        private readonly DocumentTypeConfigSearcher _searcher;

        public DocumentTypeConfigSearcherTest() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            _searcher = new DocumentTypeConfigSearcher();
        }

        [Test]
        public void SearchForApplicationResponseTest() {
            AssertFindDocument("Applikationsmeddelse", TestConstants.PATH_APPLICATIONRESPONSE_XML);
        }

        [Test]
        public void SearchForCatalogueTest() {
            AssertFindDocument("Katalog", TestConstants.PATH_CATALOGUE_XML);
        }
        
        [Test]
        public void SearchForCatalogueDeletionTest() {
            AssertFindDocument("Sletning af katalog", TestConstants.PATH_CATALOGUEDELETION_XML);
        }

        [Test]
        public void SearchForCatalogueItemSpecificationUpdateTest() {
            AssertFindDocument("Opdatering af katalogelement", TestConstants.PATH_CATALOGUEITEMSPECIFICATIONUPDATE_XML);
        }

        [Test]
        public void SearchForCataloguePricingUpdateTest() {
            AssertFindDocument("Opdatering af katalogpriser", TestConstants.PATH_CATALOGUEPRICINGUPDATE_XML);
        }

        [Test]
        public void SearchForCatalogueRequestTest() {
            AssertFindDocument("Katalogforespørgsel", TestConstants.PATH_CATALOGUEREQUEST_XML);
        }

        [Test]
        public void SearchForCreditNoteTest() {
            AssertFindDocument("Kreditnota", TestConstants.PATH_CREDITNOTE_XML);
        }

        [Test]
        public void SearchForInvoiceTest() {
            AssertFindDocument("Faktura", TestConstants.PATH_INVOICE_XML);
        }

        public void SearchForOrderTest() {
            AssertFindDocument("Order", TestConstants.PATH_ORDER_XML);
        }

        [Test]
        public void SearchForOrderCancellationTest() {
            AssertFindDocument("Ordreannulering", TestConstants.PATH_ORDERCANCELLATION_XML);
        }

        [Test]
        public void SearchForOrderChangeTest() {
            AssertFindDocument("Ordreændring", TestConstants.PATH_ORDERCHANGE_XML);
        }

        [Test]
        public void SearchForOrderResponseTest() {
            AssertFindDocument("Ordrebekræftelse", TestConstants.PATH_ORDERRESPONSE_XML);
        }

        [Test]
        public void SearchForOrderResponseSimpleTest() {
            AssertFindDocument("Simpel ordrebekræftelse", TestConstants.PATH_ORDERRESPONSESIMPLE_XML);
        }

        [Test]
        public void SearchForReminderTest() {
            AssertFindDocument("Rykker", TestConstants.PATH_REMINDER_XML);
        }

        [Test]
        public void SearchForStatementTest() {
            AssertFindDocument("KontoUdtog", TestConstants.PATH_STATEMENT_XML);
        }

        [Test]
        public void SearchForInvoice07Test() {
            AssertFindDocument("Faktura v0.7", TestConstants.PATH_INVOICE07_XML);
        }

        [Test, ExpectedException(typeof(NoDocumentTypeFoundFromXmlDocumentException))]
        public void SearchForUnkownTypeTest() {
            AssertFindDocument(null, TestConstants.PATH_UNKNOWNTYPE_XML);
        }

        [Test, ExpectedException(typeof(exception.NullArgumentException))]
        public void NullArguementTest() {
            _searcher.FindUniqueDocumentType(null);
        }

        private DocumentTypeConfig SearchForDocument(string path) {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            return documentType;
        }

        private void AssertFindDocument(string friendlyName, string documentPath) {
            DocumentTypeConfig documentType = SearchForDocument(documentPath);
            Assert.AreEqual(friendlyName, documentType.FriendlyName);
        }

    }
}
