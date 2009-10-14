using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi
{
    internal class UddiBinding
    {
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
            foreach (tModel model in tModels) {
                UddiTModel uddiTModel = new UddiTModel(model);
                if (!uddiTModel.IsProfileRole()) continue;

                string name = uddiTModel.Name;
                string description = uddiTModel.Description;
                string role = uddiTModel.GetProfileRoleId();
                string roleType = uddiTModel.GetProfileRoleTypeId();
                UddiId processDefinitionReferenceId = IdentifierUtility.GetUddiIDFromString(uddiTModel.GetProcessDefinitionReferenceId());

                ProcessRoleDefinition roleDefinition = new ProcessRoleDefinition(name, description, role, roleType, processDefinitionReferenceId);
                processes.Add(roleDefinition);
            }

            return processes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileUddiIds">A list of profiles of which only one needs to be found in order for the binding to support the profiles</param>
        /// <param name="roleIdentifier">If set to null non role check is performed and all roles are accepted.</param>
        /// <returns></returns>
        internal bool SupportsOneOrMoreProfileAndRole(List<UddiId> profileUddiIds, string roleIdentifier) {
            foreach (tModel tModelItem in GetTModelProfiles()) {
                UddiTModel uddiTModel = new UddiTModel(tModelItem);
                string profile = uddiTModel.GetProcessDefinitionReferenceId();
                string role = uddiTModel.GetProfileRoleId();
                bool hasProfileAndRole = HasOneOrMoreProfileAndRole(profile, role, profileUddiIds, roleIdentifier);
                if (hasProfileAndRole) return true;
            }

            return false;
        }

        private bool HasOneOrMoreProfileAndRole(string profile, string role, List<UddiId> profileUddiIds, string roleIdentifier) {
            if (profile == null) return false;
            if (role == null) return false;

            bool hasProfile = false;
            foreach (UddiId profileUddiId in profileUddiIds) {
                hasProfile = profile.Equals(profileUddiId.ID, StringComparison.CurrentCultureIgnoreCase);
                if (hasProfile) break;
            }

            bool hasRole;
            if (roleIdentifier == null) {
                hasRole = true;
            }
            else {
                hasRole = role.Equals(roleIdentifier, StringComparison.CurrentCultureIgnoreCase);
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

                UddiTModel uddiTModel = new UddiTModel(tModelItem);
                if (!uddiTModel.IsProfileRole()) continue;

                yield return tModelItem;
            }
        }
    }
}
