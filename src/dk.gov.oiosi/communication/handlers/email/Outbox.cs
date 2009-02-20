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

namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// A mail inbox. Has functionality for sending mails. 
    /// </summary>
    public abstract class Outbox: Mailbox, IOutbox
    {
        private object MailServerLockingToken = new object();

        // Delegate used for calling the sending method asynchronously
        private delegate string AsyncSend(MailSoap12TransportBinding mail, string inReplyTo);
        private AsyncSend _asyncSend;

        /// <summary>
        /// Constructor
        /// </summary>
        public Outbox(MailServerConfiguration mailServerConfiguration)
        {
            if (mailServerConfiguration == null) {
                throw new MailServerConfigurationMissingException();
            }

            _serverConfiguration = mailServerConfiguration;
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Outbox() { }

        #region IOutbox Members

        /// <summary>
        /// The current state of the outbox (i.e. Sending/Stopped/Faulted)
        /// </summary>
        public OutboxState OutboxState
        {
            get { return _state; }
        }
        private OutboxState _state = OutboxState.Idle;


        /// <summary>
        /// The server configuration
        /// </summary>
        public IMailServerConfiguration OutboxServerConfiguration {
            get { return _serverConfiguration; }
            set { _serverConfiguration = value; }
        }
        private IMailServerConfiguration _serverConfiguration;


        /// <summary>
        /// Sends a WCF message over mail
        /// </summary>
        /// <param name="mail">The mail Message object</param>
        /// <returns>The id of the mail just sent</returns>
        public string Send(MailSoap12TransportBinding mail) {
            return Send(mail, null);
        }

        /// <summary>
        /// Sends a mail synchronously
        /// </summary>
        /// <param name="mail">Mail Message to send</param>
        /// <param name="inReplyTo">The unique id of the mail to which this is a reply</param>
        /// <returns>The unique id of the mail just sent</returns>
        public string Send(MailSoap12TransportBinding mail, string inReplyTo)
        {
            if (_serverConfiguration == null) {
                throw new MailServerConfigurationMissingException();
            }

            _state = OutboxState.Sending;

            // If we have a In-Reply-To id, set it
            if (inReplyTo != null) mail.InReplyTo = inReplyTo;

            lock (MailServerLockingToken) {
                LogOn();
                SendViaServer(mail);
                LogOff();
            }

            _state = OutboxState.Idle;

            return mail.MessageId;
        }

        /// <summary>
        /// Begins sending a mail asynchronously
        /// </summary>
        /// <param name="mail">Mail Message to send</param>
        /// <param name="callback">Callback method</param>
        public IAsyncResult BeginSending(MailSoap12TransportBinding mail, AsyncCallback callback)
        {
            _asyncSend = new AsyncSend(Send);
            return _asyncSend.BeginInvoke(mail, null, callback, _asyncSend);
        }

        /// <summary>
        /// Begins sending a mail asynchronously
        /// </summary>
        /// <param name="mail">Mail Message to send</param>
        /// <param name="callback">Callback method</param>
        /// <param name="inReplyTo">The id the of the mail which we are responding to</param>
        public IAsyncResult BeginSending(MailSoap12TransportBinding mail, string inReplyTo, AsyncCallback callback) {
            _asyncSend = new AsyncSend(Send);
            return _asyncSend.BeginInvoke(mail, inReplyTo, callback, _asyncSend);
        }

        /// <summary>
        /// Ends asynchronous sending
        /// </summary>
        /// <returns>The unique id of the mail just sent</returns>
        public string EndSending(IAsyncResult asyncResult)
        {
            return ((AsyncSend)asyncResult.AsyncState).EndInvoke(asyncResult);
        }

        #endregion

        /// <summary>
        /// Method that is to be implemented by a component that wraps a mail library
        /// 
        /// Sends a mail via the mail server.
        /// </summary>
        /// <param name="mail">The message to be sent</param>
        protected abstract void SendViaServer(MailSoap12TransportBinding mail);

    }
}