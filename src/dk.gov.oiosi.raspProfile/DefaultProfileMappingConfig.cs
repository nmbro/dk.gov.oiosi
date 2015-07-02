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

using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.raspProfile
{
    /// <summary>
    /// Defines all OIOUBL profile mappings
    /// </summary>
    public class DefaultProfileMappingConfig
    {
        /// <summary>
        /// Adds all the mappings
        /// </summary>
        public void AddAll()
        {
            // oioxml The xml profiles is not used by the RASP, as there is no profiles in xml. But
            // it still works. When Rasp sends a xml document, no profile is added in the lookup
            // parameters, and the UDDI returns all registrated endpoints (uddiResponse), for that
            // document type (all profiles). In xml there is only on profile, so just one endpoint
            // (uddiResponse) is returned, and if just one uddiResponse is returned, that one is
            // used. AddMapping("OIOXML elektronisk handel",
            // "uddi:c001daa0-8ba3-11dd-894e-770465b08940"); AddMapping("OIOXML elektronisk handel -
            // læs ind", "uddi:cac79330-8ba3-11dd-894e-770465b08940"); AddMapping("OIOXML
            // Elektronisk Regning", "uddi:CD8A1434-AE29-4f6d-A26D-F0F25F2D3DA6");
            // AddMapping("OIOXML Elektronisk Kreditnota", "uddi:45533597-5A1A-4c15-BEA1-FF3E9EBE5C29");

            // OIOUBL - 22
            this.AddMapping("Catalogue-CatBas-1.0", "uddi:4697A391-741F-4534-A21E-8F0A460013BB");
            this.AddMapping("Catalogue-CatBasR-1.0", "uddi:03717C45-27A8-453f-833C-BD4D8AAB9675");
            this.AddMapping("Catalogue-CatSim-1.0", "uddi:BE9F86E6-03C0-4d00-A3EE-44FC29EB3882");
            this.AddMapping("Catalogue-CatSimR-1.0", "uddi:48277C4B-489F-498d-8246-303A9867C081");
            this.AddMapping("Catalogue-CatExt-1.0", "uddi:1C8E9102-6711-42e8-A35A-997C56A35BFE");
            this.AddMapping("Catalogue-CatExtR-1.0", "uddi:0B864A5A-5E1E-47da-AD3B-A3932F82DA37");
            this.AddMapping("Catalogue-CatAdv-1.0", "uddi:7B7909C1-5DE4-4630-A741-C74A9DDDC6AB");
            this.AddMapping("Catalogue-CatAdvR-1.0", "uddi:EE7F4AEA-BFDC-4886-8C5E-319596E46DA5");
            this.AddMapping("Procurement-BilSim-1.0", "uddi:362229ac-b657-452a-b8f8-c93e62c670ff");
            this.AddMapping("Procurement-BilSimR-1.0", "uddi:98070e14-ee30-4b10-84ef-986cde3b8116");
            this.AddMapping("Procurement-PayBas-1.0", "uddi:E5D505C0-4B52-4485-8169-4AB5343559A5");
            this.AddMapping("Procurement-PayBasR-1.0", "uddi:FED5F809-64EC-4523-BC57-4A57D5680DA9");
            this.AddMapping("Procurement-OrdSim-BilSim-1.0", "uddi:142C4188-3D53-440d-A64F-68D7C3B9A59B");
            this.AddMapping("Procurement-OrdSimR-BilSimR-1.0", "uddi:1e1b209e-7b8d-4f1e-8f2a-f3d9a94d1086");
            this.AddMapping("Procurement-OrdSim-BilSimR-1.0", "uddi:7A638D35-3E08-432e-B558-74CB85889905");
            this.AddMapping("Procurement-OrdSimR-BilSim-1.0", "uddi:EBABEE8B-A5D3-4dc9-B976-3AAFF9A4E855");
            this.AddMapping("Procurement-OrdAdv-BilSim-1.0", "uddi:88FBD6D5-6A25-4c08-91CC-5344C73C4D69");
            this.AddMapping("Procurement-OrdAdv-BilSimR-1.0", "uddi:76897296-08aa-4848-a933-ae068b4c604e");
            this.AddMapping("Procurement-OrdAdvR-BilSim-1.0", "uddi:1d01dd98-a302-4897-92d8-fd501447c450");
            this.AddMapping("Procurement-OrdAdvR-BilSimR-1.0", "uddi:b23940b1-d571-4640-8830-9b7f34809fbc");
            this.AddMapping("Procurement-OrdSel-BilSim-1.0", "uddi:46D94D6B-E835-4916-BBB5-F27DC655876A");
            this.AddMapping("Procurement-OrdSel-BilSimR-1.0", "uddi:42AD3EDE-BBD4-434d-AAE6-044CE3EF8D1F");

            // OIOUBL nesubl - 8
            this.AddMapping("urn:www.nesubl.eu:profiles:profile1:ver2.0", "uddi:FC1D4A1C-1538-4bdb-A718-2EFD712256C5");
            this.AddMapping("urn:www.nesubl.eu:profiles:profile2:ver2.0", "uddi:3A3BFE67-AD35-43f0-9AE7-CDF268AE221D");
            this.AddMapping("urn:www.nesubl.eu:profiles:profile3:ver2.0", "uddi:F5231EDB-FBB4-4a3b-A46C-3BCF1B3C3F35");
            this.AddMapping("urn:www.nesubl.eu:profiles:profile4:ver2.0", "uddi:80BAAA62-4F27-40a5-A434-B3578F5AA424");
            this.AddMapping("urn:www.nesubl.eu:profiles:profile5:ver2.0", "uddi:aee8b6de-298f-4cbc-a96d-9ae8aed0ac31");
            this.AddMapping("urn:www.nesubl.eu:profiles:profile6:ver2.0", "uddi:ACE3E6E7-8702-40fa-9A5D-1926122AE215");
            this.AddMapping("urn:www.nesubl.eu:profiles:profile7:ver2.0", "uddi:BB0B4FD4-F6AF-489f-98D9-7130424E7F8D");
            this.AddMapping("urn:www.nesubl.eu:profiles:profile8:ver2.0", "uddi:F4240370-CCA6-401e-9B5B-4531F413421D");

            // OIOUBL Utility - 2
            this.AddMapping("Reference-Utility-1.0", "uddi:570b3009-3f9f-46d2-b533-31d0bb4a37a0");
            this.AddMapping("Reference-UtilityR-1.0", "uddi:1bfd82e6-0eca-47df-9230-37b97c9788c6");

            // New profiles in Schematron release 20140915 - 6
            this.AddMapping("Catalogue-CatPriUpd-1.0", "uddi:a7f4c627-d459-4ef2-a132-fc6df0971db1");
            this.AddMapping("Catalogue-CatPriUpdR-1.0", "uddi:fc5313f8-19e0-408b-85ca-87c364f2a41c");
            this.AddMapping("Procurement-OrdSim-1.0", "uddi:5f2d93a7-bdb7-4543-b667-ba71192e5c5f");
            this.AddMapping("Procurement-OrdSimR-1.0", "uddi:7fe5938c-1b3a-403c-8155-039b49a06cef");
            this.AddMapping("Procurement-TecRes-1.0", "uddi:ce80ccce-3ef9-47ea-8cf4-120913a87dc2");
            this.AddMapping("Reference-Attachment-1.0", "uddi:10faffb1-1303-485e-924a-e8b551f31949");

            // Peppol profiles - 7
            this.AddMapping("urn:www.cenbii.eu:profile:bii04:ver2.0", "uddi:553e78e6-7de4-4926-8384-53ef82566560");
            this.AddMapping("urn:www.cenbii.eu:profile:bii28:ver2.0", "uddi:e15ac48e-e679-46fa-937d-10d69c558998");
            this.AddMapping("urn:www.cenbii.eu:profile:bii01:ver2.0", "uddi:c7cb8dd0-af00-42c6-a8b7-48204aa7b30f");
            this.AddMapping("urn:www.cenbii.eu:profile:bii30:ver2.0", "uddi:19ec7bb2-a39b-438c-a6ce-2124d6148f8f");
            this.AddMapping("urn:www.cenbii.eu:profile:bii05:ver2.0", "uddi:8dde0481-9055-41fe-94f7-5102ce3672e7");
            this.AddMapping("urn:www.cenbii.eu:profile:bii03:ver2.0", "uddi:90043120-5eab-468c-be4a-5562d95a73a7");
            this.AddMapping("urn:www.cenbii.eu:profile:bii36:ver2.0", "uddi:d725cfb7-4080-4c72-91a6-3c54d368dbca");
        }

        public void AddMapping(string name, string tModelGuid)
        {
            ProfileMapping profileMapping = new ProfileMapping(name, tModelGuid);
            Add(profileMapping);
        }

        /// <summary>
        /// Adds a document type definition to the collection
        /// </summary>
        /// <param name="profileMapping"></param>
        public void Add(ProfileMapping profileMapping)
        {
            ProfileMappingCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<ProfileMappingCollectionConfig>();
            if (!configuration.ContainsProfileMappingByName(profileMapping.Name))
                configuration.AddProfileMapping(profileMapping);
        }
    }
}