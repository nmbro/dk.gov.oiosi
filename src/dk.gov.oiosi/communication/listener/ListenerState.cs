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

namespace dk.gov.oiosi.communication.listener {
    
    /// <summary>
    /// Enumerator representing the different listener states
    /// </summary>
    public enum ListenerState {
        /// <summary>
        /// The listener has yet to be initialized
        /// </summary>
        uninitialized,

        /// <summary>
        /// The listener is trying to initialize
        /// </summary>
        initializing,

        /// <summary>
        /// The listener has been initialized, but is not listening
        /// </summary>
        initialized,

        /// <summary>
        /// The listener is trying to start listening
        /// </summary>
        starting,

        /// <summary>
        /// The listener is listening for incoming messages
        /// </summary>
        started,

        /// <summary>
        /// The listener is trying to stop listening
        /// </summary>
        stopping,

        /// <summary>
        /// The listener has been stopped
        /// </summary>
        stopped,

        /// <summary>
        /// 
        /// </summary>
        faulted
    }
}