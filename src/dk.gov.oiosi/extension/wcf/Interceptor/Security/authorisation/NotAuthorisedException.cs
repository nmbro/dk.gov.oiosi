using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.communication.fault;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.authorisation {
    /// <summary>
    /// Exception thrown if the sender with the given certificate, payload and documenttype
    /// is not authorised.
    /// </summary>
    public class NotAuthorisedException : InterceptorChannelException {
        /// <summary>
        /// Constructor that takes the given certificate, payload and documenttype.
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="xmlDocument"></param>
        /// <param name="documentType"></param>
        public NotAuthorisedException(X509Certificate2 certificate, XmlDocument xmlDocument, DocumentTypeConfig documentType) : base(OiosiMessageFault.OiosiFaultCode.Sender, OiosiMessageFault.OiosiInnerFaultCode.NotAuthorizedFault, Keywords.GetKeywords(certificate, xmlDocument, documentType)) { }
    }
}
