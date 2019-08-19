using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron;
using System.Xml;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile;
using System.IO;
using dk.gov.oiosi.xml.schematron;

namespace dk.gov.oiosi.test.unit.extension.wcf.Interceptor.Validation.Schematron
{
    [TestFixture]
    public class SchematronValidatorWithLookupTest
    {

        [SetUp]
        public void Setup()
        {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.Live.xml";
            ConfigurationHandler.Reset();
        }

        [Test]
        public void SchematronValidateTwentyTimesInvoice()
        {

            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice start");
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice first stylesheet start");
            string xmlDocument = File.ReadAllText(TestConstants.PATH_INVOICE_XML);
            validator.Validate(xmlDocument);
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice first stylesheet end");
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice last stylesheets start");
            for (int i = 0; i < 20; i++)
            {
                validator = new SchematronValidatorWithLookup();
                string xmlDocument2 = File.ReadAllText(TestConstants.PATH_INVOICE_XML);
                validator.Validate(xmlDocument2);
            }

            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice last stylesheets end");
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice end");
        }

        [Test]
        public void SchematronValidateClientSimulation()
        {
            SchematronValidatorWithLookup validator1 = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_INVOICE_XML);            
            validator1.Validate(xmlDocument);

            string xmlDocument2 = File.ReadAllText(TestConstants.PATH_INVOICE_XML);
            SchematronValidatorWithLookup validator2 = new SchematronValidatorWithLookup();
            validator2.Validate(xmlDocument2);
        }

        [Test]
        public void SchematronValidateOioXmlPIE()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_INVOICE07_XML);
            validator.Validate(xmlDocument);
        }

        [Test]
        public void SchematronValidateOioXmlPIEInvalid()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_INVOICE07_XML_Invalid);
            try
            {
                validator.Validate(xmlDocument);
                Assert.Fail("No exception on invalid doc");
            }
            catch (SchematronValidateDocumentFailedException)
            {
                // expected
            }
            catch (Exception)
            {
                Assert.Fail("Wrong type of exception");
            }
        }

        [Test]
        public void SchematronValidateOioUblInvoice()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_INVOICE_XML);
            validator.Validate(xmlDocument);
        }

        [Test]
        public void SchematronValidateOioUblInvoiceInvalid()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_INVOICE_XML_Invalid);
            try
            {
                validator.Validate(xmlDocument);
                Assert.Fail("No exception on invalid doc");
            }
            catch (SchematronValidateDocumentFailedException)
            {
                // expected
            }
            catch (Exception)
            {
                Assert.Fail("Wrong type of exception");
            }
        }

        [Test]
        public void SchematronValidateNemKontoKvittering0()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_NemKonto_Kvittering0);
            validator.Validate(xmlDocument);
        }

        [Test]
        public void SchematronValidateNemKontoKvittering1()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_NemKonto_Kvittering1);
            validator.Validate(xmlDocument);
        }

        [Test]
        public void SchematronValidateNemKontoPayment()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_NemKonto_Payment);
            validator.Validate(xmlDocument);
        }

        [Test]
        public void SchematronValidateNemKontoRetursvar2()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_NemKonto_Retursvar2);
            validator.Validate(xmlDocument);
        }

   

        [Test]
        public void SchematronValidateNemKontoRetursvar5()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_NemKonto_Retursvar5);
            validator.Validate(xmlDocument);
        }

        [Test]
        public void SchematronValidateNemKontoRetursvar7()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_NemKonto_Retursvar7);
            validator.Validate(xmlDocument);
        }

        [Test]
        public void SchematronValidateNemKontoRetursvar8()
        {
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_NemKonto_Retursvar8);
            validator.Validate(xmlDocument);
        }

        [Test]
        public void SchematronValidateNemKontoRetursvar9()
        {
            SchematronValidatorWithLookup validator1 = new SchematronValidatorWithLookup();
            string xmlDocument = File.ReadAllText(TestConstants.PATH_NemKonto_Retursvar9);
            validator1.Validate(xmlDocument);
        }
    }
}
