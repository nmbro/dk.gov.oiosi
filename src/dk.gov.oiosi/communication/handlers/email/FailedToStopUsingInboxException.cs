using System;
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.communication.handlers.email {
    /// <summary>
    /// Exception thrown if the inboxfactory fails to stop the ussage of an inbox
    /// </summary>
    public class FailedToStopUsingInboxException : MailHandlerException {
        /// <summary>
        /// Constuctor that takes the user and the inner exception as parameters.
        /// The user is the one attempted to use when stopping to use an inbox. The
        /// inner exception is the reason why it failed.
        /// </summary>
        /// <param name="user">The user used</param>
        /// <param name="innerException">The exception caught</param>
        public FailedToStopUsingInboxException(object user, Exception innerException) : base(KeywordFromString.GetKeyword("user", user.ToString()), innerException) {  }
    }
}
