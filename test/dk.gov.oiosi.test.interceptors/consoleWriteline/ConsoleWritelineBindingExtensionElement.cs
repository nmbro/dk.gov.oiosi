using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace dk.gov.oiosi.test.interceptors.consoleWriteline {
    public class ConsoleWritelineBindingExtensionElement : BindingElementExtensionElement {
        public override Type BindingElementType {
            get { return typeof(ConsoleWritelineBindingElement); }
        }

        protected override BindingElement CreateBindingElement() {
            return new ConsoleWritelineBindingElement();
        }
    }
}
