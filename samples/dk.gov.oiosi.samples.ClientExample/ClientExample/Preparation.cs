using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using dk.gov.oiosi.common;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.ldap;
using dk.gov.oiosi.security.lookup;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.test.request;
using dk.gov.oiosi.raspProfile.communication;
using dk.gov.oiosi.samples.ClientExample;

namespace dk.gov.oiosi.samples.consoleClientExample {

    /// <summary>
    /// Extended Request example
    /// 
    /// This example demonstrates how to perform the UDDI, LDAP and OCSP/CRL lookups needed
    /// to make a full OIOSI RASP service call.
    /// 
    /// Lessons
    /// --------
    ///     UDDI Lookup - region 1
    ///         We use the type of document we're sending and the EAN number it contains to
    ///         find a remote endpoint address and a remote endpoint certificate in the UDDI 
    ///         registry
    /// 
    ///     LDAP Lookup - region 2
    ///         We use LDAP to download the certificate returned by the UDDI registry
    /// 
    ///     Revocation Lookup - region 3
    ///         We make sure that the certificate we downloaded is valid by looking it up
    ///         on an OCSP server or a CRL
    /// 
    /// </summary>
    public class Preparation {

        public IRaspRequest PrepareRequest(OiosiMessage message, UddiType uddiType) {

            // First we need to find out what type of object we are sending
            DocumentTypeConfigSearcher typeSearcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig docTypeConfig = typeSearcher.FindUniqueDocumentType(message.MessageXml);

            // 1. Lookup the endpoint address and certificate using UDDI
            UddiLookupResponse uddiResponse = this.Uddi(message, docTypeConfig);
            
            // 2. Download the server certificate using LDAP
            X509Certificate2 serverCert = this.Ldap(uddiResponse.CertificateSubjectSerialNumber);

            // 3. Check the validity status of the certificate using OCSP
            this.Revocation(serverCert);


            // 4. Let the user configure the client certificate
            Console.WriteLine("\nPlease configure the certificate used for sending\n----------------------------------------------------");
            X509Certificate2 clientCert = this.GetCertificate(uddiType);
            Credentials credentials = new Credentials(new OcesX509Certificate(clientCert), new OcesX509Certificate(serverCert));

            // Create request
            return new RaspRequest(new Request(uddiResponse.EndpointAddress.GetAsUri(), credentials));
        }


        #region 1 - UDDI
        
        //static IUddiLookupClient uddiClient;


