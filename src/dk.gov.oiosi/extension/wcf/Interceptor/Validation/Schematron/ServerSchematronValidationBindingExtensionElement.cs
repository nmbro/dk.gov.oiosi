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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *   Jacob Mogensen, mySupply ApS
  *   Jens Madsen, Comcare
  *
  */
using System;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron {

    /// <summary>
    /// Binding extension element for the server schematron validation interceptor
    /// </summary>
    public class ServerSchematronValidationBindingExtensionElement : ValidationServerConfiguration {
        
        /// <summary>
        /// Gets a bindingelement type
        /// </summary>
        public override Type BindingElementType {
            get { return typeof(ServerSchematronValidationBindingElement); }
        }

        /// <summary>
        /// creates a new binding element
        /// </summary>
        /// <returns></returns>
        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement() {
            return new ServerSchematronValidationBindingElement(this);
        }
    }
}