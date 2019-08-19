using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.test.unit.security.oces {
    [TestFixture]
    public class OcesX509CertificateTest {

        [OneTimeSetUp]
        public void SetOcesConfiguration() {
            try {
                DefaultOcesCertificate ocesCertificates = new DefaultOcesCertificate();
                ocesCertificates.SetTestOcesCertificateConfig();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        [Test]
        public void EmployeeTypeTest() {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfigurationOcesX509.xml";
            string employeeCertificatePath = TestConstants.PATH_CERTIFICATE_EMPLOYEE;
            X509Certificate2 certificate = new X509Certificate2(employeeCertificatePath, TestConstants.PASSWORD_CERTIFICATE_EMPLOYEE);
            OcesX509Certificate ocesCertificate = new OcesX509Certificate(certificate);
            Assert.AreEqual(OcesCertificateType.OcesEmployee, ocesCertificate.OcesCertificateType);
            Assert.IsTrue(ocesCertificate.HasPrivateKey());
        }

       /*
        * We don't have a valid organisation certificate - and it is not used in RASP
        * [Test]
        public void OrganisationTypeTest() {
            string organisationCertificatePath = TestConstants.PATH_CERTIFICATE_ORGANISATION;
            X509Certificate2 certificate = new X509Certificate2(organisationCertificatePath);
            OcesX509Certificate ocesCertificate = new OcesX509Certificate(certificate);
            Assert.AreEqual(OcesCertificateType.OcesOrganisation, ocesCertificate.OcesCertificateType);
            Assert.IsFalse(ocesCertificate.HasPrivateKey());
        }*/

        [Test]
        public void DeviceTypeTest() {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfigurationOcesX509.xml";
            string deviceCertificatePath = TestConstants.PATH_CERTIFICATE_DEVICE;
            X509Certificate2 certificate = new X509Certificate2(deviceCertificatePath, TestConstants.PASSWORD_CERTIFICATE_DEVICE);
            OcesX509Certificate ocesCertificate = new OcesX509Certificate(certificate);
            Assert.AreEqual(OcesCertificateType.OcesFunction, ocesCertificate.OcesCertificateType);
            Assert.IsTrue(ocesCertificate.HasPrivateKey());
        }
    }
}
