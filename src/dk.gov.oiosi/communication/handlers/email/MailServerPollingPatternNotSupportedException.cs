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

namespace dk.gov.oiosi.communication.handlers.email
{

    /// <summary>
    /// Custom exception used the polling pattern is not supported
    /// </summary>
    public class MailServerPollingPatternNotSupportedException : MailHandlerException {
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pattern">the polling pattern</param>
        /// <param name="implementation">implementation</param>
        public MailServerPollingPatternNotSupportedException(MailServerPollingPattern pattern, string implementation) : base(GetKeywords(pattern,implementation)) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pattern">the polling pattern</param>
        /// <param name="implementation">implementation</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public MailServerPollingPatternNotSupportedException(MailServerPollingPattern pattern, string implementation, System.Exception innerException) : base(GetKeywords(pattern,implementation),innerException) { }

        private static Dictionary<string, string> GetKeywords(MailServerPollingPattern pattern, string implementation) {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("pattern", pattern.ToString());
            d.Add("implementation", implementation);
            return d;
        }
        // pattern, implementation
    }
}