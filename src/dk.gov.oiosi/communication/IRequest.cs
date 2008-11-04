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
namespace dk.gov.oiosi.communication {

    interface IRequest {

        #region Methods

        /// <summary>
        /// Synchronously sends a request and gets a response
        /// </summary>
        /// <param name="message">Request message</param>
        /// <returns>Response message</returns>
        Response GetResponse (OiosiMessage message);

        /// <summary>
        /// Asynchronously starts sending a request
        /// </summary>
        /// <param name="message">Request message</param>
        /// <param name="callback">Callback delegate</param>
        IAsyncResult BeginGetResponse (OiosiMessage message, AsyncCallback callback);
       
        /// <summary>
        /// Asynchronously ends sending a request
        /// </summary>
        /// <returns>Response message</returns>
        Response EndGetResponse (IAsyncResult asyncResult);


        /// <summary>
        /// Shut-down
        /// </summary>
        void Close();

        /// <summary>
        /// Hard shut-down
        /// </summary>
        void Abort();

        #endregion

        #region Properties

        /// <summary>
        /// Credentials
        /// </summary>
        Credentials Credentials { get; set; }

        /// <summary>
        /// Policy describing how we will send our messages
        /// </summary>
        SendPolicy Policy { get; set;}

        /// <summary>
        /// Remote endpoint that messages will be sent to
        /// </summary>
        Uri RequestUri { get; }

        #endregion
    }
}