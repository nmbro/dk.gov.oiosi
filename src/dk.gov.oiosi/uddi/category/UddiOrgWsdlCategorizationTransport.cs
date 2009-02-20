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
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// Uddiorg wsdl categorization transport category
    /// </summary>
    public class UddiOrgWsdlCategorizationTransport : ArsCategory {
        public const string CATEGORYID = "uddi:uddi.org:wsdl:categorization:transport";
        public const string DEFAULTCATEGORYKEYNAME = "uddi-org:wsdl:categorization:transport";
        private const string _httpValue = "uddi:uddi.org:transport:http";
        private const string _smtpValue = "uddi:uddi.org:transport:smtp";

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static UddiOrgWsdlCategorizationTransport() {
            // 1. Set baseclass default values:
            // Systinent UDDI does not use the uddi: convention for these taxonomies
            _categoryId = IdentifierUtility.GetUddiIDFromString("uddi:uddi.org:wsdl:categorization:transport");
            _categoryName = "uddi-org:wsdl:categorization:transport";
            _defaultKeyName = "uddi-org:wsdl:categorization:transport";
            _defaultKeyValue = "uddi:uddi.org:transport:http";

            // 2. Set list of categories & possible values for each category
            string[] values = { _httpValue, _smtpValue};

            //http protocol. Default.
            SetCategoryAndValues(CATEGORYID, values);
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public UddiOrgWsdlCategorizationTransport() { }

        public UddiOrgWsdlCategorizationTransport(KeyedReference reference) {
            if (!reference.TmodelKey.Equals(CATEGORYID, StringComparison.CurrentCultureIgnoreCase)) throw new ArgumentException("reference");
            pValue = reference.KeyValue;
        }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        public UddiOrgWsdlCategorizationTransport(UddiOrgWsdlCategorizationTransportCode uddiOrgWsdlCategorizationTransport) {

            switch (uddiOrgWsdlCategorizationTransport) {
                case UddiOrgWsdlCategorizationTransportCode.http:
                    pValue = _defaultKeyValue;
                    break;
                case UddiOrgWsdlCategorizationTransportCode.smtp:
                    pValue = _smtpValue;
                    break;
                default:
                    pValue = "";
                    break;
            }
        }

        /// <summary>
        /// Returns a code representing the transport
        /// </summary>
        /// <returns>Returns a code representing the transport</returns>
        public UddiOrgWsdlCategorizationTransportCode ToCode() {
            if (pValue == _smtpValue) return UddiOrgWsdlCategorizationTransportCode.smtp;
            if (pValue == _defaultKeyValue) return UddiOrgWsdlCategorizationTransportCode.http;

            return UddiOrgWsdlCategorizationTransportCode.http;
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