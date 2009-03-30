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

using dk.gov.oiosi.exception;
using dk.gov.oiosi.communication.handlers.email;

namespace dk.gov.oiosi.lesnikowskiMailProvider
{
    /// <summary>
    /// Thrown when logging on to a mail server fails
    /// </summary>
    public class LesnikowskiLogOnException : LesnikowskiException {
        /// <summary>
        /// Constructor with inner exception
        /// </summary>
        /// <param name="configuration">The configuration used to logon with</param>
        /// <param name="innerException">The inner exception</param>
        public LesnikowskiLogOnException(IMailServerConfiguration configuration, Exception innerException) : base(GetKeywords(configuration), innerException) { }

        private static Dictionary<string, string> GetKeywords(IMailServerConfiguration configuration) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add("serveraddress", configuration.ServerAddress);
            keywords.Add("replyaddress", configuration.ReplyAddress);
            keywords.Add("username", configuration.UserName);
            return keywords;
        }
    }
}
