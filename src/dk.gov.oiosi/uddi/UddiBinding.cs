using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common;
using dk.gov.oiosi.uddi.identifier;

namespace dk.gov.oiosi.uddi
{
    internal class UddiBinding
    {
        private const string businessProcessRoleIdentifierId = "uddi:4b2e5d7e-8e5d-4c03-92ca-3597b7f52444";
        private const string businessProcessRoleIdentifierTypeId = "uddi:8dd0fa3e-be33-47f9-847b-8d974952a8dc";
        
        private const string businessProcessDefinitionReferenceId = "uddi:d474ac8c-ec5d-4679-90b0-a227a517d745";
        
        private const string registrationConformanceClaimId = "uddi:80496ef5-4d24-4788-a3f8-12fb54a75106";
        private const string registrationConformanceClaimKeyValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.1/";

        private readonly bindingTemplate template;
        private readonly List<tModel> tModels;

        public UddiBinding(bindingTemplate template, List<tModel> tModels) {
            if (template == null) throw new ArgumentNullException("template");
            if (tModels == null) throw new ArgumentNullException("tModels");

            this.template = template;
            this.tModels = tModels;
        }

        public EndpointAddress EndpointAddress {
            get { return GetEndpointAddress(); }
        }

        public List<ProcessRoleDefinition> Processes {
            get { return GetProcesses(); }
        }

        private EndpointAddress GetEndpointAddress() {
            accessPoint accessPointItem = template.Item as accessPoint;
            if (accessPointItem == null) throw new Exception("accessPoint type expected");
            return IdentifierUtility.GetEndpointAddressFromString(accessPointItem.Value);
        }

        private List<ProcessRoleDefinition> GetProcesses() {
            List<ProcessRoleDefinition> processes = new List<ProcessRoleDefinition>();
            foreach (tModel model in tModels)
            {
                if (!IsProfile(model)) continue;
                string name = model.name.Value;
                string description = model.description[0].Value;
                string role = model.identifierBag[0].keyValue;
                string roleType = "";
                keyedReference roleTypeReference = UddiCategory.GetOptionalCategoryByIdentifier(model.categoryBag, businessProcessRoleIdentifierTypeId);
                if (roleTypeReference != null)
                {
                    roleType = roleTypeReference.keyValue;
                }
                UddiId processDefinitionReferenceId = IdentifierUtility.GetUddiIDFromString(Guid.NewGuid().ToString());
                keyedReference processDefinitionReference = UddiCategory.GetOptionalCategoryByIdentifier(model.categoryBag, businessProcessDefinitionReferenceId);
                if (processDefinitionReference != null)
                {
                    processDefinitionReferenceId = IdentifierUtility.GetUddiIDFromString(processDefinitionReference.keyValue);
                }
                ProcessRoleDefinition roleDefinition = new ProcessRoleDefinition(name, description, role, roleType, processDefinitionReferenceId);
                processes.Add(roleDefinition);
            }

            return processes;
        }

        /// <summary>
        /// Returns true if this TModel represents a valid process instance
        /// </summary>
        /// <param name="tModel">The TModel to check</param>
        /// <returns>True if valid</returns>
        private bool IsProfile(tModel tModel) {
            if (tModel == null) return false;
            if (tModel.categoryBag == null) return false;
            if (tModel.categoryBag.Items.Length < 1) return false;

            // Check registration conformance
            keyedReference confClaimKeyref = UddiCategory.GetOptionalCategoryByIdentifier(tModel.categoryBag, registrationConformanceClaimId);
            if (confClaimKeyref == null) return false;
            if (confClaimKeyref.keyValue != registrationConformanceClaimKeyValue) {
                return false;
            }

            // Check that the businessProcessDefinitionReference category exists:
            keyedReference procKeyref = UddiCategory.GetOptionalCategoryByIdentifier(tModel.categoryBag, businessProcessDefinitionReferenceId);
            return procKeyref != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileUddiIds">A list of profiles of which only one needs to be found in order for the binding to support the profiles</param>
        /// <param name="roleIdentifier">If set to null non role check is performed and all roles are accepted.</param>
        /// <returns></returns>
        internal bool SupportsOneOrMoreProfileAndRole(List<UddiId> profileUddiIds, string roleIdentifier) {
            foreach (tModel tModelItem in GetTModelProfiles()) {
                keyedReference profileCategory = UddiCategory.GetOptionalCategoryByIdentifier(tModelItem.categoryBag, businessProcessDefinitionReferenceId);
                keyedReference roleCategory = UddiCategory.GetOptionalCategoryByIdentifier(tModelItem.identifierBag, businessProcessRoleIdentifierId);
                bool hasProfileAndRole = HasOneOrMoreProfileAndRole(profileCategory, roleCategory, profileUddiIds, roleIdentifier);
                if (hasProfileAndRole) return true;
            }

            return false;
        }

        private bool HasOneOrMoreProfileAndRole(keyedReference profileCategory, keyedReference roleCategory, List<UddiId> profileUddiIds, string roleIdentifier) {
            if (profileCategory == null) return false;
            if (roleCategory == null) return false;

            bool hasProfile = false;
            foreach (UddiId profileUddiId in profileUddiIds) {
                hasProfile = profileCategory.keyValue.Equals(profileUddiId.ID, StringComparison.CurrentCultureIgnoreCase);
                if (hasProfile) break;
            }

            bool hasRole;
            if (roleIdentifier == null) {
                hasRole = true;
            }
            else {
                hasRole = roleCategory.keyValue.ToLower() == roleIdentifier.ToLower();
            }
            
            if (hasProfile && hasRole) return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if valid</returns>
        private IEnumerable<tModel> GetTModelProfiles() {
            foreach (tModel tModelItem in tModels) {
                if (tModelItem == null) continue;
                if (tModelItem.categoryBag == null) continue;
                if (tModelItem.categoryBag.Items.Length < 1) continue;

                keyedReference confClaimKeyref = UddiCategory.GetOptionalCategoryByIdentifier(tModelItem.categoryBag, registrationConformanceClaimId);
                if (confClaimKeyref == null) continue;
                if (confClaimKeyref.keyValue != registrationConformanceClaimKeyValue) continue;

                keyedReference procKeyref = UddiCategory.GetOptionalCategoryByIdentifier(tModelItem.categoryBag, businessProcessDefinitionReferenceId);
                if (procKeyref == null) continue;

                yield return tModelItem;
            }
        }
    }
}
