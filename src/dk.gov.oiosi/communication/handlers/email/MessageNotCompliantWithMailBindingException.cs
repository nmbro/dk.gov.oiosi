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

namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// Exception thrown when an WCF Message does not contain the necessary information to create a mail that is compliant with the "Smtp/MIME Base64 Transport Binding for SOAP 1.2" protocol
    /// </summary>
    public class MessageNotCompliantWithMailBindingException : MailHandlerException {
        
        /// <summary>
        /// Base constructor
        /// </summary>
        public MessageNotCompliantWithMailBindingException() : base() { }
        
        /// <summary>
        /// Constructor with keyword
        /// </summary>
        /// <param name="keywords">keyword for the message</param>
        public MessageNotCompliantWithMailBindingException(System.Collections.Generic.Dictionary<string, string> keywords) : base(keywords) { }
        
        /// <summary>
        /// Constructor with innerexception
        /// </summary>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public MessageNotCompliantWithMailBindingException(System.Exception innerException) : base(innerException) { }
        
        /// <summary>
        /// Constructor with keywords and innerexception
        /// </summary>
        /// <param name="keywords">keywords for the message</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public MessageNotCompliantWithMailBindingException(System.Collections.Generic.Dictionary<string, string> keywords, System.Exception innerException) : base(keywords, innerException) { }
    }
}