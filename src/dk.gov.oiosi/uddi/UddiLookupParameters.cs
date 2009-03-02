using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;

namespace dk.gov.oiosi.uddi {
    public class UddiLookupParameters {
        public Identifier Identifier { get; private set; }
        public UddiId ServiceId { get; private set; }
        public List<UddiId> ProfileIds { get; private set; }
        public string ProfileRoleIdentifier { get; private set; }
        public List<EndpointAddressTypeCode> AcceptedTransportProtocols { get; private set; }

        public UddiLookupParameters(
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

        public UddiLookupParameters(
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

        public UddiLookupParameters(Identifier identifier, UddiId serviceId, List<EndpointAddressTypeCode> acceptedTransportProtocols) {
            if (identifier == null) throw new ArgumentNullException("identifier");
            if (serviceId == null) throw new ArgumentNullException("serviceId");
            if (acceptedTransportProtocols == null) throw new ArgumentNullException("acceptedTransportProtocols");

            Identifier = identifier;
            ServiceId = serviceId;
            AcceptedTransportProtocols = acceptedTransportProtocols;
        }
    }
}