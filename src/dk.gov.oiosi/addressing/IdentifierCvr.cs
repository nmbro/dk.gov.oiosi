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
using System.Text;

using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.addressing {
    
    /// <summary>
    /// Represents a cvr number. May be instantiated with either of these formats:
    /// * DK12345678
    /// * 12345678
    /// 
    /// If the "DK"-format is used, "DK" is stripped off during construction.
    /// </summary>
    public class IdentifierCvr : IIdentifier {
        private string _cvrNumber;

        /// <summary>
        /// Constructor. Acc
        /// </summary>
        /// <param name="cvrNumber">A CVR number</param>
        public IdentifierCvr(string cvrNumber) {
            Set(cvrNumber);
        }

        /// <summary>
        /// Sets the CVR number
        /// </summary>
        /// <param name="cvrNumber">The CVR number</param>
        public void Set(string cvrNumber) {
            if (String.IsNullOrEmpty(cvrNumber)) throw new NullOrEmptyArgumentException("cvrNumber");
            // If string starts with "dk", strip it away
            if (cvrNumber.ToLower().StartsWith("dk") && cvrNumber.Length > 2) {
                cvrNumber = cvrNumber.Substring(2);
            }
            if (cvrNumber.Length != 8) throw new Exception("Not a valid cvr number, length is not 8.");
            int cvrNumberAsInteger = 0;
            if (!int.TryParse(cvrNumber, out cvrNumberAsInteger)) throw new Exception("Not a valid cvr number, contains non digits.");
            _cvrNumber = cvrNumber;
        }

        /// <summary>
        /// Returns the CVR number as string
        /// </summary>
        /// <returns>Returns the CVR number as string</returns>
        public string GetAsString() {
            return _cvrNumber;
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