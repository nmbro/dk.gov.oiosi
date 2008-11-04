using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.common {
    public class ExternalCodeFactoryAppConfiguration : ConfigurationElement, IExternalCodeFactoryConfiguration {
        public const string ImplementationAssemblyName = "implementationAssembly";
        public const string ImplementationNamespaceClassName = "implementationNamespaceClass";

        #region IExternalCodeFactoryConfiguration Members

        [ConfigurationProperty(ImplementationAssemblyName, IsRequired = true)]
        public string ImplementationAssembly {
            get { return (string)this[ImplementationAssemblyName]; }
        }

        [ConfigurationProperty(ImplementationNamespaceClassName, IsRequired = true)]
        public string ImplementationNamespaceClass {
            get { return (string)this[ImplementationNamespaceClassName]; }
        }

        #endregion
    }
}
