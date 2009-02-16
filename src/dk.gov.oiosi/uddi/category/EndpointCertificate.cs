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
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.common.validation;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// Certificate subject string.
    /// In UDDI 2.0, possible attributes for holding this value are no longer than 255 chars. 
    /// We reference the certificate with the certificate subject serialnumber: KeyedReference in the 
    /// IdentifierBag associated with the TModel for the web service. NOTE: we do this in UDDI 3.0 as well.
    /// </summary>
    public class EndpointCertificate : ArsCategory {
        public const string DEFAULTCATEGORYID = "uddi:e6feac92-0aee-4824-ae02-882760609e8a";
        public const string DEFAULTCATEGORYNAME = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/endpointCertificate/";
        public const string DEFAULTCATEGORYKEYNAME = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/endpointCertificate/";
        public const string DEFAULTCATEGORYKEYVALUE = "";

        private UddiId _categoryId;
        private string _categoryName;


        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static EndpointCertificate() {
            string[] values = { };
            SetCategoryAndValues(DEFAULTCATEGORYNAME, values);
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public EndpointCertificate() {
            _categoryId = IdentifierUtility.GetUddiIDFromString(DEFAULTCATEGORYID);
            _categoryName = DEFAULTCATEGORYNAME;

        }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="endpointCertificate">endpoint certificate serial</param>
        public EndpointCertificate(string endpointCertificateSubject) : this() {
            pValue = endpointCertificateSubject;
        }

        
        /// <summary>
        /// Validate Value within the categorylist idenfied by Category
        /// </summary>
        /// <param name="ValueName"></param>
        /// <param name="Required"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public override bool IsValid(string ValueName, bool Required, ref ValidationFailureCollection Failures) {
            bool isValid = true;

            if (!ValUtil.IsEmpty(Value)) {
                //TODO: this value is allways true what is the expected result here.
                if (!isValid) {
                    DataValidationFailure.AddFailure(InvalidCertificateValueFailure.Message(Value), ValueName, typeof(ArsCategory), ref Failures);
                    isValid = false;
                }
            } else
                // If field is required then it must not be null
                if (Required) {
                    DataValidationFailure.AddFailure(
                        RequiredFieldFailure.Message(ValueName), ValueName, typeof(ArsCategory), ref Failures);
                    isValid = false;
                }

            return isValid;
        }

        /// <summary>
        /// Validate Value within the categorylist idenfied by Category
        /// </summary>
        /// <param name="ValueName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public override bool IsValid(string ValueName, ref ValidationFailureCollection Failures) {
            return IsValid(ValueName, false, ref Failures);
        }
        
        #region ArsCategory abstract members

        /// <summary>
        /// Gets the category identifier (uuid)
        /// </summary>
        public override string CategoryID { get { return _categoryId.ToString(); } }

        /// <summary>
        /// Gets the category name
        /// </summary>
        public override string CategoryName { get { return _categoryName; } }

        /// <summary>
        /// Gets the default category name
        /// </summary>
        public override string DefaultCategory { get { return DEFAULTCATEGORYKEYNAME; } }

        /// <summary>
        /// Gets the default category value
        /// </summary>
        public override string DefaultCategoryValue { get { return DEFAULTCATEGORYKEYVALUE; } }

        #endregion
    }
}