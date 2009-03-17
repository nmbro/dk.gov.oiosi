/* Axis - .Net Interop NUnit Tests
 * 
 * Test script 003
 * 
 * "Timeout tests. The client and the service should 
 * both be able to handle a session that dies"
 * 
 * Written by Patrik Johansson, 2007
 * Contact: p.johansson@accenture.com
 */

using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.test.nunit.interop;
using NUnit.Framework;
using dk.gov.oiosi.communication;


namespace Interoptest {

    /// <summary>
    /// 003 Timeout tests. The client and the service should both be able to handle a session that dies
    /// </summary>
    public abstract class Test_003 : TestBaseClass {
        // 003.01
        [Test]
        [ExpectedException(typeof(TimeoutException))]
        public abstract void _003_01_Request_Timeout();
    }
}


namespace Interoptest.HTTP {
    [TestFixture]
    public class Test_003 : Interoptest.Test_003 {
        [Test]
        [ExpectedException(typeof(System.ServiceModel.CommunicationException))]
        public override void _003_01_Request_Timeout() {
            request = new Request("OiosiOmniEndpoint60SecDelay");
            Utilities.StartTiming();

            Response response;
            request.GetResponse(Utilities.GetMessageWithEmptyBody(), out response);
            Assert.IsNotNull(response);

            Console.WriteLine("Http: 003.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");
        }
    }
}

namespace Interoptest.Mail {

    [TestFixture]
    public class Test_003 : Interoptest.Test_003 {
        [Test]
        [ExpectedException(typeof(System.ServiceModel.CommunicationException))]
        public override void _003_01_Request_Timeout() {
            request = new Request("OiosiEmailEndpoint120SecDelay");
            Utilities.StartTiming();

            Response response;
            request.GetResponse(Utilities.GetMessageWithEmptyBody(), out response);
            Assert.IsNotNull(response);

            Console.WriteLine("Mail: 003.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

        }
    }
}