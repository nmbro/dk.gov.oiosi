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
using System.IO;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using NUnit.Framework;

namespace dk.gov.oiosi.test.nunit.library.uddi
{
    [TestFixture]
    public class PrioritizedLookupRegistryListTest
    {
        private LookupRegistryFallbackConfig _registryFallbackConfig;

        private string _registry1Endpoint1 = "http://test.com";
        private string _registry1Endpoint2 = "http://fallback.com";
        private string _registry2Endpoint1 = "http://test2.com";
        private string _registry2Endpoint2 = "http://fallback2.com";

        [SetUp]
        public void SetUp()
        {
            ConfigurationHandler.ConfigFilePath = "RaspConfiguration.xml";
        }

        [Test]
        public void _01_WriteRegistryListToConfig()
        {
            _registryFallbackConfig = ConfigurationHandler.GetConfigurationSection<LookupRegistryFallbackConfig>();
/*            Registry registry1 = new Registry(new List<string>{_registry1Endpoint1, _registry1Endpoint2});
            Registry registry2 = new Registry(new List<string>{_registry2Endpoint1, _registry2Endpoint2});
            _registryFallbackConfig.PrioritizedRegistryList.Add(registry1);
            _registryFallbackConfig.PrioritizedRegistryList.Add(registry2);*/
            ConfigurationHandler.SaveToFile();
        }

        [Test]
        public void _02_LoadRegistryListFromConfig()
        {
            if(_registryFallbackConfig == null)_01_WriteRegistryListToConfig();

            _registryFallbackConfig = ConfigurationHandler.GetConfigurationSection<LookupRegistryFallbackConfig>();
            Assert.AreEqual(_registry1Endpoint1, _registryFallbackConfig.PrioritizedRegistryList[0].Endpoints[0]);
            Assert.AreEqual(_registry1Endpoint2, _registryFallbackConfig.PrioritizedRegistryList[0].Endpoints[1]);
            Assert.AreEqual(_registry2Endpoint1, _registryFallbackConfig.PrioritizedRegistryList[1].Endpoints[0]);
            Assert.AreEqual(_registry2Endpoint2, _registryFallbackConfig.PrioritizedRegistryList[1].Endpoints[1]);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(ConfigurationHandler.ConfigFilePath);
        }
    }
}
