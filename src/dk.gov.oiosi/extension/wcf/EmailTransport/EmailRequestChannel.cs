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
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using dk.gov.oiosi.communication.handlers.email;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {

    /// <summary>
    /// Sets up a channel to receive requests between messaging endpoints
    /// </summary>
    public class EmailRequestChannel : CommonChannelBase, IRequestChannel {
        // The outbox used for sending
        private IMailHandler _mailHandler;

        // Object used for locking the channel while requesting
        private static object _stateLock = new object();

        // The delegate used to call our synchronous sending method asynchronously
        private delegate Message AsyncRequest(Message message, TimeSpan timeout);
        private Dictionary<object, AsyncRequest> _asyncRequests = new Dictionary<object,AsyncRequest>() ;
        private Dictionary<object, IAsyncResult> _asyncResults = new Dictionary<object, IAsyncResult>();


        private List<AutoResetEvent> _threadsDequeueing = new List<AutoResetEvent>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mailHandler">outbox used for sending</param>
        /// <param name="remoteAddress">remote address to which the request channel sends messages</param>
        /// <param name="channelManager">channel handler</param>
        public EmailRequestChannel(IMailHandler mailHandler, EndpointAddress remoteAddress, ChannelManagerBase channelManager)
            : base(channelManager) {
            _mailHandler = mailHandler;
            _remoteAddress = remoteAddress;
        }

        #region IRequestChannel Members

        /// <summary>
        /// Begins an asynchronous operation to transmit a request message to the reply-side 
        /// of a request-reply message exchange
        /// </summary>
        /// <param name="message">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <param name="timeout">The System.TimeSpan that specifies the interval of time within which a response
        /// must be received</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the completion 
        /// of the asynchronous operation transmitting a request message</param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state) {

            AsyncRequest req = new AsyncRequest(Request);
            IAsyncResult result = req.BeginInvoke(message, timeout, callback, state);

            lock (_asyncRequests) {
                _asyncRequests.Add(result.AsyncWaitHandle, req);
                _asyncResults.Add(result.AsyncWaitHandle, result);
            }
            return result;
            
        }

        /// <summary>
        /// Begins an asynchronous operation to transmit a request message to the reply-side 
        /// of a request-reply message exchange
        /// </summary>
        /// <param name="message">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the completion 
        /// of the asynchronous operation transmitting a request message</param>
        /// <param name="state">An object, specified by the application, that contains state information 
        /// associated with the asynchronous operation transmitting a request message</param>
        /// <returns>The System.IAsyncResult that references the asynchronous message transmission</returns>
        public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state) {

            AsyncRequest req = new AsyncRequest(Request);
            IAsyncResult result = req.BeginInvoke(message, TimeSpan.MaxValue, callback, state);

            lock (_asyncRequests) {
                _asyncRequests.Add(result.AsyncWaitHandle, req);
                _asyncResults.Add(result.AsyncWaitHandle, result);
            }
            return result;

        }

        /// <summary>
        /// Completes an asynchronous operation to return a message-based response to
        /// a transmitted request
        /// </summary>
        /// <param name="result">The System.IAsyncResult returned by a call to the System.ServiceModel.Channels.IInputChannel.BeginRequest()
        /// method</param>
        /// <returns>The System.ServiceModel.Channels.Message received in response to the request</returns>
        public Message EndRequest(IAsyncResult result) {
            lock (_asyncRequests) {
                // Has this async request already been finished?
                if (!_asyncRequests.ContainsKey(result.AsyncWaitHandle)) return null;

                // If not, finish it
                Message msg;
                try {
                    msg = _asyncRequests[result.AsyncWaitHandle].EndInvoke(result);
                }
                finally {
                    _asyncRequests.Remove(result.AsyncWaitHandle);
                    _asyncResults.Remove(result.AsyncWaitHandle);
                }
                return msg;
            }
        }

        /// <summary>
        /// Property for the remoteaddress
        /// </summary>
        public System.ServiceModel.EndpointAddress RemoteAddress {
            get { return _remoteAddress; }
        }
        private EndpointAddress _remoteAddress;

        /// <summary>
        /// The request
        /// </summary>
        /// <param name="message">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <param name="timeout">The System.TimeSpan that specifies the interval of time within which a response
        /// must be received</param>
        /// <returns></returns>
        public Message Request(Message message, TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Start, "Mail request channel starting request");
            Message reply = null;

            ThrowIfDisposedOrNotOpen();

            // Send
            System.Diagnostics.Debug.Write("Mail requestChannel send request.");
            string id = _mailHandler.Send(CreateMailMessage(message));
            

            // If the message sent was a fault, don't expect a reply
            if (message.IsFault) {
                message.Close();
                WCFLogger.Write(TraceEventType.Stop, "Mail request channel finishing sending a fault");
                return null;
            }
            else {
                try {
                    // Wait for the reply
                    AutoResetEvent onAbort = new AutoResetEvent(false);
                    _threadsDequeueing.Add(onAbort);
                    MailSoap12TransportBinding mail = _mailHandler.Dequeue(id, timeout, onAbort);
                    
                    
                    // If dequeue returned null, someone stopped it, throw aborted exception
                    if (mail == null) return null;

                    // Get the WCF Message
                    reply = mail.Attachment.WcfMessage;

                    WCFLogger.Write(TraceEventType.Verbose, "Mail request channel got a reply");
                }
                catch(CommunicationObjectAbortedException){
                    message.Close();
                    throw;
                }
                catch (TimeoutException) {
                    message.Close();
                    throw;
                }
                catch (Exception e) {
                    this.Fault();
                    message.Close();
                    throw new EmailResponseNotGottenException(e);
                }

                WCFLogger.Write(TraceEventType.Stop, "Mail request channel finishing requesting");
                return reply;
            }
        }

        private MailSoap12TransportBinding CreateMailMessage(Message message) {

            // Attach a To header, so that the SMTP/MIME SOAP 1.2 protocol can be complied with
            message.Headers.To = this.RemoteAddress.Uri;


            MailSoap12TransportBinding mail = new MailSoap12TransportBinding(message);

            if (_mailHandler.OutboxServerConfiguration.ReplyAddress == null || _mailHandler.OutboxServerConfiguration.ReplyAddress == "")
                throw new dk.gov.oiosi.communication.handlers.email.MailBindingFieldMissingException("replyAddress in the configuration file");
            else {
                mail.From = MailSoap12TransportBinding.TrimMailAddress(_mailHandler.OutboxServerConfiguration.ReplyAddress);
            }

            if (mail.To == null || mail.To == "") {
                mail.To = MailSoap12TransportBinding.TrimMailAddress(this.RemoteAddress.Uri);
            }

            

            return mail;

        }

        /// <summary>
        /// Sends a message-based request and returns the correlated message-based response
        /// </summary>
        /// <param name="message">The request System.ServiceModel.Channels.Message to be transmitted</param>
        /// <returns>The System.ServiceModel.Channels.Message received in response to the request</returns>
        public Message Request(Message message) {
            return Request(message, TimeSpan.MaxValue);
        }

        /// <summary>
        /// Gets the transport address to which the request is send
        /// </summary>
        public Uri Via {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        /// <summary>
        /// OnAbort event
        /// </summary>
        protected override void OnAbort() {
             WCFLogger.Write(TraceEventType.Start, "Mail request channel starting to abort...");
            lock (_stateLock) {
                base.OnAbort();

                AbortAllDequeueing();
                WCFLogger.Write(TraceEventType.Verbose, "Mail request channel aborted all dequeueing.");

                List<Exception> exceptions = new List<Exception>();
                lock (_asyncRequests) {
                    // Wait for all threads to die
                    foreach (KeyValuePair<object, AsyncRequest> kvp in _asyncRequests) {
                        IAsyncResult result = _asyncResults[kvp.Key];
                        try {
                            kvp.Value.EndInvoke(result);
                        }
                        catch (Exception e) {
                            exceptions.Add(e);
                        }

                        WCFLogger.Write(TraceEventType.Verbose, "Mail request channel aborted a thread.");
                    }
                    _asyncRequests.Clear();
                    _asyncResults.Clear();
                }
                // Stop listening to the mailbox
                if (_mailHandler.InboxState == InboxState.Listening)
                    _mailHandler.Close();

                WCFLogger.Write(TraceEventType.Verbose, "Mail request channel aborted mail handler");
                if (exceptions.Count > 0)
                    throw exceptions[0];
            }
            WCFLogger.Write(TraceEventType.Stop, "Mail request channel finished aborting.");
        }

        /// <summary>
        /// OnClose event
        /// </summary>
        /// <param name="timeout"></param>
        protected override void OnClose(TimeSpan timeout) {
            WCFLogger.Write(TraceEventType.Start, "Mail request channel starting to close...");
            lock (_stateLock) {
                base.OnClose(timeout);

                AbortAllDequeueing();
                WCFLogger.Write(TraceEventType.Verbose, "Mail request channel closed all dequeueing.");

                List<Exception> exceptions = new List<Exception>();
                lock (_asyncRequests) {
                    // Wait for all threads to die
                    foreach (KeyValuePair<object, AsyncRequest> kvp in _asyncRequests) {
                        IAsyncResult result = _asyncResults[kvp.Key];
                        try {
                            kvp.Value.EndInvoke(result);
                        }
                        catch (Exception e) {
                            exceptions.Add(e);
                        }
                        WCFLogger.Write(TraceEventType.Verbose, "Mail request channel closed a thread.");
                    }
                    _asyncRequests.Clear();
                    _asyncResults.Clear();
                }

                // Stop listening to the mailbox
                if (_mailHandler.InboxState == InboxState.Listening)
                    _mailHandler.Close();

                WCFLogger.Write(TraceEventType.Verbose, "Mail request channel closed mail handler");
                if (exceptions.Count > 0)
                    throw exceptions[0];
            }
            WCFLogger.Write(TraceEventType.Stop, "Mail request channel finished closing.");
        }


        private void AbortAllDequeueing() {
            foreach (AutoResetEvent arv in _threadsDequeueing)
                arv.Set();

            _threadsDequeueing.Clear();
        }
        #endregion
    }
}