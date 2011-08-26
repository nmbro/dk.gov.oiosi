using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.security.lookup;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens;
using dk.gov.oiosi.security.validation.configuration;

namespace dk.gov.oiosi.security.validation {
    
    public class MultipleRootX509CertificateValidator : System.IdentityModel.Selectors.X509CertificateValidator {
        private IEnumerable<X509Certificate2> trustedRootCertificates;

        /// <summary>
        /// Default constructor which makes it load configuration from
        /// app.config
        /// </summary>
        public MultipleRootX509CertificateValidator() {
            IMultipleRootX509CertificateValidatorConfiguration configuration = (IMultipleRootX509CertificateValidatorConfiguration)ConfigurationManager.GetSection(MultipleRootX509CertificateValidatorAppConfiguration.MultipleRootX509CertificateValidatorAppConfigurationName);
            this.LoadRootCertificates(configuration);
        }

        public MultipleRootX509CertificateValidator(IMultipleRootX509CertificateValidatorConfiguration configuration) {
            this.LoadRootCertificates(configuration);
        }

        public MultipleRootX509CertificateValidator(IEnumerable<X509Certificate2> trustedRootCertificates) {
            this.trustedRootCertificates = trustedRootCertificates;
        }

        public override void Validate(X509Certificate2 certificate) {
            foreach (var rootCertificate in trustedRootCertificates) {
                if (IsValid(certificate, rootCertificate)) return;
            }
            throw new SecurityTokenValidationException("The client certificate is invalid.");
        }

        public bool IsValid(X509Certificate2 certificate, X509Certificate2 rootCertificate) {
            if (!IsCertificateChildOfRoot(certificate, rootCertificate)) return false;
            if (certificate.NotAfter < DateTime.Now) return false;
            if (certificate.NotBefore > DateTime.Now) return false;
            return true;
        }

        public bool IsCertificateChildOfRoot(X509Certificate2 certificate, X509Certificate2 rootCertificate) {
            X509Chain chain = CreateChain(certificate);

            //Modified chain validation of the certificate. We are not interested in Ctl lists
            foreach (X509ChainStatus status in chain.ChainStatus) {
                switch (status.Status) {
                    case X509ChainStatusFlags.CtlNotSignatureValid:
                    case X509ChainStatusFlags.CtlNotTimeValid:
                    case X509ChainStatusFlags.CtlNotValidForUsage:
                    case X509ChainStatusFlags.NoError:
                    case X509ChainStatusFlags.RevocationStatusUnknown:
                    case X509ChainStatusFlags.OfflineRevocation:
                        break;
                    default:
                        //TODO: log what the status problem was
                        return false;
                }
            }

            // Check if the certificate has the default root certificate as its root
            bool rootIsInChain = false;
            string rootThumbprint = rootCertificate.Thumbprint.ToLower();

            if (certificate.Thumbprint.ToLower() == rootThumbprint) return false;

            foreach (X509ChainElement chainElem in chain.ChainElements) {
                if (chainElem.Certificate.Thumbprint.ToLower() == rootThumbprint) {
                    rootIsInChain = true;
                    break;
                }
            }

            return rootIsInChain;
        }

        private void LoadRootCertificates(IMultipleRootX509CertificateValidatorConfiguration configuration) {
            var loadedCertificates = new List<X509Certificate2>();

            if (configuration.TrustedRootCertificates == null) this.trustedRootCertificates = loadedCertificates;

            foreach (var storeLocation in configuration.TrustedRootCertificates)
            {
                var rootCertificate = CertificateLoader.GetCertificateFromCertificateStoreInformation(storeLocation);
                loadedCertificates.Add(rootCertificate);
            }
            this.trustedRootCertificates = loadedCertificates;
        }

        private X509Chain CreateChain(X509Certificate2 certificate) {
            try {
                X509Chain chain = new X509Chain();
                chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                chain.Build(certificate);
                return chain;
            }
            catch (Exception e) {
                throw new CertificateNotInCorrectFormatException(e);
            }
        }
    }
}
