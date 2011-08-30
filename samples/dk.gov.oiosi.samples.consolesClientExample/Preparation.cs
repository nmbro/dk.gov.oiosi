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
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.xml.xpath;
using dk.gov.oiosi.addressing;

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

        public static IRaspRequest PrepareRequest(OiosiMessage message)
        {
            RaspRequest raspRequest = null;

            // First we need to find out what type of object we are sending
            DocumentTypeConfigSearcher typeSearcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig docTypeConfig = typeSearcher.FindUniqueDocumentType(message.MessageXml);

            // 1. Lookup the endpoint address and certificate using UDDI
            UddiLookupResponse uddiResponse = Uddi(message, docTypeConfig);

            if (uddiResponse != null)
            {
                // 2. Download the server certificate using LDAP
                X509Certificate2 serverCert = Ldap(uddiResponse.CertificateSubjectSerialNumber);

                // 3. Check the validity status of the certificate using OCSP
                Revocation(serverCert);

                // Let the user configure the client certificate
                Console.WriteLine("\nPlease configure the certificate used for sending\n----------------------------------------------------");
                X509Certificate2 clientCert = GUI.GetCertificate();
                Credentials credentials = new Credentials(new OcesX509Certificate(clientCert), new OcesX509Certificate(serverCert));

                // Create request
                Request request = new Request(uddiResponse.EndpointAddress.GetAsUri(), credentials);
                raspRequest = new RaspRequest(request);
            }

            return raspRequest;
        }


        #region 1 - UDDI
        
        static IUddiLookupClient uddiClient;
        static UddiLookupResponse Uddi(OiosiMessage message, DocumentTypeConfig docTypeConfig) {

            UddiConfig uddiConfig = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            
            Console.WriteLine("1. UDDI services");
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (Registry registry in uddiConfig.LookupRegistryFallbackConfig.PrioritizedRegistryList)
            {
                foreach (string endpoint in registry.Endpoints)
                {
                    Console.WriteLine("   " + endpoint);
                }
            }

            // Create a uddi client
            RegistryLookupClientFactory uddiClientFactory = new RegistryLookupClientFactory();
            uddiClient = uddiClientFactory.CreateUddiLookupClient();

            // Get the UDDI parameters with which to call the UDDI server
            LookupParameters parameters = GetUddiParameters(message, docTypeConfig);

            // Perform the actual UDDI lookup
            UddiLookupResponse uddiResponse = PerformUddiLookup(parameters);


            
            Console.WriteLine("   EndPoint      : "+parameters.Identifier.ToString());
            Console.WriteLine("   EndPoint type : " + parameters.Identifier.KeyTypeCode);
            Console.WriteLine("   Profile       : " + GetProfileName(message, docTypeConfig));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   Got UDDI reply:");
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (uddiResponse == null)
            {
                Console.WriteLine("    The endpoint is no registrated in UDDI!");
            }
            else
            {
                Console.WriteLine("    " + uddiResponse.EndpointAddress.GetKeyAsString());
            }
            Console.ForegroundColor = ConsoleColor.White;

            return uddiResponse;
        }


        static LookupParameters GetUddiParameters(OiosiMessage message, DocumentTypeConfig docTypeConfig) 
        {
            // Use an OIOSI utility to find the endpoint type in the XML document to be sent
            EndpointKeyTypeCode endpointKeyTypeCode = Utilities.GetEndpointKeyTypeCode(message, docTypeConfig);

            // Use the XPath expression from the UBL type configuration (found in the RaspConfiguration.xml file) 
            // to find the endpoint identifier in the XML document to be sent
            Identifier endpointKey = Utilities.GetEndpointKeyByXpath(
                message.MessageXml,
                docTypeConfig.EndpointType.Key.XPath,
                docTypeConfig.Namespaces,
                endpointKeyTypeCode
            );

            // create the profile tModel
            UddiId profileTModelId = GetProfileTModelId(message, docTypeConfig);

            // Find the UDDI identifier for the service contract used by the remote endpoint
            UddiId serviceContractTModel;
            try {
                serviceContractTModel = IdentifierUtility.GetUddiIDFromString(docTypeConfig.ServiceContractTModel);
            }
            catch (Exception) {
                throw new Exception("Could not find the service contract TModel for the UDDI lookup");
            }

           LookupParameters uddiLookupParameters;
            if (profileTModelId != null)
            {
                // lookup including profile
                uddiLookupParameters = new LookupParameters(
                    endpointKey,
                    serviceContractTModel,
                    new List<UddiId> { profileTModelId },
                    new List<EndpointAddressTypeCode> { EndpointAddressTypeCode.http });
            }
            else
            {
                // lookup without profile
                uddiLookupParameters = new LookupParameters(
                    endpointKey,
                    serviceContractTModel,
                    new List<EndpointAddressTypeCode>() {EndpointAddressTypeCode.http});
            }

            return uddiLookupParameters;
        }

        private static UddiId GetProfileTModelId(OiosiMessage message, DocumentTypeConfig docTypeConfig)
        {
            UddiId uddiId = null;
            string profileName = GetProfileName(message, docTypeConfig);

            if (string.IsNullOrEmpty(profileName) == false)
            {
                var config = ConfigurationHandler.GetConfigurationSection<ProfileMappingCollectionConfig>();
                if (config.ContainsProfileMappingByName(profileName))
                {
                    ProfileMapping profileMapping = config.GetMapping(profileName);
                    string profileTModelGuid = profileMapping.TModelGuid;
                    uddiId =  IdentifierUtility.GetUddiIDFromString(profileTModelGuid);
                }
                else
                {
                    throw new Exception("DocumentProfileMappingNotFoundException: " +profileName);
                }
            }

            return uddiId;
        }

        private static string GetProfileName(OiosiMessage message, DocumentTypeConfig docTypeConfig)
        {
            string profileName = string.Empty;
                        // If doctype does't have a XPath expression to extract the document Profile 
            // then we assume that the current document type does operate with OIOUBL profiles

            if (docTypeConfig.ProfileIdXPath == null)
            {
                // no profile
            }
            else if (docTypeConfig.ProfileIdXPath.XPath == null)
            {
                // no profile
            }
            else if (string.IsNullOrEmpty(docTypeConfig.ProfileIdXPath.XPath))
            {
                // no profile
            }
            else
            {
                // Fetch the OIOUBL profile name
                string xPath = docTypeConfig.ProfileIdXPath.XPath;
                PrefixedNamespace[] nameSpaces = docTypeConfig.Namespaces;

                profileName = DocumentXPathResolver.GetElementValueByXpath(message.MessageXml, xPath, nameSpaces);
            }

            return profileName;
        }

        static UddiLookupResponse PerformUddiLookup(LookupParameters uddiLookupParameters) {

            // Do the actual UDDI call
            List<UddiLookupResponse> uddiResponses = uddiClient.Lookup(uddiLookupParameters);
            UddiLookupResponse selectedUddiResponse;
            // Pick the first response gotten
            if(uddiResponses.Count == 0)
            {
                selectedUddiResponse = null;
            }
            else
            {   
                selectedUddiResponse = uddiResponses[0];
                
                // Make sure the UDDI call returned the reference to a certificate and an endpoint address
                if (selectedUddiResponse.CertificateSubjectSerialNumber == null)
                    throw new Exception("The UDDI call didn't return a certificate serial number");
                if (selectedUddiResponse.EndpointAddress == null)
                    throw new Exception("The UDDI call didn't return any endpoint");
            }

            return selectedUddiResponse;
        }

        
        #endregion

        #region 2 - LDAP
        static X509Certificate2 Ldap(CertificateSubject certSubject) 
        {
            LdapSettings settings = ConfigurationHandler.GetConfigurationSection<LdapSettings>();

            // Print out info
            Console.WriteLine();
            Console.WriteLine("2. Certificate download");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   " + settings.Host);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();


            // Create the LDAP client
            LdapLookupFactory ldapClientFactory = new LdapLookupFactory();
            ICertificateLookup ldapClient = ldapClientFactory.CreateLdapLookupClient();

            // Lookup the certificate using LDAP
            X509Certificate2 certificate = ldapClient.GetCertificate(certSubject);

            Console.WriteLine("   Downloaded certificate with LDAP:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    " + certificate.Subject);
            Console.ForegroundColor = ConsoleColor.White;

            return certificate;

        }
        #endregion

        #region 3 - Revocation
        static void Revocation(X509Certificate2 certificate) {
            
            // Create the OCSP client
            RevocationLookupFactory revocationLookupFactory = new RevocationLookupFactory();
            IRevocationLookup revocationClient = revocationLookupFactory.CreateRevocationLookupClient();

            // Check the validity status of the certificate using OCSP
            RevocationResponse revocationResponse = revocationClient.CheckCertificate(certificate);

            // Print out info
            Console.WriteLine();
            Console.WriteLine("3. Certificate status by RevocationLookup.");

            Console.WriteLine("   Certificate status is valid: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    "+ revocationResponse.IsValid.ToString());
            Console.ForegroundColor = ConsoleColor.White;

            // Make sure the cert was valid
            if (!revocationResponse.IsValid)
                throw new Exception("The certificate returned by RevocationLookup was not valid");
        }
        #endregion
    }
}
