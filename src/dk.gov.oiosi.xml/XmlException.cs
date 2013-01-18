using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml {
    public class XmlException : Exception {
        private const string ERRORTEXT = "An xml exception has occured.";
        public XmlException() : base(ERRORTEXT) { }
        public XmlException(string message) : base(message) { }
        public XmlException(Exception innerException) : base(ERRORTEXT, innerException) { }
        public XmlException(string message, Exception innerException) : base(message, innerException) { }
    }
}
