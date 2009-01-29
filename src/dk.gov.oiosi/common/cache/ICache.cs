using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.common.cache {
    /// <summary>
    /// Interface that represents a simple cache
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ICache<TKey, TValue> {
        /// <summary>
        /// Adds the key-value pair to the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(TKey key, TValue value);
        /// <summary>
        /// Lookup whether a given key is represented in the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsKey(TKey key);
        /// <summary>
        /// Removes a given key-value pair from the cache, based on the key.
        /// </summary>
        /// <param name="key"></param>
        void Remove(TKey key);
        /// <summary>
        /// Try to get the value from a given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue(TKey key, out TValue value);
    }
}
