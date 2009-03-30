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
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security {
    /// <summary>
    /// The binding extension element for serverside proof of signature validation.
    /// </summary>
    public class ServerSignatureValidationProofBindingExtensionElement : BindingElementExtensionElement {
        /// <summary>
        /// The string key used to store the signature validation proof on the message property.
        /// </summary>
        public const string SignatureValidationProofKey = "signaturevalidationproof";
        private const string FaultOnRequestValidationExceptionKey = "FaultOnRequestValidationException";

        /// <summary>
        /// Gets whether it should fault if the validation throws an exception.
        /// </summary>
        [ConfigurationProperty(FaultOnRequestValidationExceptionKey, DefaultValue = true)]
        public bool FaultOnRequestValidationException {
            get { return (bool)base[FaultOnRequestValidationExceptionKey]; }
        }

        #region BindingElementExtensionElement overrides

        /// <summary>
        /// Gets the binding element type.
        /// </summary>
        public override Type BindingElementType {
            get { return typeof(ServerSignatureValidationProofBindingElement); }
        }

        /// <summary>
        /// Returns a new binding element that creates server side signature validation
        /// proof.
        /// </summary>
        /// <returns></returns>
        protected override BindingElement CreateBindingElement() {
            return new ServerSignatureValidationProofBindingElement(this);
        }

        #endregion
    }
}
