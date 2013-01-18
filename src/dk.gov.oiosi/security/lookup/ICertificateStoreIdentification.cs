using System.Security.Cryptography.X509Certificates;

namespace dk.gov.oiosi.security.lookup {
    /// <summary>
    /// Interface that describes how the store configuration lookup can lookup
    /// </summary>
    public interface ICertificateStoreIdentification {
        /// <summary>
        /// Gets and sets the store location.
        /// </summary>
        StoreLocation StoreLocation { get; set; }
        /// <summary>
        /// Gets and sets the store name.
        /// </summary>
        StoreName StoreName { get; set; }
        /// <summary>
        /// Gets and sets the serial number.
        /// </summary>
        string SerialNumber { get; set; }
    }
}
