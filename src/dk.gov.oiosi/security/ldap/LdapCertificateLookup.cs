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
//#define SAVECERTIFICATE

using System;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.lookup;
using dk.gov.oiosi.security.validation;
using Novell.Directory.Ldap;

namespace dk.gov.oiosi.security.ldap {
    /// <summary>
    /// The ldap certificate lookup uses the ldap protocol to query for a
    /// given certificate. 
    /// 
    /// It implements the ICertificate lookup to garantee it has a speicific 
    /// interface for querying certificates.
    /// </summary>
    public class LdapCertificateLookup : ICertificateLookup 
    {
        private LdapSettings _settings;

        private ICache<CertificateSubject, X509Certificate2> certiticateCache;

        /// <summary>
        /// The LdapCertificateLookup constructor takes an ldap settings object 
        /// as a paramter. The setting object is used everytime a lookup is done
        /// in the GetCertificate function.
        /// </summary>
        /// <param name="settings">Setting object that contains the ldap settings to be used.</param>
        public LdapCertificateLookup(LdapSettings settings) {
            _settings = settings;
            this.certiticateCache = this.CreateCache();
        }

        /// <summary>
        /// Default constructor. Attempts to read configuration settings from configuration file
        /// </summary>
        public LdapCertificateLookup()
            : this(ConfigurationHandler.GetConfigurationSection<LdapSettings>())
        {
        }

        private ICache<CertificateSubject, X509Certificate2> CreateCache()
        {
            //CacheConfig cacheConfig = ConfigurationHandler.GetConfigurationSection<CacheConfig>();
            //TimeSpan timeSpan = cacheConfig.CertificateCacheTimeSpan;

            ICache<CertificateSubject, X509Certificate2> certiticateCache = CacheFactory.Instance.CertificateCache;
            return certiticateCache;
        }

        #region ICetificateLookup Members

        /// <summary>
        /// The implementation of the interface ICertificateLookups function.
        /// 
        /// It takes the subject string and queries the ldap server for a certificate
        /// that satifies the condition in the subject string.
        /// 
        /// Also implements a certificate cache, which removes unused certificates after 14 days.
        /// </summary>
        /// <param name="subject">The subject string of an OCES certificate.</param>
        /// <returns>The certificate that satifies the conditions of the subject string.</returns>
        public X509Certificate2 GetCertificate(CertificateSubject subject) {
            if (subject == null)
                throw new ArgumentNullException("subject");

            X509Certificate2 certificateToBeReturned;

            if (!certiticateCache.TryGetValue(subject, out certificateToBeReturned))
            {
                
                LdapConnection ldapConnection = null;
                try
                {
                    ldapConnection = ConnectToServer();
                    LdapSearchResults results = Search(ldapConnection, subject);
                    certificateToBeReturned = GetCertificate(results, subject);
                }
                finally
                {
                    if (ldapConnection != null)
                    {
                        ldapConnection.Disconnect();
                    }
                }

                CertificateValidator.ValidateCertificate(certificateToBeReturned);
                certiticateCache.Set(subject, certificateToBeReturned);
            }
            else {
                CertificateValidator.ValidateCertificate(certificateToBeReturned);
            }

            return certificateToBeReturned;
        }

        #endregion


        /// <summary>
        /// Opens a new connection to the ldap server and returns it.
        /// </summary>
        /// <returns>The ldap connection object</returns>
        private LdapConnection ConnectToServer() {
            try {
                LdapConnection ldapConnection = new LdapConnection();
                //A time limit on the connection to the server is added.
                ldapConnection.Constraints.TimeLimit = _settings.ConnectionTimeoutMsec;
                ldapConnection.Connect(_settings.Host, _settings.Port);
                string authenticationMethod = ldapConnection.AuthenticationMethod;
                int protocol = ldapConnection.ProtocolVersion;
                System.Collections.IDictionary prop = ldapConnection.SaslBindProperties;
                LdapSearchConstraints searh = ldapConnection.SearchConstraints;
                
               //ldapConnection.SearchConstraints.

                return ldapConnection;
            } 
            catch (Exception e) {
                throw new ConnectingToLdapServerFailedException(_settings, e);
            }
        }

