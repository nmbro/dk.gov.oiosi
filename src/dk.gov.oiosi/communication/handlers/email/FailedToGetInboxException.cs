using System;
using System.Collections.Generic;
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.communication.handlers.email {
    /// <summary>
    /// Exception thrown if the inbox factory fails to get an inbox
    /// </summary>
    public class FailedToGetInboxException : MailHandlerException {
        /// <summary>
        /// Constructor that takes the server configuration, inbox implementation, user and
        /// inner exception as parameters. The server configuraiton, inbox implementation and
        /// user is those used when attempting to get the inbox. The inner exception is the 
        /// reason why it fails.
        /// </summary>
        /// <param name="serverConfiguration"></param>
        /// <param name="inBoxImplementationType"></param>
        /// <param name="user"></param>
        /// <param name="innerException"></param>
        public FailedToGetInboxException(IMailServerConfiguration serverConfiguration, Type inBoxImplementationType, object user, Exception innerException) : base(GetKeywords(serverConfiguration, inBoxImplementationType, user), innerException) { }
        
        private static Dictionary<string, string> GetKeywords(IMailServerConfiguration serverConfiguration, Type inBoxImplementationType, object user) {
            Dictionary<string, string> keywords = KeywordFromType.GetKeyword(inBoxImplementationType);
            keywords.Add("mailaddress", serverConfiguration.ReplyAddress);
            KeywordFromString.GetKeyword(keywords, "user", user.ToString());
            return keywords;
        }
    }
}
