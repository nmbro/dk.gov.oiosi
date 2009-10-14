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
using Lesnikowski.Client;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Headers.Constants;
using System.ServiceModel.Channels;
using System.Xml;
using System.IO;
using System.Diagnostics;

using dk.gov.oiosi.logging;


namespace dk.gov.oiosi.lesnikowskiMailProvider {
    /// <summary>
    /// A Pop3 Inbox that uses the Lesnikowski mail library for communications with the mail server
    /// </summary>
    /// <remarks>Supports only the Login_PollOnce_Logout polling pattern</remarks>
    public class Pop3InboxLesnikowski : Inbox {

        private Pop3 _pop3 = new Pop3();

        /// <summary>
        /// Constructor
        /// </summary>
        public Pop3InboxLesnikowski(MailServerConfiguration mailServerConfiguration)
            : base(mailServerConfiguration) {
            TestCompliance(mailServerConfiguration);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Pop3InboxLesnikowski() { }

        private void TestCompliance(IMailServerConfiguration mailServerConfiguration) {
            // This implementation only supports Login - Poll Once - Logout
            if (mailServerConfiguration.ConnectionPolicy == null
                || mailServerConfiguration.ConnectionPolicy.PollingPattern == MailServerPollingPattern.LogOn_KeepPolling_LogOff) {
                //RaiseExceptionEvent(new MailServerPollingPatternNotSupportedException(MailServerPollingPattern.LogOn_KeepPolling_LogOff, "the Lesnikowski inbox implementation"));
                throw new MailServerPollingPatternNotSupportedException(MailServerPollingPattern.LogOn_KeepPolling_LogOff, "the Lesnikowski inbox implementation");
            }
        }

        private void WaitForPreviousConnectionsToClose() {
            while (_pop3.Connected && InboxState != InboxState.Faulted && InboxState != InboxState.Closed)
                System.Threading.Thread.Sleep(250);
        }

        /// <summary>
        /// Logs on to the mail server using the Lesnikowski mail library
        /// </summary>
        protected override void LogOn() {
            TestCompliance(InboxServerConfiguration);

            try {
                // Check that we're not already logged on
                WaitForPreviousConnectionsToClose();

                // Check that the username, password, or server address arent empty strings
                if (InboxServerConfiguration.UserName == "" || InboxServerConfiguration.Password == "" || InboxServerConfiguration.ServerAddress == "")
                    throw new LesnikowskiMissingSettingsException();

                _pop3.User = InboxServerConfiguration.UserName;
                _pop3.Password = InboxServerConfiguration.Password;

                WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox starting to log on to account " + _pop3.User);
                
                // Log on using the type of authentication configured
                switch (InboxServerConfiguration.ConnectionPolicy.AuthenticationMode) {
                    case MailAuthenticationMode.PlainText:
                        LogOnPlainText();
                        break;
                    case MailAuthenticationMode.SSL:
                        LogOnSSL();
                        break;
                    case MailAuthenticationMode.APOP:
                        LogOnAPOP();
                        break;
                    default:
                        LogOnPlainText();
                        break;
                }
                WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox finished logging on");
            }
            catch (NullReferenceException e) {
                throw new LesnikowskiMissingSettingsException(e);
            }
            catch (Exception e) {
                throw new LesnikowskiLogOnException(InboxServerConfiguration, e);
            }
        }


        /// <summary>
        /// Logs on with username and password sent as plain text
        /// </summary>
        private void LogOnPlainText() {
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox sending username " + _pop3.User + " and pass " + _pop3.Password + " in plain text");
            int port = InboxServerConfiguration.ConnectionPolicy.Port;
            _pop3.Connect(InboxServerConfiguration.ServerAddress, port);
            
            try {
                _pop3.Login();
            }
            catch {
                _pop3.Close();
                _pop3 = new Pop3();
                throw;
            }
        }

        /// <summary>
        /// Logs on using SSL
        /// </summary>
        private void LogOnSSL() {
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox logging on using SSL");
            int port = InboxServerConfiguration.ConnectionPolicy.Port;
            _pop3.Connect(InboxServerConfiguration.ServerAddress, port, true);

            try {
                _pop3.Login();
            }
            catch {
                _pop3.Close();
                _pop3 = new Pop3();
                throw;
            }
        }

        /// <summary>
        /// Logs on using APOP
        /// </summary>
        private void LogOnAPOP() {
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox logging on using APOP");
            int port = InboxServerConfiguration.ConnectionPolicy.Port;
            _pop3.Connect(InboxServerConfiguration.ServerAddress, port);

            try {
                _pop3.APOPLogin(); 
            }
            catch {
                _pop3.Close();
                _pop3 = new Pop3();
                throw;
            }
        }

        /// <summary>
        /// Logs off from the mail server using the Lesnikowski mail library
        /// </summary>
        protected override void LogOff() {
            if (_pop3 != null)
                _pop3.Close();
            _pop3 = new Pop3();
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox logged off");
        }

        /// <summary>
        /// Peeks to see if there is any messages in the inbox
        /// </summary>
        /// <returns>First found message</returns>
        protected override MailSoap12TransportBinding PeekOnServer() {
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox peeking on server...");
            try {
                // Get the account stats and see if there were any mails
                _pop3.GetAccountStat();

                if (_pop3.MessageCount > 0) {
                    WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox found "+_pop3.MessageCount+" mails");
                    string mail = _pop3.GetMessage(1);
                    return ConvertStringToMail(mail);
                }
                else {
                    WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox found no mails");
                    return null;
                }
            }
            catch (Exception e) {
                throw new LesnikowskiCouldNotGetMailsException(e);
            }
        }

        /// <summary>
        /// Gets the first message in the inbox
        /// </summary>
        /// <returns>First found message, or null if the inbox did not contain any valid messages</returns>
        protected override MailSoap12TransportBinding PollServer() {
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox polling server...");
            try {
                // Get the account stats and see if there were any mails
                _pop3.GetAccountStat();
                MailSoap12TransportBinding msg = null;

                if (_pop3.MessageCount < 1) return null;

                // There is at least one message on the server and we have to process it
                WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox getting mail...");
                string mail = _pop3.GetMessage(1);
                _pop3.DeleteMessage(1);
                return ConvertStringToMail(mail);
            }
            catch (Exception e) {
                throw new LesnikowskiCouldNotGetMailsException(e);
            }
        }

        /// <summary>
        /// Converts a mail in string format, gotten from the mail server, to a WCF Message object
        /// </summary>
        private MailSoap12TransportBinding ConvertStringToMail(string mail) {
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Inbox converting mail to SOAP...");
            ISimpleMailMessage mailMessage = null;

            // Parse the raw mail data and structure it in a MailMessage 
            try {
                mailMessage = (new SimpleMailMessageBuilder()).CreateFromEml(mail);
                MimeData attachment = GetAttachment(mailMessage);
                Message wcfMessage = DecodeToWcfMessage(attachment);
                MailSoap12TransportBinding mailBinding = GetMailBinding(mailMessage, wcfMessage);
                WCFLogger.Write(TraceEventType.Information, "An incoming mail containing valid SOAP was found");
                return mailBinding;
            }
            catch (Exception) {
                WCFLogger.Write(TraceEventType.Warning, "An incoming mail could not be read");
                return null;
            }
        }

        /// <summary>
        /// Gets the right attachment
        /// Checks that the mimetype is 'application/soap+xml'
        /// Checks that the encoding is base64 http://people.apache.org/~pzf/SMTPBase64Binding.html
        /// This is following the paper 
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        private MimeData GetAttachment(ISimpleMailMessage mailMessage) {
            MimeData attachment = mailMessage.Attachments[0];
            MimeType mimeType = attachment.ContentType.MimeType;
            string mimeSubtypeName = attachment.ContentType.MimeSubtype.ToString();
            if (mimeType != MimeType.Application || mimeSubtypeName != "soap+xml")
                throw new LesnikowskiInvalidMimeTypeException(mimeType, mimeSubtypeName);
            string encoding = attachment.Headers["content-transfer-encoding"];
            string lowerCasedEncoding = encoding.ToLower();
            if (lowerCasedEncoding != "base64")
                throw new LesnikowskiInvalidEncodingException(encoding);
            return attachment;
        }

        private Message DecodeToWcfMessage(MimeData attachment) {
            MemoryStream memstream = attachment.GetMemoryStream();
            BufferManager bufMan = BufferManager.CreateBufferManager(0, 512000);
            ArraySegment<byte> arrSeg = new ArraySegment<byte>(memstream.ToArray());
            memstream.Close();
            MessageEncoder utf8Encoder = new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, Encoding.UTF8).CreateMessageEncoderFactory().CreateSessionEncoder();
            Message message = utf8Encoder.ReadMessage(arrSeg, bufMan);
            bufMan.Clear();
            return message;
        }

        private MailSoap12TransportBinding GetMailBinding(ISimpleMailMessage mailMessage, Message wcfMessage) {
            string from = "";
            string to = "";
            string replyTo = "";

            foreach (Lesnikowski.Mail.Headers.MailBox mb in mailMessage.From) { from = mb.Address; }
            foreach (Lesnikowski.Mail.Headers.MailBox mb in mailMessage.To) { to = mb.Address; }
            foreach (Lesnikowski.Mail.Headers.MailBox mb in mailMessage.ReplyTo) { replyTo = mb.Address; }


            MailSoap12TransportBinding mailBinding =
                new MailSoap12TransportBinding(
                    wcfMessage,
                    from,
                    to,
                    replyTo,
                    mailMessage.MessageID);

            mailBinding.InReplyTo = mailMessage.InReplyTo;
            mailBinding.Subject = mailMessage.Subject;
            mailBinding.Body = mailMessage.TextDataString;
            return mailBinding;
        }
    }
}
