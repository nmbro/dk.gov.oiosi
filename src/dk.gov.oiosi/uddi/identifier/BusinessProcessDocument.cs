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
using System.Collections.Generic;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.identifier {

    /// <summary>
    /// An identifier representing a specific business process, e.g. an UBL process. Note that 
    /// OASIS has defined a tModel format specifically for BPEL4WS. If such a document is referenced, 
    /// that tModel format shoud be used instead.
    /// </summary>
    public class BusinessProcessDocument : ArsIdentifier {

        private static List<string> _possibleValues = new List<string>();
        private static UddiId _identifierId;
        private static string _identifierName;
        private static string _defaultIdentifierValue = "";
        private static bool _defaultValueExists = false;
        private static bool _fixedValueListExists = false;

        /// <summary>
        /// Static constructor. Initializes name, id and default valute of the identifier
        /// </summary>
        static BusinessProcessDocument() {
            _identifierId = IdentifierUtility.GetUddiIDFromString("uddi:9111dd24-6734-407f-b949-d601ab427520");
            _identifierName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/businessProcessIdentifier/";
            _defaultValueExists = false;
            _fixedValueListExists = false;
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public BusinessProcessDocument() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="businessProcessDocument">Identifier for a specific business process</param>
        public BusinessProcessDocument(string businessProcessDocument) {
            pValue = businessProcessDocument;

        }

        #region ArsIdentifier abstract fields

        /// <summary>
        /// Gets the UDDI name of the identifier
        /// </summary>
        public override string IdentifierName { get { return _identifierName; } }

        /// <summary>
        /// Gets the UDDI tModel id of the identifier
        /// </summary>
        public override string IdentifierID { get { return _identifierId.ID; } }

        /// <summary>
        /// True if a default value exists
        /// </summary>
        public override bool DefaultValueExists { get { return _defaultValueExists; } }

        /// <summary>
        /// True if a predefined list of possible values exist for this identifier
        /// </summary>
        public override bool FixedValueListExists { get { return _fixedValueListExists; } }

        /// <summary>
        /// Returns the default value of this identifier if any exist, else returns null.
        /// You may check "DefaultValueExists" before calling.
        /// </summary>
        public override string DefaultValue { get { return _defaultIdentifierValue; } }

        /// <summary>
        /// Returns a list of possible values
        /// </summary>
        public override List<string> PossibleValues {
            get {
                return _possibleValues;
            }
        }

        #endregion

    }
}