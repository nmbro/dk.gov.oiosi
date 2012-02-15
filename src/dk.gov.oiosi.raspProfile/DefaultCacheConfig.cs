/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *   Jacob Mogensen, mySupply ApS
  *
  */
using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.raspProfile
{

    /// <summary>
    /// Creates the default Cache configuration
    /// </summary>
    public class DefaultCacheConfig
    {
        /// <summary>
        /// Set default cache config values
        /// </summary>
        public void SetDefaultCacheConfig()
        {
            CacheConfig cacheConfig = ConfigurationHandler.GetConfigurationSection<CacheConfig>();
            string implementationAssembly = "dk.gov.oiosi.library";
            CacheConfiguration configurationValidity10 = new CacheConfiguration("validityTimeInMinutes", "10");
            CacheConfiguration configurationValidity60 = new CacheConfiguration("validityTimeInMinutes", "60");
            CacheConfiguration configurationFrequency10 = new CacheConfiguration("frequencyInMinutes", "10");
            CacheConfiguration configurationMaxSize = new CacheConfiguration("maxSize", "10");
            string cacheName = "CacheName";

            cacheConfig.OcspLookupCache = new CacheConfigElement();
            cacheConfig.OcspLookupCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[dk.gov.oiosi.security.revocation.RevocationResponse, dk.gov.oiosi.library]]";
            cacheConfig.OcspLookupCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(configurationValidity60);
            cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "OcspLookupCache"));

            cacheConfig.CrlLookupCache = new CacheConfigElement();
            cacheConfig.CrlLookupCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.Uri, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[dk.gov.oiosi.security.revocation.crl.CrlInstance, dk.gov.oiosi.library]]";
            cacheConfig.CrlLookupCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(configurationValidity60);
            cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "CrlLookupCache"));

            cacheConfig.UddiServiceCache = new CacheConfigElement();
            cacheConfig.UddiServiceCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.uddi.UddiLookupKey, dk.gov.oiosi.library],[System.Collections.Generic.IList`1[[dk.gov.oiosi.uddi.UddiService, dk.gov.oiosi.library]], mscorlib]]";
            cacheConfig.UddiServiceCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(configurationValidity60);
            cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "UddiServiceCache"));

            cacheConfig.UddiTModelCache = new CacheConfigElement();
            cacheConfig.UddiTModelCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.uddi.UddiId, dk.gov.oiosi.library],[dk.gov.oiosi.uddi.UddiTModel, dk.gov.oiosi.library]]";
            cacheConfig.UddiTModelCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(configurationValidity60);
            cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "UddiTModelCache"));

            cacheConfig.CertificateCache = new CacheConfigElement();
            cacheConfig.CertificateCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.security.CertificateSubject, dk.gov.oiosi.library],[System.Security.Cryptography.X509Certificates.X509Certificate2, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
            cacheConfig.CertificateCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.CertificateCache.CacheConfigurationCollection.Add(configurationValidity60);
            cacheConfig.CertificateCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.CertificateCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "CertificateCache"));

            cacheConfig.SchemaStoreCache = new CacheConfigElement();
            cacheConfig.SchemaStoreCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.QuantityCache`2[[System.String, mscorlib],[System.Xml.Schema.XmlSchemaSet, System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
            cacheConfig.SchemaStoreCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.SchemaStoreCache.CacheConfigurationCollection.Add(configurationMaxSize);
            cacheConfig.SchemaStoreCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "SchemaStoreCache"));

            cacheConfig.SchematronStoreCache = new CacheConfigElement();
            cacheConfig.SchematronStoreCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.QuantityCache`2[[System.String, mscorlib],[System.Xml.Xsl.XslCompiledTransform, System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
            cacheConfig.SchematronStoreCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.SchematronStoreCache.CacheConfigurationCollection.Add(configurationMaxSize);
            cacheConfig.SchematronStoreCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "SchematronStoreCache"));

            cacheConfig.MessageIdUnfinishedSignaturesCache = new CacheConfigElement();
            cacheConfig.MessageIdUnfinishedSignaturesCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[dk.gov.oiosi.extension.wcf.Interceptor.Security.UnfinishedSignatureValidationProof, dk.gov.oiosi.library]]";
            cacheConfig.MessageIdUnfinishedSignaturesCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.MessageIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationValidity10);
            cacheConfig.MessageIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.MessageIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "MessageIdUnfinishedSignaturesCache"));

            cacheConfig.SequenceIdUnfinishedSignaturesCache = new CacheConfigElement();
            cacheConfig.SequenceIdUnfinishedSignaturesCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[System.Collections.Generic.List`1[[dk.gov.oiosi.extension.wcf.Interceptor.Security.UnfinishedSignatureValidationProof, dk.gov.oiosi.library]], mscorlib]]";
            cacheConfig.SequenceIdUnfinishedSignaturesCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.SequenceIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationValidity10);
            cacheConfig.SequenceIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.SequenceIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "SequenceIdUnfinishedSignaturesCache"));
        }

        /// <summary>
        /// Sets the default cache configuration if it does not exist
        /// </summary>
        public void SetIfNotExistsDefaulCacheConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<CacheConfig>())
            {
                // The configuration exist, nothing to do
            }
            else
            {
                this.SetDefaultCacheConfig();
            }
        }
    }
}
