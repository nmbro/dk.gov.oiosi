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

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication.configuration;


namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// Defines all OIOUBL profile mappings
    /// </summary>
    public class DefaultProfileMappingConfig {

        /// <summary>
        /// Adds all the mappings
        /// </summary>
        public void AddAll() {
            AddMapping("Catalogue-CatBas-1.0", "uddi:4697A391-741F-4534-A21E-8F0A460013BB");
            AddMapping("Catalogue-CatBasR-1.0", "uddi:03717C45-27A8-453f-833C-BD4D8AAB9675");
            AddMapping("Catalogue-CatSim-1.0", "uddi:BE9F86E6-03C0-4d00-A3EE-44FC29EB3882");
            AddMapping("Catalogue-CatSimR-1.0", "uddi:48277C4B-489F-498d-8246-303A9867C081");
            AddMapping("Catalogue-CatExt-1.0", "uddi:1C8E9102-6711-42e8-A35A-997C56A35BFE");
            AddMapping("Catalogue-CatExtR-1.0", "uddi:0B864A5A-5E1E-47da-AD3B-A3932F82DA37");
            AddMapping("Catalogue-CatAdv-1.0", "uddi:7B7909C1-5DE4-4630-A741-C74A9DDDC6AB");
            AddMapping("Catalogue-CatAdvR-1.0", "uddi:EE7F4AEA-BFDC-4886-8C5E-319596E46DA5");
            AddMapping("Procurement-BilSim-1.0", "uddi:362229ac-b657-452a-b8f8-c93e62c670ff");
            AddMapping("Procurement-BilSimR-1.0", "uddi:98070e14-ee30-4b10-84ef-986cde3b8116");
            AddMapping("Procurement-PayBas-1.0", "uddi:E5D505C0-4B52-4485-8169-4AB5343559A5");
            AddMapping("Procurement-PayBasR-1.0", "uddi:FED5F809-64EC-4523-BC57-4A57D5680DA9");
            AddMapping("Procurement-OrdSim-BilSim-1.0", "uddi:142C4188-3D53-440d-A64F-68D7C3B9A59B");
            AddMapping("Procurement-OrdSimR-BilSimR-1.0", "uddi:1e1b209e-7b8d-4f1e-8f2a-f3d9a94d1086");
            AddMapping("Procurement-OrdSim-BilSimR-1.0", "uddi:7A638D35-3E08-432e-B558-74CB85889905");
            AddMapping("Procurement-OrdSimR-BilSim-1.0", "uddi:EBABEE8B-A5D3-4dc9-B976-3AAFF9A4E855");
            AddMapping("Procurement-OrdAdv-BilSim-1.0", "uddi:88FBD6D5-6A25-4c08-91CC-5344C73C4D69");
            AddMapping("Procurement-OrdAdv-BilSimR-1.0", "uddi:76897296-08aa-4848-a933-ae068b4c604e");
            AddMapping("Procurement-OrdAdvR-BilSim-1.0", "uddi:1d01dd98-a302-4897-92d8-fd501447c450");
            AddMapping("Procurement-OrdAdvR-BilSimR-1.0", "uddi:b23940b1-d571-4640-8830-9b7f34809fbc");
            AddMapping("Procurement-OrdSel-BilSim-1.0", "uddi:46D94D6B-E835-4916-BBB5-F27DC655876A");
            AddMapping("Procurement-OrdSel-BilSimR-1.0", "uddi:42AD3EDE-BBD4-434d-AAE6-044CE3EF8D1F");
            AddMapping("OIOXML elektronisk handel",	"uddi:c001daa0-8ba3-11dd-894e-770465b08940");
            AddMapping("OIOXML elektronisk handel - læs ind", "uddi:cac79330-8ba3-11dd-894e-770465b08940");
            AddMapping("OIOXML Elektronisk Regning", "uddi:CD8A1434-AE29-4f6d-A26D-F0F25F2D3DA6");
            AddMapping("OIOXML Elektronisk Kreditnota", "uddi:45533597-5A1A-4c15-BEA1-FF3E9EBE5C29");
            AddMapping("urn:www.nesubl.eu:profiles:profile1:ver2.0", "uddi:FC1D4A1C-1538-4bdb-A718-2EFD712256C5");
            AddMapping("urn:www.nesubl.eu:profiles:profile2:ver2.0", "uddi:3A3BFE67-AD35-43f0-9AE7-CDF268AE221D");
            AddMapping("urn:www.nesubl.eu:profiles:profile3:ver2.0", "uddi:F5231EDB-FBB4-4a3b-A46C-3BCF1B3C3F35");
            AddMapping("urn:www.nesubl.eu:profiles:profile4:ver2.0", "uddi:80BAAA62-4F27-40a5-A434-B3578F5AA424");
            AddMapping("urn:www.nesubl.eu:profiles:profile5:ver2.0", "uddi:aee8b6de-298f-4cbc-a96d-9ae8aed0ac31");
            AddMapping("urn:www.nesubl.eu:profiles:profile6:ver2.0", "uddi:ACE3E6E7-8702-40fa-9A5D-1926122AE215");
            AddMapping("urn:www.nesubl.eu:profiles:profile7:ver2.0", "uddi:BB0B4FD4-F6AF-489f-98D9-7130424E7F8D");
            AddMapping("urn:www.nesubl.eu:profiles:profile8:ver2.0", "uddi:F4240370-CCA6-401e-9B5B-4531F413421D");
            AddMapping("Reference-Utility-1.0", "uddi:nemhandel.dk:570b3009-3f9f-46d2-b533-31d0bb4a37a0");
            AddMapping("Reference-UtilityR-1.0", "uddi:nemhandel.dk:1bfd82e6-0eca-47df-9230-37b97c9788c6");
        }

        private void AddMapping(string name, string tModelGuid) {
            ProfileMapping profileMapping = new ProfileMapping(name, tModelGuid);
            Add(profileMapping);
        }

        /// <summary>
        /// Adds a document type definition to the collection
        /// </summary>
        /// <param name="profileMapping"></param>
        private void Add(ProfileMapping profileMapping) {
            ProfileMappingCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<ProfileMappingCollectionConfig>();
            if (!configuration.ContainsProfileMappingByName(profileMapping.Name))
                configuration.AddProfileMapping(profileMapping);
        }
    }
}
