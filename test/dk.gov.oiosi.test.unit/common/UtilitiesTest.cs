using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.uddi;
using NUnit.Framework;
using System.Xml;
using dk.gov.oiosi.common;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.addressing;

namespace dk.gov.oiosi.test.unit.common 
{
    [TestFixture]
    public class UtilitiesTest {

        private DocumentTypeConfigSearcher _searcher;

        public UtilitiesTest() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            _searcher = new DocumentTypeConfigSearcher();
        }

        ////[Test]
        ////public void TestIdentifierEAN() {
        ////    XmlDocument document = new XmlDocument();
        ////    document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_EAN);
        ////    DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
        ////    string code = Utilities.GetEndpointKeyTypeCode(document, documentType);
        ////    Assert.AreEqual(new IdentifierEan("5798009811578").KeyTypeCode, code);

        ////    string keyXpath = documentType.EndpointType.Key.XPath;
        ////    PrefixedNamespace[] namespaces = documentType.Namespaces;

        ////    Identifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
        ////    Assert.AreEqual(typeof(IdentifierEan), identifier.GetType());

        ////}
        
        ////[Test]
        ////public void TestIdentifierOVT() {
        ////    XmlDocument document = new XmlDocument();
        ////    document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_OVT);
        ////    DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
        ////    string code = Utilities.GetEndpointKeyTypeCode(document, documentType);
        ////    Assert.AreEqual("ovt", code);

        ////    string keyXpath = documentType.EndpointType.Key.XPath;
        ////    PrefixedNamespace[] namespaces = documentType.Namespaces;

        ////    Identifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
        ////    Assert.AreEqual(typeof(Identifier), identifier.GetType());
        ////}

        ////[Test]
        ////public void TestIdentifierCVR() {
        ////    XmlDocument document = new XmlDocument();
        ////    document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_CVR);
        ////    DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
        ////    string code = Utilities.GetEndpointKeyTypeCode(document, documentType);
        ////    Assert.AreEqual("cvr", code);

        ////    string keyXpath = documentType.EndpointType.Key.XPath;
        ////    PrefixedNamespace[] namespaces = documentType.Namespaces;

        ////    Identifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
        ////    Assert.AreEqual(typeof(IdentifierCvr),identifier.GetType());
        ////}

        ////[Test]
        ////public void TestIdentifierP() {
        ////    XmlDocument document = new XmlDocument();
        ////    document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_P);
        ////    DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
        ////    string code = Utilities.GetEndpointKeyTypeCode(document, documentType);
        ////    Assert.AreEqual("p", code);

        ////    string keyXpath = documentType.EndpointType.Key.XPath;
        ////    PrefixedNamespace[] namespaces = documentType.Namespaces;

        ////    Identifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
        ////    Assert.AreEqual(typeof(Identifier), identifier.GetType());
        ////}

        ////[Test]
        ////public void TestIdentifierSE() {
        ////    XmlDocument document = new XmlDocument();
        ////    document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_SE);
        ////    DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
        ////    string code = Utilities.GetEndpointKeyTypeCode(document, documentType);
        ////    Assert.AreEqual("se", code);

        ////    string keyXpath = documentType.EndpointType.Key.XPath;
        ////    PrefixedNamespace[] namespaces = documentType.Namespaces;

        ////    Identifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
        ////    Assert.AreEqual(typeof(Identifier), identifier.GetType());
        ////}

        ////[Test]
        ////public void TestIdentifierVANS() {
        ////    XmlDocument document = new XmlDocument();
        ////    document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_VANS);
        ////    DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
        ////    string code = Utilities.GetEndpointKeyTypeCode(document, documentType);
        ////    Assert.AreEqual("vans", code);

        ////    string keyXpath = documentType.EndpointType.Key.XPath;
        ////    PrefixedNamespace[] namespaces = documentType.Namespaces;

        ////    Identifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
        ////    Assert.AreEqual(typeof(Identifier), identifier.GetType());
        ////}

        ////[Test]
        ////public void TestIdentifierIBAN() {
        ////    XmlDocument document = new XmlDocument();
        ////    document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_IBAN);
        ////    DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
        ////    string code = Utilities.GetEndpointKeyTypeCode(document, documentType);
        ////    Assert.AreEqual("iban", code);

        ////    string keyXpath = documentType.EndpointType.Key.XPath;
        ////    PrefixedNamespace[] namespaces = documentType.Namespaces;

        ////    Identifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
        ////    Assert.AreEqual(typeof(Identifier), identifier.GetType());
        ////}

        ////[Test]
        ////public void TestIdentifierDUNS() {
        ////    XmlDocument document = new XmlDocument();
        ////    document.Load(TestConstants.PATH_INVOICE_XML_IDENTIFIER_DUNS);
        ////    DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
        ////    string code = Utilities.GetEndpointKeyTypeCode(document, documentType);
        ////    Assert.AreEqual("duns", code);

        ////    string keyXpath = documentType.EndpointType.Key.XPath;
        ////    PrefixedNamespace[] namespaces = documentType.Namespaces;

        ////    Identifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
        ////    Assert.AreEqual(typeof(Identifier), identifier.GetType());
        ////}     
          
    }
}
