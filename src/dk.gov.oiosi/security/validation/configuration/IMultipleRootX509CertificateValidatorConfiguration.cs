using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.security.validation.configuration {

    public interface IMultipleRootX509CertificateValidatorConfiguration {
        IEnumerable<ICertificateStoreIdentification> TrustedRootCertificates { get; }
    }
}
