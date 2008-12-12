using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Xml;
using dk.gov.oiosi.common;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.uddi.category;

namespace dk.gov.oiosi.test.nunit.library.common {
    [TestFixture]
    public class UtilitiesTest {

        private DocumentTypeConfigSearcher _searcher;

        public UtilitiesTest() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            _searcher = new DocumentTypeConfigSearcher();
        }

        [Test]
        public void TestKeyTypeCodeDefault() {
            // Testing default keytypecode (EAN)
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_EAN);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;            
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.ean, code);
        }

        [Test]
        public void TestKeyTypeCodeEAN() {
            // Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_EAN);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.ean, code);
        }

        [Test]
        public void TestKeyTypeCodeOVT() {
            // Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_OVT);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.ovt, code);
        }

        [Test]
        public void TestKeyTypeCodeCVR() {
            // Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_CVR);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.cvr, code);
        }

        [Test]
        public void TestKeyTypeCodeP() {
            // Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_P);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.p, code);
        }

        [Test]
        public void TestKeyTypeCodeSE() {
            // Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_SE);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.se, code);
        }

        [Test]
        public void TestKeyTypeCodeVANS() {
            // Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_VANS);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.vans, code);
        }

        [Test]
        public void TestKeyTypeCodeIBAN() {
            // Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_IBAN);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.iban, code);
        }

        [Test]
        public void TestKeyTypeCodeDUNS() {
            // Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_DUNS);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.duns, code);
        }     
    }
}
