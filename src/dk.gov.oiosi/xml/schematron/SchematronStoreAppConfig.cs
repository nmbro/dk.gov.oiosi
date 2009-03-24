using System.Configuration;

namespace dk.gov.oiosi.xml.schematron {
    /// <summary>
    /// Configuration in app.config/web.config for the schematron store config.
    /// </summary>
    public class SchematronStoreAppConfig : ConfigurationSection, ISchematronStoreConfig {
        
        /// <summary>
        /// The name of the xml element in the configuration for the schematron store
        /// </summary>
        public const string SCHEMATRONSTOREAPPCONFIGNAME = "schematronStoreAppConfig";
        
        /// <summary>
        /// The maximum stylesheets to keep in memory for schematron validation
        /// </summary>
        public const string MAXCOMPILEDSTYLESHEETSINMEMORYNAME = "maxCompiledStylesheetsInMemory";

        #region ISchematronStoreConfig Members

        /// <summary>
        /// Gets the maximum number of compiled stylesheet to have in the memory
        /// </summary>
        [ConfigurationProperty(MAXCOMPILEDSTYLESHEETSINMEMORYNAME, IsRequired=false, DefaultValue=2)]
        public int MaxCompiledStylesheetsInMemory {
            get { return (int)this[MAXCOMPILEDSTYLESHEETSINMEMORYNAME]; }
        }

        #endregion
    }
}
