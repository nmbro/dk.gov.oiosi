using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.ocsp;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace dk.gov.oiosi.test.integration.security.revocation {

    [TestFixture]
    public class OcspLookupTest 
    {
        private string rootCertificate = "Resources/Certificates/TDC OCES CA.cer";
        private string funtioncertifikat = "Resources/Certificates/CVR26769388.Expire20100115.NemHandel test service.cer";

        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigurationUtil.SetupConfiguration();
        }

        [Test]
        public void LookupTestWithoutOcspServerFromCertificate(){
            OcspConfig ocspConfig = new OcspConfig();
            ocspConfig.DefaultTimeoutMsec = 10000;
            X509Certificate2 rootcert = new X509Certificate2(rootCertificate);
            IList<X509Certificate2> list = new List<X509Certificate2>();
            list.Add(rootcert);
            OcspLookup ocspLookup = new OcspLookup(ocspConfig, list);
            X509Certificate2 certificate = new X509Certificate2(funtioncertifikat, "Test1234");
            RevocationResponse response = ocspLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public void LookupTestWithoutOcspServerFromConfig() {
            OcspConfig ocspConfig = new OcspConfig();
            ocspConfig.DefaultTimeoutMsec = 10000;
            ocspConfig.ServerUrl = "http://ocsp.certifikat.dk/ocsp/status";
            X509Certificate2 rootcert = new X509Certificate2(rootCertificate);
            IList<X509Certificate2> list = new List<X509Certificate2>();
            list.Add(rootcert);
            OcspLookup ocspLookup = new OcspLookup(ocspConfig, list);
            X509Certificate2 certificate = new X509Certificate2(funtioncertifikat, "Test1234");
            RevocationResponse response = ocspLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }
    }
}