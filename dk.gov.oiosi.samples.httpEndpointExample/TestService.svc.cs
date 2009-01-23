using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.service;

namespace dk.gov.oiosi.samples.httpEndpointExample
{

    /// <summary>
    /// The service implementation
    /// Implements the general RASP contract and takes any form of SOAP (hence the Message object as a parameter)
    /// </summary>
    public class TestService : IServiceContract
    {
        public Message RequestRespond(Message request)
        {
            // Return the same message you got
            return request;
        }
    }
}
