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
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.addressing {
    
    /// <summary>
    /// Represents a ovt number
    /// </summary>
    public class IdentifierOvt : IIdentifier {
        private string _countryCode;

        /// <summary>
        /// property for countrycode
        /// </summary>
        public string CountryCode {
            get { return _countryCode; }
        }

        private string _businessIdentifier;

        /// <summary>
        /// property for businessidentifier
        /// </summary>
        public string BusinessIdentifier {
            get { return _businessIdentifier; }
        }

        private string _serialNumber;

        /// <summary>
        /// property for certificate serialnumber
        /// </summary>
        public string SerialNumber {
            get { return _serialNumber; }
        }

        /// <summary>
        /// Tries to return the business identifier as a CVR number identifier.
        /// Throws an exception if the format is not compatible.
        /// </summary>
        /// <returns>The business identifier as a CVR number identifier.</returns>
        public IdentifierCvr GetBusinessIdentifierAsCvr() {
            return new IdentifierCvr(_businessIdentifier);
        }

        private void ValidateCountryCode(string countryCode) {
            try {
                if (String.IsNullOrEmpty(countryCode)) throw new NullOrEmptyArgumentException("countryCode");
                if (countryCode.Length != 4) throw new UnexpectedNumberOfCharactersException("countryCode", 4);
            } 
            catch (Exception ex) {
                throw new IncorrectCountryCodeException(countryCode, ex);
            }
        }

        private void ValidateBusinessIdentifier(string businessIdentifier) {
            try {
                if (String.IsNullOrEmpty(businessIdentifier)) throw new NullOrEmptyArgumentException("businessIdentifier");
                if (businessIdentifier.Length != 8) throw new UnexpectedNumberOfCharactersException("businessIdentifier", 4);
            } 
            catch (Exception ex) {
                throw new IncorrectBusinessIdentifierException(businessIdentifier ,ex);
            }
        }

        private void ValidateSerialNumber(string serialNumber) {
            try {
                if (String.IsNullOrEmpty(serialNumber)) throw new NullOrEmptyArgumentException("serialNumber");
                if (serialNumber.Length != 5) throw new UnexpectedNumberOfCharactersException("serialNumber", 5);
            } 
            catch (Exception ex) {
                throw new IncorrectSerialNumberException(serialNumber, ex);
            }
        }


        /// <summary>
        /// constructor setting the ovtnumber
        /// </summary>
        /// <param name="ovtNumber">the ovt number to set</param>
        public IdentifierOvt(string ovtNumber) {
            Set(ovtNumber);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="countryCode">countrycode</param>
        /// <param name="cvrNumber">cvr number</param>
        /// <param name="serialNumber">certificate serialnumber</param>
        public IdentifierOvt(string countryCode, IdentifierCvr cvrNumber, string serialNumber) {
            if (cvrNumber == null) throw new NullArgumentException("cvrNumber");
            ValidateCountryCode(countryCode);
            ValidateSerialNumber(serialNumber);
            _countryCode = countryCode;
            _serialNumber = serialNumber;
            _businessIdentifier = cvrNumber.GetAsString();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="countryCode">countrycode</param>
        /// <param name="businessIdentifier">businessidentifier</param>
        /// <param name="serialNumber">certificate serialnumber</param>
        public IdentifierOvt(string countryCode, string businessIdentifier, string serialNumber) {
            ValidateCountryCode(countryCode);
            ValidateSerialNumber(serialNumber);
            ValidateBusinessIdentifier(businessIdentifier);
            
            _countryCode = countryCode;
            _serialNumber = serialNumber;
            _businessIdentifier = businessIdentifier;
        }

        /// <summary>
        /// Validates and sets ovt number
        /// </summary>
        /// <param name="ovtNumber"></param>
        public void Set(string ovtNumber) {
            try {
                if (String.IsNullOrEmpty(ovtNumber)) throw new NullOrEmptyArgumentException("ovtNumber");
                // 1. Check length:
                if (ovtNumber.Length != 17) throw new UnexpectedNumberOfCharactersException("ovtNumber", 17);
            }
            catch (Exception ex) {
                throw new IncorrectOvtNumberException(ovtNumber, ex);
            }
            // 2. Split:
            _countryCode = ovtNumber.Substring(0, 4);
            _businessIdentifier = ovtNumber.Substring(4, 8);
            _serialNumber = ovtNumber.Substring(12, 5);

            // 3. Validate:
            ValidateBusinessIdentifier(_businessIdentifier);
            ValidateCountryCode(_countryCode);
            ValidateSerialNumber(_serialNumber);
        }

        /// <summary>
        /// returns ovt number as a string
        /// </summary>
        /// <returns></returns>
        public string GetAsString() {
            return _countryCode + _businessIdentifier + _serialNumber;
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