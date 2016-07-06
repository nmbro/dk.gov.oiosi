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
using dk.gov.oiosi.common;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.exception;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.addressing {
    
    /// <summary>
    /// Represents and endpoint or businesEntity type identifier
    /// </summary>
    public class Identifier: IEquatable<Identifier> 
    {
        /// <summary>
        /// The value, Eg. 12345678
        /// </summary>
        private string value;

        /// <summary>
        /// The type of identifier, Eg. CVR
        /// </summary>
        private string type;

        /// <summary>
        /// Value indication whether or not the value is allowed in the transmitting header
        /// </summary>
        private bool isAllowedInPublic;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type">A identifying type</param>
        /// <param name="value">The value</param>
        public Identifier(string type, string value)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new NullOrEmptyArgumentException("identifierType");
            }

            this.type = type;
            if(this.type.Equals("", StringComparison.OrdinalIgnoreCase))
            {
                this.isAllowedInPublic = false;
            }
            this.Set(value);            
        }

        /// <summary>
        /// Gets the KeyTypeValue of the Identifier
        /// </summary>
        public virtual string KeyTypeValue 
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Gets the Type of the IIdentifier. E.g. "CPR"
        /// </summary>
        ////public abstract EndpointKeyTypeCode KeyTypeCode
        ////{
        ////    get;
        ////}
        public virtual string KeyTypeCode
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Determines whether the type of identifier is allowed in the custom rasp headers.
        /// </summary>
        public virtual bool IsAllowedInPublic 
        {
            get
            {
                return this.isAllowedInPublic;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetAsString()
        {
            return this.value;
        }

        /// <summary>
        /// Set the value
        /// </summary>
        /// <param name="identifier"></param>
        public virtual void Set(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new NullOrEmptyArgumentException("identifier");
            }

            if (this.type.Equals("DK:CVR", StringComparison.OrdinalIgnoreCase) || this.type.Equals("CVR", StringComparison.OrdinalIgnoreCase))
            {
                // If endpoint type is DK:CVR, and the value starts with "dk", strip it away
                if (identifier.StartsWith("dk", StringComparison.OrdinalIgnoreCase) && identifier.Length > 2)
                {
                    this.value = identifier.Substring(2);
                }
                else
                {
                    this.value = identifier;
                }
            }
            else
            {
                // normal endpoint type
                this.value = identifier;
            }
        }

        
        /// <summary>
        /// Compares the two objects and returns true if they have equal values
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Returns true if the two objects have identical values</returns>
        public virtual bool Equals(Identifier other)
        {
            bool result = true;
            if (other == null)
            {
                result = false;
            }
            else if (!this.GetAsString().Equals(other.GetAsString()))
            {
                result = false;
            }
            else if (!this.KeyTypeValue.Equals(other.KeyTypeValue))
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Returns hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.GetAsString().GetHashCode();
        }

        public override string ToString()
        {
            return this.GetAsString();
        }
    }
}
