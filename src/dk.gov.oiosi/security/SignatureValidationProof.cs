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
using System.Xml.Serialization;
using dk.gov.oiosi.common;
using dk.gov.oiosi.extension.wcf;

namespace dk.gov.oiosi.security {
    /// <summary>
    /// Contains information about proof of signature validation. This is used to 
    /// verify that a message has been sendt or received.
    /// </summary>
    [OiosiMessageProperty] 
    [XmlRoot(ElementName = "ProofOfSignatureValidationStructure", Namespace = Definitions.DefaultOiosiNamespace2007)]
    public class SignatureValidationProof : ISignatureValidationProof {
        private DateTime _timeStamp;
        private string _certificateSubject="";
        private bool _validCertificate;
        private bool _validSignature;
        private bool _unchangedMessage;
        private bool _encryptedMessage;
        private bool _completed;

        

        /// <summary>
        /// public default constructor.
        /// </summary>
        public SignatureValidationProof() { }
        /// <summary>
        /// Constructor that creates a new proof that is completed now for the given
        /// certificate subject.
        /// </summary>
        /// <param name="certificateSubject"></param>
        public SignatureValidationProof(string certificateSubject) {
            _timeStamp = DateTime.Now;
            _certificateSubject = certificateSubject;
            _validCertificate = true;
            _validSignature = true;
            _unchangedMessage = true;
            _encryptedMessage = true;
            //Set that the signature validation proof is completed
            _completed = true;
        }
        /// <summary>
        /// Completes a proof of signature validation.
        /// Throws an exception if the validation has been completed earlier.
        /// </summary>
        /// <param name="certificateSubject"></param>
        public void CompleteValidation(string certificateSubject) {
            if (_completed)
                throw new SignatureValidationProofAllreadyCompletedException(certificateSubject);
            _timeStamp = DateTime.Now;
            _certificateSubject = certificateSubject;
            _validCertificate = true;
            _validSignature = true;
            _unchangedMessage = true;
            _encryptedMessage = true;
            //Set that the signature validation proof is completed
            _completed = true;
        }

        /// <summary>
        /// Validates if the signature validation proof has been set.
        /// </summary>
        public void SetCompleted() {
            _completed = true;
        }

        #region ISignatureValidationProof Members

        /// <summary>
        /// Is the signature validation proof already complete?
        /// </summary>
        public bool Completed {
            get { return _completed; }
        }

        /// <summary>
        /// Gets and sets the timestamp the signature validation proof was done.
        /// </summary>
        [XmlElement(ElementName="SignatureValidationCETDateTime")]
        public DateTime TimeStamp {
            get { return _timeStamp; }
            set {
                if (_completed) throw new SignatureValidationProofAllreadyCompletedException(_certificateSubject);
                _timeStamp = value; 
            }
        }
        /// <summary>
        /// Gets and sets the certificate subject.
        /// </summary>
        [XmlElement(ElementName = "SigningCertificateSubject")]
        public string CertificateSubject {
            get { return _certificateSubject; }
            set {
                if (_completed) throw new SignatureValidationProofAllreadyCompletedException(_certificateSubject);
                _certificateSubject = value;
            }
        }
        /// <summary>
        /// Gets and sets the certificate validation.
        /// </summary>
        [XmlElement(ElementName = "CertificateValidIndicator")]
        public bool ValidCertificate {
            get { return _validCertificate; }
            set {
                if (_completed) throw new SignatureValidationProofAllreadyCompletedException(_certificateSubject);
                _validCertificate = value;
            }
        }
        /// <summary>
        /// Gets and sets the signature valid flag.
        /// </summary>
        [XmlElement(ElementName = "SignatureValidIndicator")]
        public bool ValidSignature {
            get { return _validSignature; }
            set {
                if (_completed) throw new SignatureValidationProofAllreadyCompletedException(_certificateSubject);
                _validSignature = value;
            }
        }
        /// <summary>
        /// Gets and sets the message unchanged flag.
        /// </summary>
        [XmlElement(ElementName = "MessageUnchangedIndicator")]
        public bool UnchangedMessage {
            get { return _unchangedMessage; }
            set {
                if (_completed) throw new SignatureValidationProofAllreadyCompletedException(_certificateSubject);
                _unchangedMessage = value;
            }
        }
        /// <summary>
        /// Gets and sets the encrypted message flag.
        /// </summary>
        [XmlElement(ElementName = "MessageEncryptedIndicator")]
        public bool EncryptedMessage {
            get { return _encryptedMessage; }
            set {
                if (_completed) throw new SignatureValidationProofAllreadyCompletedException(_certificateSubject);
                _encryptedMessage = value;
            }
        }
        #endregion
    }
}
