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
using System.Xml.Serialization;

namespace dk.gov.oiosi.communication.configuration {

    /// <summary>
    /// Represents a mapping between between internal and external endpoint key type codes
    /// </summary>
    [XmlType("ProfileMapping", Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
    public class OioublProfileMapping {
        private string _name;
        private string _tModelGuid;

        /// <summary>
        /// Constructor
        /// </summary>
        public OioublProfileMapping() {
            _name = "";
            _tModelGuid = "";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Value</param>
        /// <param name="tModelGuid">Value mapping</param>
        public OioublProfileMapping(string name, string tModelGuid) {
            _name = name;
            _tModelGuid = tModelGuid;
        }

        /// <summary>
        /// Gets or set the value
        /// </summary>
        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the mapping
        /// </summary>
        public string TModelGuid {
            get { return _tModelGuid; }
            set { _tModelGuid = value; }
        }
    }
}
