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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace dk.gov.oiosi.security.oces {
    /// <summary>
    /// Represents an employee oces x509 certificate.
    /// </summary>
    public class EmployeeOcesX509Certificate : OcesX509Certificate {
        private string _cvrNumber;

        /// <summary>
        /// Constructor that takes an x509 certificate as parameter.
        /// If the certificate is not an employee certificate an exception is 
        /// thrown.
        /// </summary>
        /// <param name="certificate"></param>
        public EmployeeOcesX509Certificate(X509Certificate2 certificate) : base(certificate) {
            if (OcesCertificateType != OcesCertificateType.OcesEmployee)
                throw new InvalidOcesEmployeeCertificateException(certificate);
            SetCvrNumber();
        }

        /// <summary>
        /// Constructor that takes an ocescertificate as parameter.
        /// If the certificate is not an employee certificate an exception is 
        /// thrown.
        /// </summary>
        /// <param name="certifcate"></param>
        public EmployeeOcesX509Certificate(OcesX509Certificate certifcate) : this(certifcate.Certificate) {
            if (OcesCertificateType != OcesCertificateType.OcesEmployee)
                throw new InvalidOcesEmployeeCertificateException(certifcate.Certificate);
            SetCvrNumber();
        }

        /// <summary>
        /// Gets the cvr number of the employee certificate
        /// </summary>
        public string CvrNumber {
            get { return _cvrNumber; }
        }

        private void SetCvrNumber() {
            string serialNumber = SubjectSerialNumber.SerialNumberValue;
            Regex regex = new Regex("(cvr:)(\\d)*", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(serialNumber);
            if (matches.Count < 1) throw new NoSubjectCvrNumberException(Certificate);
            if (matches.Count > 1) throw new AmbigousSubjectCvrNumberException(Certificate);
            string fullCvrString = matches[0].Value;
            _cvrNumber = fullCvrString.Substring(4);
        }
    }
}
