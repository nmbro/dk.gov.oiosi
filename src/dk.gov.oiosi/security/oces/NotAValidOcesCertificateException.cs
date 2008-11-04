using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.security {
    /// <summary>
    /// Exception thrown if the certificate given is not a valid certificate
    /// </summary>
    public class NotAValidOcesCertificateException : CertificateHandlingException {
        /// <summary>
        /// Constructor that takes the certificate that is not a valid oces certificate as
        /// parameter.
        /// </summary>
        /// <param name="certificate"></param>
        public NotAValidOcesCertificateException(X509Certificate2 certificate) : base(KeywordsFromX509Certificate2.GetKeywords(certificate)) {}
    }
}
