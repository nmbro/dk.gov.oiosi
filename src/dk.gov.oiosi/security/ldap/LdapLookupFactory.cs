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
 *   Jacob Mogensen, mySupply
  *
  */
using System;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.security.ldap {

    /// <summary>
    /// Instantiates classes with the ICertificateLookup interface, based on configuration.
    /// </summary>
    public class LdapLookupFactory
    {
        /// <summary>
        /// Instantiates classes with the ICertificateLookup interface, based on configuration.
        /// </summary>
        /// <returns>Returns a class with the ICertificateLookup interface, based on configuration.</returns>
        public ICertificateLookup CreateLdapLookupClient() {
            // 1. Get config:
            LdapLookupFactoryConfig config = ConfigurationHandler.GetConfigurationSection<LdapLookupFactoryConfig>();

            // 2. Get the type to load:
            if (config.ImplementationNamespaceClass == null || config.ImplementationNamespaceClass == "")
            { 
                throw new LdapNoImplementingClassException(); 
            }
            if (config.ImplementationAssembly == null || config.ImplementationAssembly == "") 
            { 
                throw new LdapNoImplementingAssemblyException(); 
            }

            string qualifiedTypename = config.ImplementationNamespaceClass + ", " + config.ImplementationAssembly;
            Type lookupClientType = Type.GetType(qualifiedTypename);

            if (lookupClientType == null) 
            {
                throw new FailedToLoadLookupTypeException(qualifiedTypename);
            }

            // 3. Instantiate the type:
            ICertificateLookup lookupClient;
            try 
            {
                lookupClient = (ICertificateLookup)lookupClientType.GetConstructor(new Type[0]).Invoke(null);
            }
            catch (Exception e) 
            {
                throw new LdapCertificateLookupInitializationFailedException(e);
            }

            return lookupClient;
        }
    }
}