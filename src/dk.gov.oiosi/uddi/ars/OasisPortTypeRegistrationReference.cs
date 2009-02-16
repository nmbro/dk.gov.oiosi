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

using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Holds a reference to a PortType registration tmodel
    /// </summary>
    public class OasisPortTypeRegistrationReference {

        private TModelInstanceInfo _portTypeReference = new TModelInstanceInfo();

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public OasisPortTypeRegistrationReference() {
            _portTypeReference = new TModelInstanceInfo();
        }

        /// <summary>
        /// Constructs the object from a TModelInstanceInfo object
        /// </summary>
        /// <param name="instanceInfo">The TModelInstanceInfo object</param>
        public OasisPortTypeRegistrationReference(TModelInstanceInfo instanceInfo) {
            this._portTypeReference = instanceInfo;
        }

        /// <summary>
        /// Constructor with porttype reference
        /// </summary>
        /// <param name="portTypeTModelKey">tmodelkey of porttype registration tmodel</param>
        public OasisPortTypeRegistrationReference(UddiId portTypeTModelKey) {

            try {
                SetPortTypeReference(portTypeTModelKey);
            } catch {
                throw;
            }
        }

        #endregion constructors

        #region properties

        /// <summary>
        /// Gets the uuid of the tmodel representing the porttype reference
        /// </summary>
        public UddiId ID {
            get {
                string idString = _portTypeReference.Value.tModelKey;
                return IdentifierUtility.GetUddiIDFromString(idString);
            }
            set {
                _portTypeReference.Value.tModelKey = value.ID;
            }
        }

        /// <summary>
        /// Gets a tmodelinstanceinfo
        /// </summary>
        public TModelInstanceInfo PortTypeReference {
            get { return _portTypeReference; }
        }

        #endregion properties

        #region methods

        /// <summary>
        /// Adds the reference to the porttype centralservice definition
        /// </summary>
        /// <param name="portTypeTModelKey">UddiId of porttype registration tmodel</param>
        public void SetPortTypeReference(UddiId portTypeTModelKey) {

            try {
                //1. set tmodelkey attribute to the tmodelkey
                _portTypeReference.Value.tModelKey = portTypeTModelKey.ID;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Validates the embedded data, and eventually returns a structured report if validations fails
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(string EntityName, ref dk.gov.oiosi.uddi.Validation.ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;

            TModelInstanceInfo.IsValid(_portTypeReference, "_portTypeReference", ref ChildFailures);

            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            return Failures == null;
        }
        
        #endregion methods
    }
}