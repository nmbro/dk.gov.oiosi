using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;

namespace dk.gov.oiosi.uddi {
    
    /// <summary>
    /// Parameters for use in lookup
    /// </summary>
    public class LookupParameters 
    {
        public const string RASPPROFILECONFORMANCECLAIM = "http://oio.dk/profiles/OIOSI/1.0/secureReliableAsyncProfile/1.0/";
        
        // Used in the Reference Klient
        public const string BASICPROFILECONFORMANCECLAIM = "http://oio.dk/profiles/BasicProfile1.1/";
        //public const string MODELTPROFILECONFORMANCECLAIM = "http://oio.dk/profiles/OWSA/modelT/1.0/";
        //public const string OTHERPROFILECONFORMANCECLAIM = "http://oio.dk/profiles/otherProfile/";

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
        /// The profile conformance claim, default is 
        /// "http://oio.dk/profiles/OIOSI/1.0/secureReliableAsyncProfile/1.0/"
        /// which is the RASP protocol.
        /// </summary>
        public string ProfileConformanceClaim { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier">The identifier on the service (endpointKey and endpointKeyType)</param>
        /// <param name="serviceId">The identifier on the service type (portType)</param>
        /// <param name="profileIds">The profiles that must be supported</param>
        /// <param name="acceptedTransportProtocols">The accepted transport protocols (http, mail)</param>
        /// <param name="profileRoleIdentifier">The profile role (BuyerParty, SellerParty)</param>
        /// <param name="profileConformanceClaim"></param>
        public LookupParameters(
            Identifier identifier,
            UddiId serviceId,
            List<UddiId> profileIds,
            List<EndpointAddressTypeCode> acceptedTransportProtocols,
            string profileRoleIdentifier,
            string profileConformanceClaim) {

            if (identifier == null) throw new ArgumentNullException("identifier");
            if (serviceId == null) throw new ArgumentNullException("serviceId");
            if (profileIds == null) throw new ArgumentNullException("profileIds");
            if (acceptedTransportProtocols == null) throw new ArgumentNullException("acceptedTransportProtocols");
            if (profileRoleIdentifier == null) throw new ArgumentNullException("profileRoleIdentifier");
            if (profileIds.Count == 0) throw new ArgumentException("profileIds must contain at least one item");
            if (string.IsNullOrEmpty(profileConformanceClaim)) throw new ArgumentException("profileConformanceClaim cannot be null or empty");

            Identifier = identifier;
            ServiceId = serviceId;
            ProfileIds = profileIds;
            AcceptedTransportProtocols = acceptedTransportProtocols;
            ProfileRoleIdentifier = profileRoleIdentifier;
            ProfileConformanceClaim = profileConformanceClaim;
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="identifier">The identifier on the service (endpointKey and endpointKeyType)</param>
        /// <param name="serviceId">The identifier on the service type (portType)</param>
        /// <param name="profileIds">The profiles that must be supported</param>
        /// <param name="acceptedTransportProtocols">The accepted transport protocols (http, mail)</param>
        /// <param name="profileRoleIdentifier">The profile role (BuyerParty, SellerParty)</param>
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
            ProfileConformanceClaim = RASPPROFILECONFORMANCECLAIM;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="identifier">The identifier on the service (endpointKey and endpointKeyType)</param>
        /// <param name="serviceId">The identifier on the service type (portType)</param>
        /// <param name="profileIds">The profiles that must be supported</param>
        /// <param name="acceptedTransportProtocols">The accepted transport protocols (http, mail)</param>
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
            ProfileConformanceClaim = RASPPROFILECONFORMANCECLAIM;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="identifier">The identifier on the service (endpointKey and endpointKeyType)</param>
        /// <param name="serviceId">The identifier on the service type (portType)</param>
        /// <param name="acceptedTransportProtocols">The accepted transport protocols (http, mail)</param>
        /// <param name="profileConformanceClaim"></param>
        public LookupParameters(
            Identifier identifier, 
            UddiId serviceId, 
            List<EndpointAddressTypeCode> acceptedTransportProtocols,
            string profileConformanceClaim) {
            if (identifier == null) throw new ArgumentNullException("identifier");
            if (serviceId == null) throw new ArgumentNullException("serviceId");
            if (acceptedTransportProtocols == null) throw new ArgumentNullException("acceptedTransportProtocols");
            if (string.IsNullOrEmpty(profileConformanceClaim)) throw new ArgumentException("string profileConformanceClaim cannot be null or empty");

            Identifier = identifier;
            ServiceId = serviceId;
            AcceptedTransportProtocols = acceptedTransportProtocols;
            ProfileConformanceClaim = profileConformanceClaim;
        }

        /// <summary>
        /// Constructor that is used to get all services for a specific identifier and
        /// a specific service type in the UDDI.
        /// </summary>
        /// <param name="identifier">The identifier on the service (endpointKey and endpointKeyType)</param>
        /// <param name="serviceId">The identifier on the service type (portType)</param>
        /// <param name="acceptedTransportProtocols">The accepted transport protocols (http, mail)</param>
        public LookupParameters(
            Identifier identifier, 
            UddiId serviceId, 
            List<EndpointAddressTypeCode> acceptedTransportProtocols) 
        {
            // Test code call this one
            if (identifier == null) throw new ArgumentNullException("identifier");
            if (serviceId == null) throw new ArgumentNullException("serviceId");
            if (acceptedTransportProtocols == null) throw new ArgumentNullException("acceptedTransportProtocols");

            Identifier = identifier;
            ServiceId = serviceId;
            AcceptedTransportProtocols = acceptedTransportProtocols;
            ProfileConformanceClaim = RASPPROFILECONFORMANCECLAIM;
        }

        /// <summary>
        /// Constructor that is used to get all services for at specific identifier
        /// in the UDDI.
        /// </summary>
        /// <param name="identifier">The identifier on the service (endpointKey and endpointKeyType)</param>
        /// <param name="acceptedTransportProtocols">The accepted transport protocols (http, mail)</param>
        public LookupParameters(Identifier identifier, List<EndpointAddressTypeCode> acceptedTransportProtocols) {
            if (identifier == null) throw new ArgumentNullException("identifier");
            if (acceptedTransportProtocols == null) throw new ArgumentNullException("acceptedTransportProtocols");

            Identifier = identifier;
            AcceptedTransportProtocols = acceptedTransportProtocols;
            ProfileConformanceClaim = RASPPROFILECONFORMANCECLAIM;
        }
    }
}