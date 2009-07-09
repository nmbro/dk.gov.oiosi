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
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System;
using System.Collections;
using System.Collections.Generic;


namespace dk.gov.oiosi.configuration {

    
    /// <summary>
    /// A configuration handler that saves and reads configuration sections to and from file. The configuration holds only one section of a certain class.
    /// </summary>
    /// <example>
    /// <code>
    /// // MyConfigurationSection can be a random configuration storage class that MUST be XML serializable and have a default constructor
    /// MyConfigurationSection myConfig = new MyConfigurationSection(...);
    /// RaspConfigurationSection.AddNewConfigurationSection(myConfig);
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
        
        private static Dictionary<Type, object> _configSectionsCache = new Dictionary<Type, object>();
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
            set { ConfigurationDocument.ConfigFilePath = value; }
        }

        /// <summary>
        /// Gets and sets the configuration version.
        /// </summary>
        public static string Version {
            get { return configurationDocument.Version; }
            set { configurationDocument.Version = value; }
        }

        /// <summary>
        /// The config document
        /// </summary>
        protected static ConfigurationDocument configurationDocument = new ConfigurationDocument();

        static ConfigurationHandler() {
            configurationDocument = configurationDocument.GetFromFile();
        }

        /// <summary>
        /// Used by the Unit Test to reset the static information.
        /// </summary>
        public static void Reset() {
            _configSectionsCache = new Dictionary<Type, object>();
            configurationDocument.ConfigurationSections = new ArrayList();
            configurationDocument = configurationDocument.GetFromFile();
        }

        /// <summary>
        /// Returns whether a specific configuration section is in the configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasConfigurationSection<T>() where T : new() {
            Type configSectionType = typeof(T);
            lock (lockObject) {
                bool sectionExistsInCache = _configSectionsCache.ContainsKey(typeof(T));
                if (sectionExistsInCache) return true;

                bool sectionExistsInConfigurationDocument = configurationDocument.HasConfigurationSection(configSectionType);
                if (sectionExistsInConfigurationDocument) return true;

                return false;
            }
        }

        /// <summary>
        /// Gets the first configuration section of the type T
        /// </summary>
        /// <typeparam name="T">A serializable class type with a default constructor</typeparam>
        /// <returns>A configuration section. If the configuration section is not found a new instance of the configuration section class is returned.</returns>
        public static T GetConfigurationSection<T>() where T : new() {
            Type configSectionType = typeof (T);
            lock (lockObject) {
                object section;
                bool sectionExistsInCache = _configSectionsCache.TryGetValue(typeof (T), out section);
                if (sectionExistsInCache) {
                    return (T) section;
                }

                bool sectionExistsInConfigurationDocument = configurationDocument.HasConfigurationSection(configSectionType);
                if (sectionExistsInConfigurationDocument) {
                    section = configurationDocument.GetConfigurationSection<T>(configSectionType);
                    _configSectionsCache.Add(configSectionType, section);
                    return (T) section;
                }

                section = configurationDocument.ReloadConfigSectionFromFile<T>(configSectionType);
                _configSectionsCache.Add(configSectionType, section);
                return (T) section;
            }
        }

        /// <summary>
        /// Saves the configuration to file
        /// </summary>
        public static void SaveToFile() {
            lock (lockObject) {
                configurationDocument.AddUnrecognizedSectionsFromFile();
                configurationDocument.SaveToFile();
            }
        }

        /// <summary>
        /// Register a configuration section.
        /// By registering configuration sections up front, the load time is improved.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RegisterConfigurationSection<T>() where T: new() {
            lock (lockObject) {
                configurationDocument.RegisterType<T>();
            }
        }

        /// <summary>
        /// Preloads all registered configuration sections in order to speed up load time of the configuration file
        /// </summary>
        public static void PreloadRegisteredConfigurationSections() {
            lock (lockObject) {
                configurationDocument.ReloadConfigurationFile();
            }
        }
    }
}