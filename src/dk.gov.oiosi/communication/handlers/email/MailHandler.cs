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
using System.Threading;

namespace dk.gov.oiosi.communication.handlers.email 
{
    /// <summary>
    /// A mail handler, that can send and receive mails
    /// </summary>
    public class MailHandler: IMailHandler {
        private IInbox _inbox;
        private IOutbox _outbox;
        private InboxFactory _inboxFactory;

        

        /// <summary>
        /// Event raised when an exception is thrown
        /// </summary>
        public event MailboxExceptionThrown OnExceptionThrown;

        /// <summary>
        /// Event raised whenever the inbox state changes
        /// </summary>
        public event OnInboxStateChangeDelegate OnInboxStateChange;

        /// <summary>
        /// Constructor
        /// </summary>
        public MailHandler(IMailHandlerConfiguration configuration) {
            IMailServerConfiguration sendingServerConfiguration = configuration.SendingServerConfiguration;
            IMailServerConfiguration recievingServerConfiguration = configuration.RecievingServerConfiguration;
            Type outBoxImplementationType = configuration.OutBoxImplementationType;
            Type inBoxImplementationType = configuration.InBoxImplementationType;

            _inboxFactory = InboxFactory.GetInstance();

            _inbox = _inboxFactory.GetInbox(recievingServerConfiguration, inBoxImplementationType, this);

            _outbox = (IOutbox)outBoxImplementationType.GetConstructor(new Type[0]).Invoke(null);
            _outbox.OutboxServerConfiguration = sendingServerConfiguration;


            if (_inbox != null) {
                _inbox.OnExceptionThrown += new MailboxExceptionThrown(CallbackExceptionThrown);
                _inbox.OnInboxStateChange += new OnInboxStateChangeDelegate(CallbackOnInboxStateChange);
            }
            if (_outbox != null) _outbox.OnExceptionThrown += new MailboxExceptionThrown(CallbackExceptionThrown);

            
        }

     


        /// <summary>
        /// Constructor that duplicates another MailHandler
        /// </summary>
        /// <param name="original">original mailhandler</param>
        public MailHandler(MailHandler original) {
            _inbox = original._inbox;
            _outbox = original._outbox;

            if (_inbox != null) {
                _inbox.OnExceptionThrown += new MailboxExceptionThrown(CallbackExceptionThrown);
                _inbox.OnInboxStateChange += new OnInboxStateChangeDelegate(CallbackOnInboxStateChange);
            }
            if (_outbox != null) _outbox.OnExceptionThrown += new MailboxExceptionThrown(CallbackExceptionThrown);

        }

        /// <summary>
        /// Called in case an exception has been thrown
        /// </summary>
        private void CallbackExceptionThrown(Exception e, object caller){
            if(OnExceptionThrown != null)
                OnExceptionThrown.Invoke(e, caller);
        }

        void CallbackOnInboxStateChange(InboxState newState) {
            if(OnInboxStateChange!=null)
                OnInboxStateChange(newState);
        }

        #region IInbox Members

        /// <summary>
        /// The current state of the inbox
        /// </summary>
        public InboxState InboxState {
            get { return _inbox.InboxState; }
        }

        /// <summary>
        /// The inbox server configuration
        /// </summary>
        public IMailServerConfiguration InboxServerConfiguration {
            get { return _inbox.InboxServerConfiguration; }
            set { _inbox.InboxServerConfiguration = value; }
        }

        /// <summary>
        /// Starts listening for incoming mails 
        /// </summary>
        public virtual void BeginReceiving() {
            _inbox.BeginReceiving();
        }

        /// <summary>
        /// Do we have any mails?
        /// </summary>
        public virtual MailSoap12TransportBinding Peek() {
            return _inbox.Peek();
        }

        /// <summary>
        /// De-queue the first mail
        /// </summary>
        /// <returns></returns>
        public virtual MailSoap12TransportBinding Dequeue() {
            return _inbox.Dequeue();
        }

