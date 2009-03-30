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
using System.Xml.Serialization;

namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// Policy describing the way we connect to a mail server, for example connection time and polling pattern.
    /// </summary>
    [XmlRoot("MailServerConnectionPolicy", Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
    public class MailServerConnectionPolicy {
        /// <summary>
        /// polling intercal constant
        /// </summary>
        public const int DefaultPollingIntervalInSeconds = 5;
        
        /// <summary>
        /// polling pattern constant
        /// </summary>
        public const MailServerPollingPattern DefaultPollingPattern = MailServerPollingPattern.LogOn_PollOnce_LogOff;

        private TimeSpan _pollingInterval = new TimeSpan(5000);
        private MailAuthenticationMode _authenticationMode = MailAuthenticationMode.PlainText;
        private MailServerPollingPattern _pollingPattern = MailServerPollingPattern.LogOn_PollOnce_LogOff;
        private TcpPort _port;

        /// <summary>
        /// How often should the server be polled?
        /// </summary>
        [XmlElement("PollingInterval")]
        public TimeSpan PollingInterval {
            get { return _pollingInterval; }
            set { _pollingInterval = value; }
        }

        /// <summary>
        /// What polling pattern should be used? 
        /// - LogOn, poll once, log out
        /// - LogOn, poll with a certain interval for a certain time, log out
        /// - Poll
        /// </summary>
        [XmlElement("PollingPattern")]
        public MailServerPollingPattern PollingPattern {
            get { return _pollingPattern; }
            set { _pollingPattern = value; }
        }

        /// <summary>
        /// What type of authentication is needed when logging on
        /// </summary>
        [XmlElement("AuthenticationMode")]
        public MailAuthenticationMode AuthenticationMode {
            get { return _authenticationMode; }
            set { _authenticationMode = value; }
        }

        /// <summary>
        /// The TCP port
        /// </summary>
        [XmlElement("Port")]
        public TcpPort Port {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MailServerConnectionPolicy(TimeSpan pollingTimeSpan, MailServerPollingPattern pollingPattern, TcpPort port) {
            _pollingInterval = pollingTimeSpan;
            _pollingPattern = pollingPattern;
            _port = port;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MailServerConnectionPolicy(TcpPort port) : this() {
            _port = port;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MailServerConnectionPolicy(TimeSpan pollingTimeSpan, MailServerPollingPattern pollingPattern) {
            _pollingInterval = pollingTimeSpan;
            _pollingPattern = pollingPattern;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public MailServerConnectionPolicy(TimeSpan pollingInterval) {
            _pollingInterval = pollingInterval;
            _pollingPattern = DefaultPollingPattern;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MailServerConnectionPolicy() {
            _pollingInterval = new TimeSpan(0,0, DefaultPollingIntervalInSeconds);
            _pollingPattern = DefaultPollingPattern;
        }
    }
}