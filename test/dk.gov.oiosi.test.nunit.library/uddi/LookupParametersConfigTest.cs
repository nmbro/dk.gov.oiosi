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
  *   Dennis S�gaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */

using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;

namespace dk.gov.oiosi.test.nunit.library.uddi {
    [TestFixture]
    public class LookupParametersConfigTest {
        private Stream stream01 = new MemoryStream();
        private Stream stream02 = new MemoryStream();
        private Stream stream03 = new MemoryStream();

        [Test]
        public void _01_DefaultParametersConfigTest(){
            LookupParametersConfig lookupParametersConfig1 = new LookupParametersConfig();
            Save(stream01, lookupParametersConfig1);
            LookupParametersConfig lookupParametersConfig2 = Load(stream01);
        }

        [Test]
        public void _02_AllLookupParametersConfigTest() {
            string endpointKey = "5701234567890";
            string processDefinitionId = "1234";
            string serviceContractId = "1234";
            LookupParametersConfig lookupParametersConfig1 = new LookupParametersConfig();
            lookupParametersConfig1.AddressTypeFilter = new EndpointAddressTypeCode[] { EndpointAddressTypeCode.http };
            lookupParametersConfig1.EndpointKey = endpointKey;
            lookupParametersConfig1.EndpointKeyTypeCode = EndpointKeyTypeCode.ean;
            lookupParametersConfig1.LookupReturnOption = LookupReturnOptionEnum.firstResult;
            lookupParametersConfig1.PreferredEndpointType = PreferredEndpointType.http;
            lookupParametersConfig1.ProcessDefinitionId = processDefinitionId;
            lookupParametersConfig1.RoleIdentifier = new BusinessProcessRoleIdentifier("1234");
            lookupParametersConfig1.RoleIdentifierType = BusinessProcessRoleIdentifierTypeCode.ubl2_0_ProcessRole;
            lookupParametersConfig1.ServiceContractId = "1234";

            Save(stream02, lookupParametersConfig1);
            LookupParametersConfig lookupParametersConfig2 = Load(stream02);
            Assert.AreEqual(endpointKey, lookupParametersConfig2.EndpointKey);
            Assert.AreEqual(EndpointKeyTypeCode.ean, lookupParametersConfig2.EndpointKeyTypeCode);
            Assert.AreEqual(LookupReturnOptionEnum.firstResult, lookupParametersConfig2.LookupReturnOption);
            Assert.AreEqual(PreferredEndpointType.http, lookupParametersConfig2.PreferredEndpointType);
            Assert.AreEqual(processDefinitionId, lookupParametersConfig2.ProcessDefinitionId);
            Assert.AreEqual("1234", lookupParametersConfig2.RoleIdentifier.Value);
            Assert.AreEqual(BusinessProcessRoleIdentifierTypeCode.ubl2_0_ProcessRole, lookupParametersConfig2.RoleIdentifierType);
            Assert.AreEqual(serviceContractId, lookupParametersConfig2.ServiceContractId);
        }

        [Test]
        public void _03_LookupParametersLookupParametersConfigTest() {
            string endpointKey = "5701234567890";
            string processDefinitionId = "1234";
            string serviceContractId = "1234";
            LookupParametersConfig lookupParametersConfig1 = new LookupParametersConfig();
            lookupParametersConfig1.AddressTypeFilter = new EndpointAddressTypeCode[] { EndpointAddressTypeCode.http };
            lookupParametersConfig1.EndpointKey = endpointKey;
            lookupParametersConfig1.EndpointKeyTypeCode = EndpointKeyTypeCode.ean;
            lookupParametersConfig1.LookupReturnOption = LookupReturnOptionEnum.firstResult;
            lookupParametersConfig1.PreferredEndpointType = PreferredEndpointType.http;
            lookupParametersConfig1.ProcessDefinitionId = processDefinitionId;
            lookupParametersConfig1.RoleIdentifier = new BusinessProcessRoleIdentifier("1234");
            lookupParametersConfig1.RoleIdentifierType = BusinessProcessRoleIdentifierTypeCode.ubl2_0_ProcessRole;
            lookupParametersConfig1.ServiceContractId = serviceContractId;

            Save(stream03, lookupParametersConfig1);
            LookupParametersConfig lookupParametersConfig2 = Load(stream03);
            LookupParameters lookupParameters = lookupParametersConfig2.GetLookupParameters();

            Assert.AreEqual(endpointKey, lookupParameters.EndpointKey.GetAsString());
            Assert.AreEqual(EndpointKeyTypeCode.ean, lookupParameters.EndpointKeyType.GetEndpointKeyTypeCode());
            Assert.AreEqual(LookupReturnOptionEnum.firstResult, lookupParameters.LookupReturnOption);
            Assert.AreEqual(PreferredEndpointType.http, lookupParameters.PreferredEndpointType);
            Assert.AreEqual(serviceContractId, lookupParameters.ServiceContractTModel.ID);
            Assert.AreEqual("1234", lookupParameters.RoleIdentifier.Value);
            Assert.AreEqual(processDefinitionId, lookupParameters.BusinessProcessDefinitionTModel.ID);
        }

