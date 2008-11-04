using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.xpath.discriminator;

namespace dk.gov.oiosi.test.nunit.library.communitcation.configuration {
    [TestFixture]
    public class RaspDocumentTypeCollectionConfigTest {
        [Test]
        public void _01_AddRemoveDocumentTypeTest() {
            try {
                Print.Started("_01_AddRemoveDocumentTypeTest");
                string rootName = "Test";
                string rootNamespace = "http://oio.dk/test";
                XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
                XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
                XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
                identifierDiscriminators.Add(identifierDiscriminatorA);
                identifierDiscriminators.Add(identifierDiscriminatorB);
                DocumentTypeConfig documentType = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminators);
                DocumentTypeCollectionConfig documentTypeCollection = new DocumentTypeCollectionConfig();
                documentTypeCollection.AddDocumentType(documentType);
                DocumentTypeConfig documentTypeFromCollection = documentTypeCollection.GetDocumentType(rootName, rootNamespace, identifierDiscriminators);
                Assert.IsTrue(documentTypeFromCollection.Equals(documentType));
                documentTypeCollection.RemoveDocumentType(documentTypeFromCollection);
                documentTypeFromCollection = null;
                bool anyDocument = documentTypeCollection.TryGetDocumentType(rootName, rootNamespace, identifierDiscriminators, out documentTypeFromCollection);
                Assert.IsFalse(anyDocument);
            }
            finally {
                Print.Completed("_01_AddRemoveDocumentTypeTest");
            }
        }

        [Test, ExpectedException(typeof(DocumentAllreadyAddedException))]
        public void _02_InsertDocumentWithSameIdTest() {
            try {
                Print.Started("_02_InsertDocumentWithSameIdTest");
                string rootNameA = "TestA";
                string rootNameB = "TestB";
                string rootNamespace = "http://oio.dk/test";
                XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
                XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
                XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
                identifierDiscriminators.Add(identifierDiscriminatorA);
                identifierDiscriminators.Add(identifierDiscriminatorB);
                DocumentTypeConfig documentTypeA = new DocumentTypeConfig(rootNameA, rootNamespace, identifierDiscriminators);
                DocumentTypeConfig documentTypeB = new DocumentTypeConfig(rootNameB, rootNamespace, identifierDiscriminators);
                DocumentTypeCollectionConfig documentTypeCollection = new DocumentTypeCollectionConfig();
                documentTypeCollection.AddDocumentType(documentTypeA);
                documentTypeB.Id = documentTypeA.Id;
                documentTypeCollection.AddDocumentType(documentTypeB);
            }
            finally {
                Print.Completed("_02_InsertDocumentWithSameIdTest");
            }
        }

