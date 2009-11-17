using System;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.crl;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;

namespace dk.gov.oiosi.test.integration.security.revocation
{
    [TestFixture]
    public class CrlLookupTest
    {

        [Test]
        public void LookupTestValidCertificate()
        {
            CrlLookup crlLookup = new CrlLookup();
            X509Certificate2 certificate = new X509Certificate2("Resources/Certificates/FOCES1.pkcs12", "Test1234");
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

        [Test]
        public void LookTestMultiThread() {
            List<Thread> threads = new List<Thread>();
            CrlLookup crlLookup = new CrlLookup();
            for (int i = 0; i < 32; i++) {
                Thread thread = new Thread(ThreadCertificateCheck);
                thread.Start();
                threads.Add(thread);
            }

            Predicate<Thread> isDead = delegate(Thread t) { return !t.IsAlive; };

            while (!threads.TrueForAll(isDead)) {
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        private void ThreadCertificateCheck() {
            CrlLookup crlLookup = new CrlLookup();
            for (int i = 0; i < 16; i++) {
                Random random = new Random();
                int select = random.Next(2);
                if (select < 1) {
                    Console.WriteLine("{0} ThreadCertificateCheck number:{1} certificate:0", DateTime.Now, i);
                    X509Certificate2 certificate = new X509Certificate2("Resources/Certificates/FOCES1.pkcs12", "Test1234");
                    RevocationResponse response = crlLookup.CheckCertificate(certificate);
                    Assert.IsTrue(response.IsValid);
                }
                else {
                    Console.WriteLine("{0} ThreadCertificateCheck number:{1} certificate:1", DateTime.Now, i);
                    X509Certificate2 certificate = new X509Certificate2("Resources/Certificates/Revoked.cer");
                    RevocationResponse response = crlLookup.CheckCertificate(certificate);
                    Assert.IsFalse(response.IsValid);
                }
                Console.WriteLine("{0} ThreadCertificateCheck number:{1} done", DateTime.Now, i);
            }
        }
    }
}
