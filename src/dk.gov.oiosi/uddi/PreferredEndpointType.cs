using System.Xml.Serialization;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// If more than one type of endpoint is returned from the UDDI lookup, which scheme is preferred?
    /// </summary>
    public enum PreferredEndpointType {
        /// <summary>
        /// A HTTP endpoint
        /// </summary>
        [XmlEnum("http")]
        http,

        /// <summary>
        /// A mail endpoint
        /// </summary>
        [XmlEnum("mailto")]
        mailto
    }
}
