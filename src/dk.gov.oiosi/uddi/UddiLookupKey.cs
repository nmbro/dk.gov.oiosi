using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.addressing;

namespace dk.gov.oiosi.uddi
{
    public class UddiLookupKey
    {
        private Identifier identifier;
        private UddiId serviceId;
        private Uri endpoint;
        private string profileConformanceClaim;

        public UddiLookupKey(Identifier identifier, UddiId serviceId, Uri endpoint, string profileConformanceClaim)
        {
            this.identifier = identifier;
            this.serviceId = serviceId;
            this.endpoint = endpoint;
            this.profileConformanceClaim = profileConformanceClaim;
        }

        public override int GetHashCode()
        {
            return identifier.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;
            UddiLookupKey other = (UddiLookupKey)obj;

            if (!identifier.Equals(other.identifier)) return false;

            if (serviceId == null && other.serviceId != null) return false;
            if (serviceId != null && other.serviceId == null) return false;
            if (serviceId != null && other.serviceId != null && !serviceId.Equals(other.serviceId)) return false;

            if (!endpoint.Equals(other.endpoint)) return false;

            if (!profileConformanceClaim.Equals(other.profileConformanceClaim)) return false;

            return true;
        }
    }
}
