﻿

namespace dk.gov.oiosi.configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using dk.gov.oiosi.uddi;
    using dk.gov.oiosi.common.cache;
    using System.Security.Cryptography.X509Certificates;
    using dk.gov.oiosi.security;
    using dk.gov.oiosi.security.revocation;
    using dk.gov.oiosi.security.revocation.crl;
    using dk.gov.oiosi.logging;
    using System.Reflection;
    using System.Xml.Xsl;
    using System.Xml.Schema;

    /// <summary>
    /// The factory that create the differents caches 
    /// </summary>
    public class CacheFactory
    {
        /// <summary>
        /// Singleton implementation of the cacheFactory
        /// </summary>
        private static CacheFactory cacheFactory = new CacheFactory();

        /// <summary>
        /// Cache to store the ocsp lookup - check is a certificate is valid
        /// </summary>
        private ICache<string, RevocationResponse> ocspLookupCache = null;
        
        /// <summary>
        /// Cache to store the result from the CRL list - the certificates that has been revoked, ala blacklisted
        /// </summary>
        private ICache<Uri, CrlInstance> crlLookupCache = null;

        /// <summary>
        /// 
        /// </summary>
        private ICache<UddiLookupKey, IList<UddiService>> uddiServiceCache = null;
        
        /// <summary>
        /// 
        /// </summary>
        private ICache<UddiId, UddiTModel> uddiTModelCache = null;
        
        /// <summary>
        /// Cache to store certificated, that has been retrived from LDAP
        /// </summary>
        private ICache<CertificateSubject, X509Certificate2> certificateCache = null;
        
        /// <summary>
        /// Cache to store the compiled schematron
        /// </summary>
        private ICache<string, XslCompiledTransform> schematronStoreCache = null;
        
        /// <summary>
        /// Cache to store the compiled schema file
        /// </summary>
        private ICache<string, XmlSchemaSet> schemaStoreCache = null;

        /// <summary>
        /// Private constructor
        /// </summary>
        private CacheFactory()
        {
            CacheConfig cacheConfig = ConfigurationHandler.GetConfigurationSection<CacheConfig>();
            CacheConfigElement element;

            // ocspLookupCache
            element = cacheConfig.OcspLookupCache;
            this.ocspLookupCache = this.Create<ICache<string, RevocationResponse>>(element, "OcspLookupCache");
            // crlLookupCache
            element = cacheConfig.CrlLookupCache;
            this.crlLookupCache = this.Create<ICache<Uri, CrlInstance>>(element, "CrlLookupCache");

            // uddiServiceCache
            element = cacheConfig.UddiServiceCache;
            this.uddiServiceCache = this.Create<ICache<UddiLookupKey, IList<UddiService>>>(element, "UddiServiceCache");
            
            // uddiTModelCache
            element = cacheConfig.UddiTModelCache;
            this.uddiTModelCache = this.Create<ICache<UddiId, UddiTModel>>(element, "UddiTModelCache");
            
            // certificateCache
            element = cacheConfig.CertificateCache;
            this.certificateCache = this.Create<ICache<CertificateSubject, X509Certificate2>>(element, "CertificateCache");

            // schematronCache
            element = cacheConfig.SchematronStoreCache;
            this.schematronStoreCache = this.Create<ICache<string, XslCompiledTransform>>(element, "SchematronStoreCache");

            // schematronCache
            element = cacheConfig.SchemaStoreCache;
            this.schemaStoreCache = this.Create<ICache<string, XmlSchemaSet>>(element, "SchemaStoreCache");
        }

        private T Create<T>(CacheConfigElement element, string name)
        {
            T cache;

            if (string.IsNullOrEmpty(element.ImplementationNamespaceClass))
            {
                throw new NotImplementedException("The Assembly and NamespaceClass for the cache '" + name + "' is not defined correct.");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(element.ImplementationNamespaceClass);
            if(! string.IsNullOrEmpty(element.ImplementationAssembly))
            {
                builder.Append(", ");
                builder.Append(element.ImplementationAssembly);
            }
            
            string qualifiedTypename = builder.ToString();

            Type cacheType = Type.GetType(qualifiedTypename);
            
            if (cacheType == null)
            {
                Logger.Write("Cache type not valid", "The cache type with qualifiedTypename '" + qualifiedTypename + "' is null.", LoggerCategories.Debug);
                throw new FailedToLoadLookupTypeException(qualifiedTypename);
            }

            //Type parameter = typeof(cacheType);

            Type[] parameterArray = new Type[] { typeof(IDictionary<string,string>) };
            object[] objectArray = new object[] { element.GetDictionary() };
            ConstructorInfo constructorInfo = cacheType.GetConstructor(parameterArray);
            if (constructorInfo == null)
            {
                throw new Exception("Cache implementation must contain a construtore, that takes a IDictionary<string,string> as parameter.");
            }

            cache = (T)constructorInfo.Invoke(objectArray);

            return cache;
        }

        /// <summary>
        /// Get the CacheFactory instance
        /// </summary>
        public static CacheFactory Instance
        {
            get
            {
                return cacheFactory;
            }
        }

        /// <summary>
        /// Gets the Ocsp cache
        /// </summary>
        public ICache<string, RevocationResponse> OcspLookupCache
        {
            get
            {
                return this.ocspLookupCache;
            }
        }

        /// <summary>
        /// Gets the crl cache
        /// </summary>
        public ICache<Uri, CrlInstance> CrlLookupCache
        {
            get
            {
                return this.crlLookupCache;
            }
        }

        /// <summary>
        /// Gets the uddi service cache
        /// </summary>
        public ICache<UddiLookupKey, IList<UddiService>> UddiServiceCache
        {
            get
            {
                return this.uddiServiceCache;
            }
        }

        /// <summary>
        /// Gets the uddi service cache
        /// </summary>
        public ICache<UddiId, UddiTModel> UddiTModelCache
        {
            get
            {
                return this.uddiTModelCache;
            }
        }

        /// <summary>
        /// Get the certificates cache
        /// </summary>
        public ICache<CertificateSubject, X509Certificate2> CertificateCache
        {
            get
            {
                return this.certificateCache;
            }
        }

        /// <summary>
        /// Get the schematron cache
        /// </summary>
        public ICache<string, XslCompiledTransform> SchematrongStoreCache
        {
            get
            {
                return this.schematronStoreCache;
            }
        }

        /// <summary>
        /// Get the schematron cache
        /// </summary>
        public ICache<string, XmlSchemaSet> SchemaStoreCache
        {
            get
            {
                return this.schemaStoreCache;
            }
        }
    }
}