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

using dk.gov.oiosi.exception;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.handlers.email;

namespace dk.gov.oiosi.extension.wcf.EmailTransport 
{
    /// <summary>
    /// The RASP mail binding
    /// </summary>
    public class EmailBindingElement : TransportBindingElement {
        private string _sendingServerAddress;
        private string _sendingUserName;
        private string _sendingPassword;
        private string _receivingServerAddress;
        private string _receivingPassword;
        private TimeSpan _pollingInterval;
        private string _receivingUserName;
        private string _imapFolder;
        private string _replyAddress;
        private MailAuthenticationMode _receivingAuthenticationMode;
        private MailAuthenticationMode _sendingAuthenticationMode;
        private string _outboxImplementation;
        private string _inboxImplementation;
        private TcpPort _receivingPort;
        private TcpPort _sendingPort;


        /// <summary>
        /// The address of the sending server
        /// </summary>
        public string SendingServerAddress {
            get { return _sendingServerAddress; }
            set { _sendingServerAddress = value; }
        }

        /// <summary>
        /// The username to the sending server
        /// </summary>
        public string SendingUserName {
            get { return _sendingUserName; }
            set { _sendingUserName = value; }
        }

        /// <summary>
        /// The password to the sending server
        /// </summary>
        public string SendingPassword {
            get { return _sendingPassword; }
            set { _sendingPassword = value; }
        }

        /// <summary>
        /// The address of the receiving server
        /// </summary>
        public string ReceivingServerAddress {
            get { return _receivingServerAddress; }
            set { _receivingServerAddress = value; }
        }

        /// <summary>
        /// The username to the receiving server
        /// </summary>
        public string ReceivingUserName {
            get { return _receivingUserName; }
            set { _receivingUserName = value; }
        }

        /// <summary>
        /// The password to the receiving server
        /// </summary>
        public string ReceivingPassword {
            get { return _receivingPassword; }
            set { _receivingPassword = value; }
        }

        /// <summary>
        /// The polling interval
        /// </summary>
        public TimeSpan PollingInterval {
            get { return _pollingInterval; }
            set { _pollingInterval = value; }
        }

        /// <summary>
        /// The imap folder
        /// </summary>
        public string ImapFolder {
            get { return _imapFolder; }
            set { _imapFolder = value; }
        }

        /// <summary>
        /// Reply mail address, e.g. "test@domain.com"
        /// </summary>
        public string ReplyAddress {
            get { return _replyAddress; }
            set { _replyAddress = value; }
        }

        /// <summary>
        /// Authentication mode - set to "SSL" if you run SMTP over SSL.
        /// </summary>
        public MailAuthenticationMode SendingAuthenticationMode {
            get { return _sendingAuthenticationMode; }
            set { _sendingAuthenticationMode = value; }
        }

        /// <summary>
        /// Authentication mode - set to "SSL" if you run POP over SSL.
        /// </summary>
        public MailAuthenticationMode ReceivingAuthenticationMode {
            get { return _receivingAuthenticationMode; }
            set { _receivingAuthenticationMode = value; }
        }

        /// <summary>
        /// The port for receiving
        /// </summary>
        public TcpPort ReceivingPort {
            get { return _receivingPort; }
            set { _receivingPort = value; }
        }

        /// <summary>
        /// The port for sending
        /// </summary>
        public TcpPort SendingPort {
            get { return _sendingPort; }
            set { _sendingPort = value; }
        }


        /// <summary>
        /// Outbox implementation
        /// </summary>
        public string OutboxImplementation {
            get { return _outboxImplementation; }
            set { _outboxImplementation = value; }
        }

        /// <summary>
        /// Inbox implementation
        /// </summary>
        public string InboxImplementation {
            get { return _inboxImplementation; }
            set { _inboxImplementation = value; }
        }


        /// <summary>
        /// Raised when an asynchronous exception is thrown
        /// </summary>
        public event AsyncExceptionThrownHandler ExceptionThrown;