        [Test, ExpectedException(typeof(DocumentAllreadyAddedException))]
        public void _03_InsertDocumentTypeWithSameKeyValuesTest() {
            try {
                Print.Started("_03_InsertDocumentTypeWithSameKeyValuesTest");
                string rootName = "Test";
                string rootNamespace = "http://oio.dk/test";
                XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
                XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
                XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
                identifierDiscriminators.Add(identifierDiscriminatorA);
                identifierDiscriminators.Add(identifierDiscriminatorB);
                DocumentTypeConfig documentTypeA = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminators);
                DocumentTypeConfig documentTypeB = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminators);
                DocumentTypeCollectionConfig documentTypeCollection = new DocumentTypeCollectionConfig();
                documentTypeCollection.AddDocumentType(documentTypeA);
                documentTypeCollection.AddDocumentType(documentTypeB);
            }
            finally {
                Print.Completed("_03_InsertDocumentTypeWithSameKeyValuesTest");
            }
        }

        [Test]
        public void _04_GetNonExistingDocumentTypeFromIdTest() {
            try {
                Print.Started("_04_GetNonExistingDocumentTypeFromIdTest");
                string rootName = "Test";
                string rootNamespace = "http://oio.dk/test";
                XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
                XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
                XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
                identifierDiscriminators.Add(identifierDiscriminatorA);
                identifierDiscriminators.Add(identifierDiscriminatorB);
                DocumentTypeConfig documentType = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminators);
                DocumentTypeCollectionConfig documentTypeCollection = new DocumentTypeCollectionConfig();
                documentTypeCollection.AddDocumentType(documentType);
                DocumentTypeConfig documentTypeFromCollection = null;
                bool any = documentTypeCollection.TryGetDocumentType(Guid.NewGuid(), out documentTypeFromCollection);
                Assert.IsFalse(any);
                Assert.IsNull(documentTypeFromCollection);
            }
            finally {
                Print.Completed("_04_GetNonExistingDocumentTypeFromIdTest");
            }
        }

        [Test]
        public void _05_GetNonExistingDocumentTypeFromParameters() {
            try {
                Print.Started("_05_GetNonExistingDocumentTypeFromParameters");
                string rootName = "TestA";
                string rootNamespace = "http://oio.dk/test";
                XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
                XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
                XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
                identifierDiscriminators.Add(identifierDiscriminatorA);
                identifierDiscriminators.Add(identifierDiscriminatorB);
                DocumentTypeConfig documentType = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminators);
                DocumentTypeCollectionConfig documentTypeCollection = new DocumentTypeCollectionConfig();
                documentTypeCollection.AddDocumentType(documentType);
                DocumentTypeConfig documentTypeFromCollection = null;
                bool any = documentTypeCollection.TryGetDocumentType("TestB", rootNamespace, identifierDiscriminators, out documentTypeFromCollection);
                Assert.IsFalse(any);
                Assert.IsNull(documentTypeFromCollection);
            }
            finally {
                Print.Completed("_05_GetNonExistingDocumentTypeFromParameters");
            }
        }

        [Test, ExpectedException(typeof(NoDocumentTypeFoundFromIdException))]
        public void _06_GetNonExistingDocumentTypeFromIdTest() {
            try {
                Print.Started("_06_GetNonExistingDocumentTypeFromIdTest");
                string rootName = "Test";
                string rootNamespace = "http://oio.dk/test";
                XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
                XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
                XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
                identifierDiscriminators.Add(identifierDiscriminatorA);
                identifierDiscriminators.Add(identifierDiscriminatorB);
                DocumentTypeConfig documentType = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminators);
                DocumentTypeCollectionConfig documentTypeCollection = new DocumentTypeCollectionConfig();
                documentTypeCollection.AddDocumentType(documentType);
                documentTypeCollection.GetDocumentType(Guid.NewGuid());
            }
            finally {
                Print.Completed("_06_GetNonExistingDocumentTypeFromIdTest");
            }
        }

        [Test, ExpectedException(typeof(NoDocumentTypeFoundFromParametersException))]
        public void _07_GetNonExistingDocumentTypeFromParameters() {
            try {
                Print.Started("_07_GetNonExistingDocumentTypeFromParameters");
                string rootName = "TestA";
                string rootNamespace = "http://oio.dk/test";
                XPathDiscriminatorConfig identifierDiscriminatorA = new XPathDiscriminatorConfig("//Test/Key", "A");
                XPathDiscriminatorConfig identifierDiscriminatorB = new XPathDiscriminatorConfig("//Test/Key", "B");
                XpathDiscriminatorConfigCollection identifierDiscriminators = new XpathDiscriminatorConfigCollection();
                identifierDiscriminators.Add(identifierDiscriminatorA);
                identifierDiscriminators.Add(identifierDiscriminatorB);
                DocumentTypeConfig documentType = new DocumentTypeConfig(rootName, rootNamespace, identifierDiscriminators);
                DocumentTypeCollectionConfig documentTypeCollection = new DocumentTypeCollectionConfig();
                documentTypeCollection.AddDocumentType(documentType);
                documentTypeCollection.GetDocumentType("TestB", rootNamespace, identifierDiscriminators);
            }
            finally {
                Print.Completed("_07_GetNonExistingDocumentTypeFromParameters");
            }
        }
    }
}
