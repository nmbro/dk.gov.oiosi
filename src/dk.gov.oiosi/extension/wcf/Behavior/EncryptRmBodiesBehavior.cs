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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;

namespace dk.gov.oiosi.extension.wcf.Behavior
{
    /// <summary>
    /// Behavior that defines that the body element of WS-ReliableMessaging SOAP messages should be encrypted and signed
    /// </summary>
    public class EncryptRmBodiesBehavior : IEndpointBehavior, IServiceBehavior {
        /// <summary>
        /// The actions of the messages affected by this behavior
        /// Should contain all WSRM actions
        /// </summary>
        public static string[] actionsToHaveTheirBodiesEncrypted = { 
                "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequence",
                "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequenceResponse",
                "http://schemas.xmlsoap.org/ws/2005/02/rm/SequenceAcknowledgement",
                "http://schemas.xmlsoap.org/ws/2005/02/rm/TerminateSequence",
                "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage",
                "http://schemas.xmlsoap.org/ws/2005/02/rm/AckRequested"
            };


        /// <summary>
        /// Adds the parameters specific to this behavior
        /// </summary>
        /// <param name="endpoint">The remote endpoint</param>
        /// <param name="bindingParameters">The collection of binding parameters to be modified</param>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) {
            AddProtectionRequirements(bindingParameters);
        }

        /// <summary>
        /// Not implemented by this behavior
        /// </summary>
        /// <param name="endpoint">-</param>
        /// <param name="clientRuntime">-</param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) 
        {/* Do nothing special */}

        /// <summary>
        /// Not implemented by this behavior
        /// </summary>
        /// <param name="endpoint">-</param>
        /// <param name="endpointDispatcher">-</param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) 
        {/* Do nothing special */}

        /// <summary>
        /// Not implemented by this behavior
        /// </summary>
        /// <param name="endpoint">-</param>
        public void Validate(ServiceEndpoint endpoint)
        {/* Do nothing special */}
        
       
        #region IServiceBehavior Members

        /// <summary>
        /// Adds the parameters specific to this behavior
        /// </summary>
        /// <param name="serviceDescription">The description of the service</param>
        /// <param name="serviceHostBase">The base of the service host</param>
        /// <param name="endpoints">The local endpoints</param>
        /// <param name="bindingParameters">The collection of binding parameters to be modified</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            AddProtectionRequirements(bindingParameters);
        }

        /// <summary>
        /// Not implemented by this behavior
        /// </summary>
        /// <param name="serviceDescription">-</param>
        /// <param name="serviceHostBase">-</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase) {/*Do nothing special*/}

        /// <summary>
        /// Not implemented by this behavior
        /// </summary>
        /// <param name="serviceDescription">-</param>
        /// <param name="serviceHostBase">-</param>
        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase){/*Do nothing special*/}

        #endregion

        /// <summary>
        /// Adds the requirement telling WCF to encrypt the body of RM messages
        /// </summary>
        private void AddProtectionRequirements(BindingParameterCollection bindingParameters) {
            // Get the protection requirements for the binding
            ChannelProtectionRequirements cpr = bindingParameters.Find<ChannelProtectionRequirements>();

            // If there were no requirements since before, create them
            if (cpr == null) {
                cpr = new ChannelProtectionRequirements();
                bindingParameters.Add(cpr);
            }

            //Create a requirement that says that the body should be encrypted and signed
            ChannelProtectionRequirements encryptRmBody = new ChannelProtectionRequirements();
            MessagePartSpecification body = new MessagePartSpecification();
            body.IsBodyIncluded = true;
            body.MakeReadOnly();
            
             
            // Add all the RM actions to the list of messages to be affected by this behavior
            foreach (string action in actionsToHaveTheirBodiesEncrypted) {
                encryptRmBody.OutgoingEncryptionParts.AddParts(body, action);
                encryptRmBody.OutgoingSignatureParts.AddParts(body, action);

                encryptRmBody.IncomingEncryptionParts.AddParts(body, action);
                encryptRmBody.IncomingSignatureParts.AddParts(body, action);
            }

            // Add the requirement to the channel protection requirements collection
            encryptRmBody.MakeReadOnly();
            cpr.Add(encryptRmBody);
        }
    }
}
