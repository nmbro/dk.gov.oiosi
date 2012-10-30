using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.ocsp;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System;

namespace dk.gov.oiosi.test.unit.security.revocation
{

    [TestFixture]
    public class OcspLookupTest
    {
        private string foces1RevokedCertificate = "Resources/Certificates/CVR30808460.Expire20131101.FOCES2(revoked).pfx";
        private string foces1ExpiredCertificate = "Resources/Certificates/CVR30808460.Expire20111016.FOCES1.pfx";
        private string foces1OkayCertificate = "Resources/Certificates/CVR30808460.Expire20131101.FOCES1.pfx";

        private string medarbejdercertifikatRevoked = "Resources/Certificates/CVR30808460.Expire20130307.Test MOCES1 (medarbejdercertificat 2)(Spærret).pfx";

        private string foces2RevokedCertificate = "Resources/Certificates/CVR30808460.Expire20151025.TU GENEREL FOCES2 (Spærret) (Funktionscertifikat).pfx";
        private string foces2ExpiredCertificate = "Resources/Certificates/CVR30808460.Expire20111105.TU GENEREL FOCES2 (Udløbet) (Funktionscertifikat).pfx";
        private string foces2OkayCertificate = "Resources/Certificates/CVR30808460.Expire20151026.TU GENEREL FOCES2 (Funktionscertifikat).pfx";


        private string oces1RootCertificate = TestConstants.PATH_CERTIFICATE_ROOT1;
        private string oces2RootCertificate = TestConstants.PATH_CERTIFICATE_ROOT2;

        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigurationUtil.SetupConfiguration();
        }

        private OcspLookup CreateOcesLookup()
        {
            OcspConfig ocspConfig = new OcspConfig();
            ocspConfig.DefaultTimeoutMsec = 20000;
            X509Certificate2 oces1RootCertificate = new X509Certificate2(this.oces1RootCertificate);
            X509Certificate2 oces2RootCertificate = new X509Certificate2(this.oces2RootCertificate);

            IList<X509Certificate2> list = new List<X509Certificate2>();
            list.Add(oces1RootCertificate);
            list.Add(oces2RootCertificate);

            OcspLookup ocspLookup = new OcspLookup(ocspConfig, list);

            return ocspLookup;
        }

        [Test]
        public void LookupTestOkayFoces1()
        {
            try
            {
                OcspLookup ocspLookup = this.CreateOcesLookup();
                X509Certificate2 certificate = new X509Certificate2(this.foces1OkayCertificate, "Test1234");
                RevocationResponse response = ocspLookup.CheckCertificate(certificate);
                Assert.IsTrue(response.IsValid, "Certificate is not valid.");
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.AllChecksPassed, response.RevocationCheckStatus, "Not all check was performed.");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        [Test]
        public void LookupTestOkayFoces2()
        {
            try
            {
                OcspLookup ocspLookup = this.CreateOcesLookup();
                X509Certificate2 certificate = new X509Certificate2(this.foces2OkayCertificate, "Test1234");
                RevocationResponse response = ocspLookup.CheckCertificate(certificate);
                Assert.IsTrue(response.IsValid, "Certificate is not valid.");
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.AllChecksPassed, response.RevocationCheckStatus, "Not all check was performed.");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        [Test]
        public void LookupTestRevokedFoces1()
        {
            try
            {
                OcspLookup ocspLookup = this.CreateOcesLookup();
                X509Certificate2 certificate = new X509Certificate2(this.foces1RevokedCertificate, "Test1234");
                RevocationResponse response = ocspLookup.CheckCertificate(certificate);
                Assert.IsFalse(response.IsValid, "Certificate is not valid.");
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "Not all check was performed.");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        [Test]
        public void LookupTestRevokedFoces2()
        {
            try
            {
                OcspLookup ocspLookup = this.CreateOcesLookup();
                X509Certificate2 certificate = new X509Certificate2(this.foces2RevokedCertificate, "Test1234");
                RevocationResponse response = ocspLookup.CheckCertificate(certificate);
                Assert.IsFalse(response.IsValid, "Certificate is not valid.");
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "Not all check was performed.");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }


        [Test]
        public void LookupTestExpiredFoces1()
        {
            try
            {
                OcspLookup ocspLookup = this.CreateOcesLookup();
                X509Certificate2 certificate = new X509Certificate2(this.foces1ExpiredCertificate, "Test1234");
                RevocationResponse response = ocspLookup.CheckCertificate(certificate);
                Assert.IsFalse(response.IsValid, "Certificate is not valid.");
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "Not all check was performed.");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        [Test]
        public void LookupTestExpiredFoces2()
        {
            try
            {
                OcspLookup ocspLookup = this.CreateOcesLookup();
                X509Certificate2 certificate = new X509Certificate2(this.foces2ExpiredCertificate, "Test1234");
                RevocationResponse response = ocspLookup.CheckCertificate(certificate);
                Assert.IsFalse(response.IsValid, "Certificate is not valid.");
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "Not all check was performed.");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }
    }
}