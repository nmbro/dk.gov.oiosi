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
        private string funkttionscertificat = "Resources/Certificates/CVR30808460.Expire20131101.FOCES1.pfx";
        private string medarbejdercertifikatRevoked = "Resources/Certificates/CVR30808460.Expire20130307.Test MOCES1 (medarbejdercertificat 2)(Spærret).pfx";

        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigurationUtil.SetupConfiguration();
        }

        [Test]
        public void LookupTestValidCertificate()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2(funkttionscertificat, "Test1234");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);
            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public void LookupTestRevokedCertificate()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2(medarbejdercertifikatRevoked, "Test1234");
            Assert.IsNotNull(certificate, "Test certificate was null.");
            RevocationResponse response = crlLookup.CheckCertificate(certificate);
            
            Assert.IsNull(response.Exception, "The lookup return an exception.");
            Assert.AreSame(response.RevocationCheckStatus, RevocationCheckStatus.AllChecksPassed, "The revokation validation did not parse all check");
            
            Assert.IsFalse(response.IsValid, "The revoked certifikate was valid");
        }

      /*  static void Main(string[] args)
        {
            new CrlLookupTest().ffFF();

        }

        public void ffFF()
        {
            Setup();            
            X509Certificate2 certificate = new X509Certificate2(medarbejdercertifikatRevoked, "Test1234");
            CrlLookup crlLookup = new CrlLookup();
            RevocationResponse response = crlLookup.CheckCertificate(certificate);
        }*/

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
        }

        private void ThreadCertificateCheck()
        {
            CrlLookup crlLookup = new CrlLookup();
            for (int i = 0; i < 16; i++)
            {
                Random random = new Random();
                int select = random.Next(2);
                if (select < 1)
                {
                    Console.WriteLine("{0} ThreadCertificateCheck number:{1} certificate:0", DateTime.Now, i);
                    X509Certificate2 certificate = new X509Certificate2(funkttionscertificat, "Test1234");
                    RevocationResponse response = crlLookup.CheckCertificate(certificate);
                    Assert.IsTrue(response.IsValid);
                }
                else
                {
                    Console.WriteLine("{0} ThreadCertificateCheck number:{1} certificate:1", DateTime.Now, i);
                    X509Certificate2 certificate = new X509Certificate2(medarbejdercertifikatRevoked, "Test1234");
                    RevocationResponse response = crlLookup.CheckCertificate(certificate);
                    Assert.IsFalse(response.IsValid);
                }
                Console.WriteLine("{0} ThreadCertificateCheck number:{1} done", DateTime.Now, i);
            }
        }
    }
}
