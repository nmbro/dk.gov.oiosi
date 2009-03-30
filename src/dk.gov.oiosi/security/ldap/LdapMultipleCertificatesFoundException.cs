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

namespace dk.gov.oiosi.security.ldap {

    /// <summary>
    /// Found multiple certificate matches in the LDAP
    /// </summary>
    public class LdapMultipleCertificatesFoundException : dk.gov.oiosi.security.lookup.MultipleCertificatesFoundException {

        /// <summary>
        /// The resource manager
        /// </summary>
        private static ResourceManager resourceManager = new ResourceManager(typeof(ErrorMessages));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subject">The certificate subject</param>
        public LdapMultipleCertificatesFoundException(CertificateSubject subject) : base(resourceManager, GetKeywords(subject)) { }

        private static Dictionary<string, string> GetKeywords(CertificateSubject subject) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add("subjectstring", subject.SubjectString);
            return keywords;
        }
    }
}
