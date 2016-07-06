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
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.common {

    /// <summary>
    /// Utilities for converting identifiers between strings and other types
    /// </summary>
    public class IdentifierUtility {
        public const string ANONYMOUS = " http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/anonymous";

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


        public static Identifier GetIdentifierFromKeyType(
            string endpointKey,
            string endpointKeyType)
        {
            ////Identifier identifier;
            //////EndpointKeyTypeCode code = ParseKeyTypeCode(endpointKeyType);
            ////switch (endpointKeyType.ToLowerInvariant())
            ////{
            ////    case "dk:cvr":                
            ////        {
            ////            identifier = new IdentifierCvr(endpointKeyType, endpointKey);
            ////            break;
            ////        }
            ////     case "dk:cpr":
            ////        {
            ////            identifier = new IdentifierNonePublic(endpointKeyType, endpointKey);
            ////            break;
            ////        }
            ////    default:
            ////        {
            ////            identifier = new Identifier(endpointKeyType, endpointKey);
            ////            break;
            ////        }
            ////}

            return new Identifier(endpointKeyType, endpointKey);
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