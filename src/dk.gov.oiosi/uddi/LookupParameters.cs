using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;

namespace dk.gov.oiosi.uddi {
    
    /// <summary>
    /// Parameters for use in lookup
    /// </summary>
    public class LookupParameters {
        
        /// <summary>
        /// The identifier for the service
        /// </summary>
        public Identifier Identifier { get; private set; }
        
        /// <summary>
        /// The uddi id of the service to find
        /// </summary>
        public UddiId ServiceId { get; private set; }
        
        /// <summary>
        /// List of profile id's that can be present in the result
        /// </summary>
        public List<UddiId> ProfileIds { get; private set; }
        
        /// <summary>
        /// The profile role (buyer, seller)
        /// </summary>
        public string ProfileRoleIdentifier { get; private set; }
        
        /// <summary>
        /// List of transport protocols accepted as a valid result
        /// </summary>
        public List<EndpointAddressTypeCode> AcceptedTransportProtocols { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="serviceId"></param>
        /// <param name="profileIds"></param>
        /// <param name="acceptedTransportProtocols"></param>
        /// <param name="profileRoleIdentifier"></param>
        public LookupParameters(
            Identifier identifier,
            UddiId serviceId,
            List<UddiId> profileIds,
            List<EndpointAddressTypeCode> acceptedTransportProtocols,
            string profileRoleIdentifier) {
            
            if (identifier == null) throw new ArgumentNullException("identifier");
            if (serviceId == null) throw new ArgumentNullException("serviceId");
            if (profileIds == null) throw new ArgumentNullException("profileIds");
            if (acceptedTransportProtocols == null) throw new ArgumentNullException("acceptedTransportProtocols");
            if (profileRoleIdentifier == null) throw new ArgumentNullException("profileRoleIdentifier");
            if (profileIds.Count == 0) throw new ArgumentException("profileIds must contain at least one item");

            Identifier = identifier;
            ServiceId = serviceId;
            ProfileIds = profileIds;
            AcceptedTransportProtocols = acceptedTransportProtocols;
            ProfileRoleIdentifier = profileRoleIdentifier;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="serviceId"></param>
        /// <param name="profileIds"></param>
        /// <param name="acceptedTransportProtocols"></param>
        public LookupParameters(
            Identifier identifier,
            UddiId serviceId,
            List<UddiId> profileIds,
            List<EndpointAddressTypeCode> acceptedTransportProtocols) {

            if (identifier == null) throw new ArgumentNullException("identifier");
            if (serviceId == null) throw new ArgumentNullException("serviceId");
            if (profileIds == null) throw new ArgumentNullException("profileIds");
            if (acceptedTransportProtocols == null) throw new ArgumentNullException("acceptedTransportProtocols");
            if (profileIds.Count == 0) throw new ArgumentException("profileIds must contain at least one item");

            Identifier = identifier;
            ServiceId = serviceId;
            ProfileIds = profileIds;
            AcceptedTransportProtocols = acceptedTransportProtocols;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="serviceId"></param>
        /// <param name="acceptedTransportProtocols"></param>
        public LookupParameters(Identifier identifier, UddiId serviceId, List<EndpointAddressTypeCode> acceptedTransportProtocols) {
            if (identifier == null) throw new ArgumentNullException("identifier");
            if (serviceId == null) throw new ArgumentNullException("serviceId");
            if (acceptedTransportProtocols == null) throw new ArgumentNullException("acceptedTransportProtocols");

            Identifier = identifier;
            ServiceId = serviceId;
            AcceptedTransportProtocols = acceptedTransportProtocols;
        }
    }
}