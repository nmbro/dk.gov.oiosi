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
using System;
using dk.gov.oiosi.common;
using dk.gov.oiosi.configuration;
using System.Reflection;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Factory for creating IUddiLookup implementations
    /// </summary>
    public class UddiLookupClientFactory {
        private UddiLookupClientFactoryConfig _config;

        /// <summary>
        /// Creates an IUddiLookup implementation, as set in config.
        /// </summary>
        /// <returns>The IUddiLookup implementation</returns>
        public IUddiLookupClient CreateUddiLookupClient(Uri address) {
            // 1. Get factory config:
            _config = ConfigurationHandler.GetConfigurationSection<UddiLookupClientFactoryConfig>();

            // 2. Get the type to load:
            if (string.IsNullOrEmpty(_config.ImplementationNamespaceClass)) 
            { 
                throw new UddiNoImplementingClassException(); 
            }
            
            if (string.IsNullOrEmpty(_config.ImplementationAssembly)) 
            { 
                throw new UddiNoImplementingAssemblyException(); 
            }
            
            string qualifiedTypename = _config.ImplementationNamespaceClass + ", " + _config.ImplementationAssembly;
            
            Type lookupClientType = Type.GetType(qualifiedTypename);
            if (lookupClientType == null) 
            {
                throw new CouldNotLoadTypeException(qualifiedTypename);
            }

            // 3. Instantiate the type:
        	object[] parameters = new object[]{address};
            Type[] typeArray = new Type[] { typeof(Uri) };
            ConstructorInfo constructorInfo = lookupClientType.GetConstructor(typeArray);
            IUddiLookupClient lookupClient = (IUddiLookupClient)constructorInfo.Invoke(parameters);

            return lookupClient;
        }
    }
}