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
using dk.gov.oiosi.xml.schema;
using System.Xml.Schema;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schema
{
    /// <summary>
    /// Custom exception used when schema validation throws an exception
    /// </summary>
    public class SchemaValidateDocumentFailedException : InterceptorChannelException {

        /// <summary>
        /// Constuctor with innerexception
        /// </summary>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public SchemaValidateDocumentFailedException(System.Exception innerException) 
            : base(GetFaultCode(innerException), GetInnerFaultCode(innerException), innerException) { }

        private static OiosiFaultCode GetFaultCode(Exception innerException) 
        {
            OiosiFaultCode oiosiFaultCode;
            Type type = innerException.GetType();
            if (type == typeof(XmlSchemaValidationException))
            {
                oiosiFaultCode = OiosiFaultCode.Sender;
            }
            else if (type == typeof(SchemaValidateDocumentFailedException))
            {
                oiosiFaultCode = OiosiFaultCode.Sender;
            }
            else if (type == typeof(SchemaValidationFailedException)) 
            {
                oiosiFaultCode = OiosiFaultCode.Sender;
            }
            else if (type == typeof(NoDocumentTypeFoundException)) 
            {
                oiosiFaultCode = OiosiFaultCode.Sender;
            }
            else if (type == typeof(SchemaValidationInterceptionEmptyBodyException)) 
            {
                oiosiFaultCode =  OiosiFaultCode.Sender;
            }
            else
            {
                 oiosiFaultCode = OiosiFaultCode.Receiver;
            }

            return oiosiFaultCode;
        }

        private static OiosiInnerFaultCode GetInnerFaultCode(Exception innerException)
        {
            OiosiInnerFaultCode oiosiInnerFaultCode;
            Type type = innerException.GetType();
            if (type == typeof(XmlSchemaValidationException))
            {
                oiosiInnerFaultCode = OiosiInnerFaultCode.SchemaValidationFault;
            }
            else if (type == typeof(SchemaValidateDocumentFailedException))
            {
                oiosiInnerFaultCode = OiosiInnerFaultCode.SchemaValidationFault;
            }
            else if (type == typeof(SchemaValidationFailedException))
            {
                oiosiInnerFaultCode = OiosiInnerFaultCode.SchemaValidationFault;
            }
            else if (type == typeof(NoDocumentTypeFoundException))
            {
                oiosiInnerFaultCode = OiosiInnerFaultCode.UnknownDocumentTypeFault;
            }
            else if (type == typeof(SchemaValidationInterceptionEmptyBodyException))
            {
                oiosiInnerFaultCode = OiosiInnerFaultCode.UnknownDocumentTypeFault;
            }
            else
            {
                oiosiInnerFaultCode = OiosiInnerFaultCode.InternalSystemFailureFault;
            }

            return oiosiInnerFaultCode;
        }
    }
}