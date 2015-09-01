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
  *
  */

using System.Collections.Generic;
using System.Xml.Serialization;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {
    
    /// <summary>
    /// UDDI configuration for the user.settings file.
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class UddiConfig {

        private LookupRegistryFallbackConfig lookupRegistryFallbackConfig;

        private int _fallbackTimeoutMinutes = 0;
        private bool _tryOtherHostsOnFailure;

        ////private List<KeyType> keyTypeCollection = new List<KeyType>();

        

        /// <summary>
        /// Gets or sets the fallback timeout in minutes
        /// </summary>
        public int FallbackTimeoutMinutes {
            get { return _fallbackTimeoutMinutes; }
            set { _fallbackTimeoutMinutes = value; }
        }

        /// <summary>
        /// Gets or sets the list of registries to try lookup with
        /// </summary>
        public LookupRegistryFallbackConfig LookupRegistryFallbackConfig {
            get { return lookupRegistryFallbackConfig; }
            set { lookupRegistryFallbackConfig = value; }
        }

        /// <summary>
        /// Should other hosts be tried on failure
        /// </summary>
        public bool TryOtherHostsOnFailure {
            get { return _tryOtherHostsOnFailure; }
            set { _tryOtherHostsOnFailure = value; }
        }

        ////// [XmlArrayItem("Registry")]
        ////public List<KeyType> KeyTypeMappings
        ////{
        ////    get { return this.keyTypeCollection; }
        ////    set { this.keyTypeCollection = value; }
        ////}

        ////public bool TryGetKeyTypeValue(string KeyTypeCode, out string keyTypeValue)
        ////{
        ////    keyTypeValue = string.Empty;
        ////    bool found = false;
        ////    foreach (KeyType keyType in this.KeyTypeMappings)
        ////    {
        ////        if (keyType.Key.Equals(KeyTypeCode, System.StringComparison.OrdinalIgnoreCase))
        ////        {
        ////            keyTypeValue = keyType.Value;
        ////            found = true;
        ////            break;
        ////        }
        ////    }

        ////    ////if (!found)
        ////    ////{
        ////    ////    keyTypeValue = string.Empty;
        ////    ////}

        ////    return found;
        ////}


    }
}