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
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// Type of the organization identifier, e.g. CVR.
    /// </summary>
    public class OrganizationKeyType : ArsCategory {

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static OrganizationKeyType() {
            // 1. Set baseclass default values:
            _categoryId = IdentifierUtility.GetUddiIDFromString("uddi:0debdbf2-1a1b-46e8-b7fe-7263dc939242");
            _categoryName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/organizationKeyType/";
            _defaultKeyName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/organizationKeyType/";
            _defaultKeyValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/cvrNumber/";

            // 2. Set list of categories & possible values for each category
            string[] values = { "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/cvrNumber/",
                                "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/ovtNumber/"
            };

            SetCategoryAndValues("http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/organizationKeyType/", values);
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public OrganizationKeyType() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="organizationKeyTypeCode">Organization keytype; cvr or ovt</param>
        public OrganizationKeyType(OrganizationKeyTypeCode organizationKeyTypeCode) {

            switch (organizationKeyTypeCode) {
                case OrganizationKeyTypeCode.cvrNumber:
                    pValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/cvrNumber/";
                    break;
                case OrganizationKeyTypeCode.ovtNumber:
                    pValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/ovtNumber/";
                    break;
                default:

                    pValue = "";
                    break;
            }            
        }

        /// <summary>
        /// Returns the type of the organization key
        /// </summary>
        /// <returns>Returns the type of the organization key</returns>
        public OrganizationKeyTypeCode GetOrganizationKeyTypeCode() {
            switch (pValue) {
                case "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/cvrNumber/":
                    return OrganizationKeyTypeCode.cvrNumber;
                case "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/ovtNumber/":
                    return OrganizationKeyTypeCode.ovtNumber;
                default:
                    return OrganizationKeyTypeCode.unknownNumber;
            }
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