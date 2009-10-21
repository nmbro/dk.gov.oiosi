using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi {
    internal class UddiBinding {
        private readonly bindingTemplate template;
        private readonly List<UddiTModel> tModels;

        public UddiBinding(bindingTemplate template, List<tModel> tModels) {
            if (template == null) throw new ArgumentNullException("template");
            if (tModels == null) throw new ArgumentNullException("tModels");

            this.template = template;
            Converter<tModel, UddiTModel> converter = delegate(tModel tmodel) { return new UddiTModel(tmodel); };
            this.tModels = tModels.ConvertAll<UddiTModel>(converter);
        }

        public EndpointAddress GetEndpointAddress() {
            accessPoint accessPointItem = template.Item as accessPoint;
            if (accessPointItem == null) throw new Exception("accessPoint type expected");
            return IdentifierUtility.GetEndpointAddressFromString(accessPointItem.Value);
        }

        public List<UddiTModel> GetProcessRoleTModels() {
            Predicate<UddiTModel> find = delegate(UddiTModel uddiTModel) { return uddiTModel.IsProfileRole(); };
            return tModels.FindAll(find);
        }

        public List<ProcessRoleDefinition> GetProcessRoleDefinitions() {
            List<UddiTModel> processesRoleTModels = GetProcessRoleTModels();
            Converter<UddiTModel, ProcessRoleDefinition> converter = delegate(UddiTModel tmodel) {
                string name = tmodel.Name;
                string description = tmodel.Description;
                string role = tmodel.GetProfileRoleId();
                string roleType = tmodel.GetProfileRoleTypeId();
                UddiId processDefinitionReferenceId = IdentifierUtility.GetUddiIDFromString(tmodel.GetProcessDefinitionReferenceId());
                return new ProcessRoleDefinition(name, description, role, roleType, processDefinitionReferenceId);
            };
            return processesRoleTModels.ConvertAll<ProcessRoleDefinition>(converter);
        }

        public UddiTModel GetPortType() {
            Predicate<UddiTModel> find = delegate(UddiTModel uddiTModel) { return uddiTModel.IsPortType(); };
            return tModels.Find(find);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileUddiIds">A list of profiles of which only one needs to be found in order for the binding to support the profiles</param>
        /// <param name="roleIdentifier">If set to null non role check is performed and all roles are accepted.</param>
        /// <returns></returns>
        internal bool SupportsOneOrMoreProfileAndRole(List<UddiId> profileUddiIds, string roleIdentifier) {
            List<UddiTModel> processRoles = GetProcessRoleTModels();
            foreach (UddiTModel uddiTModel in processRoles) {
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
    }
}
