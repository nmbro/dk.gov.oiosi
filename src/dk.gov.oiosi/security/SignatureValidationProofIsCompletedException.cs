using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.common.CertificateHandling {

    /// <summary>
    /// Exception thrown during signature validation proof generation
    /// </summary>
    public class SignatureValidationProofIsCompletedException : CertificateHandlingException {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="certificateSubject">The certificate subject</param>
        public SignatureValidationProofIsCompletedException(string certificateSubject) : base(KeywordFromString.GetKeyword("certificatesubject", certificateSubject)) { }
    }
}
