/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis S�gaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Serialization;
using System.Collections;
using System.IO;

namespace dk.gov.oiosi.configuration {
    /// <summary>
    /// An XML configuration file. Used by RaspConfigHandler
    /// </summary>
    [XmlRoot("RaspConfiguration", Namespace = "http://oiosi.dk/rasp/xml/2007/04/01/")]
    public class ConfigurationDocument {
        private static string _configFilePath = null;

        /// <summary>
        /// Contains all the types of configuration sections currently used
        /// </summary>
        protected static List<Type> configurationTypes = new List<Type>();
        /// <summary>
        /// A list of all the configuration sections in this config file
        /// </summary>
        protected ArrayList configurationSections = new ArrayList();

        static ConfigurationDocument() {
            _configFilePath = ConfigurationManager.AppSettings["RaspConfigurationFile"];
            if (_configFilePath == null)
                _configFilePath = "RaspConfiguration.xml";
        }

        /// <summary>
        /// A list of configuration sections
        /// </summary>
        [XmlElement("ConfigurationSection")]
        public ArrayList ConfigurationSections {
            get { return configurationSections; }
            set { configurationSections = value; }
        }

        private ArrayList GetConfigurationSectionsCopy() {
            ArrayList sections = new ArrayList();
            foreach (var section in configurationSections) {
                sections.Add(section);
            }
            return sections;
        }

        /// <summary>
        /// The path to the config file
        /// </summary>
        [XmlIgnore]
        public static string ConfigFilePath {
            get { return _configFilePath; }
            set { _configFilePath = value; }
        }

        /// <summary>
        /// Saves the configuration to a file
        /// </summary>
        public void SaveToFile() {
            StreamWriter streamWriter = null;
            try {
                XmlSerializer xmlSerializer = new XmlSerializer(this.GetType(), configurationTypes.ToArray());
                streamWriter = new StreamWriter(ConfigFilePath);
                xmlSerializer.Serialize(streamWriter, this);
                streamWriter.Flush();
            }
            catch (Exception e) {
                throw new ConfigurationCouldNotBeWrittenException(ConfigFilePath, e);
            }
            finally {
                if (streamWriter != null) streamWriter.Close();
            }
        }

