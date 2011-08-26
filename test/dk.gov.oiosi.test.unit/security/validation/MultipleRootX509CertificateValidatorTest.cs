using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using dk.gov.oiosi.security.validation;
using System.Security.Cryptography.X509Certificates;

namespace dk.gov.oiosi.test.unit.security.validation
{
    public class MultipleRootX509CertificateValidatorTest
    {

        [Test]
        public void _01_TestWithOneRootCertificate() {
            var rootCertificate = new X509Certificate2(@"Resources\Certificates\tdc_systemtest_2.cer");
            var functionCertificate = new X509Certificate2(@"Resources\Certificates\FOCES1.pkcs12", "Test1234");
            var rootCertificates = new X509Certificate2[] { rootCertificate };
            var validator = new MultipleRootX509CertificateValidator(rootCertificates);
            validator.Validate(functionCertificate);
        }

        //TODO: more tests here


    }
}
