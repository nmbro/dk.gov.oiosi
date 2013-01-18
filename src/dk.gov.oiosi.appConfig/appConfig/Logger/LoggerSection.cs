

namespace dk.gov.oiosi.appConfig.Logger
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Text;

    /// <summary>
    ///  A LoggerSection, used in the app.config configuration
    /// </summary>
    public class LoggerSection : ConfigurationSection
    {
        /// <summary>
        /// The name of the section, in the app.config file
        /// </summary>
        public const string SectionName = "LoggerSection"; 
        
        /// <summary>
        /// The name of the element, in the app.config file
        /// </summary>
        private const string ElementLogger = "Logger";

        /// <summary>
        /// Gets the LoggerElement
        /// </summary>
        [ConfigurationProperty(ElementLogger, IsDefaultCollection = true)]
        public LoggerElement LoggerElement
        {
            get 
            { 
                return (LoggerElement)base[ElementLogger]; 
            }
        }
    }
}
