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

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// For backwards compatibility with the v. 1 recommendations for storing WSDL in UDDI.
    /// </summary>
    public class UddiOrgTypes : ArsCategory {

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static UddiOrgTypes() {
            // 1. Set baseclass default values:
            // Systinent UDDI does not use the uddi: convention for these taxonomies
            //_categoryId = new UddiGuidId("uddi:c1acf26d-9672-4404-9d70-39b756e62ab4", true);
            _categoryId = new UddiNonGuidId("uddi:uddi.org:categorization:types");
            _categoryName = "uddi-org:types";
            _defaultKeyName = "uddi-org:types";
            _defaultKeyValue = "wsdlSpec";

            // 2. Set list of categories & possible values for each category
            string[] values = { "wsdlSpec" };

            SetCategoryAndValues("uddi-org:types", values);
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public UddiOrgTypes() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        public UddiOrgTypes(UddiOrgTypesCode uddiOrgTypes) {

            switch (uddiOrgTypes) {
                case UddiOrgTypesCode.wsdlSpec:
                    pValue = "wsdlSpec";
                    break;
                default:
                    pValue = "";
                    break;
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