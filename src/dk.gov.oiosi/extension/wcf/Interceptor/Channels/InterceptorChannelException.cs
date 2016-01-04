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
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.fault;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels {

    /// <summary>
    /// Used when interceptor throws exception
    /// </summary>
    public class InterceptorChannelException : InterceptorException {
        /// <summary>
        /// The fault code of the corresponding fault
        /// </summary>
        protected OiosiFaultCode pFaultCode;

        /// <summary>
        /// The inner fault code of the corresponding fault
        /// </summary>
        protected OiosiInnerFaultCode pInnerFaultCode;

        /// <summary>
        /// Receives a message fault
        /// </summary>
        /// <returns>the fault</returns>
        public MessageFault GetMessageFault() {
            return new OiosiMessageFault(this, pFaultCode, pInnerFaultCode);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        public InterceptorChannelException(Exception innerException) : base(innerException) {
            pFaultCode = OiosiFaultCode.Receiver;
            pInnerFaultCode = OiosiInnerFaultCode.InternalSystemFailureFault;
        }

        /// <summary>
        /// Constructor with faultcode and innerfaultcode
        /// </summary>
        /// <param name="faultCode">rasp message fault code</param>
        /// <param name="innerFaultCode">rasp message innerfault code</param>
        public InterceptorChannelException(OiosiFaultCode faultCode, OiosiInnerFaultCode innerFaultCode)
            : base() { 
            pFaultCode = faultCode;
            pInnerFaultCode = innerFaultCode;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="faultCode"></param>
        /// <param name="innerFaultCode"></param>
        /// <param name="message">The message</param>
        public InterceptorChannelException(OiosiFaultCode faultCode, OiosiInnerFaultCode innerFaultCode, string message)
            : base(message)
        {
            pFaultCode = faultCode;
            pInnerFaultCode = innerFaultCode;
        }

        /// <summary>
        /// Constructor with faultcode, innerfaultcode and keywords
        /// </summary>
        /// <param name="faultCode">rasp message fault code</param>
        /// <param name="innerFaultCode">rasp message innerfault code</param>
        /// <param name="keywords">keywords for the message</param>
        public InterceptorChannelException(OiosiFaultCode faultCode, OiosiInnerFaultCode innerFaultCode, System.Collections.Generic.Dictionary<string, string> keywords)
            : base(keywords) {
            pFaultCode = faultCode;
            pInnerFaultCode = innerFaultCode;
        }

        /// <summary>
        /// Constructor with faultcode, innerfaultcode and innerexception
        /// </summary>
        /// <param name="faultCode">rasp message fault code</param>
        /// <param name="innerFaultCode">rasp message innerfault code</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public InterceptorChannelException(OiosiFaultCode faultCode, OiosiInnerFaultCode innerFaultCode, System.Exception innerException)
            : base(innerException) {
            pFaultCode = faultCode;
            pInnerFaultCode = innerFaultCode;
        }

        /// <summary>
        /// Constructor with faultcode, innerfaultcode, keywords and innerexception
        /// </summary>
        /// <param name="faultCode">rasp message fault code</param>
        /// <param name="innerFaultCode">rasp message innerfault code</param>
        /// <param name="keywords">keywords for the message</param>
        /// <param name="innerException">innerexception of the thrown exception</param>
        public InterceptorChannelException(OiosiFaultCode faultCode, OiosiInnerFaultCode innerFaultCode, System.Collections.Generic.Dictionary<string, string> keywords, System.Exception innerException)
            : base(keywords, innerException) {
            pFaultCode = faultCode;
            pInnerFaultCode = innerFaultCode;
        }
    }
}