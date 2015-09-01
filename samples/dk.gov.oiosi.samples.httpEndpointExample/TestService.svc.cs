using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.communication.service;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.xml.documentType;

namespace dk.gov.oiosi.samples.httpEndpointExample
{
    /// <summary>
    /// The service implementation Implements the general RASP contract and takes any form of SOAP
    /// (hence the Message object as a parameter)
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class TestService : IServiceContract
    {
        public Message RequestRespond(Message request)
        {
            DocumentTypeConfigSearcher typeSearcher = new DocumentTypeConfigSearcher();
            OiosiMessage oiosiMessage = new OiosiMessage(request);

            // Use on of the two methods below, to retrive the document

            // This one is very expensive in in time/CPU.
            //XmlDocument xmlDocument = oiosiMessage.MessageXml;

            // This is mutch faster, as not converstion between xml an string is performed
            string xmlDocumentAsString = oiosiMessage.MessageAsString;

            DocumentTypeConfig docTypeConfig = typeSearcher.FindUniqueDocumentType(oiosiMessage.MessageXml);

            // Create the reply message (The body can be empty)
            string responseText;
            try
            {
                string version = ConfigurationHandler.Version;
                responseText = ConfigurationManager.AppSettings["ResponseText"];
                responseText = string.Format(responseText, DateTime.Now.ToString(), version);
            }
            catch (Exception)
            {
                responseText = "Request was received " + DateTime.Now.ToString();
            }

            string body = responseText;
            Message message = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, docTypeConfig.EndpointType.ReplyAction, body);

            return message;
        }
    }
}