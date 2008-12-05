using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using dk.gov.oiosi.security.ocsp;
using System.Security.Cryptography.X509Certificates;

namespace dk.gov.oiosi.integration.security.ocsp {

    [TestFixture]
    public class OcspLookupTest {

        [Test]
        public void LookupTestWithoutServerURI() {
            OcspConfig ocspConfig = new OcspConfig();
            ocspConfig.DefaultTimeoutMsec = 10000;
            X509Certificate2 rootcert = new X509Certificate2("resources/ocesca.cer");
            OcspLookup ocspLookup = new OcspLookup(ocspConfig, rootcert);
            X509Certificate2 certificate = new X509Certificate2("resources/NemHandel test service.cer");
            OcspResponse response = ocspLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }
    }
}