        /// <summary>
        /// Searchs for a certificate on the ldap server. Uses the connection given and fetches the search string
        /// by calling GetBase() and GetFilter()
        /// </summary>
        /// <exception cref="SearchFailedException">Returned if the search failed</exception>
        /// <param name="ldapConnection">Ldap connection settings</param>
        /// <param name="subject">The certificate subject used in the search</param>
        /// <returns></returns>
        private LdapSearchResults Search(LdapConnection ldapConnection, CertificateSubject subject) 
        {
            LdapSearchResults ldapSearchResults;
            string searchBase = subject.DnsSearchBase;
            string searchFilter = subject.DnsSearchFilter;

            LdapSearchConstraints lsc = new LdapSearchConstraints();
            lsc.ServerTimeLimit = _settings.SearchServerTimeoutMsec;
            lsc.TimeLimit = _settings.SearchClientTimeoutMsec;
            lsc.MaxResults = _settings.MaxResults;
            
            string[] attributes = { "userCertificate" };

            try
            {
                ldapSearchResults = ldapConnection.Search(searchBase, LdapConnection.SCOPE_SUB, searchFilter, attributes, false, lsc);
            } 
            catch (Exception e) {
                throw new SearchFailedException(e);
            }

            return ldapSearchResults;
        }

        /// <summary>
        /// Attempts to get the certificate from the ldap search result. This might fail, if so an exception
        /// is thrown.
        /// </summary>
        /// <exception cref="CertificateNotFoundException">Thrown if no certificate was found</exception>
        /// <exception cref="MultipleCertificatesFoundException">Thrown if more than one certificate was found</exception>
        /// <exception cref="ConvertingSearchResultToCertificateFailedException">Thrown if search result conversion failed</exception>
        /// <param name="ldapSearchResults">The search results</param>
        /// <param name="subject">The subject of the certificate</param>
        /// <returns>Returns the fetched certificate</returns>
        private X509Certificate2 GetCertificate(LdapSearchResults ldapSearchResults, CertificateSubject subject) {
            //The search failed to find a certificate.
            bool searchResultFound = ldapSearchResults.hasMore();
            if (searchResultFound == false)
            {
                throw new LdapCertificateNotFoundException(subject);
            }
            
            LdapEntry ldapEntry = ldapSearchResults.next();

            bool moreThanOneSearchResultFound = ldapSearchResults.hasMore();
            if (moreThanOneSearchResultFound)
            {
                throw new LdapMultipleCertificatesFoundException(subject);
            }

            try {
                LdapAttribute ldapAttribute = ldapEntry.getAttribute("userCertificate;binary");
                sbyte[] sbytes = ldapAttribute.ByteValue;
                byte[] bytes = new byte[sbytes.Length];
                for (int i = 0; i < bytes.Length; i++) {
                    bytes[i] = (byte)sbytes[i];
                }
#if SAVECERTIFICATE
                SaveCertificate(bytes);
#endif
                X509Certificate2 certificate = new X509Certificate2(bytes);
                return certificate;
            }
            catch (Exception e) {
                throw new ConvertingSearchResultToCertificateFailedException(e);
            }
        }

        /// <summary>
        /// Saves the certificate. This is used to help debugging.
        /// </summary>
        /// <exception cref="StoringCertificateFailedException">Thrown if certificate save failed</exception>
        /// <param name="byteDate">The byte date</param>
        private void SaveCertificate(byte[] byteDate) {
            System.IO.BinaryWriter bw = null;
            try {
                System.IO.FileStream fs = System.IO.File.OpenWrite("D:\\temp.cer");
                bw = new System.IO.BinaryWriter(fs);
                for (int i = 0; i < byteDate.Length; i++) {
                    bw.Write(byteDate[i]);
                }
                bw.Flush();
            }
            catch (Exception e) {
                throw new StoringCertificateFailedException(e);
            }
            finally {
                if (bw != null) bw.Close();
            }
        }
    }
}