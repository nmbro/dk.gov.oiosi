using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.validator {
    public class SchematronValidationFailedException : Exception {
        private const string ERRORTEXT = "A schematron exception has occured.";
        public SchematronValidationFailedException() : base(ERRORTEXT) { }
        public SchematronValidationFailedException(string message) : base(message) { }
        public SchematronValidationFailedException(Exception innerException) : base(ERRORTEXT, innerException) { }
        public SchematronValidationFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
