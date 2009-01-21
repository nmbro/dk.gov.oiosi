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
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.handlers.email;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {

    /// <summary>
    /// Mail binding configuration element
    /// </summary>
    public class EmailBindingExtensionElement: BindingElementExtensionElement, IEmailBindingElementConfiguration {
        /// <summary>
        /// Gets and sets the address of the sending server
        /// </summary>
        [ConfigurationProperty("sendingServerAddress", IsRequired=true)]
        public string SendingServerAddress {
            get { return (string)base["sendingServerAddress"]; }
            set { base["sendingServerAddress"] = value; }
        }

        /// <summary>
        /// Gets and sets the username to the sending server
        /// </summary>
        [ConfigurationProperty("sendingUserName")]
        public string SendingUserName {
            get { return (string)base["sendingUserName"]; }
            set { base["sendingUserName"] = value; }
        }

        /// <summary>
        /// Gets and sets the password to the sending server
        /// </summary>
        [ConfigurationProperty("sendingPassword")]
        public string SendingPassword {
            get { return (string)base["sendingPassword"]; }
            set { base["sendingPassword"] = value; }
        }

        /// <summary>
        /// Gets and sets the address of the receiving server
        /// </summary>
        [ConfigurationProperty("receivingServerAddress", IsRequired=true)]
        public string ReceivingServerAddress {
            get { return (string)base["receivingServerAddress"]; }
            set { base["receivingServerAddress"] = value; }
        }

        /// <summary>
        /// Gets and sets the username to the receiving server
        /// </summary>
        [ConfigurationProperty("receivingUserName",IsRequired=true)]
        public string ReceivingUserName {
            get { return (string)base["receivingUserName"]; }
            set { base["receivingUserName"] = value; }
        }

        /// <summary>
        /// Gets and sets the password to the receiving server
        /// </summary>
        [ConfigurationProperty("receivingPassword", IsRequired = true)]
        public string ReceivingPassword {
            get { return (string)base["receivingPassword"]; }
            set { base["receivingPassword"] = value; }
        }

        /// <summary>
        /// Gets and sets the polling interval
        /// </summary>
        [ConfigurationProperty("pollingInterval", IsRequired = false)]
        public TimeSpan PollingInterval {
            get { return (TimeSpan)base["pollingInterval"]; }
            set { base["pollingInterval"] = value; }
        }

        /// <summary>
        /// Gets and sets the imap folder
        /// </summary>
        [ConfigurationProperty("imapFolder")]
        public string ImapFolder {
            get { return (string)base["imapFolder"]; }
            set { base["imapFolder"] = value; }
        }

        /// <summary>
        /// Gets and sets reply mail address, e.g. "test@domain.com"
        /// </summary>
        [ConfigurationProperty("replyAddress", IsRequired = true)]
        public string ReplyAddress {
            get { return (string)base["replyAddress"]; }
            set { base["replyAddress"] = value; }
        }

        /// <summary>
        /// Gets and sets authentication mode - set to "SSL" if you run SMTP over SSL.
        /// </summary>
        [ConfigurationProperty("sendingAuthenticationMode", IsRequired = false, DefaultValue=MailAuthenticationMode.None)]
        public MailAuthenticationMode SendingAuthenticationMode {
            get { return (MailAuthenticationMode)base["sendingAuthenticationMode"]; }
            set { base["sendingAuthenticationMode"] = value; }
        }

        /// <summary>
        /// Gets and sets authentication mode - set to "SSL" if you run POP over SSL.
        /// </summary>
        [ConfigurationProperty("receivingAuthenticationMode", IsRequired = false, DefaultValue = MailAuthenticationMode.PlainText)]
        public MailAuthenticationMode ReceivingAuthenticationMode {
            get { return (MailAuthenticationMode)base["receivingAuthenticationMode"]; }
            set { base["receivingAuthenticationMode"] = value; }
        }


        /// <summary>
        /// Gets and sets the port for receiving mails
        /// </summary>
        [ConfigurationProperty("receivingPort", IsRequired = false, DefaultValue = 110)]
        public int ReceivingPort {
            get { return (int)base["receivingPort"]; }
            set { base["receivingPort"] = value; }
        }

        /// <summary>
        /// Gets and sets the port for sending mails
        /// </summary>
        [ConfigurationProperty("sendingPort", IsRequired = false, DefaultValue = 25)]
        public int SendingPort {
            get { return (int)base["sendingPort"]; }
            set { base["sendingPort"] = value; }
        }

        /// <summary>
        /// Gets and sets outbox implementation
        /// </summary>
        [ConfigurationProperty("outboxImplementation", IsRequired = true)]
        public string OutboxImplementation {
            get { return (string)base["outboxImplementation"]; }
            set { base["outboxImplementation"] = value; }
        }

        /// <summary>
        /// Gets and sets inbox implementation
        /// </summary>
        [ConfigurationProperty("inboxImplementation", IsRequired = true)]
        public string InboxImplementation {
            get { return (string)base["inboxImplementation"]; }
            set { base["inboxImplementation"] = value; }
        }

        /// <summary>
        /// Gets and sets the address of the sending server
        /// </summary>
        [ConfigurationProperty("maxReceivedMessageSize", IsRequired = false)]
        public long MaxReceivedMessageSize
        {
            get { return (long)base["maxReceivedMessageSize"]; }
            set { base["maxReceivedMessageSize"] = value; }
        }

        /// <summary>
        /// Gets type of binding element
        /// </summary>
        public override Type BindingElementType {
            get { return typeof(EmailBindingElement); }
        }

        /// <summary>
        /// Creates a RaspEmailBindingElement
        /// </summary>
        /// <returns>Binding element</returns>
        protected override BindingElement CreateBindingElement() {
            return (BindingElement)new EmailBindingElement(this);
        }
    }
}