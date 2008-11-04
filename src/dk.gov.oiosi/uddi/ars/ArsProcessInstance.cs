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
using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;
using dk.gov.oiosi.common.validation;
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.common;

namespace  dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Represents an ARS type process registration
    /// </summary>
    public class ArsProcessInstance : RegistrationEntity, IRegistrationEntity {
        private KeyedReference _roleReference = new KeyedReference("http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessRoleDefinition/", "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessRoleDefinition/", "uddi:bc3151a0-1144-11dd-a56f-32872391a563");
        private TModel _tModel = new TModel();

        #region Properties

        /// <summary>
        /// Gets the tmodelinstanceinfo
        /// </summary>
        public TModelInstanceInfo InstanceInfo {
            get {
                TModelInstanceInfo instanceInfo = new TModelInstanceInfo();
                instanceInfo.Value.tModelKey = _tModel.Value.tModelKey;
                return instanceInfo;
            }
        }

        /// <summary>
        /// Gets the uuid of the tmodel
        /// </summary>
        public UddiId ID {
            get {
                if (_tModel.Value.tModelKey != null && _tModel.Value.tModelKey.Length > 0)
                    return IdentifierUtility.GetUddiIDFromString(_tModel.Value.tModelKey);
                else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    _tModel.Value.tModelKey = value.ID;
                } else {
                    _tModel.Value.tModelKey = null;
                }
            }
        }

        /// <summary>
        /// The name of the model / instance
        /// </summary>
        public Name Name {
            get {
                if (_tModel != null) {
                    return _tModel.Name;
                } else {
                    return null;
                }
            }
            set {
                if (value == null) throw new ArgumentNullException("Name");
                _tModel.Name = value;
            }
        }

        /// <summary>
        /// Description of the model / instance
        /// </summary>
        public Description Description {
            get {
                if (_tModel.DescriptionCollection != null && _tModel.DescriptionCollection.Count > 0) {
                    return _tModel.DescriptionCollection[0];
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    DescriptionCollection descColl = new DescriptionCollection(value);
                    _tModel.DescriptionCollection = descColl;
                } else {
                    _tModel.DescriptionCollection = null;
                }
            }
        }

        /// <summary>
        /// Gets whether the process instance is a role definition
        /// </summary>
        /// <returns></returns>
        public bool GetIsBusinessProcessRoleDefinition() {
            KeyedReference[] categories = _tModel.CategoryBag.GetCategoryByIdentifier(_roleReference.TmodelKey);
            return categories != null && categories.Length > 0;
        }
        
        /// <summary>
        /// Sets that the process instance is a role definition
        /// </summary>
        /// <returns></returns>
        public void SetIsBusinessProcessRoleDefinition(bool isBusinessProcessRoleDefinition) {
            _tModel.CategoryBag.RemoveCategoryById(_roleReference.TmodelKey);
            if (isBusinessProcessRoleDefinition) {
                _tModel.CategoryBag.AddCategory(_roleReference);
            }
        }

        /// <summary>
        /// Gets the role identifier
        /// </summary>
        public BusinessProcessRoleIdentifier ProcessRoleIdentifier {
            get {
                if (_tModel != null && _tModel.IdentifierBag != null &&
                    _tModel.IdentifierBag.Value.Items.Length > 0
                ) {
                    BusinessProcessRoleIdentifier roleId = new BusinessProcessRoleIdentifier();
                    KeyedReference keyRef =
                        _tModel.IdentifierBagAsCategoryBagObject.GetCategoryByIdentifierAndKeyName(roleId.IdentifierID, roleId.IdentifierName);
                    if (keyRef != null) {
                        return new BusinessProcessRoleIdentifier(keyRef.KeyValue);
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    if (_tModel.IdentifierBag == null) {
                        CategoryBag identifiers = new CategoryBag();
                        identifiers.SetCategory(value.GetAsKeyedReference());
                        _tModel.IdentifierBag = identifiers.GetInnerCollectionAsKeyedReferenceCollection();
                    } else {
                        CategoryBag identifiers = _tModel.IdentifierBagAsCategoryBagObject;
                        identifiers.SetCategory(value.GetAsKeyedReference());
                        _tModel.IdentifierBag = identifiers.GetInnerCollectionAsKeyedReferenceCollection();
                    }
                } else {
                    CategoryBag identifiers = _tModel.IdentifierBagAsCategoryBagObject;
                    if (identifiers != null) {
                        identifiers.RemoveCategoryById(new BusinessProcessRoleIdentifier().IdentifierID);
                        _tModel.IdentifierBag = identifiers.GetInnerCollectionAsKeyedReferenceCollection();
                    }
                }
            }
        }

        //private string _processRoleIdentifierType;

        /// <summary>
        /// Gets the role identifier type
        /// </summary>
        public BusinessProcessRoleIdentifierType ProcessRoleIdentifierType {
            get {
                if (_tModel != null && _tModel.CategoryBag != null &&
                    _tModel.CategoryBag.Value.Items.Length > 0
                ) {
                    BusinessProcessRoleIdentifierType roleId = new BusinessProcessRoleIdentifierType();
                    KeyedReference keyRef =
                        _tModel.CategoryBag.GetCategoryByIdentifierAndKeyName(roleId.CategoryID, roleId.CategoryName);
                    if (keyRef != null) {
                        return new BusinessProcessRoleIdentifierType(keyRef);
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    if (_tModel.CategoryBag == null) {
                        _tModel.CategoryBag = new CategoryBag();
                    }
                    _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    _tModel.CategoryBag.RemoveCategoryById(
                        new BusinessProcessRoleIdentifierType().CategoryID);
                }
            }
        }

        //private string _processDefinitionReference;

        /// <summary>
        /// Gets the process definition reference
        /// </summary>
        public BusinessProcessDefinitionReference ProcessDefinitionReference {
            get {
                if (_tModel != null && _tModel.CategoryBag != null &&
                    _tModel.CategoryBag.Value.Items.Length > 0
                ) {
                    BusinessProcessDefinitionReference roleId = new BusinessProcessDefinitionReference();
                    KeyedReference keyRef =
                        _tModel.CategoryBag.GetCategoryByIdentifierAndKeyName(roleId.CategoryID, roleId.CategoryName);
                    if (keyRef != null) {
                        return new BusinessProcessDefinitionReference(keyRef);
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    if (_tModel.CategoryBag == null) {
                        _tModel.CategoryBag = new CategoryBag();
                    }
                    _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    _tModel.CategoryBag.RemoveCategoryById(
                        new BusinessProcessDefinitionReference().CategoryID);
                }
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ArsProcessInstance() {
            _tModel = new TModel();
            _tModel.CategoryBag = new CategoryBag();
            SetDefaultProperties();
        }

        /// <summary>
        /// Constructor used when initializing an excisting processinstance
        /// </summary>
        /// <param name="tmodel">an existing tmodel</param>
        public ArsProcessInstance(TModel tmodel) {
            try {
                _tModel = tmodel;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Sets the default properties
        /// </summary>
        private void SetDefaultProperties() {
            _tModel.CategoryBag.SetCategory(new RegistrationConformanceClaim(
                RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());
        }

        /// <summary>
        /// Constructor used when creating a new processinstance
        /// </summary>
        /// <param name="businessProcessRoleIdentifer">Identifies a specific role that the service 
        /// provider takes within a specific business process. </param>
        /// <param name="businessProcessRoleIdentifierType">Identifies a type of role identifier. The type 
        /// indicates the format of the role identifier</param>
        /// <param name="businessProcessDefinitionReference">A reference to the business process 
        /// definition tModel</param>
        /// <param name="tModelName">the name of the tmodel</param>
        /// <param name="descriptions">the descriptions of the tmodel</param>
        public ArsProcessInstance(BusinessProcessRoleIdentifier businessProcessRoleIdentifer,
            BusinessProcessRoleIdentifierType businessProcessRoleIdentifierType,
            BusinessProcessDefinitionReference businessProcessDefinitionReference,
            Name tModelName, DescriptionCollection descriptions) {
            try {
                //1. set tmodel attributes, names and descriptions
                SetProcessInstanceDetails(businessProcessRoleIdentifer, businessProcessRoleIdentifierType,
                    businessProcessDefinitionReference, tModelName, descriptions);
            } catch {
                throw;
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the UDDI registration associated with this process instance
        /// </summary>
        /// <param name="processInstanceId">The ID of the process instance</param>
        /// <returns>Returns the process instance</returns>
        public static ArsProcessInstance Get(UddiId processInstanceId) {

            //0. create a temp processinstance
            ArsProcessInstance tempProcessInstance = null;

            //1. create a uddi inquire instance
            Inquiry inq = new Inquiry();

            //2. call gettmodeldetail
            TModel[] tmodels;
            try {
                GetTModelDetail detail = new GetTModelDetail(processInstanceId.ID);
                tmodels = inq.GetDetail(detail.Value);

                if (tmodels.Length == 1) {
                    tempProcessInstance = new ArsProcessInstance(tmodels[0]);
                }
            } catch (GetTModelDetailException) {
                throw;
            } catch (InquiryUnexpectedException) {
                throw;
            } catch (Exception) {
                throw;
            }
            return tempProcessInstance;
        }

        /// <summary>
        /// This method is used to gather all attributes and call there relative set methods
        /// </summary>
        /// <exception cref="ArsProcessInstanceUnexpectedException">Thrown if an unexpected error occures</exception>
        private void SetProcessInstanceDetails(BusinessProcessRoleIdentifier businessProcessRoleIdentifer,
            BusinessProcessRoleIdentifierType businessProcessRoleIdentifierType,
            BusinessProcessDefinitionReference businessProcessDefinitionReference,
            Name tModelName, DescriptionCollection descriptions) {

            try {
                //1. set custom attributes
                SetCustomAttributes(businessProcessRoleIdentifer, businessProcessRoleIdentifierType,
                    businessProcessDefinitionReference);

                //2. set name
                SetName(tModelName);

                //3. description
                SetDescriptions(descriptions);
            } catch (Exception ex) {
                throw new ArsProcessInstanceUnexpectedException(ex);
            }
        }

        /// <summary>
        /// Sets all custom attributes for the tmodel; categories and identifiers
        /// </summary>
        /// <exception cref="ArsProcessInstanceUnexpectedException">Thrown if an unexpected error occures</exception>
        private void SetCustomAttributes(BusinessProcessRoleIdentifier businessProcessRoleIdentifer,
            BusinessProcessRoleIdentifierType businessProcessRoleIdentifierType,
            BusinessProcessDefinitionReference businessProcessDefinitionReference) {

            try {

                //1. set categorybag
                _tModel.CategoryBag = new CategoryBag();

                SetDefaultProperties();

                _tModel.CategoryBag.SetCategory(businessProcessRoleIdentifierType.GetAsKeyedReference());
                _tModel.CategoryBag.SetCategory(businessProcessDefinitionReference.GetAsKeyedReference());

                //2. set identifierbag
                KeyedReferenceCollection identifiers = new KeyedReferenceCollection();
                identifiers.Add(businessProcessRoleIdentifer.GetAsKeyedReference());
                _tModel.IdentifierBag = identifiers;
            } catch (Exception ex) {
                throw new ArsProcessInstanceUnexpectedException(ex);
            }
        }

        /// <summary>
        /// Sets the name of the tmodel
        /// </summary>
        /// <exception cref="ArsProcessInstanceUnexpectedException">Thrown if an unexpected error occures</exception>
        private void SetName(Name tModelName) {

            //1. add all names to the businessservice
            try {
                if (tModelName != null) {
                    _tModel.Name = tModelName;
                }
            } catch (Exception ex) {
                throw new ArsProcessInstanceUnexpectedException(ex);
            }
        }

        /// <summary>
        /// Sets the description of the tmodel
        /// </summary>
        /// <exception cref="ArsProcessInstanceUnexpectedException">Thrown if an unexpected error occures</exception>
        private void SetDescriptions(DescriptionCollection descriptions) {

            //1. add all descriptions to the businessservice
            try {
                if (descriptions != null) {
                    _tModel.DescriptionCollection = descriptions;
                }
            } catch (Exception ex) {
                throw new ArsProcessInstanceUnexpectedException(ex);
            }
        }

        #endregion Methods

        #region IRegistrationEntity

        /// <summary>
        /// Saves the process registration
        /// </summary>
        public void Save() {
            try {
                // 1. Verify properties
                Validate();

                // 3. Save
                SaveTModel();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Validates the process registration
        /// </summary>
        public void Validate() {
            try {
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Updates the process registration
        /// </summary>
        public void Update() {
            try {
                // 1. Verify properties
                Validate();

                // 3. Update in uddi
                SaveTModel();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Deletes the associated UDDI tModel representing this business process instance
        /// </summary>
        /// <exception cref="DeleteEntityException">This exception is thrown if an error 
        /// during deletion is thrown</exception>
        /// <exception cref="ArsProcessInstanceUnexpectedException">Thrown if a unexpected error occures</exception>
        /// <exception cref="DeleteEntityException">Thrown if an error occured during deletion of an entity</exception>
        public void Delete() {

            //1. check that an id exists
            if (_tModel.Value.tModelKey.Length > 0) {

                string[] temp = new string[1];
                temp[0] = _tModel.Value.tModelKey;

                //2. call Publication.Delete
                try {
                    Publication uddiPublicationProxy = new Publication(Connection);
                    bool deleted = uddiPublicationProxy.DeleteTModel(temp);

                    //3. removes the tmodel
                    if (deleted) {
                        _tModel = null;
                    } else {
                        throw new DeleteEntityException(_tModel.Value.tModelKey);
                    }
                } catch (System.ServiceModel.FaultException<uddiorg.api_v3.dispositionReport>) {
                    throw;
                } catch (DeleteTModelException) {
                    throw;
                } catch (PublicationUnexpectedException) {
                    throw;
                } catch (Exception ex) {
                    throw new ArsProcessInstanceUnexpectedException(ex);
                }
            } else {
                throw new DeleteEntityException("Entity has no identifier. Probably not instantiated");
            }
        }

        /// <summary>
        /// Saves or updates the tmodel in the UDDI registry
        /// </summary>
        /// <exception cref="ArsProcessInstanceUnexpectedException">Thrown if a unexpected error occures</exception>
        private void SaveTModel() {

            TModel[] tmodels = new TModel[1];
            tmodels[0] = _tModel;

            try {

                //1. call Publication
                Publication uddiPublicationProxy = new Publication(Connection);
                tModelDetail detail = uddiPublicationProxy.Save(tmodels);
                _tModel = new TModel(detail.tModel[0]);
            } catch (SaveTModelException) {
                throw;
            } catch (PublicationUnexpectedException) {
                throw;
            } catch (Exception ex) {
                throw new ArsProcessInstanceUnexpectedException(ex);
            }
        }

        /// <summary>
        ///  Validates the embedded data, and eventually returns a structured report if validations fails
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(string EntityName, ref ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;
            bool Valid = true;

            TModel.IsValid(_tModel, "_tModel", ref ChildFailures);

            if (ChildFailures != null){
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);
                Valid = false;
            }

            return Valid;
        }

        #endregion IRegistrationEntity
    }
}