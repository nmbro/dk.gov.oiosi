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

namespace dk.gov.oiosi.security.revocation.ocsp {

    /// <summary>
    /// Custom exception used when an unexpected exception is thrown during a check to see
    /// if the certificate is revoked
    /// </summary>
    public class CheckCertificateOcspUnexpectedException : Exception {

        /// <summary>
        /// Base constructor
        /// </summary>
        public CheckCertificateOcspUnexpectedException()
            : base()
        { }

        /// <summary>
        /// Constructor with innerexception
        /// </summary>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public CheckCertificateOcspUnexpectedException(string message) 
            : base(message)
        { }

        /// <summary>
        /// Constructor with message and innerexception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public CheckCertificateOcspUnexpectedException(string message, Exception innerException) 
            : base(message, innerException) 
        { }
    }
}