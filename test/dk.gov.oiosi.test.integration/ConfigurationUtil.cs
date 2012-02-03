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

namespace dk.gov.oiosi.test.integration {
    public class ConfigurationUtil {
        public static void SetupConfiguration() 
        {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.xml";
            ConfigurationHandler.Reset();

            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();

            DefaultProfileMappingConfig profileMappings = new DefaultProfileMappingConfig();
            profileMappings.AddAll();

            /*ConfigurationHandler.HasConfigurationSection<LdapLookupFactoryConfig>();
            ConfigurationHandler.HasConfigurationSection<RevocationLookupFactoryConfig>();
            ConfigurationHandler.HasConfigurationSection<UddiLookupClientFactoryConfig>();
            ConfigurationHandler.HasConfigurationSection<RegistryLookupClientFactoryConfig>();
            ConfigurationHandler.HasConfigurationSection<LdapSettings>();
            ConfigurationHandler.HasConfigurationSection<OcspConfig>();
            ConfigurationHandler.HasConfigurationSection<UddiConfig>();
            ConfigurationHandler.HasConfigurationSection<CacheConfig>();
            ConfigurationHandler.HasConfigurationSection<RootCertificateCollectionConfig>();
            ConfigurationHandler.HasConfigurationSection<OcesX509CertificateConfig>();
            ConfigurationHandler.HasConfigurationSection<SendingOptionConfig>();*/

           // EmailTransportUserConfig emailTransportConfig = ConfigurationHandler.GetConfigurationSection<EmailTransportUserConfig>();
        }

        public static void SetupConfiguration(string configurationFilePath) 
        {
            ConfigurationHandler.ConfigFilePath = configurationFilePath;
            ConfigurationHandler.Reset();

            /*ConfigurationHandler.GetConfigurationSection<LdapLookupFactoryConfig>();
            ConfigurationHandler.GetConfigurationSection<RevocationLookupFactoryConfig>();
            ConfigurationHandler.GetConfigurationSection<UddiLookupClientFactoryConfig>();
            ConfigurationHandler.GetConfigurationSection<RegistryLookupClientFactoryConfig>();
            ConfigurationHandler.GetConfigurationSection<LdapSettings>();
            ConfigurationHandler.GetConfigurationSection<OcspConfig>();
            ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            ConfigurationHandler.GetConfigurationSection<CacheConfig>();
            ConfigurationHandler.GetConfigurationSection<RootCertificateCollectionConfig>();
            ConfigurationHandler.GetConfigurationSection<OcesX509CertificateConfig>();
            ConfigurationHandler.GetConfigurationSection<SendingOptionConfig>();*/

            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();

            DefaultProfileMappingConfig profileMappings = new DefaultProfileMappingConfig();
            profileMappings.AddAll();

            //EmailTransportUserConfig emailTransportConfig = ConfigurationHandler.GetConfigurationSection<EmailTransportUserConfig>();
            
            //ConfigurationHandler.ConfigFilePath = configurationFilePath;
            //ConfigurationHandler.SaveToFile();
        }
    }
}