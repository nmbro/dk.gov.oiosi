using System.Collections.Generic;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// UDDI lookup client that implements cross registry lookups
    /// </summary>
    public class RegistryLookupClient : IUddiLookupClient
    {
        private readonly LookupRegistryFallbackConfig _configuration;

        /// <summary>
        /// Default constructor.
        /// It will read the configuration from RaspConfiguration.xml
        /// </summary>
        public RegistryLookupClient() 
        {
            UddiConfig uddiConfig = ConfigurationHandler.GetConfigurationSection<UddiConfig>();
            _configuration = uddiConfig.LookupRegistryFallbackConfig;
        }

        /// <summary>
        /// Constructor that takes the configuration as parameter
        /// </summary>
        /// <param name="configuration">The configuration od the lookup registry</param>
        public RegistryLookupClient(LookupRegistryFallbackConfig configuration) {
            _configuration = configuration;
        }

        #region IUddiLookupClient Members

        /// <summary>
        /// Service lookup
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<UddiLookupResponse> Lookup(LookupParameters parameters) 
        {
            List<UddiLookupResponse> response = null;
            foreach (Registry registry in _configuration.PrioritizedRegistryList) 
            {
                if (registry.Endpoints.Count > 0)
                {
                    IUddiLookupClient uddiLookupClient = new UddiFallbackClient(registry.GetAsUris());
                    response = uddiLookupClient.Lookup(parameters);

                    // Continue only as long as no result was found in the current registry
                    if (response != null && response.Count != 0)
                    {
                        break;
                    }
                }
                else
                {
                    // no endpoint is defined in the EndpointCollection
                    // so the Registry element can not be used
                }
            }
            return response;
        }

        public List<ProcessDefinition> GetProcessDefinitions(List<UddiId> processDefinitionIds) 
        {
            List<ProcessDefinition> processDefinitions = null;
            foreach (Registry registry in _configuration.PrioritizedRegistryList)
            {
                IUddiLookupClient uddiLookupClient = new UddiFallbackClient(registry.GetAsUris());
                processDefinitions = uddiLookupClient.GetProcessDefinitions(processDefinitionIds);

                // Continue until some was found
                if (processDefinitions != null && processDefinitions.Count != 0)
                {
                    break;
                }
            }

            return processDefinitions;
        }


        #endregion
    }
}
