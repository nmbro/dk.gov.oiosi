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

namespace dk.gov.oiosi.communication.Proxy {

    /// <summary>
    /// Message handler interface. A generic xml proxy must implement this interface.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://rep.oio.dk/oiosi/")]
    public interface IServiceProxyContract {

        /// <summary>
        /// By specifying Action="*" on the client side we indicate that we set the action dynamically in the message.
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>The response</returns>
        [System.ServiceModel.OperationContractAttribute(Action = "*", ReplyAction = "*")]
        System.ServiceModel.Channels.Message RequestRespond(System.ServiceModel.Channels.Message request);
    }
}