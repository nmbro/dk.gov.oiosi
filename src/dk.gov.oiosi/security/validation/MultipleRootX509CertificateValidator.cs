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
        /// <summary>
        /// Directory for root certificates, identified by Thumbprint.ToLowerInvariant() 
        /// </summary>
        private IDictionary<string, X509Certificate2> rootCertificateDirectory;

        private ILogger logger;

        /// <summary>
        /// Default constructor which makes it load configuration from
        /// app.config
        /// </summary>
        public MultipleRootX509CertificateValidator() 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.rootCertificateDirectory = new Dictionary<string, X509Certificate2>();
            RootCertificateCollectionConfig rootCertificateCollectionConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateCollectionConfig>();
            this.LoadRootCertificates(rootCertificateCollectionConfig);
        }

        public MultipleRootX509CertificateValidator(RootCertificateCollectionConfig rootCertificateCollectionConfig)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.rootCertificateDirectory = new Dictionary<string, X509Certificate2>();
            this.LoadRootCertificates(rootCertificateCollectionConfig);
        }

        public MultipleRootX509CertificateValidator(ICollection<X509Certificate2> rootCertificateCollectionConfig)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.rootCertificateDirectory = new Dictionary<string, X509Certificate2>();
            this.LoadRootCertificates(rootCertificateCollectionConfig);
        }

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
            // Check if the certificate path contain the root certificate
            bool isValid = this.IsValid(certificate, this.rootCertificateDirectory);

            return isValid;
        }

        public bool IsValid(X509Certificate2 certificate, IDictionary<string, X509Certificate2> rootCertificateDirectory)
        {
            bool isValid;

            if (certificate.NotAfter < DateTime.Now)
            {
                // certificate has expired
                isValid = false;
            }
            else if (certificate.NotBefore > DateTime.Now)
            {
                // yet valid
                isValid = false;
            }
            else
            {
                // validate the certificate path, and se if one of the
                // certificate in the cartificate path (certificate chain) is in
                // the list of valid root certificates
                isValid = this.IsCertificateChildOfRoot(certificate, rootCertificateDirectory);
            }

            return isValid;
        }

        public bool IsCertificateChildOfRoot(X509Certificate2 certificate, IDictionary<string, X509Certificate2> rootCertificateDirectory)
        {
            // valid until proved otherwhise
            bool isValid = true;
            X509Chain x509Chain = this.CreateChain(certificate);

            // Modified chain validation of the certificate. We are not interested in Ctl lists
            X509ChainStatus status;
            int index = 0;

            while (isValid && index < x509Chain.ChainStatus.Length)
            {

                status = x509Chain.ChainStatus[index];
                            
                switch (status.Status) 
                {
                    case X509ChainStatusFlags.CtlNotSignatureValid:
                    case X509ChainStatusFlags.CtlNotTimeValid:
                    case X509ChainStatusFlags.CtlNotValidForUsage:
                    case X509ChainStatusFlags.NoError:
                    case X509ChainStatusFlags.OfflineRevocation:
                    case X509ChainStatusFlags.RevocationStatusUnknown:
                    {
                            // so far, still valid
                            break;
                        }
                    case X509ChainStatusFlags.Cyclic:
                    case X509ChainStatusFlags.HasExcludedNameConstraint:
                    case X509ChainStatusFlags.HasNotDefinedNameConstraint:
                    case X509ChainStatusFlags.HasNotPermittedNameConstraint:
                    case X509ChainStatusFlags.HasNotSupportedNameConstraint:
                    case X509ChainStatusFlags.InvalidBasicConstraints:
                    case X509ChainStatusFlags.InvalidExtension:
                    case X509ChainStatusFlags.InvalidNameConstraints:
                    case X509ChainStatusFlags.InvalidPolicyConstraints:
                    case X509ChainStatusFlags.NoIssuanceChainPolicy:
                    case X509ChainStatusFlags.NotSignatureValid:
                    case X509ChainStatusFlags.NotTimeNested:
                    case X509ChainStatusFlags.NotTimeValid:
                    case X509ChainStatusFlags.NotValidForUsage:
                    case X509ChainStatusFlags.PartialChain:
                    case X509ChainStatusFlags.Revoked:
                    case X509ChainStatusFlags.UntrustedRoot:
                    {
                        isValid = false;
                        break;
                    }
                    default:
                        {
                            logger.Warn("The certificate chain.ChainStatus '" + status.Status + "' is not implemented.");
                            isValid = false;
                            break;
                        }
                }

                index++;
            }

            if (isValid)
            {
                // Check if the certificate has the default root certificate as its root
                string x509CertificateThumbprint = certificate.Thumbprint.ToLowerInvariant();
                if (rootCertificateDirectory.ContainsKey(x509CertificateThumbprint))
                {
                    // certificate is a root certificate - not valid
                    isValid = false;
                }
                else
                {
                    // Iterate though the chain, to validate if it contain a valid root vertificate
                    X509ChainElementCollection x509ChainElementCollection = x509Chain.ChainElements;
                    X509ChainElementEnumerator enumerator = x509ChainElementCollection.GetEnumerator();
                    X509ChainElement x509ChainElement;
                    X509Certificate2 x509Certificate2;

                    // At this point, the certificate is not valid, until a 
                    // it is proved that it has a valid root certificate
                    isValid = false;

                    while (isValid == false && enumerator.MoveNext())
                    {
                        x509ChainElement = enumerator.Current;
                        x509Certificate2 = x509ChainElement.Certificate;
                        x509CertificateThumbprint = x509Certificate2.Thumbprint.ToLowerInvariant();
                        if (rootCertificateDirectory.ContainsKey(x509CertificateThumbprint))
                        {
                            // The current chain element is in the trusted rootCertificateDirectory
                            isValid = true;

                            // now the loop will break, as we have found a trusted root certificate
                        }
                    }
                }
            }

            return isValid;
        }

        private void LoadRootCertificates(RootCertificateCollectionConfig rootCertificateCollectionConfig)
        {
            CertificateLoader certificateLoader = new CertificateLoader();
            X509Certificate2 loadedRootCertificate;

            foreach (RootCertificateLocation rootCertificateLocation in rootCertificateCollectionConfig.RootCertificateCollection)
            {
                loadedRootCertificate = certificateLoader.GetCertificateFromCertificateStoreInformation(rootCertificateLocation);
                this.rootCertificateDirectory.Add(loadedRootCertificate.Thumbprint.ToLowerInvariant(), loadedRootCertificate);
           }
        }

        private void LoadRootCertificates(ICollection<X509Certificate2> rootCertificateCollection)
        {
            foreach (X509Certificate2 x509Certificate2 in rootCertificateCollection)
            {
                this.rootCertificateDirectory.Add(x509Certificate2.Thumbprint.ToLowerInvariant(), x509Certificate2);
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
