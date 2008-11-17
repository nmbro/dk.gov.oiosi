/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compl,iance with the License. You may obtain
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

using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;
using dk.gov.oiosi.exception;


namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// Holds a set of parameters and options for a specific lookup call.
    /// </summary>
    public class LookupParameters {
        private IIdentifier _endpointKey;
        private EndpointKeytype _endpointKeyType;
        private UddiId _serviceContractTModel;
        private UddiId _businessProcessDefinitionTModel;
        private BusinessProcessRoleIdentifierType _roleIdentifierType;
        private PreferredEndpointType _preferredEndpointType;
        private LookupReturnOptionEnum _lookupReturnOption = LookupReturnOptionEnum.allResults;
        private RegistrationConformanceClaim _registrationConformanceClaim = new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1);
        private ConformanceClaim _profileConformanceClaim = new ConformanceClaim(ConformanceClaimCode.secureReliableAsyncProfile1_0);
        private BusinessProcessRoleIdentifier _roleIdentifier;
        private List<EndpointAddressTypeCode> _addressTypeFilter;
        private List<UddiId> _processDefinitions = new List<UddiId>();
        private ITimedCache<LookupKey, List<UddiLookupResponse>> _lookupCache = new TimedNullCache<LookupKey, List<UddiLookupResponse>>();

        /// <summary>
        /// Constructor. 
        /// Obsolete because it uses an older way to make a lookup.
        /// </summary>
        /// <param name="endpointKey">The endpoint key</param>
        /// <param name="endpointKeyType">The type of the endpoint key</param>
        /// <param name="addressTypeFilter">List of endpoint address types that should be returned in the search.
        /// If this property is null or empty, all address types should be returned.</param>
        /// <param name="preferredEndpointType">The preferred type of endpoint address, e.g. http or email</param>
        /// <param name="lookupReturnOption">Set the return semantics of the Lookup() operation. 
        /// See the LookupReturnOptionEnum for details.</param>
        /// <param name="serviceContractTModel">The UDDI id of the tModel representing the service contract</param>
        /// <param name="roleIdentifierType">The type of process role identifier. 
        /// If this value is null, the roleIdentifier must also be null</param>
        /// <param name="roleIdentifier">The type of the process role identifier. 
        /// If this value is null, the roleIdentifierType must also be null</param>
        /// <param name="processDefinitionTModel">The UDDI id of the tModel representing the 
        /// business process definition</param>
        [Obsolete]
        public LookupParameters(
            IIdentifier endpointKey,
            EndpointKeytype endpointKeyType,
            List<EndpointAddressTypeCode> addressTypeFilter,
            PreferredEndpointType preferredEndpointType,
            LookupReturnOptionEnum lookupReturnOption,
            UddiId serviceContractTModel,
            BusinessProcessRoleIdentifierType roleIdentifierType,
            BusinessProcessRoleIdentifier roleIdentifier,
            UddiId processDefinitionTModel
        ) {
            // Note that addressTypeFilter is allowed to be null or empty.
            _addressTypeFilter = addressTypeFilter;
            _lookupReturnOption = lookupReturnOption;
            _endpointKey = endpointKey;
            _endpointKeyType = endpointKeyType;
            _serviceContractTModel = serviceContractTModel;
            _roleIdentifierType = roleIdentifierType;
            _roleIdentifier = roleIdentifier;
            _businessProcessDefinitionTModel = processDefinitionTModel;
            if (processDefinitionTModel != null) {
                _processDefinitions.Add(processDefinitionTModel);
            }
            _preferredEndpointType = preferredEndpointType;
        }

        /// <summary>
        /// Constructor that takes all parameters
        /// </summary>
        /// <param name="endpointKey">The endpoint key</param>
        /// <param name="endpointKeyType">The type of the endpoint key</param>
        /// <param name="addressTypeFilter">List of endpoint address types that should be returned in the search.
        /// If this property is null or empty, all address types should be returned.</param>
        /// <param name="preferredEndpointType">The preferred type of endpoint address, e.g. http or email</param>
        /// <param name="lookupReturnOption">Set the return semantics of the Lookup() operation. 
        /// See the LookupReturnOptionEnum for details.</param>
        /// <param name="serviceContractTModel">The UDDI id of the tModel representing the service contract</param>
        /// <param name="roleIdentifierType">The type of process role identifier. 
        /// If this value is null, the roleIdentifier must also be null</param>
        /// <param name="roleIdentifier">The type of the process role identifier. 
        /// If this value is null, the roleIdentifierType must also be null</param>
        /// <param name="processDefinitions">A list of process definitions id's where one
        /// of them must be in the endpoint returned. An empty one means all is good.</param>
        public LookupParameters(
            IIdentifier endpointKey,
            EndpointKeytype endpointKeyType,
            List<EndpointAddressTypeCode> addressTypeFilter,
            PreferredEndpointType preferredEndpointType,
            LookupReturnOptionEnum lookupReturnOption,
            UddiId serviceContractTModel,
            BusinessProcessRoleIdentifierType roleIdentifierType,
            BusinessProcessRoleIdentifier roleIdentifier,
            IEnumerable<UddiId> processDefinitions
        ) {
            // Note that addressTypeFilter is allowed to be null or empty.
            _addressTypeFilter = addressTypeFilter;
            _lookupReturnOption = lookupReturnOption;
            _endpointKey = endpointKey;
            _endpointKeyType = endpointKeyType;
            _serviceContractTModel = serviceContractTModel;
            _roleIdentifierType = roleIdentifierType;
            _roleIdentifier = roleIdentifier;
            _processDefinitions.AddRange(processDefinitions);
            _preferredEndpointType = preferredEndpointType;
        }

        /// <summary>
        /// The key of the endpoint.
        /// </summary>
        public IIdentifier EndpointKey {
            get { return _endpointKey; }
        }

        /// <summary>
        /// Gets the keytype of the endpoint
        /// </summary>
        public EndpointKeytype EndpointKeyType {
            get { return _endpointKeyType; }
        }

        /// <summary>
        /// The UDDI uuid of the tModel representing the service contract. This is not the uuid of the endpoint
        /// tModel, but of the central service definition tModel, e.g. the central electronic invoice endpoint 
        /// tModel. 
        /// </summary>
        public UddiId ServiceContractTModel {
            get { return _serviceContractTModel; }
        }

        /// <summary>
        /// The UDDI uuid of the tModel representing the business process definition.
        /// </summary>
        [Obsolete]
        public UddiId BusinessProcessDefinitionTModel {
            get { return _businessProcessDefinitionTModel; }
        }

        /// <summary>
        /// The type of the business process role identifier
        /// </summary>
        public BusinessProcessRoleIdentifierType RoleIdentifierType {
            get { return _roleIdentifierType; }
        }

        /// <summary>
        /// The business process role identifier
        /// </summary>
        public BusinessProcessRoleIdentifier RoleIdentifier {
            get { return _roleIdentifier; }
        }

        /// <summary>
        /// The value of the UDDI registration conformance claim custom identifier keyedReference value.
        /// </summary>
        public RegistrationConformanceClaim RegistrationConformanceClaim {
            get { return _registrationConformanceClaim; }
        }

        /// <summary>
        /// The value of the profile registration conformance claim custom identifier 
        /// keyedReference value. Set to the default (RASP 1.0 profile)
        /// </summary>
        public ConformanceClaim ProfileConformanceClaim {
            get { return _profileConformanceClaim; }
        }

        /// <summary>
        /// List of endpoint address types that may be returned in the search.
        /// If this property is null or empty, all address types should be returned.
        /// </summary>
        public List<EndpointAddressTypeCode> AddressTypeFilter {
            get { return _addressTypeFilter; }
        }

        /// <summary>
        /// The return semantics of the Lookup() operation. See the LookupReturnOptionEnum for details.
        /// </summary>
        public LookupReturnOptionEnum LookupReturnOption {
            get { return _lookupReturnOption; }
        }
        
        /// <summary>
        /// The preferred scheme of the endpoint returned
        /// </summary>
        public PreferredEndpointType PreferredEndpointType {
            get { return _preferredEndpointType; }
        }

        /// <summary>
        /// Gets the process defintions that should be supported by the endpoint returned.
        /// The list is or'ed.
        /// </summary>
        public IEnumerable<UddiId> ProcessDefintions {
            get { return _processDefinitions; }
        }

        /// <summary>
        /// Gets wheteher the parameters has any process related constraints
        /// </summary>
        public bool HasAnyProcessConstraints {
            get { return _processDefinitions.Count > 0 || _businessProcessDefinitionTModel != null; }
        }

        /// <summary>
        /// Gets whether the parameter has any process role constraints
        /// </summary>
        public bool HasAnyProcessRoleConstraints {
            get { return _roleIdentifier != null || _roleIdentifierType != null; }
        }

        /// <summary>
        /// The caching model to use in uddi lookups.
        /// The default is no cache.
        /// </summary>
        public ITimedCache<LookupKey, List<UddiLookupResponse>> LookupCache {
            get { return _lookupCache; }
            set {
                if (value == null) {
                    throw new NullArgumentException("value was null");
                }
                _lookupCache = value;
            }
        }

        /// <summary>
        /// Returns the lookup key object used in the timed UDDI cache
        /// </summary>
        /// <returns>Returns the lookup key object used in the timed UDDI cache</returns>
        public LookupKey GetLookupKey() {
            LookupKey lookupKey = new LookupKey(
                _addressTypeFilter,
                _processDefinitions,
                _endpointKey,
                _endpointKeyType,
                _profileConformanceClaim,
                _registrationConformanceClaim,
                _roleIdentifier,
                _serviceContractTModel
                );
            return lookupKey;
        }

    }
}
