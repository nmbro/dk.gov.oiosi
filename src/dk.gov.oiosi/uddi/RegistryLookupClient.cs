using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi
{
    public class RegistryLookupClient : IUddiLookupClient
    {
        public List<UddiLookupResponse> Lookup(LookupParameters parameters)
        {
            List<UddiLookupResponse> response = null;

            LookupRegistryFallbackConfig fallbackConfig =
                ConfigurationHandler.GetConfigurationSection<UddiConfig>().LookupRegistryFallbackConfig;

        	int i = 0;
            foreach (Registry registry in fallbackConfig.PrioritizedRegistryList)
            {
				IUddiLookupClient uddiLookupClient = new UddiFallbackClient(registry.GetAsUris());
                response = uddiLookupClient.Lookup(parameters);
                
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
        private List<UddiLookupResponse> FilterResponse(LookupParameters parameters, List<UddiLookupResponse> inquiryResult)
        {
            List<UddiLookupResponse> newFilteredResult = new List<UddiLookupResponse>();
            // How many endpoints are we expecting?
            if (inquiryResult.Count > 0)
            {
                switch (parameters.LookupReturnOption)
                {
                    case LookupReturnOptionEnum.allResults:
                        return inquiryResult;
                    case LookupReturnOptionEnum.firstResult:
                        newFilteredResult.Add(inquiryResult[0]);
                        return newFilteredResult;
                    case LookupReturnOptionEnum.noMoreThanOneSetOrFail:
                        if (inquiryResult.Count > 2)
                        {
                            throw new UddiMoreThanOneEndpointException();
                        }
                        if (inquiryResult.Count == 2)
                        {
                            if (inquiryResult[0].EndpointAddress.GetType() == inquiryResult[1].EndpointAddress.GetType()) throw new UddiTwoEqualEndpointsException();
                            int preferredIndex;
                            if (inquiryResult[0].EndpointAddress is EndpointAddressSMTP)
                            {
                                preferredIndex = parameters.PreferredEndpointType == PreferredEndpointType.mailto ? 0 : 1;
                            }
                            else
                            {
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
