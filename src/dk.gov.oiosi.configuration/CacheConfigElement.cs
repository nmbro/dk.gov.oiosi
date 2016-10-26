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
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// Implement the configuration for one cache
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class CacheConfigElement
    {
        /// <summary>
        /// The namespace and class name
        /// </summary>
        private string implementationNamespaceClass = string.Empty;

        /// <summary>
        /// The assembly name
        /// </summary>
        private string implementationAssembly = string.Empty;

        /// <summary>
        /// The list of cache configurations
        /// </summary>
        private List<CacheConfiguration> cacheConfigurationList = null;

        /// <summary>
        /// Default constructor used by XMLSerialization. It should not be used.
        /// </summary>
        public CacheConfigElement()
        {
            cacheConfigurationList = new List<CacheConfiguration>();
        }

        /// <summary>
        /// Gets or sets the impmementations classname
        /// </summary>
        [XmlElement("ImplementationNamespaceClass")]
        public string ImplementationNamespaceClass
        {
            get
            {
                return this.implementationNamespaceClass;
            }

            set
            {
                this.implementationNamespaceClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the implementations assembly
        /// </summary>
        [XmlElement("ImplementationAssembly")]
        public string ImplementationAssembly
        {
            get
            {
                return this.implementationAssembly;
            }

            set
            {
                this.implementationAssembly = value;
            }
        }

        /// <summary>
        /// List of Cache Configuration
        /// </summary>
        [XmlArrayItem("Configuration")]
        public List<CacheConfiguration> CacheConfigurationCollection
        {
            get 
            { 
                return this.cacheConfigurationList; 
            }

            set
            {
                this.cacheConfigurationList = value; 
            }
        }

        /// <summary>
        /// Get the caches key/value as a dictionary
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetDictionary()
        {
            IDictionary<string, string> map = new Dictionary<string, string>();

            foreach (CacheConfiguration config in this.cacheConfigurationList)
            {
                map.Add(config.Key, config.Value);
            }

            return map;
        }
    }
}
