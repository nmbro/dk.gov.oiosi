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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.communication {

    /// <summary>
    /// Representing RASP credentials
    /// </summary>
    public class RaspCredentials {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clientCertificate">the client certificate</param>
        /// <param name="serverCertificate">the server certificate</param>
        public RaspCredentials (
            OcesX509Certificate clientCertificate,
            OcesX509Certificate serverCertificate
        ) {
            _clientCertificate = clientCertificate;
            _serverCertificate = serverCertificate;
        }

        /// <summary>
        /// Property for client certificate
        /// </summary>
        public OcesX509Certificate ClientCertificate { get { return _clientCertificate; }}
        private OcesX509Certificate _clientCertificate;
        
        /// <summary>
        /// Property for server certificate
        /// </summary>
        public OcesX509Certificate ServerCertificate { get { return _serverCertificate; }}
        private OcesX509Certificate _serverCertificate;
    }
}