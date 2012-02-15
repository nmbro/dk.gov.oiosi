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
  */

using System;
using System.Collections.Generic;

namespace dk.gov.oiosi.common.cache {
    /// <summary>
    /// A cache implementation that stores a defined number of results in
    /// memory
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class QuantityCache<TKey, TValue> : ICache<TKey, TValue>
    {
        private List<TKey> _indexSortedKeys;
        private Dictionary<TKey, TValue> _keyedDictionary;
        private int _maxSize;
        private static object _lockObject = new object();
        private string name = string.Empty;

        /// <summary>
        /// Constructor that takes the max size of the cache
        /// </summary>
        /// <param name="maxSize"></param>
        public QuantityCache(int maxSize) {
            if (maxSize < 0) throw new ArgumentOutOfRangeException("maxSize");
            this.Setup(maxSize);
        }


        public QuantityCache(IDictionary<string, string> configuration)
        {
            int maxSize;
            if (configuration.ContainsKey("maxSize"))
            {
                string value = configuration["maxSize"];
                if (int.TryParse(value, out maxSize))
                {
                    // parsed okay
                    if (maxSize < 0)
                    {
                        throw new Exception("MaxSize must not be less then zero.");
                    }
                }
                else
                {
                    throw new Exception("Argument maxSize could not be parsed to an integer.");
                }                
            }
            else
            {
                throw new Exception("Argument maxSize was not present in the configuration file.");
            }

            if (configuration.ContainsKey("CacheName"))
            {
                this.name = configuration["CacheName"];
            }

            this.Setup(maxSize);
        }

        private void Setup(int maxSize)
        {
            this._maxSize = maxSize;
            this._indexSortedKeys = new List<TKey>(maxSize);
            this._keyedDictionary = new Dictionary<TKey, TValue>(maxSize);
        }


        #region ICache<TKey,TValue> Members

        /// <summary>
        /// Adds item to cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (_maxSize == 0)
            {
                // Cache can not contain any element
                // so we do nothing
            }
            else
            {
                lock (_lockObject)
                {
                    this.CheckExpired();
                    if (this._keyedDictionary.ContainsKey(key))
                    {
                        // key does already exist
                        // so it is not added again
                        throw new ArgumentNullException("key");
                    }
                    else 
                    {
                        this._indexSortedKeys.Add(key);
                        this._keyedDictionary.Add(key, value);
                    }
                }
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

            if (_maxSize == 0)
            {
                // Cache can not contain any element
                result = value;
            }
            else
            {
                lock (_lockObject)
                {
                    this.CheckExpired();

                    if (this._keyedDictionary.TryGetValue(key, out result))
                    {
                        // The key/value does exist
                        // Existing date already retrived
                    }
                    else
                    {
                        // The key/value did not exist - updating the cache
                        this._indexSortedKeys.Add(key);
                        this._keyedDictionary.Add(key, value);
                        result = value;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Sets item in cachem by removing the old value and adding the new value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (_maxSize == 0) return;
            lock (_lockObject)
            {
                CheckExpired();
                _indexSortedKeys.Add(key);
                _keyedDictionary.Remove(key);
                _keyedDictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Returns true if the key is found in the cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key) 
        {
            bool result = false;    
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            else if (_maxSize == 0)
            {
                // max key size is 0/Zero
                // cache does not contain the key
            }
            else
            {
                lock (_lockObject)
                {
                    if (!_keyedDictionary.ContainsKey(key))
                    {
                        // The key exist
                        UpdateIndex(key);
                        result = true;
                    }
                    else
                    { 
                        // the key does not exist
                    }                    
                }
            }

            return result;
        }

        /// <summary>
        /// Removes item from cache
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key) 
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            else if (_maxSize == 0)
            {
                // current max size is 0 (zero)
                // nothing to do
            }
            else
            {
                lock (_lockObject)
                {
                    if (this._keyedDictionary.ContainsKey(key))
                    {
                        // Key exist, so it is removed
                        _indexSortedKeys.Remove(key);
                        _keyedDictionary.Remove(key);
                    }
                    else
                    {
                        // The key does not exist, so it can not be removed
                    }
                }
            }
        }

        /// <summary>
        /// Tries to get item from cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value) 
        {
            bool result = true;
            // set the default value to the 'out value'
            value = default(TValue);

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            else if (_maxSize == 0)
            {
                // current max size is 0 (zero)
                result = false;
            }
            else
            {
                lock (_lockObject)
                {
                    if (!_keyedDictionary.TryGetValue(key, out value))
                    {
                        result = false;
                    }
                    else
                    {
                        this.UpdateIndex(key);
                    }
                }
            }

            return result;
        }

        public void CheckExpired()
        {
            if (_keyedDictionary.Count == 0)
            {
                // current size is 0 (zero)
                // nothing to do
            }
            else if (_keyedDictionary.Count < _maxSize)
            {
                // the used size of the cache is less then the alowed size
                // so nothing to do
            }
            else
            {
                // We remove the last inserted element
                TKey key = _indexSortedKeys[0];
                _keyedDictionary.Remove(key);
                _indexSortedKeys.RemoveAt(0);
            }
        }

        #endregion

        /// <summary>
        /// Update the index, so the key is found faster next time
        /// </summary>
        /// <param name="key">The index key to update</param>
        private void UpdateIndex(TKey key)
        {
            _indexSortedKeys.Remove(key);
            _indexSortedKeys.Add(key);
        }
    }
}
