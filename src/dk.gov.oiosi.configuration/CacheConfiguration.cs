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
    /// 
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class CacheConfiguration
    {
        /// <summary>
        /// The key parameter
        /// </summary>
        private string key;

        /// <summary>
        /// The value parameter
        /// </summary>
        private string value;

        /// <summary>
        /// Default constructor used by XMLSerialization. It should not be used.
        /// </summary>
        public CacheConfiguration()
        { }

        /// <summary>
        /// Implementation of the CacheConfiguration
        /// </summary>
        /// <param name="key">The key parameter</param>
        /// <param name="value">The value parameter</param>
        public CacheConfiguration(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        /// <summary>
        /// Gets or sets the configuration key parameter
        /// </summary>
        [XmlElement("Key")]
        public string Key
        {
            set
            {
                this.key = value;
            }
            get
            {
                return this.key;
            }
        }

        /// <summary>
        /// Gets or sets the configuration value parameter
        /// </summary>
        [XmlElement("Value")]
        public string Value
        {
            set
            {
                this.value = value;
            }
            get
            {
                return this.value;
            }
        }

    }
}
