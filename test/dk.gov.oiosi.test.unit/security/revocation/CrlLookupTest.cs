using System;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.crl;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;

namespace dk.gov.oiosi.test.unit.security.revocation
{
    [TestFixture]
    public class CrlLookupTest
    {
        private const string foces1RevokedCertificat = "Resources/Certificates/CVR30808460.Expire20131101.FOCES2(revoked).pfx";
        private const string foces1ExpiredCertificat = "Resources/Certificates/CVR30808460.Expire20111016.FOCES1.pfx";
        private const string foces1OkayCertificat = "Resources/Certificates/CVR30808460.Expire20131101.FOCES1.pfx";

        private const string medarbejdercertifikatRevoked = "Resources/Certificates/CVR30808460.Expire20130307.Test MOCES1 (medarbejdercertificat 2)(Spærret).pfx";

        private const string foces2RevokedCertificate = "Resources/Certificates/CVR30808460.Expire20151025.TU GENEREL FOCES2 (Spærret) (Funktionscertifikat).pfx";
        private const string foces2ExpiredCertificate = "Resources/Certificates/CVR30808460.Expire20111105.TU GENEREL FOCES2 (Udløbet) (Funktionscertifikat).pfx";
        private const string foces2OkayCertificate = "Resources/Certificates/CVR30808460.Expire20151026.TU GENEREL FOCES2 (Funktionscertifikat).pfx";

        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigurationUtil.SetupConfiguration();
        }

        [Test]
        public void LookupTestOkayFoces1()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2(foces1OkayCertificat, "Test1234");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public void LookupTestOkayFoces2()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2(foces2OkayCertificate, "Test1234");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public void LookupTestRevokedFoces1()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2(foces1RevokedCertificat, "Test1234");
            Assert.IsNotNull(certificate, "Test certificate was null.");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);            

            Assert.IsNull(response.Exception, "The lookup return an exception.");
            Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");
            
            Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
        }
        


        [Test]
        public void LookupTestRevokedFoces2()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2(foces2RevokedCertificate, "Test1234");
            Assert.IsNotNull(certificate, "Test certificate was null.");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);

            Assert.IsNull(response.Exception, "The lookup return an exception.");
            Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");

            Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
        }

        [Test]
        public void LookupTestExpiredFoces1()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2(foces2RevokedCertificate, "Test1234");
            Assert.IsNotNull(certificate, "Test certificate was null.");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);

            Assert.IsNull(response.Exception, "The lookup return an exception.");
            Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");

            Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
        }

        [Test]
        public void LookupTestRevokedMoces1()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2(medarbejdercertifikatRevoked, "Test1234");
            Assert.IsNotNull(certificate, "Test certificate was null.");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);

            Assert.IsNull(response.Exception, "The lookup return an exception.");
            Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");

            Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
        }

        [Test]
        public void LookupTestExpiredFoces2()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2(foces2RevokedCertificate, "Test1234");
            Assert.IsNotNull(certificate, "Test certificate was null.");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);

            Assert.IsNull(response.Exception, "The lookup return an exception.");
            Assert.AreEqual(RevocationCheckStatus.CertificateRevoked, response.RevocationCheckStatus, "The revokation validation did not parse all check");

            Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
        }

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

        private X509Certificate2 certificateFoces = new X509Certificate2(CrlLookupTest.foces1OkayCertificat, "Test1234");
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
