using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.addressing;

namespace dk.gov.oiosi.samples.TestUddi
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }


        private Uri uddiServerUri;
        private readonly Identifier eanIdentifier = new IdentifierEan("5790001865214");

        private readonly UddiId invoiceServiceId = new UddiGuidId("uddi:2e0b402a-7a5e-476b-8686-b33f54fd1f47", true);

        private readonly UddiId nesublProfilesProfile5UddiId = new UddiGuidId("uddi:AEE8B6DE-298F-4cbc-A96D-9AE8AED0AC31", true);

        private const string nonExistingRoleIdentifier = "NonExistingSellerParty";
        private const string sellerPartyRoleIdentifier = "SellerParty";
        private readonly List<EndpointAddressTypeCode> acceptHttpProtocol = new List<EndpointAddressTypeCode>() { EndpointAddressTypeCode.http };

        public Program()
        {
            ConfigurationHandler.ConfigFilePath = "./RaspConfiguration.Live.xml";
            ConfigurationHandler.Reset();
            UddiConfig config = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            this.uddiServerUri = new Uri(config.LookupRegistryFallbackConfig.PrioritizedRegistryList[0].Endpoints[0]);

            this.PerformLookup();
        }

        public void PerformLookup()
        {
            List<UddiId> profileIds = new List<UddiId>() { this.nesublProfilesProfile5UddiId };

            LookupParameters lookupParameters = new LookupParameters(this.eanIdentifier, this.invoiceServiceId, profileIds, this.acceptHttpProtocol);
            List<UddiLookupResponse> lookupResponses = this.GetEndpointsWithProfileFromUddi(lookupParameters);
            
        }

        private List<UddiLookupResponse> GetEndpointsWithProfileFromUddi(LookupParameters lookupParameters)
        {
            UddiLookupClient lookupClient = new UddiLookupClient(this.uddiServerUri);
            return lookupClient.Lookup(lookupParameters);
        }


    }
}
