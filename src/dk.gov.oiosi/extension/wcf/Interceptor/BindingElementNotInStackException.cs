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
using System.Text;

namespace dk.gov.oiosi.extension.wcf.Interceptor {
    /// <summary>
    /// Exception thrown when an expected binding element is not in the stack
    /// </summary>
    public class BindingElementNotInStackException : InterceptorException {
        /// <summary>
        /// Constructor that takes the binding element type not found to create an
        /// error message.
        /// </summary>
        /// <param name="expectedBindingElementTypes"></param>
        public BindingElementNotInStackException(Type[] expectedBindingElementTypes) : base(GetKeyword(expectedBindingElementTypes)) { }

        private static Dictionary<string, string> GetKeyword(Type[] expectedBindingElementTypes) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();

            StringBuilder expectedStringBuilder = new StringBuilder();
            for(int i=0; i<expectedBindingElementTypes.Length; i++) {
                expectedStringBuilder.Append(expectedBindingElementTypes[i].ToString());
                if (i + 1 < expectedBindingElementTypes.Length)
                    expectedStringBuilder.Append(", ");
            }

            keywords.Add("type", expectedStringBuilder.ToString());
            return keywords;
        }
    }
}
