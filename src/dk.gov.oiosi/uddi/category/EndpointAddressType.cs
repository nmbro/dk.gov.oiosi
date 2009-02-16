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
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// Indicates the type of the AccessPoint entity, e.g. http, email or other type. 
    /// NOTE: the value of this category should probably be constrained to match a possible 
    /// reference to a binding tModel, so that e.g. a http binding is not implemented with 
    /// an email endpoint.
    /// </summary>
    public class EndpointAddressType : ArsCategory {

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static EndpointAddressType() {
            // 1. Set baseclass default values:
            _categoryId = IdentifierUtility.GetUddiIDFromString("uddi:14248882-f226-4caa-92ed-0a2ec40d3112");
            _categoryName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointAddressType/";
            _defaultKeyName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointAddressType/";
            _defaultKeyValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/http/";

            // 2. Set list of categories & possible values for each category
            string[] values = { "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/http/",
                "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/https/",
                "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/email/",
                "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/ftp/",
                "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/other/"
            };

            //Indicates an http address
            SetCategoryAndValues("http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointAddressType/", values);            
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public EndpointAddressType() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="endpointAddressType">endpoint addresstype</param>
        public EndpointAddressType(EndpointAddressTypeCode endpointAddressType) {
            switch (endpointAddressType) {
                case EndpointAddressTypeCode.http:
                    pValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/http/";
                    break;
                case EndpointAddressTypeCode.https:
                    pValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/https/";
                    break;
                case EndpointAddressTypeCode.email:
                    pValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/email/";
                    break;
                case EndpointAddressTypeCode.ftp:
                    pValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/ftp/";
                    break;
                case EndpointAddressTypeCode.other:
                    pValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/other/";
                    break;
                default:
                    pValue = "";
                    break;
            }
        }

        /// <summary>
        /// From an endpoint address, gets the UDDI key value code representing it.
        /// </summary>
        /// <returns></returns>
        public EndpointAddressTypeCode ToCode() {
            if (pValue == "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/http/")
                return EndpointAddressTypeCode.http;
            if (pValue == "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/https/")
                return EndpointAddressTypeCode.https;
            if (pValue == "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/email/")
                return EndpointAddressTypeCode.email;
            if (pValue == "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/ftp/")
                return EndpointAddressTypeCode.ftp;
            if (pValue == "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/other/")
                return EndpointAddressTypeCode.other;
            
            return EndpointAddressTypeCode.other;
        }

        #region ArsCategory abstract members

        private static UddiId _categoryId;

        /// <summary>
        /// Gets the category identifier (uuid)
        /// </summary>
        public override string CategoryID { get { return _categoryId.ToString(); } }

        private static string _categoryName;

        /// <summary>
        /// Gets the category name
        /// </summary>
        public override string CategoryName { get { return _categoryName; } }

        private static string _defaultKeyName;

        /// <summary>
        /// Gets the default category name
        /// </summary>
        public override string DefaultCategory { get { return _defaultKeyName; } }

        private static string _defaultKeyValue;

        /// <summary>
        /// Gets the default category value
        /// </summary>
        public override string DefaultCategoryValue { get { return _defaultKeyValue; } }

        #endregion

    }
}