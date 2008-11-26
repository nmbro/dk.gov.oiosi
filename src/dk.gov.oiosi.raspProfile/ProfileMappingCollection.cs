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

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication.configuration;


namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// Defines all OIOUBL profile mappings
    /// </summary>
    public class ProfileMappingCollection {

        /// <summary>
        /// Adds all the mappings
        /// </summary>
        public void AddAll() {
            AddOneMapping("urn:www.nesubl.eu:profiles:profile5:ver2.0", "uddi:aee8b6de-298f-4cbc-a96d-9ae8aed0ac31");
            AddOneMapping("Procurement-BilSim-1.0", "uddi:362229ac-b657-452a-b8f8-c93e62c670ff");
            AddOneMapping("Procurement-OrdSimR-BilSim-1.0", "uddi:EBABEE8B-A5D3-4dc9-B976-3AAFF9A4E855");
            AddOneMapping("Catalogue-CatBas-1.0", "uddi:4697A391-741F-4534-A21E-8F0A460013BB");
            AddOneMapping("OIOXML Elektronisk Regning", "uddi:CD8A1434-AE29-4f6d-A26D-F0F25F2D3DA6");
            AddOneMapping("OIOXML Elektronisk Kreditnota", "uddi:45533597-5A1A-4c15-BEA1-FF3E9EBE5C29");
            AddOneMapping("urn:www.nesubl.eu:profiles:profile1:ver2.0", "uddi:FC1D4A1C-1538-4bdb-A718-2EFD712256C5");
            AddOneMapping("urn:www.nesubl.eu:profiles:profile2:ver2.0", "uddi:3A3BFE67-AD35-43f0-9AE7-CDF268AE221D");
            AddOneMapping("urn:www.nesubl.eu:profiles:profile3:ver2.0", "uddi:F5231EDB-FBB4-4a3b-A46C-3BCF1B3C3F35");
            AddOneMapping("urn:www.nesubl.eu:profiles:profile4:ver2.0", "uddi:80BAAA62-4F27-40a5-A434-B3578F5AA424");
            AddOneMapping("urn:www.nesubl.eu:profiles:profile6:ver2.0", "uddi:ACE3E6E7-8702-40fa-9A5D-1926122AE215");
            AddOneMapping("urn:www.nesubl.eu:profiles:profile7:ver2.0", "uddi:BB0B4FD4-F6AF-489f-98D9-7130424E7F8D");
            AddOneMapping("urn:www.nesubl.eu:profiles:profile8:ver2.0", "uddi:F4240370-CCA6-401e-9B5B-4531F413421D");
            AddOneMapping("Procurement-BilSimR-1.0", "uddi:98070e14-ee30-4b10-84ef-986cde3b8116");
            AddOneMapping("Procurement-PayBas-1.0", "uddi:E5D505C0-4B52-4485-8169-4AB5343559A5");
            AddOneMapping("Procurement-PayBasR-1.0", "uddi:FED5F809-64EC-4523-BC57-4A57D5680DA9");
            AddOneMapping("Procurement-OrdSim-BilSim-1.0", "uddi:142C4188-3D53-440d-A64F-68D7C3B9A59B");
            AddOneMapping("Procurement-OrdSimR-BilSimR-1.0", "uddi:1e1b209e-7b8d-4f1e-8f2a-f3d9a94d1086");
            AddOneMapping("Procurement-OrdSim-BilSimR-1.0",	"uddi:7A638D35-3E08-432e-B558-74CB85889905");
            AddOneMapping("Procurement-OrdAdv-BilSim-1.0",	"uddi:88FBD6D5-6A25-4c08-91CC-5344C73C4D69");
            AddOneMapping("Procurement-OrdAdvR-BilSimR-1.0", "uddi:b23940b1-d571-4640-8830-9b7f34809fbc");
            AddOneMapping("Procurement-OrdAdv-BilSimR-1.0",	"uddi:76897296-08aa-4848-a933-ae068b4c604e");
            AddOneMapping("Procurement-OrdAdvR-BilSim-1.0",	"uddi:1d01dd98-a302-4897-92d8-fd501447c450");
            AddOneMapping("Procurement-OrdSel-BilSim-1.0",	"uddi:46D94D6B-E835-4916-BBB5-F27DC655876A");
            AddOneMapping("Procurement-OrdSelR-BilSimR-1.0",	"uddi:34CD4205-D7CF-47f6-87D4-DF4F8DB2AEFD");
            AddOneMapping("Procurement-OrdSel-BilSimR-1.0",	"uddi:42AD3EDE-BBD4-434d-AAE6-044CE3EF8D1F");
            AddOneMapping("Procurement-OrdSelR-BilSim-1.0",	"uddi:ACED2BA9-5B9F-44d6-9698-FB55C3DED1A9");
            AddOneMapping("Catalogue-CatBasR-1.0",	"uddi:03717C45-27A8-453f-833C-BD4D8AAB9675");
            AddOneMapping("Catalogue-CatSim-1.0",	"uddi:BE9F86E6-03C0-4d00-A3EE-44FC29EB3882");
            AddOneMapping("Catalogue-CatSimR-1.0",	"uddi:48277C4B-489F-498d-8246-303A9867C081");
            AddOneMapping("Catalogue-CatExt-1.0",	"uddi:1C8E9102-6711-42e8-A35A-997C56A35BFE");
            AddOneMapping("Catalogue-CatExtR-1.0",	"uddi:0B864A5A-5E1E-47da-AD3B-A3932F82DA37");
            AddOneMapping("Catalogue-CatAdv-1.0",	"uddi:7B7909C1-5DE4-4630-A741-C74A9DDDC6AB");
            AddOneMapping("Catalogue-CatAdvR-1.0",	"uddi:EE7F4AEA-BFDC-4886-8C5E-319596E46DA5");
            AddOneMapping("NS OIOXML Elektronisk Regning",	"uddi:367e54f0-a24e-11dc-a80b-bfc65441a808");
            AddOneMapping("NS OIOXML Elektronisk Kreditnota",	"uddi:2c33e6e0-a24e-11dc-a80b-bfc65441a808");
            AddOneMapping("NS Procurement-BilSimR-1.0",	"uddi:47a2e980-a24e-11dc-a80b-bfc65441a808");
            AddOneMapping("NS Procurement-OrdAdvR-BilSimR-1.0",	"uddi:5197bd80-a24e-11dc-a80b-bfc65441a808");
            AddOneMapping("NS NS Procurement-PayBasR-1.0",	"uddi:4d2dadf0-e05a-11dc-889b-1a827c218899");
            AddOneMapping("NKS 2.0", "uddi:fbc05a80-bde5-11dc-a81d-bfc65441a808");
            AddOneMapping("DOIP Order",	"uddi:1a967cf0-bde6-11dc-a81d-bfc65441a808");
            AddOneMapping("OIOXML elektronisk handel",	"uddi:c001daa0-8ba3-11dd-894e-770465b08940");
            AddOneMapping("OIOXML elektronisk handel - l�s ind", "uddi:cac79330-8ba3-11dd-894e-770465b08940");

        }

        private void AddOneMapping(string name, string tModelGuid)
        {
            ProfileMapping profileMapping = new ProfileMapping(name, tModelGuid);
            Add(profileMapping);
        }

        /// <summary>
        /// Adds all the document types from configuration, clears collection first
        /// </summary>
        public void CleanAdd() {
            DocumentTypeCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            configuration.Clear();
            AddAll();
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