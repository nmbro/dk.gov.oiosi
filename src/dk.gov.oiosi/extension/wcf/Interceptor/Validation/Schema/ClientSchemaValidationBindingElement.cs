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

using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schema {

    /// <summary>
    /// Binding element for the client schema validator
    /// </summary>
    public class ClientSchemaValidationBindingElement : ValidationBindingElement {
        private SchemaValidatorWithLookup _schemaValidator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">validation configuration</param>
        public ClientSchemaValidationBindingElement(ValidationConfiguration configuration) : base(configuration) {
            _schemaValidator = new SchemaValidatorWithLookup();
        }

        /// <summary>
        /// Implements the standard Clone method of the BindingElement. The function
        /// returns a copy of the current object
        /// </summary>
        /// <returns>binding element</returns>
        public override BindingElement Clone() {
            return new ClientSchemaValidationBindingElement(Configuration);
        }

        /// <summary>
        /// Message send from the client is validated. At any validation faults
        /// stop the action with an exception
        /// </summary>
        /// <param name="message">message</param>
        public override void InterceptRequest(InterceptorMessage message) {
            XmlDocument document = message.GetBody();
            _schemaValidator.Validate(document);
        }

        /// <summary>
        /// Response schema validation is not a part of the first release
        /// </summary>
        /// <param name="message">message</param>
        public override void InterceptResponse(InterceptorMessage message) { }
    }
}