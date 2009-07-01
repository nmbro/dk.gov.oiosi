using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.xml.schematron;
using System.Xml;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.communication.configuration;
using System.Xml.Xsl;

namespace dk.gov.oiosi.test.nunit.library.xml.schematron {

    [TestFixture]
    public class SchematronStoreTest {

        [Test]
        public void _01_GetOnceTest() {
            Console.WriteLine(DateTime.Now + " GetOnceTest start");
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML);
            DocumentTypeConfigSearcher searcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig documentTypeConfig = searcher.FindUniqueDocumentType(document);
            SchematronStore store = new SchematronStore();
            XslCompiledTransform transform = store.GetCompiledSchematron(documentTypeConfig.SchematronValidationConfig.SchematronDocumentPath);
            Assert.IsNotNull(transform);
            Console.WriteLine(DateTime.Now + " GetOnceTest stop");
        }

        [Test]
        public void _02_GetTwiceTest() {
            Console.WriteLine(DateTime.Now + " GetOnceTest start");
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML);
            DocumentTypeConfigSearcher searcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig documentTypeConfig = searcher.FindUniqueDocumentType(document);
            SchematronStore store = new SchematronStore();
            XslCompiledTransform transform1 = store.GetCompiledSchematron(documentTypeConfig.SchematronValidationConfig.SchematronDocumentPath);
            Assert.IsNotNull(transform1);
            XslCompiledTransform transform2 = store.GetCompiledSchematron(documentTypeConfig.SchematronValidationConfig.SchematronDocumentPath);
            Assert.IsNotNull(transform2);
            Console.WriteLine(DateTime.Now + " GetOnceTest stop");
        }

        [Test]
        public void _03_GetTenTimesTest() {
            Console.WriteLine(DateTime.Now + " GetOnceTest start");
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML);
            DocumentTypeConfigSearcher searcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig documentTypeConfig = searcher.FindUniqueDocumentType(document);
            SchematronStore store = new SchematronStore();
            XslCompiledTransform transform1 = store.GetCompiledSchematron(documentTypeConfig.SchematronValidationConfig.SchematronDocumentPath);
            Assert.IsNotNull(transform1);

            for (int i = 0; i < 9; i++) {
                XslCompiledTransform transform2 = store.GetCompiledSchematron(documentTypeConfig.SchematronValidationConfig.SchematronDocumentPath);
                Assert.IsNotNull(transform2);
            }
            Console.WriteLine(DateTime.Now + " GetOnceTest stop");
        }
    }
}
