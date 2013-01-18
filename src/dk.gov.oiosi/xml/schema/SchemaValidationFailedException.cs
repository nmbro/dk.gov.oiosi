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
  *   Jacob Mogensen, mySupply ApS
  *
  */
using System;
using System.Xml;
using System.Collections.Generic;

namespace dk.gov.oiosi.xml.schema
{
    /// <summary>
    /// Custom exception used then schemavalidation fails
    /// </summary>
    public class SchemaValidationFailedException : SchemaValidationException
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="document">the schema</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public SchemaValidationFailedException(XmlDocument document, Exception innerException)
            : base(XmlDocumentKeywords.GetKeywords(document), innerException)
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keywords">keywords for the message</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public SchemaValidationFailedException(Dictionary<string, string> keywords, Exception innerException)
            : base(keywords, innerException)
        { }
    }
}