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
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.handlers.email;

namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// A mail outbox
    /// </summary>
    public interface IOutbox
    {
        /// <summary>
        /// The current state of the inbox
        /// </summary>
        OutboxState OutboxState { get; }

        /// <summary>
        /// The server configuration
        /// </summary>
        IMailServerConfiguration OutboxServerConfiguration { get; set;}

        /// <summary>
        /// Sync sending of mails
        /// </summary>
        /// <returns>The unique id of the mail just sent</returns>
        string Send(MailSoap12TransportBinding mail);

        /// <summary>
        /// Sync sending of mails
        /// </summary>
        /// <param name="inReplyTo">The id of the mail to which this is a reply</param>
        /// <param name="mail">The mail message to send</param>
        /// <returns>The unique id of the mail just sent</returns>
        string Send(MailSoap12TransportBinding mail, string inReplyTo);


        /// <summary>
        /// Async sending of mails
        /// </summary>
        /// <param name="mail">The mail message to be sent</param>
        /// <param name="callback">Callback method</param>
        IAsyncResult BeginSending(MailSoap12TransportBinding mail, AsyncCallback callback);

        /// <summary>
        /// Async sending of mails
        /// </summary>
        /// <param name="mail">The mail message to be sent</param>
        /// <param name="inReplyTo">The id of the mail to which this is a reply</param>
        /// <param name="callback">Callback method</param>
        IAsyncResult BeginSending(MailSoap12TransportBinding mail, string inReplyTo, AsyncCallback callback);

        /// <summary>
        /// Stop async sending of mails
        /// </summary>
        /// <param name="asyncResult">async result</param>
        /// <returns>The unique id of the mail just sent</returns>
        string EndSending(IAsyncResult asyncResult);

        /// <summary>
        /// Event raised when an exception is thrown
        /// </summary>
        event MailboxExceptionThrown OnExceptionThrown;
    }
}