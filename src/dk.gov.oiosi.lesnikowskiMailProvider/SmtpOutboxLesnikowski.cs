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
        private Smtp _smtp = new Smtp();

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
                ISmtpMail lesnikowskiMail = CreateLesnikowskiMail(mail);
                WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox starting to send mail...");
                _smtp.SendMessage(lesnikowskiMail);
                WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox starting to send mail...");
            }
            catch (Exception e) {
                throw new LesnikowskiSendException(e);
            }
        }


        private void WaitForPreviousConnectionsToClose() {
            while(_smtp.Connected)
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

                _smtp.User = OutboxServerConfiguration.UserName;
                _smtp.Password = OutboxServerConfiguration.Password;


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
            _smtp.Connect(OutboxServerConfiguration.ServerAddress, port);
            string hostName = System.Net.Dns.GetHostName();
            _smtp.Ehlo(HeloType.EhloHelo, hostName);
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox connected to the mail server '" + OutboxServerConfiguration.ServerAddress + "' without authentication");
        }

        private void LogOnSSL(){
            int port = OutboxServerConfiguration.ConnectionPolicy.Port;
            _smtp.Connect(OutboxServerConfiguration.ServerAddress, port, true);
            string hostName = System.Net.Dns.GetHostName();
            _smtp.Ehlo(HeloType.EhloHelo, hostName);

            try {
                _smtp.StartTLS();
                _smtp.Login();
            }
            catch {
                LogOff();
                throw;
            }
            WCFLogger.Write(TraceEventType.Verbose, "Lesnikowski Outbox connected to the mail server '" + OutboxServerConfiguration.ServerAddress + "' using SSL");
        }

        private void LogOnPlainText() {
            int port = OutboxServerConfiguration.ConnectionPolicy.Port;
            _smtp.Connect(OutboxServerConfiguration.ServerAddress, port);
            string hostName = System.Net.Dns.GetHostName();
            _smtp.Ehlo(HeloType.EhloHelo, hostName);

            try {
                _smtp.Login();
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
            _smtp.Close();
            _smtp = new Smtp();
        }

        // Creates an SmtpMail from a MailSOAP12TransportBindingObject
        private SmtpMail CreateLesnikowskiMail(dk.gov.oiosi.communication.handlers.email.MailSoap12TransportBinding mailSoap12Binding) {

            // 1. Create the attachment
            HeaderCollection headerCollection = new HeaderCollection();
            headerCollection.Add("content-type", mailSoap12Binding.Attachment.ContentType);
            headerCollection.Add("content-description", mailSoap12Binding.Attachment.ContentDescription);
            MimeData mime = new MimeData(headerCollection);
            mime.Data = mailSoap12Binding.Attachment.Data;
            mime.ContentTransferEncoding = Lesnikowski.Mail.Headers.Constants.MimeEncoding.Base64;

            // 2. Create a MailMessage with an empty body to send
            SimpleMailMessageBuilder mailBuilder = new SimpleMailMessageBuilder();
            mailBuilder.SetTextData(mailSoap12Binding.Body);
            mailBuilder.Subject = mailSoap12Binding.Subject;
            mailBuilder.MessageID = mailSoap12Binding.MessageId;
            mailBuilder.From.Add(new MailBox(mailSoap12Binding.From, ""));
            mailBuilder.To.Add(new MailBox(mailSoap12Binding.To, ""));
            if (mailSoap12Binding.InReplyTo != null) { // If an In-Reply-To has been set, use it
                mailBuilder.InReplyTo = mailSoap12Binding.InReplyTo;
            }
            mailBuilder.AddAttachment(mime);
            ISimpleMailMessage simpleMail = mailBuilder.Create();
            
            List<string> toList = new List<string>();
            toList.Add(mailSoap12Binding.To);

            return new SmtpMail(mailSoap12Binding.From, toList, simpleMail.GetSmtpData());
        }
    }
}
