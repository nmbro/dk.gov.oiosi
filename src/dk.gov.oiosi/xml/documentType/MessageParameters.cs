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
using dk.gov.oiosi.security;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;

namespace dk.gov.oiosi.xml.documentType {
    
    /// <summary>
    /// Holds the paramaterers that can be extracted from a message relevant to performing a 
    /// service call and UDDI lookup.
    /// </summary>
    public class MessageParameters {
        /// <summary>
        /// The endpoint key
        /// </summary>
        private IIdentifier _endpointKey;

        /// <summary>
        /// The endpoint key
        /// </summary>
        public IIdentifier EndpointKey {
            get { return _endpointKey; }
        }

        private EndpointKeytype _endpointKeyType;

        /// <summary>
        /// The keytype of the endpoint
        /// </summary>
        public EndpointKeytype EndpointKeyType {
            get { return _endpointKeyType; }
        }

        /// <summary>
        /// The UDDI id of the service contract tModel
        /// </summary>
        private UddiId _serviceContractTModel;

        /// <summary>
        /// The UDDI id of the service contract tModel
        /// </summary>
        public UddiId ServiceContractTModel {
            get { return _serviceContractTModel; }
        }

        /// <summary>
        /// The UDDI id of the tModel representing the relevant process
        /// </summary>
        private UddiId _processTModel;

        /// <summary>
        /// The UDDI id of the tModel representing the relevant process
        /// </summary>
        public UddiId ProcessTModel {
            get { return _processTModel; }
        }

        /// <summary>
        /// The type of the business process role identifier
        /// </summary>
        private BusinessProcessRoleIdentifierType _roleIdentifierType;

        /// <summary>
        /// The type of the business process role identifier
        /// </summary>
        public BusinessProcessRoleIdentifierType RoleIdentifierType {
            get { return _roleIdentifierType; }
        }

        /// <summary>
        /// The business process role identifier
        /// </summary>
        private BusinessProcessRoleIdentifier _roleIdentifier;

        /// <summary>
        /// The business process role identifier
        /// </summary>
        public BusinessProcessRoleIdentifier RoleIdentifier {
            get { return _roleIdentifier; }
        }


        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="endpointKey">The endpoint key of the endpoint, e.g. an EAN number</param>
        /// <param name="endpointKeyType">The endpoint keytype of the endpoint, e.g. EAN number</param>
        /// <param name="serviceContractTModel">The tModel UDDI ID of the service contract tModel</param>
        /// <param name="processTModel">The UDDI ID of the process. May be null.</param>
        /// <param name="roleIdentifierType">The process role identifier type of the process. May be null, 
        /// if roleIdentifier is null</param>
        /// <param name="roleIdentifier">The process role identifier. May be null, if the role identifier is null</param>
        public MessageParameters(
            IIdentifier endpointKey,
            EndpointKeytype endpointKeyType,
            UddiId serviceContractTModel,
            UddiId processTModel, 
            BusinessProcessRoleIdentifierType roleIdentifierType,
            BusinessProcessRoleIdentifier roleIdentifier
        ) {
            Init(endpointKey, endpointKeyType, serviceContractTModel, processTModel, roleIdentifierType, roleIdentifier);
        }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        /// <param name="endpointKey">The endpoint key of the endpoint, e.g. an EAN number</param>
        /// <param name="endpointKeyType">The endpoint keytype of the endpoint, e.g. EAN number</param>
        /// <param name="serviceContractTModel">The tModel UDDI ID of the service contract tModel</param>
        /// <param name="processTModel">The UDDI ID of the process. May be null.</param>
        /// <param name="roleIdentifierType">The process role identifier type of the process. May be null, 
        /// if roleIdentifier is null</param>
        /// <param name="roleIdentifier">The process role identifier. May be null, if the role identifier is null</param>
        private void Init(
            IIdentifier endpointKey,
            EndpointKeytype endpointKeyType,
            UddiId serviceContractTModel,
            UddiId processTModel,
            BusinessProcessRoleIdentifierType roleIdentifierType,
            BusinessProcessRoleIdentifier roleIdentifier
        ) {
            if (endpointKey == null) 
                throw new ArgumentNullException("endpointKey");
            if (endpointKeyType == null)
                throw new ArgumentNullException("endpointKeyType");
            if (serviceContractTModel == null)
                throw new ArgumentNullException("serviceContractTModel");
            if (processTModel == null && (roleIdentifier != null || roleIdentifierType != null)) 
                throw new Exception("If processTModel is null, roleIdentifier and roleIdentifierType cannot be set");
            if ((roleIdentifier == null && roleIdentifierType != null) || (roleIdentifier != null && roleIdentifierType == null))
                throw new Exception("RoleIdentifier and roleIdentifierType must both be either set or null");

            _endpointKey = endpointKey;
            _endpointKeyType = endpointKeyType;
            _serviceContractTModel = serviceContractTModel;
            _processTModel = processTModel;
            _roleIdentifierType = roleIdentifierType;
            _roleIdentifier = roleIdentifier;
        }

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="endpointKey">The endpoint key of the endpoint, e.g. an EAN number</param>
        /// <param name="serviceContractTModel">The tModel UDDI ID of the service contract tModel</param>
        /// <param name="endpointKeyType">The type of the endpoint key (EAN/CVR...)</param>
        public MessageParameters(
            IIdentifier endpointKey,
            EndpointKeytype endpointKeyType,
            UddiId serviceContractTModel
        ) {
            Init(endpointKey, endpointKeyType, serviceContractTModel, null, null, null);
        }
    }
}
