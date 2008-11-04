using System;
using System.Collections.Generic;
using System.Text;
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
        public void OioublInvoiceValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.Invoice();
            Validate(TestConstants.PATH_INVOICE_XML, documentType);
        }

        [Test]
        public void OioublApplicationResponseValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.ApplicationResponse();
            Validate(TestConstants.PATH_APPLICATIONRESPONSE_XML, documentType);
        }

        [Test]
        public void OioublCreditNoteValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.CreditNote();
            Validate(TestConstants.PATH_CREDITNOTE_XML, documentType);
        }

        [Test]
        public void OioublOrderValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.Order();
            Validate(TestConstants.PATH_ORDER_XML, documentType);
        }

        [Test]
        public void OioublOrderResponseSimpleValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.OrderResponseSimple();
            Validate(TestConstants.PATH_ORDERRESPONSESIMPLE_XML, documentType);
        }

        [Test]
        public void OioublReminderValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.Reminder();
            Validate(TestConstants.PATH_REMINDER_XML, documentType);
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.xml.schematron.SchematronErrorException))]
        public void OioublNoIdInvoiceValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.Invoice();
            Validate(TestConstants.PATH_INVOICENOID_XML, documentType);
        }

        [Test]
        public void OioxmlInvoiceValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.InvoiceV07();
            Validate(TestConstants.PATH_INVOICE07_XML, documentType);
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.xml.schematron.SchematronErrorException))]
        public void OioxmlInvalidEanNumberInvoiceValidation() {
            DocumentTypeConfig documentType = _defaultDocumentTypes.InvoiceV07();
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
