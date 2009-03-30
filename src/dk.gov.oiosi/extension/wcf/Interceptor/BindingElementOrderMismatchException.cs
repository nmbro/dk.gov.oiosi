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
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.extension.wcf.Interceptor {
    /// <summary>
    /// Exception thrown if the binding element order mismatches.
    /// </summary>
    public class BindingElementOrderMismatchException : InterceptorException {
        /// <summary>
        /// Constructor that takes the expected type before the current type and the 
        /// current type as parameters.
        /// </summary>
        /// <param name="before"></param>
        /// <param name="current"></param>
        public BindingElementOrderMismatchException(Type[] before, Type[] current) : base(GetKeywords(before, current)) { }

        private static Dictionary<string, string> GetKeywords(Type[] before, Type[] current) {
            StringBuilder beforeStringBuilder = new StringBuilder();
            for (int i = 0; i < before.Length; i++) {
                beforeStringBuilder.Append(before[i].ToString());
                if (i + 1 < before.Length)
                    beforeStringBuilder.Append(", ");
            }
            StringBuilder currentStringBuilder = new StringBuilder();
            for (int i = 0; i < current.Length; i++) {
                currentStringBuilder.Append(current[i].ToString());
                if (i + 1 < current.Length)
                    currentStringBuilder.Append(", ");
            }
            Dictionary<string, string> keywords = KeywordFromString.GetKeyword("before", beforeStringBuilder.ToString());
            KeywordFromString.GetKeyword(keywords, "current", currentStringBuilder.ToString());
            return keywords;
        }
    }
}
