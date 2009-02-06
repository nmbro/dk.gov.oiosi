using System;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.common;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.raspProfile.extension.wcf.Interceptor.CustomHeader;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.xpath;

namespace dk.gov.oiosi.raspProfile.communication
{
    /// <summary>
    /// This class incapsulates a Request object. It extends its functionailty by 
    /// adding Rasp custom headers to the message in the GetResponse and BeginGetResponse methods.
    /// </summary>
    public class RaspRequest : IRaspRequest
    {
        private readonly Request incapsulatedRequest;

        /// <summary>
        /// Constructs a new RaspRequest given a Request object
        /// </summary>
        /// <param name="request">The Request object to be incapsulated</param>
        public RaspRequest(Request request)
        {
            incapsulatedRequest = request;
        }

        /// <summary>
        /// Adds custom RASP headers to a given OiosiMessage
        /// </summary>
        /// <param name="message">The message on which the headers should be added</param>
        /// <param name="documentId">The document Id used in the MessageIdentifier header</param>
        public static void AddCustomHeaders(OiosiMessage message, string documentId) {
            var typeSearcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig docTypeConfig = typeSearcher.FindUniqueDocumentType(message.MessageXml);

            // Add custom headers to the message
            string senderID = DocumentXPathResolver.GetElementValueByXpath(
                message.MessageXml,
                docTypeConfig.EndpointType.SenderKey.XPath,
                docTypeConfig.Namespaces);

            string receiverID = DocumentXPathResolver.GetElementValueByXpath(
                message.MessageXml,
                docTypeConfig.EndpointType.Key.XPath,
                docTypeConfig.Namespaces);
            string key = PartyIdentifierHeaderSettings.MessagePropertyKey;
            var partyIdentifierSetting = new PartyIdentifierHeaderSettings(senderID, receiverID);

            message.UbiquitousProperties[key] = partyIdentifierSetting;

            foreach (CustomHeaderXPathConfiguration xpath in docTypeConfig.CustomHeaderConfiguration.XPaths)
            {
                string value = DocumentXPathResolver.GetElementValueByXpath(
                    message.MessageXml,
                    xpath.XPath,
                    docTypeConfig.Namespaces);

                var qualifiedName = new XmlQualifiedName(xpath.Name, Definitions.DefaultOiosiNamespace2007);
                message.MessageHeaders[qualifiedName] = MessageHeader.CreateHeader(qualifiedName.Name,
                                                                                        qualifiedName.Namespace, value);
            }

            // Add the MessageIdentifier header
            var messageIdentifierHeaderName = new XmlQualifiedName("MessageIdentifier",
                                                                   Definitions.DefaultOiosiNamespace2007);
            message.MessageHeaders[messageIdentifierHeaderName] =
                MessageHeader.CreateHeader(messageIdentifierHeaderName.Name, messageIdentifierHeaderName.Namespace,
                                           documentId);

            // Change the SOAP actions
            if (!string.IsNullOrEmpty(docTypeConfig.EndpointType.RequestAction))
                message.RequestAction = docTypeConfig.EndpointType.RequestAction;
            if (!string.IsNullOrEmpty(docTypeConfig.EndpointType.ReplyAction))
                message.ReplyAction = docTypeConfig.EndpointType.ReplyAction;
        }

        #region IRaspRequest Members
        
        /// <summary>
        /// Synchronously sends a request and gets a response
        /// </summary>
        /// <param name="response">The response. If this parameter is set the sending went well and the response is safe to use</param>
        /// <param name="documentId">The document Id used in the MessageIdentifier header</param>
        /// <param name="request">Request message</param>
        public void GetResponse(OiosiMessage request, string documentId, out Response response)
        {
            AddCustomHeaders(request, documentId);
            incapsulatedRequest.GetResponse(request, out response);
        }

        /// <summary>
        /// Asynchronously starts sending a request
        /// </summary>
        /// <param name="message">Request message</param>
        /// <param name="documentId">The document Id used for the custom headers</param>
        /// <param name="callback">The asynchronous callback</param>
        /// <returns>Returns an IAsyncResult object</returns>
        public IAsyncResult BeginGetResponse(OiosiMessage message, string documentId, AsyncCallback callback)
        {
            AddCustomHeaders(message, documentId);
            return incapsulatedRequest.BeginGetResponse(message, callback);
        }

        /// <summary>
        /// Asynchronously ends sending a request
        /// </summary>
        /// <returns>Response message</returns>
        public Response EndGetResponse(IAsyncResult asyncResult)
        {
            return incapsulatedRequest.EndGetResponse(asyncResult);
        }

        /// <summary>
        /// Closes the request
        /// </summary>
        public void Close()
        {
            incapsulatedRequest.Close();
        }

        /// <summary>
        /// Aborts the request
        /// </summary>
        public void Abort()
        {
            incapsulatedRequest.Abort();
        }

        /// <summary>
        /// Property for credentials
        /// </summary>
        public Credentials Credentials
        {
            get
            {
                return incapsulatedRequest.Credentials;
            }
            set
            {
                incapsulatedRequest.Credentials = value;
            }
        }

        /// <summary>
        /// Property for the send policy
        /// </summary>
        public SendPolicy Policy
        {
            get
            {
                return incapsulatedRequest.Policy;
            }
            set
            {
                incapsulatedRequest.Policy = value;
            }
        }

        /// <summary>
        /// Property for the request uri
        /// </summary>
        public Uri RequestUri
        {
            get { return incapsulatedRequest.RequestUri; }
        }

        #endregion
    }
}
