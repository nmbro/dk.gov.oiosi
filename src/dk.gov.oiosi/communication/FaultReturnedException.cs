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
using System.ServiceModel;

namespace dk.gov.oiosi.communication {
    /// <summary>
    /// Thrown when a SOAP fault is received in response to a request
    /// </summary>
    public class FaultReturnedException : OiosiCommunicationException {
        private FaultException _fault;

        /// <summary>
        /// The SOAP fault that caused this exception to be thrown
        /// </summary>
        public FaultException Fault
        {
            get { return _fault; }
        }

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fault">The fault message</param>
        /// <param name="source">Sender/Receiver</param>
        public FaultReturnedException(FaultException fault, string source) : base(GetKeywords(fault.Reason.ToString(), source)) {
            _fault = fault;    
        }

        private static Dictionary<string, string> GetKeywords(string fault, string source) {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("fault", fault);
            d.Add("source", source);
            return d;
        }

    }
}
