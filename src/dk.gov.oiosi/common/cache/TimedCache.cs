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
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;

namespace dk.gov.oiosi.common.cache {
    /// <summary>
    /// Simple, generic timed cache type. The cache is traversed each time an element is inserted or 
    /// extracted.
    /// </summary>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public class TimedCache<TKey, TValue> : ITimedCache<TKey,TValue> {
        /// <summary>
        /// Lifetime of cache element
        /// </summary>
        private TimeSpan _timeOut;

        /// <summary>
        /// Internal list
        /// </summary>
        private Dictionary<TKey, TimedCacheValue> _cache;

        /// <summary>
        /// Cache constructor
        /// </summary>
        /// <param name="timeOut"></param>
        public TimedCache(TimeSpan timeOut) {
            _timeOut = timeOut;
            _cache = new Dictionary<TKey, TimedCacheValue>();
        }

        /// <summary>
        /// Add to chache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value) {
            DateTime timeOut = DateTime.UtcNow.Add(_timeOut);
            TimedCacheValue cacheValue = new TimedCacheValue(timeOut, value);
            lock (_cache) {
                Expire();
                _cache.Add(key, cacheValue);
            }
        }

        /// <summary>
        /// Sets a cache key/value pair
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(TKey key, TValue value)
        {
            DateTime timeOut = DateTime.UtcNow.Add(_timeOut);
            TimedCacheValue cacheValue = new TimedCacheValue(timeOut, value);
            lock (_cache) {
                Expire();
                _cache.Remove(key);
                _cache.Add(key, cacheValue);
            }
        }

        /// <summary>
        /// Get from cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value) {
            lock (_cache) {
                Expire();
                TimedCacheValue cacheValue = null;
                value = default(TValue);
                if (!_cache.TryGetValue(key, out cacheValue)) return false;
                value = cacheValue.Value;
                return true;
            }
        }

        /// <summary>
        /// Does the cache contain an element with the key Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key) {
            lock (_cache) {
                Expire();
                return _cache.ContainsKey(key);
            }
        }

        /// <summary>
        /// Romve from cache
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key) {
            lock (_cache) {
                Expire();
                _cache.Remove(key);
            }
        }

        private void Expire() {
            DateTime now = DateTime.UtcNow;
            List<TKey> expiredList = new List<TKey>();
            foreach (KeyValuePair<TKey, TimedCacheValue> pair in _cache) {
                if (pair.Value.TimeOut < now)
                    expiredList.Add(pair.Key);
            }
            foreach (TKey key in expiredList) {
                _cache.Remove(key);
            }
        }

        class TimedCacheValue {
            private DateTime _timeOut;
            private TValue _value;

            public TimedCacheValue(DateTime timeOut, TValue value) {
                _timeOut = timeOut;
                _value = value;
            }

            public DateTime TimeOut {
                get { return _timeOut; }
            }

            public TValue Value {
                get { return _value; }
            }
        }
    }
}
