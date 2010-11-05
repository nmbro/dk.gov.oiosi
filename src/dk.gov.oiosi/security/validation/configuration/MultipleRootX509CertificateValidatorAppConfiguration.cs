using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.security.validation.configuration {

    /// <summary>
    /// TODO: document this
    /// </summary>
    public class MultipleRootX509CertificateValidatorAppConfiguration : ConfigurationSection, IMultipleRootX509CertificateValidatorConfiguration {
        public const string MultipleRootX509CertificateValidatorAppConfigurationName = "multipleRootX509CertificateValidatorConfiguration";
        public const string CertificateStoreIdentificationAppConfigurationCollectionName = "rootCertificateCollection";

        #region IMultipleRootX509CertificateValidatorConfiguration Members

        public IEnumerable<ICertificateStoreIdentification> TrustedRootCertificates {
            get { throw new NotImplementedException(); }
        }

        #endregion

        [ConfigurationProperty(CertificateStoreIdentificationAppConfigurationCollectionName, IsRequired = false)]
        public CertificateStoreIdentificationAppConfigurationCollection CertificateStoreIdentificationConfigurationCollection {
            get { return (CertificateStoreIdentificationAppConfigurationCollection)this[CertificateStoreIdentificationAppConfigurationCollectionName]; }
        }
    }
}
