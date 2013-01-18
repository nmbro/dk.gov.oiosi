using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace dk.gov.oiosi.security {
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
                throw new NotAValidOcesEmployeeCertificateException(certificate);
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
                throw new NotAValidOcesEmployeeCertificateException(certifcate.Certificate);
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
            if (matches.Count < 1) throw new Exception("TODO: No cvr number found in the subject serial number.");
            if (matches.Count > 1) throw new Exception("TODO: Ambigous cvr number found in the subject serial number.");
            string fullCvrString = matches[0].Value;
            _cvrNumber = fullCvrString.Substring(4);
        }
    }
}
