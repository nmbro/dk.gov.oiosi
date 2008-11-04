using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.common {
    public interface IExternalCodeFactoryConfiguration {
        string ImplementationAssembly { get; }
        string ImplementationNamespaceClass { get; }
    }
}
