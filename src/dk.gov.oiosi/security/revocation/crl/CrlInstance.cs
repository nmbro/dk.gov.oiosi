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
  *   Jacob Mogensen, mySupply ApS
  */

namespace dk.gov.oiosi.security.revocation.crl
{
    using System;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using Org.BouncyCastle.X509;
    using dk.gov.oiosi.logging;
    using org.bouncycastle.asn1.x509;
    using org.bouncycastle.asn1;
    using Org.BouncyCastle.Asn1;

    /// <summary>
    /// Class used for storing CRLs retrieved from URL's in X509 certificates
    /// </summary>
    public class CrlInstance
    {
        private readonly X509CrlParser crlParser;
        private X509Crl data;
        private readonly Uri url;

        private readonly ReaderWriterLockSlim rwl = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private readonly X509CertificateParser cp = new X509CertificateParser();

        private ILogger logger;

        /// <summary>
        /// Creates a new CRLInstance instance.
        /// </summary>
        /// <param name="url">The URL corresponding to the CRL.</param>
        public CrlInstance(Uri url)
        {
            this.crlParser = new X509CrlParser();
            this.data = null;
            this.url = url;
            this.logger = LoggerFactory.Create(this.GetType());
        }

        /// <summary>
        /// Creates a new CRLInstance instance.
        /// </summary>
        /// <param name="crlParser">A factory used for generating X509CRL class's from a stream.</param>
        /// <param name="url">The URL corresponding to the CRL.</param>
        public CrlInstance(X509CrlParser crlParser, Uri url)
        {
            this.crlParser = crlParser;
            this.data = null;
            this.url = url;
        }

        /// <summary>
        /// Checks whether the given certificate cert is revoked.
        /// </summary>
        /// <param name="cert">The certificate to check whether revoked or not</param>
        /// <returns>Returns true if revoked, false otherwise.</returns>
        public bool IsRevoked(X509Certificate2 cert)
        {
            // Looks the data for reading.
            rwl.EnterReadLock();
            try
            {
                if (!cacheValid())
                {
                    // Upgrades lock since data is not valid.
                    rwl.ExitReadLock();
                    rwl.EnterWriteLock();
                    try
                    {
                        if (!cacheValid()) // Recheck, since another thread might have updated the cache.
                        {
                            upgradeData();
                        }
                    }
                    finally
                    {
                        // Downgrade lock
                        rwl.EnterReadLock();
                        rwl.ExitWriteLock();
                    }
                }

                // Reads the data and unlocks.
                Org.BouncyCastle.X509.X509Certificate certificateToValidate = cp.ReadCertificate(cert.RawData);
                bool isRevoked = data.IsRevoked(certificateToValidate);
                
                return isRevoked;
            }
            finally
            {
                rwl.ExitReadLock();
            }
        }

        /// <summary>
        /// Downloads a new CRL from the URL stored in the class.
        /// </summary>
        private void upgradeData()
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Only download .crl file if it is present on the server.
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();

                    if (stream == null)
                    {
                        this.logger.Warn("The stream is null.");
                    }

                    this.logger.Debug("Start 'crlParser.ReadCrl(stream)'");
                   
                    
                    // Downloads the .crl file into an X509CRL object.
                    this.data = crlParser.ReadCrl(stream);

                    this.logger.Debug("Finish with 'crlParser.ReadCrl(stream)'");

                    DateTime f = data.NextUpdate.Value;
                    stream.Close();

                    return; // Everything went well.
                }

                throw new CheckCertificateRevokedUnexpectedException(new Exception("CRL could not be downloaded: " + response.StatusDescription));
            }
            catch (IOException e)
            {
                // creation a hollow X509Crl, that is valid only for very short time
                // to prevent that the list is tried downloaded many times
                this.data = new X509CrlEmptyList();

                // Could not download new crl
                throw new CheckCertificateRevokedUnexpectedException(e);
                
            }
        }

        /// <summary>
        /// Checks whether the CRL is still valid.
        /// </summary>
        /// <returns>Returns true if CRL is still valid, false otherwise.</returns>
        private bool cacheValid()
        {
            bool result;
            if (data == null)
            {
                // data invalid, cache invalid
                result = false;
            }
            else
            {
                // The next update is defined in utc time.
                // So now is also in utc time
                DateTime now = DateTime.UtcNow;
                DateTime nextUpdateUTCTime = data.NextUpdate.Value;

                if (nextUpdateUTCTime > now)
                {
                    // next update is in the future, so cache version is valid
                    result = true;
                }
                else
                { 
                    // next update is in the past, so cache version is invalid
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// The NextUpdate value from the CRL
        /// </summary>
        /// <returns>The NextUpdate value from the CRL</returns>
        public DateTime getNextUpdate()
        {
            DateTime nextUpdate;

            rwl.EnterUpgradeableReadLock();
            try
            {
                if (data != null)
                {
                    nextUpdate = data.NextUpdate.Value;
                }
                else
                {
                    nextUpdate = new DateTime(0);
                }
            }
            finally
            {
                rwl.ExitUpgradeableReadLock();
            }

            return nextUpdate;
        }
    }
}
