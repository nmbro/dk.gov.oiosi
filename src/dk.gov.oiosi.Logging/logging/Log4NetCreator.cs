using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using log4net.Config;
using System.Configuration;
using System.Collections.Specialized;

namespace dk.gov.oiosi.logging
{
    public class Log4NetCreator : ILoggerCreator
    {
        public Log4NetCreator()
        {
        }

        /// <summary>
        /// Create a common logger, that can be used to log though log4net
        /// </summary>
        /// <returns>A class that can be used to log though log4net</returns>
        public ILogger Create()
        {
            return this.Create("dk.gov.oiosi.logging.Log4Net");
        }

        /// <summary>
        /// Create a named logger, that can be used to log though log4net
        /// </summary>
        /// <param name="loggerName">The name of the logger</param>
        /// <returns>A class that can be used to log though log4net</returns>
        public ILogger Create(string loggerName)
        {
            return new Log4Net(LogManager.GetLogger(loggerName));
        }

        /// <summary>
        /// Create a typed logger, that can be used to log though log4net
        /// </summary>
        /// <param name="type">The typed of the logger</param>
        /// <returns>A class that can be used to log though log4net</returns>
        public ILogger Create(Type type)
        {
            return new Log4Net(LogManager.GetLogger(type));
        }

        /// <summary>
        /// Configurate the log4net logger creator
        /// </summary>
        public void Configurate()
        {
            // first we try to retrive the configuration file name from app.config
            string key = "log4Net4RaspConfigurationFile";
            string log4NetConfigurationFile = ConfigurationManager.AppSettings[key];

            if(string.IsNullOrEmpty(log4NetConfigurationFile))
            {
                // the key did not exist in the default app.config
                // try the dk.gov.oiosi.logging.dll.config
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(LoggingConstant.AppConfigName);
                AppSettingsSection section = configuration.AppSettings;
                KeyValueConfigurationCollection collection = section.Settings;
                KeyValueConfigurationElement element = collection[key];
                if(element != null)
                {
                    log4NetConfigurationFile = element.Value;
                }
            }

            if (string.IsNullOrEmpty(log4NetConfigurationFile))
            {
                // configuration file still not identified. Trying default filename
                log4NetConfigurationFile = "log4net.xml";
            }

            FileInfo fileInfo = new FileInfo(log4NetConfigurationFile);
            if (fileInfo.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(fileInfo);
            }
            else
            {
                throw new LoggingException(" The configuration file '" + fileInfo.FullName + "' does not exist.");
            }
        }
    }
}
