using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using dk.gov.oiosi.communication;
using System.Xml;
using System.ServiceModel.Channels;

namespace dk.gov.oiosi.integration.communication
{
    [TestFixture]
    public class RequestTest
    {
        [Test]
        public void TestHttpRequest()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("../../resources/Invoice.2.01.xml");
            
            OiosiMessage message = new OiosiMessage(xdoc);
            AddMandatoryRaspHeaders(message);
            
            Response response;
            Request request = new Request("OiosiHttpEndpoint");
            request.GetResponse(message, out response);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ResponseMessage);
            Assert.IsNotNull(response.ResponseMessage.MessageId);
        }
       

        /// <summary>
        /// Adds the MessageIdentifier
        /// </summary>
        public static void AddMandatoryRaspHeaders(OiosiMessage msg)
        {
            // Set the name of the header
            XmlQualifiedName headerName = new XmlQualifiedName("MessageIdentifier", common.Definitions.DefaultOiosiNamespace2007);

            // Create a WCF header
            MessageHeader header = MessageHeader.CreateHeader(headerName.Name, headerName.Namespace, "1234567890");

            // Add it to our OIOSI Message
            msg.MessageHeaders.Add(headerName, header);
        }
    }
}
