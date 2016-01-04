using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.validator {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class SchemaValidationFailedException : Exception
    {
        private const string ERRORTEXT = "A schema exception has occured.";
        public SchemaValidationFailedException() : base(ERRORTEXT) { }
        public SchemaValidationFailedException(string message) : base(message) { }
        public SchemaValidationFailedException(Exception innerException) : base(ERRORTEXT, innerException) { }
        public SchemaValidationFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
