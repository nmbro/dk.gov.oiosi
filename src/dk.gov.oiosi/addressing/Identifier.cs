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

namespace dk.gov.oiosi.addressing {
    
    /// <summary>
    /// Represents and endpoint or businesEntity type identifier
    /// </summary>
    public abstract class Identifier: IEquatable<Identifier> {

        /// <summary>
        /// Gets the KeyTypeValue of the IIdentifier
        /// </summary>
        public abstract string KeyTypeValue {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract string GetAsString();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        public abstract void Set(string identifier);

        public abstract bool Equals(Identifier other);

        public override bool Equals(object obj) {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;
            Identifier other = (Identifier)obj;

            if (!GetAsString().Equals(other.GetAsString())) return false;

            if (!KeyTypeValue.Equals(other.KeyTypeValue)) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return GetAsString().GetHashCode();
        }
    }
}