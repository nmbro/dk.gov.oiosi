using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.security;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security.revocation.crl;

namespace dk.gov.oiosi.test.unit.configuration
{
    [TestFixture]
    public class CacheConfigTest
    {

        [TestFixtureSetUp]
        public void Setup()
        {
            ConfigurationHandler.ConfigFilePath = "Resources/RaspConfiguration.Test.xml";
            ConfigurationHandler.Reset();
        }

        [Test]
        public void GetCacheTest()
        {
            FileInfo fileInfo = new FileInfo(ConfigurationHandler.ConfigFilePath);
            if (!fileInfo.Exists)
            {
                throw new Exception("Rasp File does not exist: " + fileInfo.FullName);
            }

            ICache<string, RevocationResponse> ocspLookupCache = CacheFactory.Instance.OcspLookupCache;
            ICache<Uri, CrlInstance> crlLookupCache = CacheFactory.Instance.CrlLookupCache;
            ICache<UddiLookupKey, IList<UddiService>> uddiServiceCache = CacheFactory.Instance.UddiServiceCache;
            ICache<UddiId, UddiTModel> uddiTModelCache = CacheFactory.Instance.UddiTModelCache;
            ICache<CertificateSubject, X509Certificate2> certificateCache = CacheFactory.Instance.CertificateCache;

            Assert.IsNotNull(ocspLookupCache);
            Assert.IsNotNull(crlLookupCache);
            Assert.IsNotNull(uddiServiceCache);
            Assert.IsNotNull(uddiTModelCache);
            Assert.IsNotNull(certificateCache);

        }
    }
}
