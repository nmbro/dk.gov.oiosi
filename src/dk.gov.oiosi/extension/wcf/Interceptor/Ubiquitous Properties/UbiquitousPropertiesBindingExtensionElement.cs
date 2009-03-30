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
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace dk.gov.oiosi.extension.wcf.Interceptor.UbiquitousProperties {

    /// <summary>
    /// Interceptor to insert a list of properties to all messages being sent (including Reliable messaging conversations)
    /// <remarks>Should be put right beneath the reliable messaging stack element</remarks>
    /// </summary>
    public class UbiquitousPropertiesBindingExtensionElement : BindingElementExtensionElement {

        /// <summary>
        /// Gets the type of the binding element
        /// </summary>
        public override Type BindingElementType {
            get { return typeof(UbiquitousPropertiesBindingElement); }
        }

        /// <summary>
        /// Creates a binding element
        /// </summary>
        /// <returns>The binding element</returns>
        protected override BindingElement CreateBindingElement() {
            return new UbiquitousPropertiesBindingElement();
        }
    }
}
