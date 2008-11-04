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

using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.Businesses;
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.uddi.ars;

namespace dk.gov.oiosi.uddi.ars {
    /// <summary>
    /// Implementation of an Ars validator
    /// </summary>
    public class ArsValidator : IArsValidator {
        #region IArsValidator Members

        /// <summary>
        /// Validates the Entity and returns a structures report if any failures (Failures parameter)
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(IRegistrationEntity Entity, ref ValidationFailureCollection Failures) {
            return Entity.IsValid(Entity.GetType().ToString(), ref Failures);
        }

        /// <summary>
        /// Validates a ArsBusinessEntity
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        private bool IsValid(ArsBusinessEntity Entity, ref ValidationFailureCollection Failures) {
            return Entity.IsValid(Entity.Name.Text, ref Failures);
        }

        #endregion
    }
}
