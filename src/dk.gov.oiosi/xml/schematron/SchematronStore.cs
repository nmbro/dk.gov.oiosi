﻿using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.common.cache;
using System.Xml.Xsl;
using dk.gov.oiosi.xml.xslt;
using System.Xml;
using System.IO;

namespace dk.gov.oiosi.xml.schematron {
    
    /// <summary>
    /// A global schematron store that can be used anywhere in the application to access
    /// schematrons from the filesystem. Further it caches a specific number of compiled 
    /// stylesheets in memory
    /// </summary>
    public class SchematronStore {
        private object _lockObject = new object();
        private QuantityCache<string, XslCompiledTransform> _cache;

        /// <summary>
        /// Constructor that uses a default of max two cohierent compiled stylesheeets 
        /// in memory.
        /// </summary>
        public SchematronStore() {
              _cache = new QuantityCache<string, XslCompiledTransform>(2);
        }

        /// <summary>
        /// Constructor that takes the configuration for the schematron store
        /// </summary>
        /// <param name="configuration"></param>
        public SchematronStore(ISchematronStoreConfig configuration) {
            if (configuration == null) throw new ArgumentNullException("configuration");
            InitFromConfiguration(configuration);
        }

        /// <summary>
        /// Gets the compiled schematron from a given path.
        /// </summary>
        /// <param name="path">The path where to find the </param>
        /// <returns></returns>
        public XslCompiledTransform GetCompiledSchematron(string path) {
            if (path == null) throw new ArgumentNullException("path");
            FileInfo fileInfo = new FileInfo(path);
            XslCompiledTransform compiledSchematron = null;
            lock (_lockObject) {
                if (_cache.TryGetValue(fileInfo.FullName, out compiledSchematron)) return compiledSchematron;
                compiledSchematron = LoadCompiledSchematron(fileInfo);
                _cache.Add(path, compiledSchematron);
                return compiledSchematron;
            }
        }

        private void InitFromConfiguration(ISchematronStoreConfig config) {
            int maxStylesheetsInMemory = (int)config.MaxCompiledStylesheetsInMemory;
            _cache = new QuantityCache<string, XslCompiledTransform>(maxStylesheetsInMemory);
        }

        private XslCompiledTransform LoadCompiledSchematron(FileInfo fileInfo) {
            XslCompiledTransform compiledSchematron = null;
            XmlDocument xmlStylesheet = new XmlDocument();
            XsltUtility xsltUtility = new XsltUtility();
            try {
                xmlStylesheet.Load(fileInfo.FullName);
                return xsltUtility.PrecompiledStyleSheet(xmlStylesheet);
            }
            catch (Exception ex) {
                throw new FailedToLoadSchematronStylesheetException(fileInfo, ex);
            }
        }
    }
}
