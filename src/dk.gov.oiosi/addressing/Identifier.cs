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
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.addressing {
    
    /// <summary>
    /// Represents and endpoint or businesEntity type identifier
    /// </summary>
    public abstract class Identifier: IEquatable<Identifier> {

        /// <summary>
        /// Gets the KeyTypeValue of the Identifier
        /// </summary>
        public abstract string KeyTypeValue {
            get;
        }

        /// <summary>
        /// Gets the Type of the IIdentifier. E.g. "CPR"
        /// </summary>
        public abstract EndpointKeyTypeCode KeyTypeCode
        {
            get;
        }

        /// <summary>
        /// Determines whether the type of identifier is allowed in the custom rasp headers.
        /// </summary>
        public abstract bool IsAllowedInPublic {
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

        /// <summary>
        /// Compares the value of this instance with another instance of the same type
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract bool Equals(Identifier other);

        /// <summary>
        /// Equals values
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;
            Identifier other = (Identifier)obj;

            if (!GetAsString().Equals(other.GetAsString())) return false;

            if (!KeyTypeValue.Equals(other.KeyTypeValue)) return false;

            return true;
        }

        /// <summary>
        /// Returns hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return GetAsString().GetHashCode();
        }
    }
}
