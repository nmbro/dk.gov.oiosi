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
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System.Xml.Serialization;

namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// Stores the information needed to connect to and send/receive mails 
    /// from a mail server
    /// </summary>
    [XmlRoot("MailServerConfiguration", Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
    public class MailServerConfiguration : IMailServerConfiguration {
        #region Properties

        /// <summary>
        /// The address of the mail server
        /// </summary>
        [XmlElement("ServerAddress")]
        public string ServerAddress {
            get { return _serverAddress; }
            set { _serverAddress = value; }
        }
        string _serverAddress;

        /// <summary>
        /// Password used to log on to the server (combined with the UserName property)
        /// </summary>
        [XmlElement("Password")]
        public string Password {
            get { return _password; }
            set { _password = value; }
        }
        string _password;

        /// <summary>
        /// Username used to log on to the server (combined with the Password property)
        /// </summary>
        [XmlElement("UserName")]
        public string UserName {
            get { return _userName; }
            set { _userName = value; }
        }
        string _userName;


        /// <summary>
        /// What address should one reply to?
        /// </summary>
        [XmlElement("ReplyAddress")]
        public string ReplyAddress {
            get { return _replyAddress; }
            set { _replyAddress = value; }
        }
        string _replyAddress;

        /// <summary>
        /// Policy describing the way we connect to the mail server, for example connection time and polling pattern.
        /// </summary>
        [XmlElement("ConnectionPolicy")]
        public MailServerConnectionPolicy ConnectionPolicy {
            get { return _connectionPolicy; }
            set { _connectionPolicy = value; }
        }
        MailServerConnectionPolicy _connectionPolicy;
       
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public MailServerConfiguration(string serverAddress, string userName, string password, string replyAddress, MailServerConnectionPolicy connectionPolicy) {
            _userName = userName;
            _password = password;
            _serverAddress = serverAddress;
            _connectionPolicy = connectionPolicy;
            _replyAddress = replyAddress;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public MailServerConfiguration(string serverAddress, string userName, string password, string replyAddress) {
            _userName = userName;
            _password = password;
            _serverAddress = serverAddress;
            _replyAddress = replyAddress;
            _connectionPolicy = new MailServerConnectionPolicy();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MailServerConfiguration() { }

        #endregion

        /// <summary>
        /// True if complete
        /// </summary>
        /// <returns>True if complete</returns>
        public bool IsComplete() { 
            return (
                    !string.IsNullOrEmpty(ServerAddress) &&
                    !string.IsNullOrEmpty(ReplyAddress) &&
                    !(_connectionPolicy.AuthenticationMode != MailAuthenticationMode.None && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(UserName))
                    );
        }
    }
}