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
using System.Resources;

namespace dk.gov.oiosi.security.lookup {
    /// <summary>
    /// Exception that is thrown when a certificate is not found.
    /// </summary>
    public class CertificateNotFoundException : CertificateLookupException {
        /// <summary>
        /// Constructor that takes a dictionay with keywords.
        /// </summary>
        /// <param name="keywords"></param>
        public CertificateNotFoundException(Dictionary<string, string> keywords) : base(keywords) { }
        /// <summary>
        /// Constructor that takes a resource manager and a dictionary with keywords.
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <param name="keywords"></param>
        public CertificateNotFoundException(ResourceManager errorMessages, Dictionary<string, string> keywords) : base(errorMessages, keywords) { }
    }
}
