using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// Extension to the LookupParameter class that is xml serializeable so its possible to 
    /// serielize UDDI lookups.
    /// Use the property LookupParameter to get a new specifik instance 
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class LookupParametersConfig {
        private string _endpointKey;
        private EndpointKeyTypeCode _endpointKeyTypeCode;
        private string _serviceContractId;
        private string _processDefinitionId;
        private Nullable<BusinessProcessRoleIdentifierTypeCode> _roleIdentifierTypeCode;
        private BusinessProcessRoleIdentifier _roleIdentifier;
        private EndpointAddressTypeCode[] _endpointAddressTypeFilter;
        private LookupReturnOptionEnum _lookupReturnOption = LookupReturnOptionEnum.allResults;
        private PreferredEndpointType _preferredEndpointType;

        /// <summary>
        /// Default constructor, used by the XMLserializer
        /// </summary>
        public LookupParametersConfig() { }

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="endpointKey">The endpoint key</param>
        /// <param name="endpointKeyTypeCode">The type of the endpoint key</param>
        /// <param name="addressTypeFilter">List of endpoint address types that should be returned in the search.
        /// If this property is null or empty, all address types should be returned.</param>
        /// <param name="preferredEndpointType">The preferred type of endpoint address, e.g. http or email</param>
        /// <param name="lookupReturnOption">Set the return semantics of the Lookup() operation. 
        /// See the LookupReturnOptionEnum for details.</param>
        /// <param name="serviceContractTModel">The UDDI id of the tModel representing the service contract</param>
        /// <param name="roleIdentifierTypeCode">The type of process role identifier. 
        /// If this value is null, the roleIdentifier must also be null</param>
        /// <param name="roleIdentifier">The type of the process role identifier. 
        /// If this value is null, the roleIdentifierType must also be null</param>
        /// <param name="processDefinitionTModel">The UDDI id of the tModel representing the 
        /// business process definition</param>
        public LookupParametersConfig(
            string endpointKey,
            EndpointKeyTypeCode endpointKeyTypeCode,
            EndpointAddressTypeCode[] addressTypeFilter,
            PreferredEndpointType preferredEndpointType,
            LookupReturnOptionEnum lookupReturnOption,
            string serviceContractTModel,
            BusinessProcessRoleIdentifierTypeCode roleIdentifierTypeCode,
            BusinessProcessRoleIdentifier roleIdentifier,
            string processDefinitionTModel
        ) {
            // Note that addressTypeFilter is allowed to be null or empty.
            _endpointAddressTypeFilter = addressTypeFilter;
            _lookupReturnOption = lookupReturnOption;
            _endpointKey = endpointKey;
            _endpointKeyTypeCode = endpointKeyTypeCode;
            _serviceContractId = serviceContractTModel;
            _roleIdentifierTypeCode = roleIdentifierTypeCode;
            _roleIdentifier = roleIdentifier;
            _processDefinitionId = processDefinitionTModel;
            _preferredEndpointType = preferredEndpointType;
        }

        /// <summary>
        /// The key of the endpoint.
        /// </summary>
        public string EndpointKey {
            get { return _endpointKey; }
            set { _endpointKey = value; }
        }

        /// <summary>
        /// Gets the keytype of the endpoint
        /// </summary>
        public EndpointKeyTypeCode EndpointKeyTypeCode {
            get { return _endpointKeyTypeCode; }
            set { _endpointKeyTypeCode = value; }
        }

        /// <summary>
        /// The UDDI uuid of the tModel representing the service contract. This is not the uuid of the endpoint
        /// tModel, but of the central service definition tModel, e.g. the central electronic invoice endpoint 
        /// tModel. 
        /// </summary>
        public string ServiceContractId {
            get { return _serviceContractId; }
            set { _serviceContractId = value; }
        }

        /// <summary>
        /// The UDDI uuid of the tModel representing the business process definition.
        /// </summary>
        public string ProcessDefinitionId {
            get { return _processDefinitionId; }
            set { _processDefinitionId = value; }
        }

        /// <summary>
        /// The type of the business process role identifier
        /// </summary>
        public Nullable<BusinessProcessRoleIdentifierTypeCode> RoleIdentifierType {
            get { return _roleIdentifierTypeCode; }
            set { _roleIdentifierTypeCode = value; }
        }

        /// <summary>
        /// The business process role identifier
        /// </summary>
        public BusinessProcessRoleIdentifier RoleIdentifier {
            get { return _roleIdentifier; }
            set { _roleIdentifier = value; }
        }

        /// <summary>
        /// List of endpoint address types that may be returned in the search.
        /// If this property is null or empty, all address types should be returned.
        /// </summary>
        public EndpointAddressTypeCode[] AddressTypeFilter {
            get { return _endpointAddressTypeFilter; }
            set { _endpointAddressTypeFilter = value; }
        }

        /// <summary>
        /// The return semantics of the Lookup() operation. See the LookupReturnOptionEnum for details.
        /// </summary>
        public LookupReturnOptionEnum LookupReturnOption {
            get { return _lookupReturnOption; }
            set { _lookupReturnOption = value; }
        }

        /// <summary>
        /// The preferred scheme of the endpoint returned
        /// </summary>
        public PreferredEndpointType PreferredEndpointType {
            get { return _preferredEndpointType; }
            set { _preferredEndpointType = value; }
        }
        
        /// <summary>
        /// Gets the lookup parameters for making an uddi lookup from the configuration
        /// </summary>
        /// <returns>the lookup parametes</returns>
        public LookupParameters GetLookupParameters() {
            UddiId processDefinitionId = null;
            UddiId serviceContractId = null;
            BusinessProcessRoleIdentifierType roleIdentifierType = null;
            List<EndpointAddressTypeCode> endpointAddressTypeFilter = null;
            IIdentifier endpointKey = new IdentifierEan(_endpointKey);
            EndpointKeytype endpointKeyType = new EndpointKeytype(_endpointKeyTypeCode);
            if (_serviceContractId != null) {
                serviceContractId = IdentifierUtility.GetUddiIDFromString(_serviceContractId);
            }
            if (_roleIdentifierTypeCode != null) {
                roleIdentifierType = new BusinessProcessRoleIdentifierType(_roleIdentifierTypeCode.Value);
            }
            if (_endpointAddressTypeFilter != null) {
                endpointAddressTypeFilter = new List<EndpointAddressTypeCode>(_endpointAddressTypeFilter);
            }
            if (_processDefinitionId != null) {
                processDefinitionId = IdentifierUtility.GetUddiIDFromString(_processDefinitionId);
            }
            return new LookupParameters(endpointKey, endpointKeyType, endpointAddressTypeFilter, PreferredEndpointType, LookupReturnOption, serviceContractId, roleIdentifierType, RoleIdentifier, processDefinitionId); 
        }
    }
}
