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
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.extension.wcf.Interceptor.Security.Header;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.logging;
using dk.gov.oiosi.security.revocation.ocsp;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security {
    /// <summary>
    /// Serverside interceptor that attaches proof of signature validations on 
    /// messages send in the system.
    /// Validate if a certificate has been revoked. It does NOT validate activation, expired or trusted root - for this look at CertificateValidatorWithLookup.
    /// </summary>
    public class ServerSignatureValidationProofBindingElement : dk.gov.oiosi.extension.wcf.Interceptor.Validation.ValidationServerBindingElement //CommonBindingElement 
    {
        private ILogger logger;
        //private ServerSignatureValidationProofBindingExtensionElement configuration;
        private IRevocationLookup revocationLookup;

        // Why use SignatureValidationStackCheck ... ?
        // If used as designed, it will make the endpoint stop, if the interceptor throws an error        
        // private SignatureValidationStackCheck stackCheck;

        /// <summary>
        /// Constructor that takes the binding element extension for configuration reasons.
        /// </summary>
        /// <param name="configuration"></param>
        public ServerSignatureValidationProofBindingElement(dk.gov.oiosi.extension.wcf.Interceptor.Validation.ValidationServerConfiguration configuration) // (ServerSignatureValidationProofBindingExtensionElement configuration)
            : base(configuration)
        {
            this.logger = LoggerFactory.Create(this);
            RevocationLookupFactory ocspLookupFactory = new RevocationLookupFactory();
            this.revocationLookup = ocspLookupFactory.CreateRevocationLookupClient();
            //this.stackCheck = new SignatureValidationStackCheck(GetType());
        }

        #region RaspBindingElement overrides

       /* /// <summary>
        /// Overrides the method to validate the binding element order.
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            BindingElementCollection bindingElements = context.Binding.Elements;
            this.stackCheck.Check(bindingElements);
            return base.BuildChannelListener<TChannel>(context);
        }*/

        /// <summary>
        /// Intercept requests and creates the proof of validation interception before 
        /// attaching it to the message.
        /// </summary>
        /// <param name="interceptorMessage"></param>
        public override void InterceptRequest(InterceptorMessage interceptorMessage)
        {
            this.logger.Trace("Security validate the foces certificate.");
            try
            {
                Headers headers = new Headers(interceptorMessage);
                if (interceptorMessage.IsFault)
                {
                    // do nothing
                }
                else if (headers.IsCreateSequence)
                {
                    // do nothing
                }
                else if (headers.SequenceHeader == null)
                {
                    // do nothing
                }
                else if (headers.SequenceHeader.IsLastMessage)
                {
                    // do nothing
                }
                else
                {
                    // Get the certificate from the message
                    X509Certificate2 certificate;
                    try
                    {
                        certificate = interceptorMessage.Certificate;
                    }
                    catch
                    {
                        throw new FailedToGetCertificateSubjectException(interceptorMessage);
                    }

                    

                    // now we must revocate check the certificate
                    OcesX509Certificate ocesCertificate = new OcesX509Certificate(certificate);
                    RevocationResponse response = ocesCertificate.CheckRevocationStatus(revocationLookup);

                    if (response.Exception != null)
                    {
                        string msg;
                        try
                        {
                            msg = response.Exception.Message;
                        }
                        catch (Exception ex)
                        {
                            this.logger.Debug("Error finding the correct error message.", ex);
                            msg = "unknown";
                        }

                        this.logger.Warn(string.Format("The certificate '{0}' revocation check failed. Reason is: ", ocesCertificate.Certificate.SubjectName.Name, msg));

                        // some error checking the certificate
                        // make sure the error is of the correct type, and throw it
                        // note - if the original exception was not a communikation exception, it is wraped in a communikation exception
                        if (response.Exception is CertificateRevokedTimeoutException)
                        {
                            throw new CertificateRevokedValidationFailedException(response.Exception);
                        }
                        else if (response.Exception is CertificateRevokedException)
                        {
                            throw response.Exception;
                        }
                        else if (response.Exception is CertificateRevokedValidationFailedException)
                        {
                            throw response.Exception;
                        }
                        else if (response.Exception is CheckCertificateOcspUnexpectedException)
                        {
                            throw new CertificateRevokedValidationFailedException(response.Exception);
                        }
                        else if (response.Exception is CheckCertificateRevokedUnexpectedException)
                        {
                            throw new CertificateRevokedValidationFailedException(response.Exception);
                        }
                        else
                        {
                            throw new CertificateRevokedValidationFailedException(response.Exception);
                        }
                    }
                    else
                    {
                        // no exception - all good so far

                        switch (response.RevocationCheckStatus)
                        {
                            case RevocationCheckStatus.AllChecksPassed:
                                {
                                    // all good
                                    SignatureValidationProof signatureValidationProof = new SignatureValidationProof(certificate.Subject);
                                    interceptorMessage.AddProperty(ServerSignatureValidationProofBindingExtensionElement.SignatureValidationProofKey, signatureValidationProof);
                                    break;
                                }
                            case RevocationCheckStatus.CertificateRevoked:
                                {
                                    this.logger.Warn(string.Format("The certificate '{0}' is revoked.", ocesCertificate.Certificate.SubjectName.Name));
                                    throw new CertificateRevokedException();
                                    //break;
                                }
                            default:
                                {
                                    this.logger.Warn(string.Format("The certificate '{0}' failed in revocation check - reason unknown", ocesCertificate.Certificate.SubjectName.Name));
                                    throw new CertificateRevokedValidationFailedException("The certificate failed in revocation check - reason unknown.");
                                    //break;
                                }
                        }
                    }
                }
            }
            catch (FailedToGetCertificateSubjectException)
            {
                // exception is of the correct type - rethrowing it
                throw;
            }
           /* catch (CertificateRevokedTimeoutException e)
            {
                // exception is of the correct type - rethrowing it
                throw new CertificateRevokedValidationFailedException(e);
            }*/
            catch (CertificateRevokedException)
            {
                // exception is of the correct type - rethrowing it
                throw;
            }
            catch (CertificateRevokedValidationFailedException)
            {
                // exception is of the correct type - rethrowing it
                throw;
            }

          /*  catch (CheckCertificateOcspUnexpectedException)
            {
                // exception is of the correct type - rethrowing it
                throw;
            }*/
           /* catch (CheckCertificateRevokedUnexpectedException)
            {
                // exception is of the correct type - rethrowing it
                throw;
            }*/
            catch (Exception ex)
            {
                this.logger.Debug("Security validate the foces certificate", ex);
                throw new CertificateRevokedValidationFailedException(ex);
            }

            this.logger.Trace("Security validate the foces certificate - Finish.");
        }

        /// <summary>
        /// Intercept response as a stub.
        /// </summary>
        /// <param name="interceptorMessage"></param>
        public override void InterceptResponse(InterceptorMessage interceptorMessage)
        { }

        /*/// <summary>
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
            get { return this.configuration.FaultOnRequestValidationException; }
        }*/

       /* /// <summary>
        /// Clones the element.
        /// </summary>
        /// <returns></returns>
        public override BindingElement Clone() 
        {
            return new ServerSignatureValidationProofBindingElement(this.configuration);
        }*/

        /// <summary>
        /// Clones the element.
        /// </summary>
        /// <returns></returns>
        public override BindingElement Clone()
        {
            return new ServerSignatureValidationProofBindingElement(this.ValidationServerConfiguration);
        }

        #endregion
    }
}
