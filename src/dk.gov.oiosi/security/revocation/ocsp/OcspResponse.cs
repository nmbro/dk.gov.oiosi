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

namespace dk.gov.oiosi.security.revocation.ocsp {
    /// <summary>
    /// This class is used to store the result of a Ocsp certification validation process
    /// </summary>
    public class OcspResponse {

        private bool _isValid = false;
        private DateTime _nextUpdate = new DateTime();
        private Exception exception = null;
        private OcspCheckStatus ocspCheckStatus = OcspCheckStatus.NotChecked;

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

        /// <summary>
        /// Gets or sets the exception that caught doing the revocation check.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return this.exception;
            }
            set
            {
                this.exception = value;
            }
        }

        /// <summary>
        /// Gets or sets the revocasion status of the revocation check.
        /// </summary>
        public OcspCheckStatus OcspCheckStatus
        {
            get
            {
                return this.ocspCheckStatus;
            }
            set
            {
                this.ocspCheckStatus = value;
            }
        }
    }
}