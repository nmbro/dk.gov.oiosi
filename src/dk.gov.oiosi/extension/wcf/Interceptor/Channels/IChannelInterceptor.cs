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
namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels {
    /// <summary>
    /// Interface that describes the methods an interceptor on a reply channel should implement.
    /// There are two methods "InterceptRequest" and "InterceptResponse" that needs to be implemented. 
    /// The first is for incoming communication and the second is for outgoing communication.
    /// </summary>
    public interface IChannelInterceptor {

        /// <summary>
        /// True if the implemented interceptor will handle request interception
        /// </summary>
        bool DoesRequestIntercept { get; }

        /// <summary>
        /// True if the implemented interceptor handles response interception
        /// </summary>
        bool DoesResponseIntercept { get; }

        /// <summary>
        /// Indicates whether an exception should return a fault or added as a custom
        /// property on the message. This is only for listeners.
        /// True if a fault should be returned.
        /// False if the exception is added as a custom property.
        /// </summary>
        bool DoesFaultOnRequestException { get; }

        /// <summary>
        /// Intercepts the message in the request phase. It takes the message before the
        /// intercept and has the resposibility to return a new message if the message
        /// was used.
        /// </summary>
        /// <param name="message">The message that the interceptor should handle</param>
        void InterceptRequest(InterceptorMessage message);

        /// <summary>
        /// Intercepts the message in the response phase. It takes the message before the
        /// intercept and has the responsibility to return a new message if the message
        /// was used.
        /// </summary>
        /// <param name="message">The message that the interceptor should handle</param>
        void InterceptResponse(InterceptorMessage message);
    }
}
