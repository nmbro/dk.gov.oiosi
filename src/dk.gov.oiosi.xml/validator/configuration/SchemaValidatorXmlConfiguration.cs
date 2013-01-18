using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace dk.gov.oiosi.xml.validator.configuration {

    [XmlRoot("SchemaValidatorConfiguration", Namespace = "urn:dk.gov.oiosi.xml.validator.configuration")]
    public class SchemaValidatorXmlConfiguration : ISchemaValidatorConfiguration {
        private string _path;

        #region ISchemaValidatorConfiguration Members

        public string Path {
            get { return _path; }
            set { _path = value; }
        }

        #endregion
    }
}
