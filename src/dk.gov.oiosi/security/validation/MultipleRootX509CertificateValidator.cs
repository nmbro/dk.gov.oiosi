using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.security.lookup;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens;
using dk.gov.oiosi.security.validation.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.security.validation {
    
    public class MultipleRootX509CertificateValidator : System.IdentityModel.Selectors.X509CertificateValidator
    {

        private IList<X509Certificate2> trustedRootCertificates = new List<X509Certificate2>();
        private ILogger logger;

        /// <summary>
        /// Default constructor which makes it load configuration from
        /// app.config
        /// </summary>
        public MultipleRootX509CertificateValidator() 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.trustedRootCertificates = new List<X509Certificate2>();
            RootCertificateCollectionConfig rootCertificateCollectionConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateCollectionConfig>();
            this.LoadRootCertificates(rootCertificateCollectionConfig);
        }

        public MultipleRootX509CertificateValidator(RootCertificateCollectionConfig rootCertificateCollectionConfig)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.trustedRootCertificates = new List<X509Certificate2>();
            this.LoadRootCertificates(rootCertificateCollectionConfig);
        }

        public MultipleRootX509CertificateValidator(ICollection<X509Certificate2> rootCertificateCollectionConfig)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.trustedRootCertificates = new List<X509Certificate2>();
            this.LoadRootCertificates(rootCertificateCollectionConfig);
        }

        /*public MultipleRootX509CertificateValidator(IEnumerable<X509Certificate2> trustedRootCertificates) {
            this.trustedRootCertificates = trustedRootCertificates;
        }*/

        public override void Validate(X509Certificate2 certificate)
        {
            if (this.IsValid(certificate))
            {
                // certificate valid, noting more to do
            }
            else
            {
                throw new SecurityTokenValidationException("The client certificate is invalid.");
            }
        }

        /// <summary>
        /// validate the certificate, against the defined root certificates
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public bool IsValid(X509Certificate2 certificate)
        {
            bool valid = false;
            int index = 0;

            while (index < this.trustedRootCertificates.Count && valid == false)
            {
                if (this.IsValid(certificate, trustedRootCertificates[index]))
                {
                    // certificate and root certificate match
                    valid = true;
                }
                else
                {
                    // no match, chect next root certificate
                    valid = false;
                }

                index++;
            }

            return valid;
        }

        public bool IsValid(X509Certificate2 certificate, X509Certificate2 rootCertificate)
        {
            bool valid;

            if (certificate.NotAfter < DateTime.Now)
            {
                // certificate has expired
                valid = false;
            }
            else if (certificate.NotBefore > DateTime.Now)
            {
                // yet valid
                valid = false;
            }
            else if (!this.IsCertificateChildOfRoot(certificate, rootCertificate))
            {
                // not correct root certificate
                valid = false;
            }
            else 
            {
                // certificate valid
                valid = true;
            }

            return valid;
        }

        public bool IsCertificateChildOfRoot(X509Certificate2 certificate, X509Certificate2 rootCertificate)
        {
            // valid until proved otherwhise
            bool valid = true;
            X509Chain chain = this.CreateChain(certificate);

            //Modified chain validation of the certificate. We are not interested in Ctl lists
            X509ChainStatus status;
            int index = 0;
            while(valid && index < chain.ChainStatus.Length)
            {
            
                status = chain.ChainStatus[index];
                            
                switch (status.Status) 
                {
                    case X509ChainStatusFlags.CtlNotSignatureValid:
                    case X509ChainStatusFlags.CtlNotTimeValid:
                    case X509ChainStatusFlags.CtlNotValidForUsage:
                    case X509ChainStatusFlags.NoError:
                    case X509ChainStatusFlags.RevocationStatusUnknown:
                    case X509ChainStatusFlags.OfflineRevocation:
                        {
                            // so far, still valid
                            break;
                        }
                    default:
                        {
                            logger.Warn("The certificate chain.ChainStatus '" + status.Status + "' is not implemented.");
                            valid = false;
                            break;
                        }
                }

                index ++;
            }

            bool rootIsInChain = false;
            if (valid)
            {
                // Check if the certificate has the default root certificate as its root
                string rootThumbprint = rootCertificate.Thumbprint.ToLower();

                if (certificate.Thumbprint.ToLower() == rootThumbprint)
                {
                    // certificate is a root certificate - not valid
                    rootIsInChain = false;
                }
                else
                {
                    // check all the chain in the certificate to se if one of the chain is the root certificate
                    index = 0;
                    X509ChainElement chainElem;

                    while (rootIsInChain == false && index < chain.ChainElements.Count)
                    {
                        chainElem = chain.ChainElements[index];
                        if (chainElem.Certificate.Thumbprint.ToLower() == rootThumbprint)
                        {
                            // root certificate is in chain
                            rootIsInChain = true;
                        }
                        else
                        {
                            // root element not found yet - try the next dhain
                            index++;
                        }                        
                    }
                }
            }

            return rootIsInChain;
        }

        private void LoadRootCertificates(RootCertificateCollectionConfig rootCertificateCollectionConfig)
        {
            CertificateLoader certificateLoader = new CertificateLoader();
            X509Certificate2 loadedRootCertificate;

            foreach (RootCertificateLocation rootCertificateLocation in rootCertificateCollectionConfig.RootCertificateCollection)
            {
                loadedRootCertificate = certificateLoader.GetCertificateFromCertificateStoreInformation(rootCertificateLocation);
                this.trustedRootCertificates.Add(loadedRootCertificate);
           }
        }

        private void LoadRootCertificates(ICollection<X509Certificate2> rootCertificateCollection)
        {
            foreach (X509Certificate2 x509Certificate2 in rootCertificateCollection)
            {
                this.trustedRootCertificates.Add(x509Certificate2);
            }
        }

        private X509Chain CreateChain(X509Certificate2 certificate) 
        {
            X509Chain chain;
            try
            {
                chain = new X509Chain();
                chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                chain.Build(certificate);
            }
            catch (Exception e)
            {
                throw new CertificateNotInCorrectFormatException(e);
            }

            return chain;
        }
    }
}
