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

namespace dk.gov.oiosi.communication.listener
{
    /// <summary>
    /// Thrown when someone tries to open a Listener listening to more than one endpoint
    /// </summary>
    public class ListenerHasMoreThanOneEndpointException : RaspCommunicationException {
        
        /// <summary>
        /// Customm exception that throws the type as a keyword
        /// </summary>
        /// <param name="t">the type of service</param>
        public ListenerHasMoreThanOneEndpointException(Type t) : base(GetKeywords(t)){ }
        
        /// <summary>
        /// Customm exception that throws the type as a keyword and innerexception
        /// </summary>
        /// <param name="t">type of service</param>
        /// <param name="innerException">the innerexception</param>
        public ListenerHasMoreThanOneEndpointException(Type t, System.Exception innerException) : base(GetKeywords(t), innerException) { }

        private static Dictionary<string,string> GetKeywords(Type t){
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("type", t.ToString());
            return d;
        }
    }
}
