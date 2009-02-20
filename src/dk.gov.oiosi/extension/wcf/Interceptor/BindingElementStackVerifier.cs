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
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor {
    /// <summary>
    /// Class that verifies that a collection of binding elements contains all 
    /// expected binding elements.
    /// </summary>
    public class BindingElementStackVerifier {
        private Type[][] _expectedBindingElementTypes;

        /// <summary>
        /// Constructor that takes the expected binding element types as 
        /// a parameter.
        /// </summary>
        /// <param name="expectedBindingElementTypes"></param>
        public BindingElementStackVerifier(Type[] expectedBindingElementTypes) {
            _expectedBindingElementTypes = new Type[expectedBindingElementTypes.Length][];
            for (int i = 0; i < expectedBindingElementTypes.Length; i++) {
                _expectedBindingElementTypes[i] = new Type[1];
                _expectedBindingElementTypes[i][0] = expectedBindingElementTypes[i];
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="expectedBindingElementTypes">List of the expected binding element types</param>
        public BindingElementStackVerifier(Type[][] expectedBindingElementTypes) {
            _expectedBindingElementTypes = expectedBindingElementTypes;
        }

        /// <summary>
        /// Verifies that the binding element collection(the stack) given has the binding 
        /// elements given by the BindingElements property.
        /// 
        /// If at least one is missing a BindingElementNotInStackException will
        /// be thrown.
        /// </summary>
        /// <param name="bindingElements">The collection of binding elements that 
        /// needs to be verified whether it contains some excpected binding elements</param>
        public void VerifyStack(BindingElementCollection bindingElements) {
            List<Type[]> unverifiedBindingElements = new List<Type[]>(_expectedBindingElementTypes);
            foreach (BindingElement bindingElement in bindingElements) {
                Type bindingElementType = bindingElement.GetType();
                Predicate<Type[]> predicate = 
                    delegate(Type[] unverifiedBindingElementOr) {
                        foreach(Type unverifiedBindingElement in unverifiedBindingElementOr) {
                            if (unverifiedBindingElement.Equals(bindingElementType))
                                return true;
                        }
                        return false;
                    };
                unverifiedBindingElements.RemoveAll(predicate);
            }
            if (unverifiedBindingElements.Count > 0)
                throw new BindingElementNotInStackException(unverifiedBindingElements[0]);
        }
    }
}
