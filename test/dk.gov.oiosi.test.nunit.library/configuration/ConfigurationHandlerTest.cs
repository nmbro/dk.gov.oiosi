using System;
using System.IO;
using System.Xml;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.security.revocation;
using NUnit.Framework;

namespace dk.gov.oiosi.test.nunit.library.configuration {
    
    [TestFixture]
    public class ConfigurationHandlerTest {

        /// <summary>
        /// By adding configuration sections up front we can avoid reading from the configuration file
        /// one time for each section. This improves performance.
        /// </summary>
        [Test]
        public void ConfigurationFileCanBeCreatedByRegisteringConfigurationSectionsUpFront() {
            var configFile = Settings.CreateRandomPath("RaspConfig.xml");
            Directory.CreateDirectory(configFile.Directory.FullName);

            ConfigurationHandler.ConfigFilePath = configFile.FullName;
            ConfigurationHandler.Reset();

            ConfigurationHandler.RegisterConfigurationSection<DocumentTypeCollectionConfig>();
            ConfigurationHandler.RegisterConfigurationSection<ProfileMappingCollectionConfig>();
            ConfigurationHandler.PreloadRegisteredConfigurationSections();

            DocumentTypeCollectionConfig documentTypeCollectionConfig = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            documentTypeCollectionConfig.AddDocumentType(new DefaultDocumentTypes().GetCatalogue());

            ConfigurationHandler.SaveToFile();

            var rootNode = GetRaspConfigurationNode(configFile);
            AssertNodeHasConfigurationSectionWithName(rootNode, "DocumentTypeCollectionConfig");
        }

        [Test]
        public void ConfigurationFileMustContainExactlyOneSectionOfEachTypeAccessedEvenIfTheSectionIsNotPreloaded() {
            var configFileWithEmailSection = GetConfigFileWithEmailTransportConfigSectionWithOnlyCertificatesSet();
            ConfigurationHandler.ConfigFilePath = configFileWithEmailSection.FullName;
            ConfigurationHandler.Reset();
            ConfigurationHandler.PreloadRegisteredConfigurationSections();
            ConfigurationHandler.SaveToFile();

            ConfigurationHandler.GetConfigurationSection<EmailTransportUserConfig>();
            ConfigurationHandler.SaveToFile();

            var rootNode = GetRaspConfigurationNode(configFileWithEmailSection);
            AssertNodeHasExactlyOneConfigurationSectionWithName(rootNode, "EmailTransportUserConfig");
        }

        [Test]
        public void RegisteredSectionsMustOnlyBeAddedToConfigFileIfTheyAlreadyExistInFileOrAreModifiedInMemory() {
            var configFile = Settings.CreateRandomPath("RaspConfig.xml");
            Directory.CreateDirectory(configFile.Directory.FullName);

            ConfigurationHandler.ConfigFilePath = configFile.FullName;
            ConfigurationHandler.Reset();

            ConfigurationHandler.RegisterConfigurationSection<DocumentTypeCollectionConfig>();
            ConfigurationHandler.PreloadRegisteredConfigurationSections();

            ConfigurationHandler.SaveToFile();

            var rootNode = GetRaspConfigurationNode(configFile);
            AssertNodeDoesNotHaveConfigurationSectionWithName(rootNode, "DocumentTypeCollectionConfig");
        }

        [Test]
        public void AnyXmlSerializableObjectCanBeAddedToConfiguration() {
            var configFile = Settings.CreateRandomPath("RaspConfig.xml");
            Directory.CreateDirectory(configFile.Directory.FullName);
            ConfigurationHandler.ConfigFilePath = configFile.FullName;
            ConfigurationHandler.Reset();

            RevocationLookupFactoryConfig ocspFactoryConfig = ConfigurationHandler.GetConfigurationSection<RevocationLookupFactoryConfig>();
            ocspFactoryConfig.ImplementationAssembly = "dk.gov.oiosi.library";
            ocspFactoryConfig.ImplementationNamespaceClass = "dk.gov.oiosi.security.ocsp.OcspLookup";

            ConfigurationHandler.SaveToFile();

            var rootNode = GetRaspConfigurationNode(configFile);
            AssertNodeHasConfigurationSectionWithName(rootNode, "RevocationLookupFactoryConfig");
        }

