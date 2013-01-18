using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.converter.configuration {
    public class ResultSchematronValidatorAppConfigurationCollection : SchematronValidatorAppConfigurationCollection {
        public const string ResultSchematronConfigurationsName = "resultSchematronConfigurationCollection";

        protected override string ElementName {
            get { return ResultSchematronValidatorAppConfigurationCollection.ResultSchematronConfigurationsName; }
        }
    }
}
