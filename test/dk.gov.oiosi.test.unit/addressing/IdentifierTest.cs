using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.exception;
using NUnit.Framework;

namespace dk.gov.oiosi.addressing
{
    [TestFixture]
    public class IdentifierTest
    {
        [Test]
        public void ConstructorTest1()
        {
            Identifier identifierGLN = new Identifier("GLN", "123456790123");
            Identifier identifierEAN = new Identifier("EAN", "123456790123");
            Identifier identifierDKCVR = new Identifier("DK:CVR", "123456790123");
        }

        [Test]
        [ExpectedException(typeof(NullOrEmptyArgumentException))]
        public void ConstructorTest2()
        {
            Identifier identifier = new Identifier("GLN", "");
        }

        [Test]
        [ExpectedException(typeof(NullOrEmptyArgumentException))]
        public void ConstructorTest3()
        {
            Identifier identifier = new Identifier("", "123456790123");
        }

        [Test]
        [ExpectedException(typeof(IncorrectBusinessIdentifierException))]
        public void ConstructorTest4()
        {
            Identifier identifier = new Identifier("GLN", "1 2");
        }

        [Test]
        public void IsAllowedInPublicTest()
        {
            Identifier identifierCpr = new Identifier("DK:CPR", "1111111118");
            Assert.IsFalse(identifierCpr.IsAllowedInPublic);
            Identifier identifierCvr = new Identifier("DK:CVR", "DK99010080");
            Assert.IsTrue(identifierCvr.IsAllowedInPublic);
        }
    }

}
