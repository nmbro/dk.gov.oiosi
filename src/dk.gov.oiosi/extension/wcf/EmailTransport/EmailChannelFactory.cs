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
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.handlers.email;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {

    /// <summary>
    /// A Factory for email channels
    /// </summary>
    public class EmailChannelFactory: ChannelFactoryBase<IRequestChannel> {
        // The delegate used to call our synchronous opening method asynchronously
        private delegate void AsyncOnOpen(TimeSpan timeout);
        private AsyncOnOpen _asyncOnOpen;

        /// <summary>
        /// The binding context
        /// </summary>
        protected BindingContext pBindingContext;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailChannelFactory(BindingContext bindingContext) {
            pBindingContext = bindingContext;
            _asyncOnOpen = new AsyncOnOpen(OnOpen);
        }

        /// <summary>
        /// OnCreateChannel event
        /// </summary>
        /// <param name="address"></param>
        /// <param name="via"></param>
        /// <returns></returns>
        protected override IRequestChannel OnCreateChannel(System.ServiceModel.EndpointAddress address, Uri via) {
            // Get a mail handler object from the binding
            MailHandler mailHandler = EmailBindingElement.GetRaspMailHandlerFromBindingContext(pBindingContext);
            mailHandler.OnExceptionThrown += new MailboxExceptionThrown(mailHandler_OnExceptionThrown);
            
            // Create the channel
            return new EmailRequestChannel(mailHandler, address, this);
        }

        void mailHandler_OnExceptionThrown(Exception e, object caller) {
            Fault();
        }

        /// <summary>
        /// Called when the factory should start async opening
        /// </summary>
        /// <param name="timeout">Time allowed for opening</param>
        /// <param name="callback">Callback method to be called when opening is finished</param>
        /// <param name="state">Custom state object</param>
        /// <returns>Results from the asynchronous operation</returns>
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state) {
            return _asyncOnOpen.BeginInvoke(timeout, callback, state);
        }

        /// <summary>
        /// OnEndOpen event
        /// </summary>
        /// <param name="result"></param>
        protected override void OnEndOpen(IAsyncResult result) {
            if (_asyncOnOpen != null)
                _asyncOnOpen.EndInvoke(result);
        }

        /// <summary>
        /// Called when the factory is opening
        /// </summary>
        /// <param name="timeout">Allowed time for opening</param>
        protected override void OnOpen(TimeSpan timeout) { 
            /* Do nothing special */
        }

        /// <summary>
        /// Called when the factory is closing
        /// </summary>
        /// <param name="timeout">The maximum amount of time the closedown is allowed to take. After that the factory should abort.</param>
        protected override void OnClose(TimeSpan timeout) {
            base.OnClose(timeout);
        }

        /// <summary>
        /// Called when the factory is aborting (hard closedown)
        /// </summary>
        protected override void OnAbort() {
            base.OnAbort();
        }
    }
}