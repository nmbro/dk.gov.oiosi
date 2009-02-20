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

namespace dk.gov.oiosi.security.revocation {
    /// <summary>
    /// This class is used to store the result of a Ocsp certification validation process
    /// </summary>
    public class RevocationResponse {

        private bool _isValid;
        private DateTime _nextUpdate = new DateTime();

        /// <summary>
        /// This property is used to store the time at or before which newer information will be available
        /// about the status of the certificate. CURRENTLY NOT USED IN THIS COMPONENT!
        /// </summary>
        public DateTime NextUpdate {
            get { return _nextUpdate; }
            set { _nextUpdate = value; }
        }

        /// <summary>
        /// This property is used to indicate if the certificate has been revoked (either permanantly or
        /// temporarily (on hold))
        /// </summary>
        public bool IsValid {
            get { return _isValid; }
            set { _isValid = value; }            
        }        
    }
}