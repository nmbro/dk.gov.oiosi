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
  *	  Dmitriy Lapko, TrueLink A/S
  *
  */
using System;
using System.Collections.Generic;
using dk.gov.oiosi.logging;
using dk.gov.oiosi.configuration;
using System.Threading;

namespace dk.gov.oiosi.common.cache {
    /// <summary>
    /// Simple, generic timed cache type. The cache is traversed each time an element is inserted or 
    /// extracted.
    /// </summary>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public class TimedCache<TKey, TValue> : ICache<TKey,TValue>
    {
        private const int DEFAULT_FREQUENCY_IN_MINUTES = 10;
        private const int DEFAULT_VALIDITY_IN_MINUTES = 60;
        
        /// <summary>
        /// Lifetime of cache element
        /// </summary>
        private TimeSpan timeOut;

        /// <summary>
        /// Internal list
        /// </summary>
        private Dictionary<TKey, TimedCacheValue<TValue>> cache;

        private System.Threading.Timer threadingTimer;

        private bool isChecking = false;

        /// <summary>
        /// Cache constructor
        /// </summary>
        /// <param name="timeOut"></param>
        public TimedCache(TimeSpan timeOut)
            : this(timeOut, DEFAULT_FREQUENCY_IN_MINUTES)
        { }

        /// <summary>
        /// Cache constructor
        /// </summary>
        /// <param name="timeOut"></param>
        public TimedCache(TimeSpan timeOut, int frequencyInMinutes)
        {
            this.timeOut = timeOut;
            this.cache = new Dictionary<TKey, TimedCacheValue<TValue>>();
            this.CreateExpiredCheckTask(frequencyInMinutes);
        }


        public TimedCache(IDictionary<string, string> configuration)
        {
            this.cache = new Dictionary<TKey, TimedCacheValue<TValue>>();

            // validity time
            string validityTimeInMinutesString = "validityTimeInMinutes";
            string validityTimeInHoursString = "validityTimeInHours";

            int validityInMinutes;
            if (configuration.ContainsKey(validityTimeInMinutesString))
            {
                if (int.TryParse(configuration[validityTimeInMinutesString], out validityInMinutes))
                {
                    // all okay
                }
                else
                {
                    // could not be parsed, valie is invalid
                    throw new Exception("Failed to create the TimeCache, because the configuration is invalid. Could not parse the validityInMinutes value '" + configuration[validityTimeInMinutesString] + "' to an int.");
                }
            }
            else if (configuration.ContainsKey(validityTimeInHoursString))
            {
                int hours;
                if (int.TryParse(configuration[validityTimeInMinutesString], out hours))
                {
                    // all okay
                    validityInMinutes = hours * 60;
                }
                else
                {
                    // could not be parsed, valie is invalid
                    throw new Exception("Failed to create the TimeCache, because the configuration is invalid. Could not parse the validityInMinutes value '" + configuration[validityTimeInHoursString] + "' to an int.");
                }
            }
            else
            {
                // dafault value
                Logger.Write("The validityInMinutes/validityInHours was now defined for the TimeCache, default to '" + DEFAULT_FREQUENCY_IN_MINUTES + "' minutes.", LoggerCategories.General);
                validityInMinutes = DEFAULT_VALIDITY_IN_MINUTES;
            }

            this.timeOut = new TimeSpan(validityInMinutes * TimeSpan.TicksPerHour);

            // Frequency in checks
            string frequencyInMinutesString = "frequencyInMinutes";
            string frequencyInHoursString = "frequencyInHours";
            int frequencyInMinutes;

            if (configuration.ContainsKey(frequencyInMinutesString))
            {
                if (int.TryParse(configuration[frequencyInMinutesString], out frequencyInMinutes))
                {
                    // all okay
                }
                else
                {
                    // could not be parsed, valie is invalid
                    throw new Exception("Failed to create the TimeCache, because the configuration is invalid. Could not parse the frequencyInMinutes value '" + configuration[frequencyInMinutesString] + "' to an int.");
                }
            }
            else if (configuration.ContainsKey(frequencyInHoursString))
            {
                int hours;
                if (int.TryParse(configuration[frequencyInHoursString], out hours))
                {
                    // all okay
                    frequencyInMinutes = hours * 60;
                }
                else
                {
                    // could not be parsed, valie is invalid
                    throw new Exception("Failed to create the TimeCache, because the configuration is invalid. Could not parse the frequencyInHours value '" + configuration[frequencyInHoursString] + "' to an int.");
                }
            }
            else
            {
                // dafault value
                Logger.Write("The frequencyInMinutes/frequencyInHours was now defined for the TimeCache, default to '" + DEFAULT_FREQUENCY_IN_MINUTES + "' minutes.", LoggerCategories.General);
                frequencyInMinutes = DEFAULT_FREQUENCY_IN_MINUTES;
            }

            this.CreateExpiredCheckTask(frequencyInMinutes);
        }

        private void CreateExpiredCheckTask(int frequencyInMinutes)
        {
            // The frequence must be greater then one minutes
            if (frequencyInMinutes < 1)
            {
                throw new Exception("The frequency cache check '" + frequencyInMinutes + "' must be greater then one minute.");
            }

            // start a new scheduled task, starting now
            TimerCallback tc = new TimerCallback(this.CheckExpired_TimerCallback);
            TimeSpan start = new TimeSpan(0, 0, 0);
            TimeSpan frequency = new TimeSpan(0, frequencyInMinutes, 0);
            threadingTimer = new System.Threading.Timer(tc, null, start, frequency);

        }

        private void CheckExpired_TimerCallback(object value)
        {
            this.CheckExpired();
        }

        /// <summary>
        /// Add to chache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value) {
            DateTime validUntilDateTime = DateTime.UtcNow.Add(timeOut);
            TimedCacheValue<TValue> cacheValue = new TimedCacheValue<TValue>(validUntilDateTime, value);
            lock (cache) {
                this.cache.Add(key, cacheValue);
            }
        }

