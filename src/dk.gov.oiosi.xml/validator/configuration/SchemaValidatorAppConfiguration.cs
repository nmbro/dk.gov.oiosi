using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace dk.gov.oiosi.xml.validator.configuration {
    public class SchemaValidatorAppConfiguration : ConfigurationElement, ISchemaValidatorConfiguration {
        public const string SchemaDocumentPathName = "schemaDocumentPath";

        #region IXmlSchemaValidatorConfiguration Members

        /// <summary>
        /// Gets the schema path
        /// </summary>
        [ConfigurationProperty(SchemaDocumentPathName, IsRequired = true)]
        public string Path {
            get { return (string)this[SchemaDocumentPathName]; }
        }

        #endregion
    }
}
