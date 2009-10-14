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
using System.Diagnostics;
using System.Threading;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.communication.handlers.email {
    /// <summary>
    /// A mail inbox. Has functionality for receiving mails.
    /// </summary>
    public abstract class Inbox : Mailbox, IInbox {

        /// <summary>
        /// Email logon timeout default
        /// </summary>
        public static TimeSpan LogOnTimeout = new TimeSpan(0, 0, 15);

        /// <summary>
        /// Event raised when an inbox's state changes
        /// </summary>
        public event OnInboxStateChangeDelegate OnInboxStateChange;

        /// <summary>
        /// A list containing the mails gotten from the mailserver to our inbox
        /// </summary>
        protected List<MailSoap12TransportBinding> pMailQueue = new List<MailSoap12TransportBinding>();

        /// <summary>
        /// Keeps track of when mails were entered to the queue (and thus when they should be deleted)
        /// </summary>
        protected Dictionary<MailSoap12TransportBinding, DateTime> pMailEntryTimes = new Dictionary<MailSoap12TransportBinding, DateTime>();
        private TimeSpan _cacheingTime = new TimeSpan(0, 5, 0);


        // Delegate used for async calling of the sync method Receive
        private delegate void ReceiveDelegate();
        private ReceiveDelegate _asyncReceive;
        private IAsyncResult _asyncResult;

        /// <summary>
        /// Event for closing down 
        /// </summary>
        protected AutoResetEvent OnStopPolling = new AutoResetEvent(false);


        /// <summary>
        /// Event for starting up
        /// </summary>
        protected AutoResetEvent OnStartPolling = new AutoResetEvent(false);

        // Token used for locking access to the mailserver
        private object MailServerLockingToken = new object();

        /// <summary>
        /// Constructor
        /// </summary>
        public Inbox(IMailServerConfiguration mailServerConfiguration) {
            if (mailServerConfiguration == null) {
                throw new MailServerConfigurationMissingException();
            }
            _serverConfiguration = mailServerConfiguration;
            StartPollingQueue();
            _asyncReceive = new ReceiveDelegate(Receive);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Inbox() {
            StartPollingQueue();
            _asyncReceive = new ReceiveDelegate(Receive);
        }

        private void StartPollingQueue() {

            // Start the queue polling thread
            _dequeueingPollingThread = new Thread(new ThreadStart(DequeuePollingLoop));
            _dequeueingPollingThread.Start();

            logging.WCFLogger.Write(TraceEventType.Information, "Inbox started polling");

        }

        /// <summary>
        /// The server configuration
        /// </summary>
        public IMailServerConfiguration InboxServerConfiguration {
            get { return _serverConfiguration; }
            set { _serverConfiguration = value; }
        }
        private IMailServerConfiguration _serverConfiguration;

        /// <summary>
        /// Method that is to be implemented by a component that wraps a mail library
        /// 
        /// Gets the first message in the inbox, without erasing it from the mail server
        /// </summary>
        /// <returns>WCF Message created from the first mail in the inbox</returns>
        protected abstract MailSoap12TransportBinding PeekOnServer();

        /// <summary>
        /// Method that is to be implemented by a component that wraps a mail library
        /// 
        /// Gets the first message in the inbox, and erases it from the mail server
        /// </summary>
        /// <returns>WCF Message created from the first mail in the inbox</returns>
        protected abstract MailSoap12TransportBinding PollServer();


        #region IInbox Members

        /// <summary>
        /// The current state of the inbox (i.e. Listening/Stopped/Faulted)
        /// </summary>
        public InboxState InboxState {
            get { return _state; }
        }
        private InboxState _state = InboxState.Initialized;



        /// <summary>
        /// Tries to log on to the server
        /// </summary>
        private void TryLogOn() {

            // Continue to try until finished or timeout
            DateTime startTime = DateTime.Now;
            while (InboxState == InboxState.Listening || InboxState == InboxState.AttemptingLogOn) {
                try {
                    LogOn();
                    break;
                }
                catch (Exception e) {
                    if(InboxState == InboxState.Listening) SetState(InboxState.AttemptingLogOn);
                    if(DateTime.Now - startTime > LogOnTimeout)
                        throw e;

                }
                Thread.Sleep(Utilities.TimeSpanInMilliseconds(_serverConfiguration.ConnectionPolicy.PollingInterval));
            }
            if (InboxState != InboxState.Listening && InboxState != InboxState.Closing) SetState(InboxState.Listening);
            logging.WCFLogger.Write(TraceEventType.Verbose, "Logged on to '" + this._serverConfiguration.ServerAddress + "' as user '" + this._serverConfiguration.UserName + "'");
        }

        /// <summary>
        /// Begins listening for incoming mails
        /// </summary>
        public void BeginReceiving() {


            // Check to see that there is a mailserverconfig
            if (InboxServerConfiguration == null) {
                throw new MailServerConfigurationMissingException();
            }
            if (_state == InboxState.Faulted) {
                throw new MailboxFaultedException();
            }

            logging.WCFLogger.Write(TraceEventType.Verbose, "Beginning to receive incoming mails on '" + this._serverConfiguration.ServerAddress + "' as user '" + this._serverConfiguration.UserName + "'");
            SetState(InboxState.Starting);
            try {
                _asyncResult = _asyncReceive.BeginInvoke(null, null);
                OnStartPolling.WaitOne();
            }
            catch {
                SetState(InboxState.Faulted);
            }
   
        }

        /// <summary>
        /// Synchronous method that receives mails
        /// </summary>
        private void Receive() {
            // Set the name of the current thread for debugging purposes
            Thread.CurrentThread.Name = "Inbox polling thread";
            try {
                if (InboxServerConfiguration.ConnectionPolicy != null) {
                    switch (InboxServerConfiguration.ConnectionPolicy.PollingPattern) {
                        case MailServerPollingPattern.LogOn_KeepPolling_LogOff:
                            LogOn_KeepPolling_LogOff();
                            break;
                        case MailServerPollingPattern.LogOn_PollOnce_LogOff:
                            LogOn_PollOnce_LogOff();
                            break;
                        case MailServerPollingPattern.Poll:
                            Poll();
                            break;
                    }
                }
                // Default in case no policy was set
                else {
                    LogOn_PollOnce_LogOff();
                }
            }
            catch (Exception e) {
                logging.WCFLogger.Write(TraceEventType.Critical, "An exception was thrown\n\n" + e);
                OnStartPolling.Set();
                RaiseExceptionEvent(e);
             
            }
        }

        /// <summary>
        /// Logs on, polls until EndReceive() is called, and then logs off
        /// </summary>
        private void LogOn_KeepPolling_LogOff() {
            try {
                logging.WCFLogger.Write(TraceEventType.Verbose, "Using the 'Log on - Keep Polling - Log off' pattern");
                SetState(InboxState.AttemptingLogOn);
                TryLogOn();
                OnStartPolling.Set();

                // Keep polling until someone says stop
                while (_state == InboxState.Listening) {
                    MailSoap12TransportBinding msg = PollServer();
                    // If a message was fetched, lock the queue and add it
                    if (msg != null) {
                        lock (pMailQueue) {
                            pMailQueue.Add(msg);
                            pMailEntryTimes.Add(msg, DateTime.Now);
                            logging.WCFLogger.Write(TraceEventType.Verbose, "Incoming mail received from " + msg.From + " in reply to '" + msg.InReplyTo + "'");
                        }
                        continue;
                    }
                    OnStopPolling.WaitOne(Utilities.TimeSpanInMilliseconds(InboxServerConfiguration.ConnectionPolicy.PollingInterval), false);
                }
                LogOff();
            }
            catch  {
                OnStartPolling.Set();
                SetState(InboxState.Faulted);
                throw;
            }
        }


        /// <summary>
        /// Logs on, polls once, and then logs off
        /// </summary>
        private void LogOn_PollOnce_LogOff() {
            try {
                SetState(InboxState.AttemptingLogOn);
                OnStartPolling.Set();
                do {
                    MailSoap12TransportBinding msg;
                    lock (MailServerLockingToken) {
                        TryLogOn();
                        msg = PollServer();
                        if (msg != null) {
                            lock (pMailQueue) {
                                pMailQueue.Add(msg);
                                pMailEntryTimes.Add(msg, DateTime.Now);
                                logging.WCFLogger.Write(TraceEventType.Verbose, "Incoming mail received from " + msg.From + " in reply to '" + msg.InReplyTo + "'");
                            }
                        }
                        LogOff();
                    }
                    int waitTime = Utilities.TimeSpanInMilliseconds(InboxServerConfiguration.ConnectionPolicy.PollingInterval);
                    logging.WCFLogger.Write(TraceEventType.Verbose, "Waiting " + waitTime + "ms before pulling server again.");
                    OnStopPolling.WaitOne(waitTime, false);
                }
                while (_state == InboxState.Listening);
            }
            catch (Exception e) {
                OnStartPolling.Set();
                SetState(InboxState.Faulted);
                throw e;
            }

        }

        /// <summary>
        /// Just polls the server once (without logging on or off)
        /// </summary>
        private void Poll() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stop listening for incoming mails.
        /// Must only be called once.
        /// </summary>
        public void Close() {
            logging.WCFLogger.Write(TraceEventType.Information, "Inbox beginning to close down...");

            SetState(InboxState.Closing);
            OnStopPolling.Set();
            try {
                _asyncReceive.EndInvoke(_asyncResult);
            }
            catch {
                throw new MailboxClosedException();
            }
            // If the inbox has not been faulted during shutdown
            if(_state != InboxState.Faulted) SetState(InboxState.Closed);
            logging.WCFLogger.Write(TraceEventType.Information, "Inbox finished closing down");
        }


        /// <summary>
        /// Peeks to see if there has come any mails, but does not remove anything from the server.
        /// </summary>
        /// <returns></returns>
        public MailSoap12TransportBinding Peek() {
            lock (pMailQueue) {
                if (pMailQueue.Count < 1) {
                    return null;
                }
                else {
                    return pMailQueue[0];
                }
            }
        }

        /// <summary>
        /// De-queues the first mail from the internal Inbox mail queue
        /// </summary>
        /// <returns>The first mail in queue</returns>
        public MailSoap12TransportBinding Dequeue() {
            lock (pMailQueue) {
                if (pMailQueue.Count < 1) {
                    return null;
                }
                else {
                    MailSoap12TransportBinding mail = pMailQueue[0];
                    pMailQueue.Remove(mail);
                    pMailEntryTimes.Remove(mail);
                    return mail;
                }
            }
        }
        #endregion

        #region IMailQueue Members

        private class InboxRequest {
            public AutoResetEvent onFinished;
            public MailSoap12TransportBinding msg;
        }

        private List<InboxRequest> _requests = new List<InboxRequest>();
        private Thread _dequeueingPollingThread;


        /// <summary>
        /// De-queues the first mail
        /// </summary>
        /// <param name="timeout">Maximum time to wait for a mail to come</param>
        public MailSoap12TransportBinding Dequeue(TimeSpan timeout) {
            return Dequeue(timeout, null);
        }

        /// <summary>
        /// De-queues the first mail
        /// </summary>
        /// <param name="timeout">Maximum time to wait for a mail to come</param>
        /// <param name="onAbortDequeueing">AutoResetEvent that if set will abort the de-queueing</param>
        /// <returns></returns>
        public MailSoap12TransportBinding Dequeue(TimeSpan timeout, AutoResetEvent onAbortDequeueing) {

            // Is the mailbox closed?
            if (_state == InboxState.Closed ||
                _state == InboxState.Closing ||
                _state == InboxState.Faulted)
                return null;

            InboxRequest request = new InboxRequest();

            // Make sure the request can be aborted from the outside as well
            if (onAbortDequeueing == null)
                onAbortDequeueing = new AutoResetEvent(false);
            request.onFinished = onAbortDequeueing;

            // Add the request to the list of requests currently pending
            lock (_requests) {
                _requests.Add(request);
            }

            // Wait until request is finished or until a timeout occurs
            if (!request.onFinished.WaitOne(Utilities.TimeSpanInMilliseconds(timeout), false)) {
                lock (_requests) {
                    _requests.Remove(request);
                }
                //RaiseExceptionEvent(new TimeoutException());
                throw new TimeoutException();
            }

            // Remove the request from the list
            lock (_requests) {
                _requests.Remove(request);
            }

            // Return the message
            return request.msg;
        }


        private void DequeuePollingLoop() {
            // Set the name of the thread for debugging purposes
            Thread.CurrentThread.Name = "Inbox.Dequeue polling thread";
            try {
                while ((_state != InboxState.Faulted && _state != InboxState.Closed)) {
                    // Do we have any requestors?
                    if (_requests.Count > 0) {
                        MailSoap12TransportBinding msg = Dequeue();
                        // Was there a message?
                        if (msg != null) {
                            InboxRequest requestor = _requests[0];
                            requestor.msg = msg;
                            requestor.onFinished.Set();
                            lock (_requests) {
                                _requests.Remove(requestor);
                            }
                        }
                    }
                    Thread.Sleep(200);
                }
            }
            catch(Exception e) {
                SetState(InboxState.Faulted);
                RaiseExceptionEvent(e);
            }
            finally {
                // Release all requests for a mail, now that the polling is shut down
                ReleaseAllRequestors();
            }
        }

        private void ReleaseAllRequestors() {
            lock (_requests) {
                foreach (InboxRequest req in _requests) {
                    req.onFinished.Set();
                }
            }
        }

        /// <summary>
        /// De-queues the first mail
        /// </summary>
        /// <param name="inReplyToId">The In-Reply-To id of the mail requested. Can be used to find the reply to a specific mail sent.</param>
        /// <param name="timeout">Maximum time to wait for a mail to come</param>
        public MailSoap12TransportBinding Dequeue(string inReplyToId, TimeSpan timeout) {
            return Dequeue(inReplyToId, timeout, new AutoResetEvent(false));
        }

        /// <summary>
        /// De-queues the first mail
        /// </summary>
        /// <param name="inReplyToId">The In-Reply-To id of the mail requested. Can be used to find the reply to a specific mail sent.</param>
        /// <param name="timeout">Maximum time to wait for a mail to come</param>
        /// <param name="onAbortDequeueing">AutoResetEvent that if set will abort the de-queueing</param>
        public MailSoap12TransportBinding Dequeue(string inReplyToId, TimeSpan timeout, AutoResetEvent onAbortDequeueing) {
            DateTime startTime = DateTime.Now;

            // Poll to see if our mail has arrived
            while (DateTime.Now - startTime < timeout) {

                // If the mailbox is faulted, throw
                if (_state == InboxState.Closed || _state == InboxState.Faulted )
                    throw new InboxInvalidStateException(_state);

                lock (pMailQueue) {
                    for (int i = 0; i < pMailQueue.Count; i++) {
                        if (pMailQueue[i].InReplyTo == inReplyToId) {
                            MailSoap12TransportBinding mail = pMailQueue[i];
                            pMailQueue.Remove(mail);
                            pMailEntryTimes.Remove(mail);
                            return mail;
                        }
                        else { 
                            //Check if mail is expired
                            if ((DateTime.Now - pMailEntryTimes[pMailQueue[i]]) >= _cacheingTime) {
                                pMailEntryTimes.Remove(pMailQueue[i]);
                                pMailQueue.Remove(pMailQueue[i]);
                            }
                        }
                    }
                }

                // Sleep. If someone signals to abort, return null
                if (onAbortDequeueing.WaitOne(200, false)) {
                    return null;
                }
               
            }

            // We timed out
            //RaiseExceptionEvent(new TimeoutException());
            throw new TimeoutException();
        }

        #endregion

        private void SetState(InboxState state) {
            switch (_state) {
                case InboxState.Initialized:
                    FromInitialized(state);
                    break;
                case InboxState.Starting:
                    FromStarting(state);
                    break;
                case InboxState.AttemptingLogOn:
                    FromAttemptingLogon(state);
                    break;
                case InboxState.Listening:
                    FromListening(state);
                    break;
                case InboxState.Closing:
                    FromStopping(state);
                    break;
                case InboxState.Closed:
                    FromClosed(state);
                    break;
                case InboxState.Faulted:
                    FromFaulted(state);
                    break;
                default:
                    throw new InboxUnhandledStateException(_state);
            }
            _state = state;
            if (OnInboxStateChange != null) OnInboxStateChange(_state);
            logging.WCFLogger.Write(TraceEventType.Information, "Inbox changed state to " + state);
        }

        private void FromInitialized(InboxState state) {
            switch (state) {
                case InboxState.Starting:
                case InboxState.Faulted:
                    break;
                default:
                    throw new InboxIllegalStateTransactionException(_state, state);
            }
        }

        private void FromStarting(InboxState state) {
            switch (state) {
                case InboxState.AttemptingLogOn:
                case InboxState.Closing:
                case InboxState.Faulted:
                    break;
                default:
                    throw new InboxIllegalStateTransactionException(_state, state);
            }
        }

        private void FromAttemptingLogon(InboxState state) {
            switch (state) {
                case InboxState.Listening:
                case InboxState.Closing:
                case InboxState.Faulted:
                    break;
                default:
                    throw new InboxIllegalStateTransactionException(_state, state);
            }
        }

        private void FromListening(InboxState state) {
            switch (state) {
                case InboxState.AttemptingLogOn:
                case InboxState.Closing:
                case InboxState.Faulted:
                    break;
                default:
                    throw new InboxIllegalStateTransactionException(_state, state);
            }
        }

        private void FromStopping(InboxState state) {
            switch (state) {
                case InboxState.Closed:
                case InboxState.Faulted:
                    break;
                default:
                    throw new InboxIllegalStateTransactionException(_state, state);
            }
        }

        private void FromClosed(InboxState state) {

            switch (state) {
                case InboxState.Starting:
                    break;
                default:
                    throw new InboxIllegalStateTransactionException(_state, state);
            }
        }

        private void FromFaulted(InboxState state) {
            throw new InboxIllegalStateTransactionException(_state, state);
        }
    }
}
