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
using dk.gov.oiosi.communication;
using System.Text.RegularExpressions;

using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common.validation;

namespace dk.gov.oiosi.common.validation {

    /// <summary>
    /// Class to validate version-strings
    /// </summary>
    public class VersionValidator {

        /// <summary>
        /// Validates the version-string
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ValueName"></param>
        /// <param name="Required"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public static bool IsValid(string Value, string ValueName, bool Required, 
                ref ValidationFailureCollection Failures) {
            bool Valid = true;
            if (!ValUtil.IsEmpty(Value)) {
                if (Value.Length!=5 || !Regex.IsMatch(Value, @"\d.\d.\d")) {
                DataValidationFailure.AddFailure(InvalidVersionFailure.Message(Value), ValueName, typeof(Version), ref Failures);
                    Valid = false;
                }
            } else 
                // If field is required then it must not be null
                if (Required) {
                    DataValidationFailure.AddFailure(
                        RequiredFieldFailure.Message(ValueName), ValueName, typeof(Version), ref Failures);
                    Valid = false;
                }

            return Valid;
        }

        /// <summary>
        /// Validates the version-string
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ValueName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public static bool IsValid(string Value, string ValueName, ref ValidationFailureCollection Failures) {
            return IsValid(Value, ValueName, false, ref Failures);
        }
    }
}
