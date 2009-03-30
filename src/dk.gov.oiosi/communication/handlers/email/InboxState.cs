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
namespace dk.gov.oiosi.communication.handlers.email
{
    /// <summary>
    /// The state of a mail inbox
    /// </summary>
    public enum InboxState {
        /// <summary>
        /// The inbox is currently starting listening
        /// </summary>
        Starting,

        /// <summary>
        /// The inbox is trying to log on to the mail server
        /// </summary>
        AttemptingLogOn,

        /// <summary>
        /// The inbox is currently polling the server, waiting for incoming mails
        /// </summary>
        Listening,

        /// <summary>
        /// The inbox is stopping, and will not detect incoming mails
        /// </summary>
        Closing,

        /// <summary>
        /// The inbox is stopped, and will not detect incoming mails
        /// </summary>
        Initialized, 
        
        /// <summary>
        /// The inbox is closed, and can not be used any more
        /// </summary>
        Closed,

        /// <summary>
        /// The inbox is in the faulted state, and cannot be used anymore
        /// </summary>
        Faulted 
    };
}