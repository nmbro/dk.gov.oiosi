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
    /// Link to a document defining the terms of use of the web service. OWSA 1.0 field.
    /// A hyperlink, e.g. "http://myserver.dk/agreements/terms.html"
    /// </summary>
    public class TermsOfUseUrl : ArsCategory {

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static TermsOfUseUrl() {
            // 1. Set baseclass default values:
            _categoryId = IdentifierUtility.GetUddiIDFromString("uddi:8cc1e1c1-d0f4-4bbe-90e0-90cd1976d944");
            _categoryName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/termsOfUse/";
            _defaultKeyName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/termsOfUse/";
            _defaultKeyValue = "";

            string[] values = { };

            SetCategoryAndValues("http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/termsOfUse/", values);
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public TermsOfUseUrl() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="termsOfUseUrl">Url for terms of use contract</param>
        public TermsOfUseUrl(Uri termsOfUseUrl) {
            pValue = termsOfUseUrl.AbsoluteUri;
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