using System.IO;
using System.Xml.Serialization;
using dk.gov.oiosi.raspProfile;

using NUnit.Framework;

using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.test.nunit.library.configuration {
    /* 
     * 
     * This test i buggy... It fails if it's not the first test to be run
     * Martin Bentzen, 15-12-2008
     * 
     * 
    [TestFixture]
    public class ConfigurationTest {
        /// <summary>
        /// Deletes the old config file
        /// </summary>
        private void DeleteConfigFile() {
            if (File.Exists(ConfigurationHandler.ConfigFilePath)) {
                File.Delete(ConfigurationHandler.ConfigFilePath);
            }
        }

        /// <summary>
        /// Test configuration section
        /// </summary>
        [XmlRoot("TestConfigSection", Namespace = "http://oiosi.dk/rasp/xml/2007/04/01/")]
        public class TestConfigSection {
            [XmlElement("a")]
            public int a = 1;
            [XmlElement("b")]
            public int b = 2;

            public override bool Equals(object obj) {
                return (((TestConfigSection)obj).a == a && ((TestConfigSection)obj).b == b);
            }
        }


        [Test]
        public void _01FirstTest() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
        }

        [Test]
        public void SaveConfigSectionToFileAndLoadItAgain() {
            DeleteConfigFile();
            TestConfigSection configSection = ConfigurationHandler.GetConfigurationSection<TestConfigSection>();
            configSection.a = 1000;
            configSection.b = 2000;
            ConfigurationHandler.SaveToFile();
            TestConfigSection loadedFromFile = ConfigurationHandler.GetConfigurationSection<TestConfigSection>();
            Assert.AreEqual(configSection, loadedFromFile);
        }


        private void MessUpConfig() {
            TestConfigSection configSection = ConfigurationHandler.GetConfigurationSection<TestConfigSection>();
            configSection.a = 666;
            configSection.b = 666;
        }


        [Test]
        public void ConfigSectionChangedWhileInMemory() {
            DeleteConfigFile();
            TestConfigSection configSection = ConfigurationHandler.GetConfigurationSection<TestConfigSection>();
            configSection.a = 1;
            configSection.b = 2;
            MessUpConfig();

            Assert.AreNotEqual(1, configSection.a);
            Assert.AreNotEqual(2, configSection.b);
        }

        [Test]
        public void ConfigSectionSavedChangedWhileInMemoryAndSaved() {
            DeleteConfigFile();
            TestConfigSection configSection = ConfigurationHandler.GetConfigurationSection<TestConfigSection>();
            configSection.a = 1;
            configSection.b = 2;
            ConfigurationHandler.SaveToFile();
            MessUpConfig();
            ConfigurationHandler.SaveToFile();
            configSection = ConfigurationHandler.GetConfigurationSection<TestConfigSection>();
            Assert.AreNotEqual(1, configSection.a);
            Assert.AreNotEqual(2, configSection.b);
        }

        //[Test]
        public void ConfigFileMissing() {
            if (File.Exists(ConfigurationHandler.ConfigFilePath)) {
                File.Delete(ConfigurationHandler.ConfigFilePath);
            }
            TestConfigSection configSection = ConfigurationHandler.GetConfigurationSection<TestConfigSection>();
            Assert.IsNotNull(configSection);
        }

        [Test]
        public void GetSectionWhenACofigFileNotExist() {
            DeleteConfigFile();
            TestConfigSection guid = ConfigurationHandler.GetConfigurationSection<TestConfigSection>();
            Assert.IsNotNull(guid);
        }

        [XmlRoot("TestConfigSection", Namespace = "http://oiosi.dk/rasp/xml/2007/04/01/")]
        public class NewConfigSectionType { }

        [Test]
        public void GetSectionThatDoesNotExist() {
            DeleteConfigFile();
            TestConfigSection configSection = ConfigurationHandler.GetConfigurationSection<TestConfigSection>();
            ConfigurationHandler.SaveToFile();

            NewConfigSectionType section = ConfigurationHandler.GetConfigurationSection<NewConfigSectionType>();
            Assert.IsNotNull(section);
        }
    }
     * */
}
