using System;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.crl;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using dk.gov.oiosi.security.revocation.ocsp;

namespace dk.gov.oiosi.test.unit.security.revocation
{
    [TestFixture]
    public class CrlLookupTest : LookupTest
    {        
        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigurationUtil.SetupConfiguration();
        }

        [Test]
        public void LookupTestOkayFoces1()
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(LookupTest.foces1OkayCertificate, "Test1234");

                CrlLookup crlLookup = new CrlLookup();
                RevocationResponse response = crlLookup.CheckCertificate(certificate);
                Assert.IsTrue(response.IsValid);
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
                X509Certificate2 certificate = new X509Certificate2(LookupTest.foces1RevokedCertificate, "Test1234");
                Assert.IsNotNull(certificate, "Test certificate was null.");

                CrlLookup crlLookup = new CrlLookup();
                RevocationResponse response = crlLookup.CheckCertificate(certificate);
                Assert.IsFalse(response.IsValid);
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "Not all check was performed.");

                /*OcspLookup ocspLookup = new OcspLookup();
                RevocationResponse ocspResponse = ocspLookup.CheckCertificate(certificate);
                Assert.IsFalse(ocspResponse.IsValid);*/
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /*
         * Not the CRL job to check for expired certificate
         * CRL check only check if the certificate has been revoked - is has not - it does not exist in the CRL list
         * because it is very old and expired.
        [Test]
        public void LookupTestExpiredFoces1()
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(LookupTest.foces1ExpiredCertificate, "Test1234");
                Assert.IsNotNull(certificate, "Test certificate was null.");

                CrlLookup crlLookup = new CrlLookup();
                RevocationResponse response = crlLookup.CheckCertificate(certificate);
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");
                Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
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

                CrlLookup crlLookup = new CrlLookup();                
                RevocationResponse response = crlLookup.CheckCertificate(certificate);
                Assert.IsTrue(response.IsValid);
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

                CrlLookup crlLookup = new CrlLookup();
                RevocationResponse response = crlLookup.CheckCertificate(certificate);
                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");
                Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /*[Test]
        public void LookupTestRevokedMoces1()
        {
            try
            {
                CrlLookup crlLookup = new CrlLookup();
                X509Certificate2 certificate = new X509Certificate2(medarbejdercertifikatRevoked, "Test1234");
                Assert.IsNotNull(certificate, "Test certificate was null.");
                RevocationResponse response = crlLookup.CheckCertificate(certificate);

                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");

                Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }*/

        /*
         * Not the CRL job to check for expired certificate
         * CRL check only check if the certificate has been revoked - is has not - it does not exist in the CRL list
         * because it is very old and expired.
         * [Test]
        public void LookupTestExpiredFoces2()
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(LookupTest.foces2ExpiredCertificate, "Test1234");
                Assert.IsNotNull(certificate, "Test certificate was null.");

                CrlLookup crlLookup = new CrlLookup();                
                Assert.IsNotNull(certificate, "Test certificate was null.");
                RevocationResponse response = crlLookup.CheckCertificate(certificate);

                Assert.IsNull(response.Exception, "The lookup return an exception.");
                Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");

                Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }
        }*/

        /*
         * 
         * This test fail on the build server - it does not failed when it is run local
         * 
        [Test]
        public void LookTestMultiThread()
        {
            List<Thread> threads = new List<Thread>();
            CrlLookup crlLookup = new CrlLookup();
            for (int i = 0; i < 32; i++)
            {
                Thread thread = new Thread(ThreadCertificateCheck);
                thread.Start();
                threads.Add(thread);
            }

            Predicate<Thread> isDead = delegate(Thread t) { return !t.IsAlive; };

            while (!threads.TrueForAll(isDead))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }*/

        private X509Certificate2 certificateFoces = new X509Certificate2(CrlLookupTest.foces1OkayCertificate, "Test1234");
        private X509Certificate2 certificateMoces = new X509Certificate2(CrlLookupTest.medarbejdercertifikatRevoked, "Test1234");

        private void ThreadCertificateCheck()
        {
            CrlLookup crlLookup = new CrlLookup();
            for (int i = 0; i < 16; i++)
            {
                Random random = new Random();
                int select = random.Next(2);
                if (select < 1)
                {
                    Console.WriteLine("{0} ThreadCertificateCheck number:{1} foces1 allOkay", DateTime.Now, i);

                    RevocationResponse response = crlLookup.CheckCertificate(certificateFoces);
                    if (response.Exception != null)
                    {
                        Console.WriteLine("{0} ThreadCertificateCheck number:{1} foces1 Exception: " + response.Exception.ToString(), DateTime.Now, i);
                    }

                    if (response.IsValid)
                    {
                        // yes - certificate is valid
                        if (response.RevocationCheckStatus == RevocationCheckStatus.AllChecksPassed)
                        {
                            // yes - check all parsed
                        }
                        else
                        {
                            Console.WriteLine("{0} ThreadCertificateCheck number:{1} foces1 not all checked parsed ", DateTime.Now, i);
                        }
                    }
                    else
                    {
                        // arg - certificate is not valid
                        Assert.IsTrue(response.IsValid, "Foces certifiate should have been valid.");
                        Console.WriteLine("{0} ThreadCertificateCheck number:{1} foces1 not all checked parsed ", DateTime.Now, i);
                    }
                }
                else
                {
                    // moces1
                    Console.WriteLine("{0} ThreadCertificateCheck number:{1} moces1 revoked", DateTime.Now, i);

                    RevocationResponse response = crlLookup.CheckCertificate(certificateMoces);
                    if (response.Exception != null)
                    {
                        Console.WriteLine("{0} ThreadCertificateCheck number:{1} foces1 Exception: " + response.Exception.ToString(), DateTime.Now, i);
                    }

                    if (!response.IsValid)
                    {
                        // yes - certificate is revoked
                        if (response.RevocationCheckStatus == RevocationCheckStatus.CertificateRevoked)
                        {
                            // yes - check all parsed
                        }
                        else
                        {
                            Console.WriteLine("{0} ThreadCertificateCheck number:{1} moces1 RevocationCheckStatus is not revoked as expected.", DateTime.Now, i);
                        }
                    }
                    else
                    {
                        // arg - certificate is valid
                        Assert.IsFalse(response.IsValid, "Moces certificate should have been revoked.");
                        Console.WriteLine("{0} ThreadCertificateCheck number:{1} moces1 certificate is not revoked as expected.", DateTime.Now, i);

                    }
                }

                Console.WriteLine("{0} ThreadCertificateCheck number:{1} done", DateTime.Now, i);
            }
        }
    }
}
