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
using System.Text;
using System.Resources;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.extension.wcf.EmailTransport
{
    /// <summary>
    /// Thrown when the mailbox implementation given in a RaspEmailBindingElement cannot be instantiated.
    /// </summary>
    public class RaspMailboxImplementationCouldNotBeInstantiated : RaspEmailTransportException {
        
        /// <summary>
        /// Constructor with implementation type as keyword
        /// </summary>
        /// <param name="implementationType">implementation type</param>
        public RaspMailboxImplementationCouldNotBeInstantiated(string implementationType) : base(GetKeywords(implementationType)) { }
        
        /// <summary>
        /// Constructor with implementation type as keyword and innerexception
        /// </summary>
        /// <param name="implementationType">mailbox implementation type</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public RaspMailboxImplementationCouldNotBeInstantiated(string implementationType, System.Exception innerException) : base(GetKeywords(implementationType), innerException) { }

        private static Dictionary<string, string> GetKeywords(string implementationType) {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("type", implementationType);
            return d;
        }
    }
}