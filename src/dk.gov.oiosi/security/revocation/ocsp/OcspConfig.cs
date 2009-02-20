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
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.security.revocation.ocsp {

    /// <summary>
    /// OCSP dynamic configuration
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class OcspConfig {
        private Uri _serverUrl = null;
        private int _defaultTimeoutMsec = 10000;

        /// <summary>
        /// Gets or sets the URL of the OCSP server.
        /// </summary>
        public string ServerUrl {
            get {
                if (_serverUrl == null)
                    return null;
                else
                    return _serverUrl.ToString(); }
            set { _serverUrl = new Uri(value); }
        }

        /// <summary>
        /// The default timeout in milliseconds for the OCSP lookup operation.
        /// </summary>
        public int DefaultTimeoutMsec {
            get { return _defaultTimeoutMsec; }
            set {
                if (value < 0)
                    throw new InvalidOcspTimeoutValueException(value, 0, int.MaxValue);
                _defaultTimeoutMsec = value; 
            }
        }

        /// <summary>
        /// Default constructor that initializes the OscpConfig with default values
        /// </summary>
        public OcspConfig() { }

        /// <summary>
        /// Loads the configured OCES default root certificate
        /// </summary>
        /// <returns>The loaded x509 certificate. If no certificate is found, an exception is thrown.</returns>
        public X509Certificate2 GetDefaultOcesRootCertificateFromStore() {
            RootCertificateConfig rootCertificateConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateConfig>();
            return CertificateLoader.GetCertificateFromCertificateStoreInformation(rootCertificateConfig.RootCertificateLocation);
        }
    }
}