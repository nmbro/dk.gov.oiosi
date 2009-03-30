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
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.communication.fault;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.xml.schematron;

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

        private static OiosiFaultCode GetFaultCode(Exception innerException) {
            if (innerException.GetType() == typeof(SchematronErrorException)) return OiosiFaultCode.Sender;
            if (innerException.GetType() == typeof(NoDocumentTypeFoundException)) return OiosiFaultCode.Sender;
            return OiosiFaultCode.Receiver;
        }

        private static OiosiInnerFaultCode GetInnerFaultCode(Exception innerException) {
            if (innerException.GetType() == typeof(SchematronErrorException)) return OiosiInnerFaultCode.SchematronValidationFault;
            if (innerException.GetType() == typeof(NoDocumentTypeFoundException)) return OiosiInnerFaultCode.UnknownDocumentTypeFault;
            return OiosiInnerFaultCode.InternalSystemFailureFault;
        }
    }
}