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
 *   Christian Uldall Pedersen (christian.u.pedersen@accenture.com)
 *   
 */
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using dk.gov.oiosi.common.cache;
using Org.BouncyCastle.X509;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.security.revocation.crl
{
    /// <summary>
    /// Class for checking certificate revocation status against a CRL (Certificate Revocation List).
    /// 
    /// Makes use of a cache for storing CRLs
    /// </summary>
    public class CrlLookup : IRevocationLookup
    {
        /// <summary>
        /// Got cache for one day
        /// </summary>
        private ICache<Uri, CrlInstance> cache;

        private ILogger logger;

        /// <summary>
        /// Lockobject to ensure locking of the cache.
        /// </summary>
        private static object lockObject = new object();

        public CrlLookup()
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.cache = CacheFactory.Instance.CrlLookupCache;
        }

        #region IRevocationLookup Members
        
        /// <summary>
        /// Checks a certificate status in a CRL.
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>The RevocationResponse object that contains the result</returns>
        public RevocationResponse CheckCertificate(X509Certificate2 certificate) {

            /*
             * Assumptions:
             * - Certificate has an CRL Distribution Points extension value.
             * - An HTTP distribution point is present.
             */

            RevocationResponse response = new RevocationResponse();
            RevocationException innerException = null;

    	    if (certificate != null)
    	    {
    		    // Retrieve URL distribution points
    		    List<Uri> URLs = GetURLs(certificate);
        		
    		    foreach (Uri url in URLs)
    		    {
                    CrlInstance crl = this.GetInstance(url);
        			
    			    try 
    			    {
					    if(!crl.IsRevoked(certificate))
					    {
						    response.IsValid = true;
						    response.NextUpdate = crl.getNextUpdate();
						    return response;
					    }
					    else
					    {
                            response.IsValid = false;
                            return response;
					    }
				    }
                    catch (CheckCertificateRevokedUnexpectedException e) 
				    {
					    // There was an error in checking the certificate. Try the next url.
                        // implicit the certificate is true, until other whise proven invalid
                        innerException = e;
				    }
    		    }
            }

            // At this point, all checks has result in an exception. If any check has performed well,
            // the result was returned.
            // If any errors happen during CRL check, then abort the processing of the message.
            if (innerException != null)
            {
                throw innerException;
            }
            else
            {
                throw new CheckCertificateRevokedUnexpectedException(new Exception("Error during CRL lookup. Maybe certificate did not have any CRL DistPoints. Certificate: " + certificate));
            }
        }

        #endregion

        /// <summary>
        /// Gets a list of URLs from the specified certificate.
        /// </summary>
        /// <param name="cert">The certificate to find the URLs in.</param>
        /// <returns>A list of CRL URLs in the certificate</returns>
        private List<Uri> GetURLs(X509Certificate2 cert)
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

        private CrlInstance GetInstance(Uri url)
        {
            CrlInstance instance = new CrlInstance(url);
            instance = cache.TryAddValue(url, instance);
            return instance;
        }
    }
}
