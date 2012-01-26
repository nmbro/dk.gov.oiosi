using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.raspProfile.communication;
using System.IO;

namespace dk.gov.oiosi.test.request {
    
    public class GUI {

        #region 3 - Programatically configured mail

        /// <summary>
        /// Configures the mail account used for sending
        /// </summary>
        public static void GetMailSettings(IRaspRequest request) {

            // Read the settings from the console
            Console.WriteLine("\nPlease configure the mail account used for sending");
            Console.WriteLine("----------------------------------------------------");
            string server = "ebconnect.dk";
            Console.Write("\tMail server: " + server);
            string serverInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(serverInput))
            {
                server = serverInput;
            }

            string account = "ebDispatcher_out";
            Console.Write("\tAccount name: " + account);
            string accountInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(accountInput))
            {
                account = accountInput;
            }

            string password = "Unimaze1";
            Console.Write("\tPassword: " + password);
            string passwordInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(passwordInput))
            {
                password = passwordInput;
            }

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

        public static XmlDocument LoadXmlDocument() 
        {
            bool answerCorrect = false;
            //bool tryAgain = true;
            bool usePredefinedFile = false;
            ConsoleKeyInfo key;

            while (answerCorrect != true)
            {
                Console.Write("File to send - use predefined test files? (y/n)? ");
                key = Console.ReadKey();
                
                if (key.Key == ConsoleKey.Y)
                {
                    usePredefinedFile = true;
                    answerCorrect = true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    usePredefinedFile = false;
                    answerCorrect = true; 
                }
                else
                {
                    Console.WriteLine(" Answer not regonized!");
                }
            }
            
            string fileNumber;
            int fileNumberInt; 
            string file = string.Empty;
            
            if (usePredefinedFile == true)
            {
                IDictionary<int, string> files = DefaultFiles();
                while (string.IsNullOrEmpty(file))
                {
                    Console.WriteLine();
                    Console.WriteLine("Choose a file:");

                    foreach(KeyValuePair<int, string> pair in files)
                    {
                        Console.WriteLine(pair.Key + " - " + pair.Value);
                    }

                    Console.WriteLine();
                    Console.Write("Which file does you want to use? ");
                    fileNumber = Console.ReadLine();

                    if (int.TryParse(fileNumber, out fileNumberInt))
                    {
                        if (files.ContainsKey(fileNumberInt))
                        {
                            // file number found
                            file = files[fileNumberInt];
                        }
                        else
                        {
                            // file number not supported
                            Console.WriteLine("File number (" + fileNumber + ") is not supported, try again.");
                        }
                    }
                    else
                    {
                        // int not regonized
                        Console.WriteLine("File number ("+fileNumber+") not regonized, try again.");
                    }
                }
            }
            else
            {
                Console.Write("File to send: ");
                file = Console.ReadLine();
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(file);
    
            
            return xdoc;
        }

        private static IDictionary<int, string> DefaultFiles()
        {
            IDictionary<int, string> files = new Dictionary<int, string>();
            string path = "." + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Documents" + Path.DirectorySeparatorChar + "Examples" + Path.DirectorySeparatorChar;
            files.Add(1, path + "OIOXML_Invoice_v0.7.xml");
            files.Add(2, path + "OIOXML_CreditNote_v0.7.xml");

            files.Add(3, path + "OIOUBL_Invoice_v2p2.xml");

            return files;
        }

        public static Uri GetEndpointAddress() {
            Console.Write("Receiving endpoint: ");
            string endpoint = Console.ReadLine();
            return new Uri(endpoint);
        }


        public static X509Certificate2 GetCertificate() {

            Console.WriteLine("Store (empty line result in StoreName=My).");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[" + StoreName.My + "/" + StoreName.Root + "/" + StoreName.AddressBook + "/" + StoreName.CertificateAuthority + "]: ");
            Console.ForegroundColor = ConsoleColor.White;

            StoreName storeName = StoreName.My;
            string userFeedback;
            bool done = false;
            do {
                userFeedback = Console.ReadLine();
                if (string.IsNullOrEmpty(userFeedback))
                {
                    // using the default values
                    storeName = StoreName.My;
                    done = true;
                }
                else
                {

                    try
                    {
                        storeName = (StoreName)Enum.Parse(typeof(StoreName), userFeedback);
                        done = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            } while (!done);


            Console.WriteLine("Store Location (empty line result in StoreLocation=CurrentUser).");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[" + StoreLocation.CurrentUser + "/" + StoreLocation.LocalMachine + "]: ");
            Console.ForegroundColor = ConsoleColor.White;

            StoreLocation storeLocation = StoreLocation.CurrentUser;
            done = false;
            do {
                userFeedback = Console.ReadLine();
                if (string.IsNullOrEmpty(userFeedback))
                {
                    // using the default values
                    storeLocation = StoreLocation.CurrentUser;
                    done = true;
                }
                else
                {
                    try
                    {
                        storeLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), userFeedback);
                        done = true;
                    }
                    catch (Exception e) { Console.WriteLine(e); }

                }
            } while (!done);

            X509Store certStore = new X509Store(storeName, storeLocation);
            certStore.Open(OpenFlags.ReadOnly);

            Console.WriteLine("At that location the following certificate exist: ");
            Console.WriteLine(" Serial");
            Console.WriteLine("   Issuer");
            Console.WriteLine("   Subject");

            foreach (X509Certificate2 xxx in certStore.Certificates)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" " + xxx.SerialNumber);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("   " + xxx.Issuer);
                Console.WriteLine("   " + xxx.Subject);
                //Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            string defaultSerial = string.Empty;

            if (string.Equals(ConfigurationDocument.ConfigFilePath, "RaspConfiguration.Live.xml", StringComparison.OrdinalIgnoreCase))
            {
                defaultSerial = "45A2F4A1";
            }
            else if (string.Equals(ConfigurationDocument.ConfigFilePath, "RaspConfiguration.Test.xml", StringComparison.OrdinalIgnoreCase))
            {
                defaultSerial = "4037C978";
            }
             
            Console.Write("Serial number (empty line will result in " + defaultSerial + "): ");
            string serial = Console.ReadLine();

            if (string.IsNullOrEmpty(serial))
            {
                serial = defaultSerial;
            }

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
            {
                Console.WriteLine("Response received");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("  " + response.ResponseMessage.MessageXml.OuterXml);
            }
            else
            {
                Console.WriteLine("Empty response received");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion
    }
}
