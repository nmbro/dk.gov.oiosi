/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace dk.gov.oiosi.logging 
{
    /// <summary>
    /// Wrapper class used for logging with Microsoft Enterprise Library Logging
    /// </summary>
    public class Logger {
        /// <summary>
        /// Default logging category 
        /// </summary>
        public static LoggerCategories DefaultCategory {
            get { return Logger._defaultCategory; }
            set { Logger._defaultCategory = value; }
        }
        private static LoggerCategories _defaultCategory = LoggerCategories.General;

        /// <summary>
        /// Default logging priority
        /// </summary>
        public static LoggerPriorities DefaultPriority {
            get { return Logger._defaultPriority; }
            set { Logger._defaultPriority = value; }
        }
        private static LoggerPriorities _defaultPriority = LoggerPriorities.Medium;

        /// <summary>
        /// Writes to the log
        /// </summary>
        /// <param name="title">Title of the log entry</param>
        /// <param name="message">Main message</param>
        /// <param name="category">Category of the logged event</param>
        public static void Write(string title, string message, LoggerCategories category) {
            try {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(
                    message,
                    category.ToString(),
                    (int) _defaultPriority,
                    (int) category,          // ID is set to logger category 
                    TraceEventType.Information,
                    title);
            } catch {
                /* Do nothing. The logger should not throw an exception while trying to log another exception */
            }
        }

        /// <summary>
        /// Writes to the log
        /// </summary>
        /// <param name="message">Main message</param>
        /// <param name="category">Category of the logged event</param>
        public static void Write(string message, LoggerCategories category) {
            try {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(
                    message,
                    category.ToString(),
                    (int)_defaultPriority,
                    (int)category,          // ID is set to logger category 
                    TraceEventType.Information,
                    "Trace");
            } catch {
                /* Do nothing. The logger should not throw an exception while trying to log another exception */
            }
        }

        /// <summary>
        /// Writes to the log
        /// </summary>
        /// <param name="title">Title of the log entry</param>
        /// <param name="message">Main message</param>
        /// <param name="priority">Priority of the logged event</param>
        /// <param name="category">Category of the logged event</param>
        /// <param name="type">Type of logged event</param>
        public static void Write(string title, string message, LoggerPriorities priority, LoggerCategories category, TraceEventType type) {
            try {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(                 
                    message,
                    category.ToString(),
                    (int)priority,
                    (int)category,          // ID is set to logger category 
                    type,
                    title);
            } catch {
                /* Do nothing. The logger should not throw an exception while trying to log another exception */
            }
        }

        /// <summary>
        /// Writes a message to the log
        /// </summary>
        /// <param name="title">Title of the log entry</param>
        /// <param name="message">Main message</param>
        public static void Write(string title, string message) {
            Write(title, message, DefaultPriority, DefaultCategory, TraceEventType.Information);
        }

        /// <summary>
        /// Writes an exception to the log
        /// </summary>
        /// <param name="e">The exception to log</param>
        public static void Write(Exception e) {
            Write("Exception", e.ToString(), DefaultPriority, DefaultCategory, TraceEventType.Error);
        }
    }
}
