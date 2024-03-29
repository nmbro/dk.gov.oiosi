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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System;
using System.Xml.Xsl;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.communication.fault;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;

namespace dk.gov.oiosi.extension.wcf.Interceptor.XsltTransform {
    /// <summary>
    /// Exception thrown if the XSLT transformation fails.
    /// </summary>
    public class XsltTransformFailedException :  InterceptorChannelException {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public XsltTransformFailedException(Exception innerException) : base(GetFaultCode(innerException), GetInnerFaultCode(innerException), innerException) { }

        
        private static OiosiFaultCode GetFaultCode(Exception innerException) {
            if (innerException.GetType() == typeof(XsltCompileException)) return OiosiFaultCode.Receiver;
            if (innerException.GetType() == typeof(XsltException)) return OiosiFaultCode.Receiver;
            if (innerException.GetType() == typeof(NoDocumentTypeFoundException)) return OiosiFaultCode.Sender;
            return OiosiFaultCode.Receiver;
        }

        private static OiosiInnerFaultCode GetInnerFaultCode(Exception innerException) {
            if (innerException.GetType() == typeof(XsltCompileException)) return OiosiInnerFaultCode.XsltTransformationFault;
            if (innerException.GetType() == typeof(XsltException)) return OiosiInnerFaultCode.XsltTransformationFault;
            if (innerException.GetType() == typeof(NoDocumentTypeFoundException)) return OiosiInnerFaultCode.UnknownDocumentTypeFault;
            return OiosiInnerFaultCode.InternalSystemFailureFault;
        }
    }
}
