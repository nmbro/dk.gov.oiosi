using NUnit.Framework;

using dk.gov.oiosi.security;

namespace dk.gov.oiosi.test.unit.security {
    [TestFixture]
    public class CertificateSubjectTest {

        [Test]
        public void _01_CertificateSubjectWithParantheses() {
            const string certificateSubjectString = "OID.2.5.4.5=CVR:14472800-FID:1201516183216 + CN=Scan-Med NEM-Handel (funktionscertifikat), O=SCAN-MED. A/S. DENMARK // CVR:14472800, C=DK";
            CertificateSubject subject = new CertificateSubject(certificateSubjectString);

            Assert.AreEqual("DK", subject.C);
            Assert.AreEqual("Scan-Med NEM-Handel (funktionscertifikat)", subject.CN);
            Assert.AreEqual("SCAN-MED. A/S. DENMARK // CVR:14472800", subject.O);
            Assert.AreEqual("serialNumber=CVR:14472800-FID:1201516183216", subject.SerialNumber);
        }

        [Test]
        public void _02_CertificateSubjectWithDots() {
            const string certificateSubjectString = "OID.2.5.4.5=CVR:14472800-FID:1201516183216 + CN=Scan-Med NEM-Handel .net, O=SCAN-MED. A/S. DENMARK // CVR:14472800, C=DK";
            CertificateSubject subject = new CertificateSubject(certificateSubjectString);

            Assert.AreEqual("DK", subject.C);
            Assert.AreEqual("Scan-Med NEM-Handel .net", subject.CN);
            Assert.AreEqual("SCAN-MED. A/S. DENMARK // CVR:14472800", subject.O);
            Assert.AreEqual("serialNumber=CVR:14472800-FID:1201516183216", subject.SerialNumber);
        }

        [Test]
        public void _03_SpecificCertificateProblem() {
            const string certificateSubjectString = "SERIALNUMBER=CVR:82269118-FID:1225461072402 + CN=Navision Stat (funktionscertifikat), O=Dansk Landbrugsmusuem Gl. Estrup // CVR:82269118, C=DK";
            CertificateSubject subject = new CertificateSubject(certificateSubjectString);

            Assert.AreEqual("DK", subject.C);
            Assert.AreEqual("Navision Stat (funktionscertifikat)", subject.CN);
            Assert.AreEqual("Dansk Landbrugsmusuem Gl. Estrup // CVR:82269118", subject.O);
            Assert.AreEqual("serialNumber=CVR:82269118-FID:1225461072402", subject.SerialNumber);
        }
    }
}
