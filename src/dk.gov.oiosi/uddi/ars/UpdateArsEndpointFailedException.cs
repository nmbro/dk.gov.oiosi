using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.uddi.ars {
    public class UpdateArsEndpointFailedException : ArsException {

        public UpdateArsEndpointFailedException(ArsEndpoint endpoint, Exception innerException) : base(GetKeywords(endpoint), innerException) { }

        public static Dictionary<string, string> GetKeywords(ArsEndpoint endpoint) {
            Dictionary<string, string> keywords = new Dictionary<string,string>();
            string endpointid = "";
            string endpointname = "";
            string endpointdescription = "";
            string endpointbusinessid = "";
            if (endpoint.ID != null) {
                endpointid = endpoint.ID.ID;
            }
            if (endpoint.Name != null) {
                endpointname = endpoint.Name.Value.Value;
            }
            if (endpoint.Description != null) {
                endpointdescription = endpoint.Description.Value.Value;
            }
            if (endpoint.BusinessKey != null) {
                endpointbusinessid = endpoint.BusinessKey.ID;
            }
            keywords.Add("endpointid", endpointid);
            keywords.Add("endpointname", endpointname);
            keywords.Add("endpointdescription", endpointdescription);
            keywords.Add("endpointbusinessid", endpointbusinessid);
            return keywords;
        }
    }
}
