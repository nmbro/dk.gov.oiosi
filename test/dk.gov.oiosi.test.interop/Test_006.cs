/* Axis - .Net Interop NUnit Tests
 * 
 * Test script 006
 * 
 * "Sending SOAP with custom headers"
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


namespace Interoptest
{

    /// <summary>
    /// 006 Sending SOAP with custom headers
    /// </summary>
    public abstract class Test_006 : TestBaseClass
    {
        // 006.01
        public abstract void _006_01_SendWithCustomHeader();

    }
}


namespace Interoptest.HTTP
{
    [TestFixture]
    public class Test_006 : Interoptest.Test_006
    {

        [Test]
        public override void _006_01_SendWithCustomHeader()
        {
            request = new Request("OiosiOmniEndpointA");
            Utilities.StartTiming();

            Response response;
            request.GetResponse(Utilities.GetMessageWithEmptyBody(), out response);
            Assert.IsNotNull(response);

            Console.WriteLine("Http: 006.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");
        }
    }
}