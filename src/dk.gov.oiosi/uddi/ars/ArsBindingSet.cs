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
using dk.gov.oiosi.uddi.Services;
using dk.gov.oiosi.uddi.Validation;

namespace dk.gov.oiosi.uddi.ars {
    
    /// <summary>
    /// This class holds all binding templates of a service
    /// </summary>
    public class ArsBindingSet {

        private List<ArsBinding> _bindings = new List<ArsBinding>();

        #region properties

        /// <summary>
        /// Gets the list of ArsBindings
        /// </summary>
        public List<ArsBinding> Bindings {
            get { return _bindings; }
        }

        #endregion properties

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ArsBindingSet() {
        }

        /// <summary>
        /// Initializes with an existing ArsBindingSet
        /// </summary>
        /// <param name="bindings">an existing ArsBindingSet</param>
        public ArsBindingSet(List<ArsBinding> bindings) {
            _bindings = bindings;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="templates">An array of binding templates from which the object will be constructed</param>
        public ArsBindingSet(bindingTemplate[] templates) {

            try {
                for (int i = 0; i < templates.Length; i++) {
                    _bindings.Add(new ArsBinding(new BindingTemplate(templates[i])));
                }
            }
            catch (Exception exp) {
                throw new ArsBindingSetUnexpectedException(exp);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="templates"></param>
        public ArsBindingSet(BindingTemplateCollection templates) {
            try {
                foreach(BindingTemplate template in templates.Value) {
                    ArsBinding binding = new ArsBinding(template);
                    _bindings.Add(binding);
                }
            }
            catch (Exception exp) {
                throw new ArsBindingSetUnexpectedException(exp);
            }
        }

        #endregion constructors

        #region methods

        /// <summary>
        /// Adds a binding to the bindingset
        /// </summary>
        /// <param name="binding">the ArsBinding to add</param>
        public void Add(ArsBinding binding) {
            
            //1. add it to the list
            _bindings.Add(binding);
        }

        /// <summary>
        /// Removes a binding from the bindingset
        /// </summary>
        /// <param name="binding">the binding to remove</param>
        public void Remove(ArsBinding binding) {

            //1. remove a ArsBinding from the list
            _bindings.Remove(binding);
        }

        /// <summary>
        /// Gets all bindingtemplates
        /// </summary>
        /// <returns>a collection with all bindingtemplates</returns>
        public BindingTemplateCollection GetBindingTemplates() {

            //1. set temp bindingTemplate array
            BindingTemplateCollection tempBindings = null;
            try {
                tempBindings = new BindingTemplateCollection();

                for (int i = 0; i < _bindings.Count; i++) {
                    tempBindings.Add(_bindings[i].BindingTemplate.Value);
                }
            }
            catch (Exception exp) {
                throw new ArsBindingSetUnexpectedException(exp);
            }
            return tempBindings;
        }

        /// <summary>
        /// Validates the embedded data, and eventually returns a structured report if validations fails
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(string EntityName, ref ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;

            foreach (ArsBinding Binding in _bindings)
                Binding.IsValid("Binding", ref ChildFailures);

            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            return Failures == null;
        }

        #endregion methods
    }
}