using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.handlers.email;
using dk.gov.oiosi.extension.wcf.EmailTransport;

using NUnit.Framework;

namespace Interoptest
{
    public class TestBaseClass
    {
        /// <summary>
        /// Our request.
        /// </summary>
         protected Request request;

        /// <summary>
        /// Constructor
        /// </summary>
         public TestBaseClass() {
            //RaspEmailTransportExceptionEvent.ExceptionThrown += new dk.gov.oiosi.exception.AsyncExceptionThrownHandler(RaspEmailTransportExceptionEvent_ExceptionThrown);
            EmailTransportUserConfig emailTransportConfig = ConfigurationHandler.GetConfigurationSection<EmailTransportUserConfig>();

        }

        [TearDown]
        public void TearDown()
        {
            Utilities.StopRaspMailService();
            Utilities.ResetTime();

            if (request != null)
            {
                try
                {
                    request.Abort();
                }
                catch { }
            }

        }

        void RaspEmailTransportExceptionEvent_ExceptionThrown(object sender, Exception ex)
        {
            Console.WriteLine("Asynchronous exception was thrown by the email transport: " + ex);
        }
    }
}
