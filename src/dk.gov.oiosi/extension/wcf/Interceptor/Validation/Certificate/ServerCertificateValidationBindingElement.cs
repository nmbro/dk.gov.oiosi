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
  *   Jacob Mogensen, mySupply ApS
  *   Jens Madsen, Comcare
  *
  */

using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Certificate
{

    /// <summary>
    /// validation binding element
    /// </summary>
    public class ServerCertificateValidationBindingElement : ValidationServerBindingElement
    {
        private CertificateValidatorWithLookup certificateValidator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">validation configuration</param>
        public ServerCertificateValidationBindingElement(ValidationServerConfiguration configuration)
            : base(configuration)
        {
            this.certificateValidator = new CertificateValidatorWithLookup();
        }

        /// <summary>
        /// Gets request
        /// </summary>
        /// <param name="message">message</param>
        public override void InterceptRequest(InterceptorMessage message)
        {
            this.certificateValidator.Validate(message);
        }

        /// <summary>
        /// Response schema validation is not a part of the first release.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override void InterceptResponse(InterceptorMessage message)
        { }

        /// <summary>
        /// Clones a bindingelement
        /// </summary>
        /// <returns></returns>
        public override BindingElement Clone()
        {
            return new ServerCertificateValidationBindingElement(ValidationServerConfiguration);
        }
    }
}