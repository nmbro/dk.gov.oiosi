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

namespace dk.gov.oiosi.extension.wcf.Interceptor {
    /// <summary>
    /// Helper class that can check the order on the binding elements for a binding 
    /// element collection.
    /// </summary>
    /// <remarks>
    /// Given an array on the form [T1, T2, T3]. The check with a collection on the
    /// form [T1, T4, T2, T3] is acceptable because T1 is before T2 is before T3.
    /// </remarks>
    public class BindingElementOrderChecker {
        private Type[][] _bindingOrder;

        /// <summary>
        /// Constructor that takes the order of how the binding elements should be.
        /// If a binding element in the collection is not in the order it is accepted 
        /// as they can be placed anywhere.
        /// </summary>
        /// <param name="bindingOrder"></param>
        public BindingElementOrderChecker(Type[] bindingOrder) {
            _bindingOrder = new Type[bindingOrder.Length][];
            for (int i = 0; i < bindingOrder.Length; i++) {
                _bindingOrder[i] = new Type[1];
                _bindingOrder[i][0] = bindingOrder[i];
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bindingOrder">The binding order</param>
        public BindingElementOrderChecker(Type[][] bindingOrder) {
            this._bindingOrder = bindingOrder;
        }

        /// <summary>
        /// Checks the order of the binding element collection given and throws an
        /// exception if it fails. 
        /// </summary>
        /// <param name="bindingElements"></param>
        public void CheckOrder(BindingElementCollection bindingElements) {
            uint[] foundOrder = new uint[_bindingOrder.Length];
            uint k = 0;
            foreach (BindingElement bindingElement in bindingElements) {
                k++;
                Type bindingElementType = bindingElement.GetType();
                for (ushort i = 0; i < _bindingOrder.Length; i++ ) {
                    for (ushort j = 0; j < _bindingOrder[i].Length; j++) {
                        if (bindingElementType.Equals(_bindingOrder[i][j])) {
                            foundOrder[i] = k;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < foundOrder.Length; i++) {
                if (foundOrder[i] == 0)
                    throw new BindingElementNotInStackException(_bindingOrder[i]);
                if (i == 0) continue;
                if (foundOrder[i - 1] > foundOrder[i])
                    throw new BindingElementOrderMismatchException(_bindingOrder[i - 1], _bindingOrder[i]);
            }
        }
    }
}
