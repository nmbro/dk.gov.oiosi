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
  *   Jacob Mogensen, mySupply ApS
  *   Jens Madsen, Comcare
  */

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.schema;
using dk.gov.oiosi.logging;
using dk.gov.oiosi.extension.wcf.Interceptor.Channels;
using dk.gov.oiosi.extension.wcf.Interceptor.Security.Header;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.extension.wcf.Interceptor.Security;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.validation;
using System.IdentityModel.Tokens;
using dk.gov.oiosi.communication.fault;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Certificate
{

    /// <summary>
    /// Schema validation with lookup
    /// </summary>
    public class CertificateValidatorWithLookup
    {
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// The revocation lookup client
        /// </summary>
        private MultipleRootX509CertificateValidator validator;

        /// <summary>
        /// Constructor
        /// </summary>
        public CertificateValidatorWithLookup()
        {

            this.logger = LoggerFactory.Create(this.GetType());
            this.validator = new MultipleRootX509CertificateValidator();
        }

        /// <summary>
        /// Certificate validator
        /// </summary>
        /// <param name="interceptorMessage">InterceptorMessage to validate</param>
        public void Validate(InterceptorMessage interceptorMessage)
        {
            this.logger.Trace("Certificate validation.");

            // Get the certificate from the message
            X509Certificate2 certificate;
            try
            {
                certificate = interceptorMessage.Certificate;

                if (certificate == null)
                {
                    throw new FailedToGetCertificateSubjectException(interceptorMessage);
                }

                this.validator.Validate(certificate);

            }
            catch (CertificateExpiredException ex)
            {
                throw new InterceptorChannelWrapperException(OiosiFaultCode.Sender, OiosiInnerFaultCode.SignatureNotValidFault, ex);
            }
            catch (CertificateNotActiveException ex)
            {
                throw new InterceptorChannelWrapperException(OiosiFaultCode.Sender, OiosiInnerFaultCode.SignatureNotValidFault, ex);
            }
            catch (CertificateRootNotTrustedException ex)
            {
                throw new InterceptorChannelWrapperException(OiosiFaultCode.Sender, OiosiInnerFaultCode.SignatureNotValidFault, ex);
            }
            catch (FailedToGetCertificateSubjectException)
            {
                throw;
            }
            catch (Exception ex)
            {
                this.logger.Debug("Security validate the foces certificate", ex);
                throw new InterceptorChannelWrapperException(OiosiFaultCode.Receiver, OiosiInnerFaultCode.InternalSystemFailureFault, "The client certificate is invalid.");
            }

            this.logger.Trace("Security validate the foces certificate - Finish.");

        }
    }
}