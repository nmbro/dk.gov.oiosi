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

namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// Exception thrown when a given mail address does not comply with the "SMTP/MIME Base 64 Transport Binding for SOAP 1.2" protocol
    /// </summary>
    public class MailBindingAddressNotCompliantException : MessageNotCompliantWithMailBindingException {
        
        /// <summary>
        /// Constructor with uri as keyword
        /// </summary>
        /// <param name="uri">Uri as keyword</param>
        public MailBindingAddressNotCompliantException(string uri) : base(GetKeywords(uri)) { }        
        
        /// <summary>
        /// Constructor with keyword and innerexception
        /// </summary>
        /// <param name="uri">Uri as keyword</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public MailBindingAddressNotCompliantException(string uri, System.Exception innerException) : base(GetKeywords(uri), innerException) { }
        
        private static Dictionary<string, string> GetKeywords(string fieldName)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("uri", fieldName);
            return d;
        }
    }
}