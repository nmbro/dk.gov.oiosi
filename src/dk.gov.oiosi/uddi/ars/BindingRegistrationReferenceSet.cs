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
using dk.gov.oiosi.uddi.Validation;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Represents all binding registrations of a OasisServiceRegistrationReference
    /// </summary>
    public class BindingRegistrationReferenceSet : IRegistrationEntity{

        private List<OasisBindingRegistrationReference> pBindingRegistrations
            = new List<OasisBindingRegistrationReference>();

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BindingRegistrationReferenceSet() {
        }

        #endregion constructors

        #region properties

        /// <summary>
        /// Gets the list of porttype registrations
        /// </summary>
        public List<OasisBindingRegistrationReference> BindingRegistrations {
            get { return pBindingRegistrations; }
        }

        #endregion properties
        
        #region methods

        /// <summary>
        /// Adds a binding registration to a List
        /// </summary>
        /// <param name="reference">the registrationreference to add</param>
        public void Add(OasisBindingRegistrationReference reference) {

            try {
                pBindingRegistrations.Add(reference);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Removes a binding registration from a List
        /// </summary>
        /// <param name="reference">the registrationreference to delete</param>
        public void Delete(OasisBindingRegistrationReference reference) {

            try {
                pBindingRegistrations.Remove(reference);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Validates that an tmodelkey exists in the referenceset
        /// </summary>
        /// <param name="bindingReferenceTModelKey">the tmodelkey to check</param>
        /// <returns>returns true if the tmodelkey exists</returns>
        public bool ReferenceExists(UddiId bindingReferenceTModelKey) {

            bool _tempExists = false;

            try {
                foreach (OasisBindingRegistrationReference bindingRef in pBindingRegistrations) {
                    if (bindingRef.ID == bindingReferenceTModelKey) {
                        _tempExists = true;
                        break;
                    }
                }
            }
            catch {
                throw;
            }
            return _tempExists;
        }

        #endregion methods
        /// <summary>
        /// Saves the portType registrations in this set
        /// </summary>
        public void Save() {
            // 1. Verify properties for each bindingRegistration
            Validate();
        }

        /// <summary>
        /// Validates the portType registrations in this set
        /// </summary>
        public void Validate() {
        }

        /// <summary>
        /// Updates the portType registrations in this set
        /// </summary>
        public void Update() {
            // 1. Verify properties
            Validate();
        }

        /// <summary>
        /// Delete the portType registrations in this set
        /// </summary>
        public void Delete() {
        }

        /// <summary>
        /// Validates the embedded data, and eventually returns a structured report if validations fails
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(string EntityName, ref ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;

            foreach (OasisBindingRegistrationReference BindingRef in pBindingRegistrations)
                BindingRef.IsValid("BindingRegistrations", ref ChildFailures);

            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            return Failures == null;
        }
    }
}