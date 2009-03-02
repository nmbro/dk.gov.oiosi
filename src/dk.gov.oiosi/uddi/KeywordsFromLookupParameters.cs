using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// Gets the keywords for the exception from the lookup parameters.
    /// </summary>
    public class KeywordsFromLookupParameters {
        /// <summary>
        /// Returns a new dictionary with keywords from the lookup parameters.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeywords(LookupParameters lookupParameters) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeywords(keywords, lookupParameters);
            return keywords;
        }

        /// <summary>
        /// Adds the keywords from the lookup parameters to the given keyword 
        /// dictionary.
        /// </summary>
        /// <param name="keywords"></param>
        public static void GetKeywords(Dictionary<string, string> keywords, LookupParameters lookupParameters) {
            string endpointKey = lookupParameters.Identifier.GetAsString();
            string serviceContractId = lookupParameters.ServiceId.ID;
            string role;
            if (lookupParameters.ProfileIds == null) {
                role = "null";
            }
            else {
                role = lookupParameters.ProfileIds.ToString();
            }
            string roleType;
            if (lookupParameters.ProfileRoleIdentifier == null) {
                roleType = "null";
            }
            else {
                roleType = lookupParameters.ProfileRoleIdentifier;
            }

            keywords.Add("lookupparamsidentifier", endpointKey);
            keywords.Add("lookupparamsserviceid", serviceContractId);
            keywords.Add("lookupparamsprofileids", role);
            keywords.Add("lookupparamsroleidentifier", roleType);
        }
    }
}
