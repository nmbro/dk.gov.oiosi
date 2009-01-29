using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Security.Cryptography.X509Certificates;

using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.security.ocsp {
    /// <summary>
    /// Exception thrown when the given certificate is not an OCES certifcate.
    /// </summary>
    public class InvalidOcesCertificateException : OcspException {
        /// <summary>
        /// Constructor that takes the certificate that is not an OCES certificate as
        /// parameter.
        /// </summary>
        /// <param name="certificate"></param>
        public InvalidOcesCertificateException(X509Certificate2 certificate) : base(KeywordsFromX509Certificate2.GetKeywords(certificate)) { }
    }
}
