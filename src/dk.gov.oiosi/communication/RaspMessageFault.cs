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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace dk.gov.oiosi.communication {

    /// <summary>
    /// Represents a rasp message fault
    /// </summary>
    public class RaspMessageFault : MessageFault {

        private FaultCode _code;
        private FaultReason _reason;

        /// <summary>
        /// enum representing faultcode
        /// </summary>
        public enum RaspFaultCode { 
            /// <summary>
            /// Sender fault source
            /// </summary>
            Sender, 
            /// <summary>
            /// Reciever fualt source
            /// </summary>
            Reciever 
        };
        
        /// <summary>
        /// enum representing inner faultcode
        /// </summary>
        public enum RaspInnerFaultCode {
            /// <summary>
            /// 
            /// </summary>
            SchemaValidationFault, 
            /// <summary>
            /// 
            /// </summary>
            SchematronValidationFault, 
            /// <summary>
            /// 
            /// </summary>
            SignatureNotValidFault, 
            /// <summary>
            /// 
            /// </summary>
            UnknownDocumentTypeFault, 
            /// <summary>
            /// 
            /// </summary>
            MessagePersistencyFault, 
            /// <summary>
            /// 
            /// </summary>
            XsltTransformationFault, 
            /// <summary>
            /// 
            /// </summary>
            InternalSystemFailureFault,
            /// <summary>
            /// 
            /// </summary>
            MissingHeaderFault
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="e">exception</param>
        /// <param name="faultCode">fault code</param>
        /// <param name="innerFaultCode">inner fault code</param>
        public RaspMessageFault(Exception e, RaspFaultCode faultCode, RaspInnerFaultCode innerFaultCode) {
            StringBuilder reasonMessage = new StringBuilder();
            Exception currentException = e;
            do {
                reasonMessage.Append(currentException.Message);
                if (currentException.InnerException != null) 
                    reasonMessage.Append("\n");
                currentException = currentException.InnerException;
            } while (currentException != null);
            _reason = new FaultReason(reasonMessage.ToString());
            _code = CreateFaultCode(faultCode, innerFaultCode);
        }

        #region MessageFault overrides
        
        /// <summary>
        /// Property for faultcode
        /// </summary>
        public override FaultCode Code {
            get { return _code; }
        }

        /// <summary>
        /// Propery for HasDetail
        /// </summary>
        public override bool HasDetail {
            get { return false; }
        }

        /// <summary>
        /// OnWriteDetailContents event
        /// </summary>
        /// <param name="writer"></param>
        protected override void OnWriteDetailContents(System.Xml.XmlDictionaryWriter writer) { }

        /// <summary>
        /// Property for fault reason
        /// </summary>
        public override System.ServiceModel.FaultReason Reason {
            get { return _reason; }
        }

        #endregion

        private FaultCode CreateFaultCode(RaspFaultCode faultCode, RaspInnerFaultCode innerFaultCode) {
            switch (faultCode) {
                case RaspFaultCode.Sender:
                    FaultCode senderInnerFaultCode = CreateSenderFaultCode(innerFaultCode);
                    return FaultCode.CreateSenderFaultCode(senderInnerFaultCode);
                case RaspFaultCode.Reciever:
                    FaultCode recieverInnerFaultCode = CreateRecieverFaultCode(innerFaultCode);
                    return FaultCode.CreateReceiverFaultCode(recieverInnerFaultCode);
                default:
                    throw new RaspMessageFaultUnexpectedFaultCode(faultCode);
            }
        }

        private FaultCode CreateSenderFaultCode(RaspInnerFaultCode innerFaultCode) {
            return new FaultCode(innerFaultCode.ToString());
        }

        private FaultCode CreateRecieverFaultCode(RaspInnerFaultCode innerFaultCode) {
            return new FaultCode(innerFaultCode.ToString());
        }
    }
}