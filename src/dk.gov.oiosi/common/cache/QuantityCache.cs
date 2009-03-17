using System;
using System.Collections.Generic;

namespace dk.gov.oiosi.common.cache {
    /// <summary>
    /// A cache implementation that stores a defined number of results in
    /// memory
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class QuantityCache<TKey, TValue> : ICache<TKey, TValue> {
        private List<TKey> _indexSortedKeys;
        private Dictionary<TKey, TValue> _keyedDictionary;
        private int _maxSize;
        private static object _lockObject = new object();

        /// <summary>
        /// Constructor that takes the max size of the cache
        /// </summary>
        /// <param name="maxSize"></param>
        public QuantityCache(int maxSize) {
            if (maxSize < 0) throw new ArgumentOutOfRangeException("maxSize");
            _maxSize = maxSize;
            _indexSortedKeys = new List<TKey>(maxSize);
            _keyedDictionary = new Dictionary<TKey, TValue>(maxSize);
        }

        #region ICache<TKey,TValue> Members

        /// <summary>
        /// Adds item to cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value) {
            if (key == null) throw new ArgumentNullException("key");
            if (_maxSize == 0) return;
            lock (_lockObject) {
                Expire();
                _indexSortedKeys.Add(key);
                _keyedDictionary.Add(key, value);
            }
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
                Expire();
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
        public bool ContainsKey(TKey key) {
            if (key == null) throw new ArgumentNullException("key");
            if (_maxSize == 0) return false;
            lock (_lockObject) {
                if (!_keyedDictionary.ContainsKey(key)) return false;
                UpdateIndex(key);
                return true;
            }
        }

        /// <summary>
        /// Removes item from cache
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key) {
            if (key == null) throw new ArgumentNullException("key");
            if (_maxSize == 0) return;
            lock (_lockObject) {
                if (!_keyedDictionary.ContainsKey(key)) return;
                _indexSortedKeys.Remove(key);
                _keyedDictionary.Remove(key);
            }
        }

        /// <summary>
        /// Tries to get item from cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value) {
            if (key == null) throw new ArgumentNullException("key");
            value = default(TValue);
            if (_maxSize == 0) return false;
            lock (_lockObject) {
                if (!_keyedDictionary.TryGetValue(key, out value)) return false;
                UpdateIndex(key);
                return true;
            }
        }

        #endregion

        private void UpdateIndex(TKey key) {
            _indexSortedKeys.Remove(key);
            _indexSortedKeys.Add(key);
        }

        private void Expire() {
            if (_keyedDictionary.Count == 0) return;
            if (_keyedDictionary.Count < _maxSize) return;
            TKey key = _indexSortedKeys[0];
            _keyedDictionary.Remove(key);
            _indexSortedKeys.RemoveAt(0);
        }
    }
}
