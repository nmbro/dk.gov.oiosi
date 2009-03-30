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
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace dk.gov.oiosi.exception.Keyword {
    
    /// <summary>
    /// Gets keywords from an X509Certificate2 and puts them into a
    /// dictionary.
    /// </summary>
    public class KeywordsFromX509Certificate2 {

        /// <summary>
        /// Gets the keywords and return them in a new dictionary.
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeywords(X509Certificate2 certificate) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeywords(keywords, certificate);
            return keywords;
        }

        /// <summary>
        /// Adds the keywords to the given keyword dictionary
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="certificate"></param>
        public static void GetKeywords(Dictionary<string, string> keywords, X509Certificate2 certificate) {
            keywords.Add("certificatefriendlyname", certificate.FriendlyName);
            keywords.Add("certificatesubject", certificate.Subject);
            keywords.Add("certificateserialnumber", certificate.SerialNumber);
            keywords.Add("certificateissuer", certificate.Issuer);
        }
    }
}
