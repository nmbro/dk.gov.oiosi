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
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.extension.wcf.Interceptor.Security.Header;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security {
    class UnfinishedSignatureValidationProofCache {
        private TimedCache<string, UnfinishedSignatureValidationProof> _messageIdUnfinishedSignatures;
        private TimedCache<string, List<UnfinishedSignatureValidationProof>> _sequenceIdUnfinishedSignatures;
        private object lockObject = new object();

        public UnfinishedSignatureValidationProofCache(TimeSpan timeOut) {
            _messageIdUnfinishedSignatures = new TimedCache<string, UnfinishedSignatureValidationProof>(timeOut);
            _sequenceIdUnfinishedSignatures = new TimedCache<string, List<UnfinishedSignatureValidationProof>>(timeOut);
        }

        public void Add(string messageId, SequenceHeader header, UnfinishedSignatureValidationProof unfinishedSignatureValidationProof) {
            string sequenceId = header.SequenceId;
            List<UnfinishedSignatureValidationProof> sequenceUnfinishedSignatureValidationProofs = null;
            lock (lockObject) {

                // Add the unfinished signature validaton proof to the dictinary using MessageID as key
                _messageIdUnfinishedSignatures.Remove(messageId);
                _messageIdUnfinishedSignatures.Add(messageId, unfinishedSignatureValidationProof);

                // Add the unfinished signature validaton proof to the dictinary using SessionID as key
                if (!_sequenceIdUnfinishedSignatures.TryGetValue(sequenceId, out sequenceUnfinishedSignatureValidationProofs)) {
                    sequenceUnfinishedSignatureValidationProofs = new List<UnfinishedSignatureValidationProof>();
                    _sequenceIdUnfinishedSignatures.Add(sequenceId, sequenceUnfinishedSignatureValidationProofs);
                }
                
            }
            lock (sequenceUnfinishedSignatureValidationProofs) {
                Predicate<UnfinishedSignatureValidationProof> doesSignatureValidationProofForThisMessageAlreadyExist = delegate(UnfinishedSignatureValidationProof usvp) { return (usvp.Headers.MessageId.ToString() == messageId); };
                UnfinishedSignatureValidationProof duplicateSignatureValidationProof = sequenceUnfinishedSignatureValidationProofs.Find(doesSignatureValidationProofForThisMessageAlreadyExist);
                if (duplicateSignatureValidationProof != null)
                    sequenceUnfinishedSignatureValidationProofs.Remove(duplicateSignatureValidationProof);

                sequenceUnfinishedSignatureValidationProofs.Add(unfinishedSignatureValidationProof);
            }
        }

        public void Remove(string messageId, string sequenceId) {
            lock (lockObject) {
                _messageIdUnfinishedSignatures.Remove(messageId);
                _sequenceIdUnfinishedSignatures.Remove(sequenceId);
            }
        }

        public bool TryGetValueFromMessageId(string messageId, out UnfinishedSignatureValidationProof unfinishedSignatureValidationProof) {
            lock (lockObject) {
                return _messageIdUnfinishedSignatures.TryGetValue(messageId, out unfinishedSignatureValidationProof);
            }
        }

        public bool TryGetValueFromSequenceAcknowledgementHeader(SequenceAcknowledgementHeader header, out List<UnfinishedSignatureValidationProof> unfinishedSignatureValidationProofs) {
            //System.Diagnostics.Debug.WriteLine("SequenceAcknowledgementHeader header, out List<UnfinishedSignatureValidationProof> unfinishedSignatureValidationProofs");
            string sequenceId = header.SequenceId;
            unfinishedSignatureValidationProofs = null;
            List<UnfinishedSignatureValidationProof> sequenceUnfinishedSignatureValidationProofs = null;
            Predicate<UnfinishedSignatureValidationProof> isMessageNumberWithinAckRange = delegate(UnfinishedSignatureValidationProof unfinishedSignatureValidationProof) { return header.IsMessageNumberWithinRange(unfinishedSignatureValidationProof.Headers.SequenceHeader.MessageNumber); };
            lock (lockObject) {
                if (!_sequenceIdUnfinishedSignatures.TryGetValue(sequenceId, out sequenceUnfinishedSignatureValidationProofs))
                    return false;
            }
            lock (sequenceUnfinishedSignatureValidationProofs) {
                unfinishedSignatureValidationProofs = sequenceUnfinishedSignatureValidationProofs.FindAll(isMessageNumberWithinAckRange);
            }
            return true;
        }
    }
}