        [Test]
        public void _04_SaveConfigurationLookupParametersConfigTest() {
            string endpointKey = "5701234567890";
            string processDefinitionId = "1234";
            string serviceContractId = "1234";
            LookupParametersConfig lookupParametersConfig = ConfigurationHandler.GetConfigurationSection<LookupParametersConfig>();
            lookupParametersConfig.AddressTypeFilter = new EndpointAddressTypeCode[] { EndpointAddressTypeCode.http };
            lookupParametersConfig.EndpointKey = endpointKey;
            lookupParametersConfig.EndpointKeyTypeCode = EndpointKeyTypeCode.ean;
            lookupParametersConfig.LookupReturnOption = LookupReturnOptionEnum.firstResult;
            lookupParametersConfig.PreferredEndpointType = PreferredEndpointType.http;
            lookupParametersConfig.ProcessDefinitionId = processDefinitionId;
            lookupParametersConfig.RoleIdentifier = new BusinessProcessRoleIdentifier("1234");
            lookupParametersConfig.RoleIdentifierType = BusinessProcessRoleIdentifierTypeCode.ubl2_0_ProcessRole;
            lookupParametersConfig.ServiceContractId = serviceContractId;
            ConfigurationHandler.SaveToFile();
        }

        [Test]
        public void _05_LoadConfigurationLookupParametersConfigTest() {
            string endpointKey = "5701234567890";
            string processDefinitionId = "1234";
            string serviceContractId = "1234";
            LookupParametersConfig lookupParametersConfig = ConfigurationHandler.GetConfigurationSection<LookupParametersConfig>();
            LookupParameters lookupParameters = lookupParametersConfig.GetLookupParameters();

            Assert.AreEqual(endpointKey, lookupParameters.EndpointKey.GetAsString());
            Assert.AreEqual(EndpointKeyTypeCode.ean, lookupParameters.EndpointKeyType.GetEndpointKeyTypeCode());
            Assert.AreEqual(LookupReturnOptionEnum.firstResult, lookupParameters.LookupReturnOption);
            Assert.AreEqual(PreferredEndpointType.http, lookupParameters.PreferredEndpointType);
            Assert.AreEqual(processDefinitionId, lookupParameters.ServiceContractTModel.ID);
            Assert.AreEqual("1234", lookupParameters.RoleIdentifier.Value);
            Assert.AreEqual(serviceContractId, lookupParameters.BusinessProcessDefinitionTModel.ID);
        }

        private void Save(Stream stream, LookupParametersConfig lookupParametersConfig) {
            XmlSerializer xml = new XmlSerializer(typeof(LookupParametersConfig));
            xml.Serialize(stream, lookupParametersConfig);
            stream.Flush();
        }

        private LookupParametersConfig Load(Stream stream) {
            stream.Position = 0;
            XmlSerializer xml = new XmlSerializer(typeof(LookupParametersConfig));
            object deserialized = xml.Deserialize(stream);
            LookupParametersConfig lookupParametersConfig = (LookupParametersConfig)deserialized;
            return lookupParametersConfig;
        }
    }
}
