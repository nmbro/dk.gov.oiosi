using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.schematron;
using NUnit.Framework;

namespace dk.gov.oiosi.test.unit.xml.schematron
{
    [TestFixture]
    public class SchematronStoreTest
    {
        public SchematronStoreTest()
        {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.Live.xml";
            ConfigurationHandler.Reset();
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            ConfigurationHandler.SaveToFile();
        }

        [Test]
        public void _01_GetOnceTest()
        {
            Console.WriteLine(DateTime.Now + " GetOnceTest start");
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML);
            DocumentTypeConfigSearcher searcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig documentTypeConfig = searcher.FindUniqueDocumentType(document);
            SchematronStore store = new SchematronStore();
            SchematronValidationConfig[] schematronValidationConfigCollection = documentTypeConfig.SchematronValidationConfigs;
            foreach (SchematronValidationConfig schematronValidationConfig in schematronValidationConfigCollection)
            {
                CompiledXslt transform = store.GetCompiledSchematron(schematronValidationConfig.SchematronDocumentPath);
                Assert.IsNotNull(transform);
                Assert.IsNotNull(transform.XslCompiledTransform);
            }
            Console.WriteLine(DateTime.Now + " GetOnceTest stop");
        }

        [Test]
        public void _02_GetTwiceTest()
        {
            Console.WriteLine(DateTime.Now + " GetTwiceTest start");
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML);
            DocumentTypeConfigSearcher searcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig documentTypeConfig = searcher.FindUniqueDocumentType(document);
            SchematronStore store = new SchematronStore();
            SchematronValidationConfig[] schematronValidationConfigCollection = documentTypeConfig.SchematronValidationConfigs;
            foreach (SchematronValidationConfig schematronValidationConfig in schematronValidationConfigCollection)
            {
                CompiledXslt transform = store.GetCompiledSchematron(schematronValidationConfig.SchematronDocumentPath);
                Assert.IsNotNull(transform);
                Assert.IsNotNull(transform.XslCompiledTransform);
            }

            schematronValidationConfigCollection = documentTypeConfig.SchematronValidationConfigs;
            foreach (SchematronValidationConfig schematronValidationConfig in schematronValidationConfigCollection)
            {
                CompiledXslt transform = store.GetCompiledSchematron(schematronValidationConfig.SchematronDocumentPath);
                Assert.IsNotNull(transform);
                Assert.IsNotNull(transform.XslCompiledTransform);
            }

            Console.WriteLine(DateTime.Now + " GetTwiceTest stop");
        }

        [Test]
        public void _03_GetTenTimesTest()
        {
            Console.WriteLine(DateTime.Now + " GetTenTimesTest start");
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML);
            DocumentTypeConfigSearcher searcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig documentTypeConfig = searcher.FindUniqueDocumentType(document);
            SchematronStore store = new SchematronStore();
            SchematronValidationConfig[] schematronValidationConfigCollection = documentTypeConfig.SchematronValidationConfigs;

            for (int i = 0; i < 9; i++)
            {
                foreach (SchematronValidationConfig schematronValidationConfig in schematronValidationConfigCollection)
                {
                    CompiledXslt transform2 = store.GetCompiledSchematron(schematronValidationConfig.SchematronDocumentPath);
                    Assert.IsNotNull(transform2);
                    Assert.IsNotNull(transform2.XslCompiledTransform);
                }
            }

            Console.WriteLine(DateTime.Now + " GetTenTimesTest stop");
        }
    }
}