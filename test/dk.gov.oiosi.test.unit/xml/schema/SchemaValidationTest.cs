using System.IO;
using System.Xml;
using System.Xml.Schema;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.schema;
using NUnit.Framework;

namespace dk.gov.oiosi.test.unit.xml.schema
{
    [TestFixture]
    public class SchemaValidationTest
    {
        private readonly SchemaValidator _validator201;
        private readonly SchemaValidator _validator07;
        private readonly SchemaValidator _validator21b;
        private readonly SchemaValidator _validator21;
        private DocumentTypeConfigSearcher _searcher;

        public SchemaValidationTest()
        {
            this._validator07 = new SchemaValidator();
            this._validator201 = new SchemaValidator();
            this._validator21b = new SchemaValidator();
            this._validator21 = new SchemaValidator();
        }

        [TestFixtureSetUp]
        public void LoadDefault()
        {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            _searcher = new DocumentTypeConfigSearcher();
        }

        [Test]
        public void ApplicationResponse201ValidationTest()
        {
            const string xmlPath = TestConstants.PATH_APPLICATIONRESPONSE201_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void ApplicationResponse202ValidationTest()
        {
            const string xmlPath = TestConstants.PATH_APPLICATIONRESPONSE202_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CataloguelidationTest()
        {
            const string xmlPath = TestConstants.PATH_CATALOGUE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CatalogueDeletionValidationTest()
        {
            const string xmlPath = TestConstants.PATH_CATALOGUEDELETION_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CatalogueItemSpecificationUpdateValidationTest()
        {
            const string xmlPath = TestConstants.PATH_CATALOGUEITEMSPECIFICATIONUPDATE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CataloguePricingUpdateValidationTest()
        {
            const string xmlPath = TestConstants.PATH_CATALOGUEPRICINGUPDATE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CatalogueRequesttValidationTest()
        {
            const string xmlPath = TestConstants.PATH_CATALOGUEREQUEST_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void CreditNoteValidationTest()
        {
            const string xmlPath = TestConstants.PATH_CREDITNOTE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void InvoiceValidationTest()
        {
            const string xmlPath = TestConstants.PATH_INVOICE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderValidationTest()
        {
            const string xmlPath = TestConstants.PATH_ORDER_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderCancellationValidationTest()
        {
            const string xmlPath = TestConstants.PATH_ORDERCANCELLATION_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderChangeValidationTest()
        {
            const string xmlPath = TestConstants.PATH_ORDERCHANGE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderResponseValidationTest()
        {
            const string xmlPath = TestConstants.PATH_ORDERRESPONSE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void OrderResponseSimpleValidationTest()
        {
            const string xmlPath = TestConstants.PATH_ORDERRESPONSESIMPLE_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void ReminderValidationTest()
        {
            const string xmlPath = TestConstants.PATH_REMINDER_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void StatementValidationTest()
        {
            const string xmlPath = TestConstants.PATH_STATEMENT_XML;
            Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20);
        }

        [Test]
        public void UtilityStatementValidationTest()
        {
            const string xmlPath = TestConstants.PATH_UTILITYSTATEMENT_XML;
            Validate(xmlPath, _validator21b, TestConstants.PATH_SCHEMAS21b);
        }

        [Test]
        public void Invoice07ValidationTest()
        {
            string xmlPath = TestConstants.PATH_INVOICE07_XML;
            Validate(xmlPath, _validator07, TestConstants.PATH_SCHEMAS07);
        }

        // Peppol

        ////[Test]
        ////public void Peppol36aApplicationResponse()
        ////{
        ////    string xmlPath = TestConstants.PATH_PEPPOL_Peppol36aApplicationResponse_XML;
        ////    Validate(xmlPath, this._validator21, TestConstants.PATH_SCHEMAS21);
        ////}

        [Test]
        public void Peppol1aCatalogue()
        {
            //string xmlPath = TestConstants.PATH_PEPPOL_Peppol1aCatalogue_XML;
            // official test doc - all invalid
            ////Validate(TestConstants.PATH_PEPPOL/_Peppol1a_UseCase1_Catalogue_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            ////Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase2_Catalogue_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            ////Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase3_Catalogue_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            ////Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase4_Catalogue_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            ////Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase5_Catalogue_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
        }

        [Test]
        public void Peppol1aApplicationResponse()
        {
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase1_CatalogueResponse_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase2_CatalogueResponse_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase3_CatalogueResponse_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase4_CatalogueResponse_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase5_CatalogueResponse_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
        }

        [Test]
        public void Peppol5aCreditNote()
        {
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase1a_CreditNote_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase1b_CreditNote_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase2_CreditNote_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase3_CreditNote_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase4_CreditNote_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase5_CreditNote_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
        }

        [Test]
        public void Peppol5aInvoice()
        {
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_Invocie_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
        }

        [Test]
        public void Peppol30aDespatchAdvice()
        {
            Validate(TestConstants.PATH_PEPPOL_Peppol30a_UserCase1_DespatchAdvice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol30a_UserCase2_DespatchAdvice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol30a_UserCase3_DespatchAdvice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol30a_UserCase4_DespatchAdvice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol30a_UserCase5_DespatchAdvice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
        }

        [Test]
        public void Peppol4aInvoice()
        {
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase1a_Invoice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase1b_Invoice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase2_Invoice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase3_Invoice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase4_Invoice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);

            // Schema error
            //Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase5_Invoice_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
        }

        [Test]
        public void Peppol3aOrder()
        {
            Validate(TestConstants.PATH_PEPPOL_Peppol3a_UserCase1_Order_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol3a_UserCase2_Order_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol3a_UserCase3_Order_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
            Validate(TestConstants.PATH_PEPPOL_Peppol3a_UserCase4_Order_XML, this._validator21, TestConstants.PATH_SCHEMAS21);
        }

        ////[Test]
        ////public void Peppol28aOrder()
        ////{
        ////    string xmlPath = TestConstants.PATH_PEPPOL_Peppol28aOrder_XML;
        ////    Validate(xmlPath, this._validator21, TestConstants.PATH_SCHEMAS21);
        ////}

        ////[Test]
        ////public void Peppol28aOrderResponse()
        ////{
        ////    string xmlPath = TestConstants.PATH_PEPPOL_Peppol28aOrderResponse_XML;
        ////    Validate(xmlPath, this._validator21, TestConstants.PATH_SCHEMAS21);
        ////}

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

        // Need the schema cache from the cacheConfiguration ConfigurationHandler.ConfigFilePath = "Resources/RaspConfigurationCacheConfig.xml";

        // SchemaStore schemaStore = new SchemaStore(); XmlSchemaSet xmlSchemaSet = schemaStore.GetCompiledXmlSchemaSet(validDocumentType);

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

        [Test]
        public void InvoiceWrongElementValidationTest()
        {
            string xmlPath = TestConstants.PATH_INVOICEWRONGELEMENT_XML;
            Assert.Throws<dk.gov.oiosi.xml.schema.SchemaValidationFailedException>(() => this.Validate(xmlPath, _validator201, TestConstants.PATH_SCHEMAS20));
        }

        private void Validate(string xmlDocumentPath, SchemaValidator validator, string schemaPath)
        {
            // Need the schema cache from the cacheConfiguration
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.Live.xml";
            ConfigurationHandler.Reset();

            XmlDocument document = new XmlDocument();
            document.Load(xmlDocumentPath);

            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);

            SchemaStore schemaStore = new SchemaStore();
            XmlSchemaSet xmlSchemaSet = schemaStore.GetCompiledXmlSchemaSet(documentType);
            validator.SchemaValidateXmlDocument(document, xmlSchemaSet);
        }
    }
}