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
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.handlers.email;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {

    /// <summary>
    /// Establishes a request context
    /// </summary>
    public class EmailRequestContext: RequestContext {
        /// <summary>
        /// A mail handler used for sending the reply
        /// </summary>
        protected IMailHandler pMailHandler;
        
        
        // The delegate used to call our synchronous reply method asynchronously
        private delegate void AsyncReply(Message message, TimeSpan timeout);
        private AsyncReply _asyncReply;

        /// <summary>
        /// gets the message that contains the request
        /// </summary>
        public override Message RequestMessage {
            get { return _requestMessage.Attachment.WcfMessage; }
        }
        private MailSoap12TransportBinding _requestMessage;


        private EmailBindingElement _bindingElement;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        /// <param name="mailHandler">mailhandler</param>
        /// <param name="bindingElement">The binding element used in the current stack</param>
        public EmailRequestContext(MailSoap12TransportBinding msg, IMailHandler mailHandler, EmailBindingElement bindingElement) {
            _requestMessage = msg;
            pMailHandler = mailHandler;
            _bindingElement = bindingElement;
        }

        /// <summary>
        /// Replies to a request message within a specified interval of time
        /// </summary>
        /// <param name="message">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        /// <param name="timeout">The System.Timespan that specifies the interval of time to wait for the reply
        /// to a request</param>
        public override void Reply(Message message, TimeSpan timeout) {
            Reply(message);    
        }

        /// <summary>
        /// Replies to a request message
        /// </summary>
        /// <param name="message">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        public override void Reply(Message message) {
            if (message == null) {
                WCFLogger.Write(System.Diagnostics.TraceEventType.Information, "RequestContext received a null message. Shutting down.");
                return;
            }

            WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "RequestContext starting to reply...");
            try {
                pMailHandler.Send(CreateMailMessage(message), _requestMessage.MessageId);
            }
            catch(Exception e){
                _bindingElement.RaiseAsyncException(this, e);
                throw;
            }
            WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "RequestContext finished replying.");
        }

        /// <summary>
        /// Begins an asynchronous operation to reply to the request associated with the current 
        /// context within a specified interval of time
        /// </summary>
        /// <param name="message">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        /// <param name="timeout">The System.Timespan that specifies the interval of time to wait for the reply
        /// to an available request</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// reply operation completion</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous reply operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous reply operation</returns>
        public override IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state) {
            _asyncReply = new AsyncReply(Reply);
            return _asyncReply.BeginInvoke(message,timeout, callback, state);
        }

        /// <summary>
        /// When overridden in a derived class, begins an asynchronous operation to reply
        /// to the request associated with the current context
        /// </summary>
        /// <param name="message">The incoming System.ServiceModel.Channels.Message that contains the request</param>
        /// <param name="callback">The System.AsyncCallback delegate that receives the notification of the asynchronous
        /// reply operation completion</param>
        /// <param name="state">An object, specified by the application, that contains state information
        /// associated with the asynchronous reply operation</param>
        /// <returns>The System.IAsyncResult that references the asynchronous reply operation</returns>
        public override IAsyncResult BeginReply(Message message, AsyncCallback callback, object state) {
            _asyncReply = new AsyncReply(Reply);
            return _asyncReply.BeginInvoke(message, TimeSpan.MaxValue, callback, state);
        }

        /// <summary>
        /// Completes an asynchronous operation to reply to a request message
        /// </summary>
        /// <param name="result">The System.IAsyncResult returned by a call to one of the Overload:System.ServiceModel.Channels.IRequestContext.BeginReply
        /// methods</param>
        public override void EndReply(IAsyncResult result) {
            if (_asyncReply != null) {
                _asyncReply.EndInvoke(result);
            }
        }

        private MailSoap12TransportBinding CreateMailMessage(Message message) {

            

            MailSoap12TransportBinding mail = new MailSoap12TransportBinding(message);
            // 2. Check mail headers of incoming request
            if (_requestMessage.ReplyTo != null && _requestMessage.ReplyTo != "")
                mail.To = _requestMessage.ReplyTo;
            else if (_requestMessage.From != null && _requestMessage.From != "")
                mail.To = _requestMessage.From;
            else
                throw new EmailReplyCouldNotBeSentException(new dk.gov.oiosi.communication.handlers.email.MailBindingFieldMissingException("From"));
           

            // Try to set the FROM header of the mail
            if (mail.From == null || mail.From == ""){
                mail.From = MailSoap12TransportBinding.TrimMailAddress(this.pMailHandler.InboxServerConfiguration.ReplyAddress);
            }

            message.Headers.To = new Uri("mailto:" + _requestMessage.From);
            WCFLogger.Write(System.Diagnostics.TraceEventType.Verbose, "RequestContext created the reply mail");

            return mail;
        }

        /// <summary>
        /// closes the operation that is replying to the request context associated with the 
        /// current context within a specified interval of time
        /// </summary>
        /// <param name="timeout">The System.Timespan that specifies the interval of time within 
        /// which the reply operation associated with the current context must close</param>
        public override void Close(TimeSpan timeout) {
            /* Do Nothing special */
        }

        /// <summary>
        /// closes the operation that is replying to the request context associated with the 
        /// current context
        /// </summary>
        public override void Close() {
            /* Do Nothing special */
        }

        /// <summary>
        /// aborts processing the request associated with the context
        /// </summary>
        public override void Abort() {
            /* Do nothing special */
        }
    }
}