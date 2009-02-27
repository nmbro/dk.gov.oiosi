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
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.common
{

    /// <summary>
    /// Utilities for converting identifiers between strings and other types
    /// </summary>
    public class IdentifierUtility
    {


        /// <summary>
        /// Returns an UDDI identifier (e.g. a tModel key) as an UddiId object 
        /// (e.g. UddiGuidId or UddiStringId). Throws an exception if the format
        /// is not right.
        /// </summary>
        /// <param name="uddiIdentifier">The UDDI identifier to convert</param>
        /// <returns>Returns the UddiId subclass intance. Throws an exception if the format is not right</returns>
        public static UddiId GetUddiIDFromString(string uddiIdentifier) {
            UddiId idObject;
            if (uddiIdentifier.ToLower().StartsWith("uddi:")) {
                if (UddiGuidId.IsValidGuidId(uddiIdentifier, true)) {
                    idObject = new UddiGuidId(uddiIdentifier, true);
                    return idObject;
                } else {
                    idObject = new UddiStringId(uddiIdentifier, true);
                    return idObject;
                }
            } else {
                if (UddiGuidId.IsValidGuidId(uddiIdentifier, false)) {
                    idObject = new UddiGuidId(uddiIdentifier, false);
                    return idObject;
                } else {
                    idObject = new UddiStringId(uddiIdentifier, false);
                    return idObject;
                }
            }
        }

        /// <summary>
        /// From an identifier string and identifier type, returns the right
        /// IIdentifier subclass
        /// </summary>
        /// <param name="endpointKey">The endpoint key as string</param>
        /// <param name="endpointKeyType">The type of the key</param>
        /// <returns>Returns the relevant IIdentifier subclass</returns>
        public static IIdentifier GetIdentifierFromKeyType(
            string endpointKey,
            EndpointKeyTypeCode endpointKeyType
        ) {
            IIdentifier id;
            switch (endpointKeyType) {
                case EndpointKeyTypeCode.cvr:
                    id = new IdentifierCvr(endpointKey);
                    break;
                case EndpointKeyTypeCode.ean:
                    id = new IdentifierEan(endpointKey);
                    break;
                case EndpointKeyTypeCode.ovt:
                    id = new IdentifierOvt(endpointKey);
                    break;
                case EndpointKeyTypeCode.p:
                    id = new IdentifierP(endpointKey);
                    break;
                case EndpointKeyTypeCode.se:
                    id = new IdentifierSe(endpointKey);
                    break;
                case EndpointKeyTypeCode.vans:
                    id = new IdentifierVans(endpointKey);
                    break;
                case EndpointKeyTypeCode.iban:
                    id = new IdentifierIban(endpointKey);
                    break;
                case EndpointKeyTypeCode.duns:
                    id = new IdentifierDuns(endpointKey);
                    break;
                default:
                    throw new UnknownEndpointTypeException(endpointKeyType);
            }

            return id;
        }


        /// <summary>
        /// Takes an endpoint an returns a endpointaddress
        /// </summary>
        /// <param name="endpointAddress">the endpoint address</param>
        /// <returns>a specific endpointaddress</returns>
        public static EndpointAddress GetEndpointAddressFromString(string endpointAddress) {

            EndpointAddress address = null;

            if (endpointAddress.ToLower().StartsWith("http://")) {
                address = new EndpointAddressHttp(new Uri(endpointAddress));
            } else if (endpointAddress.ToLower().StartsWith("mailto:")) {
                address = new EndpointAddressSMTP(new Uri(endpointAddress));
            } else {
                address = new EndpointAddressSMTP(new System.Net.Mail.MailAddress(endpointAddress));
            }
            return address;
        }
    }
}