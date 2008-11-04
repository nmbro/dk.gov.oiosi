using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.validator {
    public class SchemaValidationFailedException : Exception {
        private const string ERRORTEXT = "A schema exception has occured.";
        public SchemaValidationFailedException() : base(ERRORTEXT) { }
        public SchemaValidationFailedException(string message) : base(message) { }
        public SchemaValidationFailedException(Exception innerException) : base(ERRORTEXT, innerException) { }
        public SchemaValidationFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
