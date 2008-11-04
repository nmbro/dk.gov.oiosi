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
  *   Dennis S�gaard (dennis.j.sogaard@accenture.com)
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
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.communication.listener;

namespace dk.gov.oiosi.communication.transport {

    /// <summary>
    /// An email service endpoint description
    /// </summary>
    public class EmailTransport: ITransport {

        /// <summary>
        /// The transport binding
        /// </summary>
        private EmailBindingElement pBindingElement;

      
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailTransport(TransportBindingElement binding) {
            pBindingElement = (EmailBindingElement)binding;
        }


        #region ITransport Members

        /// <summary>
        /// property for transportbinding
        /// </summary>
        public TransportBindingElement TransportBinding {
            get { return pBindingElement; }
        }

        #endregion
    }
}
