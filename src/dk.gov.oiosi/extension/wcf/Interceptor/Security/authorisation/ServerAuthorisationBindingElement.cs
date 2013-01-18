using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using dk.gov.oiosi.common;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.xml.documentType;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.authorisation {
    /// <summary>
    /// Implements the server authorisation interceptor, that can be extended by
    /// creating implementations of the IAthorisationValidator.
    /// </summary>
    public class ServerAuthorisationBindingElement : CommonBindingElement {
        private ServerAuthorisationBindingExtensionElement _configuration;
        private IAuthorisationValidator _authoriser;

        /// <summary>
        /// Constructor that takes the binding extension element as the configuration
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public ServerAuthorisationBindingElement(ServerAuthorisationBindingExtensionElement configuration) {
            _configuration = configuration;
            ExternalCodeFactory authoriserFactory = new ExternalCodeFactory();
            string implementationNamespaceClass = _configuration.ImplementationNamespaceClass;
            string implementationAssembly = configuration.ImplementationAssembly;
            _authoriser = authoriserFactory.CreateInstance<IAuthorisationValidator>(implementationNamespaceClass, implementationAssembly);
        }

        /// <summary>
        /// Copy constructor that takes the binding extension element as the configuration
        /// and the IAuthoriser used to authorise incoming messages.
        /// </summary>
        /// <param name="configuration">The configuration</param>
        /// <param name="authoriser"></param>
        public ServerAuthorisationBindingElement(ServerAuthorisationBindingExtensionElement configuration, IAuthorisationValidator authoriser) {
            _configuration = configuration;
            _authoriser = authoriser;
        }

        /// <summary>
        /// Overrides the abstract method and implements the basic intercept on a
        /// request.
        /// </summary>
        /// <param name="interceptorMessage"></param>
        public override void InterceptRequest(InterceptorMessage interceptorMessage) {
            try {
                X509Certificate2 certificate = interceptorMessage.Certificate;
                XmlDocument xmlDocument = interceptorMessage.GetBody();
                DocumentTypeConfigSearcher searcher = new DocumentTypeConfigSearcher();
                DocumentTypeConfig documentType = searcher.FindUniqueDocumentType(xmlDocument);
                bool authorised = _authoriser.Authorise(certificate, xmlDocument, documentType);
                if (!authorised)
                    throw new NotAuthorisedException(certificate, xmlDocument, documentType);
            } catch (NotAuthorisedException) {
                throw;
            } catch (Exception ex) {
                throw new AuthorisationProcessFailedException(ex);
            }
        }

        /// <summary>
        /// Overrides the abstract method and implements an empty intercept on a
        /// response.
        /// </summary>
        /// <param name="interceptorMessage"></param>
        public override void InterceptResponse(InterceptorMessage interceptorMessage) { }

        /// <summary>
        /// Overrides the abstract function and returns true
        /// </summary>
        public override bool DoesRequestIntercept {
            get { return true; }
        }

        /// <summary>
        /// Overrides the abstract function and returns false
        /// </summary>
        public override bool DoesResponseIntercept {
            get { return false; }
        }

        /// <summary>
        /// Overrides the abstract function and returns true
        /// </summary>
        public override bool DoesFaultOnRequestException {
            get { return true; }
        }

        /// <summary>
        /// Overrides the clone method.
        /// </summary>
        /// <returns></returns>
        public override System.ServiceModel.Channels.BindingElement Clone() {
            return new ServerAuthorisationBindingElement(_configuration, _authoriser);
        }
    }
}
