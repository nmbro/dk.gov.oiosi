using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.extension.wcf.Interceptor;
using System.ServiceModel;

namespace dk.gov.oiosi.test.interceptors.exceptionThrower {
    public class ExceptionThrowerBindingElement : CommonBindingElement {
        public override void InterceptRequest(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) {
            throw new CommunicationException("A communication exception throwing stack element was hit");
        }

        public override void InterceptResponse(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) {
            throw new CommunicationException("An communication exception throwing stack element was hit");
        }

        public override bool DoesRequestIntercept {
            get { return true; }
        }

        public override bool DoesResponseIntercept {
            get { return true; }
        }

        public override bool DoesFaultOnRequestException {
            get { return true; }
        }

        public override System.ServiceModel.Channels.BindingElement Clone() {
            return new ExceptionThrowerBindingElement();
        }
    }
}
