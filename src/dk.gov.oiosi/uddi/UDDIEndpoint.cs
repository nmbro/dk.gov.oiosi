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

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// This class represents an UDDI inquiry or publish API endpoint.
    /// </summary>
    public class UddiEndpoint {
        /// <summary>
        /// URL to the UDDI inquiry or publish API endpoint
        /// </summary>
        private Uri _uddiEndpoint;

        /// <summary>
        /// Updated dynamically by the UddiLookupClient from the status of the last call to this endpoint.
        /// Represents the time of the last successfull call to this UDDI inquiry or publish API.
        /// </summary>
        private DateTime _lastSuccessfullCall = DateTime.Now;

        /// <summary>
        /// Hide default constructor
        /// </summary>
        private UddiEndpoint () { }

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="uddiEndpoint">The URL of the UDDI inquiry or publish API endpoint</param>
        public UddiEndpoint (Uri uddiEndpoint) {
            _uddiEndpoint = uddiEndpoint;
        }
    }
}