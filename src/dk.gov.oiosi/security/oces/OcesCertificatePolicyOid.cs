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
using System.Text.RegularExpressions;

using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.security.oces {
    /// <summary>
    /// Represesents the oid of the oces certficate policy
    /// </summary>
    public class OcesCertificatePolicyOid {
        /// <summary>
        /// String value of the regular expression that selects the oid
        /// </summary>
        //public const string OIDREGULAREXPRESSION = @"\d+\.\d+\.\d+\.\d+\.\d+\.\d+\.\d+\.\d+\.\d+";
        public const string OIDREGULAREXPRESSION = @"(\d+\.){8}\d+";
        private string _policyOidString = "0.0.0.0.0.0.0.0.0";
        /// <summary>
        /// Default constructor, used by the xml serialization. It should not be used.
        /// </summary>
        public OcesCertificatePolicyOid() { }

        /// <summary>
        /// Constructor that takes a string representing the policy oid. If the 
        /// string is not in the valid format it throw an exception.
        /// </summary>
        /// <param name="policyOidString"></param>
        public OcesCertificatePolicyOid(string policyOidString) {
            if (policyOidString == null)
                throw new NullArgumentException("policyOidString");
            ValidatePolicyOidString(policyOidString);
            _policyOidString = policyOidString;
        }

        /// <summary>
        /// Gets and sets the policy oid string
        /// </summary>
        public string PolicyOidString {
            get { return _policyOidString; }
            set {
                if (value == null)
                    throw new NullArgumentException("OcesCertificatePolicyOid.PolicyOidString");
                ValidatePolicyOidString(value);
                _policyOidString = value; 
            }
        }

        /// <summary>
        /// Returns whether the two OcesCertificatePolicyOids are equal. This comparison
        /// ignores the version number.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool NonVersionEquals(OcesCertificatePolicyOid other) {
            string otherNonVersionOidString = other.GetNonVersionPolicyOidString();
            string nonVersionOidString = GetNonVersionPolicyOidString();
            return nonVersionOidString == otherNonVersionOidString;
        }

        /// <summary>
        /// Gets the policy oid string without the last version numbering.
        /// </summary>
        /// <returns></returns>
        public string GetNonVersionPolicyOidString() {
            Regex nonVersionPolicyOidRegEx = new Regex(@"^(\d+\.){7}\d+");
            Match match = nonVersionPolicyOidRegEx.Match(_policyOidString);
            return match.Value;
        }

        private void ValidatePolicyOidString(string policyOidString) {
            Regex policyOidRegEx = new Regex("^"+ OIDREGULAREXPRESSION +"$");
            if (!policyOidRegEx.IsMatch(policyOidString))
                throw new InvalidOcesCertificatePolicyOidException(policyOidString);
        }
    }
}
