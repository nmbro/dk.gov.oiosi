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

using System.Resources;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.security {

    /// <summary>
    /// Baseclass for an exception that occurs while handling a certificate
    /// </summary>
    public class CertificateHandlingException : MainException {

        private static ResourceManager resourceManager = new ResourceManager(typeof(ErrorMessages));

        /// <summary>
        /// Constructor
        /// </summary>
        public CertificateHandlingException() : base(resourceManager) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keywords">The keywords to add</param>
        public CertificateHandlingException(
            System.Collections.Generic.Dictionary<string, string> keywords) 
                : base(resourceManager, keywords) { }

        /// <summary>
        /// Constructor. Takes an exception and displays it as an inner exception
        /// </summary>
        /// <param name="innerException">The exception to display as an inner exception</param>
        public CertificateHandlingException(System.Exception innerException) 
            : base(resourceManager, innerException) { }

        /// <summary>
        /// Constructor. Takes a set of keywords and an inner exception
        /// </summary>
        /// <param name="keywords">The keywords to add</param>
        /// <param name="innerException">The exception to display as an inner exception</param>
        public CertificateHandlingException(System.Collections.Generic.Dictionary<string, string> keywords, System.Exception innerException) : base(resourceManager, keywords, innerException) { }
    }
}
