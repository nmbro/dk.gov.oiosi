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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// Configuration for the UddiLookupClientFactory
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class RegistryLookupClientFactoryConfig {
        private string _implementationNamespaceClass = "";
        private string _implementationAssembly = "";
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegistryLookupClientFactoryConfig() { }
        /// <summary>
        /// The namespace of the IUddiLookup implementation
        /// </summary>
        public string ImplementationNamespaceClass {
            get { return _implementationNamespaceClass; }
            set { _implementationNamespaceClass = value; }
        }
        /// <summary>
        /// The assembly name of the IUddiLookup implementation
        /// </summary>
        public string ImplementationAssembly {
            get { return _implementationAssembly; }
            set { _implementationAssembly = value; }
        }
    }
}