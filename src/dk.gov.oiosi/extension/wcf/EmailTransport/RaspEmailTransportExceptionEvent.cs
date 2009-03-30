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
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {

    /// <summary>
    /// Used to propagate exceptions from the email transport to the application layer
    /// </summary>
    [Obsolete("Should not be used anymore. Listen to the ExceptionThrown event on the RASP Email binding element instead.",true)]
    public class RaspEmailTransportExceptionEvent {

        /// <summary>
        /// Event that fires when an exception is thrown.
        /// </summary>
        public static event AsyncExceptionThrownHandler ExceptionThrown;

        /// <summary>
        /// Raises the event
        /// </summary>
        /// <param name="caller">The caller of the event</param>
        /// <param name="e">The exception that caused the event</param>
        public static void Raise(object caller, Exception e) {
            if (ExceptionThrown != null)
                ExceptionThrown.Invoke(caller, e);
        }
    }
}