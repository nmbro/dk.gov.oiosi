using System;
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
using dk.gov.oiosi.communication.service;

namespace dk.gov.oiosi.communication.client {

    /// <summary>
    /// Message handler interface. A generic xml proxy must implement this interface. Extends the IServiceContract with async methods.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContract(Namespace = "http://rep.oio.dk/oiosi/")]
    [Obsolete("Old mail sending/receiving interface. Use the dk.gov.oiosi.communication.service.IServiceContract.RequestRespond(Message) instead. The interface will be removed.")]
    public interface IClientProxyContract : IServiceContract 
    {
        /// <summary>
        /// Async version of request/response
        /// </summary>
        /// <param name="request">the request</param>
        /// <param name="callback">callback object</param>
        /// <param name="asyncState">async state</param>
        /// <returns></returns>
        [System.ServiceModel.OperationContract(Action = "*", ReplyAction = "*", AsyncPattern = true)]
        [Obsolete("Old mail sending/receiving interface. Use the dk.gov.oiosi.communication.service.IServiceContract.RequestRespond(Message) instead.")]
        System.IAsyncResult BeginRequestRespond(System.ServiceModel.Channels.Message request, System.AsyncCallback callback, object asyncState);

        /// <summary>
        /// Async version of request/response
        /// </summary>
        /// <param name="result">result object</param>
        /// <returns>IAsyncResult</returns>
        [Obsolete("Old mail sending/receiving interface. Use the dk.gov.oiosi.communication.service.IServiceContract.RequestRespond(Message) instead.")]
        System.ServiceModel.Channels.Message EndRequestRespond(System.IAsyncResult result);
    }
}