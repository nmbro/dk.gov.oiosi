

namespace dk.gov.oiosi.appConfig
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// A Configuration element, used in the app.config configuration
    /// </summary>
    public class ConfigurationElement : System.Configuration.ConfigurationElement
    {
        /// <summary>
        /// The key attribute, used in the app.config
        /// </summary>
        private const string AttributeKey = "key";

        /// <summary>
        /// The value attribute, used in the app.config
        /// </summary>
        private const string AttributeValue = "value";

        /// <summary>
        /// Initializes a new instance of the ConfigurationElement class.
        /// </summary>
        public ConfigurationElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConfigurationElement class.
        /// </summary>
        /// <param name="key">The lookup key of the configuration</param>
        /// <param name="value">The configuration value</param>
        public ConfigurationElement(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the key attribute of the configuration element
        /// </summary>
        [ConfigurationProperty(AttributeKey, DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Key
        {
            get
            {
                return Convert.ToString(base[AttributeKey]);
            }

            set
            {
                base[AttributeKey] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value attribute of the configuration element
        /// </summary>
        [ConfigurationProperty(AttributeValue, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Value
        {
            get
            {
                return Convert.ToString(base[AttributeValue]);
            }

            set
            {
                base[AttributeValue] = value;
            }
        }
    }
}
