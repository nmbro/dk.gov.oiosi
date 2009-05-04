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
            DocumentTypeConfig docTypeConfig = typeSearcher.FindUniqueDocumentType(new OiosiMessage(request).MessageXml);

            // Create the reply message
            string body = "Request was received " + DateTime.Now.ToString();
            return Message.CreateMessage(MessageVersion.Soap12WSAddressing10,
                docTypeConfig.EndpointType.ReplyAction, body);
        }
    }
}
