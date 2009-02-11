using System.Security.Cryptography.X509Certificates;

namespace dk.gov.oiosi.test.integration {
    public class CertificateUtil
    {
        public static void EnsureCertCertificate(StoreName storeName, StoreLocation storeLocation,
                                                 string certFileName) {
            X509Certificate2 certificate = new X509Certificate2(certFileName);
            Install(storeName, storeLocation, certificate);
        }

        public static void EnsurePfxCertificate(StoreName storeName, StoreLocation storeLocation, 
                                                string certFileName, string certPassword) {
            X509Certificate2 certificate = new X509Certificate2(certFileName, certPassword,
                                                                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
            Install(storeName, storeLocation, certificate);
        }

        public static bool IsInstalled(StoreName storeName, StoreLocation storeLocation, X509Certificate2 certificate) {
            X509Store localMachineMyStore = new X509Store(storeName, storeLocation);
            X509CertificateCollection collection = localMachineMyStore.Certificates;
            return collection.Contains(certificate);
        }

        public static void Install(StoreName storeName, StoreLocation storeLocation, X509Certificate2 certificate) {
            X509Store localMachineMyStore = new X509Store(storeName, storeLocation);
            localMachineMyStore.Open(OpenFlags.ReadWrite);
            localMachineMyStore.Add(certificate);
            localMachineMyStore.Close();
        }
    }
}