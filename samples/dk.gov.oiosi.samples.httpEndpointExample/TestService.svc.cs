using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.communication.fault;
using dk.gov.oiosi.communication.service;
using dk.gov.oiosi.raspProfile.communication.service;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.common;
using System.Xml;

namespace dk.gov.oiosi.samples.httpEndpointExample
{

    /// <summary>
    /// The service implementation
    /// Implements the general RASP contract and takes any form of SOAP (hence the Message object as a parameter)
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults=true)]
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
            string body = "Request was received " + DateTime.Now.ToString();
            Message message = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, docTypeConfig.EndpointType.ReplyAction, body);
            
            return message;
        }
    }
}
