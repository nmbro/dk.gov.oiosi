using System.Collections.Generic;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// Gets the keywords for the exception from the lookup parameters.
    /// </summary>
    public class KeywordsFromLookupParameters {
        /// <summary>
        /// Returns a new dictionary with keywords from the lookup parameters.
        /// </summary>
        /// <param name="configuration"></param>
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
        /// <param name="configuration"></param>
        public static void GetKeywords(Dictionary<string, string> keywords, LookupParameters lookupParameters) {
            string endpointKey = lookupParameters.EndpointKey.GetAsString();
            string endpointKeyType = lookupParameters.EndpointKeyType.Value;
            string serviceContractId = lookupParameters.ServiceContractTModel.ID;
            string role;
            if (lookupParameters.RoleIdentifier == null) {
                role = "null";
            }
            else {
                role = lookupParameters.RoleIdentifier.Value;
            }
            string roleType;
            if (lookupParameters.RoleIdentifierType == null) {
                roleType = "null";
            }
            else {
                roleType = lookupParameters.RoleIdentifierType.Value;
            }
            string process;
            if (lookupParameters.BusinessProcessDefinitionTModel == null) {
                process = "null";
            }
            else {
                process = lookupParameters.BusinessProcessDefinitionTModel.ID;
            }
            keywords.Add("lookupparamsendpointkey", endpointKey);
            keywords.Add("lookupparamsendpointkeytype", endpointKeyType);
            keywords.Add("lookupparamsservicecontractid", serviceContractId);
            keywords.Add("lookupparamsrole", role);
            keywords.Add("lookupparamsroletype", roleType);
            keywords.Add("lookupparamsprocess", process);
        }
    }
}
