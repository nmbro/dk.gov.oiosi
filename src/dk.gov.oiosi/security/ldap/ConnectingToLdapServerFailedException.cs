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

namespace dk.gov.oiosi.security.ldap
{
    /// <summary>
    /// Exception that is thrown when the connection attemption to the LDAP server fails.
    /// </summary>
    public class ConnectingToLdapServerFailedException : LdapException {

        /// <summary>
        /// Constructor that takes the settings used for the connection and the cause 
        /// exception. It transforms the setting object into a keyword dictionary before
        /// calling its base constructor with the dictionary and inner exception as 
        /// parameters.
        /// </summary>
        /// <param name="settings">The settings used when attempting connection</param>
        /// <param name="innerException">The cause exception</param>
        public ConnectingToLdapServerFailedException(LdapSettings settings, System.Exception innerException) : base(CreateKeywords(settings), innerException) { }

        private static Dictionary<string, string> CreateKeywords(LdapSettings settings) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add("address", settings.Host.ToString());
            keywords.Add("port", settings.Port.ToString());
            return keywords;
        }
    }
}
