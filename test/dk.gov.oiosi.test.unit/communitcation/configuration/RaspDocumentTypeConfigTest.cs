using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.xpath.discriminator;

namespace dk.gov.oiosi.test.nunit.library.communitcation.configuration {
    [TestFixture]
    public class RaspDocumentTypeConfigTest {

        [Test]
        public void _02_DocumentTypeEqualsOnIdTest() {
            Print.Started("_02_SimpleDocumentTypeEqualsOnIdTest");
            DocumentTypeConfig a = new DocumentTypeConfig();
            DocumentTypeConfig b = new DocumentTypeConfig();
            Guid id = Guid.NewGuid();
            a.Id = id;
            b.Id = id;
            a.RootName = "TestA";
            b.RootName = "TestB";
            Assert.IsTrue(a.Equals(b));
            Print.Completed("_02_SimpleDocumentTypeEqualsOnIdTest");
        }

        [Test]
        public void _03_DocumentTypeEqualsOnParameters() {
            Print.Started("_03_SimpleDocumentTypeEqualsOnParameters");
            string rootName = "Test";
            string rootNamespace = "http://oio.dk/test";
            XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
            XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
            XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
            identifierDiscriminators.Add(identifierDiscriminatorA);
            identifierDiscriminators.Add(identifierDiscriminatorB);
            DocumentTypeConfig a = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminators);
            DocumentTypeConfig b = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminators);
            Assert.IsTrue(a.Equals(b));
            Print.Completed("_03_SimpleDocumentTypeEqualsOnParameters");
        }

        [Test]
        public void _04_DocumentTypeEqualsOnParametersDifferentOrderingOfDiscriminators() {
            Print.Started("_04_DocumentTypeEqualsOnParametersDifferentOrderingOfDiscriminators");
            string rootName = "Test";
            string rootNamespace = "http://oio.dk/test";
            XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
            XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
            XpathDiscriminatorConfigCollection identifierDiscriminatorsA = new XpathDiscriminatorConfigCollection();
            identifierDiscriminatorsA.Add(identifierDiscriminatorA);
            identifierDiscriminatorsA.Add(identifierDiscriminatorB);
            XpathDiscriminatorConfigCollection identifierDiscriminatorsB = new XpathDiscriminatorConfigCollection();
            identifierDiscriminatorsB.Add(identifierDiscriminatorB);
            identifierDiscriminatorsB.Add(identifierDiscriminatorA);
            DocumentTypeConfig a = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminatorsA);
            DocumentTypeConfig b = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminatorsB);
            Assert.IsTrue(a.Equals(b));
            Print.Completed("_04_DocumentTypeEqualsOnParametersDifferentOrderingOfDiscriminators");
        }


        [Test]
        public void _05_DocumentTypeNotEqualRootNameAndId() {
            Print.Started("_05_DocumentTypeNotEqualRootNameAndId");
            string rootNameA = "TestA";
            string rootNameB = "TestB";
            string rootNamespace = "http://oio.dk/test";
            XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
            XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
            XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
            identifierDiscriminators.Add(identifierDiscriminatorA);
            identifierDiscriminators.Add(identifierDiscriminatorB);
            DocumentTypeConfig a = new DocumentTypeConfig(rootNameA, rootNamespace, identifierDiscriminators);
            DocumentTypeConfig b = new DocumentTypeConfig(rootNameB, rootNamespace, identifierDiscriminators);
            Assert.IsFalse(a.Equals(b));
            Print.Completed("_05_DocumentTypeNotEqualRootNameAndId");
        }

        [Test]
        public void _06_DocumentTypeNotEqualRootNamespaceAndId() {
            Print.Started("_06_DocumentTypeNotEqualRootNamespaceAndId");
            string rootName = "Test";
            string rootNamespaceA = "http://oio.dk/testA";
            string rootNamespaceB = "http://oio.dk/testB";
            XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
            XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
            XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
            identifierDiscriminators.Add(identifierDiscriminatorA);
            identifierDiscriminators.Add(identifierDiscriminatorB);
            DocumentTypeConfig a = new DocumentTypeConfig(rootName, rootNamespaceA, identifierDiscriminators);
            DocumentTypeConfig b = new DocumentTypeConfig(rootName, rootNamespaceB, identifierDiscriminators);
            Assert.IsFalse(a.Equals(b));
            Print.Completed("_06_DocumentTypeNotEqualRootNamespaceAndId");
        }

        [Test]
        public void _07_DocumentTypeNotEqualIndentifierDiscriminatorsAndId() {
            Print.Started("_07_DocumentTypeNotEqualIndentifierDiscriminatorsAndId");
            string rootName = "Test";
            string rootNamespace = "http://oio.dk/test";
            XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
            XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
            XPathDiscriminatorConfig identifierDiscriminatorC = new XPathDiscriminatorConfig("//Test/Key", "C");
            XpathDiscriminatorConfigCollection identifierDiscriminatorsA = new XpathDiscriminatorConfigCollection();
            identifierDiscriminatorsA.Add(identifierDiscriminatorA);
            identifierDiscriminatorsA.Add(identifierDiscriminatorB);
            XpathDiscriminatorConfigCollection identifierDiscriminatorsB = new XpathDiscriminatorConfigCollection();
            identifierDiscriminatorsB.Add(identifierDiscriminatorB);
            identifierDiscriminatorsB.Add(identifierDiscriminatorC);
            DocumentTypeConfig a = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminatorsA);
            DocumentTypeConfig b = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminatorsB);
            Assert.IsFalse(a.Equals(b));
            Print.Completed("_07_DocumentTypeNotEqualIndentifierDiscriminatorsAndId");
        }
    }
}
