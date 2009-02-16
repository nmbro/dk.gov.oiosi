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
using dk.gov.oiosi.uddi.identifier;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.Services;
using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Represents a ARS endpoint registration, complete with:
    /// a) UDDI businessService info
    /// b) All relevant custom identifieres and categories
    /// c) Process definition references
    /// </summary>
    public class ArsEndpoint : RegistrationEntity, IRegistrationEntity {
        private BusinessService _businessService;

        #region constructors

        private void Init() {
            _businessService = new BusinessService();
        }

        /// <summary>
        /// Default constructor. Constructs emtpy businessService with an ID
        /// </summary>
        public ArsEndpoint() {
            Init();
            SetDefaultProperties();
        }

        /// <summary>
        /// Constructor used when initializing an existing businessservice
        /// </summary>
        /// <param name="service">an existing businessservice</param>
        public ArsEndpoint(BusinessService service) {
            try {
                _businessService = service;
            }
            catch (ArgumentNullException) {
                throw;
            }
            catch (Exception exp) {
                throw new ArsEndpointUnexpectedException(exp);
            }
        }

        /// <summary>
        /// Sets the default properties
        /// </summary>
        private void SetDefaultProperties() {
            try {
                CategoryBag bag = new CategoryBag();

                RegistrationConformanceClaim = 
                    new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1);
                bag.AddCategory(RegistrationConformanceClaim.GetAsKeyedReference());

                AuthenticationRequired authRequired = new AuthenticationRequired(
                    AuthenticationRequiredCode.authenticationRequired);
                bag.AddCategory(authRequired.GetAsKeyedReference());

                ConformanceClaim confClaim =
                    new ConformanceClaim(ConformanceClaimCode.secureReliableAsyncProfile1_0);
                bag.AddCategory(confClaim.GetAsKeyedReference());

                // 2. Add bag:
                _businessService.Value.categoryBag = bag.Value;
            }
            catch (Exception exp) {
                throw new ArsEndpointUnexpectedException(exp);
            }
        }

        /// <summary>
        /// Constructor used when creating a new businessservice
        /// </summary>
        /// <param name="oioTaxonomy">OIO taxonomy</param>
        /// <param name="endpointContactEmail">contact email for a service contact</param>
        /// <param name="endpointKey">endpointkey. e.g. ean key, cvr key, etc.</param>
        /// <param name="endpointKeytype">key type. e.g. cvr, ean, etc.</param>
        /// <param name="endpointActivationDate">the date the endpoint will activate</param>
        /// <param name="endpointExpirationDate">the date the endpoint will expire</param>
        /// <param name="termsOfUseUrl">the url for the 'terms of use' document</param>
        /// <param name="endpointCertificate">the certificate</param>
        /// <param name="version">Major, minor and revisions version (e.g. '1.0.0')</param>
        /// <param name="newerVersionReference">a reference for a newer version of the endpoint</param>
        /// <param name="serviceName">the name of the service</param>
        /// <param name="serviceDescription">the description of the service</param>
        /// <param name="businessKey">the key to the businessentity of the service</param>
        public ArsEndpoint(OIOTaxonomy oioTaxonomy,
                           EndpointContactEmail endpointContactEmail,
                           ArsEndpointKey endpointKey,
                           EndpointKeytype endpointKeytype,
                           EndpointActivationDate endpointActivationDate,
                           EndpointExpirationDate endpointExpirationDate,
                           TermsOfUseUrl termsOfUseUrl,
                           EndpointCertificate endpointCertificate,
                           Version version,
                           NewerVersionReference newerVersionReference,
                           Name serviceName,
                           Description serviceDescription,
                           UddiId businessKey
        ) {

            Init();

            //1. set serviceentity attributes, names and descriptions
            try {
                SetBusinessServiceDetails(oioTaxonomy, endpointContactEmail,
                    endpointKey, endpointKeytype, endpointActivationDate, endpointExpirationDate,
                    termsOfUseUrl, endpointCertificate, version, newerVersionReference,
                    serviceName, serviceDescription, businessKey);
            }
            catch (Exception exp) {
                throw new ArsEndpointUnexpectedException(exp);
            }
        }

        #endregion constructors

        #region properties


        /// <summary>
        /// Gets the underlying BusinessService object
        /// </summary>
        public BusinessService Service {
            get { return _businessService; }
        }

        /// <summary>
        /// Adds a category to the category bag
        /// </summary>
        /// <param name="category">The category to set</param>
        private void SetCategory(ArsCategory category) {
            if (category != null) {
                SetCategory(category.GetAsKeyedReference().Value);
            } 
        }

        /// <summary>
        /// Adds a category to the category bag
        /// </summary>
        /// <param name="uddiKeyRef">A keyed reference</param>
        public void SetCategory(keyedReference uddiKeyRef) {
            KeyedReference keyRef = new KeyedReference(uddiKeyRef);
            if (keyRef != null) {
                if (_businessService != null) {
                    if (_businessService.CategoryBag == null) {
                        _businessService.CategoryBag = new CategoryBag(keyRef);
                    } else {
                        _businessService.CategoryBag.SetCategory(keyRef);
                    }
                } else {
                    return;
                }
            } 
        }

        /// <summary>
        /// Returns the category bag
        /// </summary>
        public CategoryBag GetCategoryBag() {
            if (_businessService == null) return new CategoryBag();
            return _businessService.CategoryBag;
        }


        /// <summary>
        /// Gets the registration conformance claim regarding Uddi registration model
        /// </summary>
        public RegistrationConformanceClaim RegistrationConformanceClaim {
            get{
                if (_businessService != null && _businessService.CategoryBag != null) {
                    RegistrationConformanceClaim regClaim = new RegistrationConformanceClaim();

                    KeyedReference keyRef = _businessService.CategoryBag.GetCategoryByIdentifierAndKeyName(regClaim.CategoryID, regClaim.CategoryName);
                    if (keyRef != null) {
                        regClaim.SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
                        return regClaim;
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    SetCategory(value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new RegistrationConformanceClaim().CategoryID);
                }
            }
        }

        /// <summary>
        /// Gets the identifier (guid) of the businessservice
        /// </summary>
        public UddiId ID {
            get {
                if (_businessService != null && _businessService.Value.serviceKey != null && _businessService.Value.serviceKey.Length > 0) {
                    string idString = _businessService.Value.serviceKey;
                    return IdentifierUtility.GetUddiIDFromString(idString);
                }
                else {
                    return null;
                }
            }
            set {
                try {
                    if (value != null) {
                        _businessService.Value.serviceKey = value.ID;
                    }
                    else {
                        _businessService.Value.serviceKey = null;
                    }
                }
                catch (Exception exp) {
                    throw new ArsEndpointUnexpectedException(exp);
                }
            }
        }

        /// <summary>
        /// Gets or sets bindings
        /// </summary>
        public ArsBindingSet Bindings {
            get {
                if (_businessService == null || _businessService.BindingTemplates == null) return new ArsBindingSet();
                ArsBindingSet bindingSet = new ArsBindingSet(_businessService.BindingTemplates.GetBindingTemplateCollection());
                return bindingSet;
            }
            set {
                if (value != null) {
                    _businessService.BindingTemplates = value.GetBindingTemplates();
                }
                else {
                    _businessService.BindingTemplates = new BindingTemplateCollection();
                }
            }
        }

        /// <summary>
        /// Gets or sets a (UDDI serviceKey) reference to a newer version of the 
        /// endpoint.
        /// </summary>
        public NewerVersionReference NewerVersionReference {
            get {
                NewerVersionReference newVersionRef = new NewerVersionReference();
                return (NewerVersionReference)GetCategory(newVersionRef);
            }
            set {
                if (value != null) {
                    SetCategory(value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new NewerVersionReference().CategoryID);
                }
            }
        }

        /// <summary>
        /// Gets or sets the version of the endpoint (major, minor, revision)
        /// Note that 'revision' is mapped to the 'build' field of the 
        /// .NET builtin 'System.Version' class.
        /// </summary>
        public Version Version {
            get {
                // 1. Get the individual version parts:
                Version v = null;
                try {
                    VersionMajor major = new VersionMajor();
                    major = (VersionMajor)GetCategory(major);
                    if (major == null) return null;

                    VersionMinor minor = new VersionMinor();
                    minor = (VersionMinor)GetCategory(minor);

                    VersionRevision rev = new VersionRevision();
                    rev = (VersionRevision)GetCategory(rev);

                    // 2. Create the version number
                    int majorInt = 0;
                    int minorInt = 0;
                    int revisionInt = 0;

                    majorInt = Int32.Parse(major.Value);
                    if (minor != null) minorInt = Int32.Parse(minor.Value);
                    if (rev != null) revisionInt = Int32.Parse(rev.Value);

                    v = new Version(majorInt, minorInt, revisionInt);
                }
                catch (Exception exp) {
                    throw new ArsEndpointUnexpectedException(exp);
                }
                return v;
            }
            set {
                if (value != null) {
                    SetCategory(new VersionMajor(value.Major.ToString()));
                    SetCategory(new VersionMinor(value.Minor.ToString()));
                    SetCategory(new VersionRevision(value.Build.ToString()));
                }
                else {
                    _businessService.CategoryBag.RemoveCategoryById(new VersionMajor().CategoryID);
                    _businessService.CategoryBag.RemoveCategoryById(new VersionMinor().CategoryID);
                    _businessService.CategoryBag.RemoveCategoryById(new VersionRevision().CategoryID);
                }
            }
        }        

        /// <summary>
        /// Gets or sets the subject string of the endpoint certificate
        /// </summary>
        public EndpointCertificate CertificateSubject {
            get {
                EndpointCertificate cert = new EndpointCertificate();
                return (EndpointCertificate)GetCategory(cert);
            }
            set {
                if (value != null) {
                    SetCategory(value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new EndpointCertificate().CategoryID);
                }
            }
        }

        /// <summary>
        /// Gets or sets the url to a document with terms of use
        /// </summary>
        public TermsOfUseUrl TermsOfUseUrl {
            get {
                TermsOfUseUrl terms = new TermsOfUseUrl();
                return (TermsOfUseUrl)GetCategory(terms);
            }
            set {
                if (value != null) {
                    SetCategory(value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new TermsOfUseUrl().CategoryID);
                }
            }
        }

        /// <summary>
        /// Gets or sets the endpoint activation date
        /// </summary>
        public EndpointActivationDate EndpointActivationDate {
            get {
                EndpointActivationDate activationDate = new EndpointActivationDate();
                return (EndpointActivationDate)GetCategory(activationDate);
            }
            set {
                if (value != null) {
                    SetCategory(value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new EndpointActivationDate().CategoryID);
                }
            }
        }

        /// <summary>
        /// Gets or sets the endpoint expiration date
        /// </summary>
        public EndpointExpirationDate EndpointExpirationDate {
            get {
                EndpointExpirationDate expirationDate = new EndpointExpirationDate();
                return (EndpointExpirationDate)GetCategory(expirationDate);
            }
            set {
                if (value != null) {
                    SetCategory(value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new EndpointExpirationDate().CategoryID);
                }
            }
        }

        /// <summary>
        /// Gets or sets the endpoint address type
        /// </summary>
        public EndpointAddressType EndpointAddressType {
            get {
                EndpointAddressType endpointAddressType = new EndpointAddressType();
                return (EndpointAddressType)GetCategory(endpointAddressType);
            }
            set {
                if (value != null) {
                    SetCategory(value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new EndpointAddressType().CategoryID);
                }
            }
        }

        /// <summary>
        /// Gets or sets the endpoint key type
        /// </summary>
        public EndpointKeytype EndpointKeyType {
            get {
                EndpointKeytype keyType = new EndpointKeytype();
                return (EndpointKeytype)GetCategory(keyType);
            }
            set {
                if (value != null) {
                    SetCategory(value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new EndpointKeytype().CategoryID);
                }
            }
        }

        /// <summary>
        /// Gets or sets the endpoint key
        /// </summary>
        public IIdentifier EndpointKey {
            get {
                ArsEndpointKey endpointKey = new ArsEndpointKey();
                KeyedReference keyRef =_businessService.CategoryBag.GetCategoryByIdentifierAndKeyName(endpointKey.CategoryID, endpointKey.CategoryName);
                if (keyRef != null) {
                    return IdentifierUtility.GetIdentifierFromKeyType(keyRef.KeyValue,
                        this.EndpointKeyType.GetEndpointKeyTypeCode());
                } else {
                    return null;
                }

            }
            set {
                if (value != null) {
                    ArsEndpointKey key = new ArsEndpointKey(value);
                    SetCategory(key);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new ArsEndpointKey().CategoryID);
                }
            }
        }

        /// <summary>
        /// Email of the endpoint contact. Returns null if this property has 
        /// not been set.
        /// </summary>
        public EndpointContactEmail ContactEmail {
            get {
                EndpointContactEmail contactEmail = new EndpointContactEmail();
                return (EndpointContactEmail)GetCategory(contactEmail);
            }
            set {
                if (value != null) {
                    SetCategory(value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new EndpointContactEmail().CategoryID);
                }
            }
        }


        /// <summary>
        /// The OIO taxonomy. Null if none is defined.
        /// </summary>
        public OIOTaxonomy Taxonomy {
            get {
                OIOTaxonomy tax = new OIOTaxonomy();
                return (OIOTaxonomy)GetCategory(tax);
            }
            set {
                if (value != null){
                    SetCategory((OIOTaxonomy) value);
                } else {
                    _businessService.CategoryBag.RemoveCategoryById(new OIOTaxonomy().CategoryID);
                }
            }
        }

        /// <summary>
        /// The name of the endpoint
        /// </summary>
        public Name Name {
            get {
                if (_businessService != null) {
                    return _businessService.Name;
                }
                else {
                    return null;
                }
            }
            set {
                if (_businessService != null) {
                    _businessService.Name = value;
                }
            }
        }

        /// <summary>
        /// Description of the endpoint
        /// </summary>
        public Description Description {
            get {
                return _businessService.Description;
            }
            set {
                _businessService.Description = value;
            }
        }


        /// <summary>
        /// Gets or sets the business key
        /// </summary>
        public UddiId BusinessKey {
            get {
                if (_businessService != null) {
                    return IdentifierUtility.GetUddiIDFromString(_businessService.BusinessKey);
                }
                else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    _businessService.BusinessKey = value.ID;
                }
                else {
                    _businessService.BusinessKey = null;
                }
            }
        }

        #endregion properties

        #region methods

        private void SaveServiceEntity() {
            //1. add the service to an array
            BusinessService[] services = new BusinessService[] { _businessService };

            try {
                //2. call Publication
                Publication uddiPublicationProxy = new Publication(Connection);
                serviceDetail detail = uddiPublicationProxy.Save(services);
                _businessService = new BusinessService(detail.businessService[0]);
            }
            catch (SaveServiceException ex) {
                throw ex;
            }
            catch (PublicationUnexpectedException) {
                throw;
            }
            catch (Exception exp) {
                throw new ArsEndpointUnexpectedException(exp);
            }
        }

        /// <summary>
        /// Adds the process instance to all bindings in the endpoint
        /// </summary>
        /// <param name="processInstance"></param>
        public void AddProcessInstanceToBindings(ArsProcessInstance processInstance) {
            foreach (ArsBinding binding in this.Bindings.Bindings) {
                binding.AddProcessInstance(processInstance);
            }
        }

        /// <summary>
        /// Removes the process instance from all bindings in the endpoint
        /// </summary>
        /// <param name="processInstance"></param>
        public void RemoveProcessInstanceFromBindings(ArsProcessInstance processInstance) {
            foreach (ArsBinding binding in this.Bindings.Bindings) {
                binding.RemoveProcessInstance(processInstance);
            }
        }

        /// <summary>
        /// Sets the process instances (roles) to the bindings of the endpoint
        /// </summary>
        /// <param name="processInstances"></param>
        public void SetProcessInstanceToBindings(IEnumerable<ArsProcessInstance> processInstances) {
            foreach (ArsBinding binding in this.Bindings.Bindings) {
                binding.ProcessInstanceSet = new ArsProcessInstanceSet();
                foreach (ArsProcessInstance processInstance in processInstances) {
                    binding.ProcessInstanceSet.Add(processInstance);
                }
            }
        }

        /// <summary>
        /// Gets the process instances (roles) defined in the bindings.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ArsProcessInstance> GetProcessInstancesFromBindings() {
            Dictionary<string, ArsProcessInstance> processInstances = new Dictionary<string, ArsProcessInstance>();

            foreach (ArsBinding binding in Bindings.Bindings) {
                IEnumerable<ArsProcessInstance> processBindings = binding.ProcessInstanceSet.Processes;
                foreach (ArsProcessInstance processInstance in processBindings) {
                    processInstances[processInstance.ID.ID] = processInstance;
                }
            }
            return processInstances.Values;
        }

        /// <summary>
        /// Constructs an ArsEndpoint object from a business service ID. 
        /// </summary>
        /// <param name="businessServiceId">business service uuid</param>
        /// <param name="getRecursively">If getRecursively = true, all sub entities are loaded as well</param>
        /// <returns></returns>
        public static ArsEndpoint Get(UddiId businessServiceId, bool getRecursively){
             
            //1. create a temp serviceentity
            ArsEndpoint tempBusinessService = null;

            //2. create a uddi inquire instance
            Inquiry inq = new Inquiry();

            //3. call getbusinessdetail
            BusinessService[] entities;
            try {
                GetServiceDetail detail = new GetServiceDetail(businessServiceId.ID);
                entities = inq.GetDetail(detail.Value);

                if (entities.Length == 1) {
                    tempBusinessService = new ArsEndpoint(entities[0]);
                }
            }
            catch (GetServiceDetailException) {
                throw;
            }
            catch (InquiryUnexpectedException) {
                throw;
            }
            catch (Exception exp) {
                throw new ArsEndpointUnexpectedException(exp);
            }

            if (getRecursively) {

                //1. add bindings                
                if (tempBusinessService._businessService.Value.bindingTemplates.Length > 0) {
                    tempBusinessService.Bindings = new ArsBindingSet(tempBusinessService._businessService.Value.bindingTemplates);
                }
            }

            return tempBusinessService;
        }

        /// <summary>
        /// This method is used to gather all attributes and call there relative set methods
        /// </summary>
        private void SetBusinessServiceDetails(
            OIOTaxonomy oioTaxonomy, 
            EndpointContactEmail endpointContactEmail, 
            ArsEndpointKey endpointKey,
            EndpointKeytype endpointKeytype, 
            EndpointActivationDate endpointActivationDate,
            EndpointExpirationDate endpointExpirationDate,
            TermsOfUseUrl termsOfUseUrl, 
            EndpointCertificate endpointCertificate, 
            Version version, 
            NewerVersionReference newerVersionReference,
            Name serviceName, 
            Description serviceDescription, 
            UddiId businessKey
        ) {

            //1. set custom attributes
            SetCustomAttributes(oioTaxonomy, endpointContactEmail,
                endpointKey, endpointKeytype, endpointActivationDate, endpointExpirationDate,
                termsOfUseUrl, endpointCertificate, version, newerVersionReference);

            //2. set names
            Name = serviceName;

            //3. description
            Description = serviceDescription;

            //4. set businesskey
            BusinessKey = businessKey;
        }        

        /// <summary>
        /// Set all custom categories
        /// </summary>
        private void SetCustomAttributes( 
            OIOTaxonomy oioTaxonomy, 
            EndpointContactEmail endpointContactEmail, 
            ArsEndpointKey endpointKey,
            EndpointKeytype endpointKeytype, 
            EndpointActivationDate endpointActivationDate,
            EndpointExpirationDate endpointExpirationDate, 
            TermsOfUseUrl termsOfUseUrl, 
            EndpointCertificate endpointCertificate, 
            Version version, 
            NewerVersionReference newerVersionReference
        ) {
            
            //1. Reset categorybag:
            _businessService.Value.categoryBag = null;
            CategoryBag bag = new CategoryBag();
            
            AuthenticationRequired authRequired = new AuthenticationRequired(
                AuthenticationRequiredCode.authenticationRequired);
            bag.AddCategory(authRequired.GetAsKeyedReference());
            ConformanceClaim confClaim =
                new ConformanceClaim(ConformanceClaimCode.secureReliableAsyncProfile1_0);
            bag.AddCategory(confClaim.GetAsKeyedReference());

            // 2. Add bag:
            _businessService.Value.categoryBag = bag.Value;

            // 3. Add predefined categories:
            SetDefaultProperties();
            
            // 4. Set other custom properties:
            Taxonomy = oioTaxonomy;
            ContactEmail = endpointContactEmail;

            // 5. Set endpointkey, based on endpointkeytype
            switch (endpointKeytype.GetEndpointKeyTypeCode()) {
                case EndpointKeyTypeCode.ean:
                    EndpointKey = new IdentifierEan(endpointKey.Value);
                    break;
                case EndpointKeyTypeCode.ovt:
                    EndpointKey = new IdentifierOvt(endpointKey.Value);
                    break;
                case EndpointKeyTypeCode.cvr:
                    EndpointKey = new IdentifierCvr(endpointKey.Value);
                    break;
                case EndpointKeyTypeCode.other:
                    throw new ArgumentException("Unsupported endpointkeytype");
                default:
                    throw new ArgumentException("Unsupported endpointkeytype");
            }

            EndpointKeyType = endpointKeytype;
            EndpointActivationDate = endpointActivationDate;
            EndpointExpirationDate = endpointExpirationDate;
            TermsOfUseUrl = termsOfUseUrl;
            CertificateSubject = endpointCertificate;
            NewerVersionReference = newerVersionReference;
            Version = version;
        }


        /// <summary>
        /// Gets an ARS category by the category ID of the 'categoryObject' 
        /// parameter. Note that we need an actual ArsCategory descendant 
        /// object to get the category identifier. If no match is found, null is returned.
        /// </summary>
        /// <param name="category">The category object from which to get the category ID.
        /// Another object, with key and value set, is returned as a result.</param>
        /// <returns></returns>
        private ArsCategory GetCategory(ArsCategory category) {
            if (category == null) return null;
            CategoryBag bag = new CategoryBag(_businessService.Value.categoryBag);
            KeyedReference[] keyRef = bag.GetCategoryByIdentifier(category.CategoryID);
            if (keyRef == null || keyRef.Length < 1) return null;
            if (keyRef.Length > 1) throw new Exception("Too many key references found");
            category.SetCategoryValue(keyRef[0].KeyName, keyRef[0].KeyValue);
            return category;
        }

        #endregion methods

        #region IRegistryEntity

        /// <summary>
        /// Saves the entity
        /// </summary>
        public void Save() {
            //Copies the bindings to the binding template
            BindingTemplateCollection bindingTemplateCollection = this.Bindings.GetBindingTemplates();
            this._businessService.BindingTemplates = bindingTemplateCollection;

            // 1. Verify properties
            Validate();

            // 2. Save
            try {
                SaveServiceEntity();
            }
            catch (Exception exp) {
                throw new SaveArsEndpointFailedException(this, exp);
                //throw new ArsEndpointUnexpectedException(exp);
            }
        }

        /// <summary>
        /// Validates the entity
        /// </summary>
        public void Validate() {
        }

        /// <summary>
        /// Updates the entity
        /// </summary>
        public void Update() {
            //Copies the bindings to the binding template
            BindingTemplateCollection bindingTemplateCollection = Bindings.GetBindingTemplates();
            this._businessService.BindingTemplates = bindingTemplateCollection;

            // 1. Verify properties
            Validate();

            // 2. Update in uddi
            try {
                SaveServiceEntity();
            }
            catch (Exception exp) {
                throw new UpdateArsEndpointFailedException(this, exp);
                //TODO: remove throw new ArsEndpointUnexpectedException(exp);
            }
        }

        /// <summary>
        /// Deletes all underlying UDDI entities associated with this endpoint
        /// </summary>
        public void Delete() {
                       
            //1. check that an id exists
            if (_businessService.Value.serviceKey.Length > 0) {
                
                //TODO: this is not needed in the new UDDI structure
                //2. delete related businessprocessinstances
                //DeleteRelatedBusinessProcessInstances();                

                string[] temp = new string[1];
                temp[0] = _businessService.Value.serviceKey;

                //2. call Publication.Delete
                try {
                    Publication uddiPublicationProxy = new Publication(Connection);
                    uddiPublicationProxy.DeleteService(temp);
                    _businessService = null; 
                }
                catch (DeleteServiceException) {
                    throw;
                }
                catch (PublicationUnexpectedException) {
                    throw;
                }
                catch (Exception exp) {
                    throw new ArsEndpointUnexpectedException(exp);
                }
            }
            else {
                throw new Exception("No service key was found");
            }
        }

        #endregion IRegistrationEntity

        #region IRegistrationEntity Members

        /// <summary>
        /// Validates the embedded data, and eventually returns a structured report if validations fails
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(string EntityName, ref dk.gov.oiosi.uddi.Validation.ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;

            BusinessService.IsValid(_businessService, "_business", ref ChildFailures);

            if (Bindings == null) {
                DataValidationFailure.AddFailure(MissingEndpointBindingFailure.Message(), EntityName, this.GetType(), ref Failures);

                if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

                return Failures == null;
            }
            Bindings.IsValid("_bindingSet", ref ChildFailures);
            if (Bindings.Bindings.Count == 0)
                DataValidationFailure.AddFailure(MissingEndpointBindingFailure.Message(), EntityName, Bindings.GetType(),
                    ref Failures);

            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            return Failures == null;
        }

        #endregion
    }
}