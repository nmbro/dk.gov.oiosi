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

using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;
using dk.gov.oiosi.uddi.Businesses;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Utility class. Holds methods for queriying an UDDI registry following the ARS
    /// profile
    /// </summary>
    public class ArsDefaultInstances {
        private static string _invoiceBindingSmtpUddiId = "uddi:28982f00-6460-4e65-a0a0-a1a83ae81b69";
        private static string _invoiceBindingSmtpName = "InvoiceBindingSmtp";

        private static string _invoiceBindingHttpUddiId = "uddi:ee073296-bbc8-4d8f-8f27-841c26857d47";
        private static string _invoiceBindingHttpName = "InvoiceBindingHttp";

        private static string _invoicePortTypeUddiId = "uddi:2e0b402a-7a5e-476b-8686-b33f54fd1f47";
        private static string _invoicePortTypeName = "InvoicePortType";

        /// <summary>
        /// 
        /// </summary>
        public static string InvoiceBindingSmtpUddiId {
            get { return ArsDefaultInstances._invoiceBindingSmtpUddiId; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static string InvoiceBindingSmtpName {
            get { return ArsDefaultInstances._invoiceBindingSmtpName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string InvoiceBindingHttpUddiId {
            get { return ArsDefaultInstances._invoiceBindingHttpUddiId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string InvoiceBindingHttpName {
            get { return ArsDefaultInstances._invoiceBindingHttpName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string InvoicePortTypeUddiId {
            get { return ArsDefaultInstances._invoicePortTypeUddiId; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static string InvoicePortTypeName {
            get { return ArsDefaultInstances._invoicePortTypeName; }
        }

        /// <summary>
        /// Looks through the existing database and creates / updates requiered instances
        /// </summary>
        public static void CheckDefaultInstances() {
        }

    }
}