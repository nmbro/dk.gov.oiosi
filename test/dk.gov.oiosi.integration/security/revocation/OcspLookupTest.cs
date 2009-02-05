using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.ocsp;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;

namespace dk.gov.oiosi.test.integration.security.revocation {

    [TestFixture]
    public class OcspLookupTest {

        [Test]
        public void LookupTestWithoutOcspServerFromCertificate(){
            OcspConfig ocspConfig = new OcspConfig();
            ocspConfig.DefaultTimeoutMsec = 10000;
            X509Certificate2 rootcert = new X509Certificate2("resources/ocesca.cer");
            OcspLookup ocspLookup = new OcspLookup(ocspConfig, rootcert);
            X509Certificate2 certificate = new X509Certificate2("resources/NemHandel test service.cer");
            RevocationResponse response = ocspLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public void LookupTestWithoutOcspServerFromConfig() {
            OcspConfig ocspConfig = new OcspConfig();
            ocspConfig.DefaultTimeoutMsec = 10000;
            ocspConfig.ServerUrl = "http://ocsp.certifikat.dk/ocsp/status";
            X509Certificate2 rootcert = new X509Certificate2("resources/ocesca.cer");
            OcspLookup ocspLookup = new OcspLookup(ocspConfig, rootcert);
            X509Certificate2 certificate = new X509Certificate2("resources/NemHandel test service.cer");
            RevocationResponse response = ocspLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }
    }
}