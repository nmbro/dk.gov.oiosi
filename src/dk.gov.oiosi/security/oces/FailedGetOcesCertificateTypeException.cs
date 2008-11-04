using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Security.Cryptography.X509Certificates;

using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.security.oces {
    /// <summary>
    /// Exception thrown if it fails to get the OCES certificate from the given 
    /// certifcate.
    /// </summary>
    public class FailedGetOcesCertificateTypeException : OcesCertificateException {
        /// <summary>
        /// Constructor that takes the certificate and an exception as the reason as 
        /// parameter.
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="innerException"></param>
        public FailedGetOcesCertificateTypeException(X509Certificate2 certificate, Exception innerException) : base(KeywordsFromX509Certificate2.GetKeywords(certificate), innerException) { }
    }
}
