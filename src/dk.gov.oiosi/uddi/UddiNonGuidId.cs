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
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.uddi {
    
    /// <summary>
    /// Represents a non-guid UDDI ID (i.e. uddi v.3 ID's apart from ID's of the format
    /// 'uddi:d01987d1-ab2e-3013-9be2-2a66eb99d824').
    /// </summary>
    public class UddiNonGuidId : UddiId {

        /// <summary>
        /// The UDDI 3.0 format key
        /// </summary>
        protected string pId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">guid</param>
        public UddiNonGuidId(string id) {
            if (String.IsNullOrEmpty(id)) throw new NullOrEmptyArgumentException("id");
            pId = id;
        }

        /// <summary>
        /// Gets the guid
        /// </summary>
        public override string ID {
            get { return pId; }
        }

        /// <summary>
        /// Gets guid as string
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return ID;
        }

    }
}