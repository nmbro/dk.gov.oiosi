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

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels {
    /// <summary>
    /// Collection that contains channel interceptor exceptions throw by
    /// the interceptors.
    /// </summary>
    [OiosiMessageProperty]
    public class InterceptorChannelExceptionCollection {
        private List<InterceptorChannelException> _exceptions;

        /// <summary>
        /// Default constructor that initializes an empty collection.
        /// </summary>
        public InterceptorChannelExceptionCollection() {
            _exceptions = new List<InterceptorChannelException>();
        }

        /// <summary>
        /// Adds an exception to the collection.
        /// </summary>
        /// <param name="exception">the exception to add</param>
        public void Add(InterceptorChannelException exception) {
            _exceptions.Add(exception);
        }

        /// <summary>
        /// Gets an current enumerable copy over the exceptions in the collection.
        /// </summary>
        /// <returns>exception collection</returns>
        public IEnumerable<InterceptorChannelException> GetExceptions() {
            List<InterceptorChannelException> exceptions = new List<InterceptorChannelException>(_exceptions);
            return exceptions;
        }
    }
}