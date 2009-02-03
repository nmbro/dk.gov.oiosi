using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace dk.gov.oiosi.xml.schematron {
    /// <summary>
    /// Configuration in app.config/web.config for the schematron store config.
    /// </summary>
    public class SchematronStoreAppConfig : ConfigurationSection, ISchematronStoreConfig {
        public const string SCHEMATRONSTOREAPPCONFIGNAME = "schematronStoreAppConfig";
        public const string MAXCOMPILEDSTYLESHEETSINMEMORYNAME = "maxCompiledStylesheetsInMemory";

        #region ISchematronStoreConfig Members

        /// <summary>
        /// Gets the maximum number of compiled stylesheet to have in the memory
        /// </summary>
        [ConfigurationProperty(MAXCOMPILEDSTYLESHEETSINMEMORYNAME, IsRequired=false, DefaultValue=2)]
        public ushort MaxCompiledStylesheetsInMemory {
            get { return (ushort)this[MAXCOMPILEDSTYLESHEETSINMEMORYNAME]; }
        }

        #endregion
    }
}
