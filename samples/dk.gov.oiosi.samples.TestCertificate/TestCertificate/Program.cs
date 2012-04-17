using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.security.ldap;
using dk.gov.oiosi.security.lookup;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.revocation.ocsp;

namespace dk.gov.oiosi.samples
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
        }

        public Program()
        {
            // First, define if we must test productive/live or the test infrastructure
            // Comment out the line Infrastructure, you don't want  to test

            string raspConfiguration = string.Empty;

            // Test
            //raspConfiguration = "RaspConfiguration.Test.xml";

            // Production/Live
            //raspConfiguration = "RaspConfiguration.Live.xml";

            // oces2
            raspConfiguration = "RaspConfiguration.Oces2.xml";
            // Set the Configuration handler to use the desired Configuration file
            ConfigurationDocument.ConfigFilePath = raspConfiguration;
            CacheFactory instance = CacheFactory.Instance;
            // Test Certificate
            this.TextCertificate();

            //TestOCSP();
        }


        private void TextCertificate()
        {
            string subject;
            
            /* Production configuration */
            // specifie the subject to test
            // Production certificates valid
            // subject = "CN = NemHandel test service (funktionscertifikat) + SERIALNUMBER = CVR:26769388-FID:1200406941690, O = IT- og Telestyrelsen // CVR:26769388, C = DK";
             //subject = "SERIALNUMBER=CVR:30808460-FID:1320135775022 + CN=TEST FOCES1 (funktionscertifikat), O=DANID A/S // CVR:30808460, C=DK";
            //subject = "OID.2.5.4.5=CVR:19020940-FID:1266418009173 + CN=E-handel ny (funktionscertifikat), O=COLOPLAST DANMARK A/S // CVR:19020940, C=DK";
            // subject = "CN=FO NemHandel Produktion (funktionscertifikat), SERIALNUMBER = CVR:26911745-FID:1300089978680, O=KMD A/S // CVR:26911745, C=DK";
            // Production certificates not valid

            /* Test configuration */
            //
            // Test certificates valid
            // subject = "CN = Testendpoint (funktionscertifikat) + SERIALNUMBER = CVR:26769388-FID:1208430425605, O = IT- og Telestyrelsen // CVR:26769388, C = DK";
            //subject = "CN = Testendpoint (funktionscertifikat) + SERIALNUMBER = CVR:26769388-FID:1208430425605, O = IT- og Telestyrelsen // CVR:26769388, C = DK";
            // Test certificates not valid
             //subject = "CN = TU GENEREL FOCES gyldig (funktionscertifikat) + SERIALNUMBER = CVR:30808460-FID:94731315, O = Danid A/S // CVR:30808460, C = DK";
            // subject = "CN=Navision (funktionscertifikat) + OID.2.5.4.5=CVR:23267519-FID:1257424251148, O=TIETGENSKOLEN // CVR:23267519, C=DK";
            //subject = "CN = TEST FOCES1 (funktionscertifikat) + SERIALNUMBER = CVR:30808460-FID:1320135775022, O = DANID A/S // CVR:30808460, C = DK";
            // not valid any more
             //subject = "CN=TU GENEREL FOCES gyldig (funktionscertifikat) + SERIALNUMBER = CVR:30808460-FID:94731315, O = Danid A/S // CVR:30808460, C = DK";
             subject = "CN=TU GENEREL FOCES gyldig (funktionscertifikat) + SERIALNUMBER=CVR:30808460-FID:94731315, O=Danid A/S // CVR:30808460, C=DK";
            // subject = "CN=FOCES1 (funktionscertifikat) + SERIALNUMBER=CVR:30808460-FID:1255692730737, O=DANID A/S // CVR:30808460, C=DK";
            // subject = "CN=TU GENEREL MOCES gyldig + SERIALNUMBER=CVR:30808460-RID:45490598, O=Danid A/S // CVR:30808460, C=DK";


            // Now - retrive the certificate in LDAP, if the certificate is pressen...
            CertificateSubject certificateSubject = new CertificateSubject(subject);
            LdapLookupFactory ldapClientFactory = new LdapLookupFactory();
            ICertificateLookup ldapClient = ldapClientFactory.CreateLdapLookupClient();

            // Lookup the certificate using LDAP
            X509Certificate2 certificate = ldapClient.GetCertificate(certificateSubject);

            if (certificate != null)
            {
                Console.Write("Certificate whith subject ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(subject);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" found in LDAP.");

                // Validate that the certificate is valid in OCSP

                RevocationLookupFactory revocationLookupFactory = new RevocationLookupFactory();
                IRevocationLookup revocationClient = revocationLookupFactory.CreateRevocationLookupClient();

                // Check the validity status of the certificate using OCSP
                RevocationResponse revocationResponse = revocationClient.CheckCertificate(certificate);
                if (revocationResponse.IsValid)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Certificate valid in OCSP/CRL");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Certificate not valid in OCSP/CRL");
                }
            }
            else
            {
                Console.Write("Certificate whith subject ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(subject);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" NOT found in LDAP.");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private void TestOCSP()
        {
            OcspLookup ocspLookup = new OcspLookup();

            X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);

            string serial = "4c 05 5a 37";

            X509Certificate2Collection collection = certStore.Certificates.Find(X509FindType.FindBySerialNumber, serial, true);
            X509Certificate2 cert = null;

            if (collection.Count > 0)
            {
                cert = collection[0];
            }
            else
            {
                // the certificate not found
                throw new NotImplementedException("The certificate was not found.");
            }

            X509Chain x509Chain = new X509Chain();
            x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
            x509Chain.Build(cert);

            // Iterate though the chain, to validate if it contain a valid root vertificate
            X509ChainElementCollection x509ChainElementCollection = x509Chain.ChainElements;
            X509ChainElementEnumerator enumerator = x509ChainElementCollection.GetEnumerator();
            X509ChainElement x509ChainElement;
            X509Certificate2 x509Certificate2 = null;
            IDictionary<string, X509Certificate2> map = new Dictionary<string, X509Certificate2>();
            IList<X509Certificate2> list = new List<X509Certificate2>();

            // At this point, the certificate is not valid, until a 
            // it is proved that it has a valid root certificate
            while (enumerator.MoveNext())
            {
                x509ChainElement = enumerator.Current;
                x509Certificate2 = x509ChainElement.Certificate;
                list.Add(x509Certificate2);
            }



            ocspLookup.RevocationResponseOnline(list[0], list[1], "http://ocsp.systemtest8.trust2408.com/responder");



        }
    }
}
