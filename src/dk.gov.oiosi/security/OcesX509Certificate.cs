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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using dk.gov.oiosi.security.ocsp;

namespace dk.gov.oiosi.security {

    /// <summary>
    /// Represents an OCES X509 certificate. 
    /// Encapsulates an X509Certificate2 object.
    /// </summary>
    public class OcesX509Certificate {
        private X509Certificate2 _x509Certificate;
        private X509CheckStatus _x509CheckStatus = X509CheckStatus.NotChecked;
        private OcspCheckStatus _ocspCheckStatus = OcspCheckStatus.NotChecked;
        private OcesCertificateType _ocesCertificateType = OcesCertificateType.NonOces;
        private CertificateSubject _subject;

        //TODO: why this ???
        private OcesX509Certificate() { }

        /// <summary>
        /// Constructor that takes the X509Certificate wrapped. If the certificate is not 
        /// an OCES-certificate an exception will be thrown.
        /// </summary>
        /// <param name="certificate">An OCES x509 certificate</param>
        public OcesX509Certificate(X509Certificate2 certificate) {
            if (certificate == null)
                throw new ArgumentNullException("certificate");
            _x509Certificate = certificate;
            _subject = new CertificateSubject(_x509Certificate.Subject);
            SetCertificateType();
            if (_ocesCertificateType == OcesCertificateType.NonOces)
                throw new NotAValidOcesCertificateException(certificate);
        }

        /// <summary>
        /// Checks the status of the certificate against an OCSP server.
        /// Updates the internal state with the result.
        /// </summary>
        /// <param name="ocspLookupClient">The OCSP client to use for the request</param>
        /// <returns>Returns the check status</returns>
        public OcspCheckStatus CheckOcspStatus(IOcspLookup ocspLookupClient) {
            OcspResponse response;
            try {
                response = ocspLookupClient.CheckCertificate(_x509Certificate);
            } catch {
                _ocspCheckStatus = OcspCheckStatus.UnknownIssue;
                throw;
            }

            if (response.IsValid)
                _ocspCheckStatus = OcspCheckStatus.AllChecksPassed;
            else
                _ocspCheckStatus = OcspCheckStatus.CertificateRevoked;
            
            return _ocspCheckStatus;
        }

        /// <summary>
        /// Gets the X509Certificate2 that is the basis of the OCES Certificate
        /// </summary>
        public X509Certificate2 Certificate {
            get { return _x509Certificate; }
        }

        /// <summary>
        /// Gets the certificate subject of the oces certificate.
        /// </summary>
        public CertificateSubject Subject {
            get { return _subject; }
        }

        /// <summary>
        /// Gets the certificate check status
        /// </summary>
        public X509CheckStatus X509CheckStatus {
            get { return _x509CheckStatus; }
        }
        
        /// <summary>
        /// Gets the ocsp check status
        /// </summary>
        public OcspCheckStatus OcspCheckStatus {
            get { return _ocspCheckStatus; }
        }
        
        /// <summary>
        /// Gets the certificate ocsp certificate type
        /// </summary>
        public OcesCertificateType OcesCertificateType {
            get { return _ocesCertificateType; }
        }

        /// <summary>
        /// Gets certificate serial number
        /// </summary>
        public CertificateSubject SubjectSerialNumber {
            get {
                if (_x509Certificate == null) return null;
                else return new CertificateSubject(_x509Certificate.Subject);
            }
        }


        public static OcesCertificateType GetOcesCertificateType(X509Certificate2 certificate) {
            try {
                CertificateSubject subject = new CertificateSubject(certificate.Subject);

                string ssn = subject.SerialNumberValue;
                if (ssn.Contains("RID:"))
                    return OcesCertificateType.OcesEmployee;
                if (ssn.Contains("UID:"))
                    return OcesCertificateType.OcesOrganisation;
                if (ssn.Contains("PID:"))
                    return OcesCertificateType.OcesPersonal;
                if (ssn.Contains("DID:"))
                    return OcesCertificateType.OcesDevice;
            } 
            catch (Exception) { }
            return OcesCertificateType.NonOces;
        }

        private void SetCertificateType() {
            _ocesCertificateType = GetOcesCertificateType(_x509Certificate);
        }
    }
}