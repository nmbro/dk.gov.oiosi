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
using System.Resources;
using System.Xml;

using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.fault;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.schematron;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron {
    
    /// <summary>
    /// Custom exception used the schematron validation throws exception
    /// </summary>
    public class SchematronValidateDocumentFailedException : InterceptorChannelException {
        
        /// <summary>
        /// Constructor with innerexception
        /// </summary>
        /// <param name="innerException">Innerexception of the thrown exception</param>
        public SchematronValidateDocumentFailedException(Exception innerException) : base(GetFaultCode(innerException), GetInnerFaultCode(innerException), innerException) { }

        private static OiosiMessageFault.OiosiFaultCode GetFaultCode(Exception innerException) {
            if (innerException.GetType() == typeof(SchematronErrorException)) return OiosiMessageFault.OiosiFaultCode.Sender;
            if (innerException.GetType() == typeof(NoDocumentTypeFoundException)) return OiosiMessageFault.OiosiFaultCode.Sender;
            return OiosiMessageFault.OiosiFaultCode.Reciever;
        }

        private static OiosiMessageFault.OiosiInnerFaultCode GetInnerFaultCode(Exception innerException) {
            if (innerException.GetType() == typeof(SchematronErrorException)) return OiosiMessageFault.OiosiInnerFaultCode.SchematronValidationFault;
            if (innerException.GetType() == typeof(NoDocumentTypeFoundException)) return OiosiMessageFault.OiosiInnerFaultCode.UnknownDocumentTypeFault;
            return OiosiMessageFault.OiosiInnerFaultCode.InternalSystemFailureFault;
        }
    }
}