        /// <summary>
        /// Try adds the key-value pair to the cache.
        /// If the key/value does not exist, it is added, and returned
        /// If the key/value does exist, the already existing value is returned
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value to cache</param>
        public TValue TryAddValue(TKey key, TValue value)
        {
            TValue result;
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            else
            {
                lock (cache)
                {
                    TimedCacheValue<TValue> cacheValue = null;
                    if (cache.TryGetValue(key, out cacheValue))
                    {
                        // check if the cached value has expired
                        DateTime now = DateTime.UtcNow;
                        if (cacheValue.TimeOut < now)
                        {
                            // cached info expired
                            // so adding the new value instead
                            this.cache.Remove(key);
                            DateTime validUntilDateTime = DateTime.UtcNow.Add(timeOut);
                            cacheValue = new TimedCacheValue<TValue>(validUntilDateTime, value);
                            this.cache.Add(key, cacheValue);
                            result = value;
                        }
                        else
                        {
                            // cached value valid
                            result = cacheValue.Value;
                        }
                    }
                    else
                    {
                        // value not cached
                        // adding the information
                        DateTime validUntilDateTime = DateTime.UtcNow.Add(timeOut);
                        cacheValue = new TimedCacheValue<TValue>(validUntilDateTime, value);
                        this.cache.Add(key, cacheValue);
                        result = value;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Sets a cache key/value pair
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(TKey key, TValue value)
        {
            DateTime validUntilDateTime = DateTime.UtcNow.Add(timeOut);
            TimedCacheValue<TValue> cacheValue = new TimedCacheValue<TValue>(validUntilDateTime, value);
            lock (cache)
            {
                cache.Remove(key);
                cache.Add(key, cacheValue);
            }
        }

        /// <summary>
        /// Get from cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value) {
            bool result;
            lock (cache)
            {
                TimedCacheValue<TValue> cacheValue = null;
                if (cache.TryGetValue(key, out cacheValue))
                {
                    // check if the cached value has expired
                    DateTime now = DateTime.UtcNow;
                    if (cacheValue.TimeOut < now)
                    {
                        // cached info expired
                        this.cache.Remove(key);
                        result = false;
                        value = default(TValue);
                    }
                    else
                    {
                        // cached value valid
                        result = true;
                        value = cacheValue.Value;
                    }
                }
                else
                {
                    // value not cached
                    result = false;
                    value = default(TValue);
                }                    
            }

            return result;
        }

       /* /// <summary>
        /// Does the cache contain an element with the key Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key) {
            lock (cache) {
               return cache.ContainsKey(key);
            }
        }*/

        /// <summary>
        /// Romve from cache
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key) {
            lock (cache) {
                cache.Remove(key);
            }
        }

        public void CheckExpired()
        {
            Logger.Write("checkExpired", LoggerCategories.Debug);
            if(isChecking == false)
            {
                // The Expired check is not running
                lock (this.cache)
                {
                    // Now we are running
                    isChecking = true;
                    try
                    {
                        int count = 0;
                        IEnumerator<KeyValuePair<TKey, TimedCacheValue<TValue>>> iterator = this.cache.GetEnumerator();
                        IList<TKey> toBeRemoved = new List<TKey>();
                        DateTime now = DateTime.UtcNow;
                        bool checkAgain = true;
                        KeyValuePair<TKey, TimedCacheValue<TValue>> pair;

                        while (iterator.MoveNext() && checkAgain)
                        {
                            pair = iterator.Current;
                            if (pair.Value.TimeOut < now)
                            {
                                count++;
                                toBeRemoved.Add(pair.Key);
                            }
                            else
                            {
                                /*
                                 * There is no sence to iterate around the whole cache, because
                                 * elements are sorted in the order they were added, and timeOut
                                 * for each of them is increasing.
                                 */
                                checkAgain = false;
                            }
                        }

                        // Remove the element that has been collected to be removed
                        foreach (TKey key in toBeRemoved)
                        {
                            this.cache.Remove(key);
                        }                       
                    }
                    finally
                    {
                        // Finish the check
                        this.isChecking = false;
                    }
                }
            }
            else
            {
                // The checking is already running
                // nothing to do
            }
        }

        /*
        private void Expire() {
            DateTime now = DateTime.UtcNow;
            List<TKey> expiredList = new List<TKey>();
            foreach (KeyValuePair<TKey, TimedCacheValue<TValue>> pair in cache)
            {
                if (pair.Value.TimeOut < now)
                    expiredList.Add(pair.Key);
            }
            foreach (TKey key in expiredList) {
                cache.Remove(key);
            }
        }*/
    }
}
