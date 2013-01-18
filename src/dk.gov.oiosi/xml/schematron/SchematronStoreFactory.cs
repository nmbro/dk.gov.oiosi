using System.Configuration;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.xml.schematron {
    /// <summary>
    /// Factory class for the schematron store. It has a single instance that is used.
    /// </summary>
    public class SchematronStoreFactory {
        private static object _lockObject = new object();
        private static SchematronStore _instance;

        /// <summary>
        /// Gets the single instance of the schematron store
        /// </summary>
        /// <returns></returns>
        public static SchematronStore GetSchematronStore() {
            lock (_lockObject) {
                if (_instance == null) {
                    ISchematronStoreConfig config;
                    if (ConfigurationHandler.HasConfigurationSection<SchematronStoreConfig>()) {
                        config = ConfigurationHandler.GetConfigurationSection<SchematronStoreConfig>();
                    }
                    else {
                        config = (SchematronStoreConfig)ConfigurationManager.GetSection(SchematronStoreAppConfig.SCHEMATRONSTOREAPPCONFIGNAME);
                    }
                    if (config == null) {
                        _instance = new SchematronStore();
                    }
                    else {
                        _instance = new SchematronStore(config);
                    }
                }
                return _instance;
            }
        }
    }
}
