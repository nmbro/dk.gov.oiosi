using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.logging
{
    /// <summary>
    /// <para>Interface to a ILogger object.</para>
    /// <para>There exist five information levels, each described below:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Debug level</term>
    ///             <description> - Information only relevant in debugging</description>
    ///     </item>
    ///     <item>
    ///         <term>Info level</term>
    ///             <description> - Information only relevant on normal information level</description>
    ///     </item>
    /// </list>
    /// </summary>
    public interface ILogger
    {
	    /// <summary>
        /// <para>Gets a value indicating whether or not information on trace level is logged</para>
        /// <para>If false, information on debug trace is not logged</para>
        /// <para>If true, information on debug trace is logged</para>
        /// </summary>
		bool IsTraceEnabled{ get; }
	
        /// <summary>
        /// <para>Gets a value indicating whether or not information on debug level is logged</para>
        /// <para>If false, information on debug level is not logged</para>
        /// <para>If true, information on debug level is logged</para>
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// <para>Gets a value indicating whether or not information on info level is logged</para>
        /// <para>If false, information on info level is not logged</para>
        /// <para>If true, information on info level is logged</para>
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// <para>Gets a value indicating whetheror not information on warning level is logged</para>
        /// <para>If false, information on warning level is not logged</para>
        /// <para>If true, information on warning level is logged</para>
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// <para>Gets a value indicating whether or not information on error level is logged</para>
        /// <para>If false, information on error level is not logged</para>
        /// <para>If true, information on error level is logged</para>
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// <para>Gets a value indicating whether or not information on fatal level is logged</para>
        /// <para>If false, information on fatal level is not logged</para>
        /// <para>If true, information on fatal level is logged</para>
        /// </summary>
        bool IsFatalEnabled { get; }

		/// <summary>
        /// Log the message on the trace level
        /// </summary>
        /// <param name="message">The message to log</param>
        void Trace(object message);

        /// <summary>
        /// Log the message and the exception on the trace level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        void Trace(object message, Exception exception);
		
        /// <summary>
        /// Log the message on the debug level
        /// </summary>
        /// <param name="message">The message to log</param>
        void Debug(object message);

        /// <summary>
        /// Log the message and the exception on the debug level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        void Debug(object message, Exception exception);

        /// <summary>
        /// Log the message on the information level
        /// </summary>
        /// <param name="message">The message to log</param>
        void Info(object message);

        /// <summary>
        /// Log the message and the exception on the information level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// Log the message on the warning level
        /// </summary>
        /// <param name="message">The message to log</param>
        void Warn(object message);

        /// <summary>
        /// Log the message and the exception on the warning level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// Log the message on the error level
        /// </summary>
        /// <param name="message">The message to log</param>
        void Error(object message);

        /// <summary>
        /// Log the message and the exception on the error level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// Log the message on the fatal level
        /// </summary>
        /// <param name="message">The message to log</param>
        void Fatal(object message);

        /// <summary>
        /// Log the message and the exception on the fatal level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception to log</param>
        void Fatal(object message, Exception exception);
    }
}
