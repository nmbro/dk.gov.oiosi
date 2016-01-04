using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using dk.gov.oiosi.xml.validator.configuration;

namespace dk.gov.oiosi.xml.converter.configuration {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class SchematronValidatorAppConfigurationCollection : ConfigurationElementCollection {
        
        protected override ConfigurationElement CreateNewElement() {
            return new SchematronValidatorAppConfiguration();
        }
        
        protected override object GetElementKey(ConfigurationElement element) {
            SchematronValidatorAppConfiguration configuration = ((SchematronValidatorAppConfiguration)element);
            return configuration.Name;
        }
    }
}
