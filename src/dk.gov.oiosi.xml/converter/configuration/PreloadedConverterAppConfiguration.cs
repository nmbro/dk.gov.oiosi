using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using dk.gov.oiosi.xml.validator.configuration;

namespace dk.gov.oiosi.xml.converter.configuration {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class PreloadedConverterAppConfiguration : ConfigurationElement, IPreloadedConverterConfiguration {
        public const string PreloadedConverterConfigurationName = "preloadedConverterAppConfiguration";
        public const string CloseSourceStreamName = "closeSourceStream";
        public const string TransformStylesheetPathName = "transformStylesheetPath";
        public const string SourceSchemaConfigurationName = "sourceSchemaConfiguration";
        public const string ResultSchemaConfigurationName = "resultSchemaConfiguration";

        private List<ISchematronValidatorConfiguration> _sourceSchematronConfigurations;
        private List<ISchematronValidatorConfiguration> _resultSchematronConfigurations;
        private object _sourceInitLock = new object();
        private object _resultInitLock = new object();

        public PreloadedConverterAppConfiguration() { }

        #region IXmlConverterConfiguration Members
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(CloseSourceStreamName, DefaultValue = false)]
        public bool CloseSourceStream {
            get { return (bool)this[CloseSourceStreamName]; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(TransformStylesheetPathName, IsRequired = true)]
        public string TransformStylesheetPath {
            get { return (string)this[TransformStylesheetPathName]; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public ISchemaValidatorConfiguration SourceSchemaConfiguration {
            get { return this.SourceSchemaAppConfiguration; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public ISchemaValidatorConfiguration ResultSchemaConfiguration {
            get { return this.ResultSchemaAppConfiguration; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public IEnumerable<ISchematronValidatorConfiguration> GetSourceSchematronConfigurations() {
            lock (_sourceInitLock) {
                if (_sourceSchematronConfigurations != null) return _sourceSchematronConfigurations;
                SourceSchematronValidatorAppConfigurationCollection collection = this.SourceSchematronAppConfigurationCollection;
                _sourceSchematronConfigurations = new List<ISchematronValidatorConfiguration>();
                foreach (SchematronValidatorAppConfiguration configuration in collection) {
                    _sourceSchematronConfigurations.Add(configuration);
                }
                return _sourceSchematronConfigurations;
            }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public IEnumerable<ISchematronValidatorConfiguration> GetResultSchematronConfigurations()  {
            lock (_resultInitLock) {
                if (_resultSchematronConfigurations != null) return _resultSchematronConfigurations;
                SchematronValidatorAppConfigurationCollection resultCollection = this.ResultSchematronAppConfigurationCollection;
                _resultSchematronConfigurations = new List<ISchematronValidatorConfiguration>();
                foreach (SchematronValidatorAppConfiguration configuration in resultCollection) {
                    _resultSchematronConfigurations.Add(configuration);
                }
                return _resultSchematronConfigurations;
            }
        }

        #endregion
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(SourceSchemaConfigurationName, IsRequired = false)]
        public SchemaValidatorAppConfiguration SourceSchemaAppConfiguration {
            get { return (SchemaValidatorAppConfiguration)this[SourceSchemaConfigurationName]; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(ResultSchemaConfigurationName, IsRequired = false)]
        public SchemaValidatorAppConfiguration ResultSchemaAppConfiguration {
            get { return (SchemaValidatorAppConfiguration)this[ResultSchemaConfigurationName]; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(SourceSchematronValidatorAppConfigurationCollection.SourceSchematronConfigurationsName, IsRequired = false)]
        public SourceSchematronValidatorAppConfigurationCollection SourceSchematronAppConfigurationCollection {
            get { return (SourceSchematronValidatorAppConfigurationCollection)this[SourceSchematronValidatorAppConfigurationCollection.SourceSchematronConfigurationsName]; }
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(ResultSchematronValidatorAppConfigurationCollection.ResultSchematronConfigurationsName, IsRequired = false)]
        public ResultSchematronValidatorAppConfigurationCollection ResultSchematronAppConfigurationCollection {
            get { return (ResultSchematronValidatorAppConfigurationCollection)this[ResultSchematronValidatorAppConfigurationCollection.ResultSchematronConfigurationsName]; }
        }
    }
}
