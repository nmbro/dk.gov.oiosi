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
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System;
using System.Collections.Generic;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.security;

namespace dk.gov.oiosi.uddi {
    
    /// <summary>
    /// Response from a ARS UDDI lookup
    /// </summary>
    public class UddiLookupResponse {
        //TODO: delete?
        //private UddiId _newerVersionReference;
        //private Identifier _endpointIdentifierActual;
        //private CertificateSubject _certificateSubjectSerialNumber;
        //private DateTime _activationDate;
        //private DateTime _expirationDate;
        //private Uri _termsOfUseUrl;
        //private EndpointAddress _endpointAddress;
        //private System.Net.Mail.MailAddress _ServiceContactEmail;
        //private Version _version;
        private List<ProcessRoleDefinition> _processRoles = new List<ProcessRoleDefinition>();

        /// <summary>
        /// Constructor
        /// </summary>
        public UddiLookupResponse() {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpointIdentifierActual">The actual endpoint identifier of the request. This may 
        /// be different from what the user/client specified, e.g. it may be a gateway range EAN.</param>
        /// <param name="endpointAddress">Address of the endpoint</param>
        /// <param name="activationDate">Activation date of the endpoint registration</param>
        /// <param name="expirationDate">Expiration date of the endpoint registration</param>
        /// <param name="certificateSubjectSerialNumber">Subject of the certificate</param>
        /// <param name="termsOfUseUrl">URL to a file describing the terms of use</param>
        /// <param name="serviceContactEmail">Email of the service contact</param>
        /// <param name="version">Version of the endpoint</param>
        /// <param name="newerVersionReference">Possible reference to a newer version</param>
        /// <param name="processes">The processes supported by the endpoint</param>
        [Obsolete("Please use the one that defines the service type as well")]
        public UddiLookupResponse(Identifier endpointIdentifierActual, EndpointAddress endpointAddress, DateTime activationDate, DateTime expirationDate, CertificateSubject certificateSubjectSerialNumber, Uri termsOfUseUrl, System.Net.Mail.MailAddress serviceContactEmail, Version version, UddiId newerVersionReference, List<ProcessRoleDefinition> processes) {
            EndpointIdentifierActual = endpointIdentifierActual;
            EndpointAddress = endpointAddress;
            ActivationDate = activationDate;
            ExpirationDate = expirationDate;
            CertificateSubjectSerialNumber = certificateSubjectSerialNumber;
            TermsOfUseUrl = termsOfUseUrl;
            ServiceContactEmail = serviceContactEmail;
            Version = version;
            NewerVersionReference = newerVersionReference; 
            _processRoles = processes;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpointIdentifierActual">The actual endpoint identifier of the request. This may 
        /// be different from what the user/client specified, e.g. it may be a gateway range EAN.</param>
        /// <param name="endpointAddress">Address of the endpoint</param>
        /// <param name="activationDate">Activation date of the endpoint registration</param>
        /// <param name="expirationDate">Expiration date of the endpoint registration</param>
        /// <param name="certificateSubjectSerialNumber">Subject of the certificate</param>
        /// <param name="termsOfUseUrl">URL to a file describing the terms of use</param>
        /// <param name="serviceContactEmail">Email of the service contact</param>
        /// <param name="version">Version of the endpoint</param>
        /// <param name="newerVersionReference">Possible reference to a newer version</param>
        /// <param name="serviceType">The service type supported at the endpoint</param>
        /// <param name="processes">The processes supported by the endpoint</param>
        public UddiLookupResponse(Identifier endpointIdentifierActual, EndpointAddress endpointAddress, DateTime activationDate, DateTime expirationDate, CertificateSubject certificateSubjectSerialNumber, Uri termsOfUseUrl, System.Net.Mail.MailAddress serviceContactEmail, Version version, UddiId newerVersionReference, UddiId serviceType, List<ProcessRoleDefinition> processes) {
            this.EndpointIdentifierActual = endpointIdentifierActual;
            this.EndpointAddress = endpointAddress;
            this.ActivationDate = activationDate;
            this.ExpirationDate = expirationDate;
            this.CertificateSubjectSerialNumber = certificateSubjectSerialNumber;
            this.TermsOfUseUrl = termsOfUseUrl;
            this.ServiceContactEmail = serviceContactEmail;
            this.Version = version;
            this.NewerVersionReference = newerVersionReference;
            this.ServiceType = serviceType;
            this._processRoles = processes;
        }

        /// <summary>
        /// Gets or sets the actual endpoint identifier of the request. This may be different from
        /// what the user/client specified only if that EAN number was in the gateway range 
        /// configuration, and if no individual UDDI registration matching the client search criteria
        /// was found on the UDDI. In this case this property holds the value of the gateway identifier.
        /// </summary>
        public Identifier EndpointIdentifierActual { get; set; }

        /// <summary>
        /// Gets or set found endpointaddress
        /// </summary>
        public EndpointAddress EndpointAddress { get; set; }

        /// <summary>
        /// Gets or set expiration date of found endpoint registration
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or set activation date of found endpoint registration
        /// </summary>
        public DateTime ActivationDate { get; set; }

        /// <summary>
        /// Gets or sets serialnumber of found endpoint address certificates
        /// </summary>
        public CertificateSubject CertificateSubjectSerialNumber { get; set; }

        /// <summary>
        /// Gets or set terms of use uri's of found endpoint address
        /// </summary>
        public Uri TermsOfUseUrl { get; set; }

        /// <summary>
        /// Gets or set service contact emails of found endpoint address contacts
        /// </summary>
        public System.Net.Mail.MailAddress ServiceContactEmail { get; set; }

        /// <summary>
        /// Gets or set version of found endpoint address
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets or set newer version reference on found endpoint address
        /// </summary>
        public UddiId NewerVersionReference { get; set; }

        /// <summary>
        /// Gets or sets the service type on the found endpoint address
        /// </summary>
        public UddiId ServiceType { get; set; }

        /// <summary>
        /// Gets or set a value indicating if there is a newer verion of found endpoints
        /// </summary>
        public bool HasNewerVersion {
            get { return NewerVersionReference != null; }
        }

        /// <summary>
        /// Gets an enumerable over the processRoles in the response
        /// </summary>
        [Obsolete("Please use the ProcessRoles instead")]
        public IEnumerable<ProcessRoleDefinition> Processes {
            get { return _processRoles; }
        }

        /// <summary>
        /// Gets an enumerable over the processRoles in the response
        /// </summary>
        public IEnumerable<ProcessRoleDefinition> ProcessRoles {
            get { return _processRoles; }
        }

    }
}