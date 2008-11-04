using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using NUnit.Framework;

using dk.gov.oiosi.xml.schema;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.communication.configuration;

namespace dk.gov.oiosi.test.nunit.library.xml.schema {

    [TestFixture]
    public class SchemaValidationTest {
        private SchemaValidator _validator201;
        private SchemaValidator _validator07;
        private DocumentTypeConfigSearcher _searcher;

        public SchemaValidationTest() {
            DirectoryInfo schema07Directory = new DirectoryInfo(TestConstants.PATH_SCHEMAS07);
            _validator07 = new SchemaValidator(schema07Directory);

            DirectoryInfo schema201Directory = new DirectoryInfo(TestConstants.PATH_SCHEMAS201);
            _validator201 = new SchemaValidator(schema201Directory);
        }

        [TestFixtureSetUp]
        public void LoadDefault() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            _searcher= new DocumentTypeConfigSearcher();
        }

        [Test]
        public void ApplicationResponseValidationTest() {
            string xmlPath = TestConstants.PATH_APPLICATIONRESPONSE_XML;
            Validate(xmlPath, _validator201);
        }

        [Test]
        public void CreditNoteValidationTest() {
            string xmlPath = TestConstants.PATH_CREDITNOTE_XML;
            Validate(xmlPath, _validator201);
        }

        [Test]
        public void InvoiceValidationTest() {
            string xmlPath = TestConstants.PATH_INVOICE_XML;
            Validate(xmlPath, _validator201);
        }

        [Test]
        public void OrderValidationTest() {
            string xmlPath = TestConstants.PATH_ORDER_XML;
            Validate(xmlPath, _validator201);
        }

        [Test]
        public void OrderResponseSimpleValidationTest() {
            string xmlPath = TestConstants.PATH_ORDERRESPONSESIMPLE_XML;
            Validate(xmlPath, _validator201);
        }

        [Test]
        public void ReminderValidationTest() {
            string xmlPath = TestConstants.PATH_REMINDER_XML;
            Validate(xmlPath, _validator201);
        }

        [Test]
        public void Invoice07ValidationTest() {
            string xmlPath = TestConstants.PATH_INVOICE07_XML;
            Validate(xmlPath, _validator07);
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.xml.schema.SchemaValidationFailedException))]
        public void InvoiceWrongNamespaceValidationTest() {
            string xmlDocumentPath = TestConstants.PATH_INVOICEWRONGNAMESPACE_XML;
            string xmlSearchDocumentPath = TestConstants.PATH_INVOICE_XML;
            XmlDocument document = new XmlDocument();
            XmlDocument xmlDocument = new XmlDocument();
            document.Load(xmlDocumentPath);
            xmlDocument.Load(xmlSearchDocumentPath);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(xmlDocument);
            string xmlSchemaPath = documentType.SchemaPath;
            FileStream stream = File.OpenRead(xmlSchemaPath);
            XmlSchema schema = XmlSchema.Read(stream, null);
            stream.Close();
            _validator201.SchemaValidateXmlDocument(document, schema);
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.xml.schema.SchemaValidationFailedException))]
        public void InvoiceWrongElementValidationTest() {
            string xmlPath = TestConstants.PATH_INVOICEWRONGELEMENT_XML;
            Validate(xmlPath, _validator201);
        }

        private void Validate(string xmlDocumentPath, SchemaValidator validator) {
            XmlDocument document = new XmlDocument();
            document.Load(xmlDocumentPath);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            FileStream stream = File.OpenRead(xmlSchemaPath);
            XmlSchema schema = XmlSchema.Read(stream, null);
            stream.Close();
            validator.SchemaValidateXmlDocument(document, schema);
        }
    }
}
