using System;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace dk.gov.oiosi.security.lookup {
    /// <summary>
    /// Class that represents the configuration of a certificate store identification. The configuration
    /// is presents in the app.config or web.config
    /// </summary>
    public class CertificateStoreIdentificationAppConfiguration : ConfigurationElement, ICertificateStoreIdentification {
        /// <summary>
        /// Constant definition of what the store location element is called
        /// </summary>
        public const string StoreLocationName = "storeLocation";
        /// <summary>
        /// Constant defintion of what the store name element is called
        /// </summary>
        public const string StoreNameName = "storeName";
        /// <summary>
        /// Constant definition of what the serial number element is called
        /// </summary>
        public const string SerialNumberName = "serialNumber";

        #region ICertificateStoreIdentification Members

        /// <summary>
        /// Gets or sets the store location
        /// </summary>
        [ConfigurationProperty(StoreLocationName, IsRequired=true)]
        public StoreLocation StoreLocation {
            get { return (StoreLocation)this[StoreLocationName]; }
            set { this[StoreLocationName] = value; }
        }

        /// <summary>
        /// Gets or sets the store name
        /// </summary>
        [ConfigurationProperty(StoreNameName, IsRequired=true)]
        public System.Security.Cryptography.X509Certificates.StoreName StoreName {
            get { return (StoreName)this[StoreNameName]; }
            set { this[StoreNameName] = value; }
        }

        /// <summary>
        /// Gets or sets the serial number
        /// </summary>
        [ConfigurationProperty(SerialNumberName, IsRequired = true)]
        public string SerialNumber {
            get { return (string)this[SerialNumberName]; }
            set { this[SerialNumberName] = value; }
        }

        #endregion
    }
}
