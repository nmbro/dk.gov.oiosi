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
            CacheConfiguration configurationValidity = new CacheConfiguration("validityTimeInMinutes", "60");
            CacheConfiguration configurationFrequency = new CacheConfiguration("frequencyInMinutes", "10");
            CacheConfiguration configurationMaxSize = new CacheConfiguration("maxSize", "10");

            cacheConfig.OcspLookupCache = new CacheConfigElement();
            cacheConfig.OcspLookupCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.String, mscorlib],[dk.gov.oiosi.security.revocation.RevocationResponse, dk.gov.oiosi.library]]";
            cacheConfig.OcspLookupCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(configurationValidity);
            cacheConfig.OcspLookupCache.CacheConfigurationCollection.Add(configurationFrequency);

            cacheConfig.CrlLookupCache = new CacheConfigElement();
            cacheConfig.CrlLookupCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[System.Uri, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[dk.gov.oiosi.security.revocation.crl.CrlInstance, dk.gov.oiosi.library]]";
            cacheConfig.CrlLookupCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(configurationValidity);
            cacheConfig.CrlLookupCache.CacheConfigurationCollection.Add(configurationFrequency);

            cacheConfig.UddiServiceCache = new CacheConfigElement();
            cacheConfig.UddiServiceCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.uddi.UddiLookupKey, dk.gov.oiosi.library],[System.Collections.Generic.IList`1[[dk.gov.oiosi.uddi.UddiService, dk.gov.oiosi.library]], mscorlib]]";
            cacheConfig.UddiServiceCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(configurationValidity);
            cacheConfig.UddiServiceCache.CacheConfigurationCollection.Add(configurationFrequency);

            cacheConfig.UddiTModelCache = new CacheConfigElement();
            cacheConfig.UddiTModelCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.uddi.UddiId, dk.gov.oiosi.library],[dk.gov.oiosi.uddi.UddiTModel, dk.gov.oiosi.library]]";
            cacheConfig.UddiTModelCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(configurationValidity);
            cacheConfig.UddiTModelCache.CacheConfigurationCollection.Add(configurationFrequency);

            cacheConfig.CertificateCache = new CacheConfigElement();
            cacheConfig.CertificateCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.TimedCache`2[[dk.gov.oiosi.security.CertificateSubject, dk.gov.oiosi.library],[System.Security.Cryptography.X509Certificates.X509Certificate2, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
            cacheConfig.CertificateCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.CertificateCache.CacheConfigurationCollection.Add(configurationValidity);
            cacheConfig.CertificateCache.CacheConfigurationCollection.Add(configurationFrequency);

            cacheConfig.SchematronStoreCache = new CacheConfigElement();
            cacheConfig.SchematronStoreCache.ImplementationNamespaceClass = "dk.gov.oiosi.common.cache.QuantityCache`2[[System.String, mscorlib],[System.Xml.Xsl.XslCompiledTransform, System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
            cacheConfig.SchematronStoreCache.ImplementationAssembly = implementationAssembly;
            cacheConfig.SchematronStoreCache.CacheConfigurationCollection.Add(configurationMaxSize);
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
