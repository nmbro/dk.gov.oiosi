using System;
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
        public AuthorisationProcessFailedException(Exception innerException) : base(OiosiFaultCode.Receiver, OiosiInnerFaultCode.InternalSystemFailureFault, innerException) { }
    }
}
