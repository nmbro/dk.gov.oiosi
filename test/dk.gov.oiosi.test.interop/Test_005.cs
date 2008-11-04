/* Axis - .Net Interop NUnit Tests
 * 
 * Test script 005
 * 
 * "Clock skews. Server and client mismatch in time. 
 * SOAP seems to have been sent a long time ago/from the 
 * future, and is rejected by the server"
 * 
 * Written by Patrik Johansson, 2007
 * Contact: p.johansson@accenture.com
 */

using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using dk.gov.oiosi.communication;


namespace Interoptest
{

    /// <summary>
    /// 005 Clock skews. Server and client mismatch in time. SOAP seems to have been sent a long time ago/from the future, and is rejected by the server
    /// </summary>
    public abstract class Test_005 : TestBaseClass
    {
        // 005.01
        public abstract void _005_01_FromTheFuture();

        // 005.02
        public abstract void _005_02_FromThePast();

        public abstract void _005_03_SecuityFromTheFuture();

        public abstract void _005_04_SecuityFromThePast();

    }
}


namespace Interoptest.HTTP
{
    [TestFixture]
    public class Test_005 : Interoptest.Test_005
    {

        [Test, ExpectedException(typeof(FaultReturnedException))]
        public override void _005_01_FromTheFuture()
        {
            Utilities.SkewTime(new TimeSpan(1, 0, 0));

            request = new Request("OiosiOmniEndpointA");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Http: 005.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

            Utilities.ResetTime();
        }

        [Test, ExpectedException(typeof(FaultReturnedException))]
        public override void _005_02_FromThePast()
        {
            Utilities.SkewTime(new TimeSpan(-1, 0, 0));

            request = new Request("OiosiOmniEndpointA");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Http: 005.02 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

            Utilities.ResetTime();
        }

        [Test, ExpectedException(typeof(FaultReturnedException))]
        public override void _005_03_SecuityFromTheFuture() {
            Utilities.SkewTime(new TimeSpan(1, 0, 0));

            request = new Request("OiosiOmniEndpointNoRm");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Http: 005.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

            Utilities.ResetTime();
        }

        [Test, ExpectedException(typeof(FaultReturnedException))]
        public override void _005_04_SecuityFromThePast() {
            Utilities.SkewTime(new TimeSpan(-1, 0, 0));

            request = new Request("OiosiOmniEndpointNoRm");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Http: 005.02 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

            Utilities.ResetTime();
        }
    }
}


namespace Interoptest.Mail
{
    [TestFixture]
    public class Test_005 : Interoptest.Test_005
    {

        [Test, ExpectedException(typeof(FaultReturnedException))]
        public override void _005_01_FromTheFuture()
        {
            Utilities.SkewTime(new TimeSpan(1, 0, 0));

            request = new Request("OiosiEmailEndpoint");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Mail: 005.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

            Utilities.ResetTime();
        }

        [Test, ExpectedException(typeof(FaultReturnedException))]
        public override void _005_02_FromThePast()
        {
            Utilities.SkewTime(new TimeSpan(-1, 0, 0));

            request = new Request("OiosiEmailEndpoint");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Mail: 005.02 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

            Utilities.ResetTime();
        }

        [Test, ExpectedException(typeof(FaultReturnedException))]
        public override void _005_03_SecuityFromTheFuture() {
            Utilities.SkewTime(new TimeSpan(1, 0, 0));

            request = new Request("MailWSSToWSS");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Http: 005.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

            Utilities.ResetTime();
        }

        [Test, ExpectedException(typeof(FaultReturnedException))]
        public override void _005_04_SecuityFromThePast() {
            Utilities.SkewTime(new TimeSpan(-1, 0, 0));

            request = new Request("MailWSSToWSS");
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine("Http: 005.01 - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");

            Utilities.ResetTime();
        }
    }
}