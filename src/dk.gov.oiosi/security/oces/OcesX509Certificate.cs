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
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.exception;
using dk.gov.oiosi.security.revocation;

namespace dk.gov.oiosi.security.oces {

    /// <summary>
    /// Represents an OCES X509 certificate. 
    /// Encapsulates an X509Certificate2 object.
    /// </summary>
    public class OcesX509Certificate {
        
        private const string POLICYREGULAREXPRESSION = @"Policy Identifier=";
        private X509Certificate2 x509Certificate;
        private X509CheckStatus x509CheckStatus = X509CheckStatus.NotChecked;
        private RevocationCheckStatus revocationCheckStatus = RevocationCheckStatus.NotChecked;
        private OcesCertificateType ocesCertificateType = OcesCertificateType.NonOces;
        private CertificateSubject subject;

        /// <summary>
        /// Constructor that takes the X509Certificate wrapped. If the certificate is not 
        /// an OCES-certificate an exception will be thrown.
        /// </summary>
        /// <param name="certificate">An OCES x509 certificate</param>
        public OcesX509Certificate(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            this.x509Certificate = certificate;
            this.subject = new CertificateSubject(this.x509Certificate.Subject);
            this.SetCertificateType();
            if (this.ocesCertificateType == OcesCertificateType.NonOces)
            {
                throw new InvalidOcesCertificateException(certificate);
            }
        }

        /// <summary>
        /// Checks the status of the certificate against an OCSP server.
        /// Updates the internal state with the result.
        /// </summary>
        /// <param name="revocationLookupClient">The OCSP client to use for the request</param>
        /// <returns>Returns the check status</returns>
        public RevocationResponse CheckRevocationStatus(IRevocationLookup revocationLookupClient)
        {
            RevocationResponse response = new RevocationResponse();

            try 
            {
                response = revocationLookupClient.CheckCertificate(x509Certificate);

                if (response.Exception == null)
                {
                    if (response.IsValid)
                    {
                        response.RevocationCheckStatus = RevocationCheckStatus.AllChecksPassed;
                    }
                    else
                    {
                        response.RevocationCheckStatus = RevocationCheckStatus.CertificateRevoked;
                    }
                }
                else
                {
                    response.RevocationCheckStatus = RevocationCheckStatus.UnknownIssue;
                }
            } 
            catch (Exception e)
            {
                response.Exception = e;
                response.RevocationCheckStatus = RevocationCheckStatus.UnknownIssue;
            }

            return response;
        }

        /// <summary>
        /// Gets the X509Certificate2 that is the basis of the OCES Certificate
        /// </summary>
        public X509Certificate2 Certificate 
        {
            get { return x509Certificate; }
        }

        /// <summary>
        /// Gets the certificate subject of the oces certificate.
        /// </summary>
        public CertificateSubject Subject
        {
            get { return subject; }
        }

        /// <summary>
        /// Gets the certificate check status
        /// </summary>
        public X509CheckStatus X509CheckStatus
        {
            get { return x509CheckStatus; }
        }
        
        /// <summary>
        /// Gets the ocsp check status
        /// </summary>
        public RevocationCheckStatus RevocationCheckStatus 
        {
            get { return revocationCheckStatus; }
        }
        
        /// <summary>
        /// Gets the certificate ocsp certificate type
        /// </summary>
        public OcesCertificateType OcesCertificateType {
            get { return ocesCertificateType; }
        }

        /// <summary>
        /// Gets certificate serial number
        /// </summary>
        public CertificateSubject SubjectSerialNumber
        {
            get {
                if (x509Certificate == null) return null;
                else return new CertificateSubject(x509Certificate.Subject);
            }
        }

        /// <summary>
        /// Returns the ocsp url
        /// </summary>
        public string OcspUrl
        {
            get {
                return "";
            }
        }

        /// <summary>
        /// Returns whether the certificate has a private key.
        /// </summary>
        /// <returns></returns>
        public bool HasPrivateKey()
        {
            return x509Certificate.PrivateKey != null;
        }

        /// <summary>
        /// Try to get the cvr number as a string value from the certificate, if such exists
        /// </summary>
        /// <param name="cvrNumberString">The resulting cvr string</param>
        /// <returns>Whether a cvr number string value could be parsed.</returns>
        public bool TryGetCvrNumberString(out string cvrNumberString) 
        {
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
        /// Try to get the cvr number as a string value from the certificate, if such exists
        /// </summary>
        /// <returns>Whether a cvr number string value could be parsed.</returns>
        public static bool TryGetCvrNumberString(CertificateSubject subject, out string cvrNumberString)
        {
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
        public static OcesCertificateType GetOcesCertificateType(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new NullArgumentException("certificate");
            }

            OcesCertificateType ocesCertificateType;
            try 
            {
                //The code is using the subject as identifier of the the oces type.
                ocesCertificateType =  GetFromSubject(certificate);
            }
            catch (Exception ex)
            {
                throw new FailedGetOcesCertificateTypeException(certificate, ex);
            }

            return ocesCertificateType;
        }

        /// <summary>
        /// Get the OCES certificate type from a given certificate subject.
        /// </summary>
        public static OcesCertificateType GetOcesCertificateType(CertificateSubject subject) 
        {
            if (subject == null)
            {
                throw new NullArgumentException("subject");
            }
             //The code is using the subject as identifier of the the oces type.
            OcesCertificateType ocesCertificateType = GetFromSubject(subject);

            return ocesCertificateType;
        }

        private static OcesCertificateType GetFromSubject(X509Certificate2 certificate) 
        {
            CertificateSubject subject = new CertificateSubject(certificate.Subject);
            return GetFromSubject(subject);
        }

        private static OcesCertificateType GetFromSubject(CertificateSubject subject)
        {
            OcesX509CertificateConfig config = ConfigurationHandler.GetConfigurationSection<OcesX509CertificateConfig>();
            OcesCertificateType ocesCertificateType = new OcesCertificateType();

            string ssn = subject.SerialNumberValue;
            if (string.IsNullOrEmpty(ssn))
            {
                ocesCertificateType = OcesCertificateType.NonOces;
            }
            else if (ssn.Contains(config.EmployeeCertificateSubjectKey.SubjectKeyString))
            {
                ocesCertificateType = OcesCertificateType.OcesEmployee;
            }
            else if (ssn.Contains(config.OrganizationCertificateSubjectKey.SubjectKeyString))
            {
                ocesCertificateType = OcesCertificateType.OcesOrganisation;
            }
            else if (ssn.Contains(config.PersonalCertificateSubjectKey.SubjectKeyString))
            {
                ocesCertificateType = OcesCertificateType.OcesPersonal;
            }
            else if (ssn.Contains(config.FunctionCertificateSubjetKey.SubjectKeyString))
            {
                ocesCertificateType = OcesCertificateType.OcesFunction;
            }
            else
            {
                ocesCertificateType = OcesCertificateType.NonOces;
            }

            return ocesCertificateType;
        }

        private void SetCertificateType() 
        {
            this.ocesCertificateType = GetOcesCertificateType(this.x509Certificate);
        }
    }
}