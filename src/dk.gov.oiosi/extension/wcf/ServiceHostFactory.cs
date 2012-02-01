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
using System.ServiceModel;
using dk.gov.oiosi.extension.wcf.Behavior;

namespace dk.gov.oiosi.extension.wcf
{
    /// <summary>
    /// Factory that creates a service host and adds the EncryptRmBodiesBehavior
    /// </summary>
    public class ServiceHostFactory : System.ServiceModel.Activation.ServiceHostFactory
    {
        /// <summary>
        /// Creates a service host
        /// </summary>
        /// <param name="constructorString">Construction parameters string</param>
        /// <param name="baseAddresses">The base address of the service host</param>
        /// <returns>Returns the service host</returns>
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            return base.CreateServiceHost(constructorString, baseAddresses);
        }

        /// <summary>
        /// Creates a service host
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="baseAddresses">The host base address</param>
        /// <returns>Creates a service host</returns>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            ServiceHost sh = base.CreateServiceHost(serviceType, baseAddresses);
            sh.Faulted += new EventHandler(sh_Faulted);
            sh.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(sh_UnknownMessageReceived);
            sh.Description.Behaviors.Add(new EncryptRmBodiesBehavior());
            return sh;
        }

        void sh_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            throw new Exception("sh_UnknownMessageReceived");
        }

        void sh_Faulted(object sender, EventArgs e)
        {
            throw new Exception("sh_Faulted");
        }
    }
}
