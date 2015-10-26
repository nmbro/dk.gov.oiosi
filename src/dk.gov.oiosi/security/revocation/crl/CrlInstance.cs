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
    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Utilities.Collections;
using System.Text;

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
        private DateTime earlistNextUpdate = DateTime.MinValue;

        /// <summary>
        /// Creates a new CRLInstance instance.
        /// </summary>
        /// <param name="url">The URL corresponding to the CRL.</param>
        public CrlInstance(Uri url)
        {
            this.logger = LoggerFactory.Create(this);
            this.crlParser = new X509CrlParser();
            this.data = null;
            this.url = url;            
        }

        /// <summary>
        /// Creates a new CRLInstance instance.
        /// </summary>
        /// <param name="crlParser">A factory used for generating X509CRL class's from a stream.</param>
        /// <param name="url">The URL corresponding to the CRL.</param>
        public CrlInstance(X509CrlParser crlParser, Uri url)
        {
            this.logger = LoggerFactory.Create(this);
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
            bool isRevoked;
            if (this.logger.IsDebugEnabled)
            {
                this.logger.Debug(string.Format("Checking certificate '{0}'. containing CRL url '{1}'.", cert.ToString(), url.ToString()));
            }

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
                            // Only update if earlistNextUpdate is reached
                            // pervent the update tast to be performed and faileds many times, if the server is offline
                            if (DateTime.Now > this.earlistNextUpdate)
                            {                                
                                this.upgradeData();
                                this.earlistNextUpdate = DateTime.MinValue;
                            }
                        }
                    }
                    catch
                    {
                        // update failed
                        // prevent that the update tast should run again, rigth now
                        // if the server is offline, give it a seconds to come online again
                        this.earlistNextUpdate = DateTime.Now.AddMinutes(5);
                    }
                    finally
                    {
                        // Downgrade lock
                        rwl.EnterReadLock();
                        rwl.ExitWriteLock();
                    }
                }

                // Reads the data and unlocks.
                Org.BouncyCastle.X509.X509Certificate certificateToValidate = this.cp.ReadCertificate(cert.RawData);
                if (this.data == null)
                {
                    throw new CrlUnavailableForDownloadException(url.ToString());
                }
                else
                {
                    isRevoked = data.IsRevoked(certificateToValidate);
                }
            }
            finally
            {
                rwl.ExitReadLock();
            }

            if (this.logger.IsInfoEnabled)
            {
                if (isRevoked)
                {
                    this.logger.Info(string.Format("Certificate '{0}' is revoked.", cert.ToString()));
                }
            }

            return isRevoked;
        }

        /// <summary>
        /// Downloads a new CRL from the URL stored in the class.
        /// </summary>
        private void upgradeData()
        {
            try
            {
                this.logger.Info(string.Format("Downloading CRL from: '{0}'.", url.ToString()));
                WebRequest request = WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Only download .crl file if it is present on the server.
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Everything went well.
                    using (Stream stream = response.GetResponseStream())
                    {
                        if (stream == null)
                        {
                            this.logger.Warn("The stream is null.");
                        }                       

                        // Downloads the .crl file into an X509CRL object.
                        this.logger.Debug(string.Format("Parsing the CRL data ({1}) retrieved from: '{0}'.", url.ToString(), stream.Length));
                       
                        this.logger.Trace("Start 'crlParser.ReadCrl(stream)'");
                        this.data = this.crlParser.ReadCrl(stream);
                        this.logger.Trace("Finish with 'crlParser.ReadCrl(stream)'");
                        

                        if (this.logger.IsDebugEnabled)
                        {
                            ISet revokedCertificates = this.data.GetRevokedCertificates();
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(string.Format("There exist {1} revoked certificates in the CRL from '{0}'.", url.ToString(), revokedCertificates.Count));
                            foreach (object obj in revokedCertificates)
                            {
                                sb.AppendLine(string.Format("  {0}", obj.ToString()));
                            }

                            this.logger.Debug(sb.ToString());                            
                        }

                        DateTime f = data.NextUpdate.Value;                        
                    }
                }
                else
                {
                    throw new CheckCertificateRevokedUnexpectedException(new Exception("CRL could not be downloaded: " + response.StatusDescription));
                }
            }
            catch (IOException e)
            {
                // Could not download new crl
                throw new CheckCertificateRevokedUnexpectedException(e);                
            }

            return; 
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
