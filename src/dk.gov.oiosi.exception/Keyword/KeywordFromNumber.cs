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
using System.Text;

namespace dk.gov.oiosi.exception.Keyword {
    /// <summary>
    /// Generates a keyword from a number.
    /// </summary>
    public class KeywordFromNumber {

        /// <summary>
        /// Creates a dictionary from a string key/int value pair. Converts the integer to a string
        /// </summary>
        /// <param name="key">The key (string)</param>
        /// <param name="value">The value (integer)</param>
        /// <returns>Returns a dictionary with a single string key/value pair</returns>
        public static Dictionary<string, string> GetKeyword(string key, int value) {
            return KeywordFromString.GetKeyword(key, value.ToString());
        }

        /// <summary>
        /// Adds a key/uint pair to a dictionary. Converts the uint to a string
        /// </summary>
        /// <param name="keywords">The keyword collection to which the key/value pair will be added</param>
        /// <param name="key">The key (string)</param>
        /// <param name="value">The value (uint)</param>
        public static void GetKeyword(Dictionary<string, string> keywords, string key, int value) {
            KeywordFromString.GetKeyword(keywords, key, value.ToString());
        }

        ///// <summary>
        ///// Creates a dictionary from a string key/uint value pair. Converts the uint to a string
        ///// </summary>
        ///// <param name="key">The key (string)</param>
        ///// <param name="value">The value (uint)</param>
        ///// <returns>Returns a dictionary with a single string key/value pair</returns>
        //public static Dictionary<string, string> GetKeyword(string key, uint value) {
        //    return KeywordFromString.GetKeyword(key, value.ToString());
        //}

        ///// <summary>
        ///// Adds a key/uint pair to a dictionary. Converts the uint to a string
        ///// </summary>
        ///// <param name="keywords">The keyword collection to which the key/value pair will be added</param>
        ///// <param name="key">The key (string)</param>
        ///// <param name="value">The value (uint)</param>
        //public static void GetKeyword(Dictionary<string, string> keywords, string key, uint value) {
        //    KeywordFromString.GetKeyword(keywords, key, value.ToString());
        //}

    }
}
