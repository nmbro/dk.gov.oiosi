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
using System.ServiceModel.Channels;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.extension.wcf.Interceptor.Security.Header;
using dk.gov.oiosi.security;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security {
    /// <summary>
    /// Clientside interceptor that attaches proof of signature validations on 
    /// messages send in the system.
    /// </summary>
    public class ClientSignatureValidationProofBindingElement : CommonBindingElement 
    {
        private UnfinishedSignatureValidationProofStore unfinishedSignaturesValidationProofStore;
        private SignatureValidationStackCheck signatureValidationStackCheck;
        private static object signatureLock = new object();

        /*static ClientSignatureValidationProofBindingElement() 
        {
            unfinishedSignaturesValidationProofStore = new UnfinishedSignatureValidationProofStore();
        }*/

        /// <summary>
        /// Default constructor that initializes the binding elements dependent components.
        /// </summary>
        public ClientSignatureValidationProofBindingElement() 
        {
            this.signatureValidationStackCheck = new SignatureValidationStackCheck(GetType());
            this.unfinishedSignaturesValidationProofStore = new UnfinishedSignatureValidationProofStore();
        }

        #region RaspBindingElement overrides

        /// <summary>
        /// Overrides the method to validate the binding element order.
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context) 
        {
            BindingElementCollection bindingElements = context.Binding.Elements;
            this.signatureValidationStackCheck.Check(bindingElements);
            return base.BuildChannelFactory<TChannel>(context);
        }

        /// <summary>
        /// Clones the client signature proof binding element.
        /// </summary>
        /// <returns></returns>
        public override BindingElement Clone() 
        {
            return new ClientSignatureValidationProofBindingElement();
        }

        /// <summary>
        /// Returns whether the interceptor intercepts requests. This is 
        /// allways true.
        /// </summary>
        public override bool DoesRequestIntercept 
        {
            get 
            {
                return true;
            }
        }

        /// <summary>
        /// Returns whether the interceptor intercepts responses. This is
        /// allways true.
        /// </summary>
        public override bool DoesResponseIntercept 
        {
            get 
            { 
                return true; 
            }
        }

        /// <summary>
        /// Returns whether a fault should be returned if the interceptor throws an
        /// exception. This is false for client side interceptors.
        /// </summary>
        public override bool DoesFaultOnRequestException
        {
            get 
            { 
                return false; 
            }
        }

        /// <summary>
        /// Intercepts the request, storing the unfinished signatures.
        /// </summary>
        /// <param name="message"></param>
        public override void InterceptRequest(InterceptorMessage message) 
        {
            try {
                Headers headers = new Headers(message);
                SequenceHeader sequenceHeader = headers.SequenceHeader;
                string messageId = string.Empty;
                if (message.IsFault)
                {
                    // ? nothing to do??
                }
                else if (sequenceHeader == null)
                {
                    // ? nothing to do??
                }
                else if (sequenceHeader.IsLastMessage)
                {
                    // ? nothing to do??
                }
                else
                {
                    messageId = headers.MessageId.ToString();
                    UnfinishedSignatureValidationProof unfinishedSignature = new UnfinishedSignatureValidationProof(headers);

                    //System.Diagnostics.Debug.WriteLine("\n\n\nAdding unfinished signature - id:" + messageId + ";unfinished signature validation proof: " + unfinishedSignature.SignatureValidationProof + "\n\n\n");
                    this.unfinishedSignaturesValidationProofStore.Add(messageId, sequenceHeader, unfinishedSignature);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Intercepts the response, finishing the signatures and adds it to the
        /// message.
        /// </summary>
        /// <param name="message"></param>
        public override void InterceptResponse(InterceptorMessage message) 
        {
            try 
            {
                //System.Diagnostics.Debug.Write("Intercepting the response - ");
                Headers headers = new Headers(message);
                SequenceAcknowledgementHeader sequenceAcknowledgement = headers.SequenceAcknowledgement;
                if (message.IsFault)
                {
                }
                else if (headers.SequenceHeader == null)
                {
                }
                else if (headers.RelatesTo != null)
                {
                    this.InterceptedMessageResponse(message, headers);
                }

                if (sequenceAcknowledgement != null)
                {
                    this.InterceptedAcknowledgementResponse(message, headers);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void InterceptedMessageResponse(InterceptorMessage message, Headers headers) {
            lock (signatureLock)
            {
                SequenceHeader sequenceHeader = headers.SequenceHeader;
                string relatesTo = headers.RelatesTo.ToString();

                // Try to get the unfinished signature validation proof. If none exists return.
                UnfinishedSignatureValidationProof unfinishedSignatureValidationProof = null;
                if (this.unfinishedSignaturesValidationProofStore.TryGetValueFromMessageId(relatesTo, out unfinishedSignatureValidationProof))
                {
                    // validation proof retrived
                    //System.Diagnostics.Debug.WriteLine("InterceptedMessageResponse relatesTo " + relatesTo);
                    SignatureValidationProof signatureValidationProof = unfinishedSignatureValidationProof.SignatureValidationProof;
                    string signatureValidationProofKey = ClientSignatureValidationProofBindingExtensionElement.SignatureValidationProofKey;
                    message.AddProperty(signatureValidationProofKey, signatureValidationProof);
                }
            }
        }

        private void InterceptedAcknowledgementResponse(InterceptorMessage message, Headers headers) 
        {
            lock (signatureLock)
            {
                SequenceAcknowledgementHeader sequenceAcknowledgementHeader = headers.SequenceAcknowledgement;
                string identityName = message.Properties.Security.ServiceSecurityContext.PrimaryIdentity.Name;
                int index = identityName.LastIndexOf(';');
                string certificateSubject = identityName.Substring(0, index);
                // Try to get the messages that have been acked in the RM session. If none exists return.
                List<UnfinishedSignatureValidationProof> ackedMessages = null;
                //System.Diagnostics.Debug.WriteLine("InterceptedAcknowledgementResponse sequenceID" + sequenceAcknowledgementHeader.SequenceId);
                if (this.unfinishedSignaturesValidationProofStore.TryGetValueFromSequenceAcknowledgementHeader(sequenceAcknowledgementHeader, out ackedMessages))
                {
                    // message
                    foreach (UnfinishedSignatureValidationProof ackedMessage in ackedMessages)
                    {
                        SignatureValidationProof signatureValidationProof = ackedMessage.SignatureValidationProof;
                        if (!signatureValidationProof.Completed)
                            signatureValidationProof.CompleteValidation(certificateSubject);
                    }
                }
            }
        }
    }
}
