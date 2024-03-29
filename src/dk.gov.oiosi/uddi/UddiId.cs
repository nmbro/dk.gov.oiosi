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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.uddi {
    
    /// <summary>
    /// Abstract base class for all UDDI v. 2.0 and 3.0 ID's, e.g. "uddi:d01987d1-ab2e-3013-9be2-2a66eb99d824".
    /// </summary>
    public abstract class UddiId: IEquatable<UddiId> {

        /// <summary>
        /// Abstract method for getting id
        /// </summary>
        public abstract string ID { get;}

        #region IEquatable<UddiId> Members

        /// <summary>
        /// Compares the two objects and returns true if they have equal values
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Returns true if the two objects have identical values</returns>
        public bool Equals(UddiId other) {
            if (ID == null) throw new NullArgumentException("ID in UddiId");
            if (other == null) return false;
            if (ID.Equals(other.ID, StringComparison.CurrentCultureIgnoreCase)) return true;
            return false;
        }

        #endregion

        /// <summary>
        /// Equals values
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;
            UddiId other = (UddiId)obj;

            return ID.Equals(other.ID);
        }

        /// <summary>
        /// Hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}