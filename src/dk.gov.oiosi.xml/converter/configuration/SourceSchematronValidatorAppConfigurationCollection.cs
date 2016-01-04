using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.converter.configuration {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class SourceSchematronValidatorAppConfigurationCollection : SchematronValidatorAppConfigurationCollection {
        public const string SourceSchematronConfigurationsName = "sourceSchematronConfigurationCollection";
        
        protected override string ElementName {
            get { return SourceSchematronValidatorAppConfigurationCollection.SourceSchematronConfigurationsName; }
        }
    }
}
