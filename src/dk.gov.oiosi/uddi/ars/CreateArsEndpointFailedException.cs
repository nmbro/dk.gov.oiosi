using System;
using System.Collections.Generic;
using System.Text;

//TODO: remove this class

namespace dk.gov.oiosi.uddi.ars {
    /// <summary>
    /// Exception thrown if creation of an ARS endpoint fails
    /// </summary>
    public class CreateArsEndpointFailedException : ArsException {
        /// <summary>
        /// Constructor that takes the endpoint that failed to be created and the exception
        /// as the reason why it failed.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="innerException"></param>
        public CreateArsEndpointFailedException(ArsEndpoint endpoint, Exception innerException) : base(UpdateArsEndpointFailedException.GetKeywords(endpoint), innerException) {}
    }
}
