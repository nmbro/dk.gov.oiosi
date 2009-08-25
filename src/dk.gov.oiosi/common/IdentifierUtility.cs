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
        public static Identifier GetIdentifierFromKeyType(
            string endpointKey,
            EndpointKeyTypeCode endpointKeyType
        ) {
            Identifier id;
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
                case EndpointKeyTypeCode.cpr:
                    id = new IdentifierCpr(endpointKey);
                    break;
                default:
                    throw new UnknownEndpointTypeException(endpointKeyType);
            }

            return id;
        }


        public static Identifier GetIdentifierFromKeyType(
            string endpointKey,
            string endpointKeyType
        ) {
            EndpointKeyTypeCode code = ParseKeyTypeCode(endpointKeyType);
            switch (code) {
                case EndpointKeyTypeCode.cvr:
                    return new IdentifierCvr(endpointKey);
                case EndpointKeyTypeCode.ean:
                    return new IdentifierEan(endpointKey);
                case EndpointKeyTypeCode.ovt:
                    return new IdentifierOvt(endpointKey);
                case EndpointKeyTypeCode.p:
                    return new IdentifierP(endpointKey);
                case EndpointKeyTypeCode.se:
                    return new IdentifierSe(endpointKey);
                case EndpointKeyTypeCode.vans:
                    return new IdentifierVans(endpointKey);
                case EndpointKeyTypeCode.iban:
                    return new IdentifierIban(endpointKey);
                case EndpointKeyTypeCode.duns:
                    return new IdentifierDuns(endpointKey);
                case EndpointKeyTypeCode.cpr:
                    return new IdentifierCpr(endpointKey);
                default:
                    throw new UnknownEndpointTypeException(code);
            }
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

        /// <summary>
        /// Parses the key type code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static EndpointKeyTypeCode ParseKeyTypeCode(string code) {
            switch (code) {
                case "cvr":
                    return EndpointKeyTypeCode.cvr;
                case "ean":
                    return EndpointKeyTypeCode.ean;
                case "ovt":
                    return EndpointKeyTypeCode.ovt;
                case "p":
                    return EndpointKeyTypeCode.p;
                case "se":
                    return EndpointKeyTypeCode.se;
                case "vans":
                    return EndpointKeyTypeCode.vans;
                case "iban":
                    return EndpointKeyTypeCode.iban;
                case "duns":
                    return EndpointKeyTypeCode.duns;
                case "cpr":
                    return EndpointKeyTypeCode.cpr;
                default:
                    return EndpointKeyTypeCode.other;
            }
        }
    }
}