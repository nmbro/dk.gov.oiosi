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

namespace dk.gov.oiosi.exception {

    /// <summary>
    /// Exception indicating that the number of characters did not match a certain constraint
    /// </summary>
    public class UnexpectedNumberOfCharactersException : MainException {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="argument">The argument that was checked for character count</param>
        /// <param name="characters">The number of characters found</param>
        public UnexpectedNumberOfCharactersException(string argument, int characters) : base(GetKeywords(argument, characters)) { }

        /// <summary>
        /// Returns the relevant keyword key/values for the exception text
        /// </summary>
        /// <param name="argument">The argument that was checked for character count</param>
        /// <param name="characters">The number of characters found</param>
        /// <returns>Returns a dictionary with the keywords</returns>
        private static Dictionary<string, string> GetKeywords(string argument, int characters) {
            Dictionary<string, string> keywords = KeywordFromNumber.GetKeyword("characters", characters);
            KeywordFromString.GetKeyword(keywords, "argument", argument);
            return keywords;
        }
    }
}
