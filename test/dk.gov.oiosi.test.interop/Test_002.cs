/* Axis - .Net Interop NUnit Tests
 * 
 * Test script 002
 * 
 * "WS-RM re-sending. When the client does not 
 * receive a reply, the request should be re-sent."
 * 
 * 
 * Written by Patrik Johansson, 2007
 * Contact: p.johansson@accenture.com
 */

using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.test.interceptors.messageCounter;


namespace Interoptest {

    /// <summary>
    /// 002 WS-RM re-sending. When the client does not receive a reply, the request should be re-sent.
    /// </summary>
    public abstract class Test_002 : TestBaseClass {
        // 002.01
        [Test]
        public abstract void _002_01_Request_Resend();


    }
}

namespace Interoptest.HTTP {
    [TestFixture]
    public class Test_002 : Interoptest.Test_002 {
        [Test]
        public override void _002_01_Request_Resend() {
            request = new Request("OiosiOmniEndpoint10SecDelay");
            Utilities.StartTiming();

            OiosiMessage m = Utilities.GetMessageWithEmptyBody();
            Response response = request.GetResponse(m);
            Assert.IsNotNull(response);

            int i = 0;
            try {
                i += MessageCounterBindingElement.NoSentMessages(m.RequestAction);
            }
            catch { }
            try {

                i += MessageCounterBindingElement.NoSentMessages("http://schemas.xmlsoap.org/ws/2005/02/rm/AckRequested");
            }
            catch { }

            Assert.Greater(i, 1);

            Console.WriteLine("Http: 002.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");
        }

    }
}

namespace Interoptest.Mail {
    [TestFixture]
    public class Test_002 : Interoptest.Test_002 {
        [Test]
        public override void _002_01_Request_Resend() {
            request = new Request("OiosiEmailEndpoint60SecDelay");
            Utilities.StartTiming();

            OiosiMessage m = Utilities.GetMessageWithEmptyBody();
            Response response = request.GetResponse(m);
            Assert.IsNotNull(response);

            int i = 0;
            try {
                i += MessageCounterBindingElement.NoSentMessages(m.RequestAction);
            }
            catch { }
            try {
                i += MessageCounterBindingElement.NoSentMessages("http://schemas.xmlsoap.org/ws/2005/02/rm/AckRequested");
            }
            catch { }
            Assert.Greater(i, 1);

            Console.WriteLine("Mail: 002.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

        }

    }
}
