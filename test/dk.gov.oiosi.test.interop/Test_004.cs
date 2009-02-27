/* Axis - .Net Interop NUnit Tests
 * 
 * Test script 004
 * 
 * "Incomplete RASP stacks. Server and client try to 
 * communicate with different setups. If the settings 
 * do not match a SOAP fault is returned"
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
    /// 004 Incomplete RASP stacks. Server and client try to communicate with different setups. If the settings do not match a SOAP fault is returned
    /// </summary>
    public abstract class Test_004 : TestBaseClass {
        public abstract void _004_01_FullStack_To_FullStack();
        public abstract void _004_02_FullStack_To_WSRM();
        public abstract void _004_03_FullStack_To_WSS();
        public abstract void _004_04_FullStack_To_Plain();

        public abstract void _004_05_WSRM_To_FullStack();
        public abstract void _004_06_WSRM_To_WSRM();
        public abstract void _004_07_WSRM_To_WSS();
        public abstract void _004_08_WSRM_To_Plain();

        public abstract void _004_09_WSS_To_FullStack();
        public abstract void _004_10_WSS_To_WSRM();
        public abstract void _004_11_WSS_To_WSS();
        public abstract void _004_12_WSS_To_Plain();

        public abstract void _004_13_Plain_To_FullStack();
        public abstract void _004_14_Plain_To_WSRM();
        public abstract void _004_15_Plain_To_WSS();
        public abstract void _004_16_Plain_To_Plain();

        public abstract void SendToEndpoint(string configurationName, string testName);
    }
}


namespace Interoptest.HTTP {
    [TestFixture]
    public class Test_004 : Interoptest.Test_004 {

        public override void SendToEndpoint(string configurationName, string testName) {
            request = new Request(configurationName);
            Utilities.StartTiming();
            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine(testName + " - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");
        }

        [Test]
        public override void _004_01_FullStack_To_FullStack() {
            SendToEndpoint("OiosiOmniEndpointA", "004.01");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_02_FullStack_To_WSRM() {
            SendToEndpoint("FullToWSRM", "004.02");
        }

        private class FaultReturnedOrProtocolMismatchException : Exception { }

        [Test, ExpectedException(typeof(FaultReturnedOrProtocolMismatchException))]
        public override void _004_03_FullStack_To_WSS() {
            try {
                SendToEndpoint("FullToWSS", "004.03");
            }
            catch (dk.gov.oiosi.communication.FaultReturnedException) {
                throw new FaultReturnedOrProtocolMismatchException();
            }
            catch (dk.gov.oiosi.communication.ProtocolMismatchException) {
                throw new FaultReturnedOrProtocolMismatchException();
            }
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_04_FullStack_To_Plain() {
            SendToEndpoint("FullToPlain", "004.04");
        }


        [Test, ExpectedException(typeof(FaultReturnedOrProtocolMismatchException))]
        public override void _004_05_WSRM_To_FullStack() {
            try {
                SendToEndpoint("WSRMToFull", "004.05");
            }
            catch (dk.gov.oiosi.communication.FaultReturnedException) {
                throw new FaultReturnedOrProtocolMismatchException();
            }
            catch (dk.gov.oiosi.communication.ProtocolMismatchException) {
                throw new FaultReturnedOrProtocolMismatchException();
            }
        }

        [Test]
        public override void _004_06_WSRM_To_WSRM() {
            SendToEndpoint("WSRMToWSRM", "004.06");
        }



        [Test, ExpectedException(typeof(FaultReturnedOrProtocolMismatchException))]
        public override void _004_07_WSRM_To_WSS() {
            try {
                SendToEndpoint("WSRMToWSS", "004.07");
            }
            catch (dk.gov.oiosi.communication.FaultReturnedException) {
                throw new FaultReturnedOrProtocolMismatchException();
            }
            catch (dk.gov.oiosi.communication.ProtocolMismatchException) {
                throw new FaultReturnedOrProtocolMismatchException();
            }
        }

        
        private class ProtocolOrActionMismatchOrFaultReturnedException : Exception { }

        [Test, ExpectedException(typeof(ProtocolOrActionMismatchOrFaultReturnedException))]
        public override void _004_08_WSRM_To_Plain() {
            try {
                SendToEndpoint("WSRMToPlain", "004.08");
            }
            catch (dk.gov.oiosi.communication.ProtocolMismatchException) {
                throw new ProtocolOrActionMismatchOrFaultReturnedException();
            }
            catch (System.ServiceModel.ActionNotSupportedException) {
                throw new ProtocolOrActionMismatchOrFaultReturnedException();
            }
            catch (dk.gov.oiosi.communication.FaultReturnedException) {
                throw new ProtocolOrActionMismatchOrFaultReturnedException();
            }
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_09_WSS_To_FullStack() {
            SendToEndpoint("WSSToFull", "004.09");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_10_WSS_To_WSRM() {
            SendToEndpoint("WSSToWSRM", "004.10");
        }

        [Test]
        public override void _004_11_WSS_To_WSS() {
            SendToEndpoint("WSSToWSS", "004.11");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_12_WSS_To_Plain() {
            SendToEndpoint("WSSToPlain", "004.12");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_13_Plain_To_FullStack() {
            SendToEndpoint("PlainToFull", "004.13");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_14_Plain_To_WSRM() {
            SendToEndpoint("PlainToWSRM", "004.14");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_15_Plain_To_WSS() {
            SendToEndpoint("PlainToWSS", "004.15");
        }

        [Test]
        public override void _004_16_Plain_To_Plain() {
            SendToEndpoint("PlainToPlain", "004.16");
        }
    }
}

namespace Interoptest.Mail {
    [TestFixture]
    public class Test_004 : Interoptest.Test_004 {

        public override void SendToEndpoint(string configurationName, string testName) {
            request = new Request(configurationName);
            Utilities.StartTiming();

            Response response = request.GetResponse(Utilities.GetMessageWithEmptyBody());
            Assert.IsNotNull(response);

            Console.WriteLine(testName + " - Requesting took " + Utilities.EndTiming().TotalSeconds + " seconds.\n\n");
        }

        [Test]
        public override void _004_01_FullStack_To_FullStack() {
            SendToEndpoint("OiosiEmailEndpoint", "004.01");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_02_FullStack_To_WSRM() {
            SendToEndpoint("MailFullToWSRM", "004.02");
        }


        private class FaultReturnedOrProtocolMismatchException : Exception { }

        [Test, ExpectedException(typeof(ProtocolOrActionMismatchException))]
        public override void _004_03_FullStack_To_WSS() {
            try {
                SendToEndpoint("MailFullToWSS", "004.03");
            }
            catch (dk.gov.oiosi.communication.ProtocolMismatchException) {
                throw new ProtocolOrActionMismatchException();
            }
            catch (FaultReturnedException) {
                throw new ProtocolOrActionMismatchException();
            }
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_04_FullStack_To_Plain() {
            SendToEndpoint("MailFullToPlain", "004.04");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.ProtocolMismatchException))]
        public override void _004_05_WSRM_To_FullStack() {
            SendToEndpoint("MailWSRMToFull", "004.05");
        }

        [Test]
        public override void _004_06_WSRM_To_WSRM() {
            SendToEndpoint("MailWSRMToWSRM", "004.06");
        }

        [Test, ExpectedException(typeof(ProtocolOrActionMismatchException))]
        public override void _004_07_WSRM_To_WSS() {
            try {
                SendToEndpoint("MailWSRMToWSS", "004.07");
            }
            catch (dk.gov.oiosi.communication.ProtocolMismatchException) {
                throw new ProtocolOrActionMismatchException();
            }
            catch (System.ServiceModel.ActionNotSupportedException) {
                throw new ProtocolOrActionMismatchException();
            }
        }


        private class ProtocolOrActionMismatchException : Exception { }

        [Test, ExpectedException(typeof(ProtocolOrActionMismatchException))]
        public override void _004_08_WSRM_To_Plain() {
            try {
                SendToEndpoint("MailWSRMToPlain", "004.08");
            }
            catch (dk.gov.oiosi.communication.ProtocolMismatchException) {
                throw new ProtocolOrActionMismatchException();
            }
            catch (System.ServiceModel.ActionNotSupportedException) {
                throw new ProtocolOrActionMismatchException();
            }
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_09_WSS_To_FullStack() {
            SendToEndpoint("MailWSSToFull", "004.09");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_10_WSS_To_WSRM() {
            SendToEndpoint("MailWSSToWSRM", "004.10");
        }

        [Test]
        public override void _004_11_WSS_To_WSS() {
            SendToEndpoint("MailWSSToWSS", "004.11");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_12_WSS_To_Plain() {
            SendToEndpoint("MailWSSToPlain", "004.12");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_13_Plain_To_FullStack() {
            SendToEndpoint("MailPlainToFull", "004.13");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_14_Plain_To_WSRM() {
            SendToEndpoint("MailPlainToWSRM", "004.14");
        }

        [Test, ExpectedException(typeof(dk.gov.oiosi.communication.FaultReturnedException))]
        public override void _004_15_Plain_To_WSS() {
            SendToEndpoint("MailPlainToWSS", "004.15");
        }

        [Test]
        public override void _004_16_Plain_To_Plain() {
            SendToEndpoint("MailPlainToPlain", "004.16");
        }
    }
}