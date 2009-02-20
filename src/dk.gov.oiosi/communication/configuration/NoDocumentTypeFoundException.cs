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
using System.Resources;

namespace dk.gov.oiosi.communication.configuration {
    /// <summary>
    /// Custom exception used when a search for document types fails
    /// </summary>
    public class NoDocumentTypeFoundException : OiosiCommunicationException {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NoDocumentTypeFoundException() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keywords">Template keywords</param>
        public NoDocumentTypeFoundException(Dictionary<string, string> keywords) : base(keywords) { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keywords">The exception keywords</param>
        /// <param name="innerException">The inner exception</param>
        public NoDocumentTypeFoundException(Dictionary<string, string> keywords, Exception innerException) : base(keywords, innerException) { }

        /// <summary>
        /// Constructor that takes a resource manager with the error messages to be used and a 
        /// dictionary with the keywords.
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <param name="keywords"></param>
        public NoDocumentTypeFoundException(ResourceManager errorMessages, Dictionary<string, string> keywords) : base(errorMessages, keywords) { }
    }
}