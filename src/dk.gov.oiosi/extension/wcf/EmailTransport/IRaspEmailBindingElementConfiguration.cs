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

using dk.gov.oiosi.communication.handlers.email;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {
    public interface IRaspEmailBindingElementConfiguration {

        /// <summary>
        /// Gets and sets the address of the sending server
        /// </summary>
        string SendingServerAddress { get; set; }

        /// <summary>
        /// Gets and sets the username to the sending server
        /// </summary>
        string SendingUserName { get; set; }

        /// <summary>
        /// Gets and sets the password to the sending server
        /// </summary>
        string SendingPassword { get; set; }

        /// <summary>
        /// Gets and sets the address of the receiving server
        /// </summary>
        string ReceivingServerAddress { get; set; }

        /// <summary>
        /// Gets and sets the username to the receiving server
        /// </summary>
        string ReceivingUserName { get; set; }

        /// <summary>
        /// Gets and sets the password to the receiving server
        /// </summary>
        string ReceivingPassword { get; set; }

        /// <summary>
        /// Gets and sets the polling interval
        /// </summary>
        TimeSpan PollingInterval { get; set; }

        /// <summary>
        /// Gets and sets the imap folder
        /// </summary>
        string ImapFolder { get; set; }

        /// <summary>
        /// Gets and sets reply mail address, e.g. "test@domain.com"
        /// </summary>
        string ReplyAddress { get; set; }

        /// <summary>
        /// Gets and sets authentication mode - set to "SSL" if you run SMTP over SSL.
        /// </summary>
        MailAuthenticationMode SendingAuthenticationMode { get; set; }

        /// <summary>
        /// Gets and sets authentication mode - set to "SSL" if you run POP over SSL.
        /// </summary>
        MailAuthenticationMode ReceivingAuthenticationMode { get; set; }

        /// <summary>
        /// Gets and sets the port for receiving mails
        /// </summary>
        int ReceivingPort { get; set; }

        /// <summary>
        /// Gets and sets the port for sending mails
        /// </summary>
        int SendingPort { get; set; }

        /// <summary>
        /// Gets and sets outbox implementation
        /// </summary>
        string OutboxImplementation { get; set; }

        /// <summary>
        /// Gets and sets inbox implementation
        /// </summary>
        string InboxImplementation { get; set; }
    }
}
