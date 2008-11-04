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
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.common.validation;

namespace dk.gov.oiosi.uddi.identifier {

    /// <summary>
    /// Base class for all ARS UDDI identifiers. May hold a list of possible values for the
    /// identifier. May hold a default value. NOTE: All static fields MUST BE set in the static 
    /// constructor of the descendant class.
    /// </summary>
    public abstract class ArsIdentifier {

        /// <summary>
        /// Internal Value value
        /// </summary>
        protected string pValue;

        /// <summary>
        /// Default constructor
        /// </summary>
        protected ArsIdentifier() {
            // 1. Set default value
            pValue = DefaultValue; ;
        }

        /// <summary>
        /// Gets or sets the value of the identifier
        /// </summary>
        public string Value {
            get { return pValue; }
            set { pValue = value; }
        }

        /// <summary>
        /// Gets the UDDI name of the identifier
        /// </summary>
        public abstract string IdentifierName { get;}

        /// <summary>
        /// Gets the UDDI tModel id of the identifier
        /// </summary>
        public abstract string IdentifierID { get;}

        /// <summary>
        /// True if a default value exists
        /// </summary>
        public abstract bool DefaultValueExists { get;}

        /// <summary>
        /// True if a predefined list of possible values exist for this identifier
        /// </summary>
        public abstract bool FixedValueListExists { get;}

        /// <summary>
        /// Returns the default value of this identifier if any exist, else returns null.
        /// You may check "DefaultValueExists" before calling.
        /// </summary>
        public abstract string DefaultValue { get; }

        /// <summary>
        /// Returns a list of possible values
        /// </summary>
        public abstract List<string> PossibleValues { get;}

        /// <summary>
        /// Returns the category as a UDDI keyedreference entity
        /// </summary>
        /// <returns>Returns the category as a UDDI keyedreference entity</returns>
        public KeyedReference GetAsKeyedReference() {
            KeyedReference keyRef = new KeyedReference();
            keyRef.TmodelKey = IdentifierID;
            keyRef.KeyName = IdentifierName;
            keyRef.KeyValue = Value;

            return keyRef;
        }

        /// <summary>
        /// Validate Value within the categorylist idenfied by Category
        /// </summary>
        /// <param name="ValueName">The value to be validated</param>
        /// <param name="Required">True if the parameter is required</param>
        /// <param name="Failures">Output parameter is a collection of validation failures</param>
        /// <returns></returns>
        public bool IsValid(string ValueName, bool Required,
                ref ValidationFailureCollection Failures) {
            bool Valid = true;
            if (!ValUtil.IsEmpty(Value)) {

                Valid = true;
                if (!Valid) {
                    DataValidationFailure.AddFailure(InvalidIdentifierValueFailure.Message(Value), ValueName, typeof(ArsIdentifier), ref Failures);
                    Valid = false;
                }
            } else
                // If field is required then it must not be null
                if (Required) {
                    DataValidationFailure.AddFailure(
                        RequiredFieldFailure.Message(ValueName), ValueName, typeof(ArsIdentifier), ref Failures);
                    Valid = false;
                }

            return Valid;
        }

        /// <summary>
        /// Validate Value within the categorylist idenfied by Category
        /// </summary>
        /// <param name="ValueName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(/*string Value, string Category,*/ string ValueName, ref ValidationFailureCollection Failures) {
            return IsValid(/*Value, Category,*/ ValueName, false, ref Failures);
        }

    }
}