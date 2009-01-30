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
  *
  */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using dk.gov.oiosi.common.cache;
using Iloveit.Ocsp.Demo;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.common;
using System.Text.RegularExpressions;

namespace dk.gov.oiosi.security.revocation.ocsp {

    /// <summary>
    /// Class for checking certificate revocation status against an OCSP server.
    /// </summary>
    public class OcspLookup : IRevocationLookup {

        OcspConfig _configuration;
        X509Certificate2 _defaultOcesRootCertificate;

        private static readonly ICache<string, RevocationResponse> ocspCache = new TimedCache<string, RevocationResponse>(new TimeSpan(1, 0, 0));

        /// <summary>
        /// Gets the configuration of the lookup client
        /// </summary>
        public OcspConfig Configuration {
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
        /// <param name="defaultRootCertificate">If the default root certificate is set to null, an attempt is made
        /// to get a default root certificate from a Configuration.xml file</param>
        private void Init(OcspConfig configuration, X509Certificate2 defaultRootCertificate) {
            try {
                // 1. Set configuration
                _configuration = configuration;

                // 2. Get default certificate:
                if (defaultRootCertificate == null) {
                    _defaultOcesRootCertificate = _configuration.GetDefaultOcesRootCertificateFromStore();
                } else {
                    _defaultOcesRootCertificate = defaultRootCertificate;
                }
            } catch (UriFormatException) {
                throw;
            } catch (ArgumentNullException) {
                throw;
            } catch (OverflowException) {
                throw;
            } catch (FormatException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (Exception) {
                throw;
            }
        }

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
        /// <param name="defaultRootCertificate">The default root certificate</param>
        public OcspLookup(OcspConfig configuration, X509Certificate2 defaultRootCertificate) {
            Init(configuration, defaultRootCertificate);
        }

        /// <summary>
        /// Default constructor. Attempts to load configuration from configuration file.
        /// </summary>
        public OcspLookup() {
            OcspConfig configuration = ConfigurationHandler.GetConfigurationSection<OcspConfig>();
            Init(configuration, null);
        }

        /// <summary>
        /// Checks a certificate status on a ocsp server
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>The RevocationResponse object that contains the result</returns>
        /// <exception cref="CheckCertificateOcspUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        private RevocationResponse CheckCertificateCall(X509Certificate2 certificate) {
            RevocationResponse response = new RevocationResponse();

            try {
                //1. instantiate ocsp library
                OcspClient ocsp = new OcspClient(_defaultOcesRootCertificate.RawData);

                //2. make binary request

                //converts hexadecimal to decimal
                uint uiHex = new uint();
                byte[] req = null;
                if (certificate != null) {
                    uiHex = System.Convert.ToUInt32(certificate.SerialNumber, 16);
                    req = ocsp.MakeOcspRequest(uiHex.ToString());

                    //3. send request
                    byte[] resp;
                    if (string.IsNullOrEmpty(_configuration.ServerUrl)){
                        // Use OCSP server specified in certificate
                        resp = ocsp.Send(req, GetServerUriFromCertificate(certificate));
                    }
                    else {
                        // Use OCSP server specified in configuration
                        resp = ocsp.Send(req, _configuration.ServerUrl.ToString());
                    }

                    //4. check result
                    if (ocsp.GetValidSerials(resp).Contains(uiHex.ToString())) {
                        response.IsValid = true;
                    }
                } else {
                    throw new CheckCertificateOcspUnexpectedException();
                }
            } catch (ArgumentNullException) {
                throw;
            } catch (OverflowException) {
                throw;
            } catch (FormatException) {
                throw;
            } catch (CryptographicUnexpectedOperationException) {
                throw;
            } catch (CryptographicException) {
                throw;
            } catch (Exception e) {
                throw new CheckCertificateOcspUnexpectedException(e);
            }
            return response;
        }

        /// <summary>
        /// Gets the Server URI from certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns>Server URI</returns>
        private string GetServerUriFromCertificate(X509Certificate2 certificate) {
            string retval = "";
            
            try {
                Regex liveOcesOidRegEx = new Regex(@"URL=http://[\w|/\.]*");
                string extensionDataString = certificate.Extensions["1.3.6.1.5.5.7.1.1"].Format(false);
                MatchCollection matches = liveOcesOidRegEx.Matches(extensionDataString);
                if (matches.Count == 0 || matches.Count > 1) {
                    // No or more than one matches
                    throw new Exception();
                }
                else {
                    retval = matches[0].ToString().Substring(4);
                }
            }
            catch 
            {
                throw new InvalidOcesCertificateException(certificate);
            }
            return retval;
        } 
        
        /// <summary>
        /// Checks a certificate status on a ocsp server
        /// </summary>
        /// <param name="certificate">The certificate to check</param>
        /// <returns>The RevocationResponse object that contains the result</returns>
        /// <exception cref="CheckCertificateOcspUnexpectedException">This exception is thrown, if an unexpected exception is thrown during the method</exception>
        public RevocationResponse CheckCertificate(X509Certificate2 certificate)
        {
            //To call the CheckCertificate asynchronously, we initialize the delegate and call it with IAsyncResult
            RevocationResponse response;

            if (!ocspCache.TryGetValue(certificate.SubjectName.Name, out response)) {
                
                try
                {
                    AsyncOcspCall asyncOcspCall = new AsyncOcspCall(CheckCertificateCall);
                    IAsyncResult asyncResult = asyncOcspCall.BeginInvoke(certificate, null, null);

                    if (!asyncResult.AsyncWaitHandle.WaitOne(Utilities.TimeSpanInMilliseconds(TimeSpan.FromMilliseconds(_configuration.DefaultTimeoutMsec)), false))
                    {
                        throw new CertificateRevokedTimeoutException(TimeSpan.FromMilliseconds(_configuration.DefaultTimeoutMsec));
                    }
                    else
                    {
                        response = asyncOcspCall.EndInvoke(asyncResult);
                    }
                }
                catch (CheckCertificateOcspUnexpectedException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }

                ocspCache.Add(certificate.SubjectName.Name, response);
            }
            
            return response;
        }
    }
}