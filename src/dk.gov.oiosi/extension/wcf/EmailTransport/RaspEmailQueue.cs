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
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.handlers.email;
using System.Threading;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {

    /// <summary>
    /// Used by the transport channel to wrap a mail handler. Is instantiated with a message, which will be put first in the queue. 
    /// After the first message, the one that is stored locally, has been extracted the mail handler will be polled as usual.
    /// </summary>
    [Obsolete("Do not use anymore. One message per channel now.", true)]
    public class RaspEmailQueue: RaspMailHandler
    {
        MailSoap12TransportBinding _firstMessage;
        IMailHandler _mailHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstMessage">The first message in the queue</param>
        /// <param name="mailHandler">The mail handler from which the rest of the messages will be gotten</param>
        public RaspEmailQueue(MailSoap12TransportBinding firstMessage, RaspMailHandler mailHandler)
            :base(mailHandler)
        {
            _firstMessage = firstMessage;
            _mailHandler = mailHandler;
        }


        /// <summary>
        /// Do we have any mails?
        /// </summary>
        public override MailSoap12TransportBinding Peek() 
        {
            if (_firstMessage != null) {
                MailSoap12TransportBinding firstMessage = _firstMessage;
                _firstMessage = null;
                return firstMessage;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// De-queue the first mail
        /// </summary>
        /// <returns></returns>
        public override MailSoap12TransportBinding Dequeue() 
        {
            if (_firstMessage != null) {
                MailSoap12TransportBinding firstMessage = _firstMessage;
                _firstMessage = null;
                return firstMessage;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// De-queues the first mail from the internal Inbox mail queue. Waits until timeout has ocurred.
        /// </summary>
        /// <param name="onAbortDequeueing">Set when the dequeueing is supposed to stop before the timeout occurs</param>
        /// <param name="timeout">Time parameter</param>
        public override MailSoap12TransportBinding Dequeue(TimeSpan timeout, AutoResetEvent onAbortDequeueing) {
            if (_firstMessage != null) {
                MailSoap12TransportBinding firstMessage = _firstMessage;
                _firstMessage = null;
                return firstMessage;
            }
            else {
                return null;
            }
        }
    }
}