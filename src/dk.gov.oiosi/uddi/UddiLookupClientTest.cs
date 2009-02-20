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
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Dummy implementation of the IUddiLookupClient interface for test purposes.
    /// Always returns the same result on requests, no use of network/UDDI.
    /// </summary>
    public class UddiLookupClientTest : IUddiLookupClient{
        private UddiLookupClientTestConfig _config;

        /// <summary>
        /// Default constructor. Attempts to load config from file.
        /// </summary>
        public UddiLookupClientTest() {
            _config = ConfigurationHandler.GetConfigurationSection<UddiLookupClientTestConfig>();
        }

        #region IUddiLookupClient Members

        /// <summary>
        /// Implements the IUddiLookupClient interface for test purposes.
        /// </summary>
        /// <param name="parameters">The lookup parameters</param>
        /// <returns>Returns a list of results</returns>
        public List<UddiLookupResponse> Lookup(LookupParameters parameters) {
            EndpointAddress endpointAddress;
            if (_config.EndpointAddress.ToLower().StartsWith("http://")) {
                endpointAddress = new EndpointAddressHttp(new Uri(_config.EndpointAddress));
            } else {
                endpointAddress = new EndpointAddressSMTP(new Uri(_config.EndpointAddress));
            }

            UddiLookupResponse dummyResponse = new UddiLookupResponse(
                parameters.EndpointKey,
                endpointAddress,
                _config.ActivationDate,
                _config.ExpirationDate,
                new CertificateSubject(_config.CertificateSubjectSerialNumber),
                new Uri(_config.TermsOfUseUrl),
                new System.Net.Mail.MailAddress(_config.ServiceContactEmail),
                new Version(_config.Version),
                null);
                
            List<UddiLookupResponse> dummyList = new List<UddiLookupResponse>();
            dummyList.Add(dummyResponse);

            return dummyList;
        }

        #endregion
    }
}