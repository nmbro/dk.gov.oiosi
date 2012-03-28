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

namespace dk.gov.oiosi.security.validation
{

    /// <summary>
    /// Class to validate X509 certificates
    /// </summary>
    public class CertificateValidator
    {
        private CertificateValidator()
        { }

        /// <summary>
        /// Attempts to validate the certificate. If the certificate is invalid an exception is thrown.
        /// Checks that all certificates in the chain are without flags marking them as non-valid,
        /// and checks expiration and activation dates for the top-level certificate.
        /// It only validates what can be done locally without the CRL list to avoid
        /// dependency on downloading the CRL to validate a certificate.
        /// </summary>
        /// <remarks>
        /// To check whether the certificate is trusted use the Ocsp module instead.
        /// </remarks>
        /// <exception cref="CertificateFailedChainValidationException"></exception>
        /// <param name="certificate">The certificate to be validated</param>
        public static void ValidateCertificate(X509Certificate2 certificate)
        {
            // first we check the activation and expire date - those cast the most specifict errors
            this.CheckCertificateActivated(certificate);
            this.CheckCertificateExpired(certificate);

            X509Chain chain = this.CreateChain(certificate);

            //Modified chain validation of the certificate. We are not interested in Ctl lists
            foreach (X509ChainStatus status in chain.ChainStatus)
            {
                switch (status.Status)
                {
                    case X509ChainStatusFlags.CtlNotSignatureValid:
                    case X509ChainStatusFlags.CtlNotTimeValid:
                    case X509ChainStatusFlags.CtlNotValidForUsage:
                    case X509ChainStatusFlags.NoError:
                    case X509ChainStatusFlags.RevocationStatusUnknown:
                    case X509ChainStatusFlags.OfflineRevocation:
                        break;
                    default:
                        throw new CertificateFailedChainValidationException(status);
                }
            }
        }

        /// <summary>
        /// Attempts to validate the certificate. If the certificate is invalid an exception is thrown.
        /// Checks that all certificates in the chain are without flags marking them as non-valid,
        /// checks expiration and activation dates for the top-level certificate, and that the specified
        /// root certificate is in the chain.
        /// It only validates what can be done locally without the CRL list to avoid
        /// dependency on downloading the CRL to validate a certificate.
        /// </summary>
        /// <remarks>
        /// To check whether the certificate is trusted use the Ocsp module instead.
        /// </remarks>
        /// <exception cref="CertificateFailedChainValidationException"></exception>
        /// <param name="certificate">The certificate to be validated</param>
        /// <param name="rootCertificate">The root certificate of the certificate. If not null, checks
        /// that the root certificate exists in the certificate chain.</param>
        public static void ValidateCertificate(X509Certificate2 certificate, X509Certificate2 rootCertificate)
        {
            this.CheckCertificateActivated(certificate);
            this.CheckCertificateExpired(certificate);

            X509Chain chain = this.CreateChain(certificate);

            //Modified chain validation of the certificate. We are not interested in Ctl lists
            foreach (X509ChainStatus status in chain.ChainStatus)
            {
                switch (status.Status)
                {
                    case X509ChainStatusFlags.CtlNotSignatureValid:
                    case X509ChainStatusFlags.CtlNotTimeValid:
                    case X509ChainStatusFlags.CtlNotValidForUsage:
                    case X509ChainStatusFlags.NoError:
                    case X509ChainStatusFlags.RevocationStatusUnknown:
                    case X509ChainStatusFlags.OfflineRevocation:
                        break;
                    default:
                        throw new CertificateFailedChainValidationException(status);
                }
            }

            // Check if the certificate has the default root certificate as its root
            bool rootIsInChain = false;
            string rootThumbprint = rootCertificate.Thumbprint.ToLower();

            if (certificate.Thumbprint.ToLower() != rootThumbprint)
            {
                foreach (X509ChainElement chainElem in chain.ChainElements)
                {
                    if (chainElem.Certificate.Thumbprint.ToLower() == rootThumbprint)
                    {
                        rootIsInChain = true;
                        break;
                    }
                }
            }
            else
            {
                throw new CertificateFailedChainValidationException("The root certificate and certificate must be different");
            }

            if (rootIsInChain == false)
            {
                throw new CertificateFailedChainValidationException("The specified root certificate was not part of the certificate chain");
            }


        }

        /// <summary>
        /// Creates the chain that chain vailidates a certificate. 
        /// If the certificate is in incorrect format an exception is thrown.
        /// NOTE: revocation checking is disabled. To check certificate revocation
        /// information, perform a separate call against OCSP.
        /// </summary>
        /// <param name="certificate">The certificate to create a chain from</param>
        /// <exception cref="CertificateNotInCorrectFormatException">Thrown if there are any chain validation errors</exception>
        /// <returns>Returns the X509 chain object</returns>
        private static X509Chain CreateChain(X509Certificate2 certificate)
        {
            try
            {
                X509Chain chain = new X509Chain();
                chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                chain.Build(certificate);
                return chain;
            }
            catch (Exception e)
            {
                throw new CertificateNotInCorrectFormatException(e);
            }
        }


        /// <summary>
        /// Checks if the certificate is activated
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        public static void CheckCertificateActivated(X509Certificate2 certificate)
        {
            if (certificate.NotBefore > DateTime.Now)
            {
                throw new CertificateNotActiveException(certificate.NotBefore);
            }
        }

        /// <summary>
        /// Checks if the certificate is expired
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        public static void CheckCertificateExpired(X509Certificate2 certificate)
        {
            if (certificate.NotAfter < DateTime.Now)
            {
                throw new CertificateExpiredException(certificate.NotAfter);
            }
        }
    }
}