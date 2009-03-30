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
using System.Xml;

namespace dk.gov.oiosi.exception.Keyword {

    /// <summary>
    /// Handles keywords in xml documents
    /// </summary>
    public class KeywordFromXmlDocument {

        /// <summary>
        /// Gets the keywords for the xml document
        /// </summary>
        /// <param name="xmlDocument">The xml document</param>
        /// <returns>Returns a keyword dictionary</returns>
        public static Dictionary<string, string> GetKeywords(XmlDocument xmlDocument) {
            Dictionary<string, string> keywords = new Dictionary<string,string>();
            GetKeywords(keywords, xmlDocument);
            return keywords;
        }

        /// <summary>
        /// Returns a dictionary of keywords
        /// </summary>
        /// <param name="keywords">Keyword</param>
        /// <param name="xmlDocument">The xml document</param>
        public static void GetKeywords(Dictionary<string, string> keywords, XmlDocument xmlDocument) {
            keywords.Add("rootname", xmlDocument.DocumentElement.Name);
            keywords.Add("rootnamespace", xmlDocument.DocumentElement.NamespaceURI);
        }
    }
}
