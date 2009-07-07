using dk.gov.oiosi.configuration;
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.security.revocation.ocsp;

namespace dk.gov.oiosi.test.integration {
    public class ConfigurationUtil {
        public static void SetupConfiguration() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.Add();

            DefaultProfileMappingConfig profileMappings = new DefaultProfileMappingConfig();
            profileMappings.AddAll();

            DefaultLdapConfig ldapConfig = new DefaultLdapConfig();
            ldapConfig.SetIfNotExistsLdapLookupFactoryConfig();
            ldapConfig.SetIfNotExistsDefaultLdapConfig();

            DefaultUddiConfig uddiConfig = new DefaultUddiConfig();
            uddiConfig.SetIfNotExistsUddiLookupFactoryConfig();
            uddiConfig.SetIfNotExistsDefaultUddiConfig();

            DefaultRootCertificateConfig rootCertificateConfig = new DefaultRootCertificateConfig();
            rootCertificateConfig.SetIfNotExistsProductionDefaultRootCertificateConfig();

            DefaultSchematronConfig schematronConfig = new DefaultSchematronConfig();
            schematronConfig.SetIfNotExistsOcesCertificateConfig();

            DefaultOcesCertificate ocesCertifcates = new DefaultOcesCertificate();
            ocesCertifcates.SetIfNotExistsOcesCertificateConfig();

            DefaultRevocationConfig defaultRevocationConfig = new DefaultRevocationConfig();
            defaultRevocationConfig.SetTestRevocationLookupFactoryConfig();

            OcspLookupTestConfig ocspLookupTestConfig = ConfigurationHandler.GetConfigurationSection<OcspLookupTestConfig>();
            ocspLookupTestConfig.ReturnPositiveResponse = true;

            EmailTransportUserConfig emailTransportConfig = ConfigurationHandler.GetConfigurationSection<EmailTransportUserConfig>();

            ConfigurationHandler.SaveToFile();
        }

    }
}