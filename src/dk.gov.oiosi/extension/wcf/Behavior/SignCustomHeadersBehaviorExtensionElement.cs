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
using System.Xml;
using System.Configuration;
using System.ServiceModel.Configuration;
using dk.gov.oiosi;

namespace dk.gov.oiosi.extension.wcf.Behavior {

    /// <summary>
    /// Header to be signed configuration section
    /// </summary>
    public class HeaderToBeSigned : ConfigurationSection {

        /// <summary>
        /// Name of the header element
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        /// <summary>
        /// NS of the header element
        /// </summary>
        [ConfigurationProperty("namespace", IsRequired = true)]
        public string Namespace {
            get { return (string)base["namespace"]; }
            set { base["namespace"] = value; }
        }
    }

    /// <summary>
    /// Configuration section collection
    /// </summary>
    public class HeaderToBeSignedCollection : ConfigurationElementCollection {

        /// <summary>
        /// Creates a new configuration element
        /// </summary>
        /// <returns>The configuration element</returns>
        protected override ConfigurationElement CreateNewElement() {
            return new HeaderToBeSigned();
        }

        /// <summary>
        /// Gets the configuration element key
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element) {
            return ((HeaderToBeSigned)element).Name;
        }

        /// <summary>
        /// The name of the configuration element
        /// </summary>
        protected override string ElementName {
            get {
                return "header";
            }
        }
    }

    /// <summary>
    /// Sign custom headers behaviour configuration element
    /// </summary>
    public class SignCustomHeadersBehaviorExtensionElement : BehaviorExtensionElement {

        /// <summary>
        /// The sign headers configuration element collection
        /// </summary>
        [ConfigurationProperty("headers")]
        public HeaderToBeSignedCollection Headers {
            get { return (HeaderToBeSignedCollection)base["headers"]; }
            set { base["headers"] = value; }
        }

        /// <summary>
        /// The type of behaviour
        /// </summary>
        public override Type BehaviorType {
            get { return typeof(SignCustomHeadersBehavior); }
        }

        /// <summary>
        /// Creates the behaviour
        /// </summary>
        /// <returns>The behaviour</returns>
        protected override object CreateBehavior() {
            XmlQualifiedName[] headerNames = new XmlQualifiedName[Headers.Count];
            int i = 0;
            foreach (HeaderToBeSigned header in Headers)
                headerNames[i++] = new XmlQualifiedName(header.Name, header.Namespace);

            return new SignCustomHeadersBehavior(headerNames);
        }
    }
}
