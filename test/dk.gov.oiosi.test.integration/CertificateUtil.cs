using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.test.integration
{
    public class CertificateUtil
    {
        public static X509Certificate2 InstallAndGetFunctionCertificateFromCertificateStore()
        {
            const string certificateSerialNumber = "40 37 fb 49";

            string certificateFile = "Resources/Certificates/CVR30808460.Expire20131101.FOCES1.pfx";
            string rootCertificateFile = "Resources/Certificates/TDC OCES Systemtest CA II.cer";
            string certificatePassword = "Test1234";
            CertificateUtil.EnsurePfxCertificate(StoreName.My, StoreLocation.CurrentUser, certificateFile, certificatePassword);
            CertificateUtil.EnsureCerCertificate(StoreName.Root, StoreLocation.CurrentUser, rootCertificateFile);
            CertificateStoreIdentification sendCertificateLocation = new CertificateStoreIdentification(StoreLocation.CurrentUser, StoreName.My, certificateSerialNumber);
            CertificateLoader certificateLoader = new CertificateLoader();
            X509Certificate2 certificate = certificateLoader.GetCertificateFromCertificateStoreInformation(sendCertificateLocation);
            return certificate;
        }


        public static void EnsureCertCertificate(StoreName storeName, StoreLocation storeLocation, string certFileName)
        {
            X509Certificate2 certificate = new X509Certificate2(certFileName);
            CertificateUtil.Install(storeName, storeLocation, certificate);
        }

        public static void EnsurePfxCertificate(StoreName storeName, StoreLocation storeLocation,
                                                string certFileName, string certPassword)
        {
            X509Certificate2 certificate = new X509Certificate2(certFileName, certPassword,
                                                                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
            if (IsInstalled(storeName, storeLocation, certificate))
            {
                // already installed
            }
            else
            {
                CertificateUtil.Install(storeName, storeLocation, certificate);
            }
        }

        public static void EnsureCerCertificate(StoreName storeName, StoreLocation storeLocation, string certFileName)
        {
            X509Certificate2 certificate = new X509Certificate2(certFileName);
            if (IsInstalled(storeName, storeLocation, certificate))
            {
                // already installed
            }
            else
            {
                CertificateUtil.Install(storeName, storeLocation, certificate);
            }
        }

        public static bool IsInstalled(StoreName storeName, StoreLocation storeLocation, X509Certificate2 certificate)
        {
            X509Store localMachineMyStore = new X509Store(storeName, storeLocation);
            X509CertificateCollection collection = localMachineMyStore.Certificates;
            return collection.Contains(certificate);
        }

        public static void Install(StoreName storeName, StoreLocation storeLocation, X509Certificate2 certificate)
        {
            X509Store localMachineMyStore = new X509Store(storeName, storeLocation);
            localMachineMyStore.Open(OpenFlags.ReadWrite);
            localMachineMyStore.Add(certificate);
            localMachineMyStore.Close();
        }
    }
}