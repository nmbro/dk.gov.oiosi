/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace dk.gov.oiosi.extension.wcf.Interceptor.XsltTransform {
    /// <summary>
    /// The prunning binding extension element. This class is the element used to add pruning
    /// to the wcf stack.
    /// </summary>
    public class ServerXsltTransformationBindingExtensionElement : BindingElementExtensionElement {
        /// <summary>
        /// The key name for the orignal body property on the message.
        /// </summary>
        public const string ORIGINALBODYPROPERTYNAME = "originalbody";
        private const string propagateOriginalMessage = "PropagateOriginalMessage";
        private const string faultOnTransformationException = "FaultOnTransformationException";

        /// <summary>
        /// Gets whether the the request should be validated.
        /// </summary>
        [ConfigurationProperty(propagateOriginalMessage, DefaultValue = true)]
        public bool PropagateOriginalMessage {
            get { return (bool)base[propagateOriginalMessage]; }
        }

        /// <summary>
        /// Gets whether the response should be validated.
        /// </summary>
        [ConfigurationProperty(faultOnTransformationException, DefaultValue = true)]
        public bool FaultOnTransformationException {
            get { return (bool)base[faultOnTransformationException]; }
        }

        /// <summary>
        /// The method or operation is not implemented.
        /// </summary>
        public override Type BindingElementType {
            get { return typeof(ServerXsltTransformationBindingElement); }
        }

        /// <summary>
        /// The method or operation is not implemented.
        /// </summary>
        /// <returns></returns>
        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement() {
            return new ServerXsltTransformationBindingElement(this);
        }
    }
}
