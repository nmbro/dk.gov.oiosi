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
namespace dk.gov.oiosi.security {
    
    /// <summary>
    /// Status of a check of an x509 certificate
    /// </summary>
    public enum X509CheckStatus {
        /// <summary>
        /// The check was not performed
        /// </summary>
        NotChecked,
        /// <summary>
        /// All checks passed
        /// </summary>
        AllChecksPassed,
        /// <summary>
        /// A certificate chain issue was found
        /// </summary>
        ChainIssue,
        /// <summary>
        /// The certificate failed the validity issue check
        /// </summary>
        ValidityPeriodIssue,
        /// <summary>
        /// The certificate failed the chain- and validity issue check
        /// </summary>
        ChainAndValidityPeriodIssue,
        /// <summary>
        /// An unknown issue caused the check to fail
        /// </summary>
        UnknownIssue
    }
}
