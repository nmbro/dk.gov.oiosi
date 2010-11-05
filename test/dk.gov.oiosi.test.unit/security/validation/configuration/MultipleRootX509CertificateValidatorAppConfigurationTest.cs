using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Configuration;
using dk.gov.oiosi.security.validation.configuration;

namespace dk.gov.oiosi.test.unit.security.validation.configuration {
    
    public class MultipleRootX509CertificateValidatorAppConfigurationTest {
        
        [Test]
        public void _01_TestInitConfigurationEmpty() {
            var configuration = (MultipleRootX509CertificateValidatorAppConfiguration)ConfigurationManager.GetSection(MultipleRootX509CertificateValidatorAppConfiguration.MultipleRootX509CertificateValidatorAppConfigurationName + "_01");
            Assert.IsNotNull(configuration.CertificateStoreIdentificationConfigurationCollection);
        }

        [Test]
        public void _02_TestInitConfigurationOneElement()
        {
            var configuration = (MultipleRootX509CertificateValidatorAppConfiguration)ConfigurationManager.GetSection(MultipleRootX509CertificateValidatorAppConfiguration.MultipleRootX509CertificateValidatorAppConfigurationName + "_02");
            Assert.IsNotNull(configuration.CertificateStoreIdentificationConfigurationCollection);
            int count = 0;
            foreach (var element in configuration.CertificateStoreIdentificationConfigurationCollection)
            {
                count++;
            }
            Assert.AreEqual(1, count);
        }

        [Test]
        public void _03_TestInitConfigurationOneElement()
        {
            var configuration = (MultipleRootX509CertificateValidatorAppConfiguration)ConfigurationManager.GetSection(MultipleRootX509CertificateValidatorAppConfiguration.MultipleRootX509CertificateValidatorAppConfigurationName + "_03");
            Assert.IsNotNull(configuration.CertificateStoreIdentificationConfigurationCollection);
            int count = 0;
            foreach (var element in configuration.CertificateStoreIdentificationConfigurationCollection)
            {
                count++;
            }
            Assert.AreEqual(2, count);
        }
    }
}
