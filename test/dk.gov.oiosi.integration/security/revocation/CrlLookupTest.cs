using System;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.crl;
using NUnit.Framework;

namespace dk.gov.oiosi.test.integration.security.revocation
{
    [TestFixture]
    public class CrlLookupTest
    {

        [Test]
        public void LookupTestValidCertificate()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2("Resources/Certificates/Testendpoint (funktionscertifikat) (40 36 d8 5e).pfx", "Test1234");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public void LookupTestRevokedCertificate()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2("Resources/Certificates/Revoked.cer");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);
            Assert.IsFalse(response.IsValid);
        }
    }
}
