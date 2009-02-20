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
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.handlers.email;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {

    /// <summary>
    /// Preferences for e-mail transport communication
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class EmailTransportUserConfig {
        private const int DEFAULT_POP3_PORT = 110; // POP3
        private const int DEFAULT_SMTP_PORT = 25;  // SMTP

        private CertificateStoreIdentification _sendCertificate;
        private CertificateStoreIdentification _receiveCertificate;

        private MailServerConfiguration _sendOutbox = new MailServerConfiguration("", "", "", "");
        private MailServerConfiguration _sendInbox = new MailServerConfiguration("", "", "", "");

        private MailServerConfiguration _receiveInbox = new MailServerConfiguration("", "", "", "");
        private MailServerConfiguration _receiveOutbox = new MailServerConfiguration("", "", "", "");

        /// <summary>
        /// Deafult constructor
        /// </summary>
        public EmailTransportUserConfig() {
            TcpPort defaultSmtpPort = (TcpPort)DEFAULT_SMTP_PORT;
            TcpPort defaultPop3Port = (TcpPort)DEFAULT_POP3_PORT;
            _receiveCertificate = new CertificateStoreIdentification();
            _sendCertificate = new CertificateStoreIdentification();
            MailServerConnectionPolicy sendOutboxConnectionPolicy = new MailServerConnectionPolicy(defaultSmtpPort);
            MailServerConnectionPolicy receiveOutboxConnectionPolicy = new MailServerConnectionPolicy(defaultSmtpPort);
            MailServerConnectionPolicy sendInboxConnectionPolicy = new MailServerConnectionPolicy(defaultPop3Port);
            MailServerConnectionPolicy receiveInboxConnectionPolicy = new MailServerConnectionPolicy(defaultPop3Port);
            _sendOutbox = new MailServerConfiguration("", "", "", "", sendOutboxConnectionPolicy);
            _sendInbox = new MailServerConfiguration("", "", "", "", sendInboxConnectionPolicy);
            _receiveInbox = new MailServerConfiguration("", "", "", "", receiveInboxConnectionPolicy);
            _receiveOutbox = new MailServerConfiguration("", "", "", "", receiveOutboxConnectionPolicy);
        }

        /// <summary>
        /// When initiating a send operation the SendOutbox is used
        /// </summary>
        public MailServerConfiguration SendOutbox {
            get { return _sendOutbox; }
            set { _sendOutbox = value; }
        }

        /// <summary>
        /// When receiving reply to a send operation the SendInbox is used
        /// </summary>
        public MailServerConfiguration SendInbox {
            get { return _sendInbox; }
            set { _sendInbox = value; }
        }

        /// <summary>
        /// When receiving a send operation (initiated by other client) then the ReceiveInbox is used
        /// </summary>
        public MailServerConfiguration ReceiveInbox {
            get { return _receiveInbox; }
            set { _receiveInbox = value; }
        }

        /// <summary>
        /// when replying to send operation (initiated by other client) then the ReceiveOutbox is used
        /// </summary>
        public MailServerConfiguration ReceiveOutbox {
            get { return _receiveOutbox; }
            set { _receiveOutbox = value; }
        }

        /// <summary>
        /// Gets and sets the send certificate x509 store indentification
        /// </summary>
        public CertificateStoreIdentification SendCertificate {
            get { return _sendCertificate; }
            set { _sendCertificate = value; }
        }

        /// <summary>
        /// Gets and sets the receive certificate x509 store indentification
        /// </summary>
        public CertificateStoreIdentification ReceiveCertificate {
            get { return _receiveCertificate; }
            set { _receiveCertificate = value; }
        }
    }
}
