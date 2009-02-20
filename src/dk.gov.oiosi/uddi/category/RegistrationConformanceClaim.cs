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
    /// Claims regarding conformance to the UDDI registration model, 
    /// e.g. the registration model proposed in this document.
    /// </summary>
    public class RegistrationConformanceClaim : ArsCategory, IEquatable<RegistrationConformanceClaim> {

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static RegistrationConformanceClaim() {
            // 1. Set baseclass default values:
            _categoryId = IdentifierUtility.GetUddiIDFromString("uddi:80496ef5-4d24-4788-a3f8-12fb54a75106");
            _categoryName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/registrationConformanceClaim/";
            _defaultKeyName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/registrationConformanceClaim/";
            _defaultKeyValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.1/";

            // 2. Set list of categories & possible values for each category
            string[] values = { "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/registrationModel/1.0",
                                "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.0/",
                                "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.1/"
            };
            SetCategoryAndValues("http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/registrationConformanceClaim/", values);           
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public RegistrationConformanceClaim() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="registrationConformanceClaim">registrationconformanceclaim profile</param>
        public RegistrationConformanceClaim(RegistrationConformanceClaimCode registrationConformanceClaim) {
            switch (registrationConformanceClaim) {
                case RegistrationConformanceClaimCode.owsa1_0:
                    pValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/registrationModel/1.0";
                    break;
                case RegistrationConformanceClaimCode.oiosi1_0:
                    pValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.0/";
                    break;
                case RegistrationConformanceClaimCode.oiosi1_1:
                    pValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.1/";
                    break;
                default:
                    pValue = "";
                    break;
            }
        }

        public static RegistrationConformanceClaimCode GetRegistrationConformanceClaimCode(string registrationConformanceClaimValue) {
            switch (registrationConformanceClaimValue) {
                case "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/registrationModel/1.0":
                    return RegistrationConformanceClaimCode.owsa1_0;
                case "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.0/":
                    return RegistrationConformanceClaimCode.oiosi1_0;
                case "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.1/":
                    return RegistrationConformanceClaimCode.oiosi1_1;
                default:
                    throw new Exception("ConformanceClaim not known: " + registrationConformanceClaimValue);
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


        #region IEquatable<RegistrationConformanceClaim> Members

        /// <summary>
        /// Compares the two objects and returns true if they have equal values
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Returns true if the two objects have identical values</returns>
        public bool Equals(RegistrationConformanceClaim other) {
            if (other == null) return false;

            if (Category != other.Category) return false;
            if (Value != other.Value) return false;
            return true;
        }

        #endregion
    }
}