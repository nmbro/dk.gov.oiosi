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
namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation {

    /// <summary>
    /// Abstract class for server validation binding element
    /// </summary>
    public abstract class ValidationServerBindingElement : ValidationBindingElement {
        private readonly ValidationServerConfiguration _validationServerConfiguration;

        /// <summary>
        /// The configuration for the validation server binding element
        /// </summary>
        public ValidationServerConfiguration ValidationServerConfiguration {
            get { return _validationServerConfiguration; }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">server configuration</param>
        public ValidationServerBindingElement(ValidationServerConfiguration configuration) : base(configuration) {
            _validationServerConfiguration = configuration;
        }

        /// <summary>
        /// Gets whether it should fault if the validation throws an exception
        /// </summary>
        public override bool DoesFaultOnRequestException {
            get { return _validationServerConfiguration.FaultOnRequestValidationException; }
        }
    }
}