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
using System.Resources;
using dk.gov.oiosi.uddi.ExceptionHandling.Uddi;

namespace dk.gov.oiosi.common.validation {

    /// <summary>
    /// Failure type
    /// </summary>
    public class MainFailure {
        private static ResourceManager resourceManager = new ResourceManager(typeof(ErrorMessages));
  
        /// <summary>
        /// Failure type message
        /// </summary>
        /// <param name="FailureType">The type of the failure</param>
        /// <returns>Returns the failure type message</returns>
        protected static string FailureTypeMessage(Type FailureType) {
            string FailureTypeString = FailureType.ToString();
            string Key = FailureTypeString.Replace('.', '_');
            int index = Key.IndexOf('`');
            if (index > -1) Key = Key.Remove(index);
            string FailureTypeMessage = resourceManager.GetString(Key);
            if (FailureTypeMessage == null || FailureTypeMessage=="") throw new MessageToExceptionNotFoundException(FailureTypeString);
            return FailureTypeMessage;
        }

        /// <summary>
        /// Gets a replaced message
        /// </summary>
        /// <param name="KeywordName">The replacement keyword</param>
        /// <param name="KeywordValue">Keyword value</param>
        /// <param name="FailureType">The type of the failure</param>
        /// <returns>Returns the replaced message</returns>
        protected static string GetReplacedMessage(string KeywordName, string KeywordValue, Type FailureType) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add(KeywordName, KeywordValue);
            return GetReplacedMessage(keywords, FailureType);
        }

        /// <summary>
        /// Gets a replaced message
        /// </summary>
        /// <param name="keywords">The replacement keyword</param>
        /// <param name="FailureType">The failure type</param>
        /// <returns>Returns the replaced message</returns>
        protected static string GetReplacedMessage(Dictionary<string, string> keywords, Type FailureType) {
            string unformatedErrorMessage = FailureTypeMessage(FailureType);
            char[] charArray = unformatedErrorMessage.ToCharArray();
            string keyword = "";
            string fixedString = "";
            bool partOfKeyword = false;

            foreach (char character in charArray) {
                switch (character) {
                    case '[':
                        //This exception cannot be handled by the same way as the standard exception
                        if (partOfKeyword) throw new DoubleStartOfKeywordException("Der dobbelt start af keyword i beskeden : '" + unformatedErrorMessage + "'");
                        partOfKeyword = true;
                        continue;
                    case ']':
                        //This exception cannot be handled by the same way as the standard exception
                        if (!partOfKeyword) throw new UnexpectedEndOfKeywordException("Der er et uventet afslutning af keyword i beskeden : '" + unformatedErrorMessage + "'");
                        string foundKeyword = "";
                        bool keywordExits = keywords.TryGetValue(keyword, out foundKeyword);
                        //This exception cannot be handled by the same way as the standard exception
                        if (!keywordExits) throw new KeywordNotFoundException("Keyword '" + keyword + "' ikke fundet i de medsendte keywords til exception '" + FailureType.ToString() + "'");
                        fixedString += foundKeyword;
                        keyword = "";
                        partOfKeyword = false;
                        continue;
                    default:
                        break;
                }

                if (partOfKeyword) {
                    keyword += character;
                } else {
                    fixedString += character;
                }
            }
            return fixedString;
        }

    }
}
