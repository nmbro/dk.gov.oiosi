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

namespace dk.gov.oiosi.communication.Proxy {

    /// <summary>
    /// Core RASP proxy class for sending XML messages to services.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class RaspProxy : System.ServiceModel.ClientBase<IRaspProxyContract>, IRaspProxyContract {

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public RaspProxy() {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpointConfigurationName">the name of the endpoint configuration</param>
        public RaspProxy(string endpointConfigurationName)
            :base(endpointConfigurationName) {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpointConfigurationName">the name of the endpoint configuration</param>
        /// <param name="remoteAddress">remote address</param>
        public RaspProxy(string endpointConfigurationName, string remoteAddress)
            :base(endpointConfigurationName, remoteAddress) {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpointConfigurationName">the name of the endpoint configuration</param>
        /// <param name="remoteAddress">remote address</param>
        public RaspProxy(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress)
            :base(endpointConfigurationName, remoteAddress) {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding">binding</param>
        /// <param name="remoteAddress">remote address</param>
        public RaspProxy(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress)
            :base(binding, remoteAddress) {
        }
        #endregion

        /// <summary>
        /// Sends a request to the remote endpoint, and returns the reply
        /// </summary>
        public System.ServiceModel.Channels.Message RequestRespond(System.ServiceModel.Channels.Message request) {
            return base.Channel.RequestRespond(request);
        }

        /// <summary>
        /// Async request/response begin
        /// </summary>
        public System.IAsyncResult BeginRequestRespond(System.ServiceModel.Channels.Message request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginRequestRespond(request, callback, asyncState);
        }

        /// <summary>
        /// Async request/response end
        /// </summary>
        public System.ServiceModel.Channels.Message EndRequestRespond(System.IAsyncResult result) {
            return base.Channel.EndRequestRespond(result);
        }
    }
}