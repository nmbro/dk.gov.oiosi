using System;

namespace dk.gov.oiosi.configuration
{
    /// <summary>
    /// Creates the default Cache configuration
    /// </summary>
    public class DefaultCacheConfig
    {
        const string implementationAssembly = "dk.gov.oiosi.library";
        const string cacheName = "CacheName";
        static CacheConfiguration configurationValidityHours1 = new CacheConfiguration("validityTimeInHours", "1");
        static CacheConfiguration configurationValidityHours24 = new CacheConfiguration("validityTimeInHours", "24");
        static CacheConfiguration configurationValidity10 = new CacheConfiguration("validityTimeInMinutes", "10");
        static CacheConfiguration configurationValidity60 = new CacheConfiguration("validityTimeInMinutes", "60");
        static CacheConfiguration configurationFrequency10 = new CacheConfiguration("frequencyInMinutes", "10");

        static CacheConfiguration configurationMaxSize10 = new CacheConfiguration("maxSize", "10");
        static CacheConfiguration configurationMaxSize20 = new CacheConfiguration("maxSize", "20");

        public static CacheConfigElement GetDefaultCacheConfigElement(string cacheType)
        {
            CacheConfig cacheConfig = ConfigurationHandler.GetConfigurationSection<CacheConfig>();

            switch (cacheType)
            {
                case "OcspLookupCache":
                    cacheConfig.OcspLookupCache = new CacheConfigElement();
                    cacheConfig.OcspLookupCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[dk.gov.oiosi.security.revocation.RevocationResponse, dk.gov.oiosi.library]]";
                    cacheConfig.OcspLookupCache.ImplementationAssembly = implementationAssembly;
                    cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(configurationValidityHours1);
                    cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(configurationFrequency10);
                    cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "OcspLookupCache"));
                    return cacheConfig.OcspLookupCache;
                case "CrlLookupCache":
                    cacheConfig.CrlLookupCache = new CacheConfigElement();
                    cacheConfig.CrlLookupCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.QuantityCache`2[[System.Uri, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[dk.gov.oiosi.security.revocation.crl.CrlInstance, dk.gov.oiosi.library]]";
                    cacheConfig.CrlLookupCache.ImplementationAssembly = implementationAssembly;
                    cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(configurationMaxSize10);
                    cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(configurationValidity10);
                    cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "CrlLookupCache"));
                    return cacheConfig.CrlLookupCache;
                case "UddiServiceCache":
                    cacheConfig.UddiServiceCache = new CacheConfigElement();
                    cacheConfig.UddiServiceCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.uddi.UddiLookupKey, dk.gov.oiosi.library],[System.Collections.Generic.IList`1[[dk.gov.oiosi.uddi.UddiService, dk.gov.oiosi.library]], mscorlib]]";
                    cacheConfig.UddiServiceCache.ImplementationAssembly = implementationAssembly;
                    cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(configurationValidityHours1);
                    cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(configurationFrequency10);
                    cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "UddiServiceCache"));
                    return cacheConfig.UddiServiceCache;
                case "UddiTModelCache":
                    cacheConfig.UddiTModelCache = new CacheConfigElement();
                    cacheConfig.UddiTModelCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.uddi.UddiId, dk.gov.oiosi.library],[dk.gov.oiosi.uddi.UddiTModel, dk.gov.oiosi.library]]";
                    cacheConfig.UddiTModelCache.ImplementationAssembly = implementationAssembly;
                    cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(configurationValidityHours24);
                    cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(configurationFrequency10);
                    cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "UddiTModelCache"));
                    return cacheConfig.UddiTModelCache;
                case "CertificateCache":
                    cacheConfig.CertificateCache = new CacheConfigElement();
                    cacheConfig.CertificateCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.security.CertificateSubject, dk.gov.oiosi.library],[System.Security.Cryptography.X509Certificates.X509Certificate2, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
                    cacheConfig.CertificateCache.ImplementationAssembly = implementationAssembly;
                    cacheConfig.CertificateCache.CacheConfigurationCollection.Add(configurationValidityHours24);
                    cacheConfig.CertificateCache.CacheConfigurationCollection.Add(configurationFrequency10);
                    cacheConfig.CertificateCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "CertificateCache"));
                    return cacheConfig.CertificateCache;;
                case "SchemaStoreCache":
                    cacheConfig.SchemaStoreCache = new CacheConfigElement();
                    cacheConfig.SchemaStoreCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.QuantityCache`2[[System.String, mscorlib],[System.Xml.Schema.XmlSchemaSet, System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
                    cacheConfig.SchemaStoreCache.ImplementationAssembly = implementationAssembly;
                    cacheConfig.SchemaStoreCache.CacheConfigurationCollection.Add(configurationMaxSize20);
                    cacheConfig.SchemaStoreCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "SchemaStoreCache"));
                    return cacheConfig.SchemaStoreCache;
                case "SchematronStoreCache":
                    cacheConfig.SchematronStoreCache = new CacheConfigElement();
                    cacheConfig.SchematronStoreCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.QuantityCache`2[[System.String, mscorlib],[dk.gov.oiosi.xml.schematron.CompiledXslt, dk.gov.oiosi.library]]";
                    cacheConfig.SchematronStoreCache.ImplementationAssembly = implementationAssembly;
                    cacheConfig.SchematronStoreCache.CacheConfigurationCollection.Add(configurationMaxSize20);
                    cacheConfig.SchematronStoreCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "SchematronStoreCache"));
                    return cacheConfig.SchematronStoreCache;
                case "MessageIdUnfinishedSignaturesCache":
                    cacheConfig.MessageIdUnfinishedSignaturesCache = new CacheConfigElement();
                    cacheConfig.MessageIdUnfinishedSignaturesCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[dk.gov.oiosi.extension.wcf.Interceptor.Security.UnfinishedSignatureValidationProof, dk.gov.oiosi.library]]";
                    cacheConfig.MessageIdUnfinishedSignaturesCache.ImplementationAssembly = implementationAssembly;
                    cacheConfig.MessageIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationValidity60);
                    cacheConfig.MessageIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationFrequency10);
                    cacheConfig.MessageIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "MessageIdUnfinishedSignaturesCache"));
                    return cacheConfig.MessageIdUnfinishedSignaturesCache;;
                case "SequenceIdUnfinishedSignaturesCache":
                    cacheConfig.SequenceIdUnfinishedSignaturesCache = new CacheConfigElement();
                    cacheConfig.SequenceIdUnfinishedSignaturesCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[System.Collections.Generic.List`1[[dk.gov.oiosi.extension.wcf.Interceptor.Security.UnfinishedSignatureValidationProof, dk.gov.oiosi.library]], mscorlib]]";
                    cacheConfig.SequenceIdUnfinishedSignaturesCache.ImplementationAssembly = implementationAssembly;
                    cacheConfig.SequenceIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationValidity60);
                    cacheConfig.SequenceIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationFrequency10);
                    cacheConfig.SequenceIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "SequenceIdUnfinishedSignaturesCache"));
                    return cacheConfig.SequenceIdUnfinishedSignaturesCache;;
                default:
                    throw new NotImplementedException($"Default cache for '{cacheType}' has not been defined.");
            }
        }

        /// <summary>
        /// Set default cache config values
        /// </summary>
        public virtual void SetDefaultCacheConfig()
        {
            CacheConfig cacheConfig = ConfigurationHandler.GetConfigurationSection<CacheConfig>();

            cacheConfig.OcspLookupCache = new CacheConfigElement();
            cacheConfig.OcspLookupCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[dk.gov.oiosi.security.revocation.RevocationResponse, dk.gov.oiosi.library]]";
            cacheConfig.OcspLookupCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(configurationValidityHours1);
            cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "OcspLookupCache"));

            cacheConfig.CrlLookupCache = new CacheConfigElement();
            cacheConfig.CrlLookupCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.QuantityCache`2[[System.Uri, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[dk.gov.oiosi.security.revocation.crl.CrlInstance, dk.gov.oiosi.library]]";
            cacheConfig.CrlLookupCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(configurationMaxSize10);
            cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(configurationValidity10);
            cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "CrlLookupCache"));

            cacheConfig.UddiServiceCache = new CacheConfigElement();
            cacheConfig.UddiServiceCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.uddi.UddiLookupKey, dk.gov.oiosi.library],[System.Collections.Generic.IList`1[[dk.gov.oiosi.uddi.UddiService, dk.gov.oiosi.library]], mscorlib]]";
            cacheConfig.UddiServiceCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(configurationValidityHours1);
            cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "UddiServiceCache"));

            cacheConfig.UddiTModelCache = new CacheConfigElement();
            cacheConfig.UddiTModelCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.uddi.UddiId, dk.gov.oiosi.library],[dk.gov.oiosi.uddi.UddiTModel, dk.gov.oiosi.library]]";
            cacheConfig.UddiTModelCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(configurationValidityHours24);
            cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "UddiTModelCache"));

            cacheConfig.CertificateCache = new CacheConfigElement();
            cacheConfig.CertificateCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.security.CertificateSubject, dk.gov.oiosi.library],[System.Security.Cryptography.X509Certificates.X509Certificate2, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
            cacheConfig.CertificateCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.CertificateCache.CacheConfigurationCollection.Add(configurationValidityHours24);
            cacheConfig.CertificateCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.CertificateCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "CertificateCache"));

            cacheConfig.SchemaStoreCache = new CacheConfigElement();
            cacheConfig.SchemaStoreCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.QuantityCache`2[[System.String, mscorlib],[System.Xml.Schema.XmlSchemaSet, System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
            cacheConfig.SchemaStoreCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.SchemaStoreCache.CacheConfigurationCollection.Add(configurationMaxSize20);
            cacheConfig.SchemaStoreCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "SchemaStoreCache"));

            cacheConfig.SchematronStoreCache = new CacheConfigElement();
            cacheConfig.SchematronStoreCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.QuantityCache`2[[System.String, mscorlib],[dk.gov.oiosi.xml.schematron.CompiledXslt, dk.gov.oiosi.library]]";
            cacheConfig.SchematronStoreCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.SchematronStoreCache.CacheConfigurationCollection.Add(configurationMaxSize20);
            cacheConfig.SchematronStoreCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "SchematronStoreCache"));

            cacheConfig.MessageIdUnfinishedSignaturesCache = new CacheConfigElement();
            cacheConfig.MessageIdUnfinishedSignaturesCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[dk.gov.oiosi.extension.wcf.Interceptor.Security.UnfinishedSignatureValidationProof, dk.gov.oiosi.library]]";
            cacheConfig.MessageIdUnfinishedSignaturesCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.MessageIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationValidity60);
            cacheConfig.MessageIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.MessageIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "MessageIdUnfinishedSignaturesCache"));

            cacheConfig.SequenceIdUnfinishedSignaturesCache = new CacheConfigElement();
            cacheConfig.SequenceIdUnfinishedSignaturesCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[System.Collections.Generic.List`1[[dk.gov.oiosi.extension.wcf.Interceptor.Security.UnfinishedSignatureValidationProof, dk.gov.oiosi.library]], mscorlib]]";
            cacheConfig.SequenceIdUnfinishedSignaturesCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.SequenceIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationValidity60);
            cacheConfig.SequenceIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(configurationFrequency10);
            cacheConfig.SequenceIdUnfinishedSignaturesCache.CacheConfigurationCollection.Add(new CacheConfiguration(cacheName, "SequenceIdUnfinishedSignaturesCache"));
        }

        /// <summary>
        /// Sets the default cache configuration if it does not exist
        /// </summary>
        public virtual void SetIfNotExistsDefaulCacheConfig()
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
