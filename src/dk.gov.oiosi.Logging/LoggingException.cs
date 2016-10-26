using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi
{
    /// <summary>
    /// A logging exception
    /// </summary>
    [Serializable]
    public class LoggingException : Exception
    {
        /// <summary>
        /// Logging exception
        /// </summary>
        public LoggingException()
            : base()
        { }

        /// <summary>
        /// Logging exception
        /// </summary>
        /// <param name="message"></param>
        public LoggingException(string message)
            : base(message)
        { }

        /// <summary>
        /// Logging exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public LoggingException(string message, Exception exception)
            : base(message, exception)
        { }
    }
}