        [Test]
        public void ConfigurationSectionsReadFromDiskCanBeEditedAndTheChangesSaved() {
            var configFileWithOnlyOneDocumentType = GetConfigFileWithDocumentSectionWithOnlyOneDocumentType();
            ConfigurationHandler.ConfigFilePath = configFileWithOnlyOneDocumentType.FullName;
            ConfigurationHandler.Reset();

            var documentTypeCollectionConfig = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            documentTypeCollectionConfig.AddDocumentType(new DefaultDocumentTypes().GetCatalogue());

            ConfigurationHandler.SaveToFile();

            var rootNode = GetRaspConfigurationNode(configFileWithOnlyOneDocumentType);
            AssertNodeHasConfigurationSectionWithName(rootNode, "DocumentTypeCollectionConfig");

            var expectedDocumentTypeCount = 2;
            AssertDocumentTypesCount(rootNode, expectedDocumentTypeCount);
        }


        [Test]
        public void SectionsMissingInExistingConfigurationFileShouldBeAddedFromTheDefaultConfiguration() {
            FileInfo configFileWithOnlyOneDocumentType = GetConfigFileWithDocumentSectionWithOnlyOneDocumentType();
            ConfigurationHandler.ConfigFilePath = configFileWithOnlyOneDocumentType.FullName;
            ConfigurationHandler.Reset();

            DefaultLdapConfig ldapConfig = new DefaultLdapConfig();
            ldapConfig.SetIfNotExistsLdapLookupFactoryConfig();

            ConfigurationHandler.SaveToFile();

            XmlNode rootNode = GetRaspConfigurationNode(configFileWithOnlyOneDocumentType);
            AssertNodeHasConfigurationSectionWithName(rootNode, "DocumentTypeCollectionConfig");
            AssertNodeHasConfigurationSectionWithName(rootNode, "LdapLookupFactoryConfig");
        }

        [Test]
        public void UnrecognizedAndUnusedSectionsInConfigurationFileOnDiskMustBeKeptInConfigurationFile() {
            FileInfo configFileWithOnlyOneDocumentType = GetConfigFileWithDocumentSectionWithOnlyOneDocumentType();
            ConfigurationHandler.ConfigFilePath = configFileWithOnlyOneDocumentType.FullName;
            ConfigurationHandler.Reset();
            ConfigurationHandler.SaveToFile();

            XmlNode rootNode = GetRaspConfigurationNode(configFileWithOnlyOneDocumentType);
            AssertNodeHasConfigurationSectionWithName(rootNode, "DocumentTypeCollectionConfig");
        }

        [Test]
        public void VersionNumberMustBeAddedToXmlFileWhenSerializing() {
            var configFile = Settings.CreateRandomPath("RaspConfig.xml");
            Directory.CreateDirectory(configFile.Directory.FullName);
            ConfigurationHandler.ConfigFilePath = configFile.FullName;
            ConfigurationHandler.Reset();

            string expectedVersion = "1.1.1.4";
            ConfigurationHandler.Version = expectedVersion;

            ConfigurationHandler.SaveToFile();

            XmlNode rootNode = GetRaspConfigurationNode(configFile);
            string actualVersion = rootNode.Attributes["Version"].Value;
            Assert.AreEqual(expectedVersion, actualVersion);
        }

        [Test]
        public void VersionNumberMustBeNullIfNotPresentInXmlFile() {
            FileInfo configFileWithOnlyOneDocumentType = GetConfigFileWithDocumentSectionWithOnlyOneDocumentType();
            ConfigurationHandler.ConfigFilePath = configFileWithOnlyOneDocumentType.FullName;
            ConfigurationHandler.Reset();
            Assert.AreEqual(null, ConfigurationHandler.Version);
        }

