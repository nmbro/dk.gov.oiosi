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

namespace dk.gov.oiosi.common.cache {
    /// <summary>
    /// Interface that represents a simple cache
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ICache<TKey, TValue> 
    {
        /// <summary>
        /// Adds the key-value pair to the cache.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value to cache</param>
        void Add(TKey key, TValue value);

        /// <summary>
        /// Try adds the key-value pair to the cache.
        /// If the key/value does not exist, it is added, and returned
        /// If the key/value does exist, the already existing value is returned
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value to cache</param>
        TValue TryAddValue(TKey key, TValue value);

        /// <summary>
        /// Sets the value for the given key in the cache. Overwrites already present values.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value to cache</param>
        void Set(TKey key, TValue value);

        /*
         * Do not use this method.
         * The element can be remove beween this check, and then when the element is retrived
         * 
         * /// <summary>
        /// Lookup whether a given key is represented in the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsKey(TKey key);*/
        
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

        /// <summary>
        /// Check the validity of the data in the cache
        /// </summary>
        void CheckExpired();
    }
}
