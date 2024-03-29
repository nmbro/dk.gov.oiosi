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
  *
  */
using System;
using System.ServiceModel.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security {
    /// <summary>
    /// Class that checks the signature validation proof stack.
    /// </summary>
    public class SignatureValidationStackCheck {
        private BindingElementOrderChecker _orderChecker;

        /// <summary>
        /// Default constructor that builds needed variables used when checking the stack
        /// </summary>
        public SignatureValidationStackCheck(Type specificBindingElement) {
            Type[][] order = new Type[3][];
            order[0] = new Type[1];
            order[1] = new Type[1];
            order[2] = new Type[2];
            order[0][0] = typeof(System.ServiceModel.Channels.ReliableSessionBindingElement);
            order[1][0] = specificBindingElement;
            order[2][0] = typeof(System.ServiceModel.Channels.SymmetricSecurityBindingElement);
            order[2][1] = typeof(System.ServiceModel.Channels.AsymmetricSecurityBindingElement);
            _orderChecker = new BindingElementOrderChecker(order);
        }

        /// <summary>
        /// Checks the stack given.
        /// </summary>
        /// <param name="stack">The stack that are to be tested</param>
        public void Check(BindingElementCollection stack) {
            _orderChecker.CheckOrder(stack);
        }
    }
}