        /// <summary>
        /// Raised when the inbox changes states
        /// </summary>
        public event OnInboxStateChangeDelegate OnInboxStateChange;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configurationElement">bindingextension</param>
        public EmailBindingElement(IEmailBindingElementConfiguration configurationElement) {
            CopyConfigurationElement(configurationElement);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmailBindingElement() { }
        

        /// <summary>
        /// Raises the event
        /// </summary>
        /// <param name="caller">The caller of the event</param>
        /// <param name="e">The exception that caused the event</param>
        internal void RaiseAsyncException(object caller, Exception e) {
            if (ExceptionThrown != null)
                ExceptionThrown.Invoke(caller, e);
        }

        /// <summary>
        /// Raises the event
        /// </summary>
        /// <param name="newState">The state we're changing to</param>
        internal void InboxStateChange(InboxState newState) {
            if (OnInboxStateChange != null)
                OnInboxStateChange(newState);
        }

        /// <summary>
        /// Copies all settings into the binding
        /// </summary>
        private void CopyConfigurationElement(IEmailBindingElementConfiguration configurationElement) {
            _receivingAuthenticationMode = configurationElement.ReceivingAuthenticationMode;
            _sendingAuthenticationMode = configurationElement.SendingAuthenticationMode;
            _imapFolder = configurationElement.ImapFolder;
            _pollingInterval = configurationElement.PollingInterval;
            _receivingPassword = configurationElement.ReceivingPassword;
            _receivingServerAddress = configurationElement.ReceivingServerAddress;
            _receivingUserName = configurationElement.ReceivingUserName;
            _replyAddress = configurationElement.ReplyAddress;
            _sendingPassword = configurationElement.SendingPassword;
            _sendingServerAddress = configurationElement.SendingServerAddress;
            _sendingUserName = configurationElement.SendingUserName;
            _outboxImplementation = configurationElement.OutboxImplementation;
            _inboxImplementation = configurationElement.InboxImplementation;
            _receivingPort = (TcpPort)configurationElement.ReceivingPort;
            _sendingPort = (TcpPort)configurationElement.SendingPort;
        }


        /// <summary>
        /// Copies all settings into the binding
        /// </summary>
        public void CopyElement(EmailBindingElement original) {
            _sendingAuthenticationMode = original.SendingAuthenticationMode;
            _receivingAuthenticationMode = original.ReceivingAuthenticationMode;
            _imapFolder = original.ImapFolder;
            _pollingInterval = original.PollingInterval;
            _receivingPassword = original.ReceivingPassword;
            _receivingServerAddress = original.ReceivingServerAddress;
            _receivingUserName = original.ReceivingUserName;
            _replyAddress = original.ReplyAddress;
            _sendingPassword = original.SendingPassword;
            _sendingServerAddress = original.SendingServerAddress;
            _sendingUserName = original.SendingUserName;
            _outboxImplementation = original.OutboxImplementation;
            _inboxImplementation = original.InboxImplementation;
            _receivingPort = original._receivingPort;
            _sendingPort = original._sendingPort;
        }

        /// <summary>
        /// overriding Scheme to return smtp
        /// </summary>
        public override string Scheme {
            get { return "mailto"; }
        }

        /// <summary>
        /// Overriding Clone to return rasp binding
        /// </summary>
        /// <returns>the binding element</returns>
        public override BindingElement Clone() {
            EmailBindingElement b = new EmailBindingElement();
            b.CopyElement(this);
            b.ExceptionThrown = this.ExceptionThrown;
            b.OnInboxStateChange = this.OnInboxStateChange;
            return b;
        }

        /// <summary>
        /// Indicates the type of factory
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool CanBuildChannelFactory<TChannel>(BindingContext context) {
            // We can only build factories that produce RaspMailRequestChannel
            return (typeof(TChannel) == typeof(IRequestChannel)); 
        }

        /// <summary>
        /// Builds a channel factory
        /// </summary>
        /// <typeparam name="TChannel">type of channel</typeparam>
        /// <param name="context">the context</param>
        /// <returns>IChannelFactory</returns>
        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }
            if (typeof(TChannel) != typeof(IRequestChannel)) {
                throw new EmailTransportChannelCouldNotBeBuiltException(typeof(TChannel).ToString());
            }
            else {
                return (IChannelFactory<TChannel>) new EmailChannelFactory(context);
            }
        }

