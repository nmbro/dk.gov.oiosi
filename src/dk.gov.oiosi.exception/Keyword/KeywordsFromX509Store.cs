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
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace dk.gov.oiosi.exception.Keyword {

    /// <summary>
    /// Holds keywords from an x509 store
    /// </summary>
    public class KeywordsFromX509Store {

        /// <summary>
        /// Gets a dictionary of keywords
        /// </summary>
        /// <param name="store">The store to search</param>
        /// <returns>Gets a dictionary of keywords</returns>
        public static Dictionary<string, string> GetKeywords(X509Store store) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeywords(keywords, store);
            return keywords;
        }

        /// <summary>
        /// Gets the keywords from the store
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="store"></param>
        public static void GetKeywords(Dictionary<string, string> keywords, X509Store store) {
            keywords.Add("x509storelocation", store.Location.ToString());
            keywords.Add("x509storename", store.Name.ToString());
        }
    }
}
