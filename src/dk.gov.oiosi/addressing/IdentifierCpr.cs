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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.exception;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.addressing
{
    public class IdentifierCpr : Identifier
    {
        private string _cprNumber;
        private const string keyTypeValue = "http://oio.dk/profiles/OIOSI/1.2/UDDI/Identifiers/cprNumber/";
    
        public override string KeyTypeValue
        {
            get { return keyTypeValue; }
        }

        public override EndpointKeyTypeCode KeyTypeCode
        {
            get { return EndpointKeyTypeCode.cpr; }
        }

        public override bool IsAllowedInPublic
        {
            get { return false; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cprNumber">A CPR number</param>
        public IdentifierCpr(string cprNumber) {
            Set(cprNumber);
        }

        public override string GetAsString()
        {
            return _cprNumber;
        }

        public override void Set(string identifier)
        {
            if (String.IsNullOrEmpty(identifier)) throw new NullOrEmptyArgumentException("identifier");
            _cprNumber = identifier;
        }

        public override bool Equals(Identifier other)
        {
            if (other == null) return false;

            if (GetAsString() != other.GetAsString()) return false;
            return true;
        }
    }
}
