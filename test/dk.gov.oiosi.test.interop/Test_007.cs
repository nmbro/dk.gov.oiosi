/* Axis - .Net Interop NUnit Tests
 * 
 * Test script 007
 * 
 * "When sending over mail the “SMTP/MIME Base64 Transport 
 * Binding for SOAP 1.2” [1] protocol is upheld. Non-compliance 
 * does not lead to crashes"
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
    /// 007 When sending over mail the “SMTP/MIME Base64 Transport Binding for SOAP 1.2” [1] protocol is upheld. Non-compliance does not lead to crashes
    /// </summary>
    public abstract class Test_007 : TestBaseClass
    {
        public abstract void _007_01_MissingHeader();
        public abstract void _007_02_WrongEncoding();

    }
}


namespace Interoptest.Mail
{
    [TestFixture]
    public class Test_007 : Interoptest.Test_007
    {

        [Test, ExpectedException(typeof(TimeoutException))]
        public override void _007_01_MissingHeader()
        {
            request = new Request("OiosiEmailEndpointMissingHeader");
            Utilities.StartTiming();


            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Mail: 007.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");
        }

        [Test, ExpectedException(typeof(TimeoutException))]
        public override void _007_02_WrongEncoding()
        {
            request = new Request("OiosiEmailEndpointWrongEncoding");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Mail: 007.02 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");
        }
    }
}