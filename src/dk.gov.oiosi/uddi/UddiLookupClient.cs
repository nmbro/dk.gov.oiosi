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
  *
  */
using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Class for resolving endpoints on the UDDI-based Address Resolution Service (ARS).
    /// Features of this class:
    ///
    /// * Resolves a business level endpoint to a service endpoint, using the UDDI 2.0 inquiry API
    /// * Performs automatic caching of resolving attempts
    /// * Supports caching policies based on call result feedback
    /// 
    /// Extension points:
    /// 
    /// * Extension mechanism for adding additional key types for resolving
    /// * Extendable configuration model
    /// </summary>
    public class UddiLookupClient : IUddiLookupClient {
        private UddiConfig _configuration;
        private object CacheLock = new object();

        private void Init() {
            // 1. If a default connection has not been set, set it
            if (UddiConnection.DefaultConnection == null) {
                UddiConnection.DefaultConnection = new UddiConnection(
                    new UddiConnectionNetworkParams(
                        _configuration.UddiInquireEndpointURL,
                        _configuration.UddiInquireEndpointURLFallback,
                        _configuration.UddiPublishEndpointURL,
                        _configuration.UddiSecurityEndpointURL,
                        _configuration.FallbackTimeoutMinutes,
                        _configuration.TryOtherHostsOnFailure)
                );
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UddiLookupClient(UddiConfig configuration) {
            _configuration = configuration;
            Init();
        }

        /// <summary>
        /// Default constructor. Attempts to load configuration from file.
        /// </summary>
        public UddiLookupClient() {
            _configuration = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            Init();
        }

        /// <summary>
        /// Translates a business level key ("EndpointKey", e.g. an EAN number) to an endpoint address (e.g. an URL).
        /// </summary>
        /// <param name="parameters">The business level key of the endpoint, e.g. an EAN number
        /// as well as options of the translation, i.e. address type filters</param>
        /// <returns>Returns a collection of matching addresses</returns>
        public List<UddiLookupResponse> Lookup(
           LookupParameters parameters
        ) {
            try {
                // Check cache, if not found there, inquire UDDI
                List<UddiLookupResponse> inquiryResult = CachedInquire(parameters);

                // Is the request in the gateway range (if a gateway range exists)?
                if ((inquiryResult == null || inquiryResult.Count < 1)
                    && (parameters.EndpointKey is IdentifierEan)
                    && (_configuration.GatewayRange.IsInRange(parameters.EndpointKey.GetAsString())
                    && _configuration.GatewayRange != null
                    && _configuration.GatewayRange.GatewayRegistrationParameters != null
                    && !String.IsNullOrEmpty(_configuration.GatewayRange.GatewayRegistrationParameters.GatewayRegistrationKeyEan))
                ) {
                    inquiryResult = GatewayLookup(parameters);
                }

                // If inquiry result is null, return empty list
                if (inquiryResult == null) return new List<UddiLookupResponse>();

                // Filter result
                List<UddiLookupResponse> filteredResult = FilterResponse(parameters, inquiryResult);

                // Return result
                return filteredResult;
            }
            catch (Exception ex) {
                string endpKey = "NULL";
                if (parameters != null && parameters.EndpointKey != null) {
                    endpKey = parameters.EndpointKey.GetAsString();
                }
                throw new UddiLookupException(endpKey, ex);
            }
        }

        /// <summary>
        /// Check cache, if not found there, inquire UDDI
        /// </summary>
        /// <param name="parameters">The business level key of the endpoint, e.g. an EAN number
        /// as well as options of the translation, i.e. address type filters</param>
        /// <returns>Returns a collection of matching results</returns>
        private List<UddiLookupResponse> CachedInquire(LookupParameters parameters) {
            List<UddiLookupResponse> inquiryResult;
            UddiInquiry inquiry = new UddiInquiry(_configuration);
            // 1. Perform (pre- and post-filtered) query

            // 2. Get result from cache or "live"
            LookupKey cacheLookupKey = parameters.GetLookupKey();


            bool cacheLookupResult = parameters.LookupCache.TryGetValue(cacheLookupKey, out inquiryResult);
            if (!cacheLookupResult) {
                inquiryResult = inquiry.Inquire(
                    parameters.EndpointKey,
                    parameters,
                    UddiLookupClientPolicy.Default
                );
            }

            // 3. Add result to cache, if relevant
            lock (CacheLock) {
                if (!parameters.LookupCache.ContainsKey(cacheLookupKey) && inquiryResult != null && inquiryResult.Count != 0) {
                    parameters.LookupCache.Add(cacheLookupKey, inquiryResult);
                }
            }
            return inquiryResult;
        }

        /// <summary>
        /// Filters the response 
        /// </summary>
        /// <returns></returns>
        private List<UddiLookupResponse> FilterResponse(LookupParameters parameters, List<UddiLookupResponse> inquiryResult) {
            List<UddiLookupResponse> newFilteredResult = new List<UddiLookupResponse>();
            // How many endpoints are we expecting?
            if (inquiryResult.Count > 0) {
                switch (parameters.LookupReturnOption) {
                    case LookupReturnOptionEnum.allResults:
                        return inquiryResult;
                    case LookupReturnOptionEnum.firstResult:
                        newFilteredResult.Add(inquiryResult[0]);
                        return newFilteredResult;
                    case LookupReturnOptionEnum.noMoreThanOneSetOrFail:
                        if (inquiryResult.Count > 2) {
                            throw new UddiMoreThanOneEndpointException();
                        } 
                        if (inquiryResult.Count == 2) {
                            if (inquiryResult[0].EndpointAddress.GetType() == inquiryResult[1].EndpointAddress.GetType()) throw new UddiTwoEqualEndpointsException();
                            int preferredIndex;
                            if (inquiryResult[0].EndpointAddress is EndpointAddressSMTP) {
                                preferredIndex = parameters.PreferredEndpointType == PreferredEndpointType.mailto ? 0 : 1;
                            } else {
                                preferredIndex = parameters.PreferredEndpointType == PreferredEndpointType.http ? 0 : 1;
                            }
                            newFilteredResult.Add(inquiryResult[preferredIndex]);
                            return newFilteredResult;
                        } 
                        newFilteredResult.Add(inquiryResult[0]);
                        return newFilteredResult;
                    default:
                        return inquiryResult;
                }
            }
            return inquiryResult;
        }

        /// <summary>
        /// Translates a business level key ("EndpointKey", e.g. an EAN number) to an endpoint 
        /// address (e.g. an URL) for a gateway registration. Performs the same actions as first
        /// part of Lookup(), but without checking for gateway ranges.
        /// </summary>
        /// <param name="parameters">The business level key of the endpoint, e.g. an EAN number
        /// as well as options of the translation, i.e. address type filters</param>
        /// <returns>Returns a collection of matching addresses</returns>
        private List<UddiLookupResponse> GatewayLookup(LookupParameters parameters) {
            List<UddiLookupResponse> inquiryResult = null;
            // 1. Change the endpoint key to that of the gateway registration:
            parameters.EndpointKey.Set(_configuration.GatewayRange.GatewayRegistrationParameters.GatewayRegistrationKeyEan);
            // 2. Check cache, if not found there, inquire UDDI
            inquiryResult = CachedInquire(parameters);
            return inquiryResult;
        }
    }
}