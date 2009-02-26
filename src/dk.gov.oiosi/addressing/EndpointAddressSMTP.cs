/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the Licens e at http://www.mozilla.org/MPL/
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
using System.Net.Mail;
using dk.gov.oiosi.uddi.category;

namespace dk.gov.oiosi.addressing {
    /// <summary>
    /// Represents an URL endpoint address
    /// </summary>
    public class EndpointAddressSMTP : EndpointAddress {
        private MailAddress _endpointSmtp;

        public override EndpointAddressTypeCode EndpointAddressTypeCode {
            get { return EndpointAddressTypeCode.email; }
        }

        /// <summary>
        /// Hide the default constructor
        /// </summary>
        private EndpointAddressSMTP() { }

        /// <summary>
        /// Constructs an EndpointAddressSMTP from a regular mail format, e.g. "test@mail.com"
        /// </summary>
        /// <param name="endpointSmtp"></param>
        public EndpointAddressSMTP(MailAddress endpointSmtp) {
            if (endpointSmtp == null) throw new NullReferenceException("Email address was null");
            _endpointSmtp = endpointSmtp;
        }

        /// <summary>
        /// Constructs and EndpointAddressSMTP from an URI formatted email address,
        /// e.g. "mailto:test@mail.com"
        /// </summary>
        /// <param name="endpointSmtp">an URI formatted email address, e.g. "mailto:test@mail.com"</param>
        public EndpointAddressSMTP(Uri endpointSmtp) {
            if (endpointSmtp == null) throw new NullReferenceException("Email address was null");
            if (!endpointSmtp.AbsoluteUri.ToLower().StartsWith("mailto:")
                || endpointSmtp.AbsoluteUri.Length < 12){
                throw new ArgumentException("Email format must be in the format 'mailto:test@mail.com'", "endpointSmtp");
            }
            string mailPart = endpointSmtp.AbsoluteUri.Substring("mailto:".Length);
            _endpointSmtp = new MailAddress(mailPart);
        }

        /// <summary>
        /// Gets key as string
        /// </summary>
        /// <returns></returns>
        public override string GetKeyAsString() {
            return "mailto:" + _endpointSmtp.Address;
        }

        /// <summary>
        /// Gets keytype as string
        /// </summary>
        /// <returns></returns>
        public override string GetKeyTypeAsString() {
            return "email";
        }

        /// <summary>
        /// Return the Microsoft.Uddi.UrlType as a sting
        /// </summary>
        /// <returns>the url</returns>
        public override string GetUrlTypeAsString() {
            return "Mailto";
        }
    }
}