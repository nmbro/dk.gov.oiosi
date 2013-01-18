

namespace dk.gov.oiosi.appConfig.Logger
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Text;

    /// <summary>
    /// A Logger element, used in the app.config configuration
    /// </summary>
    public class LoggerElement : System.Configuration.ConfigurationElement
    {
        /// <summary>
        /// The provider creator attribute, used in the app.config
        /// </summary>
        private const string AttributeCreator = "creator";

        /// <summary>
        /// The child element name of this node, in the app.config
        /// </summary>
        private const string ElementConfigurations = "LoggerConfigurations";

        /// <summary>
        /// The child element name of the LoggerConfigurations element, in the app.config
        /// </summary>
        private const string ElementConfiguration = "LoggerConfiguration";

        /// <summary>
        /// Initializes a new instance of the LoggerElement class.
        /// </summary>
        public LoggerElement()
        {
        }
        
        /// <summary>
        /// Gets or sets the creator attribute of the logger element
        /// </summary>
        [ConfigurationProperty(AttributeCreator, DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Creator
        {
            get
            {
                return Convert.ToString(base[AttributeCreator]);
            }

            set
            {
                base[AttributeCreator] = value;
            }
        }

        /// <summary>
        /// Gets the dk.gov.oiosi.appConfig.ConfigurationsElement element
        /// </summary>
        [ConfigurationProperty(ElementConfigurations, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ConfigurationsElement), AddItemName = ElementConfiguration)]
        public ConfigurationsElement GetConfigurationsElement
        {
            get 
            { 
                return (ConfigurationsElement)base[ElementConfigurations];
            }
        }
    }
}
