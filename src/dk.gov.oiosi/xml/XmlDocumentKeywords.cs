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
using System.Xml;
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.xml {
    /// <summary>
    /// Represents standard ways to get keywords from an XmlDocument.
    /// </summary>
    public class XmlDocumentKeywords {

        /// <summary>
        /// Gets the xml document keywords and puts them in a new dictionary.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeywords(XmlDocument document) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add("documentname", document.DocumentElement.Name);
            keywords.Add("documentnamespace", document.DocumentElement.NamespaceURI);
            return keywords;
        }

        /// <summary>
        /// Gets the xml document keywords and puts them into the dictionary.
        /// </summary>
        /// <param name="keywords">xml document keywords</param>
        /// <param name="document">the document</param>
        public static void GetKeywords(Dictionary<string, string> keywords, XmlDocument document) {
            keywords.Add("documentname", document.DocumentElement.Name);
            keywords.Add("documentnamespace", document.DocumentElement.NamespaceURI);
        }

        /// <summary>
        /// Returns the keyword dictionary representing the parameters
        /// </summary>
        /// <param name="rootName">The root name</param>
        /// <param name="rootNamespace">The root namespace</param>
        /// <returns>Returns the keyword dictionary representing the parameters</returns>
        public static Dictionary<string, string> GetKeywords(string rootName, string rootNamespace) {
            Dictionary<string, string> keywords = KeywordFromString.GetKeyword("documentname", rootName);
            KeywordFromString.GetKeyword(keywords, "documentnamespace", rootNamespace);
            return keywords;
        }
    }
}