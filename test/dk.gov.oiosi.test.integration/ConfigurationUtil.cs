using dk.gov.oiosi.configuration;
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.security.revocation.ocsp;

namespace dk.gov.oiosi.test.integration {
    public class ConfigurationUtil {
        public static void SetupConfiguration() {
            ConfigurationHandler.ConfigFilePath = "RaspConfiguration.xml";

            DefaultSchematronConfig schematronConfig = new DefaultSchematronConfig();
            schematronConfig.SetIfNotExistsOcesCertificateConfig();

            EmailTransportUserConfig emailTransportConfig = ConfigurationHandler.GetConfigurationSection<EmailTransportUserConfig>();

        }
    }
}