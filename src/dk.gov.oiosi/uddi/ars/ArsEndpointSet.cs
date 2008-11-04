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
    /// Represents a set of endpoints
    /// </summary>
    public class ArsEndpointSet {
        private List<ArsEndpoint> _endpoints = new List<ArsEndpoint>();

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ArsEndpointSet() {
        }
        
        /// <summary>
        /// Constructor that initializes an ArsEndpointSet with existing businessservices
        /// </summary>
        /// <param name="services">an array of existing businessServices</param>
        /// <exception cref="ArsEndpointSetUnexpectedException">Thrown if an unexpected error occures</exception>
        public ArsEndpointSet(BusinessService[] services) {
            try {
                if (services != null) {
                    //1. runs through all services and adds them to the innerlist
                    foreach (BusinessService service in services) {
                        ArsEndpoint tempEndpoint = new ArsEndpoint(service);
                        _endpoints.Add(tempEndpoint);
                    }
                }
            }
            catch (Exception ex) {
                throw new ArsEndpointSetUnexpectedException(ex);
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets and sets Endpoints
        /// </summary>
        public List<ArsEndpoint> Endpoints {
            get { return _endpoints; }
            set { _endpoints = value; }
        }

        #endregion properties

        #region methods

        /// <summary>
        /// Adds a endpoint to the innerlist
        /// </summary>
        /// <param name="endpoint">the endpoint to add</param>
        /// <exception cref="ArsEndpointSetUnexpectedException">Thrown if an unexpected error occures</exception>
        public void Add(ArsEndpoint endpoint) {
            try {
                //1. add the endpoint to the innerlist
                if (endpoint != null) {
                    Endpoints.Add(endpoint);
                }
            }
            catch (Exception ex) {
                throw new ArsEndpointSetUnexpectedException(ex);
            }
        }

        /// <summary>
        /// Removes a endpoint from the innerlist
        /// </summary>
        /// <param name="endpoint">the endpoint to remove</param>
        /// <exception cref="ArsEndpointSetUnexpectedException">Thrown if an unexpected error occures</exception>
        public void Remove(ArsEndpoint endpoint) {

            try {
                //1. remove a endpoint from the innerlist
                if (endpoint != null) {
                    Endpoints.Remove(endpoint);
                }
            }
            catch (Exception ex) {
                throw new ArsEndpointSetUnexpectedException(ex);
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

            foreach (ArsEndpoint Endpoint in _endpoints)
                Endpoint.IsValid("Endpoint", ref ChildFailures);

            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            return Failures == null;
        }

        #endregion methods
    }
}