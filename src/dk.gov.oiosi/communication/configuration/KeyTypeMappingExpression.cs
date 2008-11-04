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
using System.Xml.Serialization;

namespace dk.gov.oiosi.communication.configuration {
    /// <summary>
    /// Contains mapping expression
    /// </summary>
    [XmlType("KeyTypeMappingExpressions", Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
    public class KeyTypeMappingExpression {
        private string _name;
        private string _xpathExpression;
        private Dictionary<string, KeyTypeMapping> _mappings;

        /// <summary>
        /// Default constructor used by the XmlSerializer.
        /// </summary>
        /// <remarks>Do not use this constructor</remarks>
        public KeyTypeMappingExpression() : this("", "") { }

        /// <summary>
        /// Constructor that creates a mapping expression with the given name
        /// and xpath expression. Further it has no mappings.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="xpathExpression"></param>
        public KeyTypeMappingExpression(string name, string xpathExpression) {
            _name = name;
            _xpathExpression = xpathExpression;
            _mappings = new Dictionary<string, KeyTypeMapping>();
        }

        /// <summary>
        /// Constructor that creates a mapping expression with the given name, xpath 
        /// expression and mappings. It copies the mappings from the IEnumerable given.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="xpathExpression"></param>
        /// <param name="mappings"></param>
        public KeyTypeMappingExpression(string name, string xpathExpression, IEnumerable<KeyTypeMapping> mappings) {
            _name = name;
            _xpathExpression = xpathExpression;
            SetMappings(mappings);
        }

        /// <summary>
        /// Gets and sets the name.
        /// </summary>
        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets and sets the xpath expression.
        /// </summary>
        public string XPathExpression {
            get { return _xpathExpression; }
            set { _xpathExpression = value; }
        }

        /// <summary>
        /// Gets and sets the mappings. It is used by the xml serilizer,
        /// it should not be used otherwise.
        /// </summary>
        /// <remarks> This property should not be used.</remarks>
        public KeyTypeMapping[] Mappings {
            get {
                KeyTypeMapping[] mappings = new KeyTypeMapping[_mappings.Values.Count];
                uint i = 0;
                foreach (KeyTypeMapping mapping in _mappings.Values) {
                    mappings[i++] = mapping;
                }
                return mappings;
            }
            set {
                SetMappings(value);
            }
        }

        /// <summary>
        /// Gets a mapping with a given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public KeyTypeMapping GetMapping(string value) {
            try {
                return _mappings[value];
            } 
            catch (Exception ex) {
                throw new KeyTypeMappingFailedException(value, ex);
            }
        }

        /// <summary>
        /// Adds a mapping.
        /// </summary>
        /// <param name="mapping"></param>
        public void AddMapping(KeyTypeMapping mapping) {
            _mappings.Add(mapping.Value, mapping);
        }

        /// <summary>
        /// Removes the mapping with the given name
        /// </summary>
        /// <param name="value"></param>
        public void RemoveMapping(string value) {
            _mappings.Remove(value);
        }

        private void SetMappings(IEnumerable<KeyTypeMapping> mappings) {
            _mappings.Clear();
            foreach (KeyTypeMapping mapping in mappings) {
                _mappings.Add(mapping.Value, mapping);
            }
        }
    }
}
