using dk.gov.oiosi.configuration;
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.security.revocation.ocsp;
using dk.gov.oiosi.security.ldap;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.xml.schematron;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.test.unit
{
    public class ConfigurationUtil
    {
        public static void SetupConfiguration()
        {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.Test.xml";
            ConfigurationHandler.Reset();

            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();

            DefaultProfileMappingConfig profileMappings = new DefaultProfileMappingConfig();
            profileMappings.AddAll();

        }

        public static void SetupConfiguration(string configurationFilePath)
        {
            ConfigurationHandler.ConfigFilePath = configurationFilePath;
            ConfigurationHandler.Reset();

            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();

            DefaultProfileMappingConfig profileMappings = new DefaultProfileMappingConfig();
            profileMappings.AddAll();
        }
    }
}