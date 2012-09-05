using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1;

namespace dk.gov.oiosi.security.revocation.crl
{
    public class CertificateUtil
    {
        /// <summary>
        /// Gets a list of URLs from the specified certificate.
        /// </summary>
        /// <param name="cert">The certificate to find the URLs in.</param>
        /// <returns>A list of CRL URLs in the certificate</returns>
        public List<Uri> getCrlURLs(X509Certificate2 cert)
        {
            List<Uri> urls = new List<Uri>();

            foreach (System.Security.Cryptography.X509Certificates.X509Extension extension in cert.Extensions)
            {

                if (extension.Oid.Value == X509Extensions.CrlDistributionPoints.Id)
                {
                    // Retrieves the raw ASN1 data of the CRL Dist Points X509 extension, and wraps it in a container class
                    CrlDistPoint crldp = CrlDistPoint.GetInstance(Asn1Object.FromByteArray(extension.RawData));

                    DistributionPoint[] distPoints = crldp.GetDistributionPoints();

                    foreach (DistributionPoint dp in crldp.GetDistributionPoints())
                    {
                        // Only use the "General name" data in the distribution point entry.
                        GeneralNames gns = (GeneralNames)dp.DistributionPointName.Name;

                        foreach (GeneralName name in gns.GetNames())
                        {
                            // Only retrieve URLs
                            if (name.TagNo == GeneralName.UniformResourceIdentifier)
                            {
                                DerStringBase s = (DerStringBase)name.Name;
                                urls.Add(new Uri(s.GetString()));
                            }
                        }
                    }

                    // There is only one CRL list so faster to break.
                    break;
                }
            }

            return urls;
        }
    }
}
