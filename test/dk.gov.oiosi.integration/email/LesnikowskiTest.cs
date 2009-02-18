using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Text;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.handlers.email;
using NUnit.Framework;
using dk.gov.oiosi.lesnikowskiMailProvider;

namespace dk.gov.oiosi.test.integration.email
{
    [TestFixture]
    public class LesnikowskiTest
    {
        [Test]
        [Ignore]
        public void TestSmtpOutbox() {
            //MailServerConfiguration mailConf = new MailServerConfiguration("relay.netic.dk", "", "", "");
            SmtpOutboxLesnikowski smtpOut = new SmtpOutboxLesnikowski();
            smtpOut.OutboxServerConfiguration = new MailServerConfiguration("relay.netic.dk", "", "", "replyto@test.dk");
            smtpOut.OutboxServerConfiguration.ConnectionPolicy = new MailServerConnectionPolicy(new TcpPort(25));
            smtpOut.OutboxServerConfiguration.ConnectionPolicy.AuthenticationMode = MailAuthenticationMode.None;
            string output = smtpOut.Send(new MailSoap12TransportBinding(Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "Soap:Action", "Send this!"), "from@test.dk", "christian.u.pedersen@accenture.com", "replyto@test.dk", "MessageID"));
        }
    }
}
