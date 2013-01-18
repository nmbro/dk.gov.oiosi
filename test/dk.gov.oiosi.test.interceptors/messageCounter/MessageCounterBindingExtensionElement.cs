using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace dk.gov.oiosi.test.interceptors.messageCounter {
    public class MessageCounterBindingExtensionElement: BindingElementExtensionElement {

        public override Type BindingElementType {
            get { return typeof(MessageCounterBindingElement); }
        }

        protected override BindingElement CreateBindingElement() {
            return new MessageCounterBindingElement();
        }
    }
}
