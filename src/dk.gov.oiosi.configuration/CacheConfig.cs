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

namespace dk.gov.oiosi.configuration
{
    using System;
    using System.Collections.Generic;
    using dk.gov.oiosi.exception;
    using System.Xml.Serialization;

    /// <summary>
    /// Configuration of the CacheConfig class. 
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class CacheConfig
    {
        private CacheConfigElement ocspLookupCache = null;

        private CacheConfigElement crlLookupCache = null;

        private CacheConfigElement uddiServiceCache = null;

        private CacheConfigElement uddiTModelCache = null;

        private CacheConfigElement certificateCache = null;

        private CacheConfigElement schematronStoreCache = null;

        private CacheConfigElement schemaStoreCache = null;

        /// <summary>
        /// Default constructor used by XMLSerialization. It should not be used.
        /// </summary>
        public CacheConfig()
        {
            this.ocspLookupCache = new CacheConfigElement();
            this.crlLookupCache = new CacheConfigElement();
            this.uddiServiceCache = new CacheConfigElement();
            this.uddiTModelCache = new CacheConfigElement();
            this.certificateCache = new CacheConfigElement();
            this.schematronStoreCache = new CacheConfigElement();
            this.schemaStoreCache = new CacheConfigElement();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ocspLookupCache"></param>
        /// <param name="crlLookupCache"></param>
        /// <param name="uddiServiceCache"></param>
        /// <param name="uddiTModelCache"></param>
        /// <param name="certificateCache"></param>
        /// <param name="schematronStoreCache"></param>
        /// <param name="schemaStoreCache"></param>
        public CacheConfig(CacheConfigElement ocspLookupCache, CacheConfigElement crlLookupCache, CacheConfigElement uddiServiceCache, CacheConfigElement uddiTModelCache, CacheConfigElement certificateCache, CacheConfigElement schematronStoreCache, CacheConfigElement  schemaStoreCache)
        {
            if (ocspLookupCache == null)
            {
                throw new NullArgumentException("CacheConfigElement");
            }
            if (crlLookupCache == null)
            {
                throw new NullArgumentException("crlLookupCache");
            }
            if (uddiServiceCache == null)
            {
                throw new NullArgumentException("uddiServiceCache");
            }
            if (uddiTModelCache == null)
            {
                throw new NullArgumentException("uddiTModelCache");
            }
            if (certificateCache == null)
            {
                throw new NullArgumentException("certificateCache");
            }
            if (schematronStoreCache == null)
            {
                throw new NullArgumentException("schematromStoreCache");
            }
            if (schematronStoreCache == null)
            {
                throw new NullArgumentException("schemaStoreCache");
            }

            this.ocspLookupCache = ocspLookupCache;
            this.crlLookupCache = crlLookupCache;
            this.uddiServiceCache = uddiServiceCache;
            this.uddiTModelCache = uddiTModelCache;
            this.certificateCache = certificateCache;
            this.schematronStoreCache = schematronStoreCache;
            this.schemaStoreCache = schemaStoreCache;
        }

        /// <summary>
        /// Gets or set the ocspLookupCache configuration element
        /// </summary>
        [XmlElement("OcspLookupCache")]
        public CacheConfigElement OcspLookupCache
        {
            get
            {
                return this.ocspLookupCache;
            }

            set
            {
                this.ocspLookupCache = value;
            }
        }

        /// <summary>
        /// Gets or set the crlLookupCache configuration element
        /// </summary>
        [XmlElement("CrlLookupCache")]
        public CacheConfigElement CrlLookupCache
        {
            get
            {
                return this.crlLookupCache;
            }

            set
            {
                this.crlLookupCache = value;
            }
        }

        /// <summary>
        /// Gets or set the uddiServiceCache configuration element
        /// </summary>
        [XmlElement("UddiServiceCache")]
        public CacheConfigElement UddiServiceCache
        {
            get
            {
                return this.uddiServiceCache;
            }

            set
            {
                this.uddiServiceCache = value;
            }
        }

        /// <summary>
        /// Gets or set the uddiTModelCache configuration element
        /// </summary>
        [XmlElement("UddiTModelCache")]
        public CacheConfigElement UddiTModelCache
        {
            get
            {
                return this.uddiTModelCache;
            }

            set
            {
                this.uddiTModelCache = value;
            }
        }

        /// <summary>
        /// Gets or set the uddiTModelCache configuration element
        /// </summary>
        [XmlElement("CertificateCache")]
        public CacheConfigElement CertificateCache
        {
            get
            {
                return this.certificateCache;
            }

            set
            {
                this.certificateCache = value;
            }
        }

        /// <summary>
        /// Gets or set the SchematronStoreCache configuration element
        /// </summary>
        [XmlElement("SchematronStoreCache")]
        public CacheConfigElement SchematronStoreCache
        {
            get
            {
                return this.schematronStoreCache;
            }

            set
            {
                this.schematronStoreCache = value;
            }
        }

        /// <summary>
        /// Gets or set the SchemaStoreCache configuration element
        /// </summary>
        [XmlElement("SchemaStoreCache")]
        public CacheConfigElement SchemaStoreCache
        {
            get
            {
                return this.schemaStoreCache;
            }

            set
            {
                this.schemaStoreCache = value;
            }
        }
    }
}
