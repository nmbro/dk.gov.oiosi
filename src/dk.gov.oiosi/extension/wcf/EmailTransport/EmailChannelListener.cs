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
using System.Threading;
using dk.gov.oiosi.communication.handlers.email;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.extension.wcf.EmailTransport {

    /// <summary>
    /// RSAP email listener
    /// </summary>
    public class EmailChannelListener: ChannelListenerBase<IReplyChannel> {
        /// <summary>
        /// 
        /// </summary>
        protected BindingContext pContext;

        /// <summary>
        /// 
        /// </summary>
        protected MailHandler pMailHandler;

        AutoResetEvent _onEndDequeueing = new AutoResetEvent(false);

        // The delegate used to call our synchronous opening method asynchronously
        private delegate void AsyncOnOpen(TimeSpan timeout);
        private AsyncOnOpen _asyncOnOpen;

        // The delegate used to call our synchronous closing method asynchronously
        private delegate void AsyncOnClose(TimeSpan timeout);
        private AsyncOnClose _asyncOnClose;

        // The delegate used to call our synchronous OnAccept method asynchronously
        private delegate IReplyChannel AsyncOnAcceptChannel(TimeSpan timeout);
        private AsyncOnAcceptChannel _asyncOnAcceptChannel;

        /// <summary>
        /// Overrides the Uri to return listener uri base address
        /// </summary>
        public override Uri Uri {
            get { return pContext.ListenUriBaseAddress; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">the binding context</param>
        public EmailChannelListener(BindingContext context) {
            pContext = context;
            _asyncOnOpen = new AsyncOnOpen(OnOpen);
            _asyncOnClose = new AsyncOnClose(OnClose);
            _asyncOnAcceptChannel = new AsyncOnAcceptChannel(OnAcceptChannel);
        }

        /// <summary>
        /// OnAcceptChannel event
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected override IReplyChannel OnAcceptChannel(TimeSpan timeout) {
            Thread.CurrentThread.Name = "RaspEmailChannelListener.OnAcceptChannel";
            WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Listener starting to listen for a mail...");
            MailSoap12TransportBinding mail = pMailHandler.Dequeue(timeout, _onEndDequeueing);
            if (mail == null) {
                WCFLogger.Write(System.Diagnostics.TraceEventType.Information, "Listener got no mail.");
                WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Listener finished listening for a mail.");
                return null;
            }
            else {
                WCFLogger.Write(System.Diagnostics.TraceEventType.Information, "Listener found a mail.");
                WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Listener finished listening for a mail.");
                return new EmailReplyChannel(this, pMailHandler, mail, pContext.Binding.Elements.Find<EmailBindingElement>());
            }
        }
        
        /// <summary>
        /// OnBeginAcceptChannel event
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state) {
            return _asyncOnAcceptChannel.BeginInvoke(timeout, callback, state);
        }

        /// <summary>
        /// OnEndAcceptChannel event
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override IReplyChannel OnEndAcceptChannel(IAsyncResult result) {
            if (_asyncOnAcceptChannel != null)
                return _asyncOnAcceptChannel.EndInvoke(result);
            else
                return null;
        }

        /// <summary>
        /// OnWaitForChannel event
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected override bool OnWaitForChannel(TimeSpan timeout) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// OnWaitForChannel event
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// OnEndWaitForChannel event
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override bool OnEndWaitForChannel(IAsyncResult result) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// OnAbort event
        /// </summary>
        protected override void OnAbort() {
            WCFLogger.Write(System.Diagnostics.TraceEventType.Start, "Listener beginning to abort...");
            _onEndDequeueing.Set();
            if(pMailHandler != null)
                pMailHandler.Close();
            WCFLogger.Write(System.Diagnostics.TraceEventType.Stop, "Listener finished aborting.");

        }

        #region Initialization and cleanup

        /// <summary>
        /// Begins opening the channel
        /// </summary>
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state) {
            return _asyncOnOpen.BeginInvoke(timeout, callback, state);
        }
        /// <summary>
        /// Ends opening the channel
        /// </summary>
        protected override void OnEndOpen(IAsyncResult result) {
            if (_asyncOnOpen != null)
                _asyncOnOpen.EndInvoke(result);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        protected override void OnOpen(TimeSpan timeout) {
            // Get a mail handler object from the binding
            pMailHandler = EmailBindingElement.GetRaspMailHandlerFromBindingContext(pContext);
            pMailHandler.OnExceptionThrown += new MailboxExceptionThrown(CallbackMailExceptionThrown);
            pMailHandler.OnInboxStateChange += new OnInboxStateChangeDelegate(CallbackOnInboxStateChange);
        }

        

        /// <summary>
        /// Begins closing the channel
        /// </summary>        
        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state) {
            return _asyncOnClose.BeginInvoke(timeout, callback, state);
        }

        /// <summary>
        /// Ends closing the channel
        /// </summary>
        /// <param name="result"></param>
        protected override void OnEndClose(IAsyncResult result) {
            if (_asyncOnClose != null)
                _asyncOnClose.EndInvoke(result);
        }

        /// <summary>
        /// Clean up
        /// </summary>
        protected override void OnClose(TimeSpan timeout) {
            _onEndDequeueing.Set();
            if(pMailHandler != null)
                pMailHandler.Close();
        }

        private void CallbackMailExceptionThrown(Exception e, object caller){
            pContext.Binding.Elements.Find<EmailBindingElement>().RaiseAsyncException(this, e);
            //pMailHandler.Close();
            //pMailHandler = null;
            this.Abort();
        }

        void CallbackOnInboxStateChange(InboxState newState) {
            pContext.Binding.Elements.Find<EmailBindingElement>().InboxStateChange(newState);
        }

        #endregion
    }
}