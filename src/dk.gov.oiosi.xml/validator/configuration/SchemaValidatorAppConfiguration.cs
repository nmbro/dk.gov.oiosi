using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace dk.gov.oiosi.xml.validator.configuration {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class SchemaValidatorAppConfiguration : ConfigurationElement, ISchemaValidatorConfiguration
    {
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public const string SchemaDocumentPathName = "schemaDocumentPath";

        #region IXmlSchemaValidatorConfiguration Members

        /// <summary>
        /// Gets the schema path
        /// </summary>
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [ConfigurationProperty(SchemaDocumentPathName, IsRequired = true)]
        public string Path {
            get { return (string)this[SchemaDocumentPathName]; }
        }

        #endregion
    }
}
