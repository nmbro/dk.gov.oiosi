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

using System;
using System.Collections.Generic;

namespace dk.gov.oiosi.security.oces {
    /// <summary>
    /// General Oces certificate exception
    /// </summary>
    public class OcesCertificateException : CertificateHandlingException {
        /// <summary>
        /// Default constructor
        /// </summary>
        public OcesCertificateException() { }
        /// <summary>
        /// Constructor that takes a dictionary of keywords as parameter
        /// </summary>
        /// <param name="keywords"></param>
        public OcesCertificateException(Dictionary<string, string> keywords) : base(keywords) { }
        /// <summary>
        /// Constructor that takes an inner exception as parameter
        /// </summary>
        /// <param name="innerException"></param>
        public OcesCertificateException(Exception innerException) : base(innerException) { }
        /// <summary>
        /// Constructor that takes a dictionary of keywords and an inner exception
        /// as parameter
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="innerException"></param>
        public OcesCertificateException(Dictionary<string, string> keywords, Exception innerException) : base(keywords, innerException) { }
    }
}
