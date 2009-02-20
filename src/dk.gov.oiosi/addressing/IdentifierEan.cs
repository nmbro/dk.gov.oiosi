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
    /// Represents an EAN identifier
    /// </summary>
    public class IdentifierEan : IIdentifier {
        private string _eanNumber;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eanNumber">The EAN identifier</param>
        public IdentifierEan(string eanNumber) {
            Set(eanNumber);
        }

        /// <summary>
        /// Validates and sets the EAN identifier
        /// </summary>
        /// <param name="eanNumber">The EAN number</param>
        public void Set(string eanNumber) {
            if (String.IsNullOrEmpty(eanNumber)) {
                throw new NullOrEmptyArgumentException("eanNumber");
            }
            _eanNumber = eanNumber;
        }

        /// <summary>
        /// Returns the EAN identifier as a string
        /// </summary>
        /// <returns>Returns the EAN identifier</returns>
        public string GetAsString() {
            return _eanNumber;
        }

        /// <summary>
        /// Compares the two objects and returns true if they have equal values
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Returns true if the two objects have identical values</returns>
        public bool Equals(IIdentifier other) {
            if (other == null) return false;

            if (GetAsString() != other.GetAsString()) return false;
            return true;
        }

    }
}
