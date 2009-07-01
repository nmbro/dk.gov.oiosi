using System;
using System.Xml;

using NUnit.Framework;

using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.xml.schematron;

namespace dk.gov.oiosi.test.nunit.library.xml.schematron {
    [TestFixture]
    public class SchematronValidationTest {

        private DefaultDocumentTypes _defaultDocumentTypes;

        [TestFixtureSetUp]
        public void InitDocumentTypes() {
            _defaultDocumentTypes = new DefaultDocumentTypes();
        }

        [Test]
        public void OioublApplicationResponse201Validation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetApplicationResponse();
            Validate(TestConstants.PATH_APPLICATIONRESPONSE201_XML, documentType);
        }

        [Test]
        public void OioublApplicationResponse202Validation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetApplicationResponse();
            Validate(TestConstants.PATH_APPLICATIONRESPONSE202_XML, documentType);
        }

        [Test]
        public void OioublCatalogueValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetCatalogue();
            Validate(TestConstants.PATH_CATALOGUE_XML, documentType);
        }

        [Test]
        public void OioublCatalogueDeletionValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetCatalogueDeletion();
            Validate(TestConstants.PATH_CATALOGUEDELETION_XML, documentType);
        }
        
        [Test]
        public void OioublCatalogueItemSpecificationUpdateValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetCatalogueItemSpecificationUpdate();
            Validate(TestConstants.PATH_CATALOGUEITEMSPECIFICATIONUPDATE_XML, documentType);
        }
        
        [Test]
        public void OioublCataloguePricingUpdateValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetCataloguePricingUpdate();
            Validate(TestConstants.PATH_CATALOGUEPRICINGUPDATE_XML, documentType);
        }
        
        [Test]
        public void OioublCatalogueRequestValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetCatalogueRequest();
            Validate(TestConstants.PATH_CATALOGUEREQUEST_XML, documentType);
        }
        
        [Test]
        public void OioublCreditNoteValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetCreditNote();
            Validate(TestConstants.PATH_CREDITNOTE_XML, documentType);
        }

        [Test]
        public void OioublInvoiceValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetInvoice();
            Validate(TestConstants.PATH_INVOICE_XML, documentType);
        }

        [Test]
        public void OioublOrderValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOrder();
            Validate(TestConstants.PATH_ORDER_XML, documentType);
        }

        [Test]
        public void OioublOrderCancellationValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOrderCancellation();
            Validate(TestConstants.PATH_ORDERCANCELLATION_XML, documentType);
        }

        [Test]
        public void OioublOrderChangeValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOrderChange();
            Validate(TestConstants.PATH_ORDERCHANGE_XML, documentType);
        }

        [Test]
        public void OioublOrderResponseValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOrderResponse();
            Validate(TestConstants.PATH_ORDERRESPONSE_XML, documentType);
        }

        [Test]
        public void OioublOrderResponseSimpleValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOrderResponseSimple();
            Validate(TestConstants.PATH_ORDERRESPONSESIMPLE_XML, documentType);
        }

        [Test]
        public void OioublReminderValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetReminder();
            Validate(TestConstants.PATH_REMINDER_XML, documentType);
        }

        [Test]
        public void OioublStatementValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetStatement();
            Validate(TestConstants.PATH_STATEMENT_XML, documentType);
        }

        [Test, ExpectedException(typeof(SchematronErrorException))]
        public void OioublNoIdInvoiceValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetInvoice();
            Validate(TestConstants.PATH_INVOICENOID_XML, documentType);
        }

        [Test]
        public void OioxmlInvoiceValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetInvoiceV07();
            Validate(TestConstants.PATH_INVOICE07_XML, documentType);
        }

        [Test, ExpectedException(typeof(SchematronErrorException))]
        public void OioxmlInvalidEanNumberInvoiceValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetInvoiceV07();
            Validate(TestConstants.PATH_INVOICEN7INVALIDEANNUMBER_XML, documentType);
        }

        private void Validate(string xmlDocumentPath, DocumentTypeConfig documentType) {
            Console.WriteLine("{0} Schematron validation started ", DateTime.Now);
            SchematronValidationConfig schematronValidationConfig = documentType.SchematronValidationConfig;
            XmlDocument document = new XmlDocument();
            Console.WriteLine("{0} Loading Xml Document '{1}'", DateTime.Now, xmlDocumentPath);
            document.Load(xmlDocumentPath);
            Console.WriteLine("{0} Instanciating the validator ", DateTime.Now);
            SchematronValidator validator = new SchematronValidator(schematronValidationConfig);
            Console.WriteLine("{0} Schematron validation", DateTime.Now);
            validator.SchematronValidateXmlDocument(document);
            Console.WriteLine("{0} Schematron validation completed", DateTime.Now);
        }
    }
}
