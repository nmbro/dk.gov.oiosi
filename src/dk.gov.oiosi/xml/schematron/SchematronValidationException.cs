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
  *   Jacob Mogensen, mySupply ApS
  *   
  */

using System.Resources;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.xml.schematron {

    /// <summary>
    /// Abstract schematrin validation exception
    /// </summary>
    public abstract class SchematronValidationException : MainException {

        private static ResourceManager resourceManager = new ResourceManager(typeof(ErrorMessages));

        /// <summary>
        /// Base constructor
        /// </summary>
        public SchematronValidationException() : base(resourceManager) { }
        
        /// <summary>
        /// Constructor with keywords
        /// </summary>
        /// <param name="keywords">keywords for the message</param>
        public SchematronValidationException(System.Collections.Generic.Dictionary<string, string> keywords) : base(resourceManager, keywords) { }
        
        /// <summary>
        /// Constructor with innerexception
        /// </summary>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public SchematronValidationException(System.Exception innerException) : base(resourceManager, innerException) { }
        
        /// <summary>
        /// Constructor with keywords and innerexceptions
        /// </summary>
        /// <param name="keywords">keywords for the message</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public SchematronValidationException(System.Collections.Generic.Dictionary<string, string> keywords, System.Exception innerException) : base(resourceManager, keywords, innerException) { }
    }
}