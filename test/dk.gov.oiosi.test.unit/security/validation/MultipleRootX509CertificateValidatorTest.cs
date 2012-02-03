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
        public void _01_TestWithOneRootCertificate() 
        {
            X509Certificate2 rootCertificate1 = new X509Certificate2(TestConstants.PATH_CERTIFICATE_ROOT);
            X509Certificate2 rootCertificate2 = new X509Certificate2(TestConstants.PATH_CERTIFICATE_ROOT);
            X509Certificate2 functionCertificate = new X509Certificate2(TestConstants.PATH_CERTIFICATE_DEVICE, TestConstants.PASSWORD_CERTIFICATE_DEVICE);

            X509Certificate2[] rootCertificates = new X509Certificate2[] { rootCertificate1, rootCertificate2 };
            MultipleRootX509CertificateValidator validator = new MultipleRootX509CertificateValidator(rootCertificates);
            
            validator.Validate(functionCertificate);
        }

        //TODO: more tests here


    }
}
