using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi
{
    /// <summary>
    /// A logging exception
    /// </summary>
    public class LoggingException : Exception
    {
        public LoggingException()
            : base()
        { }

        public LoggingException(string message)
            : base(message)
        { }

        public LoggingException(string message, Exception exception)
            : base(message, exception)
        { }
    }
}
