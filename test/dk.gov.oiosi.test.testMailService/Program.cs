using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

using dk.gov.oiosi.communication.listener;
using dk.gov.oiosi.security;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.raspProfile;

namespace TestMailService
{
    /// <summary>
    /// Test application for the dk.gov.oiosi.communication.listener.Listener class.
    /// 
    /// Starts a service, and keeps it running until a key is pressed. 
    /// If space was pressed the service is restarted, else it's stopped.
    /// </summary>
    class Program
    {
        static string _address;
        
        static void Main(string[] args)
        {
            // Sets up the needed certificate config
            SetupDefaultConfig();

            ConsoleKeyInfo key;
            do
            {
                PrintOutTitle();
                
                Listener listener = new Listener();

                // For threading reasons we need to listen to asynchronous exceptions 
                // thrown by the listener (that is to listen to the event Exception thrown)
                listener.ExceptionThrown += new dk.gov.oiosi.exception.AsyncExceptionThrownHandler(listener_ExceptionThrown);

                // Listen to event raised when a message is received
                listener.MessageReceive += new dk.gov.oiosi.communication.MessageEventDelegate(listener_MessageReceive);

                // Write info about our service to the screen
                _address = FindCurrentAddress(listener);
                PrintOutStack();

                // Start listening
                listener.Start();

                Console.WriteLine("Press any key to stop [space for restart, 'i' for stack info]\n\n");
                
                // Did we hit the info button?
                while(true){
                    key = Console.ReadKey();
                    if(key.Key == ConsoleKey.I)PrintOutStack();
                    else break;
                }

                // A button was hit, and it wasn't i

                Console.WriteLine("Stopping...");
                listener.Stop();

            } 
            // If the key was space, restart
            while (key.Key == ConsoleKey.Spacebar);
            
        }

        /// <summary>
        /// Callback for event raised when a message was received
        /// </summary>
        /// <param name="message"></param>
        /// <param name="processStatus"></param>
        static void listener_MessageReceive(ListenerRequest message, dk.gov.oiosi.communication.MessageProcessStatus processStatus) {
            
            // Write the SOAP action of the message to screen
            Console.WriteLine("Message with action '" + message.RequestMessage.RequestAction + "' received");
        
        
            // Check the signature validation proof status
            try {
                
                SignatureValidationProof svp = message.GetProperty<SignatureValidationProof>();
                bool valid = (svp.ValidCertificate && svp.ValidSignature && svp.UnchangedMessage && svp.EncryptedMessage && svp.Completed);
                Console.WriteLine("\tSigned and encrypted with a valid certificate: " + valid);
                
            }
            catch {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tNo signature validation proof could be found on the incoming message. Make sure that a signature validation proof interceptor is set in the application configuration file.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        /// <summary>
        /// Callback method for asynchronous exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        static void listener_ExceptionThrown(object sender, Exception ex) {
            
            // Write the exception to screen
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Exception:\n\n" + ex);
            Console.ForegroundColor = ConsoleColor.White;
        }


        // Goes down the stack to the Transport layer and gets the service address we're listening to
        static string FindCurrentAddress(Listener listener) {
            return ((EmailBindingElement)listener.Identity.Transport.TransportBinding).ReplyAddress;
        }

        /// <summary>
        /// Prints out the title of the application
        /// </summary>
        static void PrintOutTitle() {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(
                "\n" +
                "RASP Mail test endpoint\n" +
                "---------------------------------------"
                );
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Prints out the current stack
        /// </summary>
        static void PrintOutStack(){

            Console.Write("\nListening at ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(_address + "\n");
            Console.ForegroundColor = ConsoleColor.White;

            BindingsSection bs = (BindingsSection)System.Configuration.ConfigurationManager.GetSection("system.serviceModel/bindings");

            if (bs.CustomBinding.Bindings.Count == 1) {
                bool rm = false;
                bool sec = false;
                
                CustomBindingElement cbe = (CustomBindingElement)bs.CustomBinding.ConfiguredBindings[0];
                foreach (PropertyInformation pi in cbe.ElementInformation.Properties) {
                    if (pi.Name == "reliableSession") rm = true;
                    else if (pi.Name == "security") sec = true;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\t<stack>");
                if (!rm) Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\t  <rm>");
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (!sec) Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\t  <security>");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t  <encoding>");
                Console.WriteLine("\t  <mail transport>");
                Console.WriteLine("\t</stack>\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        /// <summary>
        /// Sets up default document, ocsp and ldap config
        /// </summary>
        static void SetupDefaultConfig() {
            
            try {
                DefaultDocumentTypes docTypes = new DefaultDocumentTypes();
                DocumentTypeCollectionConfig docs = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
                docs.AddDocumentType(docTypes.GetApplicationResponse());
                docs.AddDocumentType(docTypes.GetCreditNote());
                docs.AddDocumentType(docTypes.GetInvoice());
                docs.AddDocumentType(docTypes.GetOrder());
                docs.AddDocumentType(docTypes.GetOrderResponseSimple());
                docs.AddDocumentType(docTypes.GetReminder());
                ConfigurationHandler.SaveToFile();
            }
            catch { }

            try {
                DefaultLdapConfig ldap = new DefaultLdapConfig();
                ldap.SetDefaultLdapConfigTest();
                ldap.SetTestLdapLookupFactoryConfig();
            }
            catch { }

            try {
                DefaultRevocationConfig revocation = new DefaultRevocationConfig();
                revocation.SetTestRevocationLookupFactoryConfig();
                revocation.SetTestOscpConfig();
            }
            catch { }

            ConfigurationHandler.SaveToFile();
        }

    }
}
