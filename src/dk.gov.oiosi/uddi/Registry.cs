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
    public class Registry
    {
        private List<string> endpoints;

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
            this.endpoints = endpoints;
        }

        /// <summary>
        /// A list of access points to this registry
        /// </summary>
        [XmlArray("EndpointCollection")]
        [XmlArrayItem("Endpoint")]
        public List<string> Endpoints
        {
            get { return this.endpoints; }
            set { this.endpoints = value; }
        }

        /// <summary>
        /// Return the list of endpoints as Uri's
        /// </summary>
        /// <returns>The list of Uri's</returns>
		public List<Uri> GetAsUris()
		{
            List<Uri> endpointList = new List<Uri>();
            Uri uri;
            foreach (string endpoint in this.endpoints)
            {
                if (string.IsNullOrEmpty(endpoint))
                {
                    // the endpint is invalid
                }
                else
                {
                    uri = new Uri(endpoint);
                    endpointList.Add(uri);
                }
            }

            return endpointList;
		}
    }
}
