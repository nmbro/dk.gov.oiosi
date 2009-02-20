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
namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation {

    /// <summary>
    /// Abstract class for server validation binding element
    /// </summary>
    public abstract class ValidationServerBindingElement : ValidationBindingElement {
        /// <summary>
        /// The configuration for the validation server binding element
        /// </summary>
        protected new ValidationServerConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">server configuration</param>
        public ValidationServerBindingElement(ValidationServerConfiguration configuration) : base(configuration) {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets whether it should fault if the validation throws an exception
        /// </summary>
        public override bool DoesFaultOnRequestException {
            get { return _configuration.FaultOnRequestValidationException; }
        }
    }
}