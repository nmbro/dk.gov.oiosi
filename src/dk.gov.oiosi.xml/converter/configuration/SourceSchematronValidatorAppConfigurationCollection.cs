using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.converter.configuration {
    public class SourceSchematronValidatorAppConfigurationCollection : SchematronValidatorAppConfigurationCollection {
        public const string SourceSchematronConfigurationsName = "sourceSchematronConfigurationCollection";

        protected override string ElementName {
            get { return SourceSchematronValidatorAppConfigurationCollection.SourceSchematronConfigurationsName; }
        }
    }
}
