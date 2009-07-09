using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.listener;
using dk.gov.oiosi.security.oces;
using NUnit.Framework;

namespace dk.gov.oiosi.test.integration.communication.listener {
    
    [TestFixture]
    public class ListenerTest {
        
        [Test]
        public void HttpListenerTest() {
            ConfigurationUtil.SetupConfiguration();
            X509Certificate2 serverCertificate = CertificateUtil.InstallAndGetFunctionCertificateFromCertificateStore();
            OcesX509Certificate ocesServerCertificate = new OcesX509Certificate(serverCertificate);
            Type serviceImplementationType = typeof (raspProfile.communication.service.RaspServiceImplementation);
            var listenerIdentity = new ListenerIdentity(serviceImplementationType, ocesServerCertificate);
            var listener = new Listener(listenerIdentity);

            listener.MessageReceive += IncomingMessage;
            listener.ExceptionThrown += Listener_ExceptionThrown;
            listener.Start();
            Thread.SpinWait(10000);
        }

        private void Listener_ExceptionThrown(object sender, Exception ex) {
            throw new NotImplementedException("Exception thrown");
        }

        private void IncomingMessage(ListenerRequest message, MessageProcessStatus processStatus) {
            throw new NotImplementedException("new message");
        }
    }
}