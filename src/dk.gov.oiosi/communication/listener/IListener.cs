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

using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.communication.listener {

    /// <summary>
    /// An OIOSI service listener
    /// </summary>
    public interface IListener: IAsyncExceptionThrower {
        
        /// <summary>
        /// Aborts (hard close-down) the listening
        /// </summary>
        void Abort();
        
        /// <summary>
        /// Starts listening for incoming messages
        /// </summary>
        void Start();

        /// <summary>
        /// Stop listening for incoming messages
        /// </summary>
        void Stop();

        /// <summary>
        /// Event raised whenever a message has been received
        /// </summary>
        event MessageEventDelegate MessageReceive;

        /// <summary>
        /// The identity of the listener (such as credentials)
        /// </summary>
        ListenerIdentity Identity { get; }

        /// <summary>
        /// The state the listener is currently in (i.e. Listening/Stopped/...)
        /// </summary>
        ListenerState State { get; }
    }
}