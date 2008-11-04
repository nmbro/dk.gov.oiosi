using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.test.nunit.library.security.oces {
    [TestFixture]
    public class OcesX509CertificateTest {

        [TestFixtureSetUp]
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
            string employeeCertificatePath = TestConstants.PATH_CERTIFICATE_EMPLOYEE;
            X509Certificate2 certificate = new X509Certificate2(employeeCertificatePath);
            OcesX509Certificate ocesCertificate = new OcesX509Certificate(certificate);
            Assert.AreEqual(OcesCertificateType.OcesEmployee, ocesCertificate.OcesCertificateType);
            Assert.IsFalse(ocesCertificate.HasPrivateKey());
        }

        [Test]
        public void OrganisationTypeTest() {
            string organisationCertificatePath = TestConstants.PATH_CERTIFICATE_ORGANISATION;
            X509Certificate2 certificate = new X509Certificate2(organisationCertificatePath);
            OcesX509Certificate ocesCertificate = new OcesX509Certificate(certificate);
            Assert.AreEqual(OcesCertificateType.OcesOrganisation, ocesCertificate.OcesCertificateType);
            Assert.IsFalse(ocesCertificate.HasPrivateKey());
        }

        [Test]
        public void DeviceTypeTest() {
            string deviceCertificatePath = TestConstants.PATH_CERTIFICATE_DEVICE;
            X509Certificate2 certificate = new X509Certificate2(deviceCertificatePath);
            OcesX509Certificate ocesCertificate = new OcesX509Certificate(certificate);
            Assert.AreEqual(OcesCertificateType.OcesFunction, ocesCertificate.OcesCertificateType);
            Assert.IsFalse(ocesCertificate.HasPrivateKey());
        }
    }
}
