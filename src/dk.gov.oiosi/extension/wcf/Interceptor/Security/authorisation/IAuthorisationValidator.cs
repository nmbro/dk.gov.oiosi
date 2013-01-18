using System.Security.Cryptography.X509Certificates;
using System.Xml;
using dk.gov.oiosi.communication.configuration;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.authorisation {
    /// <summary>
    /// The athorisation validator interface used by the serverside authorisation 
    /// interceptor.
    /// </summary>
    public interface IAuthorisationValidator {
        /// <summary>
        /// Ask to authorise the caller with the given certificate, payload (XML-document)
        /// and with the given document type.
        /// 
        /// Throw an exception if the validation process fails.
        /// </summary>
        /// <param name="certificate">The certificate used by sender</param>
        /// <param name="xmlDocument">The payload send by sender</param>
        /// <param name="documentType">The documenttype of the payload send by sender</param>
        /// <returns>True for accept and false for reject</returns>
        bool Authorise(X509Certificate2 certificate, XmlDocument xmlDocument, DocumentTypeConfig documentType);
    }
}
