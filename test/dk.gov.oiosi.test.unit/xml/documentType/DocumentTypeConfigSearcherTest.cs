using System.Xml;

using NUnit.Framework;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.configuration;
using System;

namespace dk.gov.oiosi.test.unit.xml.documentType {

    [TestFixture]
    public class DocumentTypeConfigSearcherTest {
        private const string friendlyName = "No Namespace";
        private readonly DocumentTypeConfigSearcher _searcher;
        private DefaultDocumentTypes _documentTypes;

        public DocumentTypeConfigSearcherTest() {
            ConfigurationHandler.ConfigFilePath = "Resources/DocumentTypeSearcherRaspConfiguration.xml";
            ConfigurationHandler.Reset();
            _documentTypes = new DefaultDocumentTypes();
            _documentTypes.CleanAdd();
            AddNoNamespaceTestDocumentType();
            _searcher = new DocumentTypeConfigSearcher();
            ConfigurationHandler.SaveToFile();
        }

        [TestFixtureTearDown]
        public void TearDown() {
            ConfigurationHandler.ConfigFilePath = "RaspConfiguration.xml";
            ConfigurationHandler.Reset();
        }

        [Test]
        public void SearchForApplicationResponse201Test() {
            AssertFindDocument("Applikationsmeddelse", TestConstants.PATH_APPLICATIONRESPONSE201_XML);
        }

        [Test]
        public void SearchForApplicationResponse202Test() {
            AssertFindDocument("Applikationsmeddelse", TestConstants.PATH_APPLICATIONRESPONSE202_XML);
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
        public void SearchForUtilityStatementTest() {
            AssertFindDocument("Forsynings Specifikation", TestConstants.PATH_UTILITYSTATEMENT_XML);
        }

        [Test]
        public void SearchForInvoice07Test() {
            AssertFindDocument("Faktura v0.7", TestConstants.PATH_INVOICE07_XML);
        }

        [Test]
        public void SearchForNoNamespaceTest() {
            AssertFindDocument(friendlyName, TestConstants.PATH_NONAMESPACE_XML);
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

        private void AddNoNamespaceTestDocumentType() {
            Console.WriteLine("ConfigurationHandler.Version=" + ConfigurationHandler.Version);
            Console.WriteLine("ConfigurationHandler.ConfigFilePath=" + ConfigurationHandler.ConfigFilePath);
            DocumentTypeCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            DocumentTypeConfig documentType = new DocumentTypeConfig();
            documentType.FriendlyName = friendlyName;
            documentType.RootName = "NoNamespace";
            documentType.RootNamespace = "";
            configuration.AddDocumentType(documentType);
        }
    }
}
