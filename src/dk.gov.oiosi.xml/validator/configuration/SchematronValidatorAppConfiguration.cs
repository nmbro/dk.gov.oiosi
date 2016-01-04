using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using dk.gov.oiosi.xml.converter.configuration;

namespace dk.gov.oiosi.xml.validator.configuration {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class SchematronValidatorAppConfiguration : ConfigurationElement, ISchematronValidatorConfiguration
    {
        public const string XmlSchematronValidatorAppConfigurationName = "xmlSchematronValidatorAppConfiguration";
        public const string NameName = "name";
        public const string MinSizeForErrorsName = "minSizeForErrors";
        public const string ErrorXPathName = "errorXPath";
        public const string ErrorMessageXPathName = "errorMessageXPath";
        public const string ConverterConfigurationName = "converterConfiguration";

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(NameName, IsRequired = true, IsKey = true)]
        public string Name {
            get { return (string)this[NameName]; }
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(ConverterConfigurationName, IsRequired = true)]
        public PreloadedConverterAppConfiguration ConverterAppConfiguration {
            get { return (PreloadedConverterAppConfiguration)this[ConverterConfigurationName]; }
        }

        #region ISchematronValidatorConfiguration Members

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(MinSizeForErrorsName, IsRequired = true)]
        public uint MinSizeForErrors {
            get { return (uint)this[MinSizeForErrorsName]; }
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(ErrorXPathName, IsRequired = true)]
        public string ErrorXPath {
            get { return (string)this[ErrorXPathName]; }
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(ErrorMessageXPathName, IsRequired = true)]
        public string ErrorMessageXPath {
            get { return (string)this[ErrorMessageXPathName]; }
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public IPreloadedConverterConfiguration ConverterConfiguration
        {
            get { return this.ConverterAppConfiguration; }
        }

        #endregion
    }
}
