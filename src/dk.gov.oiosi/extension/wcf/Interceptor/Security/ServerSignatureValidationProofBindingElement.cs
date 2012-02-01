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
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.extension.wcf.Interceptor.Security.Header;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.security.revocation;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security {
    /// <summary>
    /// Serverside interceptor that attaches proof of signature validations on 
    /// messages send in the system.
    /// </summary>
    public class ServerSignatureValidationProofBindingElement : CommonBindingElement {
        private ServerSignatureValidationProofBindingExtensionElement _configuration;
        private IRevocationLookup revocationLookup;
        private SignatureValidationStackCheck _stackCheck;

        /// <summary>
        /// Constructor that takes the binding element extension for configuration reasons.
        /// </summary>
        /// <param name="configuration"></param>
        public ServerSignatureValidationProofBindingElement(ServerSignatureValidationProofBindingExtensionElement configuration)
        {
            _configuration = configuration;
            RevocationLookupFactory ocspLookupFactory = new RevocationLookupFactory();
            revocationLookup = ocspLookupFactory.CreateRevocationLookupClient();
            _stackCheck = new SignatureValidationStackCheck(GetType());
        }

        #region RaspBindingElement overrides

        /// <summary>
        /// Overrides the method to validate the binding element order.
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            BindingElementCollection bindingElements = context.Binding.Elements;
            _stackCheck.Check(bindingElements);
            return base.BuildChannelListener<TChannel>(context);
        }

        /// <summary>
        /// Intercept requests and creates the proof of validation interception before 
        /// attaching it to the message.
        /// </summary>
        /// <param name="interceptorMessage"></param>
        public override void InterceptRequest(InterceptorMessage interceptorMessage) 
        {
            Headers headers = new Headers(interceptorMessage);
            if (interceptorMessage.IsFault) return;
            if (headers.IsCreateSequence) return;
            if (headers.SequenceHeader == null) return;
            if (headers.SequenceHeader.IsLastMessage) return;

            // Get the certificate from the message
            X509Certificate2 certificate;
            try {
                certificate = interceptorMessage.Certificate;
            }
            catch {
                throw new FailedToGetCertificateSubjectException(interceptorMessage);
            }

            OcesX509Certificate ocesCertificate = new OcesX509Certificate(certificate);
            RevocationCheckStatus status = ocesCertificate.CheckRevocationStatus(revocationLookup);
            
            if (status != RevocationCheckStatus.AllChecksPassed) 
                throw new Exception();
            SignatureValidationProof signatureValidationProof = new SignatureValidationProof(certificate.Subject);
            interceptorMessage.AddProperty(ServerSignatureValidationProofBindingExtensionElement.SignatureValidationProofKey, signatureValidationProof);
        }

        /// <summary>
        /// Intercept response as a stub.
        /// </summary>
        /// <param name="interceptorMessage"></param>
        public override void InterceptResponse(InterceptorMessage interceptorMessage)
        { }

        /// <summary>
        /// Returns whether the request interception should be done. Allways true.
        /// </summary>
        public override bool DoesRequestIntercept 
        {
            get { return true; }
        }

        /// <summary>
        /// Returns whether the response interception should be done. Allways false.
        /// </summary>
        public override bool DoesResponseIntercept 
        {
            get { return false; }
        }

        /// <summary>
        /// Returns whether an exception should be propergated og send a fault.
        /// </summary>
        public override bool DoesFaultOnRequestException 
        {
            get { return _configuration.FaultOnRequestValidationException; }
        }

        /// <summary>
        /// Clones the element.
        /// </summary>
        /// <returns></returns>
        public override BindingElement Clone() 
        {
            return new ServerSignatureValidationProofBindingElement(_configuration);
        }

        #endregion
    }
}
