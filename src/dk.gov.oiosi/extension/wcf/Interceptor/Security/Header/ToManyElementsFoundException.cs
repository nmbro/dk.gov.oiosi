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

using System.Collections.Generic;
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.Header {
    /// <summary>
    /// Exception thrown when a search for an element returns to many results.
    /// </summary>
    public class ToManyElementsFoundException : InterceptorException {
        /// <summary>
        /// Constructor that takes the tag name and the number of resulst.
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="results"></param>
        public ToManyElementsFoundException(string tagName, int results) : base(GetKeywords(tagName, results)) { }

        private static Dictionary<string, string> GetKeywords(string tagName, int results) {
            Dictionary<string, string> keywords = KeywordFromString.GetKeyword("tagname", tagName);
            KeywordFromNumber.GetKeyword(keywords, "results", results);
            return keywords;
        }
    }
}
