using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.test.unit.security.oces {
    [TestFixture]
    public class OcesCertificateSubjectKeyTest {

        [Test]
        public void ConstructorValidValueTest() {
            string value = "PID";
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey(value);
            Assert.AreEqual(value, subjectKey.SubjectKeyString);
        }

        [Test]
        public void SetValidValueTest() {
            string value = "PID";
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey();
            subjectKey.SubjectKeyString = value;
            Assert.AreEqual(value, subjectKey.SubjectKeyString);
        }

        [Test, ExpectedException(typeof(NullOrEmptyArgumentException))]
        public void ConstructorNullValueTest() {
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey(null);
        }

        [Test, ExpectedException(typeof(NullOrEmptyArgumentException))]
        public void SetNullValueTest() {
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey();
            subjectKey.SubjectKeyString = null;
        }

        [Test, ExpectedException(typeof(NullOrEmptyArgumentException))]
        public void ConstructorEmptyValueTest() {
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey("");
        }

        [Test, ExpectedException(typeof(NullOrEmptyArgumentException))]
        public void SetEmptyValueTest() {
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey();
            subjectKey.SubjectKeyString = "";
        }

        [Test, ExpectedException(typeof(Exception))]
        public void ConstructorInvalidValueTest() {
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey(@"\w");
        }

        [Test, ExpectedException(typeof(Exception))]
        public void SetInvalidValueTest() {
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey();
            subjectKey.SubjectKeyString = @"\d";
        }
    }
}
