using System;
using System.Collections.Generic;

namespace dk.gov.oiosi.uddi{
    /// <summary>
    /// An UDDI lookup client that implements fallbacks.
    /// </summary>
	public class UddiFallbackClient : IUddiLookupClient{
		private readonly IEnumerable<Uri> _fallbackList;

        /// <summary>
        /// Constructor that takes the fallback list as parameter
        /// </summary>
        /// <param name="fallbackList">The fallback list</param>
		public UddiFallbackClient(IEnumerable<Uri> fallbackList){
			_fallbackList = fallbackList;
		}

        #region IUddiLookupClient Members

        /// <summary>
        /// Implementation of the lookup method from the IUddiLookupClient interface
        /// </summary>
        /// <param name="parameters">The parameters used to make a lookup</param>
        /// <returns></returns>
		public List<UddiLookupResponse> Lookup(LookupParameters parameters) {
		    List<UddiLookupResponse> result;

			Exception exception = null;
			foreach (Uri uri in _fallbackList) {
				try{
					IUddiLookupClient client = new UddiLookupClientFactory().CreateUddiLookupClient(uri);
					result = client.Lookup(parameters);
					return result;
				}
				catch(Exception e){
					exception = e;
					continue;
				}
			}
            //The fallbacklist was empty we return an empty list as result.
            if (exception == null) return new List<UddiLookupResponse>();
			// We never got a valid result, so the last known exception is thrown
            throw exception;
		}

		#endregion
	}
}