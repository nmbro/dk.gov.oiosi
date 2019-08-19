using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.lookup;
using dk.gov.oiosi.security.validation.configuration;
using NUnit.Framework;

namespace dk.gov.oiosi.test.unit.security.validation.configuration
{
    public class MultipleRootX509CertificateValidatorAppConfigurationTest
    {
        [Test]
        public void _01_TestInitConfigurationEmpty()
        {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.Live.xml";
            ConfigurationHandler.Reset();
            RootCertificateCollectionConfig rootCertificateCollectionConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateCollectionConfig>();
            RootCertificateLocation[] rootCertificateLocation = rootCertificateCollectionConfig.RootCertificateCollection;

            Assert.AreEqual(1, rootCertificateLocation.Length, "Expected 2 root certificated.");
        }
    }
}