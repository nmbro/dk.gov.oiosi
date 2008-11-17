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
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

using dk.gov.oiosi.security;

namespace dk.gov.oiosi.security.ldap {

    /// <summary>
    /// Certificate not found in LDAP exception
    /// </summary>
    public class LdapCertificateNotFoundException : dk.gov.oiosi.security.lookup.CertificateNotFoundException {
        private static ResourceManager resources = new ResourceManager(typeof(ErrorMessages));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subject">The certificate subject</param>
        public LdapCertificateNotFoundException(CertificateSubject subject) : base(resources, GetKeywords(subject)) { }

        private static Dictionary<string, string> GetKeywords(CertificateSubject subject) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add("subjectstring", subject.SubjectString);
            return keywords;
        }
    }
}