using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// Exception thrown when one tries to use a faulted mailbox
    /// </summary>
    public class MailboxFaultedException : MainException {
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public MailboxFaultedException() : base() { }
        
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="innerException">An inner exception</param>
        public MailboxFaultedException(System.Exception innerException) : base(innerException) { }
        
    }
}
