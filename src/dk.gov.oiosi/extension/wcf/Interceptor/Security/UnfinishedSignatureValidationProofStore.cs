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
using System.Collections.Generic;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.extension.wcf.Interceptor.Security.Header;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security 
{
    public class UnfinishedSignatureValidationProofStore 
    {
        private ICache<string, UnfinishedSignatureValidationProof> messageIdUnfinishedSignaturesCache;
        private ICache<string, List<UnfinishedSignatureValidationProof>> sequenceIdUnfinishedSignaturesCache;
        private object lockObject = new object();

        public UnfinishedSignatureValidationProofStore()
        {
            this.messageIdUnfinishedSignatures = CacheFactory.Instance.MessageIdUnfinishedSignaturesCache; 
            this.sequenceIdUnfinishedSignatures = CacheFactory.Instance.SequenceIdUnfinishedSignaturesCache;
        }

        public void Add(string messageId, SequenceHeader header, UnfinishedSignatureValidationProof unfinishedSignatureValidationProof) 
        {
            string sequenceId = header.SequenceId;
            List<UnfinishedSignatureValidationProof> sequenceUnfinishedSignatureValidationProofs = null;
            lock (lockObject) {

                // Add the unfinished signature validaton proof to the dictinary using MessageID as key
                this.messageIdUnfinishedSignatures.Remove(messageId);
                this.messageIdUnfinishedSignatures.Add(messageId, unfinishedSignatureValidationProof);

                // Add the unfinished signature validaton proof to the dictinary using SessionID as key
                if (!this.sequenceIdUnfinishedSignatures.TryGetValue(sequenceId, out sequenceUnfinishedSignatureValidationProofs)) 
                {
                    sequenceUnfinishedSignatureValidationProofs = new List<UnfinishedSignatureValidationProof>();
                    this.sequenceIdUnfinishedSignatures.Add(sequenceId, sequenceUnfinishedSignatureValidationProofs);
                }                
            }

            lock (sequenceUnfinishedSignatureValidationProofs) 
            {
                Predicate<UnfinishedSignatureValidationProof> doesSignatureValidationProofForThisMessageAlreadyExist = delegate(UnfinishedSignatureValidationProof usvp) { return (usvp.Headers.MessageId.ToString() == messageId); };
                UnfinishedSignatureValidationProof duplicateSignatureValidationProof = sequenceUnfinishedSignatureValidationProofs.Find(doesSignatureValidationProofForThisMessageAlreadyExist);
                if (duplicateSignatureValidationProof != null)
                {
                    sequenceUnfinishedSignatureValidationProofs.Remove(duplicateSignatureValidationProof);
                }

                sequenceUnfinishedSignatureValidationProofs.Add(unfinishedSignatureValidationProof);
            }
        }

        public void Remove(string messageId, string sequenceId) 
        {
            lock (lockObject)
            {
                this.messageIdUnfinishedSignatures.Remove(messageId);
                this.sequenceIdUnfinishedSignatures.Remove(sequenceId);
            }
        }

        public bool TryGetValueFromMessageId(string messageId, out UnfinishedSignatureValidationProof unfinishedSignatureValidationProof) 
        {
            lock (lockObject)
            {
                return this.messageIdUnfinishedSignatures.TryGetValue(messageId, out unfinishedSignatureValidationProof);
            }
        }

        public bool TryGetValueFromSequenceAcknowledgementHeader(SequenceAcknowledgementHeader header, out List<UnfinishedSignatureValidationProof> unfinishedSignatureValidationProofs) 
        {
            bool result = true; 
            //System.Diagnostics.Debug.WriteLine("SequenceAcknowledgementHeader header, out List<UnfinishedSignatureValidationProof> unfinishedSignatureValidationProofs");
            string sequenceId = header.SequenceId;
            unfinishedSignatureValidationProofs = null;
            List<UnfinishedSignatureValidationProof> sequenceUnfinishedSignatureValidationProofs = null;
            Predicate<UnfinishedSignatureValidationProof> isMessageNumberWithinAckRange = delegate(UnfinishedSignatureValidationProof unfinishedSignatureValidationProof) { return header.IsMessageNumberWithinRange(unfinishedSignatureValidationProof.Headers.SequenceHeader.MessageNumber); };
            lock (lockObject) 
            {
                if (!_sequenceIdUnfinishedSignatures.TryGetValue(sequenceId, out sequenceUnfinishedSignatureValidationProofs))
                {
                    result = false;
                }
            }

            if (sequenceUnfinishedSignatureValidationProofs != null)
            {
                lock (sequenceUnfinishedSignatureValidationProofs)
                {
                    unfinishedSignatureValidationProofs = sequenceUnfinishedSignatureValidationProofs.FindAll(isMessageNumberWithinAckRange);
                }
            }

            return result;
        }
    }
}
