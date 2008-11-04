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
    /// The exception is thrown if an regular expression pattern cannot be found in the
    /// subject string.
    /// </summary>
    public class PatternsDoesNotMatchException : LdapException
    {
        /// <summary>
        /// Constructor that takes the subject stríng and the patterns that have failed as 
        /// parameters. It transforms the parameters into a keyword dictionary. The 
        /// constructor finally calls a base constructor with the keyword dictionary
        /// </summary>
        /// <param name="subject">subject string to test</param>
        /// <param name="patternsArray">the regulary expression</param>
        public PatternsDoesNotMatchException(string subject, string[] patternsArray) : base(SetMessage(subject, patternsArray)) { }

        private static Dictionary<string, string> SetMessage(string subject, string[] patternsArray)
        {
            string patterns = "";
            foreach (string pattern in patternsArray)
            {
                patterns += "'" + pattern + "' ";
            }
            patterns = patterns.Trim();
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add("subject", subject);
            keywords.Add("patterns", patterns);
            return keywords;
        }
    }
}