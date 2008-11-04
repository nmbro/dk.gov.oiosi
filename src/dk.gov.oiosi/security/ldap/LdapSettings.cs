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
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.security.ldap {
    /// <summary>
    /// The settings information with information about different connection settings.
    /// The settings are server hostname, server port, connection timeout, search on 
    /// the server timeout, search from the client timeout and the maximum number of 
    /// results.
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class LdapSettings{
        private string _host;
        private short _port;
        private short _connectionTimeoutMsec;
        private short _searchServerTimeoutMsec;
        private short _searchClientTimeoutMsec;
        private short _maxResults;

        /// <summary>
        /// Gets and sets Host
        /// </summary>
        public string Host {
            get { return _host; }
            set { _host = value; }
        }

        /// <summary>
        /// Gets and sets the port.
        /// </summary>
        public short Port {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// Gets and sets the connection timeout.
        /// </summary>
        public short ConnectionTimeoutMsec {
            get { return _connectionTimeoutMsec; }
            set { _connectionTimeoutMsec = value; }
        }

        /// <summary>
        /// Gets and sets the search server timeout.
        /// </summary>
        public short SearchServerTimeoutMsec {
            get { return _searchServerTimeoutMsec; }
            set { _searchServerTimeoutMsec = value; }
        }

        /// <summary>
        /// Gets and sets the search client timeout.
        /// </summary>
        public short SearchClientTimeoutMsec {
            get { return _searchClientTimeoutMsec; }
            set { _searchClientTimeoutMsec = value; }
        }

        /// <summary>
        /// Get and sets the maximum number of results.
        /// </summary>
        public short MaxResults {
            get { return _maxResults; }
            set { _maxResults = value; }
        }
    }
}