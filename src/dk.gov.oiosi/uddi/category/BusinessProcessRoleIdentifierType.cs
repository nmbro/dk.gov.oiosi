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
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// Identifies a type of role identifier. The type indicates the format of a role identifier. 
    /// </summary>
    public class BusinessProcessRoleIdentifierType : ArsCategory {
        public const string DEFAULTUDDIID = "uddi:8dd0fa3e-be33-47f9-847b-8d974952a8dc";
        public const string DEFAULTNAME = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessRoleIdentifierType/";
        public const string DEFAULTKEYNAME = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessRoleIdentifierType/";
        public const string DEFAULTKEYVALUE = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/UBL2.0ProcessRole/";

        /// <summary>
        /// Static constructor. Sets list of categories and possible values for each.
        /// </summary>
        static BusinessProcessRoleIdentifierType() {
            //  Set list of categories & possible values for each category
            string[] values = { DEFAULTKEYVALUE };
            SetCategoryAndValues(DEFAULTKEYNAME, values);
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public BusinessProcessRoleIdentifierType() { }

        /// <summary>
        /// Constructs a BusinessProcessIdentifierType from a keyed reference
        /// </summary>
        /// <param name="keyRef"></param>
        public BusinessProcessRoleIdentifierType(KeyedReference keyRef) {
            SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
        }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        public BusinessProcessRoleIdentifierType(BusinessProcessRoleIdentifierTypeCode businessProcessRoleIdentifierType) {

            switch (businessProcessRoleIdentifierType) {
                case BusinessProcessRoleIdentifierTypeCode.ubl2_0_ProcessRole:
                    pValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/UBL2.0ProcessRole/";
                    break;
                default:
                    pValue = "";
                    break;
            }
        }

        #region ArsCategory abstract members

        /// <summary>
        /// Gets the category identifier (uuid)
        /// </summary>
        public override string CategoryID {
            get { return DEFAULTUDDIID; }
        }

        /// <summary>
        /// Gets the category name
        /// </summary>
        public override string CategoryName {
            get { return DEFAULTNAME; }
        }

        /// <summary>
        /// Gets the default category name
        /// </summary>
        public override string DefaultCategory {
            get { return DEFAULTKEYNAME; }
        }

        /// <summary>
        /// Gets the default category value
        /// </summary>
        public override string DefaultCategoryValue {
            get { return DEFAULTKEYVALUE; }
        }

        #endregion

        /// <summary>
        /// String value of code
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return pValue;
        }
    }
}