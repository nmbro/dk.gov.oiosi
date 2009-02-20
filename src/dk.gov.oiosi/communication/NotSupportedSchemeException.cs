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

namespace dk.gov.oiosi.communication
{
    /// <summary>
    /// Custom exception used when message uses a non-supported scheme
    /// </summary>
    public class NotSupportedSchemeException : OiosiCommunicationException {
        
        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="scheme">the used scheme</param>
        public NotSupportedSchemeException(string scheme) : base(GetKeywords(scheme)) { }
        
        /// <summary>
        /// Constructor with scheme and innerexception
        /// </summary>
        /// <param name="scheme">the used scheme</param>
        /// <param name="innerException">the innnerexception of the thrown exception</param>
        public NotSupportedSchemeException(string scheme, System.Exception innerException) : base(GetKeywords(scheme), innerException) { }

        private static Dictionary<string, string> GetKeywords(string scheme) {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("scheme", scheme);
            return d;
        }
    }
}