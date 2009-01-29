using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.common.cache {
    public class QuantityCache<TKey, TValue> : ICache<TKey, TValue> {
        private List<TKey> _indexSortedKeys;
        private Dictionary<TKey, TValue> _keyedDictionary;
        private int _maxSize;
        private static object _lockObject = new object();

        public QuantityCache(int maxSize) {
            if (maxSize < 0) throw new ArgumentOutOfRangeException("maxSize");
            _maxSize = maxSize;
            _indexSortedKeys = new List<TKey>(maxSize);
            _keyedDictionary = new Dictionary<TKey, TValue>(maxSize);
        }

        #region ICache<TKey,TValue> Members

        public void Add(TKey key, TValue value) {
            if (key == null) throw new ArgumentNullException("key");
            if (_maxSize == 0) return;
            lock (_lockObject) {
                Expire();
                _indexSortedKeys.Add(key);
                _keyedDictionary.Add(key, value);
            }
        }

        public bool ContainsKey(TKey key) {
            if (key == null) throw new ArgumentNullException("key");
            if (_maxSize == 0) return false;
            lock (_lockObject) {
                if (!_keyedDictionary.ContainsKey(key)) return false;
                UpdateIndex(key);
                return true;
            }
        }

        public void Remove(TKey key) {
            if (key == null) throw new ArgumentNullException("key");
            if (_maxSize == 0) return;
            lock (_lockObject) {
                if (!_keyedDictionary.ContainsKey(key)) return;
                _indexSortedKeys.Remove(key);
                _keyedDictionary.Remove(key);
            }
        }

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
