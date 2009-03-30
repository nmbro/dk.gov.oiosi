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
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System.Collections.Generic;
using System.Xml.Serialization;

namespace dk.gov.oiosi.communication.configuration {
    /// <summary>
    /// Xpath to a service endpoint unique ID (such as e.g. an EAN number), used for UDDI lookups,
    /// and an endpoint ID type.
    /// </summary>
    [XmlType("EndpointKey", Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
    public class ServiceEndpointKey {
        private string _xPath = "";
        private Dictionary<string, KeyTypeMappingExpression> _mappingExpressions = new Dictionary<string, KeyTypeMappingExpression>();

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceEndpointKey() { }

        /// <summary>
        /// The key for finding the endpoint in a UDDI lookup
        /// </summary>
        /// <param name="xpath">The xpath expression to where in the document the key can be found</param>
        public ServiceEndpointKey(string xpath) {
            _xPath = xpath;
        }

        /// <summary>
        /// XPath expression, to where in the UBL document the key can be found
        /// </summary>
        [XmlElement("Xpath")]
        public string XPath { 
            get { return _xPath; } 
            set { _xPath = value; } 
        }

        /// <summary>
        /// Gets and sets the mapping expressions. Used by the xml serielizer, should
        /// not by used otherwise
        /// </summary>
        /// <remarks>This property should not be used.</remarks>
        [XmlArray("MappingExpressions")]
        public KeyTypeMappingExpression[] MappingExpressions {
            get {
                KeyTypeMappingExpression[] mappingExpressions = new KeyTypeMappingExpression[_mappingExpressions.Values.Count];
                uint i = 0;
                foreach (KeyTypeMappingExpression mappingExpression in _mappingExpressions.Values) {
                    mappingExpressions[i++] = mappingExpression;
                }
                return mappingExpressions; 
            }
            set { SetMappingExpressions(value); }
        }

        /// <summary>
        /// Gets a mapping expression with a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public KeyTypeMappingExpression GetMappingExpression(string name) {
            return _mappingExpressions[name];
        }

        /// <summary>
        /// Add a mapping expression
        /// </summary>
        /// <param name="mappingExpression"></param>
        /// <returns></returns>
        public void AddMappingExpression(KeyTypeMappingExpression mappingExpression) {
            _mappingExpressions.Add(mappingExpression.Name, mappingExpression);
        }

        /// <summary>
        /// Removes a mapping expression
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void RemoveMappingExpression(string name) {
            _mappingExpressions.Remove(name);
        }

        private void SetMappingExpressions(IEnumerable<KeyTypeMappingExpression> mappingExpressions) {
            _mappingExpressions.Clear();
            foreach (KeyTypeMappingExpression mappingExpression in mappingExpressions) {
                _mappingExpressions.Add(mappingExpression.Name, mappingExpression);
            }
        }
    }
}