        [Test]
        public void VersionNumberMustBeReadFromXmlFile() {
            FileInfo configWithVersion1117 = GetConfigFileWithVersion1117();
            ConfigurationHandler.ConfigFilePath = configWithVersion1117.FullName;
            ConfigurationHandler.Reset();
            Assert.AreEqual("1.1.1.7", ConfigurationHandler.Version);
        }

        # region Helper methods

        private void AssertNodeHasConfigurationSectionWithName(XmlNode node, string configurationSectionName) {
            bool nameFound = false;
            foreach (XmlNode childNode in node.ChildNodes) {
                var nodeConfigSectionName = GetConfigSectionName(childNode);
                if (nodeConfigSectionName == configurationSectionName) nameFound = true;
            }
            Assert.IsTrue(nameFound, "ConfigurationSection not found: " + configurationSectionName);
        }

        private void AssertNodeDoesNotHaveConfigurationSectionWithName(XmlNode node, string configurationSectionName) {
            bool nameFound = false;
            foreach (XmlNode childNode in node.ChildNodes) {
                var nodeConfigSectionName = GetConfigSectionName(childNode);
                if (nodeConfigSectionName == configurationSectionName) nameFound = true;
            }
            Assert.IsFalse(nameFound, "ConfigurationSection found (not expected): " + configurationSectionName);

        }

        private void AssertNodeHasExactlyOneConfigurationSectionWithName(XmlNode node, string configurationSectionName) {
            int nameFoundCount = 0;
            foreach (XmlNode childNode in node.ChildNodes) {
                var nodeConfigSectionName = GetConfigSectionName(childNode);
                if (nodeConfigSectionName == configurationSectionName) nameFoundCount++;
            }
            Assert.IsTrue(nameFoundCount == 1, "Configuration section found more than one time: " + nameFoundCount);
       }

        private void AssertDocumentTypesCount(XmlNode node, int expectedDocumentTypeCount) {
            XmlNode documentTypesNode = node.ChildNodes[0].ChildNodes[0];
            int documentTypeCount = documentTypesNode.ChildNodes.Count;
            Assert.AreEqual(expectedDocumentTypeCount, documentTypeCount, "Wrong number of document types found: " + documentTypeCount);
        }

        private FileInfo GetConfigFileWithDocumentSectionWithOnlyOneDocumentType() {
            var sourceFile = new FileInfo("Resources\\RaspConfigurationWithOnlyOneDocumentType.xml");
            var configFile = Settings.CreateRandomPath("RaspConfiguration.xml");
            Directory.CreateDirectory(configFile.Directory.FullName);
            File.Copy(sourceFile.FullName, configFile.FullName);
            return configFile;
        }

        private FileInfo GetConfigFileWithVersion1117() {
            var sourceFile = new FileInfo("Resources\\RaspConfigurationWithVersion1117.xml");
            var configFile = Settings.CreateRandomPath("RaspConfiguration.xml");
            Directory.CreateDirectory(configFile.Directory.FullName);
            File.Copy(sourceFile.FullName, configFile.FullName);
            return configFile;
        }

        private FileInfo GetConfigFileWithEmailTransportConfigSectionWithOnlyCertificatesSet() {
            var sourceFile = new FileInfo("Resources\\RaspConfigurationWithEmailTransportConfigSectionWithOnlyCertificatesSet.xml");
            var configFile = Settings.CreateRandomPath("RaspConfiguration.xml");
            Directory.CreateDirectory(configFile.Directory.FullName);
            File.Copy(sourceFile.FullName, configFile.FullName);
            return configFile;
        }

        private string GetConfigSectionName(XmlNode node) {
            return node.Attributes[0].Value;
        }

        private XmlNode GetRaspConfigurationNode(FileInfo configFile) {
            XmlDocument configXmlDocument = new XmlDocument();
            configXmlDocument.Load(configFile.FullName);
            var nodes = configXmlDocument.SelectNodes("/");
            return nodes[0].ChildNodes[1];
        }

        # endregion
    }

} 