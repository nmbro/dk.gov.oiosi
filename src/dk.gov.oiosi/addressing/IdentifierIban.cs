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
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.addressing {

    /// <summary>
    /// Represents a IBAN number identifier
    /// </summary>
    public class IdentifierIban : Identifier {
        private string _ibanNumber;
        private const string keyTypeValue = "http://oio.dk/profiles/OIOSI/1.1/UDDI/Identifiers/ibanNumber/";

        /// <summary>
        /// Identifier key type value
        /// </summary>
        public override string KeyTypeValue {
            get { return keyTypeValue; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ibanNumber">The IBAN identifier</param>
        public IdentifierIban(string ibanNumber) {
            Set(ibanNumber);
        }

        /// <summary>
        /// Validates and sets the IBAN identifier
        /// </summary>
        /// <param name="ibanNumber">The IBAN number</param>
        public override void Set(string ibanNumber) {
            if (String.IsNullOrEmpty(ibanNumber)) {
                throw new NullOrEmptyArgumentException("ibanNumber");
            }
            _ibanNumber = ibanNumber;
        }

        /// <summary>
        /// Returns the IBAN identifier as a string
        /// </summary>
        /// <returns>Returns the IBAN identifier</returns>
        public override string GetAsString() {
            return _ibanNumber;
        }

        /// <summary>
        /// Compares the two objects and returns true if they have equal values
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Returns true if the two objects have identical values</returns>
        public override bool Equals(Identifier other) {
            if (other == null) return false;

            if (GetAsString() != other.GetAsString()) return false;
            return true;
        }

    }
}
