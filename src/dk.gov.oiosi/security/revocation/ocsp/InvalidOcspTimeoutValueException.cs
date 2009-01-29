using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;

using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.security.revocation.ocsp {
    /// <summary>
    /// Exception thrown if the OCSP timeout value is invalid.
    /// </summary>
    public class InvalidOcspTimeoutValueException : RevocationException {
       /// <summary>
       /// Constructor that takes the timeout value that is incorrect and 
       /// the min value and max value as a parameter
       /// </summary>
       /// <param name="timeoutValue"></param>
       /// <param name="minValue"></param>
       /// <param name="maxValue"></param>
        public InvalidOcspTimeoutValueException(int timeoutValue, int minValue, int maxValue) : base(KeywordFromNumber.GetKeyword("timeoutvalue", timeoutValue)) { }

        private static Dictionary<string, string> GetKeywords(int timeoutValue, int minValue, int maxValue) {
            Dictionary<string, string> keywords = KeywordFromNumber.GetKeyword("timeoutvalue", timeoutValue);
            KeywordFromNumber.GetKeyword(keywords, "minvalue", minValue);
            KeywordFromNumber.GetKeyword(keywords, "maxvalue", maxValue);
            return keywords;
        }
    }
}
