using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron;
using System.Xml;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile;
using System.IO;

namespace dk.gov.oiosi.test.unit.extension.wcf.Interceptor.Validation.Schematron
{
    [TestFixture]
    public class SchematronValidatorWithLookupAttachedDocumentTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.AttacthedDocument.xml";
            ConfigurationHandler.Reset();
        }       

        [Test]
        public void SchematronValidateOioUblAttachedDocument()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_AttachedDocument202_XML);
            validator.Validate(xmlDocument);
        }      
    }
}
