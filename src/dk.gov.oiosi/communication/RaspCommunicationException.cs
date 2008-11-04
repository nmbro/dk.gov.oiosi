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

namespace dk.gov.oiosi.communication {
    
    /// <summary>
    /// A custom exception representing raspcommunication
    /// </summary>
    public class RaspCommunicationException : dk.gov.oiosi.exception.MainException {

        private static ResourceManager resourceManager = new ResourceManager(typeof(ErrorMessages));

        /// <summary>
        /// Constructor
        /// </summary>
        public RaspCommunicationException() : base(resourceManager) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keywords">the keyword for the message</param>
        public RaspCommunicationException(System.Collections.Generic.Dictionary<string, string> keywords) : base(resourceManager, keywords) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="innerException">the innerexception of the thrown message</param>
        public RaspCommunicationException(System.Exception innerException) : base(resourceManager, innerException) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keywords">keyword for the message</param>
        /// <param name="innerException">the innerexception of the thrown message</param>
        public RaspCommunicationException(System.Collections.Generic.Dictionary<string, string> keywords, System.Exception innerException) : base(resourceManager, keywords, innerException) { }

        /// <summary>
        /// Constructor that takes a resource manager and a dictionary of keywords.
        /// The resource manager is used for errormessage lookup instead of the standard.
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <param name="keywords"></param>
        public RaspCommunicationException(ResourceManager errorMessages, System.Collections.Generic.Dictionary<string, string> keywords) : base(errorMessages, keywords) { }
    }
}