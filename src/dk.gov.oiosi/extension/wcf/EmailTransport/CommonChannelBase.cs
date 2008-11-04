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
using System.Threading;

namespace dk.gov.oiosi.extension.wcf.EmailTransport
{
    /// <summary>
    /// Channel base that implements asynchronous calling of the synchronous OnOpen and OnClose methods
    /// </summary>
    public class CommonChannelBase : ChannelBase {
        /// <summary>
        /// Auto reset event that is called when Close or Abort is called 
        /// </summary>
        protected AutoResetEvent pOnClose = new AutoResetEvent(false);

        // The delegate used to call our synchronous opening method asynchronously
        private delegate void AsyncOnOpen(TimeSpan timeout);
        private AsyncOnOpen _asyncOnOpen;


        // The delegate used to call our synchronous closing method asynchronously
        private delegate void AsyncOnClose(TimeSpan timeout);
        private AsyncOnClose _asyncOnClose;

        /// <summary>
        /// The channel manager
        /// </summary>
        protected ChannelManagerBase pChannelManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public CommonChannelBase(ChannelManagerBase channelManager) :base(channelManager) {
            _asyncOnOpen = new AsyncOnOpen(OnOpen);
            _asyncOnClose = new AsyncOnClose(OnClose);
        }


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
            if(_asyncOnOpen != null)
                _asyncOnOpen.EndInvoke(result);
        }
        /// <summary>
        /// Initialization
        /// </summary>
        protected override void OnOpen(TimeSpan timeout) {/* Do nothing special */ }


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
            if(_asyncOnClose != null)
                _asyncOnClose.EndInvoke(result);
        }

        /// <summary>
        /// Clean up
        /// </summary>
        protected override void OnClose(TimeSpan timeout) { 
            pOnClose.Set(); 
        }

        /// <summary>
        /// Abortion clean up
        /// </summary>
        protected override void OnAbort() {
            pOnClose.Set();
        }
    }
}