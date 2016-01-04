using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace dk.gov.oiosi.xml.validator.configuration {

    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    [XmlRoot("SchemaValidatorConfiguration", Namespace = "urn:dk.gov.oiosi.xml.validator.configuration")]
    public class SchemaValidatorXmlConfiguration : ISchemaValidatorConfiguration {
        private string _path;

        #region ISchemaValidatorConfiguration Members

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        #endregion
    }
}
