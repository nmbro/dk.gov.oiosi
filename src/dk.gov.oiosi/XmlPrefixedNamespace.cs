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

namespace dk.gov.oiosi
{
    /// <summary>
    /// An XML namespace, coupled with a prefix
    /// </summary>
    [XmlType("PrefixedNamespace", Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
    public class PrefixedNamespace
    {
        /// <summary>
        /// The actual, full, namespace
        /// </summary>
        [XmlElement("Namespace")]
        public string Namespace { get { return _namespace; } set { _namespace = value; } }
        private string _namespace;

        /// <summary>
        /// The namespace prefix
        /// </summary>
        [XmlElement("Prefix")]
        public string Prefix { get { return _prefix; } set { _prefix = value; } }
        private string _prefix;

        /// <summary>
        /// Constructor
        /// </summary>
        public PrefixedNamespace() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ns">Namespace</param>
        /// <param name="prefix">Prefix</param>
        public PrefixedNamespace(string ns, string prefix) 
        {
            _namespace = ns;
            _prefix = prefix;
        }
    }
}