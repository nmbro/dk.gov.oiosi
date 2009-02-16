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

namespace dk.gov.oiosi.uddi.identifier {

    /// <summary>
    /// Link to a document containing technical description of all interfaces of the 
    /// service definition (other than WSDL).
    /// Example: http://testdomain.com/documentation.pdf
    /// </summary>
    public class ServiceDocumentationUrl : ArsIdentifier {

        private static List<string> _possibleValues = new List<string>();
        private static UddiId _identifierId;
        private static string _identifierName;
        private static string _defaultIdentifierValue = "";
        private static bool _defaultValueExists = false;
        private static bool _fixedValueListExists = false;

        /// <summary>
        /// Static constructor. Initializes name, id and default valute of the identifier
        /// </summary>
        static ServiceDocumentationUrl() {
            _identifierId = IdentifierUtility.GetUddiIDFromString("uddi:a2d88813-d3db-4890-b1dc-170215106896");
            _identifierName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/documentationURL/";
            _defaultValueExists = false;
            _fixedValueListExists = false;
        }

        /// <summary>
        /// Default constructor. Sets to default.
        /// </summary>
        public ServiceDocumentationUrl() { }

        /// <summary>
        /// Use this constructor to set a value
        /// </summary>
        /// <param name="serviceDocumentationUrl">Url for the servicedocumentation</param>
        public ServiceDocumentationUrl(Uri serviceDocumentationUrl) {
            pValue = serviceDocumentationUrl.AbsoluteUri;
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