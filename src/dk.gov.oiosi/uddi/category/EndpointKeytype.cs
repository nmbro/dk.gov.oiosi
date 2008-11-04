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

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// Indicates the type of the EndpointKey attribute, e.g. an EAN location number.
    /// </summary>
    public class EndpointKeytype : ArsCategory, IEquatable<EndpointKeytype> {

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static EndpointKeytype() {
            // 1. Set baseclass default values:
            _categoryId = new UddiGuidId("uddi:182a4a2b-3717-4283-b97c-55cc3b684dae", true);
            _categoryName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointKeyType/";
            _defaultKeyName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointKeyType/";
            _defaultKeyValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/eanNumber/";

            // 2. Set list of categories & possible values for each category
            string[] values = { "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/eanNumber/",
                                "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/ovtNumber/",
                                "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/cvrNumber/"
            };

            //Indicates an EAN number type
            SetCategoryAndValues("http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointKeyType/", values);
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public EndpointKeytype() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="endpointKeyType">endpoint keytype</param>
        public EndpointKeytype(EndpointKeyTypeCode endpointKeyType) {
            switch (endpointKeyType) {
                case EndpointKeyTypeCode.ean:
                    pValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/eanNumber/";
                    break;
                case EndpointKeyTypeCode.ovt:
                    pValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/ovtNumber/";
                    break;
                case EndpointKeyTypeCode.cvr:
                    pValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/cvrNumber/";
                    break;
                default:
                    throw new UnknownEndpointTypeException(endpointKeyType);
            }
        }

        /// <summary>
        /// Gets the EndpointKeyType enum value
        /// </summary>
        /// <returns>EndpointKeyType enum value</returns>
        public EndpointKeyTypeCode GetEndpointKeyTypeCode() {
            switch (Value) {
                case "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/eanNumber/":
                    return EndpointKeyTypeCode.ean;
                case "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/ovtNumber/":
                    return EndpointKeyTypeCode.ovt;
                case "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/cvrNumber/":
                    return EndpointKeyTypeCode.cvr;
                default:
                    throw new UnknownEndpointTypeException(Value);
            }
        }

        #region ArsCategory abstract members

        private static UddiId _categoryId;

        /// <summary>
        /// Gets the category identifier (uuid)
        /// </summary>
        public override string CategoryID { get { return _categoryId.ToString(); } }

        private static string _categoryName = "";

        /// <summary>
        /// Gets the category name
        /// </summary>
        public override string CategoryName { get { return _categoryName; } }

        private static string _defaultKeyName = "";

        /// <summary>
        /// Gets the default category name
        /// </summary>
        public override string DefaultCategory { get { return _defaultKeyName; } }

        private static string _defaultKeyValue = "";

        /// <summary>
        /// Gets the default category value
        /// </summary>
        public override string DefaultCategoryValue { get { return _defaultKeyValue; } }

        #endregion

        #region IEquatable<EndpointKeytype> Members

        /// <summary>
        /// Compares the two objects and returns true if they have equal values
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Returns true if the two objects have identical values</returns>
        public bool Equals(EndpointKeytype other) {
            if (other == null) return false;

            if (GetEndpointKeyTypeCode() != other.GetEndpointKeyTypeCode()) return false;
            return true;
        }

        #endregion
    }
}