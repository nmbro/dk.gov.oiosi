using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.extension.wcf.EmailTransport;

namespace dk.gov.oiosi.test.request {
    
    public class GUI {

        #region 3 - Programatically configured mail

        /// <summary>
        /// Configures the mail account used for sending
        /// </summary>
        public static void GetMailSettings(Request request) {

            // Read the settings from the console
            Console.WriteLine("\nPlease configure the mail account used for sending");
            Console.WriteLine("----------------------------------------------------");
            Console.Write("\tMail server: ");
            string server = Console.ReadLine();
            Console.Write("\tAccount name: ");
            string account = Console.ReadLine();
            Console.Write("\tPassword: ");
            string password = Console.ReadLine();
            string replyAddress = account + "@" + server;
            Console.WriteLine("\tThe address '" + replyAddress + "' will be used");

            // Configure the mail accounts in the dynamic configuration file
            // This code uses the same server address and account info for sending and receiving
            EmailTransportUserConfig mailConfig = ConfigurationHandler.GetConfigurationSection<EmailTransportUserConfig>();
            mailConfig.SendInbox.ServerAddress = mailConfig.SendOutbox.ServerAddress = server;
            mailConfig.SendInbox.UserName = mailConfig.SendOutbox.UserName = account;
            mailConfig.SendInbox.Password = mailConfig.SendOutbox.Password = password;
            mailConfig.SendInbox.ReplyAddress = mailConfig.SendOutbox.ReplyAddress = "mailto:" + replyAddress;

            // Configure the request to use the settings
            request.Policy.InboxMailConfiguration = mailConfig.SendInbox;
            request.Policy.OutboxMailConfiguration = mailConfig.SendOutbox;
        }
        #endregion

        #region GUI

        // Possible menu choices
        public enum MenuChoice { Default, Custom };

        /// <summary>
        /// Simple menu handling mehtod
        /// </summary>
        public static MenuChoice DisplayMenu() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("OIOSI Request Test Application\n------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  1. Simple Request");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\t\tUses the settings in App.Config");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  2. Customized Request");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\t\tLets the user set up the proxy programatically");
            Console.ForegroundColor = ConsoleColor.White;

            ConsoleKeyInfo key;
            do {
                Console.Write("\nChoice: ");
                key = Console.ReadKey();
                Console.WriteLine();
            }
            while (key.Key != ConsoleKey.D1 && key.Key != ConsoleKey.D2);

            switch (key.Key) {
                case ConsoleKey.D1:
                    return MenuChoice.Default;
                case ConsoleKey.D2:
                    return MenuChoice.Custom;
                default:
                    return MenuChoice.Default;
            }
        }

        /// <summary>
        /// Asks the user whether to restart the application
        /// </summary>
        public static bool AskIfRestart() {
            // See if the user wants to send again
            ConsoleKeyInfo key;
            do {
                Console.Write("Do you want to restart? [y/n]");
                key = Console.ReadKey();
                Console.WriteLine();
            }
            while (key.Key != ConsoleKey.N && key.Key != ConsoleKey.Y);

            if (key.Key == ConsoleKey.N)
                return false;
            else
                return true;
        }

        public static XmlDocument LoadXmlDocument() {
            Console.Write("File to send: ");
            string file = Console.ReadLine();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(file);
            return xdoc;
        }

        public static Uri GetEndpointAddress() {
            Console.Write("Receiving endpoint: ");
            string endpoint = Console.ReadLine();
            return new Uri(endpoint);
        }


        public static X509Certificate2 GetCertificate() {

            Console.Write("Store ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[" + StoreName.My + "/" + StoreName.Root + "/" + StoreName.AddressBook + "/" + StoreName.CertificateAuthority + "]: ");
            Console.ForegroundColor = ConsoleColor.White;

            StoreName storeName = StoreName.My;
            bool done = false;
            do {
                try {
                    storeName = (StoreName)Enum.Parse(typeof(StoreName), Console.ReadLine());
                    done = true;
                }
                catch { }
            } while (!done);


            Console.Write("Store Location ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[" + StoreLocation.CurrentUser + "/" + StoreLocation.LocalMachine + "]: ");
            Console.ForegroundColor = ConsoleColor.White;


            StoreLocation storeLocation = StoreLocation.CurrentUser;
            done = false;
            do {
                try {
                    storeLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), Console.ReadLine());
                    done = true;
                }
                catch { }
            } while (!done);



            Console.Write("Serial number: ");
            string serial = Console.ReadLine();


            X509Store certStore = new X509Store(storeName, storeLocation);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2 clientCert = certStore.Certificates.Find(X509FindType.FindBySerialNumber,
                serial, true)[0];
            certStore.Close();

            return clientCert;
        }

        public static void PrintException(Exception e) {
            Console.ForegroundColor = ConsoleColor.Red;
            #if DEBUG
                Console.WriteLine(e);
            #else
                Console.WriteLine(e.Message);
            #endif
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintResponse(Response response) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (response.ResponseMessage.HasBody)
                Console.WriteLine("\nResponse received: " + response.ResponseMessage.MessageXml.OuterXml + "\n");
            else
                Console.WriteLine("Empty response received\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion
    }
}
