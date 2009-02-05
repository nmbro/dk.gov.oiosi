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

using dk.gov.oiosi.communication.service;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.fault;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.raspProfile.communication.service {
    
    /// <summary>
    /// Rasp service implementation
    /// </summary>
    public class RaspServiceImplementation: ServiceImplementation {

        /// <summary>
        /// List of the mandatory custom headers
        /// </summary>
        public static string[,] MandatoryCustomHeaders =  
            {   {"MessageIdentifier", Definitions.DefaultOiosiNamespace2007},
                {"SenderPartyIdentifier", Definitions.DefaultOiosiNamespace2007},
                {"ReceiverPartyIdentifier", Definitions.DefaultOiosiNamespace2007}};

        /// <summary>
        /// The main request response method
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>Returns a WCF message object</returns>
        public override Message RequestRespond(Message request) {
            
            // Make sure all headers that should be there are there
            for(int i=0;i<=MandatoryCustomHeaders.GetUpperBound(0);i++){
                if (request.Headers.FindHeader(MandatoryCustomHeaders[i, 0], MandatoryCustomHeaders[i, 1]) < 0) {
                    return System.ServiceModel.Channels.Message.CreateMessage(MessageVersion.Soap12WSAddressing10,
                        new OiosiMessageFault(new Exception("Missing mandatory header " + MandatoryCustomHeaders[i, 1] + ":" + MandatoryCustomHeaders[i, 0]),
                        OiosiFaultCode.Sender,
                        OiosiInnerFaultCode.MissingHeaderFault),
                        common.Definitions.DefaultOiosiNamespace2007 + OiosiInnerFaultCode.MissingHeaderFault.ToString());
                }
            }

            return base.RequestRespond(request);
        }
    }

}
