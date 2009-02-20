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
using System;
using System.Threading;

namespace dk.gov.oiosi.communication.handlers.email {
    
    /// <summary>
    /// A mail queue (such as a local inbox)
    /// </summary>
    public interface IMailQueue 
    {
        /// <summary>
        /// Do we have any mails?
        /// </summary>
        MailSoap12TransportBinding Peek();

        /// <summary>
        /// De-queue the first mail
        /// </summary>
        /// <returns>The first mail, or null if there were no mails</returns>
        MailSoap12TransportBinding Dequeue();

        /// <summary>
        /// De-queues the first mail from the internal Inbox mail queue. Waits until timeout has ocurred.
        /// </summary>
        MailSoap12TransportBinding Dequeue(TimeSpan timeout);
        
        /// <summary>
        /// De-queues the first mail from the internal Inbox mail queue. Waits until timeout has ocurred.
        /// </summary>
        /// <param name="onAbortDequeueing">Set when the dequeueing is supposed to stop before the timeout occurs</param>
        /// <param name="timeout">Maximum allowed time for the dequeueing</param>
        MailSoap12TransportBinding Dequeue(TimeSpan timeout, AutoResetEvent onAbortDequeueing);

        /// <summary>
        /// Dequeues the first reply mail with the matching In-Reply-To 
        /// </summary>
        MailSoap12TransportBinding Dequeue(string inReplyToId, TimeSpan timeout);

        /// <summary>
        /// Dequeues the first reply mail with the matching In-Reply-To 
        /// </summary>
        MailSoap12TransportBinding Dequeue(string inReplyToId, TimeSpan timeout, AutoResetEvent onAbortDequeueing);
    }
}