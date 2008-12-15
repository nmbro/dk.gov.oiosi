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
using dk.gov.oiosi.addressing;

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
        public void dummy() {
            Assert.IsNotNull(1);
        }
        
        [Test]
        public void TestIdentifierEAN() {
            //Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_EAN);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.ean, code);

            string keyXpath = documentType.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = documentType.Namespaces;

            IIdentifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            Assert.AreEqual(typeof(IdentifierEan), identifier.GetType());

        }
        
        [Test]
        public void TestIdentifierOVT() {
            // Testing OVT keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_OVT);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.ovt, code);

            string keyXpath = documentType.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = documentType.Namespaces;

            IIdentifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            Assert.AreEqual(typeof(IdentifierOvt), identifier.GetType());
        }

        [Test]
        public void TestIdentifierCVR() {
            // Testing EAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_CVR);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.cvr, code);

            string keyXpath = documentType.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = documentType.Namespaces;

            IIdentifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            Assert.AreEqual(typeof(IdentifierCvr),identifier.GetType());
        }

        [Test]
        public void TestIdentifierP() {
            // Testing P keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_P);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.p, code);

            string keyXpath = documentType.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = documentType.Namespaces;

            IIdentifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            Assert.AreEqual(typeof(IdentifierP), identifier.GetType());
        }

        [Test]
        public void TestIdentifierSE() {
            // Testing SE keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_SE);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.se, code);

            string keyXpath = documentType.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = documentType.Namespaces;

            IIdentifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            Assert.AreEqual(typeof(IdentifierSe), identifier.GetType());
        }

        [Test]
        public void TestIdentifierVANS() {
            // Testing VANS keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_VANS);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.vans, code);

            string keyXpath = documentType.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = documentType.Namespaces;

            IIdentifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            Assert.AreEqual(typeof(IdentifierVans), identifier.GetType());
        }

        [Test]
        public void TestIdentifierIBAN() {
            // Testing IBAN keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_IBAN);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.iban, code);

            string keyXpath = documentType.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = documentType.Namespaces;

            IIdentifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            Assert.AreEqual(typeof(IdentifierIban), identifier.GetType());
        }

        [Test]
        public void TestIdentifierDUNS() {
            // Testing DUNS keytypecode
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_DUNS);
            DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
            string xmlSchemaPath = documentType.SchemaPath;
            EndpointKeyTypeCode code = Utilities.GetEndpointKeyTypeCode(document, documentType);
            Assert.AreEqual(EndpointKeyTypeCode.duns, code);

            string keyXpath = documentType.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = documentType.Namespaces;

            IIdentifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            Assert.AreEqual(typeof(IdentifierDuns), identifier.GetType());
        }     
          
    }
}
