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
    /// Minor version of the web service. This field MUST NOT be empty. A positive 32-bit integer.
    /// Example: "0" or "53"
    /// </summary>
    public class VersionMinor : ArsCategory {

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static VersionMinor() {
            // 1. Set baseclass default values:
            _categoryId = new UddiGuidId("uddi:ba4afec4-f6c1-4205-8212-5eb0472000f9", true);
            _categoryName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/serviceVersionMinor/";
            _defaultKeyName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/serviceVersionMinor/";
            _defaultKeyValue = "0";

            string[] values = { };

            SetCategoryAndValues("http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/serviceVersionMinor/", values);
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public VersionMinor() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="versionMinor">minor version</param>
        public VersionMinor(string versionMinor) {
            pValue = versionMinor;
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