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
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// UDDI category representing an OIO taxonomy
    /// </summary>
    public class OIOTaxonomy : ArsCategory {

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static OIOTaxonomy() {
            // 1. Set baseclass default values:
            _categoryId = new UddiGuidId("uddi:f699264c-384d-47a2-bb46-c6a476242e55", true);
            _categoryName = "OIO subject scheme";
            _defaultKeyName = "OIO subject scheme / TRADE AND INDUSTRY / E-COMMERCE /";
            _defaultKeyValue = "uddi:ea4bc88f-9479-4f9b-a354-4acabdb99336";

            string[] values = {"OIO subject scheme / CULTURE / LITERATURE AND LANGUAGES /",
                "OIO subject scheme / TRADE AND INDUSTRY / E-COMMERCE /",
                "uddi:41d2b968-d99c-4496-8b9e-1d67f43a4fbc",
                "uddi:ea4bc88f-9479-4f9b-a354-4acabdb99336",
                "uddi:f699264c-384d-47a2-bb46-c6a476242e55"
            };

            SetCategoryAndValues("OIO subject scheme", values);
            SetCategoryAndValues("OIO subject scheme / TRADE AND INDUSTRY / E-COMMERCE /", new string[] { "uddi:ea4bc88f-9479-4f9b-a354-4acabdb99336" });
            SetCategoryAndValues("OIO subject scheme / CULTURE / LITERATURE AND LANGUAGES /", new string[] { "uddi:41d2b968-d99c-4496-8b9e-1d67f43a4fbc" });
        }

        /// <summary>
        /// Override of baseclass method. Sets the category name and value, 
        /// but does not check the validity of the value.
        /// </summary>
        /// <param name="category">The category name</param>
        /// <param name="value">The category value</param>
        public override void SetCategoryValue(string category, string value) {
            if (category == null) throw new NullArgumentException("category");
            if (value == null) throw new NullArgumentException("value");
            pKey = category;
            pValue = value;
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public OIOTaxonomy() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="category">readable taxonomyname</param>
        /// <param name="categoryGuid">guid for the taxonomy</param>
        public OIOTaxonomy(string category, string categoryGuid) {
            pKey = category;
            pValue = categoryGuid;
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