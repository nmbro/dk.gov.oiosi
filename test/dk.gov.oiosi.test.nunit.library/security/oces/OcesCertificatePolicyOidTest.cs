using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.test.nunit.library.security.oces {
    [TestFixture]
    public class OcesCertificatePolicyOidTest {
        [Test]
        public void EmployeeCertificatePolicyOidTest() {
            string oidString = "1.2.208.169.1.1.1.2.4";
            OcesCertificatePolicyOid oid = new OcesCertificatePolicyOid(oidString);
            Assert.AreEqual(oidString, oid.PolicyOidString);
        }

        [Test]
        public void OrganizationCertificatePolicyOidTest() {
            string oidString = "1.2.206.169.1.1.1.3.3";
            OcesCertificatePolicyOid oid = new OcesCertificatePolicyOid(oidString);
            Assert.AreEqual(oidString, oid.PolicyOidString);
        }

        [Test]
        public void PersonalCertificatePolicyOidTest() {
            string oidString = "1.2.208.169.1.1.1.1.1";
            OcesCertificatePolicyOid oid = new OcesCertificatePolicyOid(oidString);
            Assert.AreEqual(oidString, oid.PolicyOidString);
        }

        [Test]
        public void DeviceCertificatePolicyOidTest() {
            string oidString = "1.2.208.169.1.1.1.4.1";
            OcesCertificatePolicyOid oid = new OcesCertificatePolicyOid(oidString);
            Assert.AreEqual(oidString, oid.PolicyOidString);
        }

        [Test]
        public void LargeNumbersTest() {
            string oidString = "97823418907.17246789123.89127348.2347108.928134790.982713480.90821389037.123740.2347890";
            OcesCertificatePolicyOid oid = new OcesCertificatePolicyOid(oidString);
            Assert.AreEqual(oidString, oid.PolicyOidString);
        }

        [Test, ExpectedException(typeof(InvalidOcesCertificatePolicyOidException))]
        public void TooFewNumbersTest() {
            string oidString = "1.1.1.1.1.1.1.1";
            OcesCertificatePolicyOid oid = new OcesCertificatePolicyOid(oidString);
            Assert.AreEqual(oidString, oid.PolicyOidString);
        }

        [Test, ExpectedException(typeof(InvalidOcesCertificatePolicyOidException))]
        public void TooManyNumbersTest() {
            string oidString = "1.1.1.1.1.1.1.1.1.1.1";
            OcesCertificatePolicyOid oid = new OcesCertificatePolicyOid(oidString);
            Assert.AreEqual(oidString, oid.PolicyOidString);
        }

        [Test]
        public void NonVersionEqualsTest() {
            string oidStringv3 = "1.2.206.169.1.1.1.3.3";
            string oidStringv4 = "1.2.206.169.1.1.1.3.4";
            OcesCertificatePolicyOid oidv3 = new OcesCertificatePolicyOid(oidStringv3);
            OcesCertificatePolicyOid oidv4 = new OcesCertificatePolicyOid(oidStringv4);
            Assert.IsTrue(oidv3.NonVersionEquals(oidv4));
        }

        [Test]
        public void NonVersionNotEqualsTest() {
            string oidString3 = "1.2.206.169.1.1.1.3.3";
            string oidString4 = "1.2.206.169.1.1.1.2.4";
            OcesCertificatePolicyOid oid3 = new OcesCertificatePolicyOid(oidString3);
            OcesCertificatePolicyOid oid4 = new OcesCertificatePolicyOid(oidString4);
            Assert.IsFalse(oid3.NonVersionEquals(oid4));
        }

    }
}
