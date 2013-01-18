using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Configuration;
using System.ServiceModel.Channels;

namespace dk.gov.oiosi.test.interceptors.exceptionThrower {
    public class ExceptionThrowerBindingExtensionElement : BindingElementExtensionElement {

        public override Type BindingElementType {
            get { return typeof(ExceptionThrowerBindingElement); }
        }

        protected override BindingElement CreateBindingElement() {
            return new ExceptionThrowerBindingElement();
        }
    }
}
