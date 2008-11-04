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
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Xml;


namespace dk.gov.oiosi.configuration {

    
    /// <summary>
    /// A configuration handler that saves and reads configuration sections to and from file. The configuration holds only one section of a certain class.
    /// </summary>
    /// <example>
    /// <code>
    /// // MyConfigurationSection can be a random configuration storage class that MUST be XML serializable and have a default constructor
    /// MyConfigurationSection myConfig = new MyConfigurationSection(...);
    /// RaspConfigurationSection.AddConfigurationSection(myConfig);
    /// 
    /// // Read the same config section
    /// <![CDATA[
    /// MyConfigurationSection anotherConfig = RaspConfigurationHandler.GetConfigurationSection<MyConfigurationSection>();
    /// ]]>
    /// 
    /// //... do whatever with the config...
    /// 
    /// //Save
    /// RaspConfigurationHandler.SaveToFile();
    /// </code>
    /// </example>
    /// <remarks>Always make sure your configuration sections have a constructor that takes no arguments</remarks>
    public class ConfigurationHandler {
        private static Dictionary<Type, object> _sections = new Dictionary<Type, object>();
        private static object lockObject = new object();
        /// <summary>
        /// The default rasp namespace url
        /// </summary>
        public const string RaspNamespaceUrl = "http://oiosi.dk/rasp/xml/2007/04/01/";

        /// <summary>
        /// The path to the config file
        /// </summary>
        public static string ConfigFilePath { 
            get { return ConfigurationDocument.ConfigFilePath; }
            set { 
                ConfigurationDocument.ConfigFilePath = value;
                pConfigDocument = pConfigDocument.GetFromFile();
            }
        }

        /// <summary>
        /// The config document
        /// </summary>
        protected static ConfigurationDocument pConfigDocument = new ConfigurationDocument();

        static ConfigurationHandler() {
            pConfigDocument = pConfigDocument.GetFromFile();
        }

        /// <summary>
        /// Returns whether a specific configuration section is in the configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasConfigurationSection<T>() where T : new() {
            lock (lockObject) {
                // Check to see if we already have the section in memory
                object section = null;
                if (_sections.TryGetValue(typeof(T), out section)) 
                    return true;
                int i = pConfigDocument.IndexOfConfigurationSection(typeof(T));
                if (i < 0) {
                    pConfigDocument.RegisterType(typeof(T));
                    pConfigDocument = pConfigDocument.GetFromFile();
                    i = pConfigDocument.IndexOfConfigurationSection(typeof(T));
                }
                // See if the section can be found
                if (i < 0) {
                    // If not, create a new one and add to memory and file
                    return false;
                }
                // If the section was in file, get it in memory
                section = pConfigDocument.ConfigurationSections[i];
                _sections.Add(typeof(T), section);
                return true;
            }
        }

        /// <summary>
        /// Gets the first configuration section of the type T
        /// </summary>
        /// <typeparam name="T">A serializable class type with a default constructor</typeparam>
        /// <returns>A configuraion section. If the configuration section was not found a new instance of the configuration section class is returned.</returns>
        public static T GetConfigurationSection<T>() where T : new() {
            lock (lockObject) {
                // Check to see if we already have the section in memory
                object section = null;
                if (_sections.TryGetValue(typeof(T), out section)) return (T)section;
                int i = pConfigDocument.IndexOfConfigurationSection(typeof(T));
                if (i < 0) {
                    pConfigDocument.RegisterType(typeof(T));
                    pConfigDocument = pConfigDocument.GetFromFile();
                    i = pConfigDocument.IndexOfConfigurationSection(typeof(T));
                }
                // See if the section can be found
                if (i < 0) {
                    // If not, create a new one and add to memory and file
                    T newConfigSection = new T();
                    AddConfigurationSection(newConfigSection);
                    _sections.Add(typeof(T), newConfigSection);
                    return newConfigSection;
                }
                // If the section was in file, get it in memory
                section = pConfigDocument.ConfigurationSections[i];
                _sections.Add(typeof(T), section);
                return (T)section;
            }
        }

        /// <summary>
        /// Saves the configuration to file
        /// </summary>
        public static void SaveToFile() {
            lock (lockObject) {
                int i = 0;
                foreach (KeyValuePair<Type, object> pair in _sections) {
                    i = pConfigDocument.IndexOfConfigurationSection(pair.Key);
                    pConfigDocument.ConfigurationSections[i] = pair.Value;
                }
                pConfigDocument.SaveToFile();
            }
        }

        /// <summary>
        /// Updates a configuration section. If the section is not already present it will be created.
        /// </summary>
        /// <param name="configSection">The configuration section to be updated or added (Must have a default constructor and be XML serializable)</param>
        private static void AddConfigurationSection(object configSection) {
            object[] attributes = configSection.GetType().GetCustomAttributes(typeof(System.Xml.Serialization.XmlRootAttribute), true);
            if (attributes.Length != 1) {
                throw new ConfigurationSectionMissingXmlRootAttributeException();
            }
            System.Xml.Serialization.XmlRootAttribute rootAttribute =
                (System.Xml.Serialization.XmlRootAttribute)attributes[0];

            if (String.IsNullOrEmpty(rootAttribute.Namespace)) {
                throw new ConfigurationSectionMissingXmlRootAttributeException();
            }

            lock (lockObject) {
                Type t = configSection.GetType();
                if (t.GetConstructor(Type.EmptyTypes) == null)
                    throw new ConfigurationTypeHasNoDefaultConstructorException();
                pConfigDocument.RegisterType(t);
                int j = pConfigDocument.IndexOfConfigurationSection(t);
                if (j >= 0) {
                    throw new ConfigurationSectionAlreadyExistsException();
                }
                // If no section from before was found, add the updated section
                pConfigDocument.ConfigurationSections.Add(configSection);
                SaveToFile();
            }
        }
   }
}