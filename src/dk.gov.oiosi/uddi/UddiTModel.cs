﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi 
{
    public class UddiTModel
    {
        private const string wsdlTypeId = "uddi:uddi.org:wsdl:types";
        private const string businessProcessIdentifierId = "uddi:9111dd24-6734-407f-b949-d601ab427520";
        private const string businessProcessIdentifierTypeId = "uddi:e03ae1dd-634f-4c6b-abd2-814c165ab5bf";
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

        public UddiId UddiId {
            get { return new UddiStringId(tModel.tModelKey, true); }
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

        public string GetProfileId() {
            foreach (keyedReference keyedRef in tModel.identifierBag) {
                if (!businessProcessIdentifierId.Equals(keyedRef.tModelKey, StringComparison.CurrentCultureIgnoreCase)) continue;
                if (keyedRef.keyValue == null) return "";
                return keyedRef.keyValue;
            }
            return "";
        }

        public string GetProfileTypeId() {
            keyedReference keyedRef;
            if (!categoryBag.TryGetKeyedReference(businessProcessIdentifierTypeId, out keyedRef)) return "";
            if (keyedRef.keyValue == null) return "";
            return keyedRef.keyValue;
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
            if (!categoryBag.TryGetKeyedReference(businessProcessDefinitionReferenceId, out keyedRef)) return "";
            if (keyedRef.keyValue == "") return "";
            return keyedRef.keyValue;
        }

        public string GetRegistrationConformanceClaim() {
            keyedReference keyedRef;
            if (!categoryBag.TryGetKeyedReference(registrationConformanceClaimId, out keyedRef)) return "";
            if (keyedRef.keyValue == "") return "";
            return keyedRef.keyValue;
        }

        public bool IsProfileRole() {
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

        public bool IsPortType() {
            keyedReference wsdlType;
            if (!categoryBag.TryGetKeyedReference(wsdlTypeId, out wsdlType)) {
                return false;
            }
            return wsdlType.keyValue.Equals("portType");
        }
    }
}