        /// <summary>
        /// De-queues the first mail from the internal Inbox mail queue. Waits until timeout has ocurred.
        /// </summary>
        public virtual MailSoap12TransportBinding Dequeue(TimeSpan timeout) {
            return _inbox.Dequeue(timeout);
        }

        /// <summary>
        /// De-queues the first mail from the internal Inbox mail queue. Waits until timeout has ocurred.
        /// </summary>
        /// <param name="onAbortDequeueing">Set when the dequeueing is supposed to stop before the timeout occurs</param>
        /// <param name="timeout">Maximum allowed time for the dequeueing</param>
        public virtual MailSoap12TransportBinding Dequeue(TimeSpan timeout, AutoResetEvent onAbortDequeueing) {
            return _inbox.Dequeue(timeout, onAbortDequeueing);
        }

        /// <summary>
        /// Dequeues the first reply mail with the matching In-Reply-To 
        /// </summary>
        public virtual MailSoap12TransportBinding Dequeue(string inReplyToId, TimeSpan timeout) {
            return _inbox.Dequeue(inReplyToId, timeout);
        }

        /// <summary>
        /// Dequeues the first reply mail with the matching In-Reply-To 
        /// </summary>
        /// <param name="onAbortDequeueing">Set when the dequeueing is supposed to stop before the timeout occurs</param>
        /// <param name="inReplyToId">The In-Reply-To id of the mail we're requesting. Can be used to match an outgoing mail with it's reply.</param>
        /// <param name="timeout">Maximum allowed time for the dequeueing</param>
        public virtual MailSoap12TransportBinding Dequeue(string inReplyToId, TimeSpan timeout, AutoResetEvent onAbortDequeueing) {
            return _inbox.Dequeue(inReplyToId, timeout, onAbortDequeueing);
        }

        /// <summary>
        /// Closes down the mail handler for further listening
        /// </summary>
        public void Close() {
            _inboxFactory.FinishedUsingInbox(this);
            //if (_inbox != null)
            //    _inbox.Close();
        }

        #endregion

        #region IOutbox Members

        /// <summary>
        /// Gets the state of the outbox
        /// </summary>
        public OutboxState OutboxState {
            get { return _outbox.OutboxState; }
        }

        /// <summary>
        /// The server configuration
        /// </summary>
        public IMailServerConfiguration OutboxServerConfiguration {
            get { return _outbox.OutboxServerConfiguration; }
            set { _outbox.OutboxServerConfiguration = value; }
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="mail">the mail message to send</param>
        /// <returns>The unique id of the mail just sent</returns>
        public virtual string Send(MailSoap12TransportBinding mail) {
            return _outbox.Send(mail);
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="mail">the meil message to send</param>
        /// <param name="inReplyTo">The In-Reply-To id of the mail we're sending. Can be used to match an outgoing mail with it's reply.</param>
        /// <returns>The unique id of the mail just sent</returns>
        public virtual string Send(MailSoap12TransportBinding mail, string inReplyTo) {
            return _outbox.Send(mail, inReplyTo);
        }

        /// <summary>
        /// Starts sending a message
        /// </summary>
        /// <param name="mail">the mail message to send</param>
        /// <param name="callback">callback method</param>
        /// <returns>iasync result</returns>
        public virtual IAsyncResult BeginSending(MailSoap12TransportBinding mail, AsyncCallback callback) {
            return _outbox.BeginSending(mail, callback);
        }

        /// <summary>
        /// Starts sending a message
        /// </summary>
        /// <param name="mail">the mail message to send</param>
        /// <param name="inReplyTo">The id of the mail to which this is a reply</param>
        /// <param name="callback">callback method</param>
        /// <returns>iasync result</returns>
        public virtual IAsyncResult BeginSending(MailSoap12TransportBinding mail, string inReplyTo, AsyncCallback callback) {
            return _outbox.BeginSending(mail, inReplyTo, callback);
        }

        /// <summary>
        /// Stop async sending of mails
        /// </summary>
        /// <param name="asyncResult">async result</param>
        public virtual string EndSending(IAsyncResult asyncResult) {
            if (_outbox != null)
                return _outbox.EndSending(asyncResult);
            else
                return null;
        }

        #endregion






    }
}