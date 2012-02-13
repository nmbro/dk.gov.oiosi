using System.IO;
using System.Xml;
using System.Xml.Schema;
using NUnit.Framework;

using dk.gov.oiosi.xml.schema;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.test.unit.xml.schema {

    [TestFixture]
    public class SchemaValidationTest {
        private readonly SchemaValidator _validator201;
        private readonly SchemaValidator _validator07;
        private readonly SchemaValidator _validator21b;
        private DocumentTypeConfigSearcher _searcher;

        public SchemaValidationTest() {
            DirectoryInfo schema07Directory = new DirectoryInfo(TestConstants.PATH_SCHEMAS07);
            _validator07 = new SchemaValidator();//schema07Directory

            DirectoryInfo schema201Directory = new DirectoryInfo(TestConstants.PATH_SCHEMAS20);
            _validator201 = new SchemaValidator();//schema201Directory

            DirectoryInfo schema21bDirectory = new DirectoryInfo(TestConstants.PATH_SCHEMAS21b);
            _validator21b = new SchemaValidator();//schema21bDirectory
        }

        [TestFixtureSetUp]
        public void LoadDefault() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            _searcher= new DocumentTypeConfigSearcher();
        }

        [Test]
        public void ApplicationResponse201ValidationTest() {
            const string xmlPath = TestConstants.PATH_APPLICATIONRESPONSE201_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void ApplicationResponse202ValidationTest() {
            const string xmlPath = TestConstants.PATH_APPLICATIONRESPONSE202_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CataloguelidationTest() {
            const string xmlPath = TestConstants.PATH_CATALOGUE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CatalogueDeletionValidationTest() {
            const string xmlPath = TestConstants.PATH_CATALOGUEDELETION_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CatalogueItemSpecificationUpdateValidationTest() {
            const string xmlPath = TestConstants.PATH_CATALOGUEITEMSPECIFICATIONUPDATE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CataloguePricingUpdateValidationTest() {
            const string xmlPath = TestConstants.PATH_CATALOGUEPRICINGUPDATE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CatalogueRequesttValidationTest() {
            const string xmlPath = TestConstants.PATH_CATALOGUEREQUEST_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CreditNoteValidationTest() {
            const string xmlPath = TestConstants.PATH_CREDITNOTE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void InvoiceValidationTest() {
            const string xmlPath = TestConstants.PATH_INVOICE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderValidationTest() {
            const string xmlPath = TestConstants.PATH_ORDER_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderCancellationValidationTest() {
            const string xmlPath = TestConstants.PATH_ORDERCANCELLATION_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderChangeValidationTest() {
            const string xmlPath = TestConstants.PATH_ORDERCHANGE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderResponseValidationTest() {
            const string xmlPath = TestConstants.PATH_ORDERRESPONSE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderResponseSimpleValidationTest() {
            const string xmlPath = TestConstants.PATH_ORDERRESPONSESIMPLE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void ReminderValidationTest() {
            const string xmlPath = TestConstants.PATH_REMINDER_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void StatementValidationTest() {
            const string xmlPath = TestConstants.PATH_STATEMENT_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void UtilityStatementValidationTest() {
            const string xmlPath = TestConstants.PATH_UTILITYSTATEMENT_XML;
            Validate(xmlPath, _validator21b, TestConstants.PATH_SCHEMAS21b);
        }

        [Test]
        public void Invoice07ValidationTest() {
            string xmlPath = TestConstants.PATH_INVOICE07_XML;
            Validate(xmlPath, _validator07, TestConstants.PATH_SCHEMAS07);
        }

   /*     [Test, ExpectedException(typeof(dk.gov.oiosi.xml.schema.SchemaValidationFailedException))]
        public void InvoiceWrongNamespaceValidationTest() {
            string xmlDocumentPath = TestConstants.PATH_INVOICEWRONGNAMESPACE_XML;
            string xmlSearchDocumentPath = TestConstants.PATH_ORDER_XML;
            XmlDocument invalidXmlDocument = new XmlDocument();
            XmlDocument validXmlDocument = new XmlDocument();
            invalidXmlDocument.Load(xmlDocumentPath);
            validXmlDocument.Load(xmlSearchDocumentPath);*/
            //DocumentTypeConfig invalidDocumentType = _searcher.FindUniqueDocumentType(invalidXmlDocument);
           // DocumentTypeConfig validDocumentType = _searcher.FindUniqueDocumentType(validXmlDocument);

            // Need the schema cache from the cacheConfiguration
           // ConfigurationHandler.ConfigFilePath = "Resources/RaspConfigurationCacheConfig.xml";    
            
            //
           // SchemaStore schemaStore = new SchemaStore();
          //  XmlSchemaSet xmlSchemaSet = schemaStore.GetCompiledXmlSchemaSet(validDocumentType);
            
            /*
            string xmlSchemaPath = documentType.SchemaPath;
            FileStream stream = File.OpenRead(xmlSchemaPath);
            XmlSchema schema = XmlSchema.Read(stream, null);
            stream.Close();
            */
            //SchemaStore schemaStore = new SchemaStore();
            //XmlSchemaSet xmlSchemaSet = schemaStore.LoadXmlSchemaSet(TestConstants.PATH_SCHEMAS20, schema);

         //   _validator201.SchemaValidateXmlDocument(invalidXmlDocument, xmlSchemaSet);
        //}

        [Test, ExpectedException(typeof(dk.gov.oiosi.xml.schema.SchemaValidationFailedException))]
        public void InvoiceWrongElementValidationTest() {
            string xmlPath = TestConstants.PATH_INVOICEWRONGELEMENT_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        private void Validate(string xmlDocumentPath, SchemaValidator validator, string schemaPath) 
        {
            // Need the schema cache from the cacheConfiguration
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.Test.xml";    
            
            XmlDocument document = new XmlDocument();
            document.Load(xmlDocumentPath);
            
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);

            SchemaStore schemaStore = new SchemaStore();
            XmlSchemaSet xmlSchemaSet = schemaStore.GetCompiledXmlSchemaSet(documentType);
            validator.SchemaValidateXmlDocument(document, xmlSchemaSet);
        }
    }
}
