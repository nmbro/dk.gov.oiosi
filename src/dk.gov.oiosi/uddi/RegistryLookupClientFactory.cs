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
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Factory for creating IUddiLookup implementations
    /// </summary>
    public class RegistryLookupClientFactory {
        private RegistryLookupClientFactoryConfig _config;

        /// <summary>
        /// Creates an IUddiLookup implementation, as set in config.
        /// </summary>
        /// <returns>The IUddiLookup implementation</returns>
        public IUddiLookupClient CreateUddiLookupClient() {
            // 1. Get factory config:
            _config = ConfigurationHandler.GetConfigurationSection<RegistryLookupClientFactoryConfig>();

            // 2. Get the type to load:
            if (_config.ImplementationNamespaceClass == null || _config.ImplementationNamespaceClass == "") { 
                throw new UddiNoImplementingClassException(); }
            if (_config.ImplementationAssembly == null || _config.ImplementationAssembly == "") { 
                throw new UddiNoImplementingAssemblyException(); }
             string qualifiedTypename = _config.ImplementationNamespaceClass + ", " + _config.ImplementationAssembly;
            
            Type lookupClientType = Type.GetType(qualifiedTypename);
            if (lookupClientType == null) {
                throw new CouldNotLoadTypeException(qualifiedTypename);
            }

            // 3. Instantiate the type:
            IUddiLookupClient lookupClient = (IUddiLookupClient)lookupClientType.GetConstructor(new Type[0]).Invoke(null);

            return lookupClient;
        }
    }
}