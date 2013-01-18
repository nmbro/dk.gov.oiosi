using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.security {
    /// <summary>
    /// Exception thrown when attemting to construct an OCES employee certificate from an
    /// certificate that is not an oces employee certificate.
    /// </summary>
    public class NotAValidOcesEmployeeCertificateException : CertificateHandlingException {
        /// <summary>
        /// Constructor that takes the certificate attempted to initialize the OCES 
        /// employee certificate with.
        /// </summary>
        /// <param name="certificate"></param>
        public NotAValidOcesEmployeeCertificateException(X509Certificate2 certificate) : base(KeywordsFromX509Certificate2.GetKeywords(certificate)) { }
    }
}
