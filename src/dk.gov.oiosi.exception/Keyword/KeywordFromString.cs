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
    /// Generates a keyword from a string.
    /// </summary>
    public class KeywordFromString {

        /// <summary>
        /// Returns a dictionary with the keyword key/value pair
        /// </summary>
        /// <param name="key">The keyword key</param>
        /// <param name="value">The keyword value</param>
        /// <returns>A dictionary with the keyword key/value</returns>
        public static Dictionary<string, string> GetKeyword(string key, string value) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeyword(keywords, key, value);
            return keywords;
        }

        /// <summary>
        /// Adds a keyword key/value pair to the supplied keyword dictionary
        /// </summary>
        /// <param name="keywords">The keyword collection</param>
        /// <param name="key">The keyword key to add</param>
        /// <param name="value">The keyword value to add</param>
        public static void GetKeyword(Dictionary<string, string> keywords, string key, string value) {
            keywords.Add(key, value);
        }
    }
}
