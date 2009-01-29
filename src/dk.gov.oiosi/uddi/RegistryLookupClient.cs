using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// UDDI lookup client that implements cross registry lookups
    /// </summary>
    public class RegistryLookupClient : IUddiLookupClient {
        private readonly LookupRegistryFallbackConfig _configuration;

        /// <summary>
        /// Default constructor.
        /// It will read the configuration from RaspConfiguration.xml
        /// </summary>
        public RegistryLookupClient() {
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

        public List<UddiLookupResponse> Lookup(LookupParameters parameters) {
            List<UddiLookupResponse> response = null;
            //Uses different parameters for the lookup mechanism below.
            LookupParameters subParams = new LookupParameters(parameters.EndpointKey, parameters.EndpointKeyType, parameters.AddressTypeFilter, parameters.PreferredEndpointType, LookupReturnOption.allResults, parameters.ServiceContractTModel, parameters.RoleIdentifierType, parameters.RoleIdentifier, parameters.ProcessDefinitions, parameters.LookupCache);
            foreach (Registry registry in _configuration.PrioritizedRegistryList) {
				IUddiLookupClient uddiLookupClient = new UddiFallbackClient(registry.GetAsUris());
                response = uddiLookupClient.Lookup(subParams);
                // Continue only as long as no result was found in the current registry
                if (response != null && response.Count != 0)
                    break;
            }
            return FilterResponse(parameters, response);
        }

        /// <summary>
        /// Filters the response 
        /// </summary>
        /// <returns></returns>
        private List<UddiLookupResponse> FilterResponse(LookupParameters parameters, List<UddiLookupResponse> inquiryResult) {
            List<UddiLookupResponse> newFilteredResult = new List<UddiLookupResponse>();
            // How many endpoints are we expecting?
            if (inquiryResult.Count > 0) {
                switch (parameters.LookupReturnOption) {
                    case LookupReturnOption.allResults:
                        return inquiryResult;
                    case LookupReturnOption.firstResult:
                        newFilteredResult.Add(inquiryResult[0]);
                        return newFilteredResult;
                    case LookupReturnOption.noMoreThanOneSetOrFail:
                        if (inquiryResult.Count > 2) {
                            throw new UddiMoreThanOneEndpointException();
                        }
                        if (inquiryResult.Count == 2) {
                            if (inquiryResult[0].EndpointAddress.GetType() == inquiryResult[1].EndpointAddress.GetType()) throw new UddiTwoEqualEndpointsException();
                            int preferredIndex;
                            if (inquiryResult[0].EndpointAddress is EndpointAddressSMTP) {
                                preferredIndex = parameters.PreferredEndpointType == PreferredEndpointType.mailto ? 0 : 1;
                            }
                            else {
                                preferredIndex = parameters.PreferredEndpointType == PreferredEndpointType.http ? 0 : 1;
                            }
                            newFilteredResult.Add(inquiryResult[preferredIndex]);
                            return newFilteredResult;
                        }
                        newFilteredResult.Add(inquiryResult[0]);
                        return newFilteredResult;
                    default:
                        return inquiryResult;
                }
            }
            return inquiryResult;
        }
    }
}
