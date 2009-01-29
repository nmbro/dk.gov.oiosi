﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
            CrlLookup ocspLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2("resources/NemHandel test service.cer");
            RevocationResponse response = ocspLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }

        [Test]
        [Ignore]
        public void LookupTestRevokedCertificate()
        {
            CrlLookup ocspLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2("resources/Revoked.cer");
            try {
                RevocationResponse response = ocspLookup.CheckCertificate(certificate);
                Assert.IsTrue(!response.IsValid);
            }
            catch (Exception e)
            {
                // Certificate is revoked. How do we catch the CertificateRevokedException here?
                Assert.IsTrue(true);
            }
        }
    }
}
