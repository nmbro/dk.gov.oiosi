using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Configuration;
using dk.gov.oiosi.security.validation.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.test.unit.security.validation.configuration {
    
    public class MultipleRootX509CertificateValidatorAppConfigurationTest {
        
        [Test]
        public void _01_TestInitConfigurationEmpty() 
        {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.Test.xml";
            ConfigurationHandler.Reset();
            RootCertificateCollectionConfig rootCertificateCollectionConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateCollectionConfig>();
            RootCertificateLocation[] rootCertificateLocation = rootCertificateCollectionConfig.RootCertificateCollection;

            Assert.AreEqual(2, rootCertificateLocation.Length, "Expected 2 root certificated.");
        }
    }
}
