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
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.Services;
using dk.gov.oiosi.uddi.ars;
using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.common.validation;
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Service registration
    /// </summary>
    public class OasisServiceSingleBindingRegistration { //: RegistrationEntity, IRegistrationEntity {

        OasisPortTypeRegistrationReference _portTypeRegistrationRef;
        OasisBindingRegistrationReference _bindingRegistrationRef;

        EndpointAddressType _transport = new EndpointAddressType(EndpointAddressTypeCode.email);

        /// <summary>
        /// The selected binding / transport type
        /// </summary>
        public EndpointAddressType Transport {
            get { return _transport; }
            set { _transport = value; }
        }

        /// <summary>
        /// Constructor taking registration set
        /// </summary>
        public OasisServiceSingleBindingRegistration(/*OasisPortTypeRegistrationReference portTypeRef,
            OasisBindingRegistrationReference bindingRef*/
                                                          ) {

            try {
                ArsDefaultInstances.CheckDefaultInstances();

                //1. initialize the sets
                //SetPortTypeRef(portTypeRef);
                //SetBindingRef(bindingRef);

                _portTypeRegistrationRef =
                    new OasisPortTypeRegistrationReference(
                        (UddiGuidId)IdentifierUtility.GetUddiIDFromString(ArsDefaultInstances.InvoicePortTypeUddiId));

                _bindingRegistrationRef =
                    new OasisBindingRegistrationReference(
                        (UddiGuidId)IdentifierUtility.GetUddiIDFromString(ArsDefaultInstances.InvoiceBindingHttpUddiId));

                /*_smtpBindingRegistrationRef =
                    new OasisBindingRegistrationReference(
                        (UddiGuidId)IdentifierUtility.GetUddiIDFromString(ArsDefaultInstances.InvoiceBindingSmtpUddiId));
                 */
            } catch {
                throw;
            }
        }

        private void SetPortTypeRef(OasisPortTypeRegistrationReference portTypeRef) {
            if (portTypeRef != null && _portTypeRegistrationRef != null)
                _portTypeRegistrationRef.ID = IdentifierUtility.GetUddiIDFromString(portTypeRef.ID.ID);
            else
                _portTypeRegistrationRef.ID = null;
        }

        private void SetBindingRef(OasisBindingRegistrationReference bindingRef) {
            if (bindingRef != null && _bindingRegistrationRef != null) {
                _bindingRegistrationRef.ID = IdentifierUtility.GetUddiIDFromString(bindingRef.ID.ID);
                this.Transport = new EndpointAddressType(bindingRef.Transport);
            } else {
                _bindingRegistrationRef.ID = null;
            }
        }

        private OasisPortTypeRegistrationReference GetPortTypeRef() {
            if (_portTypeRegistrationRef != null) {
                OasisPortTypeRegistrationReference temp = new OasisPortTypeRegistrationReference();
                temp.ID = IdentifierUtility.GetUddiIDFromString(_portTypeRegistrationRef.ID.ID);
                return temp;
            } else
                return null;
        }

        private OasisBindingRegistrationReference GetBindingRef() {
            if (_bindingRegistrationRef != null) {
                OasisBindingRegistrationReference temp = new OasisBindingRegistrationReference();
                temp.ID = _bindingRegistrationRef.ID;
                return temp;
            } else
                return null;
        }

        /// <summary>
        /// Set of porttype registrations
        /// </summary>
        public OasisPortTypeRegistrationReference PortTypeRegistrationRef {
            get { return GetPortTypeRef(); }
            set { SetPortTypeRef(value); }
        }

        /// <summary>
        /// Set of binding registrations
        /// </summary>
        public OasisBindingRegistrationReference BindingRegistrationRef {
            get { return GetBindingRef(); }
            set { SetBindingRef(value); }
        }


        /// <summary>
        /// Validates a portType registration and updates the service registration reference set
        /// as well
        /// </summary>
        public void Validate() {
            if (_portTypeRegistrationRef == null || _bindingRegistrationRef == null) {
                
            }
        }

       

        #region IRegistrationEntity Members


        /// <summary>
        /// Validates the embedded data, and eventually returns a structured report if validations fails
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(string EntityName, ref dk.gov.oiosi.uddi.Validation.ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;

            BindingRegistrationRef.IsValid("Email Binding", ref ChildFailures);
            //HttpBindingRegistrationRef.IsValid("Http Binding", ref ChildFailures);
            PortTypeRegistrationRef.IsValid("PortType Reference", ref ChildFailures);

            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            if (BindingRegistrationRef == null)
                DataValidationFailure.AddFailure(MissingServiceDefinitionBindingFailure.Message(), "Bindings",
                    typeof(OasisBindingRegistrationReference), ref Failures);

            return Failures == null;
        }

        #endregion
    }
}