        /// <summary>
        /// Indicates if type of raspmail reply channel
        /// </summary>
        /// <typeparam name="TChannel">type of channel</typeparam>
        /// <param name="context">the context</param>
        /// <returns>true if type of Rasp mail reply channel</returns>
        public override bool CanBuildChannelListener<TChannel>(BindingContext context) {
            // We can only build listeners that produce RaspMailreplyChannel
            return (typeof(TChannel) == typeof(IReplyChannel)); 
        }

        /// <summary>
        /// Listener factory
        /// </summary>
        /// <typeparam name="TChannel">type of channel</typeparam>
        /// <param name="context">the context element</param>
        /// <returns>channel listener</returns>
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }
            if (typeof(TChannel) != typeof(IReplyChannel)) {
                throw new EmailTransportChannelCouldNotBeBuiltException(typeof(TChannel).ToString());
            }
            else {
                return (IChannelListener<TChannel>)new EmailChannelListener(context);
            }
        }

        /// <summary>
        /// Creates a rasp mail handler
        /// </summary>
        /// <param name="bindingContext">the binding context</param>
        /// <returns>a rasp mail handler</returns>
        public static MailHandler GetRaspMailHandlerFromBindingContext(BindingContext bindingContext) {
            // Get the mail binding from the context
            EmailBindingElement mailBinding = (EmailBindingElement)bindingContext.Binding.Elements.Find<EmailBindingElement>();
            if(mailBinding == null)
                throw new EmailBindingElemenNotFoundtException();

            // Get the type of the outbox implementation
            
            Type outBoxImplementationType = Type.GetType(mailBinding.OutboxImplementation);
            if (outBoxImplementationType == null)
                throw new MailboxImplementationCouldNotBeFoundException("outbox");

            // Get the type of the inbox implementation
            Type inBoxImplementationType = Type.GetType(mailBinding.InboxImplementation);
            if (inBoxImplementationType == null)
                throw new MailboxImplementationCouldNotBeFoundException("inbox");

            
            // Read the connection policy from config
            MailServerConnectionPolicy inboxConnectionPolicy = new MailServerConnectionPolicy();
            if (mailBinding.PollingInterval != null && mailBinding.PollingInterval != TimeSpan.Zero)
                inboxConnectionPolicy.PollingInterval = mailBinding.PollingInterval;
            
            inboxConnectionPolicy.AuthenticationMode = mailBinding.ReceivingAuthenticationMode;
            inboxConnectionPolicy.Port = mailBinding.ReceivingPort;

            MailServerConnectionPolicy outboxConnectionPolicy = new MailServerConnectionPolicy();
            outboxConnectionPolicy.PollingInterval = inboxConnectionPolicy.PollingInterval;
            outboxConnectionPolicy.AuthenticationMode = mailBinding.SendingAuthenticationMode;
            outboxConnectionPolicy.Port = mailBinding.SendingPort;


            MailServerConfiguration recievingServerConfiguration = new MailServerConfiguration(
                        mailBinding.ReceivingServerAddress,
                        mailBinding.ReceivingUserName,
                        mailBinding.ReceivingPassword,
                        mailBinding.ReplyAddress,
                        inboxConnectionPolicy);
            MailServerConfiguration sendingServerConfiguration = new MailServerConfiguration(
                        mailBinding.SendingServerAddress,
                        mailBinding.SendingUserName,
                        mailBinding.SendingPassword,
                        mailBinding.ReplyAddress,
                        outboxConnectionPolicy);
            MailHandlerConfiguration handlerConfiguration = new MailHandlerConfiguration(
                        outBoxImplementationType,
                        inBoxImplementationType,
                        sendingServerConfiguration,
                        recievingServerConfiguration);
            return new MailHandler(handlerConfiguration);
        }
    }
}