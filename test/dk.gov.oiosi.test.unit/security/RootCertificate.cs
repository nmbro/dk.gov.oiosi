using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.ocsp;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace dk.gov.oiosi.test.unit.security.revocation {

    [TestFixture]
    public class RootCertificate 
    {
        [Test]
        public void CertificateTestRootOces1()
        {
            StoreName storeName = StoreName.Root;
            StoreLocation storeLocation = StoreLocation.LocalMachine;
            X509Store certStore = new X509Store(storeName, storeLocation);
           
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection collection = certStore.Certificates.Find(X509FindType.FindBySerialNumber, TestConstants.SERIAL_CERTIFICATE_TEST_ROOT_OCES1, true);
            certStore.Close();

            //Assert.AreEqual(1, collection.Count, "The root certificate is not installed.");
            
        }

        [Test]
        public void CertificateTestRootOces2()
        {
            StoreName storeName = StoreName.Root;
            StoreLocation storeLocation = StoreLocation.LocalMachine;
            X509Store certStore = new X509Store(storeName, storeLocation);
            
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection collection = certStore.Certificates.Find(X509FindType.FindBySerialNumber, TestConstants.SERIAL_CERTIFICATE_TEST_ROOT_OCES2, true);
            certStore.Close();

            //Assert.AreEqual(1, collection.Count, "The root certificate is not installed.");
            
        }

        [Test]
        public void CertificateProdRootOces1()
        {
            StoreName storeName = StoreName.Root;
            StoreLocation storeLocation = StoreLocation.LocalMachine;
            X509Store certStore = new X509Store(storeName, storeLocation);
            
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection collection = certStore.Certificates.Find(X509FindType.FindBySerialNumber, TestConstants.SERIAL_CERTIFICATE_PROD_ROOT_OCES1, true);
            certStore.Close();

            Assert.AreEqual(1, collection.Count, "The root certificate is not installed.");
            
        }

        [Test]
        public void CertificateProdRootOces2()
        {
            StoreName storeName = StoreName.Root;
            StoreLocation storeLocation = StoreLocation.LocalMachine;

            X509Store certStore = new X509Store(storeName, storeLocation);
           
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection collection = certStore.Certificates.Find(X509FindType.FindBySerialNumber, TestConstants.SERIAL_CERTIFICATE_PROD_ROOT_OCES2, true);
            certStore.Close();

           // Assert.AreEqual(1, collection.Count, "The root certificate is not installed.");
            
        }
    }
}