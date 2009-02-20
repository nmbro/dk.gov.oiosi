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
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.security {
    /// <summary>
    /// This class' public properties are used to store the result of a certification validation
    /// </summary>
    public class CertificateCheckResult {

        private bool _allTestsPassed;
        private bool _rootCertificateAsRoot;
        private bool _rootCertificateActivated;
        private bool _rootCertificateValid;     
        private bool _certificateActivated;
        private bool _certificateValid;
        private bool _certificateRevoked;
        

        private OcesCertificateType _certificateType;

        /// <summary>
        /// All tests have been passed
        /// </summary>
        public bool AllTestsPassed {
            get { return _allTestsPassed; }
            set { _allTestsPassed = value; }
        }

        /// <summary>
        /// Indicates if a chainvalidation has been successfull
        /// </summary>
        public bool RootCertificateAsRoot {
            get { return _rootCertificateAsRoot; }
            set { _rootCertificateAsRoot = value; }
        }

        /// <summary>
        /// Indicates if the rootcertificate is activated
        /// </summary>
        public bool RootCertificateActivated {
            get { return _rootCertificateActivated; }
            set { _rootCertificateActivated = value; }
        }

        /// <summary>
        /// Indicates if the root certificate is valid (is not expired)
        /// </summary>
        public bool RootCertificateValid {
            get { return _rootCertificateValid; }
            set { _rootCertificateValid = value; }
        }

        /// <summary>
        /// Indicates if the certificate is activated
        /// </summary>
        public bool CertificateActivated {
            get { return _certificateActivated; }
            set { _certificateActivated = value; }
        }

        /// <summary>
        /// Indicates if the certificate is valid (is not expired)
        /// </summary>
        public bool CertificateValid {
            get { return _certificateValid; }
            set { _certificateValid = value; }
        }

        /// <summary>
        /// Indicates if the certificate has been revoked
        /// </summary>
        public bool CertificateRevoked {
            get { return _certificateRevoked; }
            set { _certificateRevoked = value; }
        }

        /// <summary>
        /// The type of the OCES certificate
        /// </summary>
        public OcesCertificateType CertificateType {
            get { return _certificateType; }
            set { _certificateType = value; }
        }
    }
}