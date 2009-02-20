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
  *   Dennis S�gaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */

using System.Collections.Generic;
using System.Resources;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.security.lookup {
    /// <summary>
    /// The main exception of the Certificate Lookup module. There will never be created an 
    /// instance of this exception, but all exceptions in the module inherits from this 
    /// exception.
    /// 
    /// The exception inherits the MainException from the ExceptionHandeling module. This is
    /// to implement that the exception messages are stored in the ErrorMessage.resx file of 
    /// this module.
    /// </summary>
    public abstract class CertificateLookupException : MainException {
        private static ResourceManager resourceManager = new ResourceManager(typeof(ErrorMessages));

        /// <summary>
        /// Standard default constructor, gives the base constructor the resource manager as 
        /// parameter.
        /// </summary>
        public CertificateLookupException() : base(resourceManager) { }

        /// <summary>
        /// Standard constructor that takes a dictionary with keywords as parameter and calls
        /// a base constructor with the keywords and the resource manager.
        /// </summary>
        /// <param name="keywords">A dictionary that contains keywords that are used in building the exception message</param>
        public CertificateLookupException(System.Collections.Generic.Dictionary<string, string> keywords) : base(resourceManager, keywords) { }

        /// <summary>
        /// Standard constructor that takes an exception that is the inner exception as 
        /// parameter and calls the base constructor with both the inner exception and the 
        /// resource manager.
        /// </summary>
        /// <param name="innerException">The inner exception of this exception</param>
        public CertificateLookupException(System.Exception innerException) : base(resourceManager, innerException) { }

        /// <summary>
        /// Standard constructor that takes a dictionary with keywords and an exception 
        /// that is the inner exception. Then it calls a base constructor with the keywords,
        /// the inner exception and the resource manager.
        /// </summary>
        /// <param name="keywords">A dictionary that contains keywords that are used in building the exception message</param>
        /// <param name="innerException">The inner exception of this exception</param>
        public CertificateLookupException(System.Collections.Generic.Dictionary<string, string> keywords, System.Exception innerException) : base(resourceManager, keywords, innerException) { }

        /// <summary>
        /// Constructor that takes a resources manager and a dictionary of keywords as parameter.
        /// </summary>
        /// <param name="resourceManager"></param>
        /// <param name="keywords"></param>
        public CertificateLookupException(ResourceManager resourceManager, Dictionary<string, string> keywords) : base(resourceManager, keywords) { }
    }
}
