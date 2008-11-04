using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using dk.gov.oiosi.xml.converter.configuration;

namespace dk.gov.oiosi.xml.validator.configuration {
    public class SchematronValidatorAppConfiguration : ConfigurationElement, ISchematronValidatorConfiguration {
        public const string XmlSchematronValidatorAppConfigurationName = "xmlSchematronValidatorAppConfiguration";
        public const string NameName = "name";
        public const string MinSizeForErrorsName = "minSizeForErrors";
        public const string ErrorXPathName = "errorXPath";
        public const string ErrorMessageXPathName = "errorMessageXPath";
        public const string ConverterConfigurationName = "converterConfiguration";

        [ConfigurationProperty(NameName, IsRequired = true, IsKey = true)]
        public string Name {
            get { return (string)this[NameName]; }
        }

        [ConfigurationProperty(ConverterConfigurationName, IsRequired = true)]
        public PreloadedConverterAppConfiguration ConverterAppConfiguration {
            get { return (PreloadedConverterAppConfiguration)this[ConverterConfigurationName]; }
        }

        #region ISchematronValidatorConfiguration Members

        [ConfigurationProperty(MinSizeForErrorsName, IsRequired = true)]
        public uint MinSizeForErrors {
            get { return (uint)this[MinSizeForErrorsName]; }
        }

        [ConfigurationProperty(ErrorXPathName, IsRequired = true)]
        public string ErrorXPath {
            get { return (string)this[ErrorXPathName]; }
        }

        [ConfigurationProperty(ErrorMessageXPathName, IsRequired = true)]
        public string ErrorMessageXPath {
            get { return (string)this[ErrorMessageXPathName]; }
        }

        public IPreloadedConverterConfiguration ConverterConfiguration {
            get { return this.ConverterAppConfiguration; }
        }

        #endregion
    }
}
