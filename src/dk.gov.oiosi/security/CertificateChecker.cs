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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using dk.gov.oiosi.security.validation;
using dk.gov.oiosi.security.ocsp;
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.security {

    /// <summary>
    /// Methods for performing various certificate checks, such as revocation and
    /// root certificate relations
    /// </summary>
    public class CertificateChecker {
        private X509Certificate2 _defaultOCESrootCertificate;
        private OcspLookup _ocsp;

        /// <summary>
        /// Instantiates CertificateChecker
        /// </summary>
        /// <param name="ocspLookupConfiguration">The ocsp server configuration</param>
        /// <param name="defaultRootCertificate">default OCES root certificate</param>
        public CertificateChecker(OcspConfig ocspLookupConfiguration, X509Certificate2 defaultRootCertificate) {
            try {
                _defaultOCESrootCertificate = defaultRootCertificate;

                //Initializes the component, that will do the actual ocsp lookup
                _ocsp = new OcspLookup(ocspLookupConfiguration, defaultRootCertificate);
            } catch (UriFormatException) {
                throw;
            } catch (ArgumentNullException) {
                throw;
            } catch (OverflowException) {
                throw;
            } catch (FormatException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Checks a certificate, with default rootcertificate.
        /// All fields of the CertificateCheckResult structure are initialized as "false".
        /// Certificate checking may end before all checks have been performed, if individual
        /// checks fail. In that case, only the fields of the CertificateCheckResult structure that
        /// corresponds to checks already performed are valid.
        /// </summary>
        /// <param name="certificate">certificate to check</param>
        /// <param name="rootCertificate">a given rootcertificate</param>
        /// <returns>The object that contains the result. Note that all fields of the CertificateCheckResult 
        /// structure are initialized as "false". Certificate checking may end before all checks have 
        /// been performed, if individual checks fail. In that case, only the fields of the 
        /// CertificateCheckResult structure that
        /// corresponds to checks already performed are valid.</returns>
        public CertificateCheckResult CheckCertificate(X509Certificate2 certificate, X509Certificate2 rootCertificate) {
            CertificateCheckResult result = new CertificateCheckResult();

            try {
                //1. that the certificate has the default rootcertificate as root.
                // Also checks that the cert is not expired or not yet activated.
                CheckCertificateChain(certificate, result);

                if (!result.CertificateActivated 
                    || !result.CertificateValid 
                    || !result.RootCertificateAsRoot
                ) {
                    result.AllTestsPassed = false;
                    return result;
                }

                //2. that the rootcertificate is not expired or not activated           
                if (rootCertificate == null) {
                    CheckRootCertificateValidation(_defaultOCESrootCertificate, result);
                    CheckRootCertificateActivated(_defaultOCESrootCertificate, result);
                } else {
                    CheckRootCertificateValidation(rootCertificate, result);
                    CheckRootCertificateActivated(rootCertificate, result);
                }

                if (!result.RootCertificateValid || !result.RootCertificateActivated) {
                    result.AllTestsPassed = false;
                    return result;
                }

                //4. check if the certificate is revoked async
                OcspResponse response = CheckCertificateRevocation(certificate);
                if (!response.IsValid) {
                    result.AllTestsPassed = false;
                    result.CertificateRevoked = true;
                    return result;
                }

                //5. check certificatetype
                CheckCertificateType(certificate, result);

                if (result.CertificateActivated && !result.CertificateRevoked && result.CertificateValid
                    && result.RootCertificateActivated && result.RootCertificateAsRoot && result.RootCertificateValid) {
                    result.AllTestsPassed = true;
                }
            } catch (ArgumentNullException) {
                throw;
            } catch (OverflowException) {
                throw;
            } catch (FormatException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (CheckCertificateOcspUnexpectedException) {
                throw;
            } catch (CertificateRevokedTimeoutException) {
                throw;
            } catch (Exception) {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Checks a certificate, with a given rootcertificate.
        /// </summary>
        /// <param name="certificate">certificate to check</param>
        /// <returns></returns>
        public CertificateCheckResult CheckCertificate(X509Certificate2 certificate) {
            return CheckCertificate(certificate, null);
        }

        /// <summary>
        /// Performs a certificate chain validation
        /// </summary>
        /// <param name="certificate">the certificate to validate</param>
        /// <param name="result">the object to store the result</param>
        /// <exception cref="CheckCertificateChainUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        private void CheckCertificateChain(X509Certificate2 certificate, CertificateCheckResult result) {
            try {
                CertificateValidator.ValidateCertificate(certificate, _defaultOCESrootCertificate);
                result.RootCertificateAsRoot = true;
                result.CertificateActivated = true;
                result.CertificateValid = true;
            } catch (CertificateFailedChainValidationException) {
                result.RootCertificateAsRoot = false;
            } catch (CertificateNotActiveException) {
                result.CertificateActivated = false;
            } catch (CertificateExpiredException) {
                result.CertificateValid = false;
            } catch (ArgumentNullException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (Exception e) {
                throw new CheckCertificateChainUnexpectedException(e);
            }
        }

        /// <summary>
        /// Checks if the rootcertificate is activated
        /// </summary>
        /// <param name="root">The rootcertificate to check</param>
        /// <param name="result">The object to store the result of the check</param>
        /// <exception cref="CheckRootCertificateActivatedUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        private void CheckRootCertificateActivated(X509Certificate2 root, CertificateCheckResult result) {
            try {
                if (root.NotBefore.CompareTo(DateTime.Now) < 0 ||
                    root.NotBefore.CompareTo(DateTime.Now) == 0) {
                    result.RootCertificateActivated = true;
                }
            } catch (ArgumentNullException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (Exception e) {
                throw new CheckRootCertificateActivatedUnexpectedException(e);
            }
        }

        /// <summary>
        /// Checks if the rootcertificate is valid
        /// </summary>
        /// <param name="root">The rootcertificate to check</param>
        /// <param name="result">The object to store the result of the check</param>
        /// <exception cref="CheckRootCertificateValidUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        private void CheckRootCertificateValidation(X509Certificate2 root, CertificateCheckResult result) {
            try {
                if (root.NotAfter.CompareTo(DateTime.Now) > 0 ||
                    root.NotAfter.CompareTo(DateTime.Now) == 0) {
                    result.RootCertificateValid = true;
                }
            } catch (ArgumentNullException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (Exception e) {
                throw new CheckRootCertificateValidUnexpectedException(e);
            }
        }

        /// <summary>
        /// Checks if the certificate is activated
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>boolean stating whether the certificate is activated (true) or not (false)</returns>
        /// <exception cref="CheckCertificateActivatedUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        public bool CheckCertificateActivated(X509Certificate2 certificate) {
            bool isActive = false;
            try {
                CertificateValidator.CheckCertificateActivated(certificate);
                isActive = true;
            } catch (CertificateNotActiveException) {
                isActive = false;
            } catch (ArgumentNullException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (Exception e) {
                throw new CheckCertificateActivatedUnexpectedException(e);
            }
            return isActive;
        }

        /// <summary>
        /// Checks if the certificate is valid
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>boolean stating whether the certificate is valid (true) or not (false)</returns>
        /// <exception cref="CheckCertificateValidUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        public bool CheckCertificateValidation(X509Certificate2 certificate) {
            bool isValid = false;
            try {
                CertificateValidator.CheckCertificateExpired(certificate);
                isValid = true;
            } catch (CertificateExpiredException) {
                isValid = false;
            } catch (ArgumentNullException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (Exception e) {
                throw new CheckCertificateValidUnexpectedException(e);
            }
            return isValid;
        }

        /// <summary>
        /// Check if the certificate is revoced against ocsp server
        /// </summary>
        /// <param name="certificate">the certificate to check</param>
        /// <returns>OcspResponse object to store the result</returns>
        /// <exception cref="CheckCertificateOcspUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        /// <exception cref="CertificateRevokedTimeoutException">This exception is thrown, if the call to Ocsp server takes longer time than the allowed timeout</exception>
        /// <exception cref="CheckCertificateRevokedUnexpectedException">Thrown if an unexpected error occured</exception>
        private OcspResponse CheckCertificateRevocation(X509Certificate2 certificate) {
            OcspResponse validityCheck = null;
            try {
                // Checks the certificate
                validityCheck = _ocsp.CheckCertificate(certificate);
            } catch (ArgumentNullException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (ArithmeticException) {
                throw;
            } catch (CheckCertificateOcspUnexpectedException) {
                throw;
            } catch (CertificateRevokedTimeoutException) {
                throw;
            } catch (Exception e) {
                throw new CheckCertificateRevokedUnexpectedException(e);
            }
            return validityCheck;
        }

        /// <summary>
        /// Returns the type of OCES certificate, e.g. "company" or "employee" certificates
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>Returns the OCES certificate type</returns>
        public static OcesCertificateType GetCertificateType(X509Certificate certificate) {
            OcesCertificateType certType = OcesCertificateType.NonOces;

            try {
                //no check included for LRA and Device
                string subjectString = certificate.Subject;
                if (subjectString.Contains("PID")) {
                    certType = OcesCertificateType.OcesPersonal;
                } else if (subjectString.Contains("CVR")) {
                    if (subjectString.Contains("RID")) {
                        certType = OcesCertificateType.OcesEmployee;
                    } else if (subjectString.Contains("UID")) {
                        certType = OcesCertificateType.OcesOrganisation;
                    }
                }
            } catch (ArgumentNullException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (Exception e) {
                throw new CheckCertificateTypeUnexpectedException(e);
            }
            return certType;
        }

        /// <summary>
        /// Sets the certificate type
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <param name="result">The result object to store the result</param>
        private void CheckCertificateType(X509Certificate2 certificate, CertificateCheckResult result) {
            try {
                result.CertificateType = GetCertificateType(certificate);
            } catch (ArgumentNullException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (CheckCertificateTypeUnexpectedException) {
                throw;
            } catch (Exception) {
                throw;
            }
        }
    }
}