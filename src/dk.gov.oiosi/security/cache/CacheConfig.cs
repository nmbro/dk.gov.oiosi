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
using dk.gov.oiosi.configuration;
using System;

namespace dk.gov.oiosi.security.cache {

    /// <summary>
    /// Configuration of the CacheConfig class. 
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class CacheConfig 
    {
        private string revocationLookupCacheTime = string.Empty;

        private string uddiServiceCacheTime = string.Empty;

        private string uddiTModelCacheTime = string.Empty;

        private string crlCacheTime = string.Empty;

        private string certificateCacheTime = string.Empty;


        /// <summary>
        /// Default constructor
        /// </summary>
        public CacheConfig()
        {
        }

        #region RevocationLookup

        /// <summary>
        /// The RevocationLookup cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string RevocationLookupCacheTimeSpan
        {
            get
            {
                return this.revocationLookupCacheTime;
            }
            set
            {
                this.revocationLookupCacheTime = value;
            }
        }

        /// <summary>
        /// The RevocationLookup cache TimeSpan
        /// </summary>
        public TimeSpan RevocationLookupTimeSpan
        {
            get
            {
                return CreateTimeSpan(this.revocationLookupCacheTime);
            }
        }

        # endregion RevocationLookup

        # region UddiService

        /// <summary>
        /// The UddiService cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string UddiServiceCacheTimeSpan
        {
            get
            {
                return this.uddiServiceCacheTime;
            }
            set
            {
                this.uddiServiceCacheTime = value;
            }
        }

        /// <summary>
        /// The UddiService cache TimeSpan
        /// </summary>
        public TimeSpan UddiServiceTimeSpan
        {
            get
            {
                return CreateTimeSpan(this.uddiServiceCacheTime);
            }
        }

        # endregion UddiService



        # region UddiTModel

        /// <summary>
        /// The UddiTModel cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string UddiTModelCacheTimeSpan
        {
            get
            {
                return this.uddiTModelCacheTime;
            }
            set
            {
                this.uddiTModelCacheTime = value;
            }
        }

        /// <summary>
        /// The UddiTModel cache TimeSpan
        /// </summary>
        public TimeSpan UddiTModelCache
        {
            get
            {
                return CreateTimeSpan(this.uddiTModelCacheTime);
            }
        }

        # endregion UddiTModel

        # region CRL

        /// <summary>
        /// The Crl cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string CrlCacheTimeSpan
        {
            get
            {
                return this.crlCacheTime;
            }
            set
            {
                this.crlCacheTime = value;
            }
        }

        /// <summary>
        /// The Crl cache TimeSpan
        /// </summary>
        public TimeSpan CrlCache
        {
            get
            {
                return CreateTimeSpan(this.crlCacheTime);
            }
        }

        # endregion CRL

        # region Certificate

        /// <summary>
        /// The Certificate cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string CertificateCacheTimeSpan
        {
            get
            {
                return this.certificateCacheTime;
            }
            set
            {
                this.certificateCacheTime = value;
            }
        }

        /// <summary>
        /// The Certificate cache TimeSpan
        /// </summary>
        public TimeSpan CertificateCache
        {
            get
            {
                return CreateTimeSpan(this.certificateCacheTime);
            }
        }

        # endregion Certificate

        /// <summary>
        /// Create the TimeSpan based on the configuration values
        /// </summary>
        /// <param name="configuratedTime"></param>
        /// <returns></returns>
        private static System.TimeSpan CreateTimeSpan(string configuratedTime)
        {
            return CreateTimeSpan(configuratedTime, 0, 1, 0, 0);
        }

        /// <summary>
        /// Create the cache timespan
        /// </summary>
        /// <param name="configuratedTime">The configurated value</param>
        /// <param name="days">default value</param>
        /// <param name="hours">default value</param>
        /// <param name="minutes">default value</param>
        /// <param name="seconds">default value</param>
        /// <returns></returns>
        private static System.TimeSpan CreateTimeSpan(string configuratedTime, int days, int hours, int minutes, int seconds)
        {
            TimeSpan cacheTime;

            string value = configuratedTime;
            if (string.IsNullOrEmpty(value))
            {
                // not defined, using default cache time
                cacheTime = new TimeSpan(days, hours, minutes, seconds);
            }
            else
            {
                if (System.TimeSpan.TryParse(value, out cacheTime))
                {
                    // values succesfull parsed to boolean
                }
                else
                {
                    // parsing to TimeSpan failed
                    // using default cache time
                    logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Warning, "Unable to parse the value '" + configuratedTime + "' to a timespan. Defaulting to (dd.hh:MM:ss): " + days + "." + hours + ":" + minutes + ":" + seconds + ".");
                    cacheTime = new TimeSpan(days, hours, minutes, seconds);
                }
            }

            return cacheTime;
        }
    }
}