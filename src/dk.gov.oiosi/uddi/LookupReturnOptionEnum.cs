using System.Xml.Serialization;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// Options for the lookup.
    /// </summary>
    public enum LookupReturnOptionEnum {

        /// <summary>
        /// Throws an exceptions if not either one or zero results were returned.
        /// </summary>
        [XmlEnum("noMoreThanOneSetOrFail")]
        noMoreThanOneSetOrFail,

        /// <summary>
        /// If multiple results were returned from the UDDI inquiry, return only the first
        /// </summary>
        [XmlEnum("firstResult")]
        firstResult,

        /// <summary>
        /// Return all results from the UDDI inquiry
        /// </summary>
        [XmlEnum("allResults")]
        allResults
    }
}
