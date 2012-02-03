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
            raspConfiguration = "RaspConfiguration.Test.xml";

            // Production/Live
            //raspConfiguration = "RaspConfiguration.Live.xml";

            // Set the Configuration handler to use the desired Configuration file
            ConfigurationDocument.ConfigFilePath = raspConfiguration;
            CacheFactory instance = CacheFactory.Instance;
            // Test Certificate
            this.TextCertificate();

        }

        private void TextCertificate()
        {
            string subject;
            
            /* Production configuration */
            // specifie the subject to test
            // Production certificates valid
            // subject = "CN = NemHandel test service (funktionscertifikat) + SERIALNUMBER = CVR:26769388-FID:1200406941690, O = IT- og Telestyrelsen // CVR:26769388, C = DK";
             //subject = "SERIALNUMBER=CVR:30808460-FID:1320135775022 + CN=TEST FOCES1 (funktionscertifikat), O=DANID A/S // CVR:30808460, C=DK";
             subject = "SERIALNUMBER=CVR:30808460-FID:1320135775022 + CN=TEST FOCES1 (funktionscertifikat), O=DANID A/S // CVR:30808460, C=DK";
            // subject = "CN=FO NemHandel Produktion (funktionscertifikat), SERIALNUMBER = CVR:26911745-FID:1300089978680, O=KMD A/S // CVR:26911745, C=DK";
            // Production certificates not valid

            /* Test configuration */
            //
            // Test certificates valid
            // subject = "CN = Testendpoint (funktionscertifikat) + SERIALNUMBER = CVR:26769388-FID:1208430425605, O = IT- og Telestyrelsen // CVR:26769388, C = DK";
            // subject = "CN = TEST FOCES1 (funktionscertifikat) + SERIALNUMBER = CVR:30808460-FID:1320135775022, O = DANID A/S // CVR:30808460, C = DK";
            // Test certificates not valid
            // subject = "CN = TU GENEREL FOCES gyldig (funktionscertifikat) + SERIALNUMBER = CVR:30808460-FID:94731315, O = Danid A/S // CVR:30808460, C = DK";
            // subject = "CN=Navision (funktionscertifikat) + OID.2.5.4.5=CVR:23267519-FID:1257424251148, O=TIETGENSKOLEN // CVR:23267519, C=DK";

            // not valid any more
            // subject = "CN=TU GENEREL FOCES gyldig (funktionscertifikat) + SERIALNUMBER=CVR:30808460-FID:94731315, O=Danid A/S // CVR:30808460, C=DK";
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
                    Console.Write("Certificate valid in OCSP");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Certificate not valid in OCSP");
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

            Console.ReadLine();
        }
    }
}
