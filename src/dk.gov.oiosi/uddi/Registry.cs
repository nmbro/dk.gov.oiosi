using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi
{
    /// <summary>
    /// Representation of a service lookup registry (often, but not necessarily, a UDDI registry)
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class 
        Registry
    {
        private List<string> _endpoints;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Registry(){}

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoints">A list of access points to this registry</param>
        public Registry(List<string> endpoints)
        {
            _endpoints = endpoints;
        }

        /// <summary>
        /// A list of access points to this registry
        /// </summary>
        [XmlArray("EndpointCollection")]
        [XmlArrayItem("Endpoint")]
        public List<string> Endpoints
        {
            get { return _endpoints; }
            set { _endpoints = value; }
        }

		public List<Uri> GetAsUris()
		{
			List<Uri> result = new List<Uri>();
			foreach(string s in _endpoints)
				result.Add(new Uri(s));
			return result;
		}
    }
}