        private UddiLookupResponse Uddi(OiosiMessage message, DocumentTypeConfig docTypeConfig) {
            
            // Create a uddi client
            RegistryLookupClientFactory uddiClientFactory = new RegistryLookupClientFactory();
            IUddiLookupClient uddiClient = uddiClientFactory.CreateUddiLookupClient();

            // Get the UDDI parameters with which to call the UDDI server
            LookupParameters parameters = this.GetUddiParameters(message, docTypeConfig);

            // Perform the actual UDDI lookup
            UddiLookupResponse uddiResponse = this.PerformUddiLookup(parameters, uddiClient);

            // Print out info
            Console.Write("\n  1. Got UDDI reply\n       ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(uddiResponse.EndpointAddress.GetKeyAsString());
            Console.ForegroundColor = ConsoleColor.White;

            return uddiResponse;
        }


        private LookupParameters GetUddiParameters(OiosiMessage message, DocumentTypeConfig docTypeConfig) {

            // Use an OIOSI utility to find the endpoint type in the XML document to be sent
            EndpointKeyTypeCode endpointKeyTypeCode = Utilities.GetEndpointKeyTypeCode(message, docTypeConfig);

            // Use the XPath expression from the UBL type configuration (found in the RaspConfiguration.xml file) 
            // to find the endpoint identifier in the XML document to be sent
            dk.gov.oiosi.addressing.Identifier endpointKey = Utilities.GetEndpointKeyByXpath(
                message.MessageXml,
                docTypeConfig.EndpointType.Key.XPath,
                docTypeConfig.Namespaces,
                endpointKeyTypeCode
            );

            // Find the UDDI identifier for the service contract used by the remote endpoint
            UddiId serviceContractTModel;
            try {
                serviceContractTModel = IdentifierUtility.GetUddiIDFromString(docTypeConfig.ServiceContractTModel);
            }
            catch (Exception) {
                throw new Exception("Could not find the service contract TModel for the UDDI lookup");
            }


            LookupParameters uddiLookupParameters = new LookupParameters(
                endpointKey,
                serviceContractTModel,
                new List<EndpointAddressTypeCode>() {EndpointAddressTypeCode.http, EndpointAddressTypeCode.email});
            
            
            return uddiLookupParameters;
        }


        private UddiLookupResponse PerformUddiLookup(LookupParameters uddiLookupParameters, IUddiLookupClient uddiClient)
        {
            UddiLookupResponse selectedUddiResponse = null;

            // Do the actual UDDI call
            List<UddiLookupResponse> uddiResponses = uddiClient.Lookup(uddiLookupParameters);

            // Pick the first response gotten
            if (uddiResponses.Count == 0)
            {
                // No endpoint found in the uddi(s).
                throw new Exception("Endpoint does not exixt. " + uddiLookupParameters.Identifier.KeyTypeCode.ToString()+ ": " + uddiLookupParameters.Identifier + ". ");

            }
            else if (uddiResponses.Count == 1)
            {
                selectedUddiResponse = uddiResponses[0];
                // Make sure the UDDI call returned the reference to a certificate and an endpoint address
                if (selectedUddiResponse.CertificateSubjectSerialNumber == null)
                {
                    throw new Exception("The UDDI call didn't return a certificate serial number");
                }
                if (selectedUddiResponse.EndpointAddress == null)
                {
                    throw new Exception("The UDDI call didn't return any endpoint");
                }
            }
            else
            {
                // more the one - not good
                Console.WriteLine("To many endpoints found (" + uddiResponses.Count + "), for " + uddiLookupParameters.Identifier.KeyTypeCode.ToString() + ": " + uddiLookupParameters.Identifier + ". Using the first one.");
                selectedUddiResponse = uddiResponses[0];
                // Make sure the UDDI call returned the reference to a certificate and an endpoint address
                if (selectedUddiResponse.CertificateSubjectSerialNumber == null)
                {
                    throw new Exception("The UDDI call didn't return a certificate serial number");
                }
                if (selectedUddiResponse.EndpointAddress == null)
                {
                    throw new Exception("The UDDI call didn't return any endpoint");
                }
            }

            return selectedUddiResponse;
        }

        
        #endregion

        #region 2 - LDAP
        private X509Certificate2 Ldap(CertificateSubject certSubject) {
            
            // Create the LDAP client
            LdapLookupFactory ldapClientFactory = new LdapLookupFactory();
            ICertificateLookup ldapClient = ldapClientFactory.CreateLdapLookupClient();

            // Lookup the certificate using LDAP
            X509Certificate2 certificate = ldapClient.GetCertificate(certSubject);

            // Print out info
            Console.Write("  2. Downloaded certificate with LDAP\n       ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(certificate.Subject);
            Console.ForegroundColor = ConsoleColor.White;

            return certificate;

        }
        #endregion

        #region 3 - Revocation
        private void Revocation(X509Certificate2 certificate) {
            
            // Create the OCSP client
            RevocationLookupFactory revocationLookupFactory = new RevocationLookupFactory();
            IRevocationLookup revocationClient = revocationLookupFactory.CreateRevocationLookupClient();

            // Check the validity status of the certificate using OCSP
            RevocationResponse revocationResponse = revocationClient.CheckCertificate(certificate);

            // Print out info
            Console.Write("  3. Certificate status returned by RevocationLookup.\n       Is valid: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(revocationResponse.IsValid.ToString());
            Console.ForegroundColor = ConsoleColor.White;

            // Make sure the cert was valid
            if (!revocationResponse.IsValid)
                throw new Exception("The certificate returned by RevocationLookup was not valid");
        }
        #endregion

        #region 4 - Certificate
        public X509Certificate2 GetCertificate(UddiType UddiType)
        {
            StoreName storeName = StoreName.My;

            Console.Write("Store ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[" + StoreName.My + "/" + StoreName.Root + "/" + StoreName.AddressBook + "/" + StoreName.CertificateAuthority + "]: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(storeName.ToString());

            StoreLocation storeLocation = StoreLocation.CurrentUser;
            Console.Write("Store Location ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[" + StoreLocation.CurrentUser + "/" + StoreLocation.LocalMachine + "]: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(storeLocation.ToString());
            

            //Console.Write("Serial number: 45 a2 f4 a1");
            //string serial = "45 a2 f4 a1";
            // Test certificate - Must be importet into windows key store

            /*
             * Certificat - Virksomhedscertificat
             * Issued To      Christian Pedersen
             * Issued by      TDC OCES Systemtest CA II
             * Valid From     26-05-2010
             * Valid To       26-05-2012
             * Serial number  40 37 86 cc
             * StoreName      My
             * StoreLocation  CurrentUser
             * 
             * can not be used - is not a funktionscertificat
             */

            /*
            * Certificat 
            * Issued To      Testendpoint (funktionscertifikat)
            * Issued by      TDC OCES Systemtest CA II
            * Valid From     17-04-2008
            * Valid To       17-04-2010
            * Serial number  40 36 d8 5e
            * StoreName      My
            * StoreLocation  CurrentUser
            * 
            *  Can not be used - Is a funktionscertificat, but it has expired
            */

            /* 
            * Certificat 
            * Issued To      FOCES1 (funktionscertifikat)
            * Issued by      TDC OCES Systemtest CA II
            * Valid From     16-10-2011
            * Valid To       16-10-2009
            * Serial number  40 37 60 8e
            * StoreName      My
            * StoreLocation  CurrentUser
            * 
            *  Installed from https://www.certifikat.dk/export/sites/dk.certifikat.oc/da/developer/eksempler/
            */

            string serial = null;

            switch (UddiType)
            {
                case UddiType.Production :
                    {
                        serial = "45 be bc 3e";
                        break;
                    }
                case UddiType.Test :
                    {
                        serial = "40 37 fb 49";
                        break;
                    }
                default :
                        {
                            throw new NotImplementedException("The uddi type '" + UddiType.ToString() + "' is unknown.");
                        }
            }

            Console.WriteLine("Serial number: " + serial);

            X509Store certStore = new X509Store(storeName, storeLocation);
            certStore.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection collection = certStore.Certificates.Find(X509FindType.FindBySerialNumber, serial, true);
            X509Certificate2 clientCert = null;

            if (collection.Count > 0)
            {
                clientCert = collection[0];
            }
            else
            {
                // the certificate not found
                throw new NotImplementedException("The certificate was not found.");
            }

            Console.WriteLine("Expire: " + clientCert.GetExpirationDateString());

            certStore.Close();
                        
            return clientCert;
        }

        #endregion
    }
}
