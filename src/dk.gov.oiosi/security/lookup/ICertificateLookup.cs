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
using System.Text;
using System.Security.Cryptography.X509Certificates;
using dk.gov.oiosi.security;

namespace dk.gov.oiosi.security.lookup
{
    /// <summary>
    /// Describes an interface where it is possible to get a certificate from the subject string of
    /// the certifacte.
    /// </summary>
    public interface ICertificateLookup
    {
        /// <summary>
        /// Gets a certificate from the subject string and returns a X509Certficate2 object of the
        /// certificate found.
        /// 
        /// All exceptions are rooted with the CertificateLookupException
        /// 
        /// Several things can go wrong in the search and these are represented as the following exceptions
        /// CertificateNotFoundException
        /// CertificateValidationFailedException
        /// MultipleCertificateFoundException
        /// SearchFailedException
        /// </summary>
        /// <param name="subjectSerialNumber">The subject serial number of the certificate wished</param>
        /// <returns>The certificate with the subject given as parameter</returns>
        X509Certificate2 GetCertificate(CertificateSubject subjectSerialNumber);
    }
}
