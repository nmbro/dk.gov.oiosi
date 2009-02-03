using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.xml.schematron {
    /// <summary>
    /// Configuration section for the schematron store.
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class SchematronStoreConfig : ISchematronStoreConfig {
        private ushort _maxCompiledStylesheetsInMemory = 2;

        #region ISchematronStoreConfig Members

        /// <summary>
        /// Gets and sets the max compiled stylesheets to have in memory.
        /// </summary>
        public ushort MaxCompiledStylesheetsInMemory {
            get { return _maxCompiledStylesheetsInMemory; }
            set { _maxCompiledStylesheetsInMemory = value; }
        }

        #endregion
    }
}
