using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ServiceModel.Channels;
using System.Security.Cryptography.X509Certificates;

using dk.gov.oiosi.communication;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.test.request {

    /// <summary>
    /// Test application for the dk.gov.oiosi.communication.Request class.
    /// 
    /// Lessons
    /// --------
    /// - Programatically set up the request: see region 1
    /// - Set up the request in config: see region 2
    /// - Programatically configuring mail accounts: see region 3 in the file GUI.cs
    /// 
    /// </summary>
    public class Program {

        // The request object used for sending
        static Request request;

        static void Main(string[] args) {

            while (true) 
            {
                try {

                    GUI.MenuChoice mode = GUI.DisplayMenu();

                    // Let the user select an XML document to send
                    XmlDocument xdoc = GUI.LoadXmlDocument();
                    Response response;

                    // Create the OIOSI message object to send, and add the mandatory MessageIdentifier header
                    OiosiMessage message = new OiosiMessage(xdoc);
                    AddMandatoryRaspHeaders(message);

                    // If the user would like to set proxy config programatically
                    if (mode == GUI.MenuChoice.Custom) {

                        #region 1 - Programatically configured Request
                        
                        // Let the user select a remote endpoint to which the document will be sent
                        Uri endpoint = GUI.GetEndpointAddress();

                        // Let the user select credentials
                        Console.WriteLine("\nPlease configure the certificate used for sending\n----------------------------------------------------");
                        X509Certificate2 clientCert = GUI.GetCertificate();
                        Console.WriteLine("\nPlease configure the certificate used by the remote endpoint\n-------------------------------------------------------------------------");
                        X509Certificate2 serverCert = GUI.GetCertificate();
                        Credentials credentials = new Credentials(new OcesX509Certificate(clientCert), new OcesX509Certificate(serverCert));
                        
                        // Create the Request object
                        request = new Request(endpoint, credentials);

                        // Let the user configure his mail account
                        if (endpoint.Scheme == "mailto")
                            GUI.GetMailSettings(request);

                        // Use the OIOSI library class Request to send the document
                        Console.WriteLine("Starting to send...");
                        request.GetResponse(message, out response);
                        
                        #endregion
                    }

                    // The user would like to use the proxy settings in app.config
                    else {

                        #region 2 - Request configured in App.Config

                        // Use the OIOSI library class Request to send the document
                        Console.WriteLine("Starting to send...");
                        request = new Request("OiosiHttpEndpoint");
                        request.GetResponse(message, out response);

                        #endregion
                    }
                    
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
