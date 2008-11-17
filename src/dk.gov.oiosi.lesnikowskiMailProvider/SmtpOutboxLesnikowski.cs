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
  *   Dennis S�gaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */

using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.communication.handlers.email;
using Lesnikowski.Client;
using Lesnikowski.Mail;
using Lesnikowski.Mail.Headers;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.IO;
using System.Diagnostics;

using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.lesnikowskiMailProvider {
    /// <summary>
    /// Represents an smtp outbox based on the Lesnikowski mail library implementation
    /// </summary>
    public class SmtpOutboxLesnikowski: Outbox {
        private Smtp _proxy = new Smtp();

        /// <summary>
        /// Constructs the outbox from configuration
        /// </summary>
        /// <param name="mailServerConfiguration">The mail server configuration</param>
        public SmtpOutboxLesnikowski(MailServerConfiguration mailServerConfiguration)
            : base(mailServerConfiguration) {}

        /// <summary>
        /// Default constructor
        /// </summary>
        public SmtpOutboxLesnikowski() { }

        /// <summary>
        /// Sends via smtp server
        /// </summary>
        /// <param name="mail">The email to send</param>
        protected override void SendViaServer(dk.gov.oiosi.communication.handlers.email.MailSoap12TransportBinding mail) {
            try {
                WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox starting to send mail...");
                SmtpMail lesnikowskiMail = CreateLesnikowskiMail(mail);
                WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox starting to send mail...");
                _proxy.SendMessage(lesnikowskiMail);
                WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox starting to send mail...");
            }
            catch (Exception e) {
                throw new LesnikowskiSendException(e);
            }
        }


        private void WaitForPreviousConnectionsToClose() {
            while(_proxy.Connected)
                System.Threading.Thread.Sleep(250);
        }

        /// <summary>
        /// Performs an smtp logon
        /// </summary>
        protected override void LogOn() {
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox starting to log on to account " + OutboxServerConfiguration.UserName);
            WaitForPreviousConnectionsToClose();

            try {
                // Check that the username, password, or server address arent empty strings
                if (OutboxServerConfiguration.ServerAddress == "")
                    throw new LesnikowskiMissingSettingsException();

                _proxy.User = OutboxServerConfiguration.UserName;
                _proxy.Password = OutboxServerConfiguration.Password;


                switch (OutboxServerConfiguration.ConnectionPolicy.AuthenticationMode) {
                    case MailAuthenticationMode.PlainText:
                        LogOnPlainText();
                        break;
                    case MailAuthenticationMode.SSL:
                        LogOnSSL();
                        break;
                    default:
                        LogOnWithoutAuthentication();
                        break;
                }



                WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox finished loggin on");
            }
            catch (NullReferenceException e) {
                WCFLogger.Write(TraceEventType.Error, "Lesnikowski Outbox failed logging on: " + e);
                throw new LesnikowskiMissingSettingsException(e);
            }
            catch (Exception e) {
                WCFLogger.Write(TraceEventType.Error, "Lesnikowski Outbox failed logging on: " + e);
                throw new LesnikowskiLogOnException(OutboxServerConfiguration, e);
            }
        }

        private void LogOnWithoutAuthentication() {
            int port = OutboxServerConfiguration.ConnectionPolicy.Port;
            _proxy.Connect(OutboxServerConfiguration.ServerAddress, port);
            string hostName = System.Net.Dns.GetHostName();
            _proxy.Ehlo(HeloType.EhloHelo, hostName);
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox connected to the mail server '" + OutboxServerConfiguration.ServerAddress + "' without authentication");
        }

        private void LogOnSSL(){
            int port = OutboxServerConfiguration.ConnectionPolicy.Port;
            _proxy.Connect(OutboxServerConfiguration.ServerAddress, port, true);
            string hostName = System.Net.Dns.GetHostName();
            _proxy.Ehlo(HeloType.EhloHelo, hostName);

            try {
                _proxy.Login();
            }
            catch {
                LogOff();
                throw;
            }
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox connected to the mail server '" + OutboxServerConfiguration.ServerAddress + "' using SSL");
        }

        private void LogOnPlainText() {
            int port = OutboxServerConfiguration.ConnectionPolicy.Port;
            _proxy.Connect(OutboxServerConfiguration.ServerAddress, port);
            string hostName = System.Net.Dns.GetHostName();
            _proxy.Ehlo(HeloType.EhloHelo, hostName);

            try {
                _proxy.Login();
            }
            catch {
                LogOff();
                throw;
            }
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox connected to the mail server '" + OutboxServerConfiguration.ServerAddress + "' sending username and password as plain text");
        }

        /// <summary>
        /// Performs an smtp logoff.
        /// </summary>
        protected override void LogOff() {
            _proxy.Close();
            _proxy = new Smtp();
        }

        // Creates an SmtpMail from a MailSOAP12TransportBindingObject
        private Lesnikowski.Client.SmtpMail CreateLesnikowskiMail(dk.gov.oiosi.communication.handlers.email.MailSoap12TransportBinding mailSoap12Binding) {
            // 1. Create a MailMessage with an empty body to send
            Lesnikowski.Mail.SimpleMailMessage simpleMail = new Lesnikowski.Mail.SimpleMailMessage();
            simpleMail.Subject = mailSoap12Binding.Subject;
            simpleMail.TextDataString = mailSoap12Binding.Body;
            simpleMail.From.Add(new MailBox(mailSoap12Binding.From, ""));
            simpleMail.To.Add(new MailBox(mailSoap12Binding.To, ""));
            

            // 2. Create the attachment
            MimeData mime = new MimeData();
            mime.Headers.Add("content-type", mailSoap12Binding.Attachment.ContentType);
            mime.Headers.Add("content-description", mailSoap12Binding.Attachment.ContentDescription);
            mime.Data = mailSoap12Binding.Attachment.Data;

            mime.ContentTransferEncoding = Lesnikowski.Mail.Headers.Constants.MimeEncoding.Base64;
            simpleMail.Attachments.Add(mime);
            MailMessage mailMsg = simpleMail.CreateMail();

            // Add the ID header
            mailMsg.Mime.Headers.Add("Message-Id", mailSoap12Binding.MessageId);

            // If an In-Reply-To has been set, use it
            if (mailSoap12Binding.InReplyTo != null)
                mailMsg.InReplyTo = mailSoap12Binding.InReplyTo;

            return new SmtpMail(mailMsg);
        }
    }
}