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
using System.Collections.ObjectModel;
using System.Text;
using System.Resources;

namespace dk.gov.oiosi.exception.MessageStore
{
    /// <summary>
    /// The interface for MessageStore exceptions
    /// </summary>
    public interface IExceptionMessageStore {
        /// <summary>
        /// Returns an exception message as string
        /// </summary>
        /// <param name="resources">The list of resource managers to involve</param>
        /// <param name="exceptionType">The type of the exception</param>
        /// <param name="keywords">Keyword pairs to use</param>
        /// <returns>Returns the message string</returns>
        string GetExceptionMessage(IEnumerable<ResourceManager> resources, Type exceptionType, Dictionary<string, string> keywords);

        /// <summary>
        /// Returns a bool whether an exception message can be returned as a string
        /// </summary>
        /// <param name="resources">The resources</param>
        /// <param name="exceptionType">Type of the exception</param>
        /// <param name="keywords">Dictionary of keywords</param>
        /// <param name="exceptionMessage">The exception message</param>
        /// <returns>True if the message exists</returns>
        bool TryGetExternalExceptionMessage(IEnumerable<ResourceManager> resources, Type exceptionType, Dictionary<string, string> keywords, out string exceptionMessage);
    }
}
