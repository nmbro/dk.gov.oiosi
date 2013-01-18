using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.security.lookup {


    /// <summary>
    /// TODO: document this
    /// </summary>
    public class CertificateStoreIdentificationAppConfigurationCollection : ConfigurationElementCollection {

        protected override ConfigurationElement CreateNewElement() {
            return new CertificateStoreIdentificationAppConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            var certificateStoreIdentification = (CertificateStoreIdentificationAppConfiguration)element;
            return certificateStoreIdentification.Name;
        }
    }
}
