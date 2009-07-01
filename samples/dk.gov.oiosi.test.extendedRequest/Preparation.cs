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

        public static IRaspRequest PrepareRequest(OiosiMessage message) {

            // First we need to find out what type of object we are sending
            DocumentTypeConfigSearcher typeSearcher = new DocumentTypeConfigSearcher();
            DocumentTypeConfig docTypeConfig = typeSearcher.FindUniqueDocumentType(message.MessageXml);

            // 1. Lookup the endpoint address and certificate using UDDI
            UddiLookupResponse uddiResponse = Uddi(message, docTypeConfig);
            
            // 2. Download the server certificate using LDAP
            X509Certificate2 serverCert = Ldap(uddiResponse.CertificateSubjectSerialNumber);

            // 3. Check the validity status of the certificate using OCSP
            Revocation(serverCert);


            // Let the user configure the client certificate
            Console.WriteLine("\nPlease configure the certificate used for sending\n----------------------------------------------------");
            X509Certificate2 clientCert = GUI.GetCertificate();
            Credentials credentials = new Credentials(new OcesX509Certificate(clientCert), new OcesX509Certificate(serverCert));

            // Create request
            return new RaspRequest(new Request(uddiResponse.EndpointAddress.GetAsUri(), credentials));
        }


        #region 1 - UDDI
        
        static IUddiLookupClient uddiClient;
        static UddiLookupResponse Uddi(OiosiMessage message, DocumentTypeConfig docTypeConfig) {
            
            // Create a uddi client
            RegistryLookupClientFactory uddiClientFactory = new RegistryLookupClientFactory();
            uddiClient = uddiClientFactory.CreateUddiLookupClient();

            // Get the UDDI parameters with which to call the UDDI server
            LookupParameters parameters = GetUddiParameters(message, docTypeConfig);

            // Perform the actual UDDI lookup
            UddiLookupResponse uddiResponse = PerformUddiLookup(parameters);

            // Print out info
            Console.Write("\n  1. Got UDDI reply\n       ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(uddiResponse.EndpointAddress.GetKeyAsString());
            Console.ForegroundColor = ConsoleColor.White;

            return uddiResponse;
        }


        static LookupParameters GetUddiParameters(OiosiMessage message, DocumentTypeConfig docTypeConfig) {

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
                new List<EndpointAddressTypeCode>() {EndpointAddressTypeCode.http});
            return uddiLookupParameters;
        }


        static UddiLookupResponse PerformUddiLookup(LookupParameters uddiLookupParameters) {

            // Do the actual UDDI call
            List<UddiLookupResponse> uddiResponses = uddiClient.Lookup(uddiLookupParameters);

            // Pick the first response gotten
            UddiLookupResponse selectedUddiResponse = uddiResponses[0];
            
            // Make sure the UDDI call returned the reference to a certificate and an endpoint address
            if (selectedUddiResponse.CertificateSubjectSerialNumber == null)
                throw new Exception("The UDDI call didn't return a certificate serial number");
            if (selectedUddiResponse.EndpointAddress == null)
                throw new Exception("The UDDI call didn't return any endpoint");

            return selectedUddiResponse;
        }

        
        #endregion

        #region 2 - LDAP
        static X509Certificate2 Ldap(CertificateSubject certSubject) {
            
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
        static void Revocation(X509Certificate2 certificate) {
            
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
    }
}
