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
        private string ocspLookupCacheTimeInHours = string.Empty;

        private string crlLookupCacheTimeInHours = string.Empty;

        private string uddiServiceCacheTimeInHours = string.Empty;

        private string uddiTModelCacheTimeInHours = string.Empty;

        private string certificateCacheTimeInDays = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CacheConfig()
        {
        }

        #region RevocationLookup

        /// <summary>
        /// The OcspLookup cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string OcspLookupCacheTimeInHours
        {
            get
            {
                return this.ocspLookupCacheTimeInHours;
            }
            set
            {
                this.ocspLookupCacheTimeInHours = value;
            }
        }

        /// <summary>
        /// The OcspLookup cache TimeSpan
        /// </summary>
        public TimeSpan OcspLookupCacheTimeSpan
        {
            get
            {
                return this.TimeSpanFromHours(this.ocspLookupCacheTimeInHours);
            }
        }

        # endregion RevocationLookup

        # region CRL

        /// <summary>
        /// The Crl cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string CrlLookupCacheTimeInHours
        {
            get
            {
                return this.crlLookupCacheTimeInHours;
            }
            set
            {
                this.crlLookupCacheTimeInHours = value;
            }
        }

        /// <summary>
        /// The Crl cache TimeSpan
        /// </summary>
        public TimeSpan CrlLookupCacheTimeSpan
        {
            get
            {
                return this.TimeSpanFromHours(this.crlLookupCacheTimeInHours);
            }
        }

        # endregion CRL

        # region UddiService

        /// <summary>
        /// The UddiService cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string UddiServiceCacheTimeInHours
        {
            get
            {
                return this.uddiServiceCacheTimeInHours;
            }
            set
            {
                this.uddiServiceCacheTimeInHours = value;
            }
        }

        /// <summary>
        /// The UddiService cache TimeSpan
        /// </summary>
        public TimeSpan UddiServiceCacheTimeSpan
        {
            get
            {
                return this.TimeSpanFromHours(this.uddiServiceCacheTimeInHours);
            }
        }

        # endregion UddiService

        # region UddiTModel

        /// <summary>
        /// The UddiTModel cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string UddiTModelCacheTimeInHours
        {
            get
            {
                return this.uddiTModelCacheTimeInHours;
            }
            set
            {
                this.uddiTModelCacheTimeInHours = value;
            }
        }

        /// <summary>
        /// The UddiTModel cache TimeSpan
        /// </summary>
        public TimeSpan UddiTModelCacheTimeSpan
        {
            get
            {
                return this.TimeSpanFromHours(this.uddiTModelCacheTimeInHours);
            }
        }

        # endregion UddiTModel
                
        # region Certificate

        /// <summary>
        /// The Certificate cache TimeSpan as string (hh:MM:ss)
        /// </summary>
        public string CertificateCacheTimeInDays
        {
            get
            {
                return this.certificateCacheTimeInDays;
            }
            set
            {
                this.certificateCacheTimeInDays = value;
            }
        }

        /// <summary>
        /// The Certificate cache TimeSpan
        /// </summary>
        public TimeSpan CertificateCacheTimeSpan
        {
            get
            {
                return this.TimeSpanFromDays(this.certificateCacheTimeInDays);
            }
        }

        # endregion Certificate



        private TimeSpan TimeSpanFromHours(string value)
        {
            TimeSpan timeSpan;
            double time;
            if (string.IsNullOrEmpty(value))
            {
                time = 1;
            }
            else
            {
                if (double.TryParse(value, out time))
                {
                     // values succesfull parsed to boolean
                }
                else
                {
                     // parsing to double failed
                    // using default cache time
                    logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Warning, "Unable to parse the value '" + value + "' to a double.");
                    time = 1;
                }
            }

            timeSpan = TimeSpan.FromHours(time);
            return timeSpan;
        }

         
        private TimeSpan TimeSpanFromDays(string value)
        {
            TimeSpan timeSpan;
            double time;
            if (string.IsNullOrEmpty(value))
            {
                time = 1;
            }
            else
            {
                if (double.TryParse(value, out time))
                {
                     // values succesfull parsed to boolean
                }
                else
                {
                     // parsing to double failed
                    // using default cache time
                    logging.WCFLogger.Write(System.Diagnostics.TraceEventType.Warning, "Unable to parse the value '" + value + "' to a double.");
                    time = 1;
                }
            }

            timeSpan = TimeSpan.FromDays(time);
            return timeSpan;
        }

      /*  /// <summary>
        /// Create the TimeSpan based on the configuration values
        /// </summary>
        /// <param name="configuratedTime"></param>
        /// <returns></returns>
        private static System.TimeSpan CreateTimeSpan(string configuratedTime)
        {
            return CreateTimeSpan(configuratedTime, 0, 1, 0, 0);
        }*/


     /*   /// <summary>
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
        }*/
    }
}