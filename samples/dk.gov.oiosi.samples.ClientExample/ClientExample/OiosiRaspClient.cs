using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.samples.consoleClientExample;
using dk.gov.oiosi.test.request;
using dk.gov.oiosi.raspProfile.communication;
using System.Configuration;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.samples.ClientExample
{
    public class OiosiRaspClient
    {
        private string PATH_INVOICE_XML = "./Resources/xml/TestUddi/TestUddi_Oioubl_Invoice_EndpointNetHttpV1_2_3.xml";

        public UddiType uddiType;

        public string xmlDocumentUrl;

        public OiosiRaspClient()
        {
            this.uddiType = UddiType.Test;
            this.xmlDocumentUrl = this.PATH_INVOICE_XML;
        }

        public OiosiRaspClient(UddiType uddiType, String xmlDocumentUrl)
        {
            this.uddiType = uddiType;
            this.xmlDocumentUrl = xmlDocumentUrl;
        }

        public bool SendDocument()
        {
            bool result = false;

            try
            {
                // define the RaspConfigurationFile to use
                switch (this.uddiType)
                {
                    case UddiType.Production:
                        {
                            ConfigurationManager.AppSettings["RaspConfigurationFile"] = "RaspConfiguration.Live.xml";
                            Console.WriteLine("Sending the document though production uddi.");
                            break;
                        }
                    case UddiType.Test:
                        {
                            ConfigurationManager.AppSettings["RaspConfigurationFile"] = "RaspConfiguration.Test.xml";
                            Console.WriteLine("Sending the document though test uddi.");
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException("The uddiType '" + this.uddiType.ToString() + "' not regonized.");
                        }
                }

                //CacheConfig v = ConfigurationHandler.GetConfigurationSection<CacheConfig>();

                // Load the document
                XmlDocument xdoc = new XmlDocument();
                FileInfo fileInfo = new FileInfo(this.xmlDocumentUrl);

                if (fileInfo.Exists == false)
                {
                    Console.WriteLine("Error - The file does not exist");
                    Console.WriteLine(fileInfo.FullName);
                    // this.Exit();
                    result = false;
                }
                else
                {
                    Console.WriteLine("Start sending the document.");
                    Console.WriteLine(fileInfo.FullName);

                    xdoc.Load(fileInfo.FullName);

                    // Create the OIOSI message object to send, and add the mandatory MessageIdentifier header
                    OiosiMessage message = new OiosiMessage(xdoc);

                    // Prepare the request

                    Preparation preparation = new Preparation();


                    IRaspRequest request = preparation.PrepareRequest(message, this.uddiType);

                    // Let the user configure his mail account
                    if (request.RequestUri.Scheme == "mailto")
                    {
                        //throw new NotImplementedException("Mail sending not implemented.");
                        GUI.GetMailSettings(request);
                    }

                    // Use the OIOSI library class Request to send the document
                    Console.WriteLine("Starting to send...");
                    Response response;
                    request.GetResponse(message, out response, Guid.NewGuid().ToString());

                    // Print out the reply
                    GUI.PrintResponse(response);

                    result = true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            return result;
        }







        private void Exit()
        {
            Console.WriteLine("Press any key to exist.");
            Console.ReadKey();
            System.Environment.Exit(0);
        }
    }
}
