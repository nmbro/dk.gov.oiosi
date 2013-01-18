

namespace dk.gov.oiosi.appConfig
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Text;

    /// <summary>
    /// A Configurations element, used in the app.config configuration
    /// </summary>
    [ConfigurationCollection(typeof(ConfigurationElement))]
    public class ConfigurationsElement : ConfigurationElementCollection
    {
        /// <summary>
        /// Returns the dk.gov.oiosi.appConfig.ConfigurationElement with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to return.</param>
        /// <returns>Thedk.gov.oiosi.appConfig.ConfigurationElement with the specified key; otherwise, null.</returns>
        public new ConfigurationElement this[string key]
        {
            get
            {
                return (ConfigurationElement)base.BaseGet(key);
            }

            set
            {
                base.BaseAdd(value);
            }
        }

        /// <summary>
        /// Gets the dk.gov.oiosi.appConfig.ConfigurationElement at the specified index location.
        /// </summary>
        /// <param name="index">The index location of the dk.gov.oiosi.appConfig.ConfigurationElement to return.</param>
        /// <returns>The dk.gov.oiosi.appConfig.ConfigurationElement at the specified index.</returns>
        public ConfigurationElement this[int index]
        {
            get
            {
                return (ConfigurationElement)BaseGet(index);
            }
        }

        /// <summary>
        /// Create a empty dk.gov.oiosi.appConfig.ConfigurationElement object
        /// </summary>
        /// <returns>A new empty System.Configuration.ConfigurationElement object</returns>
        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new ConfigurationElement();
        }

        /// <summary>
        /// Get the key values of a dk.gov.oiosi.appConfig.ConfigurationElement.
        /// </summary>
        /// <param name="element">The dk.gov.oiosi.appConfig.ConfigurationElement element, from which the key element is desired.</param>
        /// <returns>The key element of the Adk.gov.oiosi.appConfig.ConfigurationElement, as a object.</returns>
        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((ConfigurationElement)element).Key;
        }
    }
}