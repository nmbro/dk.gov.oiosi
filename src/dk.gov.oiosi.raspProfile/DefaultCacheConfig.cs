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
using dk.gov.oiosi.security.cache;

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
            cacheConfig.UddiServiceCacheTimeSpan = "01:00:00";
            cacheConfig.UddiTModelCacheTimeSpan = "01:00:00";
            cacheConfig.RevocationLookupCacheTimeSpan = "01:00:00";
            cacheConfig.CrlCacheTimeSpan = "01:00:00";
            cacheConfig.CertificateCacheTimeSpan = "01.00:00:00";
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
