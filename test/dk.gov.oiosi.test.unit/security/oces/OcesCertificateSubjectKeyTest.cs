using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.test.unit.security.oces
{
    [TestFixture]
    public class OcesCertificateSubjectKeyTest
    {
        [Test]
        public void ConstructorValidValueTest()
        {
            string value = "PID";
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey(value);
            Assert.AreEqual(value, subjectKey.SubjectKeyString);
        }

        [Test]
        public void SetValidValueTest()
        {
            string value = "PID";
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey();
            subjectKey.SubjectKeyString = value;
            Assert.AreEqual(value, subjectKey.SubjectKeyString);
        }

        [Test]
        public void ConstructorNullValueTest()
        {
            Assert.Throws<NullOrEmptyArgumentException>(() => new OcesCertificateSubjectKey(null));
        }

        [Test]
        public void SetNullValueTest()
        {
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey();
            Assert.Throws<NullOrEmptyArgumentException>(() => subjectKey.SubjectKeyString = null);
        }

        [Test]
        public void ConstructorEmptyValueTest()
        {
            Assert.Throws<NullOrEmptyArgumentException>(() => new OcesCertificateSubjectKey(string.Empty));
        }

        [Test]
        public void SetEmptyValueTest()
        {
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey();
            Assert.Throws<NullOrEmptyArgumentException>(() => subjectKey.SubjectKeyString = string.Empty);
        }

        [Test]
        public void ConstructorInvalidValueTest()
        {
            Assert.Throws<Exception>(() => new OcesCertificateSubjectKey(@"\w"));
        }

        [Test]
        public void SetInvalidValueTest()
        {
            OcesCertificateSubjectKey subjectKey = new OcesCertificateSubjectKey();
            Assert.Throws<Exception>(() => subjectKey.SubjectKeyString = @"\d");
        }
    }
}
