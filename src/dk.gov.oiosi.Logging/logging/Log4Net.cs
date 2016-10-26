using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.logging
{
    /// <summary>
    /// Log4Net logger
    /// </summary>
    public class Log4Net : ILogger
    {
        /// <summary>
        /// A type instance of LogggerLog4Net.
        /// </summary>
        private readonly Type declaringType = typeof(Log4Net);
        //// http://www.l4ndash.com/Log4NetMailArchive/tabid/70/forumid/1/postid/18491/view/topic/Default.aspx

        /// <summary>
        /// The name of the source module property, used by log4net.
        /// </summary>
        private const string SourceModule = "sourceModule";

        /// <summary>
        /// The name of the source class property, used by log4net.
        /// </summary>
        private const string SourceClass = "sourceClass";

        /// <summary>
        /// The name of the source method property, used by log4net.
        /// </summary>
        private const string SourceMethod = "sourceMethod";

        /// <summary>
        /// The line of the source property, used by log4net.
        /// </summary>
        private const string SourceLine = "sourceLine";

        /// <summary>
        /// An instance of a log4net logger, used to log the information.
        /// </summary>
        private log4net.ILog log;

        private SourceDataRetriver sourceDataRetriver;

        /// <summary>
        ///  Initializes a new instance of the Log4Net class.
        /// </summary>
        /// <param name="log">The instance of log4net logger</param>
        public Log4Net(log4net.ILog log)
        {
            this.log = log;
            this.sourceDataRetriver = new SourceDataRetriver();
        }

        #region Is... Enable

        /// <summary>
        /// <para>Gets a value indicating whether or not information on trace level is logged</para>
        /// <para>If false, information on debug level is not logged</para>
        /// <para>If true, information on debug level is logged</para>
        /// </summary>
        public bool IsTraceEnabled
        {
            get
            {
                return this.log.IsDebugEnabled;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether or not information on debug level is logged</para>
        /// <para>If false, information on debug level is not logged</para>
        /// <para>If true, information on debug level is logged</para>
        /// </summary>
        public bool IsDebugEnabled
        {
            get
            {
                return this.log.IsDebugEnabled;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether or not information on info level is logged</para>
        /// <para>If false, information on info level is not logged</para>
        /// <para>If true, information on info level is logged</para>
        /// </summary>
        public bool IsInfoEnabled
        {
            get
            {
                return this.log.IsInfoEnabled;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether or not information on warning level is logged</para>
        /// <para>If false, information on warning level is not logged</para>
        /// <para>If true, information on warning level is logged</para>
        /// </summary>
        public bool IsWarnEnabled
        {
            get
            {
                return this.log.IsWarnEnabled;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether or not information on error level is logged</para>
        /// <para>If false, information on error level is not logged</para>
        /// <para>If true, information on error level is logged</para>
        /// </summary>
        public bool IsErrorEnabled
        {
            get
            {
                return this.log.IsErrorEnabled;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether or not information on debug level is logged</para>
        /// <para>If false, information on debug level is not logged</para>
        /// <para>If true, information on debug level is logged</para>
        /// </summary>
        public bool IsFatalEnabled
        {
            get
            {
                return this.log.IsFatalEnabled;
            }
        }

        #endregion Is... Enable

        #region Trace

        /// <summary>
        /// Log the message on the trace level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Trace(object message)
        {
            if (this.IsTraceEnabled)
            {
                this.Log(log4net.Core.Level.Trace, message, null);
            }
        }

        /// <summary>
        /// Log the message and the exception on the trace level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        public void Trace(object message, Exception exception)
        {
            if (this.IsTraceEnabled)
            {
                this.Log(log4net.Core.Level.Trace, message, exception);
            }
        }

        #endregion Debug

        #region Debug

        /// <summary>
        /// Log the message on the debug level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Debug(object message)
        {
            if (this.IsDebugEnabled)
            {
                this.Log(log4net.Core.Level.Debug, message, null);
            }
        }

        /// <summary>
        /// Log the message and the exception on the debug level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        public void Debug(object message, Exception exception)
        {
            if (this.IsDebugEnabled)
            {
                this.Log(log4net.Core.Level.Debug, message, exception);
            }
        }

        #endregion Debug

        #region Info

        /// <summary>
        /// Log the message on the info level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Info(object message)
        {
            if (this.IsInfoEnabled)
            {
                this.Log(log4net.Core.Level.Info, message, null);
            }
        }

        /// <summary>
        /// Log the message and the exception on the info level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        public void Info(object message, Exception exception)
        {
            if (this.IsInfoEnabled)
            {
                this.Log(log4net.Core.Level.Info, message, exception);
            }
        }

        #endregion Info

        #region Warn

        /// <summary>
        /// Log the message on the warning level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Warn(object message)
        {
            if (this.IsWarnEnabled)
            {
                this.Log(log4net.Core.Level.Warn, message, null);
            }
        }

        /// <summary>
        /// Log the message and the exception on the warning level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        public void Warn(object message, Exception exception)
        {
            if (this.IsWarnEnabled)
            {
                this.Log(log4net.Core.Level.Warn, message, exception);
            }
        }

        #endregion Warn

        #region Error

        /// <summary>
        /// Log the message on the error level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Error(object message)
        {
            if (this.IsErrorEnabled)
            {
                this.Log(log4net.Core.Level.Error, message, null);
            }
        }

        /// <summary>
        /// Log the message and the exception on the error level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        public void Error(object message, Exception exception)
        {
            if (this.IsErrorEnabled)
            {
                this.Log(log4net.Core.Level.Error, message, exception);
            }
        }

        #endregion Error

        #region Fatal

        /// <summary>
        /// Log the message on the fatal level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Fatal(object message)
        {
            if (this.IsFatalEnabled)
            {
                this.Log(log4net.Core.Level.Fatal, message, null);
            }
        }

        /// <summary>
        /// Log the message and the exception on the fatal level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        public void Fatal(object message, Exception exception)
        {
            if (this.IsFatalEnabled)
            {
                this.Log(log4net.Core.Level.Fatal, message, exception);
            }
        }

        #endregion Fatal

        /// <summary>
        /// Log the message and exception, on the defined level
        /// </summary>
        /// <param name="level">The level of the info</param>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        private void Log(log4net.Core.Level level, object message, Exception exception)
        {
            log4net.Core.LoggingEvent loggingEvent;
            loggingEvent = new log4net.Core.LoggingEvent(
                this.declaringType,
                this.log.Logger.Repository,
                this.log.Logger.Name,
                level,
                message,
                exception);

            SourceData data = this.sourceDataRetriver.SourceData(3);
            loggingEvent.Properties[SourceModule] = data.ModuleName;
            loggingEvent.Properties[SourceClass] = data.ClassName;
            loggingEvent.Properties[SourceMethod] = data.MethodName;
            loggingEvent.Properties[SourceLine] = data.Line;

            this.log.Logger.Log(loggingEvent);
        }
    }
}
