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
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.lookup;


namespace dk.gov.oiosi.raspProfile {
    /// <summary>
    /// Default configuration for the root certificate
    /// </summary>
    public class DefaultRootCertificateCollectionConfig {

        /// <summary>
        /// Test default root certificate configuration
        /// </summary>
        public void SetTestDefaultRootCertificateCollectionConfig()
        {
            RootCertificateCollectionConfig rootCertificateCollectionConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateCollectionConfig>();
            
            // OCES 1
            RootCertificateLocation certificatLocation = new RootCertificateLocation();
            certificatLocation.Description = "TDC OCES Systemtest CA II";
            certificatLocation.SerialNumber = "403617FC";
            certificatLocation.StoreLocation = StoreLocation.LocalMachine;
            certificatLocation.StoreName = StoreName.Root;
            rootCertificateCollectionConfig.GetAsList().Add(certificatLocation);

            // OCES 2
            // ToDo - rod certifikatet for OCES2 mangler
        }

        /// <summary>
        /// Liver certificate default root certificate configuration 
        /// </summary>
        public void SetProductionDefaultRootCertificateCollectionConfig()
        {
            RootCertificateCollectionConfig rootCertificateCollectionConfig = ConfigurationHandler.GetConfigurationSection<RootCertificateCollectionConfig>();

            // OCES 1
            RootCertificateLocation certificatLocation = new RootCertificateLocation();
            certificatLocation.Description = "TDC OCES CA";
            certificatLocation.SerialNumber = "3E48BDC4";
            certificatLocation.StoreLocation = StoreLocation.LocalMachine;
            certificatLocation.StoreName = StoreName.Root;
            rootCertificateCollectionConfig.GetAsList().Add(certificatLocation);
            
            // OCES 2
            // ToDo - rod certifikatet for OCES2 mangler
        }

        /// <summary>
        /// Sets the test default root certificate configuration if it does not exist in configuration
        /// </summary>
        public void SetIfNotExistsTestDefaultRootCertificateCollectionConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<RootCertificateCollectionConfig>())
                return;
            SetTestDefaultRootCertificateCollectionConfig();
        }

        /// <summary>
        /// Sets the default live root certificate configuration if it does not exist in configuration
        /// </summary>
        public void SetIfNotExistsProductionDefaultRootCertificateCollectionConfig()
        {
            if (ConfigurationHandler.HasConfigurationSection<RootCertificateCollectionConfig>())
                return;
            SetProductionDefaultRootCertificateCollectionConfig();
        }
    }
}
