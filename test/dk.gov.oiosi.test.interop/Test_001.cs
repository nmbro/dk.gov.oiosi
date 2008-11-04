/* Axis - .Net Interop NUnit Tests
 * 
 * Test script 001
 * 
 * "Sending a correctly formatted xml 
 * document over the OIOSI RASP stack, 
 * and receiving a reply"
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

namespace Interoptest {

    /// <summary>
    /// 001 Sending a correctly formatted xml document over the OIOSI RASP stack, and receiving a reply
    /// </summary>
    public abstract class Test_001 : TestBaseClass {

        // 001.1
        public abstract void _001_01_Send_Correctly_Formatted_XML_Document();
    }
}

namespace Interoptest.HTTP {
    [TestFixture]
    public class Test_001 : Interoptest.Test_001 {
        [Test]
        public override void _001_01_Send_Correctly_Formatted_XML_Document() {
            request = new Request("OiosiOmniEndpointA");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Http: 001.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");
        }
    }
}

namespace Interoptest.Mail {
    [TestFixture]
    public class Test_001 : Interoptest.Test_001 {
        [Test]
        public override void _001_01_Send_Correctly_Formatted_XML_Document() {
            request = new Request("OiosiEmailEndpoint");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Mail: 001.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");
        }
    }
}