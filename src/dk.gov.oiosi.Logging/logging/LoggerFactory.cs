using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.appConfig.Logger;
using System.Runtime.Remoting;
using System.Configuration;
using System.IO;

namespace dk.gov.oiosi.logging
{
    /// <summary>
    /// Log factory
    /// </summary>
    public class LoggerFactory
    {
        private static LoggerFactory factory = new LoggerFactory();
        private static ILoggerCreator loggerCreator;

        /// <summary>
        /// Private constructor
        /// </summary>
        private LoggerFactory()
        {
            loggerCreator = this.LoadConfiguration();
        }

        /// <summary>
        /// Create a logger
        /// </summary>
        /// <returns>A instance of a logger</returns>
        public static ILogger Create()
        {
            return loggerCreator.Create();
        }

        /// <summary>
        /// Create a logger
        /// </summary>
        /// <param name="loggerName">The name of the logger</param>
        /// <returns>A instance of a logger</returns>
        public static ILogger Create(string loggerName)
        {
            return loggerCreator.Create(loggerName);
        }

        /// <summary>
        /// Create a logger
        /// </summary>
        /// <param name="source">The type of the logger</param>
        /// <returns>A instance of a logger</returns>
        public static ILogger Create(object source)
        {
            return loggerCreator.Create(source.GetType());
        }

        /// <summary>
        /// Create a logger
        /// </summary>
        /// <param name="type">The type of the logger</param>
        /// <returns>A instance of a logger</returns>
        public static ILogger Create(Type type)
        {
            return loggerCreator.Create(type);
        }

        /// <summary>
        /// Load the logger configuration from the app.config file, and create a ILoggerCreator object
        /// </summary>
        /// <returns>An object of ILoggerCreator</returns>
        private ILoggerCreator LoadConfiguration()
        {
            ILoggerCreator loggerCreator = null;

            try
            {
                LoggerSection loggerSection;

                // Gets the logger section from the default loggins
                loggerSection = (LoggerSection)ConfigurationManager.GetSection(LoggerSection.SectionName);

                if (loggerSection == null)
                {
                    // Gets the logger from the framework app.condig file
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(LoggingConstant.AppConfigName);
                    loggerSection = (LoggerSection)configuration.GetSection(LoggerSection.SectionName);
                }

                if (loggerSection != null)
                {
                    // The LoggerSection exist.
                    // Try to create the defined logger.
                    loggerCreator = this.LoadLoggerElement(loggerSection.LoggerElement);
                }
                else
                {
                    throw new LoggingException("The logger section in the app.config file does not exist.");
                }
            }
            catch (LoggingException)
            {
                throw;
            }
            catch (Exception exception)
            {
                // Failed to create the loggerCreater defined in the app.config file.
                throw new LoggingException("Unable to create a logger base on the parameter in the app.Config file.", exception);
            }

            return loggerCreator;
        }

        /// <summary>
        /// Create a ILoggerCreator, that can create the desired logger, when needed
        /// </summary>
        /// <param name="loggerElement">The logger element, that contain the logger provider and configuration data</param>
        /// <returns>An object of ILoggerCreator</returns>
        private ILoggerCreator LoadLoggerElement(LoggerElement loggerElement)
        {
            ILoggerCreator loggerCreator = null;

            string[] parameters = loggerElement.Creator.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (parameters.Length == 2)
            {
                // Try to load the logger dynamicly
                loggerCreator = this.LoadAssembly(parameters[0], parameters[1]);
            }
            else
            {
                // The numbers of parameter in the logger provider is not supported
                throw new LoggingException("Unable to create a logger base on the parameter '" + loggerElement.Creator + "'. The parameter must contain the classname and assambly name.");
            }

            return loggerCreator;
        }

        /// <summary>
        /// Load the class from the assembly
        /// </summary>
        /// <param name="className">The name of the class (namespace+class name)</param>
        /// <param name="assemblyName">The assembly that contain the class</param>
        /// <returns>An instance of ILoggerCreator</returns>
        private ILoggerCreator LoadAssembly(string className, string assemblyName)
        {
            ObjectHandle objectHandle = Activator.CreateInstance(assemblyName, className);
            ILoggerCreator loggerCreator = (ILoggerCreator)objectHandle.Unwrap();
            loggerCreator.Configurate();

            return loggerCreator;
        }
    }
}
