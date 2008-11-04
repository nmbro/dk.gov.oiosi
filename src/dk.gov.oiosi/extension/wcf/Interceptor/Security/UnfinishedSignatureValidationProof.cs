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
using System.ServiceModel.Channels;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.security;
using dk.gov.oiosi.extension.wcf.Interceptor.Security.Header;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security {

    /// <summary>
    /// Represents an unfinished signature validation proof
    /// </summary>
    public class UnfinishedSignatureValidationProof {
        private Headers _headers;
        private Message _responseMessage;
        private SignatureValidationProof _signatureValidationProof;

        /// <summary>
        /// Constructor. Adds WCF headers
        /// </summary>
        /// <param name="headers">The headers to set</param>
        public UnfinishedSignatureValidationProof(Headers headers) {
            _headers = headers;
            _signatureValidationProof = new SignatureValidationProof();
        }

        /// <summary>
        /// Gets the WCF headers
        /// </summary>
        public Headers Headers {
            get { return _headers; }
        }

        /// <summary>
        /// Gets or sets the Signature validation proof
        /// </summary>
        public SignatureValidationProof SignatureValidationProof {
            get { return _signatureValidationProof; }
            set { _signatureValidationProof = value; }
        }

        /// <summary>
        /// Gets or sets the WCF response message
        /// </summary>
        public Message ResponseMessage {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }
    }
}
