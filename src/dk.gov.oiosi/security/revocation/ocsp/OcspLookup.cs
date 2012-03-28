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
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using dk.gov.oiosi.common;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.configuration;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;
//using Novell.Directory.Ldap.Asn1;

namespace dk.gov.oiosi.security.revocation.ocsp {

    /// <summary>
    /// Class for checking certificate revocation status against an OCSP (Online Certificate Status Protocol) server.
    /// </summary>
    public class OcspLookup : IRevocationLookup {

        private OcspConfig _configuration;

        private readonly int BufferSize = 4096 * 8;

        /// <summary>
        /// List of the default OCES (OCES1 and OCES2) root certificate
        /// </summary>
        private IList<X509Certificate2> defaultOcesRootCertificateList;

        private IDictionary<string, X509Certificate2> rootCertificateDirectory;

        private ICache<string, RevocationResponse> ocspCache;

        /// <summary>
        /// Instantiates OcspLookup and loads the OCES default root certificate
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        public OcspLookup(OcspConfig configuration) {
            Init(configuration, null);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="defaultRootCertificateList">The default root certificate</param>
        public OcspLookup(OcspConfig configuration, IList<X509Certificate2> defaultRootCertificateList) {
            Init(configuration, defaultRootCertificateList);
        }

        /// <summary>
        /// Default constructor. Attempts to load configuration from configuration file.
        /// </summary>
        public OcspLookup() {
            OcspConfig configuration = ConfigurationHandler.GetConfigurationSection<OcspConfig>();
            Init(configuration, null);
        }
        

        /// <summary>
        /// Gets the configuration of the lookup client
        /// </summary>
        public OcspConfig Configuration
        {
            get { return _configuration; }
        }

        /// <summary>
        /// To be able to call the CheckCertificate method asynchron, we create a delegate.
        /// </summary>
        /// <param name="certificate">the certificate to check</param>
        /// <returns>the OcSpResponse object to store the result</returns>
        public delegate RevocationResponse AsyncOcspCall(X509Certificate2 certificate);

        /// <summary>
        /// Initializes. If the default root certificate is set to null, an attempt is made
        /// to get a default root certificate from a Configuration.xml file
        /// </summary>
        /// <param name="configuration">OCSP configuration</param>
        /// <param name="defaultRootCertificateList">If the default root certificate is set to null, an attempt is made
        /// to get a default root certificate from a Configuration.xml file</param>
        private void Init(OcspConfig configuration, IList<X509Certificate2> defaultRootCertificateList)
        {
            this.ocspCache = CacheFactory.Instance.OcspLookupCache;
            try
            {
                // 1. Set configuration
                _configuration = configuration;

                // 2. Get default certificate:
                if (defaultRootCertificateList == null)
                {
                    defaultRootCertificateList = _configuration.GetDefaultOcesRootCertificateListFromStore();
                }

                // put the root certificates into the directory

                this.rootCertificateDirectory = new Dictionary<string, X509Certificate2>();

                foreach (X509Certificate2 x509Certificate2 in defaultRootCertificateList)
                {
                    this.rootCertificateDirectory.Add(x509Certificate2.Thumbprint.ToLowerInvariant(), x509Certificate2);
                }
            }
            catch (UriFormatException e)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (OverflowException)
            {
                throw;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (CryptographicUnexpectedOperationException)
            {
                throw;
            }
            catch (CryptographicException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Checks a certificate status on a ocsp server
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>The RevocationResponse object that contains the result</returns>
        /// <exception cref="CheckCertificateOcspUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        private RevocationResponse RevocationResponse(X509Certificate2 x509Certificate2)
        {
            // this method can be call requsiv, so check the cache first
            RevocationResponse revocationResponse;

            bool ocspResponseExistsInCache = this.ocspCache.TryGetValue(x509Certificate2.SubjectName.Name, out revocationResponse);
            if (ocspResponseExistsInCache)
            {
                // response already in cache.
                // Check if the response still is valid
                if (revocationResponse.NextUpdate < DateTime.Now)
                {
                    // the cached value is to old
                    // get new value
                    revocationResponse = this.RevocationResponseOnline(x509Certificate2);
                }
            }
            else
            {
                // respons is not in cache
                revocationResponse = this.RevocationResponseOnline(x509Certificate2);
            }

            return revocationResponse;
        }

        /// <summary>
        /// Checks a certificate status on a ocsp server
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>The RevocationResponse object that contains the result</returns>
        /// <exception cref="CheckCertificateOcspUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        private RevocationResponse RevocationResponseOnline(X509Certificate2 x509Certificate2)
        {
            RevocationResponse revocationResponse = new RevocationResponse();
           
                if (x509Certificate2 == null)
                {
                    throw new CheckCertificateOcspUnexpectedException();
                }
                // http://bouncy-castle.1462172.n4.nabble.com/c-ocsp-verification-td3160243.html
                X509Certificate2 issuerX509Certificate2 = this.FindIssuerCertificate(x509Certificate2);

                if (issuerX509Certificate2.Thumbprint.Equals(x509Certificate2.Thumbprint, StringComparison.OrdinalIgnoreCase))
                {
                    // the certificate and the issuer certificace is the same
                    // this mean that the root certificate is not trusted
                    revocationResponse = null;
                }
                else
                {
                    revocationResponse = this.RevocationResponseOnline(x509Certificate2, issuerX509Certificate2); 

                    if(revocationResponse != null)
                    {
                        if (revocationResponse.Exception == null)
                        {
                            // no exception recorded
                            if (revocationResponse.IsValid)
                            {                                
                                // now we know the certificate is valid.
                                // if the issuer is a trusted root certificate, all is good
                                if (this.rootCertificateDirectory.ContainsKey(issuerX509Certificate2.Thumbprint.ToLowerInvariant()))
                                {
                                    // the root certificate is trusted, so the RevocationResponse can be put on the cache
                                    this.ocspCache.Set(x509Certificate2.SubjectName.Name, revocationResponse);
                                }
                                else
                                {
                                    // we do not yet know if the certificate is valid.
                                    // the certificate migth be good, but if the issueing certificate is revoked,
                                    // then the certificate should also be revoked.
                                    // validate the issuer certificate
                                    // this is required, because certificate can have a chain that is longer then 2
                                    
                                    // The only problem is, that we can not ocsp validate the intermiddel certificate.
                                    // It does not contain the Authority Info Access, containng the rl to where the certificate must be validated
                                    // We must therefore gues, that the certificate is valid.
                                    
                                    /*
                                    RevocationResponse issuerRevocationResponse = RevocationResponseOnline(issuerX509Certificate2);

                                    if(issuerX509Certificate2 != null)
                                    {
                                        // the issuer certificate is validated, the validity of the issuer certificate
                                        // is copied to the revocationResponse
                                        revocationResponse.IsValid = issuerRevocationResponse.IsValid;
                                        revocationResponse.Exception = issuerRevocationResponse.Exception;
                                       // revocationResponse.RevocationCheckStatus = issuerRevocationResponse.RevocationCheckStatus;
                                    }
                                    else
                                    {
                                        revocationResponse.IsValid = false;
                                        revocationResponse.Exception = new CheckCertificateOcspUnexpectedException("The issueing certificate could not be validated.");
                                        //revocationResponse.RevocationCheckStatus = issuerRevocationResponse.RevocationCheckStatus;
                                    }*/
                                }
                            }
                            else
                            {
                                // the certificate is not valid
                                // no need to check the issuer certificate
                                this.ocspCache.Set(x509Certificate2.SubjectName.Name, revocationResponse);
                            }
                        }
                        else
                        {
                            // some exception returned.
                            // do not add to cache
                        }                            
                    }

                }

            return revocationResponse;
        }


        private RevocationResponse RevocationResponseOnline(X509Certificate2 serverX509Certificate2, X509Certificate2 issuerX509Certificate2)
        {
            RevocationResponse revocationResponse = new RevocationResponse();
            try
            {
                if (issuerX509Certificate2 == null)
                {
                    throw new Exception("Issuer certificate for server certificate not identified");
                }

                // create BouncyCastle certificates
                X509CertificateParser certParser = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate issuerX509Certificate = certParser.ReadCertificate(issuerX509Certificate2.RawData);
                Org.BouncyCastle.X509.X509Certificate serverX509Certificate = certParser.ReadCertificate(serverX509Certificate2.RawData);

                // 1. Get server url
                List<string> urlList = this.GetAuthorityInformationAccessOcspUrl(serverX509Certificate);

                if (urlList.Count == 0)
                {
                    throw new Exception("No OCSP url found in ee certificate.");
                }

                // we always validate against the first defined url
                string url = urlList[0];

                // 2. Generate request
                OcspReq req = this.GenerateOcspRequest(issuerX509Certificate, serverX509Certificate.SerialNumber);

                // 2. make binary request online
                byte[] binaryResp = this.PostData(url, req.GetEncoded(), "application/ocsp-request", "application/ocsp-response");

                //3. check result
                revocationResponse = this.ProcessOcspResponse(serverX509Certificate, issuerX509Certificate, binaryResp);
            }
            catch (CheckCertificateOcspUnexpectedException)
            {
                throw;
            }
            catch (ArgumentNullException e)
            {
                revocationResponse.Exception = new CheckCertificateOcspUnexpectedException("ArgumentNullException", e);
            }
            catch (OverflowException e)
            {
                revocationResponse.Exception = new CheckCertificateOcspUnexpectedException("OverflowException", e);
            }
            catch (FormatException e)
            {
                revocationResponse.Exception = new CheckCertificateOcspUnexpectedException("FormatException", e);
            }
            catch (CryptographicUnexpectedOperationException e)
            {
                revocationResponse.Exception = new CheckCertificateOcspUnexpectedException("CryptographicUnexpectedOperationException", e);
            }
            catch (CryptographicException e)
            {
                revocationResponse.Exception = new CheckCertificateOcspUnexpectedException("CryptographicException", e);
            }
            catch (Exception e)
            {
                revocationResponse.Exception = new CheckCertificateOcspUnexpectedException("OCSP valideringen fejlede.", e);
            }

            return revocationResponse;
        }

        public X509Certificate2 FindIssuerCertificate(X509Certificate2 serverX509Certificate2)
        {
            X509Certificate2 issuerX509Certificate2 = null;
            
            // Find the issuer certificate
            X509Chain x509Chain = new X509Chain();
            x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
            x509Chain.Build(serverX509Certificate2);

            // Iterate though the chain, to validate if it contain a valid root vertificate
            X509ChainElementCollection x509ChainElementCollection = x509Chain.ChainElements;
            X509ChainElementEnumerator enumerator = x509ChainElementCollection.GetEnumerator();
            X509ChainElement x509ChainElement;
            X509Certificate2 x509Certificate2 = null;
            IDictionary<string, X509Certificate2> map = new Dictionary<string, X509Certificate2>();

            // At this point, the certificate is not valid, until a 
            // it is proved that it has a valid root certificate
            while (enumerator.MoveNext())
            {
                x509ChainElement = enumerator.Current;
                x509Certificate2 = x509ChainElement.Certificate;
                map.Add(x509Certificate2.Subject, x509Certificate2);
            }

            if (map.ContainsKey(serverX509Certificate2.Issuer))
            {
                issuerX509Certificate2 = map[serverX509Certificate2.Issuer];
            }

            return issuerX509Certificate2;
        }

       /* public X509Certificate2 FindRootCertificate(X509Certificate2 serverX509Certificate2, IDictionary<string, X509Certificate2> rootCertificateDirectory)
        {
            bool rootCertificateFound = false;
            X509Certificate2 desiredRootX509Certificate2 = null;
            // Find the desired root certificate
            X509Chain x509Chain = new X509Chain();
            x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
            x509Chain.Build(serverX509Certificate2);

            // Iterate though the chain, to validate if it contain a valid root vertificate
            X509ChainElementCollection x509ChainElementCollection = x509Chain.ChainElements;
            X509ChainElementEnumerator enumerator = x509ChainElementCollection.GetEnumerator();
            X509ChainElement x509ChainElement;
            X509Certificate2 x509Certificate2 = null;
            string x509CertificateThumbprint;
            // At this point, the certificate is not valid, until a 
            // it is proved that it has a valid root certificate
            while (rootCertificateFound == false && enumerator.MoveNext())
            {
                x509ChainElement = enumerator.Current;
                x509Certificate2 = x509ChainElement.Certificate;
                x509CertificateThumbprint = x509Certificate2.Thumbprint.ToLowerInvariant();
                if (rootCertificateDirectory.ContainsKey(x509CertificateThumbprint))
                {
                    // The current chain element is in the trusted rootCertificateDirectory
                    rootCertificateFound = true;

                    // now the loop will break, as we have found a trusted root certificate
                }
            }

            if (rootCertificateFound)
            {
                // root certificate is found
                desiredRootX509Certificate2 = x509Certificate2;
            }

            return desiredRootX509Certificate2;
        }*/

        public List<string> GetAuthorityInformationAccessOcspUrl(Org.BouncyCastle.X509.X509Certificate rootX509Certificate)
        {
            List<string> ocspUrls = new List<string>();

            try
            {
                // "1.3.6.1.5.5.7.1.1"
                Asn1Object asn1Object = this.GetExtensionValue(rootX509Certificate, X509Extensions.AuthorityInfoAccess.Id);

                if (asn1Object == null)
                {
                    return null;
                }

                // For a strange reason I cannot acess the aia.AccessDescription[]. 
                // Hope it will be fixed in the next version (1.5). 
                // mySupply ApS - JLM - Still not working in 1.7
                // AuthorityInformationAccess aia = AuthorityInformationAccess.GetInstance(asn1Object); 

                // Switched to manual parse 
                Asn1Sequence s = (Asn1Sequence)asn1Object;
                IEnumerator elements = s.GetEnumerator();

                while (elements.MoveNext())
                {
                    Asn1Sequence element = (Asn1Sequence)elements.Current;
                    DerObjectIdentifier oid = (DerObjectIdentifier)element[0];

                    if (oid.Id.Equals("1.3.6.1.5.5.7.48.1")) // Is Ocsp 
                    {
                        Asn1TaggedObject taggedObject = (Asn1TaggedObject)element[1];
                        GeneralName gn = (GeneralName)GeneralName.GetInstance(taggedObject);
                        ocspUrls.Add(((DerIA5String)DerIA5String.GetInstance(gn.Name)).GetString());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error parsing AIA.", e);
            }

            return ocspUrls;
        }

        private Asn1Object GetExtensionValue(Org.BouncyCastle.X509.X509Certificate rootX509Certificate, string oid)
        {
            if (rootX509Certificate == null)
            {
                return null;
            }

            byte[] bytes = rootX509Certificate.GetExtensionValue(new DerObjectIdentifier(oid)).GetOctets();

            if (bytes == null)
            {
                return null;
            }

            Asn1InputStream aIn = new Asn1InputStream(bytes);

            return aIn.ReadObject();
        } 

       /* private OcspReq GenerateOcspRequest(X509Certificate2 rootX509Certificate2, BigInteger serialNumber)
        {
            X509Certificate rootX509Certificate = rootX509Certificate2.Export(X509ContentType.Cert);
            return this.GenerateOcspRequest(rootX509Certificate, serialNumber);
        }*/

       /* private OcspReq GenerateOcspRequest(X509Certificate rootX509Certificate, byte serialNumber)
        {
            BigInteger serialNumberBigInteger = new BigInteger(serialNumber);
            return this.GenerateOcspRequest(rootX509Certificate, serialNumberBigInteger);
        }*/


        private OcspReq GenerateOcspRequest(Org.BouncyCastle.X509.X509Certificate rootX509Certificate, BigInteger serialNumber)
        {
            CertificateID certificateID = new CertificateID(CertificateID.HashSha1, rootX509Certificate, serialNumber);
            return this.GenerateOcspRequest(certificateID);
        }

        private OcspReq GenerateOcspRequest(CertificateID id)
        {
            OcspReqGenerator ocspRequestGenerator = new OcspReqGenerator();

            ocspRequestGenerator.AddRequest(id);

            BigInteger nonce = BigInteger.ValueOf(new DateTime().Ticks);

            ArrayList oids = new ArrayList();
            Hashtable values = new Hashtable();

            oids.Add(OcspObjectIdentifiers.PkixOcsp);

            Asn1OctetString asn1 = new DerOctetString(new DerOctetString(new byte[] { 1, 3, 6, 1, 5, 5, 7, 48, 1, 1 }));

            values.Add(OcspObjectIdentifiers.PkixOcsp, new Org.BouncyCastle.Asn1.X509.X509Extension(false, asn1));
            X509Extensions x509Extensions = new X509Extensions(oids, values);
            ocspRequestGenerator.SetRequestExtensions(x509Extensions);

            OcspReq ocspReq = ocspRequestGenerator.Generate();

            return ocspRequestGenerator.Generate();
        }
        
        private byte[] PostData(string url, byte[] data, string contentType, string accept)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = contentType;
            request.ContentLength = data.Length;
            request.Accept = accept;
            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream respStream = response.GetResponseStream();
            byte[] resp = this.ToByteArray(respStream);
            respStream.Close();

            return resp;
        }

        public byte[] ToByteArray(Stream stream)
        {
            byte[] buffer = new byte[this.BufferSize];
            MemoryStream ms = new MemoryStream();

            int read = 0;

            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms.ToArray();
        }

        private RevocationResponse ProcessOcspResponse(Org.BouncyCastle.X509.X509Certificate serverX509Certificate, Org.BouncyCastle.X509.X509Certificate rootX509Certificate, byte[] binaryResp)
        {
            OcspResp r = new OcspResp(binaryResp);
            //CertificateStatus cStatus = CertificateStatus.Unknown;
            RevocationResponse revocationResponse = new RevocationResponse();

            X509CertificateParser parser = null;
            

            switch (r.Status)
            {
                case OcspRespStatus.Successful:
                    {
                        BasicOcspResp or = (BasicOcspResp)r.GetResponseObject();

                        // ValidateResponse(or, issuerCert); 

                        if (or.Responses.Length == 1)
                        {
                            SingleResp resp = or.Responses[0];

                            this.ValidateCertificateId(serverX509Certificate, rootX509Certificate, resp.GetCertID());

                            // ValidateThisUpdate(resp); 
                            // ValidateNextUpdate(resp); 

                            Object certificateStatus = resp.GetCertStatus();

                            if (certificateStatus == null)
                            {
                                // no revocation data exist - the certificate must be valid

                                revocationResponse.IsValid = true;
                                revocationResponse.NextUpdate = resp.NextUpdate.Value;
                            }
                            else if (certificateStatus == Org.BouncyCastle.Ocsp.CertificateStatus.Good)
                            {
                                revocationResponse.IsValid = true;
                                revocationResponse.NextUpdate = resp.NextUpdate.Value;
                            }
                            else if (certificateStatus is Org.BouncyCastle.Ocsp.RevokedStatus)
                            {
                                revocationResponse.IsValid = false;
                                revocationResponse.NextUpdate = resp.NextUpdate.Value;
                            }
                            else if (certificateStatus is Org.BouncyCastle.Ocsp.UnknownStatus)
                            {
                                throw new CheckCertificateOcspUnexpectedException("CertificateStatus is Unknown");
                            }
                            else
                            {
                                throw new CheckCertificateOcspUnexpectedException("CertificateStatus is unknown '" + certificateStatus.ToString()+ "'.");
                            }
                        }
                        break;
                    }
                default:
                    {
                        throw new CheckCertificateOcspUnexpectedException("Unknow status '" + r.Status + "'.");
                    }
            }

            return revocationResponse;
        }

        //1. The certificate identified in a received response corresponds to 
        //that which was identified in the corresponding request; 
        private void ValidateCertificateId(Org.BouncyCastle.X509.X509Certificate serverX509Certificate, Org.BouncyCastle.X509.X509Certificate rootX509Certificate, CertificateID certificateId)
        {
            CertificateID expectedId = new CertificateID(CertificateID.HashSha1, rootX509Certificate, serverX509Certificate.SerialNumber);

            if (!expectedId.SerialNumber.Equals(certificateId.SerialNumber))
            {
                throw new CheckCertificateOcspUnexpectedException("Invalid certificate ID in response");
            }

            if (!Org.BouncyCastle.Utilities.Arrays.AreEqual(expectedId.GetIssuerNameHash(), certificateId.GetIssuerNameHash()))
            {
                throw new CheckCertificateOcspUnexpectedException("Invalid certificate Issuer in response");
            }
        } 

        /// <summary>
        /// Checks a certificate status on a ocsp server
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>The RevocationResponse object that contains the result</returns>
        /// <exception cref="CheckCertificateOcspUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        public RevocationResponse CheckCertificate(X509Certificate2 x509Certificate2) {
            //To call the CheckCertificate asynchronously, we initialize the delegate and call it with IAsyncResult
            RevocationResponse revocationResponse;

            bool ocspResponseExistsInCache = this.ocspCache.TryGetValue(x509Certificate2.SubjectName.Name, out revocationResponse);
            if (ocspResponseExistsInCache)
            {
                // response already in cache.
                // Check if the response still is valid
                if (revocationResponse.NextUpdate < DateTime.Now)
                {
                    // the cached value is to old
                    // get new value
                    revocationResponse = this.CheckCertificateAsync(x509Certificate2);
                }
            }
            else
            {
                // respons is not in cache
                revocationResponse = this.CheckCertificateAsync(x509Certificate2);
            }

            /*// now to validate the result
            if (revocationResponse.Exception == null)
            {
                // no exception recorded
                // update the cache
                ocspCache.Set(x509Certificate2.SubjectName.Name, revocationResponse);
                //return response;
            }
            else
            {
                // some exception returned.
                // do not add to cache
            }*/

            return revocationResponse;
        }

        /// <summary>
        /// Checks a certificate status on a ocsp server
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>The RevocationResponse object that contains the result</returns>
        /// <exception cref="CheckCertificateOcspUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        public RevocationResponse CheckCertificateAsync(X509Certificate2 certificate)
        {
            //To call the CheckCertificate asynchronously, we initialize the delegate and call it with IAsyncResult
            RevocationResponse response;

            AsyncOcspCall asyncOcspCall = new AsyncOcspCall(this.RevocationResponse);
            IAsyncResult asyncResult = asyncOcspCall.BeginInvoke(certificate, null, null);

            bool ocspRepliedInTime = asyncResult.AsyncWaitHandle.WaitOne(Utilities.TimeSpanInMilliseconds(TimeSpan.FromMilliseconds(_configuration.DefaultTimeoutMsec)), false);
            if (ocspRepliedInTime)
            {
                // okay, the operation has finish.
                response = asyncOcspCall.EndInvoke(asyncResult);
            }
            else
            {
                // Note - The validation is still running, and is not closed

                // operation timeout
                throw new CertificateRevokedTimeoutException(TimeSpan.FromMilliseconds(_configuration.DefaultTimeoutMsec));
            }

            return response;
        }
    }
}