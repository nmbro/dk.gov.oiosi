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
    /// Contains a set of process
    /// </summary>
    public class ArsProcessInstanceSet : IRegistrationEntity {
        private List<ArsProcessInstance> _processes = new List<ArsProcessInstance>();

        #region methods

        /// <summary>
        /// Get current process'
        /// </summary>
        public List<ArsProcessInstance> Processes {
            get { return _processes; }
        }

        /// <summary>
        /// Removes a processinstance from the instanceset
        /// </summary>
        /// <param name="instance">the process instance to remove</param>
        public void Remove(ArsProcessInstance instance) {
            try
            {
                for (int i = 0; i < _processes.Count; i++) {
                    if (_processes[i].ID.ID == instance.ID.ID) {
                        _processes.RemoveAt(i);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion methods

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ArsProcessInstanceSet() {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="instanceSet"></param>
        public ArsProcessInstanceSet(ArsProcessInstanceSet instanceSet)
        {
            foreach (ArsProcessInstance instance in instanceSet.Processes)
            {
                _processes.Add(instance);
            }
        }
        
        #endregion constructors

        #region IRegistrationEntity Members

        /// <summary>
        /// Saves the process set
        /// </summary>
        public void Save() {

            try {
                Validate();

                foreach (ArsProcessInstance process in _processes) {
                    process.Save();
                }
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Validates the process set
        /// </summary>
        public void Validate() {
            try {
                foreach (ArsProcessInstance process in _processes) {
                    process.Validate();
                }
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Updates the process set
        /// </summary>
        public void Update() {

            try {
                Validate();
                foreach (ArsProcessInstance process in _processes) {
                    process.Update();
                }
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Deletes the process set
        /// </summary>
        public void Delete() {

            try {
                // 1. Delete processes:
                List<ArsProcessInstance> toBeDeleted = new List<ArsProcessInstance>(_processes);
                foreach (ArsProcessInstance process in toBeDeleted) {
                    // a. delete from UDDI
                    process.Delete();

                    // b. remove from list
                    _processes.Remove(process);
                }
            }
            catch {
                throw;
            }
        }
        
        /// <summary>
        /// Adds a process to the set, but does not update the UDDI.
        /// </summary>
        /// <param name="process">The process registration to add</param>
        public void Add(ArsProcessInstance process) {
            if (process == null) throw new ArgumentNullException("process");
            bool exists = false;
            try {
                for (int i = 0; i < _processes.Count; i++) {
                    if (process.ID!=null && _processes[i].ID.ID == process.ID.ID) {
                        _processes[i] = process;
                        exists = true;
                        break;
                    }
                }
                if (!exists) {
                    _processes.Add(process);
                }
            }
            catch {
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

            foreach (ArsProcessInstance Process in _processes)
                Process.IsValid("_processes[i]", ref ChildFailures);

            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            return Failures == null;
        }
        
    #endregion IRegistrationEntity Members
    }
}