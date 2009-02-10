using System;
using System.ServiceModel;
using System.Runtime.InteropServices;
using System.Xml;
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.raspProfile.extension.wcf.Interceptor.CustomHeader;
using dk.gov.oiosi.uddi.category;


namespace Interoptest
{
    public class Utilities
    {
        #region Services

        private static ServiceHost _serviceHost;
        /// <summary>
        /// Starts a mail service, listening at a mail address defined in App.Config
        /// </summary>
        public static void StartRaspMailService(Type typeOfService) {
            _serviceHost = new ServiceHost(typeOfService, new Uri("mailto:apa.com"));
            _serviceHost.Open();

            Console.WriteLine("A service of the type '" + typeOfService + "' was started at " + DateTime.Now + ", listening at " + _serviceHost.Description.Endpoints[0].Address.Uri.OriginalString + "\n");
        }

        public static void StopRaspMailService() {

            try
            {
                Console.WriteLine("Trying to stop service listening to " + _serviceHost.Description.Endpoints[0].Address.Uri.OriginalString + ", " + DateTime.Now + "\n");
                _serviceHost.Close();
                Console.WriteLine("Service stopped listening to " + _serviceHost.Description.Endpoints[0].Address.Uri.OriginalString + ", " + DateTime.Now + "\n");
            }
            catch { }
        }
        #endregion

        #region Logging
        public static void LogRequest(OiosiMessage r)
        {
            Console.WriteLine("A request is being sent " + DateTime.Now);
        }
        public static void LogResponse(Response r) {
            Console.WriteLine("A response was gotten " + DateTime.Now + ": " + r.ResponseMessage.MessageXml.OuterXml);
        }


        private static DateTime _startTime;
        public static void StartTiming() {
            _startTime = DateTime.Now;
        }

        public static TimeSpan EndTiming()
        {
            return DateTime.Now - _startTime;
        }
        #endregion

        #region Messages
        public static OiosiMessage GetMessageWithEmptyBody() { 
            OiosiMessage m = new OiosiMessage(Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice201Interface/SubmitInvoiceRequest"));
            XmlQualifiedName headerName = new XmlQualifiedName("MessageIdentifier", dk.gov.oiosi.common.Definitions.DefaultOiosiNamespace2007);
            m.MessageHeaders.Add(headerName, 
                MessageHeader.CreateHeader(headerName.Name, headerName.Namespace, "1234567890"));
            m.UbiquitousProperties.Add(PartyIdentifierHeaderSettings.MessagePropertyKey, new PartyIdentifierHeaderSettings("1230000000001", EndpointKeyTypeCode.ean, "1230000000002", EndpointKeyTypeCode.ean));
            return m;
        }
        #endregion

        #region Time

        [DllImport("Kernel32.dll")]
        private static extern bool SetLocalTime(ref SYSTEMTIME Time);

        private static TimeSpan _timeSkew;
        private static bool _isTimeSkewed = false;
        private static object _timeSkewLock = new object();
        public static void SkewTime(TimeSpan skew) {
            _timeSkew = skew;

            Console.WriteLine("Time before skew: " +  DateTime.Now);

            lock (_timeSkewLock)
            {
                SYSTEMTIME st = new SYSTEMTIME();
                st.FromDateTime(DateTime.Now + _timeSkew);
                SetLocalTime(ref st);
                _isTimeSkewed = true;
            }

            Console.WriteLine("Time after skew: " + DateTime.Now);
        }

        public static void ResetTime() {

            lock (_timeSkewLock)
            {
                if (_isTimeSkewed)
                {
                    SYSTEMTIME st = new SYSTEMTIME();
                    st.FromDateTime(DateTime.Now - _timeSkew);
                    SetLocalTime(ref st);
                    _isTimeSkewed = false;

                    Console.WriteLine("Time after skew reset: " + DateTime.Now);
                }
            }
            
        }
        #endregion
    }
}
