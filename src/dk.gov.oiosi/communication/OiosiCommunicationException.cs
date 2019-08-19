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
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System.Collections.Generic;
using System.Resources;

namespace dk.gov.oiosi.communication {
    
    /// <summary>
    /// A general communication exception
    /// </summary>
    public class OiosiCommunicationException : dk.gov.oiosi.exception.MainException
    {
        private static ICollection<ResourceManager> resourceManagerCollection = new List<ResourceManager>()
            {
                //// Project - dk.gov.oiosi
                new ResourceManager(typeof(dk.gov.oiosi.addressing.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.common.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.communication.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.communication.fault.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.communication.handlers.email.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.extension.wcf.EmailTransport.ErrorMessages)),                
                new ResourceManager(typeof(dk.gov.oiosi.extension.wcf.Interceptor.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.security.ldap.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.security.lookup.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.security.validation.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.security.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.uddi.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.xml.schema.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.xml.schematron.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.xml.ErrorMessages)),
                new ResourceManager(typeof(dk.gov.oiosi.ErrorMessages)),
                
                //// new ResourceManager(typeof(.ErrorMessages))   
            };


        /// <summary>
        /// Constructor
        /// </summary>
        public OiosiCommunicationException() : base(resourceManagerCollection) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keywords">the keyword for the message</param>
        public OiosiCommunicationException(System.Collections.Generic.Dictionary<string, string> keywords) : base(resourceManagerCollection, keywords) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="innerException">the innerException of the thrown message</param>
        public OiosiCommunicationException(System.Exception innerException) : base(resourceManagerCollection, innerException) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keywords">keyword for the message</param>
        /// <param name="innerException">the innerException of the thrown message</param>
        public OiosiCommunicationException(System.Collections.Generic.Dictionary<string, string> keywords, System.Exception innerException) : base(resourceManagerCollection, keywords, innerException) { }

        /// <summary>
        /// Constructor that takes a resource manager and a dictionary of keywords.
        /// The resource manager is used for errorMessage lookup instead of the standard.
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <param name="keywords"></param>
        public OiosiCommunicationException(ResourceManager errorMessages, System.Collections.Generic.Dictionary<string, string> keywords) : base(errorMessages, keywords) { }

        /// <summary>
        /// Constructor that takes a resource manager and a dictionary of keywords.
        /// The resource manager is used for errorMessage lookup instead of the standard.
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <param name="keywords"></param>
        public OiosiCommunicationException(ResourceManager errorMessages, System.Collections.Generic.Dictionary<string, string> keywords, System.Exception innerException) : base(errorMessages, keywords, innerException) { }
    }
}