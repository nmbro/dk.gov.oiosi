using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Text;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.handlers.email;
using NUnit.Framework;
using dk.gov.oiosi.lesnikowskiMailProvider;
using System.Threading;

namespace dk.gov.oiosi.test.integration.email {

    [TestFixture]
    public class LesnikowskiTest {
        private string[] USERNAMES = { "integrationtest01", "integrationtest02", "integrationtest03", "integrationtest04", "integrationtest05", "integrationtest06", "integrationtest07", "integrationtest08", "integrationtest09" };
        private const string MAILSERVERADDRESS = "test.ehandel.gov.dk";
        private const string PASSWORD = "Test1234";


        [Test]
        public void TestSendAndReceive() {
            Random random = new Random(DateTime.Now.Second);
            int usernameIndex = random.Next(0,9);
            string username = USERNAMES[usernameIndex];
            string fromEmail = username + "@" + MAILSERVERADDRESS;
            string toEmail = username + "@" + MAILSERVERADDRESS;

            MailServerConfiguration smtpMailConf = new MailServerConfiguration(MAILSERVERADDRESS, username, "Test1234", fromEmail);
            smtpMailConf.ConnectionPolicy.Port = new TcpPort(25);
            smtpMailConf.ConnectionPolicy.PollingPattern = MailServerPollingPattern.LogOn_PollOnce_LogOff;
            smtpMailConf.ConnectionPolicy.PollingInterval = TimeSpan.FromSeconds(5);
            smtpMailConf.ConnectionPolicy.AuthenticationMode = MailAuthenticationMode.None;
            SmtpOutboxLesnikowski smtp = new SmtpOutboxLesnikowski(smtpMailConf);

            MailServerConfiguration pop3MailConf = new MailServerConfiguration(MAILSERVERADDRESS, username, "Test1234", fromEmail);
            pop3MailConf.ConnectionPolicy.Port = new TcpPort(110);
            pop3MailConf.ConnectionPolicy.PollingPattern = MailServerPollingPattern.LogOn_PollOnce_LogOff;
            pop3MailConf.ConnectionPolicy.PollingInterval = TimeSpan.FromSeconds(5);
            pop3MailConf.ConnectionPolicy.AuthenticationMode = MailAuthenticationMode.PlainText;
            Pop3InboxLesnikowski pop3 = new Pop3InboxLesnikowski(pop3MailConf);
            
            Message payload = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "Soap:Action", "Send this!");
            MailSoap12TransportBinding outBinding = new MailSoap12TransportBinding(payload, fromEmail, toEmail, "", "LesnikowskiTest:" + Guid.NewGuid());
            string output = smtp.Send(outBinding);

            pop3.BeginReceiving();

            //Wait for the mail
            Thread.Sleep(TimeSpan.FromSeconds(5));

            while (pop3.Peek() != null) {
                MailSoap12TransportBinding inBinding = pop3.Dequeue(TimeSpan.FromMinutes(1));
                Assert.IsNotNull(inBinding);
                Assert.AreEqual(fromEmail, inBinding.From, "Unexpected from email");
                Assert.AreEqual(toEmail, inBinding.To, "Unexpected to email");
                MailSoap12TransportBindingAttachment attachment = inBinding.Attachment;
                Assert.IsNotNull(attachment);
                Message recievedPayload = attachment.WcfMessage;
                Assert.IsNotNull(recievedPayload);
            }
        }

        [Test]
        public void TestSendAndReceiveGMAIL() {
            string username = "hougein@gmail.com";
            string password = "test1234";
            string fromEmail = "hougein@gmail.com";
            string toEmail = "hougein@gmail.com";

            MailServerConfiguration smtpMailConf = new MailServerConfiguration("smtp.gmail.com", username, password, fromEmail);
            smtpMailConf.ConnectionPolicy.Port = new TcpPort(465);
            smtpMailConf.ConnectionPolicy.PollingPattern = MailServerPollingPattern.LogOn_PollOnce_LogOff;
            smtpMailConf.ConnectionPolicy.PollingInterval = TimeSpan.FromSeconds(5);
            smtpMailConf.ConnectionPolicy.AuthenticationMode = MailAuthenticationMode.SSL;
            SmtpOutboxLesnikowski smtp = new SmtpOutboxLesnikowski(smtpMailConf);

            MailServerConfiguration pop3MailConf = new MailServerConfiguration("pop.gmail.com", username, password, fromEmail);
            pop3MailConf.ConnectionPolicy.Port = new TcpPort(995);
            pop3MailConf.ConnectionPolicy.PollingPattern = MailServerPollingPattern.LogOn_PollOnce_LogOff;
            pop3MailConf.ConnectionPolicy.PollingInterval = TimeSpan.FromSeconds(5);
            pop3MailConf.ConnectionPolicy.AuthenticationMode = MailAuthenticationMode.SSL;
            Pop3InboxLesnikowski pop3 = new Pop3InboxLesnikowski(pop3MailConf);

            Message payload1 = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "Soap:Action", "Send this!");
            Message payload2 = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "Soap:Action", "Send this!");
            MailSoap12TransportBinding outBinding1 = new MailSoap12TransportBinding(payload1, fromEmail, toEmail, "", "LesnikowskiTest1:" + Guid.NewGuid());
            MailSoap12TransportBinding outBinding2 = new MailSoap12TransportBinding(payload2, fromEmail, toEmail, "", "LesnikowskiTest2:" + Guid.NewGuid());
            smtp.Send(outBinding1);
            string output = smtp.Send(outBinding2);

            pop3.BeginReceiving();

            try {
                //Wait for the mail
                Thread.Sleep(TimeSpan.FromSeconds(30));

                while (pop3.Peek() != null) {
                    MailSoap12TransportBinding inBinding = pop3.Dequeue(TimeSpan.FromMinutes(1));
                    Assert.IsNotNull(inBinding);
                    Assert.AreEqual(fromEmail, inBinding.From, "Unexpected from email");
                    Assert.AreEqual(toEmail, inBinding.To, "Unexpected to email");
                    MailSoap12TransportBindingAttachment attachment = inBinding.Attachment;
                    Assert.IsNotNull(attachment);
                    Message recievedPayload = attachment.WcfMessage;
                    Assert.IsNotNull(recievedPayload);
                }
            }
            finally {
                pop3.Close();
            }
        }
    }
}
