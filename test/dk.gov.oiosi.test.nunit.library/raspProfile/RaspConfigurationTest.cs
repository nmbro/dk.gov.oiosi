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
using System;
using System.IO;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile;
using NUnit.Framework;

namespace dk.gov.oiosi.test.nunit.library.raspProfile {

    [TestFixture]
    public class RaspConfigurationTest {


        /// <summary>
        /// Deletes the old config file
        /// </summary>
        [Test]
        public void SetupAllSections() {
            SetupDefaultDocumentTypes();
            SetupProfileMappings();
            SetupDefaultLdapConfig();
            SetupDefaultOscpConfig();
            SetupDefaultUddiConfig();
            SetupDefaultRootCertificateConfig();
            SetupDefaultOcesCertificates();
            ConfigurationHandler.SaveToFile();

            Assert.IsTrue(File.Exists(ConfigurationHandler.ConfigFilePath));
            FileInfo file = new FileInfo(ConfigurationHandler.ConfigFilePath);
            Assert.IsTrue(file.Length > 1024);

            DocumentTypeCollectionConfig docTypeConfig =
                ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            Assert.AreEqual(docTypeConfig.DocumentTypes.Length, 8);

            OioublProfileMappingCollectionConfig profileMappingConfig =
                ConfigurationHandler.GetConfigurationSection<OioublProfileMappingCollectionConfig>();
            Assert.AreEqual(profileMappingConfig.ProfileMappings.Length, 43);
        }

        private void SetupDefaultDocumentTypes() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.Add();
        }

        private void SetupProfileMappings() {
            OioublProfileMappingCollection profileMappings = new OioublProfileMappingCollection();
            profileMappings.AddAll();
        }

        /// <summary>
        /// Setup default LDAP settings
        /// </summary>
        private void SetupDefaultLdapConfig() {
            DefaultLdapConfig ldapConfig = new DefaultLdapConfig();
            ldapConfig.SetIfNotExistsLdapLookupFactoryConfig();
            ldapConfig.SetIfNotExistsDefaultLdapConfig();
        }

        /// <summary>
        /// Setup default OSCP settings
        /// </summary>
        private void SetupDefaultOscpConfig() {
            DefaultOcspConfig ocspConfig = new DefaultOcspConfig();
            ocspConfig.SetIfNotExistsOcspLookupFactoryConfig();
            ocspConfig.SetIfNotExistsOscpConfig();
        }

        /// <summary>
        /// Setup default UDDI settings
        /// </summary>
        private void SetupDefaultUddiConfig() {
            DefaultUddiConfig uddiConfig = new DefaultUddiConfig();
            uddiConfig.SetIfNotExistsUddiLookupFactoryConfig();
            uddiConfig.SetIfNotExistsDefaultUddiConfig();
        }

        private void SetupDefaultRootCertificateConfig() {
            DefaultRootCertificateConfig rootCertificateConfig = new DefaultRootCertificateConfig();
            rootCertificateConfig.SetIfNotExistsProductionDefaultRootCertificateConfig();
        }

        private void SetupDefaultOcesCertificates() {
            DefaultOcesCertificate ocesCertifcates = new DefaultOcesCertificate();
            ocesCertifcates.SetIfNotExistsOcesCertificateConfig();
        }

    }
}
