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
    /// Gets a keyword dictionary matching a specific type
    /// </summary>
    public class KeywordFromType {

        /// <summary>
        /// Gets the keyword dictionary from a type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>Returns the keyword dictionary from a type</returns>
        public static Dictionary<string, string> GetKeyword(Type type) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeyword(keywords, type);
            return keywords;
        }

        /// <summary>
        /// Associates a set of keywords with a given type
        /// </summary>
        /// <param name="keywords">A dictionary of keywords</param>
        /// <param name="type">The type to associate with the keywords</param>
        public static void GetKeyword(Dictionary<string, string> keywords, Type type) {
            keywords.Add("type", type.ToString());
        }
    }
}
