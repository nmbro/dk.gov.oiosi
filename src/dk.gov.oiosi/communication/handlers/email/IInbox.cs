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
    /// A mail Inbox
    /// </summary>
    public interface IInbox: IMailQueue
    {
        /// <summary>
        /// The current state of the inbox
        /// </summary>
        InboxState InboxState { get; }

        /// <summary>
        /// The server configuration
        /// </summary>
        IMailServerConfiguration InboxServerConfiguration { get; set;}

        /// <summary>
        /// Starts listening for incoming mails 
        /// </summary>
        void BeginReceiving();

        /// <summary>
        /// Event raised when the inbox state changes
        /// </summary>
        event OnInboxStateChangeDelegate OnInboxStateChange;

        /// <summary>
        /// Closes down the inbox
        /// </summary>
        void Close();


        /// <summary>
        /// Event raised when an exception is thrown
        /// </summary>
        event MailboxExceptionThrown OnExceptionThrown;


    }
}