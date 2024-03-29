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
 *   Dennis S�gaard (dennis.j.sogaard@accenture.com)
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

        /// <summary>
        /// The logger
        /// </summary>
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
        public RevocationResponse CheckCertificate(X509Certificate2 certificate)
        {

            /*
             * Assumptions:
             * - Certificate has an CRL Distribution Points extension value.
             * - An HTTP distribution point is present.
             */

            // create RevocationResponse and set default values
            RevocationResponse response = new RevocationResponse();
            response.IsValid = true;
            response.NextUpdate = DateTime.MinValue;

            if (certificate != null)
            {
                X509Chain x509Chain = new X509Chain();
                x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                x509Chain.Build(certificate);

                // Iterate though the chain, to get the certificate list
                X509ChainElementCollection x509ChainElementCollection = x509Chain.ChainElements;
                X509ChainElementEnumerator enumerator = x509ChainElementCollection.GetEnumerator();
                X509ChainElement x509ChainElement;
                X509Certificate2 x509Certificate2 = null;
                IDictionary<string, X509Certificate2> map = new Dictionary<string, X509Certificate2>();
                IList<X509Certificate2> list = new List<X509Certificate2>();

                while (enumerator.MoveNext())
                {
                    x509ChainElement = enumerator.Current;
                    x509Certificate2 = x509ChainElement.Certificate;
                    list.Add(x509Certificate2);
                }

                // now we have a list of the certificate chain
                // list[0] -> the function certificate
                // list[0 .. lsit.Count] -> middel certificates
                //   oces1 : none middle certificate exist
                //   oces2 : one middle certificate exist
                // list[list.Count] - > root certificate
                // we needed to validate all certificates, except the root certificates
                // The question wheather the root certificates is trusted, is validated in MultipleRootX509CertificateValidator
                // However - In the case where the root certificate is not installed,
                // the chain list will only be 1 length, so no validation is perfored at all.


                int index = 0;
                //bool chainValid = true;
                while (index < (list.Count -1) && response.IsValid == true)
                {
                    // this.logger.Info("CRL validation the certificate: " + list[index].Subject);
                    // Retrieve URL distribution points
                    List<Uri> URLs = this.GetURLs(list[index]);

                    // The list should only contain one element
                    // so we are only interesting in the first CRL list
                    if (URLs.Count > 0)
                    {
                        Uri url = URLs[0];
                        CrlInstance crl = this.GetInstance(url);

                        try
                        {
                            if (!crl.IsRevoked(certificate))
                            {
                                // so the certificate is not revoked.
                                // remember, that the issueing certificate could be revoked.
                                // So the next update must be the earlist of the all
                                response.IsValid = true;
                                if (response.NextUpdate == DateTime.MinValue)
                                {
                                    response.NextUpdate = crl.getNextUpdate();
                                }
                                else if (response.NextUpdate < crl.getNextUpdate())
                                {
                                    // no new update
                                    // the already registrated 'NextUpdate' is before the crl.getNextUpdate
                                }
                                else
                                {
                                    // new update time
                                    // The already registrated 'NextUpdate', is greater (futher in the future) then crl.getNextUpdate
                                    // so we use the crl.getNextUpdate as next update
                                    response.NextUpdate = crl.getNextUpdate();
                                }
                            }
                            else
                            {
                                response.IsValid = false;
                            }
                        }
                        catch (CheckCertificateRevokedUnexpectedException exception)
                        {
                            // could not validate the certificate - so i don't trust it
                            response.Exception = exception;
                            response.IsValid = false;

                        }
                    }
                    else
                    {
                        // url server not identified, so we don't trust this certificate
                        response.IsValid = false;
                    }

                    // increase the index, to check the next certificate
                    index++;
                }

                // all the certificate in the chain is now checked.
                if (response.IsValid == true)
                {
                    response.RevocationCheckStatus = RevocationCheckStatus.AllChecksPassed;
                }
                else
                {
                    response.RevocationCheckStatus = RevocationCheckStatus.CertificateRevoked;
                }

                x509Chain.Reset();
            }
            else
            {
                response.IsValid = false;
                response.Exception = new CheckCertificateRevokedUnexpectedException(new Exception("Error during CRL lookup. The certificate is null"));//did not have any CRL DistPoints. Certificate: " + certificate));
                response.RevocationCheckStatus = RevocationCheckStatus.UnknownIssue;
            }

            return response;
        }

        #endregion

        /// <summary>
        /// Gets a list of URLs from the specified certificate.
        /// </summary>
        /// <param name="cert">The certificate to find the URLs in.</param>
        /// <returns>A list of CRL URLs in the certificate</returns>
        private List<Uri> GetURLs(X509Certificate2 cert)
        {
            CertificateUtil util = new CertificateUtil();
            List<Uri> urls = util.getCrlURLs(cert);
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
