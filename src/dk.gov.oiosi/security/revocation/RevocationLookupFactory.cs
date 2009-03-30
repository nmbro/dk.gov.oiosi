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
using System;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.security.revocation {
    
    
    /// <summary>
    /// Creates an instance of a class wiht the IRevocationLookup interface
    /// </summary>
    public class RevocationLookupFactory {

        /// <summary>
        /// Builds an instance of an IRevocationLookup client based on configuration.
        /// </summary>
        /// <returns>ocsp lookup</returns>
        public IRevocationLookup CreateRevocationLookupClient() {
            // 1. Get factory config:
            RevocationLookupFactoryConfig config = ConfigurationHandler.GetConfigurationSection<RevocationLookupFactoryConfig>();

            return CreateRevocationLookupClient(config);
        }

        /// <summary>
        /// Builds an instance of an IRevocationLookup client based on configuration.
        /// </summary>
        /// <returns>ocsp lookup</returns>
        public IRevocationLookup CreateRevocationLookupClient(RevocationLookupFactoryConfig config) {
            // 1. Get the type to load:
            if (config.ImplementationNamespaceClass == null || config.ImplementationNamespaceClass == "")
                throw new RevocationNoImplementingClassException();
            if (config.ImplementationAssembly == null || config.ImplementationAssembly == "")
                throw new RevocationNoImplementingAssemblyException();
            string qualifiedTypename = config.ImplementationNamespaceClass + ", " + config.ImplementationAssembly;
            Type lookupClientType = Type.GetType(qualifiedTypename);
            if (lookupClientType == null)
                throw new FailedToLoadLookupTypeException(qualifiedTypename);

            // 3. Instantiate the type:
            IRevocationLookup lookupClient = (IRevocationLookup)lookupClientType.GetConstructor(new Type[0]).Invoke(null);

            return lookupClient;
        }
    }
}