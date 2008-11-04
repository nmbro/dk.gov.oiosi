using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ServiceModel.Channels;
using System.Security.Cryptography.X509Certificates;

using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.security.ldap;
using dk.gov.oiosi.security.lookup;
using dk.gov.oiosi.security.ocsp;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.extension.wcf.EmailTransport;

using dk.gov.oiosi.test.request;

namespace dk.gov.oiosi.test.extendedRequest {

    /// <summary>
    /// Extended Request example
    /// 
    /// This example demonstrates how to perform the UDDI, LDAP and OCSP lookups needed
    /// to make a full OIOSI RASP service call.
    /// 
    /// The code unique to this test application can be found in the file Preparation.cs
    /// </summary>
    public class Program {

        // The request object used for sending
        static Request request;
     
        static void Main(string[] args) {
            while (true) {
                try {
                    // Print title
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("OIOSI Extended Request Test Application\n-----------------------------------\n");
                    Console.ForegroundColor = ConsoleColor.White;


                    // Let the user select an XML document to send
                    XmlDocument xdoc = GUI.LoadXmlDocument();

                    // Create the OIOSI message object to send, and add the mandatory MessageIdentifier header
                    OiosiMessage message = new OiosiMessage(xdoc);
                    AddMandatoryRaspHeaders(message);
                    
                    // Prepare the request
                    request = Preparation.PrepareRequest(message);

                    // Let the user configure his mail account
                    if (request.RequestUri.Scheme == "mailto")
                        GUI.GetMailSettings(request);

                    // Use the OIOSI library class Request to send the document
                    Console.WriteLine("Starting to send...");
                    Response response;
                    request.GetResponse(message, out response);

                    // Print out the reply
                    GUI.PrintResponse(response);

                }
                catch (Exception e) {
                    GUI.PrintException(e);
                }


                // Ask the user if he wants to send again
                if (!GUI.AskIfRestart())
                    break;

                Console.WriteLine("\n");
            }


        }

        /// <summary>
        /// Adds the MessageIdentifier
        /// </summary>
        /// <param name="msg"></param>
        public static void AddMandatoryRaspHeaders(OiosiMessage msg) {
            // Set the name of the header
            XmlQualifiedName headerName = new XmlQualifiedName("MessageIdentifier", common.Definitions.DefaultOiosiNamespace2007);

            // Create a WCF header
            MessageHeader header = MessageHeader.CreateHeader(headerName.Name, headerName.Namespace, "1234567890");

            // Add it to our OIOSI Message
            msg.MessageHeaders.Add(headerName, header);
        }
    }
}
