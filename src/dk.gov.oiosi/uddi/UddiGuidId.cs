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
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.uddi {
    
    /// <summary>
    /// Represents a UDDI type guid.
    /// </summary>
    public class UddiGuidId : UddiId {
        private UddiStringId id;

        /// <summary>
        /// Creates a new GUID
        /// </summary>
        public UddiGuidId() {
            id = new UddiStringId();
        }

        /// <summary>
        /// Tests a guid to see if it is a valid uddi guid
        /// </summary>
        /// <param name="guid">guid to test</param>
        /// <param name="isUddiType">indicates if it is a uddi type guid</param>
        /// <returns>Returns true if the guid is valid</returns>
        public static bool IsValidGuidId(string guid, bool isUddiType) {
            if (String.IsNullOrEmpty(guid) || guid.Length < 10) return false;
            
            string guidString = "";

            if (isUddiType) {
                guidString = guid.Substring(5);
            } else {
                guidString = guid;
            }

            try {
                Guid g = new Guid(guidString);
            } catch (Exception) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="isUddiType">is it uddi type</param>
        public UddiGuidId(string guid, bool isUddiType) {
            id = new UddiStringId(guid, isUddiType);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="guid">guid</param>
        public UddiGuidId(Guid guid) {
            id = new UddiStringId(guid);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Id"></param>
        public UddiGuidId(UddiId Id): this(Id.ID, true) {}

        /// <summary>
        /// Gets the guid
        /// </summary>
        public override string ID {
            get { return id.ID; }
        }

        /// <summary>
        /// Gets as string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString() {
            return ID;
        }

    }
}