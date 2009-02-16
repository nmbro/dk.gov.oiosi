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
  *   Dennis S�gaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.uddi.ars;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// Date of activation of the endpoint. Clients may use this information to cache 
    /// endpoint addresses.
    /// Activation date in UTC in the following format: "yyyy-MM-ddTHH:mm:ss.fffffffZ", e.g. "2006-08-02T11:05:00.0000000Z"
    /// </summary>
    public class EndpointActivationDate : ArsCategory {

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static EndpointActivationDate() {
            // 1. Set baseclass default values:
            _categoryId = IdentifierUtility.GetUddiIDFromString("uddi:B5449299-B951-4266-9952-4C4470970782");
            _categoryName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointActivationDate/";
            _defaultKeyName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointActivationDate/";
            _defaultKeyValue = "";

            string[] values = { };

            SetCategoryAndValues("http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointActivationDate/", values);
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public EndpointActivationDate() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="endpointActivationDate">endpoint Activation date; "yyyy-MM-ddTHH:mm:ss.fffffffZ"
        /// If a local date is used here, it is converted to UTC</param>
        public EndpointActivationDate(DateTime endpointActivationDate) {

            try {
                DateTime tmpTime = endpointActivationDate;
                // 1. Convert from local time to UTC:
                if (tmpTime.Kind != DateTimeKind.Utc) {
                    tmpTime = tmpTime.ToUniversalTime();
                }

                // 2. Make sure string is not converted to local date:
                pValue = tmpTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Returns the value of this instance as a local time converted DateTime
        /// </summary>
        /// <returns>Returns the value of this instance as a local time converted DateTime</returns>
        public DateTime GetAsLocalDateTime() {
            DateTime localDateTime = DateTime.Parse(
                pValue,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AdjustToUniversal);

            return localDateTime.ToLocalTime();
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