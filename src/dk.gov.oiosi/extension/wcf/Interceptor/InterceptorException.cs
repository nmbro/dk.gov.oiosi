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
using System.Resources;
using System.ServiceModel;
using dk.gov.oiosi.exception.MessageStore;

namespace dk.gov.oiosi.extension.wcf.Interceptor {

    /// <summary>
    /// Exception used by the custom implemented interceptors to propaget
    /// communication exceptions up the stack while using the exception 
    /// message pattern.
    /// </summary>
    public class InterceptorException : CommunicationException {
        private static List<ResourceManager> resources = new List<ResourceManager>();
        private static ResourceManager resourceManager = new ResourceManager(typeof(ErrorMessages));
        private IExceptionMessageStore exceptionMessageStore = new ResourceFileExceptionMessageStore();
        private string _message;
        /// <summary>
        /// Standard default constructor, gives the base constructor the resource manager as 
        /// parameter.
        /// </summary>
        public InterceptorException() {
            SetMessage(resourceManager);
        }

        /// <summary>
        /// Standard constructor that takes a dictionary with keywords as parameter and calls
        /// a base constructor with the keywords and the resource manager.
        /// </summary>
        /// <param name="keywords">A dictionary that contains keywords that are used in building the exception message</param>
        public InterceptorException(System.Collections.Generic.Dictionary<string, string> keywords) {
            SetMessage(resourceManager, keywords);
        }

        /// <summary>
        /// Standard constructor that takes an exception that is the inner exception as 
        /// parameter and calls the base constructor with both the inner exception and the 
        /// resource manager.
        /// </summary>
        /// <param name="innerException">The inner exception of this exception</param>
        public InterceptorException(System.Exception innerException) : base("", innerException) {
            SetMessage(resourceManager);
        }

        /// <summary>
        /// Standard constructor that takes a dictionary with keywords and an exception 
        /// that is the inner exception. Then it calls a base constructor with the keywords,
        /// the inner exception and the resource manager.
        /// </summary>
        /// <param name="keywords">A dictionary that contains keywords that are used in building the exception message</param>
        /// <param name="innerException">The inner exception of this exception</param>
        public InterceptorException(Dictionary<string, string> keywords, System.Exception innerException) : base("", innerException) {
            SetMessage(resourceManager, keywords);
        }

        /// <summary>
        /// Property to get the error message
        /// </summary>
        public override string Message {
            get { return _message; }
        }

        private void SetMessage(ResourceManager resource) {
            Dictionary<string, string> keywords = new Dictionary<string,string>();
            SetMessage(resource, keywords);
        }

        private void SetMessage(ResourceManager resource, Dictionary<string, string> keywords) {
            Type exceptionType = this.GetType();
            List<ResourceManager> collectiveResources = new List<ResourceManager>(resources);
            collectiveResources.Add(resource);
            _message = exceptionMessageStore.GetExceptionMessage(collectiveResources, exceptionType, keywords);
        }

    }
}