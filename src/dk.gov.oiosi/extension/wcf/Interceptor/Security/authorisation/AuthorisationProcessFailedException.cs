using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

using dk.gov.oiosi.exception.Keyword;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.communication.fault;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.authorisation {
    /// <summary>
    /// Exception thrown if the authorisation process fails.
    /// </summary>
    public class AuthorisationProcessFailedException : InterceptorChannelException {
        /// <summary>
        /// Constructor that takes an inner exception as the reasone to why
        /// the process fails.
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        public AuthorisationProcessFailedException(Exception innerException) : base(OiosiMessageFault.OiosiFaultCode.Reciever, OiosiMessageFault.OiosiInnerFaultCode.InternalSystemFailureFault, innerException) { }
    }
}
