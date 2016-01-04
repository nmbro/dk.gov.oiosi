using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.converter.configuration {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class ResultSchematronValidatorAppConfigurationCollection : SchematronValidatorAppConfigurationCollection {
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public const string ResultSchematronConfigurationsName = "resultSchematronConfigurationCollection";
        
        protected override string ElementName {
            get { return ResultSchematronValidatorAppConfigurationCollection.ResultSchematronConfigurationsName; }
        }
    }
}
