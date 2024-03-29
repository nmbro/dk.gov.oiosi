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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *   Jacob Mogensen, mySupply ApS
  */

namespace dk.gov.oiosi.common.cache {
    /// <summary>
    /// Cache implementation that never stores anything. Used when the cache function should be turned off.
    /// </summary>
    /// <typeparam name="TKey">The type of the key</typeparam>
    /// <typeparam name="TValue">The type of the value</typeparam>
    public class ZeroCache<TKey, TValue> : ICache<TKey, TValue> {
        #region ITimedCache<TKey,TValue> Members

        /// <summary>
        /// Does not add anything. This is a stub.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value) { }

        /// <summary>
        /// Does not add anything. This is a stub.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public TValue TryAddValue(TKey key, TValue value) 
        {
            return value;
        }

        /// <summary>
        /// Does not set anything. This is a stub.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(TKey key, TValue value) { }

        /// <summary>
        /// Returns false.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key) {
            return false;
        }

        /// <summary>
        /// Does not do anything. This is just a stub.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key) { }

        /// <summary>
        /// Returns false allways.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value) {
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Do nothing, no data cached
        /// </summary>
        public void CheckExpired()
        {
            // nothing to do - we do not cache anything
        }

        #endregion

    }
}
