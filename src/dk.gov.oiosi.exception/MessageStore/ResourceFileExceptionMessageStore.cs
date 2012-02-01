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
using System.Resources;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.exception.MessageStore {
    /// <summary>
    /// Used to handle the resourcefile in the MessageStore custom exceptions
    /// </summary>
    public class ResourceFileExceptionMessageStore : IExceptionMessageStore 
    {
        private ResourceManager internalErrorMessages = new ResourceManager(typeof(ErrorMessages));
        private ILogger logger;

        public ResourceFileExceptionMessageStore()
        {
            this.logger = LoggerFactory.Create(this.GetType());
        }

        #region IExceptionHandle Members

        /// <summary>
        /// Returns an exception message as string
        /// </summary>
        /// <param name="resources">The list of resource managers to involve</param>
        /// <param name="exceptionType">The type of the exception</param>
        /// <param name="keywords">Keyword pairs to use</param>
        /// <returns>Returns the message string</returns>
        public string GetExceptionMessage(IEnumerable<ResourceManager> resources, Type exceptionType, Dictionary<string, string> keywords) 
        {
            List<ResourceManager> allResources = new List<ResourceManager>(resources);
            allResources.Add(internalErrorMessages);
            string unformatedErrorMessage = GetUnformatedExceptionMessage(allResources, exceptionType, true);
            string formatedErrorMessage = GetFormatedExceptionMessage(keywords, unformatedErrorMessage, exceptionType);
            
            return formatedErrorMessage;
        }

        /// <summary>
        /// Try to get the exception message. It does not throw any exceptions if 
        /// it fails but returns a boolean whether it succeeded or not.
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="exceptionType"></param>
        /// <param name="keywords"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public bool TryGetExternalExceptionMessage(IEnumerable<ResourceManager> resources, Type exceptionType, Dictionary<string, string> keywords, out string exceptionMessage) {
            exceptionMessage = null;
            string unformatedErrorMessage = GetUnformatedExceptionMessage(resources, exceptionType, false);
            if (unformatedErrorMessage == null) return false;
            string formatedErrorMessage = GetFormatedExceptionMessage(keywords, unformatedErrorMessage, exceptionType);
            exceptionMessage = formatedErrorMessage;
            return true;
        }

        #endregion
        
        private string GetUnformatedExceptionMessage(IEnumerable<ResourceManager> resources, Type exceptionType, bool throwException) {
            string exceptionTypeString = exceptionType.ToString();
            string key = exceptionTypeString.Replace('.', '_');
            int index = key.IndexOf('`');
            if (index > -1)
            {
                key = key.Remove(index);
            }

            string unformatedErrorMessage = string.Empty;
            foreach (ResourceManager resourceManager in resources)
            {
                unformatedErrorMessage = resourceManager.GetString(key);
                if (unformatedErrorMessage != null)
                {
                    break;
                }
            }

            if (unformatedErrorMessage == null && throwException)
            {
                throw new MessageToExceptionNotFoundException("Der kunne ikke findes en fejlbesked til fejlen '" + exceptionType.ToString() + "'.");
            }

            return unformatedErrorMessage;
        }

        private string GetFormatedExceptionMessage(Dictionary<string, string> keywords, string unformatedErrorMessage, Type exceptionType)
        {
            char[] charArray = unformatedErrorMessage.ToCharArray();
            string keyword = "";
            string fixedString = "";
            bool partOfKeyword = false;

            foreach (char character in charArray)
            {
                switch (character)
                {
                    case '[':
                        {
                            //This exception cannot be handled by the same way as the standard exception
                            if (partOfKeyword)
                            {
                                throw new DoubleStartOfKeywordException("Der dobbelt start af keyword i beskeden : '" + unformatedErrorMessage + "'.");
                            }

                            partOfKeyword = true;
                            continue;
                        }
                    case ']':
                        {
                            // This exception cannot be handled by the same way as the standard exception
                            if (!partOfKeyword)
                            {
                                // part two found before part one - not good.
                                throw new UnexpectedEndOfKeywordException("Der er et uventet afslutning af keyword i beskeden : '" + unformatedErrorMessage + "'.");
                            }

                            string foundKeyword = string.Empty;
                            bool keywordExits = keywords.TryGetValue(keyword, out foundKeyword);
                            
                            // This exception cannot be handled by the same way as the standard exception
                            if (!keywordExits)
                            {
                                logger.Warn("Keyword '" + keyword + "' ikke fundet i de medsendte keywords til excpetion '" + exceptionType.ToString() + "'.");
                                throw new KeywordNotFoundException("Keyword '" + keyword + "' ikke fundet i de medsendte keywords til excpetion '" + exceptionType.ToString() + "'.");
                            }

                            fixedString += foundKeyword;
                            keyword = "";
                            partOfKeyword = false;
                            continue;
                        }
                    default:
                        {
                            // all other cars - do nothing
                            break;
                        }
                }

                if (partOfKeyword)
                {
                    keyword += character;
                }
                else
                {
                    fixedString += character;
                }
            }
            return fixedString;
        }
    }
}