        /// <summary>
        /// Reads generic RASP configuration from a file
        /// </summary>
        public ConfigurationDocument GetFromFile() {
            if (!File.Exists(ConfigFilePath)) {
                return this;
            }

            StreamReader streamReader = null;
            try {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConfigurationDocument), configurationTypes.ToArray());
                streamReader = new StreamReader(ConfigFilePath);
                ConfigurationDocument newDoc = (ConfigurationDocument)xmlSerializer.Deserialize(streamReader);
                return newDoc;
            }
            catch (Exception e) {
                throw new ConfigurationCouldNotBeReadFromFileException(ConfigFilePath, e);
            }
            finally {
                if (streamReader != null) streamReader.Close();
            }
        }

        ///// <summary>
        ///// Reads generic RASP configuration from a file
        ///// </summary>
        //public ConfigurationDocument GetFromFile() {
        //    // Check if a config file exists, if not create an empty config
        //    if (!File.Exists(ConfigFilePath)) {
        //        //new RaspConfigurationDocument();
        //        return new ConfigurationDocument();
        //    }
        //    StreamReader streamReader = null;
        //    try {
        //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConfigurationDocument), pTypes.ToArray());
        //        streamReader = new StreamReader(ConfigFilePath);
        //        ConfigurationDocument newDoc = (ConfigurationDocument)xmlSerializer.Deserialize(streamReader);
        //        return newDoc;
        //    }
        //    catch (Exception e) {
        //        throw new ConfigurationCouldNotBeReadFromFileException(ConfigFilePath, e);
        //    }
        //    finally {
        //        if (streamReader != null) streamReader.Close();
        //    }
        //}

        ///// <summary>
        ///// Registers a new configuration section type with the configuration handler
        ///// </summary>
        ///// <param name="t">The type to be registered</param>
        //public void RegisterType(Type t) {
        //    if (!pTypes.Contains(t)) {
        //        pTypes.Add(t);
        //    }
        //}

        /// <summary>
        /// Registers a new configuration section type with the configuration handler
        /// </summary>
        public void RegisterType<T>() where T: new() {
            Type configSectionType = typeof (T);
            if (configurationTypes.Contains(configSectionType)) return;

            configurationTypes.Add(configSectionType);
        }

        /// <summary>
        /// Deserializes the configuration to a string
        /// </summary>
        public override string ToString() {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = null;

            try {
                XmlSerializer xmlSerializer = new XmlSerializer(this.GetType(), configurationTypes.ToArray());
                streamWriter = new StreamWriter(memoryStream);
                xmlSerializer.Serialize(streamWriter, this);
                streamWriter.Flush();
                return System.Text.ASCIIEncoding.ASCII.GetString(memoryStream.ToArray());
            }
            catch (Exception e) {
                throw new ConfigurationCouldNotBeWrittenException(ConfigFilePath, e);
            }
            finally {
                if (streamWriter != null) streamWriter.Close();
                if (memoryStream != null) memoryStream.Close();
            }
        }

        /// <summary>
        /// Finds a configuration section of a certain type
        /// </summary>
        /// <param name="t">Type to be found</param>
        /// <returns>Index in the configuration section table</returns>
        public int IndexOfConfigurationSection(Type t) {
            for (int i = 0; i < configurationSections.Count; i++) {
                if (configurationSections[i].GetType() == t)
                    return i;
            }
            return -1;
        }

        public bool HasConfigurationSection(Type configSectionType) {
            int indexOfConfigurationSection = IndexOfConfigurationSection(configSectionType);
            if (indexOfConfigurationSection == -1) return false;
            return true;
        }

        public T GetConfigurationSection<T>(Type configSectionType) where T : new() {
            int indexOfConfigurationSection = IndexOfConfigurationSection(configSectionType);
            return (T)configurationSections[indexOfConfigurationSection];    
        }

       
        public T AddNewConfigurationSection<T>(Type configSectionType) where T : new() {
            T configSection = GetNewConfigSection<T>(configSectionType);
            AddConfigurationSection(configSection);
            return configSection;
        }

        public void AddReplaceConfigurationSection(object configurationSection) {
            Type configSectionType = configurationSection.GetType();
            int indexOfConfigurationSection = IndexOfConfigurationSection(configSectionType);
            if (indexOfConfigurationSection == -1) {
                AddConfigurationSection(configurationSection);
            }
            else {
                configurationSections[indexOfConfigurationSection] = configurationSection;    
            }
        }

        public void ReloadConfigurationFile() {
            ConfigurationDocument configurationDocumentFromFile = GetFromFile();
            var configurationsSectionsFromFile = configurationDocumentFromFile.GetConfigurationSectionsCopy();
            foreach (var configurationSection in configurationsSectionsFromFile) {
                if (configurationTypes.Contains(configurationSection.GetType())) {
                    AddReplaceConfigurationSection(configurationSection);
                }
            }
        }

        public T ReloadConfigSectionFromFile<T>(Type configSectionType) where T: new() {
            RegisterType<T>();
            ConfigurationDocument configurationDocumentFromFile = GetFromFile();
            if (configurationDocumentFromFile.HasConfigurationSection(configSectionType)) {
                T configSection = configurationDocumentFromFile.GetConfigurationSection<T>(configSectionType);
                AddReplaceConfigurationSection(configSection);
                return configSection;
            }
            return AddNewConfigurationSection<T>(configSectionType);
        }

        /// <summary>
        /// Adds all configuration sections from the configuration file, that hasn't been loaded/modified in the session
        /// </summary>
        public void AddUnrecognizedSectionsFromFile() {
            ConfigurationDocument configurationFromFile = GetFromFile();
            foreach (var configurationSection in configurationFromFile.ConfigurationSections) {
                if (HasConfigurationSection(configurationSection.GetType()) == false) {
                    AddConfigurationSection(configurationSection);
                }
            }
        }

        private void AddConfigurationSection(object configSection) {
            configurationSections.Add(configSection);
        }

        private T GetNewConfigSection<T>(Type configSectionType) where T : new() {
            object[] attributes = configSectionType.GetCustomAttributes(typeof(XmlRootAttribute), true);
            if (attributes.Length != 1) throw new ConfigurationSectionMissingXmlRootAttributeException();

            XmlRootAttribute rootAttribute = (XmlRootAttribute)attributes[0];
            if (String.IsNullOrEmpty(rootAttribute.Namespace)) {
                throw new ConfigurationSectionMissingXmlRootAttributeException();
            }

            if (configSectionType.GetConstructor(Type.EmptyTypes) == null)
                throw new ConfigurationTypeHasNoDefaultConstructorException();
            RegisterType<T>();
            int indexOfConfigurationSection = IndexOfConfigurationSection(configSectionType);
            if (indexOfConfigurationSection >= 0) {
                throw new ConfigurationSectionAlreadyExistsException();
            }

            T newConfigSection = new T();
            return newConfigSection;
        }

    }
}
