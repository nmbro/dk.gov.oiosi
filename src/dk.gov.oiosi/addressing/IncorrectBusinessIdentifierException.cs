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
using System.Resources;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.addressing {
    /// <summary>
    /// Communication exception with the description: The business identifier [businessidentifier] is not in the correct format.
    /// </summary>
    public class IncorrectBusinessIdentifierException : OiosiCommunicationException {
        /// <summary>
        /// IncorrectBusinessIdentifierException constructor
        /// </summary>
        /// <param name="businessIdentifier"></param>
        /// <param name="innerException"></param>
        public IncorrectBusinessIdentifierException(string businessIdentifier, Exception innerException) 
            : base(new ResourceManager(typeof(dk.gov.oiosi.addressing.ErrorMessages)), KeywordFromString.GetKeyword("businessidentifier", businessIdentifier), innerException) 
            { }

        public IncorrectBusinessIdentifierException(string businessIdentifier) 
            : base(new ResourceManager(typeof(dk.gov.oiosi.addressing.ErrorMessages)), KeywordFromString.GetKeyword("businessidentifier", businessIdentifier)) 
            { }
    }
}
