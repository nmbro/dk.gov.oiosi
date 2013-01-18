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
namespace dk.gov.oiosi.communication.handlers.email {
    
    /// <summary>
    /// Mail server configuration
    /// </summary>
    public interface IMailServerConfiguration 
    {
        /// <summary>
        /// The address of the mail server
        /// </summary>
        string ServerAddress { get; set; }
        
        /// <summary>
        /// Password used to log on to the server (combined with the UserName property)
        /// </summary>
        string Password { get; set; }
 
        /// <summary>
        /// Username used to log on to the server (combined with the Password property)
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// What address should one reply to?
        /// </summary>
        string ReplyAddress { get; set; }

        /// <summary>
        /// Policy describing the way we connect to the mail server, for example connection time and polling pattern.
        /// </summary>
        MailServerConnectionPolicy ConnectionPolicy { get; set; }
    }
}