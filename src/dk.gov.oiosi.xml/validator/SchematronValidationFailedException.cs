using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.validator {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class SchematronValidationFailedException : Exception
    {
        private const string ERRORTEXT = "A schematron exception has occured.";
        public SchematronValidationFailedException() : base(ERRORTEXT) { }
        public SchematronValidationFailedException(string message) : base(message) { }
        public SchematronValidationFailedException(Exception innerException) : base(ERRORTEXT, innerException) { }
        public SchematronValidationFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
