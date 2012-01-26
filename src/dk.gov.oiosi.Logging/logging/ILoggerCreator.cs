using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.logging
{
    public interface ILoggerCreator
    {
        /// <summary>
        /// Create an instance of ILogger
        /// </summary>
        /// <returns>The created instance of ILogger</returns>
        ILogger Create();

        /// <summary>
        /// Create an instance of ILogger
        /// </summary>
        /// <param name="info">Info to create the logger</param>
        /// <returns>The created instance of ILogger</returns>
        ILogger Create(string info);

        /// <summary>
        /// Create an instance of ILogger
        /// </summary>
        /// <param name="info">The type of that class, that which to retrive a logger</param>
        /// <returns>The created instance of ILogger</returns>
        ILogger Create(Type type);

        /// <summary>
        /// Inform the creator to configurate the logger, based on the values in the app.config file.
        /// </summary>
        void Configurate();

    }
}
