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
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.security.lookup {
    /// <summary>
    /// Exception thrown when the certificate loader fails to find a specific certificate.
    /// </summary>
    public class CertificateLoaderCertificateNotFoundException : CertificateNotFoundException {
        /// <summary>
        /// Constructor that takes the store, find type and search string used in the 
        /// search that failed to yield any results.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="findType"></param>
        /// <param name="searchString"></param>
        public CertificateLoaderCertificateNotFoundException(X509Store store, X509FindType findType, string searchString)
            : base(GetKeywords(store, findType, searchString)) {
        }

        private static Dictionary<string, string> GetKeywords(X509Store store, X509FindType findType, string searchString) {
            Dictionary<string, string> keywords = KeywordsFromX509Store.GetKeywords(store);
            KeywordFromString.GetKeyword(keywords, "x509findtype", findType.ToString());
            KeywordFromString.GetKeyword(keywords, "searchstring", searchString);
            return keywords;
        }
    }
}
