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
using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.Validation;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Holds a reference to a binding registration tmodel
    /// </summary>
    public class OasisBindingRegistrationReference {

        private TModelInstanceInfo _bindingReference = new TModelInstanceInfo();
        private EndpointAddressTypeCode _transport = EndpointAddressTypeCode.http;

        /// <summary>
        /// Gets or sets the type of endpoint address
        /// </summary>
        public EndpointAddressTypeCode Transport {
            get { return _transport; }
            set { _transport = value; }
        }

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public OasisBindingRegistrationReference() {
        }

        /// <summary>
        /// Constructs a binding registration reference from a TModelInstanceInfo object
        /// </summary>
        /// <param name="instanceInfo">The TModelInstanceInfo object</param>
        public OasisBindingRegistrationReference(TModelInstanceInfo instanceInfo) {
            this._bindingReference = instanceInfo;
        }

        /// <summary>
        /// Constructor with porttype reference
        /// </summary>
        /// <param name="bindingTModelKey">UddiGuidId of porttype registration tmodel</param>
        public OasisBindingRegistrationReference(UddiGuidId bindingTModelKey) {

            try {
                SetBindingReference(bindingTModelKey);
            } catch {
                throw;
            }

        }

        #endregion constructors

        #region properties

        /// <summary>
        /// Gets the uuid binding reference
        /// </summary>
        public UddiId ID {
            get {
                return new UddiGuidId(_bindingReference.Value.tModelKey, true);
            }
            set {
                _bindingReference.Value.tModelKey = value.ID;
            }
        }

        /// <summary>
        /// Gets a tmodelinstanceinfo
        /// </summary>
        public TModelInstanceInfo BindingReference {
            get { return _bindingReference; }
        }

        #endregion properties

        #region methods

        /// <summary>
        /// Adds the reference to the binding centralservice definition
        /// </summary>
        /// <param name="bindingTModelKey">UddiGuidId of binding registration tmodel</param>
        public void SetBindingReference(UddiGuidId bindingTModelKey) {

            try {
                //1. set tmodelkey attribute to the tmodelkey
                _bindingReference.Value.tModelKey = bindingTModelKey.ID;
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
        public bool IsValid(string EntityName, ref ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;

            TModelInstanceInfo.IsValid(_bindingReference, "_bindingReference", ref ChildFailures);
            
            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            return Failures == null;
        }

        #endregion methods
    }
}