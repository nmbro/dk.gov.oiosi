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
using System.Xml.Serialization;

namespace dk.gov.oiosi.communication.configuration {

    /// <summary>
    /// One Xpath expression
    /// </summary>
    [XmlType("XPathConfiguration", Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
    public class CustomHeaderXPathConfiguration {
        private string _name;
        private string _xpath;

        /// <summary>
        /// Name of expression
        /// </summary>
        [XmlAttribute("name")]
        public string Name {
            get { return _name; }
            set { _name = value; }
        }
        
        /// <summary>
        /// Value of expression
        /// </summary>
        [XmlAttribute("xpath")]
        public string XPath {
            get { return _xpath; }
            set { _xpath = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomHeaderXPathConfiguration() { }

        /// <summary>
        /// Constructor. Takes Name and expresion as parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="xpath"></param>
        public CustomHeaderXPathConfiguration(string name, string xpath) {
            _name = name;
            _xpath = xpath;
        }
    }
}
