/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Key for use in dictionaries.
    /// Combines all the unique properties of a LookupParameter.
    /// Instantiate using the GetLookupKey in the LookupParameters class.
    /// </summary>
    public class LookupKey {
        private List<EndpointAddressTypeCode> _addressTypeFilter;
        private readonly IList<UddiId> _businessProcessDefinitionTModels;
        private IIdentifier _endpointKey;
        private EndpointKeytype _endpointKeyType;
        private ConformanceClaim _profileConformanceClaim;
        private RegistrationConformanceClaim _registrationConformanceClaim;
        private BusinessProcessRoleIdentifier _roleIdentifier;
        private UddiId _serviceContractTModel;

        /// <summary>
        /// Creates a lookup key.
        /// Usually called from within the LookupParameters class as this is a subset of the properties.
        /// </summary>
        public LookupKey(
            List<EndpointAddressTypeCode> addressTypeFilter,
            IList<UddiId> businessProcessDefinitionTModels,
            IIdentifier endpointKey,
            EndpointKeytype endpointKeyType,
            ConformanceClaim profileConformanceClaim,
            RegistrationConformanceClaim registrationConformanceClaim,
            BusinessProcessRoleIdentifier roleIdentifier,
            UddiId serviceContractTModel) {
            _addressTypeFilter = addressTypeFilter;
            _businessProcessDefinitionTModels = businessProcessDefinitionTModels;
            _endpointKey = endpointKey;
            _endpointKeyType = endpointKeyType;
            _profileConformanceClaim = profileConformanceClaim;
            _registrationConformanceClaim = registrationConformanceClaim;
            _roleIdentifier = roleIdentifier;
            _serviceContractTModel = serviceContractTModel;
        }

        /// <summary>
        /// Returns the hashcode of the endpoint key
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            int hashCode = _endpointKey.ToString().GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two instances of a lookup key.
        /// All property values are compared.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if (this.GetType() != obj.GetType()) return false;
            LookupKey other = (LookupKey) obj;

            if (!AreListsEqual<EndpointAddressTypeCode>(_addressTypeFilter, other._addressTypeFilter)) return false;
            if (!AreListsEqual<UddiId>(_businessProcessDefinitionTModels, other._businessProcessDefinitionTModels)) return false;
            if (!AreEqual<IIdentifier>(_endpointKey, other._endpointKey)) return false;
            if (!AreEqual<EndpointKeytype>(_endpointKeyType, other._endpointKeyType)) return false;
            if (!AreEqual<ConformanceClaim>(_profileConformanceClaim, other._profileConformanceClaim)) return false;
            if (!AreEqual<RegistrationConformanceClaim>(_registrationConformanceClaim, other._registrationConformanceClaim)) return false;
            if (!AreEqual<BusinessProcessRoleIdentifier>(_roleIdentifier, other._roleIdentifier)) return false;
            if (!AreEqual<UddiId>(_serviceContractTModel, other._serviceContractTModel)) return false;

            return true;
        }

        private bool AreEqual<T>(T obj1, T obj2) where T : IEquatable<T> {
            if (obj1 == null && obj2 == null) return true;
            if (obj1 != null && obj2 == null) return false;
            if (obj1 == null && obj2 != null) return false;

            if (!obj1.Equals(obj2)) return false;
            return true;
        }

        private bool AreListsEqual<T>(IList<T> list1, IList<T> list2) {
            if (list1 != null && list2 == null) return false;
            if (list1 == null && list2 != null) return false;
            if (list1 == null && list2 == null) return true;
            if (list1.Count != list2.Count) return false;

            int index = 0;
            foreach (T typeCode in list1) {
                if (!typeCode.Equals(list2[index])) return false;
                index++;
            }
            return true;
        }


    }
}
