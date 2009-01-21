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
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.exception;
using dk.gov.oiosi.security.ocsp;

namespace dk.gov.oiosi.security.oces {

    /// <summary>
    /// Represents an OCES X509 certificate. 
    /// Encapsulates an X509Certificate2 object.
    /// </summary>
    public class OcesX509Certificate {
        
        private const string POLICYREGULAREXPRESSION = @"Policy Identifier=";
        private X509Certificate2 _x509Certificate;
        private X509CheckStatus _x509CheckStatus = X509CheckStatus.NotChecked;
        private OcspCheckStatus _ocspCheckStatus = OcspCheckStatus.NotChecked;
        private OcesCertificateType _ocesCertificateType = OcesCertificateType.NonOces;
        private CertificateSubject _subject;

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
                throw new InvalidOcesCertificateException(certificate);
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

        /// <summary>
        /// Returns the ocsp url
        /// </summary>
        public string OcspUrl {
            get {
                return "";
            }
        }

        /// <summary>
        /// Returns whether the certificate has a private key.
        /// </summary>
        /// <returns></returns>
        public bool HasPrivateKey() {
            return _x509Certificate.PrivateKey != null;
        }

        /// <summary>
        /// Try to get the cvr number as a string value from the certificate, if such exists
        /// </summary>
        /// <param name="cvrNumberString">The resulting cvr string</param>
        /// <returns>Whether a cvr number string value could be parsed.</returns>
        public bool TryGetCvrNumberString(out string cvrNumberString) {
            cvrNumberString = null;
            string serialNumberValue = _subject.SerialNumberValue;
            Regex regEx = new Regex("CVR:\\d+");
            Match match = regEx.Match(serialNumberValue);
            if (match == null) return false;
            cvrNumberString = match.Value.Replace("CVR:", "");
            cvrNumberString = cvrNumberString.Trim();
            return !string.IsNullOrEmpty(cvrNumberString);
        }


        /// <summary>
        /// Try to get the cvr number as a string value from the certificate, if such exists
        /// </summary>
        /// <param name="cvrNumberString">The resulting cvr string</param>
        /// <returns>Whether a cvr number string value could be parsed.</returns>
        public static bool TryGetCvrNumberString(CertificateSubject subject, out string cvrNumberString) {
            cvrNumberString = null;
            string serialNumberValue = subject.SerialNumberValue;
            Regex regEx = new Regex("CVR:\\d+");
            Match match = regEx.Match(serialNumberValue);
            if (match == null) return false;
            cvrNumberString = match.Value.Replace("CVR:", "");
            cvrNumberString = cvrNumberString.Trim();
            return !string.IsNullOrEmpty(cvrNumberString);
        }

        /// <summary>
        /// Get the OCES certificate type from a given certificate.
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static OcesCertificateType GetOcesCertificateType(X509Certificate2 certificate) {
            if (certificate == null) throw new NullArgumentException("certificate");
            try {
                //The code is using the subject as identifier of the the oces type.
                return GetFromSubject(certificate);
            }
            catch (Exception ex) {
                throw new FailedGetOcesCertificateTypeException(certificate, ex);
            }
        }

        /// <summary>
        /// Get the OCES certificate type from a given certificate subject.
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static OcesCertificateType GetOcesCertificateType(CertificateSubject subject) {
            if (subject == null) throw new NullArgumentException("subject");
             //The code is using the subject as identifier of the the oces type.
             return GetFromSubject(subject);
        }

        private static OcesCertificateType GetFromSubject(X509Certificate2 certificate) {
            CertificateSubject subject = new CertificateSubject(certificate.Subject);
            return GetFromSubject(subject);
        }

        private static OcesCertificateType GetFromSubject(CertificateSubject subject) {
                        OcesX509CertificateConfig _config = ConfigurationHandler.GetConfigurationSection<OcesX509CertificateConfig>();
            string ssn = subject.SerialNumberValue;
            if (ssn == null)
                return OcesCertificateType.NonOces;
            if (ssn.Contains(_config.EmployeeCertificateSubjectKey.SubjectKeyString))
                return OcesCertificateType.OcesEmployee;
            if (ssn.Contains(_config.OrganizationCertificateSubjectKey.SubjectKeyString))
                return OcesCertificateType.OcesOrganisation;
            if (ssn.Contains(_config.PersonalCertificateSubjectKey.SubjectKeyString))
                return OcesCertificateType.OcesPersonal;
            if (ssn.Contains(_config.FunctionCertificateSubjetKey.SubjectKeyString))
                return OcesCertificateType.OcesFunction;
            return OcesCertificateType.NonOces;
        }

        private void SetCertificateType() {
            _ocesCertificateType = GetOcesCertificateType(_x509Certificate);
        }
    }
}