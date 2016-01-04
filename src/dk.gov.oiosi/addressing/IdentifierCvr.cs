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
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.addressing {
    
    /// <summary>
    /// Represents a cvr number. May be instantiated with either of these formats:
    /// * DK12345678
    /// * 12345678
    /// 
    /// If the "DK"-format is used, "DK" is stripped off during construction.
    /// </summary>
    public class IdentifierCvr : Identifier
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cvrNumber">A CVR number</param>
        public IdentifierCvr(string cvrNumber)
            : base ("DK:CVR", cvrNumber) 
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value">A CVR number</param>
        public IdentifierCvr(string type, string value)
            : base(type, value)
        { }  

        /// <summary>
        /// Sets the CVR number
        /// </summary>
        /// <param name="cvrNumber">The CVR number</param>
        public override void Set(string cvrNumber) 
        {
            if (String.IsNullOrEmpty(cvrNumber))
            {
                throw new NullOrEmptyArgumentException("cvrNumber");
            }

            // If string starts with "dk", strip it away
            if (cvrNumber.ToLower().StartsWith("dk") && cvrNumber.Length > 2) {
                cvrNumber = cvrNumber.Substring(2);
            }

            if (cvrNumber.Length != 8)
            {
                throw new Exception("Not a valid cvr number, length is not 8.");
            }

            int cvrNumberAsInteger = 0;
            if (!int.TryParse(cvrNumber, out cvrNumberAsInteger))
            {
                throw new Exception("Not a valid cvr number, contains non digits.");
            }

            base.Set(cvrNumber);
        }       
    }
}
