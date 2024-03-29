﻿using dk.gov.oiosi.xml.schematron;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// Default schematron config
    /// </summary>
    public class DefaultSchematronConfig {
        /// <summary>
        /// Set default, live values
        /// </summary>
        public void SetSchematronStoreConfig() {
            SchematronStoreConfig config = ConfigurationHandler.GetConfigurationSection<SchematronStoreConfig>();
            config.MaxCompiledStylesheetsInMemory = 2;
        }

        /// <summary>
        /// Use the default values
        /// </summary>
        public void SetIfNotExistsOcesCertificateConfig() {
            if (ConfigurationHandler.HasConfigurationSection<SchematronStoreConfig>())
                return;
            SetSchematronStoreConfig();
        }
    }
}
