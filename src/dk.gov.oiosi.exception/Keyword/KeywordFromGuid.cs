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
  *
  */
using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.exception.Keyword {

    /// <summary>
    /// Associates a dictionary of keywords with a guid
    /// </summary>
    public class KeywordFromGuid {
        
        /// <summary>
        /// Creates an dictionary with the given guid.
        /// </summary>
        /// <param name="guid">The guid to get the keyword dictionary from</param>
        /// <returns>A keyword dictionary</returns>
        public static Dictionary<string, string> GetKeyword(Guid guid) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeyword(keywords, guid);
            return keywords;
        }

        /// <summary>
        /// Creates an dictionary with the given guid.
        /// </summary>
        /// <param name="id">The guid to get the keyword dictionary from</param>
        /// <returns>A keyword dictionary</returns>
        public static Dictionary<string, string> GetKeyword(string id)
        {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeyword(keywords, id);
            return keywords;
        }

        /// <summary>
        /// Adds the guid keyword to the given dictionary.
        /// </summary>
        /// <param name="keywords">The keyword dictionary</param>
        /// <param name="guid">The guid to add</param>
        public static void GetKeyword(Dictionary<string, string> keywords, Guid guid)
        {
            keywords.Add("guid", guid.ToString());
        }

        /// <summary>
        /// Adds the guid keyword to the given dictionary.
        /// </summary>
        /// <param name="keywords">The keyword dictionary</param>
        /// <param name="id">The guid to add</param>
        public static void GetKeyword(Dictionary<string, string> keywords, string id)
        {
            keywords.Add("guid", id);
        }
    }
}
