using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.uddi.category;

namespace dk.gov.oiosi.uddi {
    public class UddiLookupParameters {
        public IIdentifier Identifier { get; private set; }
        public UddiId ServiceId { get; private set; }
        public List<UddiId> ProfileIds { get; private set; }
        public string ProfileRoleIdentifier { get; private set; }
        public List<EndpointAddressTypeCode> IncludedTransportProtocols { get; private set; }

        public UddiLookupParameters(
            IIdentifier identifier,
            UddiId serviceId,
            List<UddiId> profileIds,
            List<EndpointAddressTypeCode> includedTransportProtocols,
            string profileRoleIdentifier) {
            
            if (identifier == null) throw new ArgumentNullException("identifier");
            if (serviceId == null) throw new ArgumentNullException("serviceId");
            if (profileIds == null) throw new ArgumentNullException("profileIds");
            if (includedTransportProtocols == null) throw new ArgumentNullException("includedTransportProtocols");
            if (profileRoleIdentifier == null) throw new ArgumentNullException("profileRoleIdentifier");
            
            Identifier = identifier;
            ServiceId = serviceId;
            ProfileIds = profileIds;
            IncludedTransportProtocols = includedTransportProtocols;
            ProfileRoleIdentifier = profileRoleIdentifier;
        }

        public UddiLookupParameters(
            IIdentifier identifier,
            UddiId serviceId,
            List<UddiId> profileIds,
            List<EndpointAddressTypeCode> includedTransportProtocols) {

            if (identifier == null) throw new ArgumentNullException("identifier");
            if (serviceId == null) throw new ArgumentNullException("serviceId");
            if (profileIds == null) throw new ArgumentNullException("profileIds");
            if (includedTransportProtocols == null) throw new ArgumentNullException("includedTransportProtocols");

            Identifier = identifier;
            ServiceId = serviceId;
            ProfileIds = profileIds;
            IncludedTransportProtocols = includedTransportProtocols;
        }

    }
}