/*
 * Copyright (c) 2007, Danish National IT and Telecom Agency All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 *
 *     * Redistributions of source code must retain the above copyright notice,
 *       this list of conditions and the following disclaimer.
 *
 *     * Redistributions in binary form must reproduce the above copyright notice,
 *       this list of conditions and the following disclaimer in the documentation
 *       and/or other materials provided with the distribution.
 *
 *     * Neither the name of the Danish National IT and Telecom Agency nor the names
 *       of its contributors may be used to endorse or promote products derived from
 *       this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 *
 */

using System.IO;
using System.Threading;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile;
using NUnit.Framework;

namespace dk.gov.oiosi.test.unit.raspProfile
{
    [TestFixture]
    public class RaspConfigurationTest
    {
        /// <summary>
        /// Deletes the old config file
        /// </summary>
        [Test]
        public void SetupAllSections()
        {
            string fileName = "RaspConfiguration.UnitTest.SetupAllSections.xml";
            FileInfo fileInfo = new FileInfo(fileName);

            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            while (File.Exists(fileName))
            {
                // wait
                Thread.Sleep(1);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }

            ConfigurationHandler.ConfigFilePath = fileName;
            ConfigurationHandler.Reset();

            SetupDefaultDocumentTypes();
            SetupProfileMappings();
            SetupDefaultLdapConfig();
            SetupDefaultOscpConfig();
            SetupDefaultUddiConfig();
            SetupDefaultCacheConfig();
            SetupDefaultRootCertificateConfig();
            SetupDefaultOcesCertificates();
            ConfigurationHandler.SaveToFile();

            Assert.IsTrue(File.Exists(ConfigurationHandler.ConfigFilePath));
            FileInfo file = new FileInfo(ConfigurationHandler.ConfigFilePath);
            Assert.IsTrue(file.Length > 1024);

            DocumentTypeCollectionConfig docTypeConfig =
                ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            Assert.AreEqual(35, docTypeConfig.DocumentTypes.Length, "Expected number of document types not found.");

            ProfileMappingCollectionConfig profileMappingConfig =
                ConfigurationHandler.GetConfigurationSection<ProfileMappingCollectionConfig>();

            //// Count : 32 - OIOUBL + UTS Count : 39 - OIOUBL1.6
            //// Rasp 2.1.0: Peppol 7 new profiles ->
            //// NemKonto + 2.
            //// Count 43
            Assert.AreEqual(43, profileMappingConfig.ProfileMappings.Length, "Expected number of profilemappings not found.");
        }

        private void SetupDefaultCacheConfig()
        {
            DefaultCacheConfig defaultCacheConfig = new DefaultCacheConfig();
            defaultCacheConfig.SetIfNotExistsDefaulCacheConfig();
        }

        private void SetupDefaultDocumentTypes()
        {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.Add();
        }

        /// <summary>
        /// Setup default LDAP settings
        /// </summary>
        private void SetupDefaultLdapConfig()
        {
            DefaultLdapConfig ldapConfig = new DefaultLdapConfig();
            ldapConfig.SetIfNotExistsLdapLookupFactoryConfig();
            ldapConfig.SetIfNotExistsDefaultLdapConfig();
        }

        private void SetupDefaultOcesCertificates()
        {
            DefaultOcesCertificate ocesCertifcates = new DefaultOcesCertificate();
            ocesCertifcates.SetIfNotExistsOcesCertificateConfig();
        }

        /// <summary>
        /// Setup default OSCP settings
        /// </summary>
        private void SetupDefaultOscpConfig()
        {
            DefaultRevocationConfig revocationConfig = new DefaultRevocationOcspConfig();
            revocationConfig.SetIfNotExistsRevocationLookupFactoryConfig();
            revocationConfig.SetIfNotExistsOscpConfig();
        }

        private void SetupDefaultRootCertificateConfig()
        {
            DefaultRootCertificateCollectionConfig rootCertificateConfig = new DefaultRootCertificateCollectionConfig();
            rootCertificateConfig.SetIfNotExistsProductionDefaultRootCertificateCollectionConfig();
        }

        /// <summary>
        /// Setup default UDDI settings
        /// </summary>
        private void SetupDefaultUddiConfig()
        {
            DefaultUddiConfig uddiConfig = new DefaultUddiConfig();
            uddiConfig.SetIfNotExistsUddiLookupFactoryConfig();
            uddiConfig.SetIfNotExistsDefaultUddiConfig();
        }

        private void SetupProfileMappings()
        {
            DefaultProfileMappingConfig profileMappings = new DefaultProfileMappingConfig();
            profileMappings.AddAll();
        }
    }
}