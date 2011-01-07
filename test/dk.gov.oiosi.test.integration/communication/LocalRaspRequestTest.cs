using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.service;
using System.ServiceModel;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.raspProfile.communication;
using System.Xml;
using dk.gov.oiosi.xml.documentType;
using System.ServiceModel.Channels;
using System.Security.Cryptography.X509Certificates;

namespace dk.gov.oiosi.test.integration.communication {

    [TestFixture]
    public class LocalRaspRequestTest {
        private const string LOCALHOST_STRING = "http://localhost:8080/test_rasp_service";
        private const string CLIENT_CERTIFICATE_PATH = "Resources/Certificates/FOCES1.cer";
        private X509Certificate2 privateKeyCertificate;
        private X509Certificate2 publicKeyCertificate;

        [TestFixtureSetUp]
        public void Setup() {
            privateKeyCertificate = CertificateUtil.InstallAndGetFunctionCertificateFromCertificateStore();
            publicKeyCertificate = new X509Certificate2(CLIENT_CERTIFICATE_PATH);
            ConfigurationUtil.SetupConfiguration("RaspConfigurationRaspRequestTest.xml");
        }

        [Test]
        public void OioublApplicationResponse201MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_ApplicationResponse_v2p1.xml");
        }

        [Test]
        public void OioublApplicationResponse202MustBeSendableByRaspRequest() {
            AssertSendable("Resources/Documents/Test/OIOUBL_ApplicationResponse_v2p2.xml");
        }


        # region Private methods

        private void AssertSendable(string path) {
            var host = new ServiceHost(typeof(ServiceStubImplementation), new Uri[] { new Uri(LOCALHOST_STRING) });
            host.Credentials.ServiceCertificate.Certificate = privateKeyCertificate;

            try {
                host.Open(TimeSpan.FromSeconds(5));

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));

                var oioublFile = new FileInfo(path);

                var response = SendRequestAndGetResponse(oioublFile);
                Assert.IsNotNull(response);
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (host.State != CommunicationState.Faulted) {
                    host.Close(TimeSpan.FromSeconds(30));
                }
            }
        }

        private Response SendRequestAndGetResponse(FileInfo file) {
            var documentId = "TEST:" + Guid.NewGuid();
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(file.FullName);
            var oiosiMessage = new OiosiMessage(xmlDocument);
            var raspRequest = GetRaspRequest(oiosiMessage);
            Response response;
            raspRequest.GetResponse(oiosiMessage, documentId, out response);
            return response;
        }

        private RaspRequest GetRaspRequest(OiosiMessage oiosiMessage) {
            var documentTypeConfigSearcher = new DocumentTypeConfigSearcher();
            var documentTypeConfig = documentTypeConfigSearcher.FindUniqueDocumentType(oiosiMessage.MessageXml);
            var endpointAddressUri = new Uri(LOCALHOST_STRING);

            var credentials = new Credentials(new OcesX509Certificate(privateKeyCertificate), new OcesX509Certificate(publicKeyCertificate));
            var request = new Request(endpointAddressUri, credentials);
            var raspRequest = new RaspRequest(request);
            return raspRequest;
        }

        # endregion

        public class ServiceStubImplementation : IServiceContract {
            
            #region IServiceContract Members

            public System.ServiceModel.Channels.Message RequestRespond(System.ServiceModel.Channels.Message request) {
                var oiosiMessage = new OiosiMessage(request);

                var documentTypeConfigSearcher = new DocumentTypeConfigSearcher();
                var documentTypeConfig = documentTypeConfigSearcher.FindUniqueDocumentType(oiosiMessage.MessageXml);

                // Create the reply message (The body can be empty)
                string body = "Request was received " + DateTime.Now.ToString();

                return Message.CreateMessage(MessageVersion.Soap12WSAddressing10, documentTypeConfig.EndpointType.ReplyAction, body);
            }

            #endregion
        }
    
    }
}
