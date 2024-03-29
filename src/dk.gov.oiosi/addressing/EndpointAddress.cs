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
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.addressing {

    /// <summary>
    /// Abstract base class of the address of an endpoint, e.g. a URL or e-mail address.
    /// Instantiate this class to represent a specific endpoint address type.
    /// </summary>
    public abstract class EndpointAddress {
        
        /// <summary>
        /// Endoint address type code
        /// </summary>
        public abstract EndpointAddressTypeCode EndpointAddressTypeCode { get; }

        /// <summary>
        /// Returns the endpoint address in a string representation
        /// </summary>
        /// <returns>Returns the business key in a string representation</returns>
        public abstract string GetKeyAsString ();


        /// <summary>
        /// Returns the type of endpoint address as a human readable string
        /// </summary>
        /// <returns>Returns the type of string as a human readable string</returns>
        public abstract string GetKeyTypeAsString ();

        /// <summary>
        /// Returns the UDDI UrlType as a human readable string
        /// </summary>
        /// <returns>UrlType as string</returns>
        public abstract string GetUrlTypeAsString();

        /// <summary>
        /// Gets Uri
        /// </summary>
        /// <returns></returns>
        public Uri GetAsUri() {
            string key = GetKeyAsString();
            Uri uri = new Uri(key);

            return uri;
        }

        /// <summary>
        /// Gets endpointaddress
        /// </summary>
        /// <returns></returns>
        public System.ServiceModel.EndpointAddress GetAsWCFEndpointAddress()
        {
            string key = GetKeyAsString();
            System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(key);

            return endpointAddress;
        }

    }
}