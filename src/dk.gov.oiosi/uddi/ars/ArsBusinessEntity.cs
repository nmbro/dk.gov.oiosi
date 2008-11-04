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
using dk.gov.oiosi.communication;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.Businesses;
using dk.gov.oiosi.uddi.ars;
using dk.gov.oiosi.uddi.Services;
using dk.gov.oiosi.common.validation;
using dk.gov.oiosi.uddi.Validation;

namespace  dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Represents an ARS UDDI business entity registration. This class only holds the 
    /// business entity registration, not the services included in this.
    /// </summary>
    public class ArsBusinessEntity : RegistrationEntity, IRegistrationEntity {
        private BusinessEntity _businessEntity = new BusinessEntity();

        /// <summary>
        /// Default constructor. Creates an empty business entity with a random id.
        /// </summary>
        public ArsBusinessEntity() {
            // Create new instance with new ID
            _businessEntity = new BusinessEntity();
            _businessEntity.CategoryBag = new CategoryBag();
            SetDefaultProperties();
            Init();
        }

        /// <summary>
        /// Constructor. Constructs an ArsBusinessEntity from a BusinessEntity object.
        /// </summary>
        /// <param name="businessEntity">The BusinessEntity object</param>
        public ArsBusinessEntity(BusinessEntity businessEntity) {
            Init();
            _businessEntity = businessEntity;
        }

        /// <summary>
        /// Sets default properties
        /// </summary>
        private void SetDefaultProperties() {
            this.RegistrationConformanceClaim 
                = new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1);
        }


        /// <summary>
        /// Constructor, taking the values to set on the businessentity. 
        /// This constructor sets a default registration conformance claim of
        /// OIOSI 1.1. 
        /// </summary>
        /// <param name="oioTaxonomy">OIO taxonomy</param>
        /// <param name="organizationKey">Organization identifier</param>
        /// <param name="organizationKeyType">Type of the organization identifier</param>
        /// <param name="businessEntityContact">Contact for the entity</param>
        /// <param name="businessDiscoveryUrl">discovery url</param>
        /// <param name="businessName">Name of the businessentity</param>
        /// <param name="description">descriptions of the entity</param>
        public ArsBusinessEntity(OIOTaxonomy oioTaxonomy,
                                 OrganizationKey organizationKey,
                                 OrganizationKeyType organizationKeyType,
                                 UddiContactSimple businessEntityContact,
                                 UddiDiscoveryUrl businessDiscoveryUrl,
                                 Name businessName,
                                 Description description
        ) {
            //1. set businessentity attributes, contacts and discoveryUrl
            RegistrationConformanceClaim = new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1);

            SetBusinessEntityDetails(RegistrationConformanceClaim,
                oioTaxonomy,
                organizationKey,
                organizationKeyType,
                businessEntityContact,
                businessDiscoveryUrl,
                businessName,
                description
            );
        } 

        /// <summary>
        /// Constructor, taking the values to set on the businessentity. Some of these values are raw
        /// Uddi 3.0 types
        /// </summary>
        /// <param name="registrationConformanceClaim">Claims regarding conformance to the UDDI registration model</param>
        /// <param name="oioTaxonomy">OIO taxonomy</param>
        /// <param name="organizationKey">Organization identifier</param>
        /// <param name="organizationKeyType">Type of the organization identifier</param>
        /// <param name="businessEntityContact">Contact for the entity</param>
        /// <param name="businessDiscoveryUrl">discovery url</param>
        /// <param name="businessName">Name of the businessentity</param>
        /// <param name="description">descriptions of the entity</param>
        public ArsBusinessEntity(
            RegistrationConformanceClaim registrationConformanceClaim,
            OIOTaxonomy oioTaxonomy,
            OrganizationKey organizationKey,
            OrganizationKeyType organizationKeyType,
            UddiContactSimple businessEntityContact,
            UddiDiscoveryUrl businessDiscoveryUrl,
            Name businessName,
            Description description
        ) {
            //1. set businessentity attributes, contacts and discoveryUrl
            SetBusinessEntityDetails(
                registrationConformanceClaim,
                oioTaxonomy,
                organizationKey,
                organizationKeyType,
                businessEntityContact,
                businessDiscoveryUrl,
                businessName,
                description
            );
        }

        #region custom attributes

        /// <summary>
        /// Gets the registration conformance claim regarding Uddi registration model
        /// </summary>
        public RegistrationConformanceClaim RegistrationConformanceClaim {
            get {
                if (_businessEntity != null && _businessEntity.CategoryBag != null) {
                    RegistrationConformanceClaim regClaim = new RegistrationConformanceClaim();

                    KeyedReference keyRef = _businessEntity.CategoryBag.GetCategoryByIdentifierAndKeyName(regClaim.CategoryID, regClaim.CategoryName);
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
                RegistrationConformanceClaim test = new RegistrationConformanceClaim();
                if (value != null) {
                    if (_businessEntity == null) return;
                    // Check that category ID is valid:
                    
                    if (value.CategoryID.ToLower() != test.CategoryID.ToLower()) {
                        
                        throw new Exception("Cannot set registration conformance claim: " +
                            "Tried to set with wrong category ID ('" + value.CategoryID + "')");
                    }

                    if (_businessEntity.CategoryBag == null) {
                        _businessEntity.CategoryBag = new CategoryBag();
                    }

                    _businessEntity.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    _businessEntity.CategoryBag.RemoveCategoryById(test.CategoryID);
                }
            }
        }

        /// <summary>
        /// Gets or sets the OIO Taxonomy
        /// </summary>
        public OIOTaxonomy OIOTaxonomy { 
            get {
                if (_businessEntity == null && _businessEntity.CategoryBag == null) return null;
                OIOTaxonomy tax = new OIOTaxonomy();
                KeyedReference[] keyRef = _businessEntity.CategoryBag.GetCategoryByIdentifier(tax.CategoryID);
                if (keyRef == null || keyRef.Length < 1) return null;
                if (keyRef.Length > 1) throw new Exception("Too many OIO taxonomies found.");
                tax.SetCategoryValue(keyRef[0].KeyName, keyRef[0].KeyValue);
                return tax;
            } 
            set {
                if (value != null) {
                    SetCategory(value.GetAsKeyedReference().Value);
                } else {
                    SetCategory(null);
                }
            } 
        }

        /// <summary>
        /// Gets or sets the organization key
        /// </summary>
        public OrganizationKey OrganizationKey {
            get {
                if (_businessEntity != null && _businessEntity.IdentifierBag != null) {
                    OrganizationKey orgKey = new OrganizationKey();
                    KeyedReference keyRef =
                        _businessEntity.IdentifierBag.GetCategoryByIdentifierAndKeyName(orgKey.IdentifierID, orgKey.IdentifierName);
                    if (keyRef == null) {
                        return null;
                    } else {
                        orgKey.Value = keyRef.KeyValue;
                        return orgKey;
                    }
                } else {
                    return null;
                }
            }
            set {
                SetIdentifier(value.GetAsKeyedReference().Value);
            } 
        }


        /// <summary>
        /// Gets or sets the organization keytype
        /// </summary>
        public OrganizationKeyType OrganizationKeyType {
            get {
                if (_businessEntity != null && _businessEntity.CategoryBag != null) {
                    OrganizationKeyType keyType = new OrganizationKeyType();
                    KeyedReference keyRef =
                        _businessEntity.CategoryBag.GetCategoryByIdentifierAndKeyName(keyType.CategoryID, keyType.CategoryName);
                    if (keyRef == null) {
                        return null;
                    } else {
                        keyType.SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
                        return keyType;
                    }
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    SetCategory(value.GetAsKeyedReference().Value);
                } else {
                    SetCategory(null);
                }
            } 
        }


        /// <summary>
        /// Adds an identifier to the identifier bag
        /// </summary>
        /// <param name="uddiKeyRef">The keyed reference to add to the identifier bag</param>
        private void SetIdentifier(keyedReference uddiKeyRef) {
            KeyedReference keyRef = new KeyedReference(uddiKeyRef);
            if (keyRef != null) {
                if (_businessEntity != null) {
                    if (_businessEntity.IdentifierBag == null) {
                        _businessEntity.IdentifierBag = new CategoryBag(keyRef);
                    } else {
                        _businessEntity.IdentifierBag.SetCategory(keyRef);
                    }
                } else {
                    return;
                }
            } else {
                if (_businessEntity != null && _businessEntity.IdentifierBag != null) {
                    _businessEntity.IdentifierBag.RemoveCategory(keyRef);
                } else {
                    return;
                }
            }
        }


        /// <summary>
        /// Adds a category to the category bag
        /// </summary>
        /// <param name="uddiKeyRef">The keyed reference</param>
        public void SetCategory(keyedReference uddiKeyRef) {
            KeyedReference keyRef = new KeyedReference(uddiKeyRef);
            if (keyRef != null) {
                if (_businessEntity != null) {
                    if (_businessEntity.CategoryBag == null) {
                        _businessEntity.CategoryBag = new CategoryBag(keyRef);
                    } else {
                        _businessEntity.CategoryBag.SetCategory(keyRef);
                    }
                } else {
                    return;
                }
            } 
        }

        /// <summary>
        /// Adds a category to the category bag
        /// </summary>
        public CategoryBag GetCategoryBag() {
            if (_businessEntity == null) return new CategoryBag();
            return _businessEntity.CategoryBag;
        }


        #endregion custom attributes

        #region standard uddi attributes


        /// <summary>
        /// The name of the business entity
        /// </summary>
        public Name Name {
            get {
                if (_businessEntity.Names != null && _businessEntity.Names.Length > 0) {
                    return _businessEntity.Names[0];
                } else {
                    return new Name();
                }
            }
            set {
                if (value != null) {
                    _businessEntity.Names = new Name[] { value };
                } else {
                    _businessEntity.Names = null;
                }
            }
        }

        /// <summary>
        /// Description of the business entity
        /// </summary>
        public Description Description {
            get {
                if (_businessEntity.Descriptions != null && _businessEntity.Descriptions.Length > 0) {
                    return _businessEntity.Descriptions[0];
                } else {
                    return new Description();
                }

            }
            set {
                if (value != null) {
                    _businessEntity.Descriptions = new Description[] { value };
                } else {
                    _businessEntity.Descriptions = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the business entity contact information
        /// </summary>
        public UddiContactSimple Contact {
            get {
                if (_businessEntity.Provider.contacts != null
                    && _businessEntity.Provider.contacts.Length > 0) {
                    return new UddiContactSimple(_businessEntity.Provider.contacts[0]);
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    _businessEntity.Provider.contacts = new contact[] { value.Value };
                } else {
                    _businessEntity.Provider.contacts = null;
                }
            }
        }


        /// <summary>
        /// Gets or sets the homepage of the business entity
        /// </summary>
        public UddiDiscoveryUrl Homepage {
            get {
                if (_businessEntity.Provider.discoveryURLs != null
                    && _businessEntity.Provider.discoveryURLs.Length > 0) {
                    return new UddiDiscoveryUrl(_businessEntity.Provider.discoveryURLs[0]);
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    _businessEntity.Provider.discoveryURLs = new discoveryURL[] { value.Value };
                } else {
                    _businessEntity.Provider.discoveryURLs = null;
                }
            }
        }

        #endregion

        /// <summary>
        /// The Endpoints of the entity
        /// </summary>
        //public ArsEndpointSet Endpoints = new ArsEndpointSet();
        public ArsEndpointSet Endpoints {
            get {
                if (_businessEntity != null && _businessEntity.Services != null && _businessEntity.Services.Length > 0) {
                    return new ArsEndpointSet(_businessEntity.Services);
                } else {
                    return new ArsEndpointSet();
                }
            }
            set {
                if (value != null && value.Endpoints != null && value.Endpoints.Count > 0) {
                    BusinessService[] services = new BusinessService[value.Endpoints.Count];
                    for (int i = 0; i < value.Endpoints.Count; i++) {
                        services[i] = value.Endpoints[i].Service;
                    }
                    _businessEntity.Services = services;
                } else {
                    _businessEntity.Services = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the uuid of the entity
        /// </summary>
        public UddiId ID {
            get {
                if (_businessEntity!= null && _businessEntity.Provider.businessKey != null && 
                    _businessEntity.Provider.businessKey.Length > 0)
                    return new UddiGuidId(_businessEntity.Provider.businessKey, true);
                else {
                    return null;
                }
            }
            set {
                if (value == null) {
                    _businessEntity.Provider.businessKey = null;
                } else {
                    _businessEntity.Provider.businessKey = value.ID;
                }
            }
        }

        /// <summary>
        /// Saves the entity to the Uddi registry. Only saves this entity, not e.g. associated endpoints and processes.
        /// </summary>
        public void Save() {
            //Verify properties
            Validate();

            //Save
            try {
                
                SaveBusinessEntity();
            }
            catch {
                throw;
            }
        }        

        /// <summary>
        /// Validates a entity
        /// </summary>
        public void Validate() {
        }

        /// <summary>
        /// Updates a entity in the UDDI registry
        /// </summary>
        public void Update() {
            // 1. Verify properties
            Validate();

            // 2. Update
            try {
                SaveBusinessEntity();
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Deletes a entity and all underlying binding registrations and portTypeRegistrations
        /// </summary>
        public void Delete() {

            //1. check that an id exists
            if (_businessEntity.Provider.businessKey.Length > 0) {

                string[] temp = new string[1];
                temp[0] = _businessEntity.Provider.businessKey;

                //2. call Publication.Delete
                try {
                    Publication uddiPublicationProxy = new Publication(Connection);
                    bool deleted = uddiPublicationProxy.DeleteBusinessEntity(temp);
                    if (deleted) {
                        _businessEntity = null;
                    }
                }
                catch (DeleteBusinessException) {
                    throw;
                }
                catch (PublicationUnexpectedException) {
                    throw;
                }
                catch (Exception exp) {
                    throw new ArsBusinessEntityUnexpectedException(exp);
                }
            }
            else {
                throw new Exception("No businessentity key was found");
            }
        }

        /// <summary>
        /// Deletes the specified service, and removes the reference to it from this business entity
        /// </summary>
        /// <param name="endpoint">The endpoint to be deleted</param>
        public void DeleteEndpoint(ArsEndpoint endpoint) {
            string[] businessServiceKey = new string[1];
            businessServiceKey[0] = endpoint.ID.ID;
            bool _endpointFoundAndDeleted = false;            

            //1. check that the endpoint exists on the businessentity            
            try {
                ArsEndpointSet finalEndpointSet = new ArsEndpointSet();
                ArsEndpointSet currentEndpointSet = Endpoints;

                for (int i = 0; i < currentEndpointSet.Endpoints.Count; i++) {

                    if (currentEndpointSet.Endpoints[i].ID.ID == endpoint.ID.ID) {
                        currentEndpointSet.Endpoints[i].Delete();
                        _endpointFoundAndDeleted = true;
                    } else {
                        finalEndpointSet.Add(currentEndpointSet.Endpoints[i]);
                    }
                }

                // Set the updated list of endpoints:
                Endpoints = finalEndpointSet;

                if (!_endpointFoundAndDeleted) {
                    throw new Exception("An error occured during deletion of endpoint: " +
                        endpoint.ID.ID + ". It was either not found or couldn't be deleted");
                }
            }
            catch (DeleteServiceException) {
                throw;
            }
            catch (PublicationUnexpectedException) {
                throw;
            }
            catch (Exception exp) {
                throw new ArsBusinessEntityUnexpectedException(exp);
            }
            //return this;
        }

        /// <summary>
        /// get a ArsBusinessEntity
        /// </summary>
        /// <param name="businessEntityId">the uuid of the entity to retrieve</param>
        /// <param name="getRecursively">If getRecursively is true, all subentities are got as well, i.e.
        /// all ArsEndpoint objects</param>
        /// <returns>a businessentity</returns>
        public static ArsBusinessEntity Get(UddiId businessEntityId, bool getRecursively)
        {
            
            //1. create a temp businessentity
            ArsBusinessEntity tempBusinessEntity = new ArsBusinessEntity();

            //2. create a uddi inquire instance
            Inquiry inq = new Inquiry();

            //3. call getbusinessdetail
            BusinessEntity[] entities;
            try {
                GetBusinessDetail detail = new GetBusinessDetail(businessEntityId.ID);
                entities = inq.GetDetail(detail.GetProviderDetail);

                if (entities != null && entities.Length == 1) {
                    tempBusinessEntity._businessEntity = entities[0];
                } else
                    return null;
            }
            catch (GetBusinessEntityException) {
                throw;
            }
            catch (InquiryUnexpectedException) {
                throw;
            }
            catch (Exception exp) {
                throw new ArsBusinessEntityUnexpectedException(exp);
            }

            //4. make a if / else for recursively
            if (getRecursively) {

                //5. get ArsEndpoints
                tempBusinessEntity.Endpoints = new ArsEndpointSet(tempBusinessEntity._businessEntity.Services);
            }

            return tempBusinessEntity;
        }

        private void SaveBusinessEntity() {
            BusinessEntity[] providers = new BusinessEntity[1];
            providers[0] = _businessEntity;

            try {
                //1. call Publication
                Publication uddiPublicationProxy = new Publication(this.Connection);
                businessDetail detail = uddiPublicationProxy.Save(providers);
                _businessEntity = new BusinessEntity(detail.businessEntity[0]);
            }
            catch (SaveBusinessEntityException) {
                throw;
            }
            catch (PublicationUnexpectedException) {
                throw;
            }
            catch (Exception exp) {
                throw new ArsBusinessEntityUnexpectedException(exp);
            }
        }

        /// <summary>
        /// Sets the specified endpoint. If the endoint does not exist in the business entity, it is added.
        /// If an endpoint with the same ID exists, it is replaced with the specified endpoint.
        /// NOTE: Replacement only works for endpoints with an UDDI ID. Newly created endpoints without this ID are simply added.
        /// Use ReplaceEndpoint() if you update in-memory endpoint objects without UDDI ID's.
        /// </summary>
        /// <param name="endpoint">The endpoint to set</param>
        public void SetEndpoint(ArsEndpoint endpoint) {
            if (endpoint == null) return;
            // 1. Does an endpoint with the same ID exist?
            if (endpoint.ID != null) {
                foreach (ArsEndpoint existingEndpoint in Endpoints.Endpoints) {
                    // If a match was found, replace the data
                    if (existingEndpoint.ID != null) {
                        if (existingEndpoint.ID.ID == endpoint.ID.ID) {
                            existingEndpoint.Service.Value = endpoint.Service.Value;
                            return;
                        }
                    } 
                }
            }

            // 2. If no match was found, add the endpoint to the endpoint collection
            ArsEndpointSet endpointSet = Endpoints;
            endpointSet.Add(endpoint);
            Endpoints = endpointSet;
        }


        /// <summary>
        /// Replaces an existing endpoint with a new endpoint. If the specified existing endpoint does not exist, the new endpoint is added instead.
        /// Comparison is reference-based, not based on the ID of the endpoint.
        /// </summary>
        /// <param name="existingEndpoint">The existing endpoint</param>
        /// <param name="newEndpoint">The new endpoint</param>
        public void ReplaceEndpoint(ArsEndpoint existingEndpoint, ArsEndpoint newEndpoint) {
            if (existingEndpoint == null || newEndpoint == null) return;
            
            foreach (ArsEndpoint endpoint in Endpoints.Endpoints) {
                // If a match was found, replace the data
                if (existingEndpoint.Service.Value == endpoint.Service.Value) {
                    existingEndpoint.Service.Value = newEndpoint.Service.Value;
                    return;
                }
            }

            // If no match was found, add the endpoint instead
            ArsEndpointSet endpointSet = Endpoints;
            endpointSet.Add(newEndpoint);
            Endpoints = endpointSet;

        }

        /// <summary>
        /// This method sets all the businessentities values
        /// </summary>
        /// <param name="registrationConformanceClaim">Claims regarding conformance to the UDDI registration model</param>
        /// <param name="oioTaxonomy">OIO taxonomy</param>
        /// <param name="organizationKey">Organization identifier</param>
        /// <param name="organizationKeyType">Organization keytype</param>
        /// <param name="businessEntityContact">Entity contact</param>
        /// <param name="businessDiscoveryUrl">Discovery Url</param>
        /// <param name="description">Description text</param>
        /// <param name="businessName">Name of the business entity</param>
        public void SetBusinessEntityDetails(
            RegistrationConformanceClaim registrationConformanceClaim,
            OIOTaxonomy oioTaxonomy, 
            OrganizationKey organizationKey, 
            OrganizationKeyType organizationKeyType, 
            UddiContactSimple businessEntityContact, 
            UddiDiscoveryUrl businessDiscoveryUrl,
            Name businessName,
            Description description
        ) {
            
            
            try {
                //1. set custom attributes
                SetCustomAttributes(registrationConformanceClaim, oioTaxonomy, organizationKey,
                    organizationKeyType);

                //2. set contact
                Contact = businessEntityContact;

                //3. set discoveryUrl
                Homepage = businessDiscoveryUrl;

                //4. set names
                Name = businessName;

                //5. set description
                Description = description;
            }
            catch (Exception exp) {
                throw new ArsBusinessEntityUnexpectedException(exp);
            }
        }

        /// <summary>
        /// Sets all custom attributes for the businessentity; categories and identifiers
        /// </summary>
        /// <param name="registrationConformanceClaim">registrationconformanceclaim profile</param>
        /// <param name="oioTaxonomy">OIO taxonomy</param>
        /// <param name="organizationKey">Specific organizationkey</param>
        /// <param name="organizationKeyType">organizationkeytype</param>
        private void SetCustomAttributes(RegistrationConformanceClaim registrationConformanceClaim, 
            OIOTaxonomy oioTaxonomy, OrganizationKey organizationKey, 
            OrganizationKeyType organizationKeyType) {
                        
            try {
                //1. set categorybag
                CategoryBag categoryBag = new CategoryBag();
                this.RegistrationConformanceClaim = registrationConformanceClaim;
                categoryBag.AddCategory(oioTaxonomy.GetAsKeyedReference());
                categoryBag.AddCategory(organizationKeyType.GetAsKeyedReference());
                categoryBag.AddCategory(registrationConformanceClaim.GetAsKeyedReference());

                //2. set identifierbag
                KeyedReferenceCollection identifierBag = new KeyedReferenceCollection();
                identifierBag.Add(organizationKey.GetAsKeyedReference());

                //3. add categorybag to businessentity
                _businessEntity.Provider.categoryBag = categoryBag.Value;

                //4. add identifierbag to businessentity
                _businessEntity.Provider.identifierBag = identifierBag.GetKeyedReferenceCollection();
            }
            catch (Exception exp) {
                throw new ArsBusinessEntityUnexpectedException(exp);
            }
        }

        private void Init() {}

        #region IRegistrationEntity Members


        /// <summary>
        /// Validates the embedded data, and eventually returns a structured report if validations fails
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(string EntityName, ref ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;
            bool Valid = true;

            RegistrationConformanceClaim.IsValid("RegistrationConformanceClaim", ref Failures);
            OrganizationKey.IsValid("OrganizationKey", ref Failures);
            OIOTaxonomy.IsValid("OIO taksonomi", ref Failures);
            BusinessEntity.IsValid(_businessEntity, "_businessEntity", ref ChildFailures);

            Endpoints.IsValid("Endpoints", ref ChildFailures);

            if (ChildFailures != null){
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, typeof(BusinessEntity),
                    ChildFailures, ref Failures);
                Valid = false;
            }

            return Valid;
        }

        #endregion
    }
}