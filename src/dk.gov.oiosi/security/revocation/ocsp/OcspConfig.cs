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
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.lookup;
using System.Collections.Generic;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.security.revocation.ocsp {

    /// <summary>
    /// OCSP dynamic configuration
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class OcspConfig 
    {
        private ILogger logger;
        private Uri _serverUrl = null;
        private int _defaultTimeoutMsec = 10000;

        /// <summary>
        /// Default constructor that initializes the OscpConfig with default values
        /// </summary>
        public OcspConfig()
        {
            this.logger = LoggerFactory.Create(this.GetType());
        }

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
        /// Loads the configured OCES default root certificate
        /// </summary>
        /// <returns>The loaded x509 certificate. If no certificate is found, an exception is thrown.</returns>
        public IList<X509Certificate2> GetDefaultOcesRootCertificateListFromStore()
        {
            IList<X509Certificate2> list = new List<X509Certificate2>();
            RootCertificateCollectionConfig rootCertificateConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateCollectionConfig>();
            X509Certificate2 certificate2;
            CertificateLoader certificateLoader = new CertificateLoader();

            IList<Exception> exceptions = new List<Exception>();
            foreach(CertificateStoreIdentification certificateStoreIdentification in rootCertificateConfig.GetAsList())
            {
                // reset certificate to null
                certificate2 = null;

                try
                {
                    // tries to retrive the certificate
                    certificate2 = certificateLoader.GetCertificateFromCertificateStoreInformation(certificateStoreIdentification);
                }
                catch (Exception ex)
                {
                    // store the exception
                    // it is log later
                    exceptions.Add(ex);
                }

                // only add the certificate, if one found
                if (certificate2 != null)
                {
                    list.Add(certificate2);
                }
            }

            if (list.Count == 0)
            {
                // no root certificate was found.
                // logging the exception
                foreach (Exception exception in exceptions)
                {
                    this.logger.Error(exceptions);
                }

                // and throw the latest exception
                if (exceptions.Count > 0)
                {
                    throw exceptions[exceptions.Count - 1];
                }
                else
                {
                    // no exception
                    // an no root certificate
                    throw new CertificateHandlingException(new Exception("No root certificate was found!")); 
                }
            }
            else
            {
                // some root certificate found
                // only log exceptions as debug
                foreach (Exception exception in exceptions)
                {
                    this.logger.Debug(exceptions);
                }
            }

            return list;
        }
    }
}