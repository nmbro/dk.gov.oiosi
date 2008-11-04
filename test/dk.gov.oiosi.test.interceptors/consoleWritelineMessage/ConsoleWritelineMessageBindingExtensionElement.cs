using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace dk.gov.oiosi.test.interceptors.consoleWritelineMessage {
    public class ConsoleWritelineMessageBindingExtensionElement : BindingElementExtensionElement {
        public override Type BindingElementType {
            get { return typeof(ConsoleWritelineMessageBindingElement); }
        }

        protected override BindingElement CreateBindingElement() {
            return new ConsoleWritelineMessageBindingElement();
        }
    }
}
