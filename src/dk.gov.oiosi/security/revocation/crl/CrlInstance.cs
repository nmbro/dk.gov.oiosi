﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Org.BouncyCastle.X509;

namespace dk.gov.oiosi.security.revocation.crl
{
    /// <summary>
    /// Class used for storing CRLs retrieved from URL's in X509 certificates
    /// </summary>
    class CrlInstance
    {
        private readonly X509CrlParser crlParser;
        private X509Crl data;
        private readonly Uri url;

        /*
         * TODO:
         * Update ReaderWriterLock to ReaderWriterLockSlim when upgrading to .NET 3.5
         */
        private readonly ReaderWriterLock rwl = new ReaderWriterLock();
        private readonly X509CertificateParser cp = new X509CertificateParser();

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
            rwl.AcquireReaderLock(0);
            if (!cacheValid())
            {
                // Upgrades lock since data is not valid.
                LockCookie cookie = rwl.UpgradeToWriterLock(0);
                if (!cacheValid()) // Recheck, since another thread might have updated the cache.
                {
                    try
                    {
                        upgradeData();
                    }
                    catch (CheckCertificateRevokedUnexpectedException e)
                    {
                        rwl.ReleaseLock();
                        throw e;
                    }
                }

                // Downgrade lock
                rwl.DowngradeFromWriterLock(ref cookie);
            }

            // Reads the data and unlocks.
            bool isRevoked = data.IsRevoked(cp.ReadCertificate(cert.RawData));
            rwl.ReleaseLock();
            return isRevoked;
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

                    // Downloads the .crl file into an X509CRL object.
                    data = crlParser.ReadCrl(stream);

                    stream.Close();

                    return; // Everything went well.
                }

                throw new CheckCertificateRevokedUnexpectedException(new Exception("CRL could not be downloaded: " + response.StatusDescription));
            }
            catch (IOException e)
            {
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
            return data != null && data.NextUpdate.Value > DateTime.Now;
        }

        /// <summary>
        /// The NextUpdate value from the CRL
        /// </summary>
        /// <returns>The NextUpdate value from the CRL</returns>
        public DateTime getNextUpdate()
        {
            rwl.AcquireReaderLock(0);
            DateTime date = data != null ? data.NextUpdate.Value : new DateTime(0);
            rwl.ReleaseReaderLock();
            return date;
        }
    }
}