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

using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron {

    /// <summary>
    /// Schematron validation binding element
    /// </summary>
    public class ServerSchematronValidationBindingElement : ValidationServerBindingElement {
        private SchematronValidatorWithLookup _validator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">validation configuration</param>
        public ServerSchematronValidationBindingElement(ValidationServerConfiguration configuration)
            : base(configuration) {
            _validator = new SchematronValidatorWithLookup();
        }

        /// <summary>
        /// Gets a message
        /// </summary>
        /// <param name="message">message</param>
        public override void InterceptRequest(InterceptorMessage message) {
            XmlDocument document = message.GetBody();
            _validator.Validate(document);
        }

        /// <summary>
        /// Response schematron validation is not a part of the first release.
        /// </summary>
        /// <param name="message"></param>
        public override void InterceptResponse(InterceptorMessage message) { }

        /// <summary>
        /// Clones a binding element
        /// </summary>
        /// <returns></returns>
        public override BindingElement Clone() {
            return new ServerSchematronValidationBindingElement(_configuration);
        }
    }
}