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

using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.security.oces {
    /// <summary>
    /// Excpetion thrown when there is no cvr number in the OCES certificate subject.
    /// </summary>
    public class NoSubjectCvrNumberException : OcesCertificateException {
        /// <summary>
        /// Constructor that takes the certificate that has no cvr number in its subject.
        /// </summary>
        /// <param name="certificate"></param>
        public NoSubjectCvrNumberException(X509Certificate2 certificate) : base(KeywordsFromX509Certificate2.GetKeywords(certificate)) { }
    }
}
