using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Text;

using dk.gov.oiosi.extension.wcf.Interceptor;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.test.interceptors.consoleWriteline {

    public class ConsoleWritelineBindingElement : CommonBindingElement {

        public override bool DoesFaultOnRequestException{
            get { return false; }
        }
        
        public override bool DoesRequestIntercept{
            get { return true; }
        }

        public override bool DoesResponseIntercept{
            get { return true; }
        }

        public override void InterceptRequest(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) {
            try {
                Console.WriteLine("{0} : Sent: {1} to '{2}'", DateTime.Now, interceptorMessage.GetHeaders().Action, interceptorMessage.GetHeaders().To);
            }
            catch(Exception e) {
                Console.WriteLine("Console.WriteLine Interceptor failed: " + e);
            }
        }

        public override void InterceptResponse(InterceptorMessage interceptorMessage) {
            try {
                Console.WriteLine("{0} : Received: {1} from '{2}'", DateTime.Now, interceptorMessage.GetHeaders().Action, interceptorMessage.GetHeaders().From);
            }
            catch (Exception e) {
                Console.WriteLine("Console.WriteLine Interceptor failed: " + e);
            }
        }

        public override BindingElement Clone() {
            return new ConsoleWritelineBindingElement();
        }
    }
}
