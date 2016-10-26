using System;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.common;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.raspProfile.extension.wcf.Interceptor.CustomHeader;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.xpath;
using dk.gov.oiosi.addressing;

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
        private void AddCustomHeaders(OiosiMessage message, string documentId) {
            var typeSearcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig docTypeConfig = typeSearcher.FindUniqueDocumentType(message.MessageXml);

            // Add custom headers to the message
            Identifier senderIdentifier = getSenderID(message, docTypeConfig);
            Identifier receiverIdentifier = GetReceiverID(message, docTypeConfig);

            string key = PartyIdentifierHeaderSettings.MessagePropertyKey;
            var partyIdentifierSetting = new PartyIdentifierHeaderSettings(senderIdentifier, receiverIdentifier);
            message.UbiquitousProperties[key] = partyIdentifierSetting;

            // Adds custom headers by xpaths
            foreach (CustomHeaderXPathConfiguration xpath in docTypeConfig.CustomHeaderConfiguration.XPaths) {
                var value = DocumentXPathResolver.GetElementValueByXPathNavigator(message.MessageXml, xpath.XPath, docTypeConfig.Namespaces);
                var qualifiedName = new XmlQualifiedName(xpath.Name, Definitions.DefaultOiosiNamespace2007);
                message.MessageHeaders[qualifiedName] = MessageHeader.CreateHeader(qualifiedName.Name, qualifiedName.Namespace, value);
            }

            // Add the MessageIdentifier header
            var messageIdentifierHeaderName = new XmlQualifiedName("MessageIdentifier", Definitions.DefaultOiosiNamespace2007);
            message.MessageHeaders[messageIdentifierHeaderName] = MessageHeader.CreateHeader(messageIdentifierHeaderName.Name, messageIdentifierHeaderName.Namespace, documentId);

            // Change the SOAP actions
            if (!string.IsNullOrEmpty(docTypeConfig.EndpointType.RequestAction))
                message.RequestAction = docTypeConfig.EndpointType.RequestAction;
            if (!string.IsNullOrEmpty(docTypeConfig.EndpointType.ReplyAction))
                message.ReplyAction = docTypeConfig.EndpointType.ReplyAction;
        }

        private Identifier GetReceiverID(OiosiMessage message, DocumentTypeConfig docTypeConfig)
        {
            string receiverID = DocumentXPathResolver.GetElementValueByXPathNavigator(
                            message.MessageXml,
                            docTypeConfig.EndpointType.Key.XPath,
                            docTypeConfig.Namespaces);
            var receiverKeyType = Utilities.GetEndpointKeyTypeCode(message.MessageXml, docTypeConfig);
            Identifier id = IdentifierUtility.GetIdentifierFromKeyType(receiverID, receiverKeyType);

            if (!id.IsAllowedInPublic)
            {
                string cvrNumberString;
                string endpointType;
                if (Credentials.ServerCertificate.TryGetCvrNumberString(out endpointType, out cvrNumberString))
                {
                    id = new Identifier(endpointType, cvrNumberString);
                }
                else
                {
                    throw new Exception("Receiver ID is not allowed to be added to header. It was not possible to retrieve ID from certificate");
                }
            }

            return id;
        }

        private Identifier getSenderID(OiosiMessage message, DocumentTypeConfig docTypeConfig)
        {
            string senderID = DocumentXPathResolver.GetElementValueByXPathNavigator(
                            message.MessageXml,
                            docTypeConfig.EndpointType.SenderKey.XPath,
                            docTypeConfig.Namespaces);
            var senderKeyType = Utilities.GetSenderKeyTypeCode(message.MessageXml, docTypeConfig);
            Identifier id = IdentifierUtility.GetIdentifierFromKeyType(senderID, senderKeyType);

            if (!id.IsAllowedInPublic)
            {
                string cvrNumberString;
                string endpointType;
                if (Credentials.ClientCertificate.TryGetCvrNumberString(out endpointType, out cvrNumberString))
                {
                    id = new Identifier(endpointType, cvrNumberString);
                }
                else
                {
                    throw new Exception("Sender ID is not allowed to be added to header. It was not possible to retrieve ID from certificate");
                }
            }

            return id;
        }

        #region IRaspRequest Members


        /// <summary>
        /// Synchronously sends a request and gets a response
        /// </summary>
        /// <param name="request">Request message</param>
        /// <param name="documentId">The document Id used in the MessageIdentifier header</param>
        /// <param name="response">The response. If this parameter is set the sending went well and the response is safe to use</param>
        public void GetResponse(OiosiMessage request, string documentId, out Response response)
        {
            // Validate the OiosiMessage
            SendingValidation sendingValidation = new SendingValidation();
            sendingValidation.Validate(request);

            this.AddCustomHeaders(request, documentId);
            this.incapsulatedRequest.GetResponse(request, out response);
        }

        /// <summary>
        /// GetResponse
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="documentId"></param>
        [Obsolete("void GetResponse(OiosiMessage request, out Response response, string documentId", false)]
        public void GetResponse(OiosiMessage request, out Response response, string documentId)
        {
            this.GetResponse(request, documentId, out response);
        }

        /// <summary>
        /// Asynchronously starts sending a request
        /// </summary>
        /// <param name="message">Request message</param>
        /// <param name="documentId">The document Id used for the custom headers</param>
        /// <param name="response">The response object</param>
        /// <param name="callback">The asynchronous callback</param>
        /// <returns>Returns an IAsyncResult object</returns>
        public IAsyncResult BeginGetResponse(OiosiMessage message, string documentId, out Response response, AsyncCallback callback)
        {
            // Validate the OiosiMessage
            SendingValidation sendingValidation = new SendingValidation();
            sendingValidation.Validate(message);

            AddCustomHeaders(message, documentId);
            return this.incapsulatedRequest.BeginGetResponse(message, out response, callback);
        }

        /// <summary>
        /// Asynchronously ends sending a request
        /// </summary>
        /// <returns>Response message</returns>
        public void EndGetResponse(IAsyncResult asyncResult, out Response response)
        {
            this.incapsulatedRequest.EndGetResponse(asyncResult, out response);
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
