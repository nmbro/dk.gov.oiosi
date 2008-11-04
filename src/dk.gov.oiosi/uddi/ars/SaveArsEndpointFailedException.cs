using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.uddi.ars {
    public class SaveArsEndpointFailedException : ArsException {
        public SaveArsEndpointFailedException(ArsEndpoint endpoint, Exception innerException) : base(UpdateArsEndpointFailedException.GetKeywords(endpoint), innerException) { }
    }
}
