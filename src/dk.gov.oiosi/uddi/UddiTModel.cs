using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi {
    internal class UddiTModel {
        private const string businessProcessRoleIdentifierId = "uddi:4b2e5d7e-8e5d-4c03-92ca-3597b7f52444";
        private const string businessProcessRoleIdentifierTypeId = "uddi:8dd0fa3e-be33-47f9-847b-8d974952a8dc";
        private const string businessProcessDefinitionReferenceId = "uddi:d474ac8c-ec5d-4679-90b0-a227a517d745";
        private const string registrationConformanceClaimId = "uddi:80496ef5-4d24-4788-a3f8-12fb54a75106";
        private const string registrationConformanceClaimKeyValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.1/";

        private readonly tModel tModel;
        private readonly UddiCategoryBag categoryBag;

        public UddiTModel(tModel tModel) {
            if (tModel == null) throw new ArgumentNullException("tModel");
            
            this.tModel= tModel;
            this.categoryBag = new UddiCategoryBag(tModel.categoryBag);
        }

        public tModel TModel {
            get { return tModel; }
        }

        public string Name {
            get {
                if (tModel.name == null) return "";
                if (tModel.name.Value == null) return "";
                return tModel.name.Value;
            }
        }

        public string Description {
            get {
                if (tModel.description == null) return "";
                if (tModel.description.Length == 0) return "";
                if (tModel.description[0].Value == null) return "";
                return tModel.description[0].Value;
            }
        }

        public string GetProfileRoleId() {
            foreach (keyedReference keyedRef in tModel.identifierBag) {
                if (!businessProcessRoleIdentifierId.Equals(keyedRef.tModelKey, StringComparison.CurrentCultureIgnoreCase)) continue;
                if (keyedRef.keyValue == null) return "";
                return keyedRef.keyValue;
            }
            return "";
        }

        public string GetProfileRoleTypeId() {
            keyedReference keyedRef;
            if (!categoryBag.TryGetKeyedReference(businessProcessRoleIdentifierTypeId, out keyedRef)) return "";
            if (keyedRef.keyValue == null) return "";
            return keyedRef.keyValue;
        }

        public string GetProcessDefinitionReferenceId() {
            keyedReference keyedRef;
            if (!categoryBag.TryGetKeyedReference(businessProcessRoleIdentifierTypeId, out keyedRef)) return "";
            if (keyedRef.keyValue == "") return "";
            return keyedRef.keyValue;
        }

        public bool IsProfileRole() {
            if (tModel.categoryBag == null) return false;
            if (tModel.categoryBag.Items.Length < 1) return false;

            // Check registration conformance
            keyedReference confClaimKeyref;
            if (!categoryBag.TryGetKeyedReference(registrationConformanceClaimId, out confClaimKeyref)) {
                return false;
            }
            if (confClaimKeyref.keyValue != registrationConformanceClaimKeyValue) {
                return false;
            }

            // Check that the businessProcessDefinitionReference category exists:
            keyedReference procKeyref;
            return categoryBag.TryGetKeyedReference(businessProcessDefinitionReferenceId, out procKeyref);
        }
    }
}
