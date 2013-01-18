using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.extension.wcf.Interceptor;

namespace dk.gov.oiosi.test.interceptors.consoleWritelineMessage {
    class ConsoleWritelineMessageBindingElement : CommonBindingElement {
        public override bool DoesFaultOnRequestException {
            get { return true; }
        }

        public override bool DoesRequestIntercept {
            get { return true; }
        }

        public override bool DoesResponseIntercept {
            get { return true; }
        }

        public override void InterceptRequest(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) {
            Console.WriteLine("--------------Sending Message----------------");
            Console.WriteLine(interceptorMessage.GetCopy().ToString());
            Console.WriteLine("---------------------------------------------");
        }

        public override void InterceptResponse(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) {
            Console.WriteLine("-------------Recieving Message---------------");
            Console.WriteLine(interceptorMessage.GetCopy().ToString());
            Console.WriteLine("---------------------------------------------");
        }

        public override System.ServiceModel.Channels.BindingElement Clone() {
            return new ConsoleWritelineMessageBindingElement();
        }
    }
}
