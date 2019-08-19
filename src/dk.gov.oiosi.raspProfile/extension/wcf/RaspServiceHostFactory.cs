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
using dk.gov.oiosi.extension.wcf.Behavior;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;


namespace dk.gov.oiosi.raspProfile.extension.wcf
{

    /// <summary>
    /// Factory that creates a service host and adds the EncryptRmBodiesBehavior and the rasp custom headers
    /// </summary>
    public class RaspServiceHostFactory : ServiceHostFactoryBase
    {
        /// <summary>
        /// Creates a service host
        /// </summary>
        /// <param name="constructorString"></param>
        /// <param name="baseAddresses">The host base address</param>
        /// <returns>Creates a service host</returns>
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses) {
            ServiceHost sh = new ServiceHost(constructorString, baseAddresses);

            // Add the encrypt RM bodies behavior
            sh.Description.Behaviors.Add(new EncryptRmBodiesBehavior());

            return sh;
        }
    }
}