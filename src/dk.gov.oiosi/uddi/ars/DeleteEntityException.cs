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
  *   Dennis S�gaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.uddi.ars;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Thrown when an exception occurs when deleting a UDDI entity
    /// </summary>
    public class DeleteEntityException : ArsException {
        
        /// <summary>
        /// Constructor with the timeout as keyword
        /// </summary>
        /// <param name="entityguid">The UDDI key of the entity</param>
        public DeleteEntityException(string entityguid) : base(GetKeywords(entityguid)) { }

        private static Dictionary<string, string> GetKeywords(string entityguid)
        {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add("entityguid", entityguid);
            return keywords;
        }
    }
}