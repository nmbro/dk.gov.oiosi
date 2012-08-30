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
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace dk.gov.oiosi.security.validation
{
    /// <summary>
    /// Custom exception used when chain validating a certificate
    /// </summary>
    class CertificateFailedChainValidationException : CertificateValidationException
    {
        /// <summary>
        /// Constructor with chainstatus as keyword
        /// </summary>
        /// <param name="chainStatus">chainstatus as keyword</param>
        public CertificateFailedChainValidationException(X509ChainStatus chainStatus, string subject)
            : base(GetKeywords(chainStatus, subject)) { }

        /// <summary>
        /// Constructor with status
        /// </summary>
        /// <param name="chainStatusString">chainstatus as string</param>
        public CertificateFailedChainValidationException(string chainStatusString, string subject)
            : base(GetKeywords(chainStatusString, subject)) { }
        
        private static Dictionary<string, string> GetKeywords(X509ChainStatus chainStatus, string subject)
        {
            return GetKeywords(chainStatus.Status.ToString(), subject);
        }

        private static Dictionary<string, string> GetKeywords(string chainStatus, string subject)
        {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            keywords.Add("chainstatus", chainStatus);
            keywords.Add("subject", subject);
            return keywords;
        }
    }
}