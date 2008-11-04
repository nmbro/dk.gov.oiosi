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

using dk.gov.oiosi.exception;
using dk.gov.oiosi.common.validation;
using dk.gov.oiosi.uddi.Validation;

namespace dk.gov.oiosi.uddi.category {

    /// <summary>
    /// Base class for all ARS UDDI categories. Holds a list of categoris and possible values for 
    /// each category. NOTE: All static fields MUST BE set in the static constructor
    /// of the descendant class.
    /// </summary>    
    public abstract class ArsCategory: IEquatable<ArsCategory> {
        /// <summary>
        /// Static list of possible category names
        /// </summary>
        protected static List<string> pCategoryNames = new List<string>();
        /// <summary>
        /// Static index of list of possible values for a specific category name
        /// </summary>
        protected static Dictionary<string, List<string>> pCategoryValues = new Dictionary<string, List<string>>();

        /// <summary>
        /// The category key
        /// </summary>
        protected string pKey;

        /// <summary>
        /// The category value
        /// </summary>
        protected string pValue;

        /// <summary>
        /// Default constructor
        /// </summary>
        protected ArsCategory() {
            // 1. Set default key / value
            pKey = DefaultCategory;
            pValue = DefaultCategoryValue;
        }

        /// <summary>
        /// Sets the category name and value
        /// </summary>
        /// <param name="category">The category name</param>
        /// <param name="value">The category value</param>
        public virtual void SetCategoryValue(string category, string value) {
            if (category == null) throw new NullArgumentException("category");
            if (value == null) throw new NullArgumentException("value");

            if (!pCategoryNames.Contains(category)) {
                throw new Exception("Category " + category + " does not exist in the category list. ");
            }
            pKey = category;

            List<string> possibleValues = pCategoryValues[category];
            if (possibleValues.Count > 0 && !possibleValues.Contains(value)) {
                throw new Exception("Category '" + category + "' cannot take the value '" + value + "'. ");
            }
            pValue = value;
        }

        /// <summary>
        /// Sets a catgory/value pair in the static category list to the selected value
        /// </summary>
        /// <param name="categoryAndValue">The value to set as category name/value</param>
        protected static void SetCategoryAndValues(string categoryAndValue) {
            SetCategoryAndValues(categoryAndValue, new string[] { categoryAndValue });
        }

        /// <summary>
        /// Associates a list of possible values with a single category
        /// </summary>
        /// <param name="category">The category</param>
        /// <param name="possibleValues">The possible values for the category</param>
        protected static void SetCategoryAndValues(string category, string[] possibleValues) {
            pCategoryNames.Add(category);
            pCategoryValues[category] = new List<string>(possibleValues);
        }

        /// <summary>
        /// Gets the category name ('key' in UDDI terminology)
        /// </summary>
        public string Category { get { return pKey; } }

        /// <summary>
        /// Gets the category value
        /// </summary>
        public string Value { 
            get { return pValue; }
            set { pValue = value; }
        }


        /// <summary>
        /// Gets the category identifier (uuid)
        /// </summary>
        public abstract string CategoryID { get;}

        /// <summary>
        /// Gets the category name
        /// </summary>
        public abstract string CategoryName { get;}

        /// <summary>
        /// Gets the default category name
        /// </summary>
        public abstract string DefaultCategory { get;}

        /// <summary>
        /// Gets the default category value
        /// </summary>
        public abstract string DefaultCategoryValue { get;}


        /// <summary>
        /// Returns a list of possible values
        /// </summary>
        public Dictionary<string, List<string>> CategoryValues {
            get {
                return pCategoryValues;
            }
        }

        /// <summary>
        /// Returns the category as a UDDI keyedreference entity
        /// </summary>
        /// <returns>Return the value as a keyed reference</returns>
        public KeyedReference GetAsKeyedReference() {
            KeyedReference keyRef = new KeyedReference();
            keyRef.TmodelKey = CategoryID;
            keyRef.KeyName = pKey;
            keyRef.KeyValue = pValue;

            return keyRef;
        }

        /// <summary>
        /// Validate Value within the categorylist idenfied by Category
        /// </summary>
        /// <param name="ValueName"></param>
        /// <param name="Required"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public virtual bool IsValid(/*string Value, string Category,*/ string ValueName, bool Required,
                ref ValidationFailureCollection Failures) {
            bool Valid = true;

            if (!ValUtil.IsEmpty(Value)) {
                List<string> possibleValues = pCategoryValues[DefaultCategory];
                if (!possibleValues.Contains(Value)) {
                    DataValidationFailure.AddFailure(InvalidCategoryValueFailure.Message(Value), ValueName, typeof(ArsCategory), ref Failures);
                    Valid = false;
                }
            } else
                // If field is required then it must not be null
                if (Required) {
                    DataValidationFailure.AddFailure(
                        RequiredFieldFailure.Message(ValueName), ValueName, typeof(ArsCategory), ref Failures);
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
        public virtual bool IsValid(/*string Value, string Category,*/ string ValueName, ref ValidationFailureCollection Failures) {
            return IsValid(/*Value, Category,*/ ValueName, false, ref Failures);
        }

        #region IEquatable<ArsCategory> Members

        /// <summary>
        /// Compares the two objects and returns true if they have equal values
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Returns true if the two objects have identical values</returns>
        public bool Equals(ArsCategory other) {
            if (other == null) return false;

            if (Category != other.Category) return false;
            if (Value != other.Value) return false;
            return true;
        }

        #endregion

    }
}