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

using System.Collections.Generic;

namespace dk.gov.oiosi.communication.handlers.email
{

    /// <summary>
    /// Exception thrown when an illigal transaction takes place in the inbox
    /// </summary>
    public class InboxIllegalStateTransactionException : MailHandlerException {

        /// <summary>
        /// Constructor with keyword
        /// </summary>
        /// <param name="fromState">The state the inbox was in</param>
        /// <param name="toState">The state the inbox was going to</param>
        public InboxIllegalStateTransactionException(InboxState fromState, InboxState toState): base(GetKeywords(fromState, toState)){}


        private static Dictionary<string, string> GetKeywords(InboxState fromState, InboxState toState)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("FromState", fromState.ToString());
            d.Add("ToState", toState.ToString());
            return d;
        }
    }
}