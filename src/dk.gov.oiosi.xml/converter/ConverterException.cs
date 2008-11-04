using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.converter {
    public class ConverterException : Exception {
        private const string ERRORTEXT = "A converter exception has occured.";
        public ConverterException() : base(ERRORTEXT) { }
        public ConverterException(string message) : base(message) { }
        public ConverterException(Exception innerException) : base(ERRORTEXT, innerException) { }
        public ConverterException(string message, Exception innerException) : base(message, innerException) { }
    }
}
