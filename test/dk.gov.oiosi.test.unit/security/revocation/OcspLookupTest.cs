using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.ocsp;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System;

namespace dk.gov.oiosi.test.unit.security.revocation
{

    [TestFixture]
    public class OcspLookupTest : LookupTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigurationUtil.SetupConfiguration();
        }

        private OcspLookup CreateOcesLookup()
        {
            OcspConfig ocspConfig = new OcspConfig();
            ocspConfig.DefaultTimeoutMsec = 20000;
            
            X509Certificate2 oces2RootCertificate = new X509Certificate2(LookupTest.oces2RootCertificate);

            IList<X509Certificate2> list = new List<X509Certificate2>();
            
            list.Add(oces2RootCertificate);

            OcspLookup ocspLookup = new OcspLookup(ocspConfig, list);

            return ocspLookup;
        }

        //[Test]
        //public void LookupTestOkayFoces1()
        //{
        //    try
        //    {
        //        X509Certificate2 certificate = new X509Certificate2(LookupTest.foces1OkayCertificate, "Test1234");
        //        Assert.IsNotNull(certificate, "Test certificate was null.");

        //        OcspLookup ocspLookup = this.CreateOcesLookup();                
        //        RevocationResponse response = ocspLookup.CheckCertificate(certificate);
        //        Assert.IsTrue(response.IsValid, "Certificate is not valid.");
        //        Assert.IsNull(response.Exception, "The lookup return an exception.");
        //        Assert.AreEqual(RevocationCheckStatus.AllChecksPassed, response.RevocationCheckStatus, "Not all check was performed.");
        //    }
        //    catch (Exception exception)
        //    {
        //        Assert.Fail(exception.ToString());
        //    }
        //}


        // Foces1 certificate is not used anymore - can not get a general foces1 that is revoked
        //[Test]
        //public void LookupTestRevokedFoces1()
        //{
        //    try
        //    {
        //        X509Certificate2 certificate = new X509Certificate2(LookupTest.foces1RevokedCertificate, "Test1234");
        //        Assert.IsNotNull(certificate, "Test certificate was null.");

        //        OcspLookup ocspLookup = this.CreateOcesLookup();                
        //        RevocationResponse response = ocspLookup.CheckCertificate(certificate);
        //        Assert.IsFalse(response.IsValid, "Certificate is not valid.");
        //        Assert.IsNull(response.Exception, "The lookup return an exception.");
        //        Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "Not all check was performed.");
        //    }
        //    catch (Exception exception)
        //    {
        //        Assert.Fail(exception.ToString());
        //    }
        //}

 
       /*
        * Not the OCSP job to check for expired certificate
        * [Test]
        public void LookupTestExpiredFoces1()
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(LookupTest.foces1ExpiredCertificate, "Test1234");
                Assert.IsNotNull(certificate, "Test certificate was null.");

                OcspLookup ocspLookup = this.CreateOcesLookup();                
                RevocationResponse response = ocspLookup.CheckCertificate(certificate);
                Assert.IsFalse(response.IsValid, "Certificate is not valid.");
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "Not all check was performed.");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }*/
        

        [Test]
        public void LookupTestOkayFoces2()
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(LookupTest.foces2OkayCertificate, "Test1234");
                Assert.IsNotNull(certificate, "Test certificate was null.");

                OcspLookup ocspLookup = this.CreateOcesLookup();
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
        public void LookupTestRevokedFoces2()
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(LookupTest.foces2RevokedCertificate, "Test1234");
                Assert.IsNotNull(certificate, "Test certificate was null.");

                OcspLookup ocspLookup = this.CreateOcesLookup();
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

        /*
         * Not the OCSP job to check for expired certificate
         * [Test]
        public void LookupTestExpiredFoces2()
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(LookupTest.foces2ExpiredCertificate, "Test1234");
                Assert.IsNotNull(certificate, "Test certificate was null.");

                OcspLookup ocspLookup = this.CreateOcesLookup();
                RevocationResponse response = ocspLookup.CheckCertificate(certificate);
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");
                Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }*/

        /* */

       /* [Test]
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
        }*/
    }
}