using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Text;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.authorisation {
    /// <summary>
    /// The binding extension element of the server authorisation interceptor and
    /// implements the configuration of the interceptor as well.
    /// </summary>
    public class ServerAuthorisationBindingExtensionElement : BindingElementExtensionElement {
        private const string IMPLEMENTATIONNAMESPACECLASS = "ImplementationNamespaceClass";
        private const string IMPLEMENTATIONASSEMBLY = "ImplementationAssembly";

        #region override BindingElementExtensionElement
        /// <summary>
        /// Override
        /// </summary>
        public override Type BindingElementType {
            get { return typeof(ServerAuthorisationBindingElement); }
        }
        /// <summary>
        /// Override
        /// </summary>
        /// <returns></returns>
        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement() {
            return new ServerAuthorisationBindingElement(this);
        }
        #endregion

        /// <summary>
        /// Gets the implementation class
        /// </summary>
        [ConfigurationProperty(IMPLEMENTATIONNAMESPACECLASS)]
        public string ImplementationNamespaceClass {
            get { return (string)base[IMPLEMENTATIONNAMESPACECLASS]; }
        }

        /// <summary>
        /// Gets the implementation assembly
        /// </summary>
        [ConfigurationProperty(IMPLEMENTATIONASSEMBLY)]
        public string ImplementationAssembly {
            get { return (string)base[IMPLEMENTATIONASSEMBLY]; }
        }
    }
}
