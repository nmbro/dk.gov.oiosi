﻿/*
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

    /// <summary>
    /// Response gotten from the service call
    /// </summary>
    public interface IResponse {

        /// <summary>
        /// The actual response message
        /// </summary>
        OiosiMessage ResponseMessage { get;}

        /// <summary>
        /// Gets a custom property set on the reply (for example a X509 Certificate or an by XSLT un-transformed message)
        /// </summary>
        /// <typeparam name="PropertyType">Properties are identified by their types</typeparam>
        /// <returns>The property requested</returns>
        PropertyType GetProperty<PropertyType> () where PropertyType : class;

        /// <summary>
        /// Sets a custom property set on the reply (for example a X509 Certificate or an by XSLT un-transformed message)
        /// </summary>
        void AddProperty(object property);

        /// <summary>
        /// The remote endpoint from which the response was gotten
        /// </summary>
        Uri ResponseUri { get; }
    }
}