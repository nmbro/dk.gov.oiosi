using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron;
using System.Xml;

namespace dk.gov.oiosi.test.nunit.library.extension.wcf.Interceptor.Validation.Schematron {

    [TestFixture]
    public class SchematronValidatorWithLookupTest {

        [Test]
        public void SchematronValidateTwentyTimesInvoice() {
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice start");
            SchematronValidatorWithLookup validator = new SchematronValidatorWithLookup();
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice first stylesheet start");
            XmlDocument document = new XmlDocument();
            document.Load(TestConstants.PATH_INVOICE_XML);
            validator.Validate(document);
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice first stylesheet end");
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice last stylesheets start");
            for (int i=0; i<20; i++) {
                validator = new SchematronValidatorWithLookup();
                document = new XmlDocument();
                document.Load(TestConstants.PATH_INVOICE_XML);
                validator.Validate(document);
            }
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice last stylesheets end");
            Console.WriteLine(DateTime.Now + " SchematronValidateTwentyTimesInvoice end");
        }

        [Test]
        public void SchematronValidateClientSimulation() {
            SchematronValidatorWithLookup validator1 = new SchematronValidatorWithLookup();
            XmlDocument document1 = new XmlDocument();
            document1.Load(TestConstants.PATH_INVOICE_XML);
            validator1.Validate(document1);

            SchematronValidatorWithLookup validator2 = new SchematronValidatorWithLookup();
            XmlDocument document2 = new XmlDocument();
            document2.Load(TestConstants.PATH_INVOICE_XML);
            validator2.Validate(document2);

        }

    }
}
