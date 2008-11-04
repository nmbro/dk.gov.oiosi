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
using System.Text;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.security.ldap {

    /// <summary>
    /// Configuration for the LdapCertificateLookupTest implementation of the ICertificateLookup interface
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class LdapCertificateLookupTestConfig {

        /// <summary>
        /// Types of lookup actions
        /// </summary>
        public enum LookupAction { 

            /// <summary>
            /// Find certificate
            /// </summary>
            FindCertificate, 
            
            /// <summary>
            /// Connection failed
            /// </summary>
            ConnectionFailed, 
            /// <summary>
            /// Search failed
            /// </summary>
            SearchFailed 
        }

        private LookupAction _action = LookupAction.FindCertificate;
        private StoreLocation _storeLocation = StoreLocation.CurrentUser;
        private StoreName _storeName = StoreName.My;

        /// <summary>
        /// The store location of the default OCES root certificate, 
        /// e.g. "LocalMachine".
        /// </summary>
        public StoreLocation StoreLocation {
            get { return _storeLocation; }
            set { _storeLocation = value; }
        }

        /// <summary>
        /// The store name of the default OCES root certificate, e.g. "Root"
        /// </summary>
        public StoreName StoreName {
            get { return _storeName; }
            set { _storeName = value; }
        }

        /// <summary>
        /// The lookup action
        /// </summary>
        public LookupAction Action {
            get { return _action; }
            set { _action = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public LdapCertificateLookupTestConfig() { }
    }
}