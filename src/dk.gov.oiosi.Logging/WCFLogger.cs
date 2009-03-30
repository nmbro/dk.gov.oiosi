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
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace dk.gov.oiosi.logging {
    
    /// <summary>
    /// Class that logs in the WCF style
    /// </summary>
    public class WCFLogger {

        private static int _id = new Random().Next(int.MaxValue);

        /// <summary>
        /// Writes to a trace source
        /// </summary>
        /// <param name="source">The name of the trace source</param>
        /// <param name="eventType">The type of event to be logged</param>
        /// <param name="message">The message to be written</param>
        public static void Write(string source, TraceEventType eventType, string message) {
            try {
                TraceSource traceSource = new TraceSource(source);
                traceSource.Switch.Level = SourceLevels.All;

                // Format the message the way WCF usually does
                StringBuilder xmlStringBuilder = new StringBuilder();
                xmlStringBuilder.Append("<TraceRecord xmlns=\"http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord\" Severity=\"");
                xmlStringBuilder.Append(eventType);
                xmlStringBuilder.Append("\">");
                xmlStringBuilder.Append("<TraceIdentifier>");
                xmlStringBuilder.Append(source);
                xmlStringBuilder.Append("</TraceIdentifier>");
                AddDescription(xmlStringBuilder, message);
                AddAppDomain(xmlStringBuilder);
                xmlStringBuilder.Append("<Source>");
                xmlStringBuilder.Append(source);
                xmlStringBuilder.Append("</Source>");
                xmlStringBuilder.Append("</TraceRecord>");

                string xml = xmlStringBuilder.ToString();
                XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(xml));
                XPathDocument xPathDoc = new XPathDocument(xmlTextReader);
                XPathNavigator myNav = xPathDoc.CreateNavigator();

                // Write
                traceSource.TraceData(eventType, _id, myNav);
                traceSource.Flush();
                traceSource.Close();
            }
            catch {/* Do nothing. No exceptions should be throw*/ }
        }

        /// <summary>
        /// Writes to a trace source with the name of the assembly from which this method was called
        /// </summary>
        /// <param name="eventType">The type of event to be logged</param>
        /// <param name="message">The message to be written</param>
        public static void Write(TraceEventType eventType, string message){
            try {
                // Set the source name to the name of the assmbly from which this method was called
                string callingAssemblyName = Assembly.GetCallingAssembly().GetName().Name;
                Write(callingAssemblyName, eventType, message);               
            }
            catch {/* Do nothing. No exceptions should be throw*/ }
        }

        private static void AddDescription(StringBuilder xmlStringBuilder, string message) {
            xmlStringBuilder.Append("<Description>");
            if (message != null)
                xmlStringBuilder.Append(message);
            xmlStringBuilder.Append("</Description>");
        }

        private static void AddAppDomain(StringBuilder xmlStringBuilder) {
            xmlStringBuilder.Append("<AppDomain>");
            Assembly assembly = Assembly.GetEntryAssembly();
            if (assembly != null)
                xmlStringBuilder.Append(assembly.GetName().Name);
            xmlStringBuilder.Append("</AppDomain>");
        }
    }
}
