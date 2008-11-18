using System;
using System.Collections.Generic;

namespace dk.gov.oiosi.uddi{
	public class UddiFallbackClient : IUddiLookupClient{
		private readonly IEnumerable<Uri> _fallbackList;

		public UddiFallbackClient(IEnumerable<Uri> fallbackList){
			_fallbackList = fallbackList;
		}

		#region IUddiLookupClient Members

		public List<UddiLookupResponse> Lookup(LookupParameters parameters){

			List<UddiLookupResponse> result = null;

			Exception exception = null;
			foreach (Uri uri in _fallbackList){
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

			// We never got a valid result, so the last known exception is thrown
			throw exception;
		}

		#endregion
	}
}