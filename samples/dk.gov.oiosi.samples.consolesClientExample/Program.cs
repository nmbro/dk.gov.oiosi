using System;
using System.Xml;
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.test.request;
using dk.gov.oiosi.raspProfile.communication;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.samples.consoleClientExample {

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
        static IRaspRequest request;
     
        static void Main(string[] args) {

            // Print title
            Console.Title = "  OIOSI Extended Request Test Application  ";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  OIOSI Extended Request Test Application  ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine("Select if the test or the live NemHandel Infrastruktur should be used:");
            Console.Write(" Type 'Live' to use the live system: ");
            string answer = Console.ReadLine();

            if (string.IsNullOrEmpty(answer))
            {
                ConfigurationDocument.ConfigFilePath = "RaspConfiguration.Test.xml";
            }
            else
            {
                if (string.Equals("Live", answer, StringComparison.OrdinalIgnoreCase))
                {
                    ConfigurationDocument.ConfigFilePath = "RaspConfiguration.Live.xml";
                }
                else
                {
                    ConfigurationDocument.ConfigFilePath = "RaspConfiguration.Test.xml";
                }
            }

            while (true) {
                try
                {

                    // Print title
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  OIOSI Extended Request Test Application  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine();

                    if (string.Equals("Live", answer, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("The '");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Live/Productiv");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("' NemHandel infrastruktur is used.");
                    }
                    else
                    {
                        Console.Write("The '");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Test");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("' NemHandel infrastruktur is used.");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine();
                    // Let the user select an XML document to send
                    XmlDocument xdoc = GUI.LoadXmlDocument();

                    // Create the OIOSI message object to send, and add the mandatory MessageIdentifier header
                    OiosiMessage message = new OiosiMessage(xdoc);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.WriteLine("File identified - Start preparing");
                    Console.ForegroundColor = ConsoleColor.White;

                    // Prepare the request
                    request = Preparation.PrepareRequest(message);

                    if (request != null)
                    {
                        // Let the user configure his mail account
                        
                        if (request.RequestUri.Scheme == "mailto")
                            GUI.GetMailSettings(request);

                        // Use the OIOSI library class Request to send the document
                        Console.WriteLine("Starting to send...");
                        Response response;
                        request.GetResponse(message, out response, Guid.NewGuid().ToString());

                        // Print out the reply
                        GUI.PrintResponse(response);

                    }
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
