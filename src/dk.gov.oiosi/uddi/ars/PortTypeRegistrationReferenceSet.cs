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
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;
using dk.gov.oiosi.uddi.Validation;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// PortType registration set
    /// </summary>
    public class PortTypeRegistrationReferenceSet : IRegistrationEntity {

        /// <summary>
        /// The list of portTypeRegistrations
        /// </summary>
        private List<OasisPortTypeRegistrationReference> _portTypeRegistrations = new List<OasisPortTypeRegistrationReference>();

        /// <summary>
        /// Returns a list of portType registration entities
        /// </summary>
        public List<OasisPortTypeRegistrationReference> PortTypeRegistrations {
            get { return _portTypeRegistrations; }
            set { _portTypeRegistrations = value; }
        }

        /// <summary>
        /// Adds a PortType registration to a List
        /// </summary>
        /// <param name="reference">the registrationreference to add</param>
        public void Add(OasisPortTypeRegistrationReference reference) {

            try {
                _portTypeRegistrations.Add(reference);
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Removes a PortType registration from a List
        /// </summary>
        /// <param name="reference">the registrationreference to delete</param>
        public void Delete(OasisPortTypeRegistrationReference reference) {

            try {
                _portTypeRegistrations.Remove(reference);
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Validates that an tmodelkey exists in the referenceset
        /// </summary>
        /// <param name="PortTypeReferenceTModelKey">the tmodelkey to check</param>
        /// <returns>returns true if the tmodelkey exists</returns>
        public bool ReferenceExists(UddiGuidId PortTypeReferenceTModelKey) {

            bool _tempExists = false;

            try {
                foreach (OasisPortTypeRegistrationReference PortTypeRef in _portTypeRegistrations) {
                    if (PortTypeRef.ID == PortTypeReferenceTModelKey) {
                        _tempExists = true;
                        break;
                    }
                }
            } catch {
                throw;
            }
            return _tempExists;
        }

        /// <summary>
        /// Saves the portType registrations in this set
        /// </summary>
        public void Save() {
            // 1. Verify properties for each PortTypeRegistration
            Validate();

            // 3. Save
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

            // 3. Update in uddi
        }

        /// <summary>
        /// Delete the portType registrations in this set
        /// </summary>
        public void Delete() {
        }
    
        /// <summary>
        /// True if valid
        /// </summary>
        /// <param name="EntityName">The name of the entity</param>
        /// <param name="Failures">Collection of failures</param>
        /// <returns>True if valid</returns>
        public bool IsValid(string EntityName, ref dk.gov.oiosi.uddi.Validation.ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;

            foreach (OasisPortTypeRegistrationReference portTypeRef in _portTypeRegistrations)
                portTypeRef.IsValid("portType", ref ChildFailures);

            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            return Failures == null;
        }
    }
}