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
  *
  */
using System.Collections.Generic;

namespace dk.gov.oiosi.xml.schema
{
    /// <summary>
    /// Custom exception used when an error occured because of a namespace
    /// </summary>
    public class UnexpectedNamespaceException : SchemaValidationException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentNamespace">namespace in the document</param>
        /// <param name="expectedNamespace">the expected namespace</param>
        public UnexpectedNamespaceException(string documentNamespace, string expectedNamespace) : base(GetKeywords(documentNamespace, expectedNamespace)) { }
        
        /// <summary>
        /// Gets keywords
        /// </summary>
        /// <param name="documentNamespace">namespace in the document</param>
        /// <param name="expectedNamespace">the expected namespace</param>
        /// <returns>keywords</returns>
        public static Dictionary<string, string> GetKeywords(string documentNamespace, string expectedNamespace)
        {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add("documentnamespace", documentNamespace);
            keywords.Add("expectednamespace", expectedNamespace);
            return keywords;
        }
    }
}