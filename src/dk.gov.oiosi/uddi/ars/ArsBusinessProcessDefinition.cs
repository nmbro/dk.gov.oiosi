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
  *//*
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
    /// Represents the Oasis v.2.0.2 recommendations for registering a WSDL in UDDI.
    /// </summary>
    public class ArsBusinessProcessDefinition : RegistrationEntity, IRegistrationEntity {
        
        private TModel _tModel;

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init() {
            _tModel = new TModel();
            SetDefaultProperties();
        }

        private void SetupConnection() {
        }

        /// <summary>
        /// Default constructor. Constructs emtpy internal 
        /// tModel entities.
        /// </summary>
        public ArsBusinessProcessDefinition() {
            Init();
            SetupConnection();
        }

        /// <summary>
        /// Constructor. Constructs the ArsBusinessProcessDefinition from the underlying tModel
        /// </summary>
        /// <param name="tModel">The underlying tModel</param>
        public ArsBusinessProcessDefinition(TModel tModel) {
            _tModel = tModel;
            SetupConnection();
        }

        /// <summary>
        /// Constructor that initializes a BusinessProcessDefinition with a specific porttype
        /// </summary>
        /// <param name="portTypeRegistrationTModelKey">porttype registration tmodel</param>
        public ArsBusinessProcessDefinition(UddiGuidId portTypeRegistrationTModelKey) {
            Init();
            
            //1. set the tmodelkey attribute on the tmodel
            _tModel.Value.tModelKey = portTypeRegistrationTModelKey.ID;
            SetupConnection();
        }

        /// <summary>
        /// Sets the default properties of this instance.
        /// </summary>
        private void SetDefaultProperties() {
            _tModel.CategoryBag.SetCategory(new RegistrationConformanceClaim(
                RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());
        }


        /// <summary>
        /// Gets the uuid of the port type registration
        /// </summary>
        public UddiId ID {
            get {
                if (_tModel == null || _tModel.Value == null || _tModel.Value.tModelKey == null ||
                    _tModel.Value.tModelKey.Length < 1) {
                    return null;
                } else {
                    return IdentifierUtility.GetUddiIDFromString(_tModel.Value.tModelKey);
                } 
            }
            set {
                if (value != null) {
                    _tModel.Value.tModelKey = value.ID;
                } else {
                    _tModel.Value.tModelKey = "";
                }
            }
        }

        /// <summary>
        /// Gets or sets the business process definition reference
        /// </summary>
        public BusinessProcessDocument BusinessProcessDocument {
            get {
                if (_tModel != null && _tModel.IdentifierBag != null) {
                    BusinessProcessDocument procRef = new BusinessProcessDocument();
                    KeyedReference keyref = _tModel.IdentifierBagAsCategoryBagObject.GetCategoryByIdentifierAndKeyName(procRef.IdentifierID, procRef.IdentifierName);
                    if (keyref != null) {
                        procRef.Value = keyref.KeyValue;
                        return procRef;
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
                    BusinessProcessDocument procRefSample = new BusinessProcessDocument();
                    CategoryBag identifiers = _tModel.IdentifierBagAsCategoryBagObject;
                    identifiers.RemoveCategoryById(procRefSample.IdentifierID);
                    _tModel.IdentifierBag = identifiers.GetInnerCollectionAsKeyedReferenceCollection();
                }
            }
        }


        /// <summary>
        /// Gets or sets the business process identifier type
        /// </summary>
        public BusinessProcessIdentifierType BusinessProcessIdentifierType {
            get {
                if (_tModel != null && _tModel.CategoryBag != null) {
                    BusinessProcessIdentifierType idType = new BusinessProcessIdentifierType();
                    KeyedReference keyref = _tModel.CategoryBag.GetCategoryByIdentifierAndKeyName(idType.CategoryID, idType.CategoryName);
                    if (keyref != null) {
                        idType.SetCategoryValue(keyref.KeyName, keyref.KeyValue);
                        return idType;
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
                    BusinessProcessIdentifierType idTypeSample = new BusinessProcessIdentifierType();
                    _tModel.CategoryBag.RemoveCategoryById(idTypeSample.CategoryID);
                }
            }
        }

        /// <summary>
        /// Description of the endpoint
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
        /// The name of the endpoint
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
                if (_tModel != null) {
                    _tModel.Name = value;
                }
            }
        }

        /// <summary>
        /// Saves all underlying UDDI registrations associated with this portType
        /// </summary>
        public void Save() {
            // 1. Validate
            Validate();
            SaveTModel();
        }        

        /// <summary>
        /// Validates all underlying UDDI registrations associated with this portType
        /// </summary>
        public void Validate() { }

        /// <summary>
        /// Updates all underlying UDDI registrations associated with this portType
        /// </summary>
        public void Update() {
            try {
                // Validate
                Validate();

                // Update
                SaveTModel();
            }
            catch (Exception exp) {
                throw new ArsBusinessProcessDefinitionUnexpectedException(exp);
            }
        }

        private void SaveTModel() {

            TModel[] tmodels = new TModel[1];
            tmodels[0] = _tModel;

            try {
                //1. call Publication                
                Publication uddiPublicationProxy = new Publication(Connection);
                tModelDetail detail = uddiPublicationProxy.Save(tmodels);
                _tModel.Value = detail.tModel[0];
            }
            catch (SaveTModelException) {
                throw;
            }
            catch (PublicationUnexpectedException) {
                throw;
            }
            catch (Exception exp) {
                throw new ArsBusinessProcessDefinitionUnexpectedException(exp);
            }
        }

        /// <summary>
        /// Deletes all underlying UDDI registrations associated with this portType
        /// NOTE: associated bindings are not deleted as part of this operation.
        /// </summary>
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
                    }
                    else {
                        throw new DeleteEntityException(_tModel.Value.tModelKey);
                    }
                }
                catch (DeleteTModelException) {
                    throw;
                }
                catch (PublicationUnexpectedException) {
                    throw;
                }
                catch (Exception exp) {
                throw new ArsBusinessProcessDefinitionUnexpectedException(exp);
            }
            }
            else {
                throw new DeleteEntityException("Entity has no identifier. Probably not instantiated");
            }
        }

        /// <summary>
        /// Gets the UDDI registration associated with this portType
        /// </summary>
        /// <param name="businessProcessDefinitionTModelKey">uuid of the process definition tmodel</param>
        /// <returns>the porttyperegistration</returns>
        public static ArsBusinessProcessDefinition Get(UddiId businessProcessDefinitionTModelKey) {

            //1. create a temp tmodel
            ArsBusinessProcessDefinition definition = null;

            //2. create a uddi inquire instance
            Inquiry inq = new Inquiry();

            //3. call gettmodeldetail
            TModel[] tmodels;
            try {
                GetTModelDetail detail = new GetTModelDetail(businessProcessDefinitionTModelKey.ID);
                tmodels = inq.GetDetail(detail.Value);

                if (tmodels.Length == 1) {
                    definition = new ArsBusinessProcessDefinition(tmodels[0]);
                    /*definition._tModel = tmodels[0];

                    CategoryBag bag = tmodels[0].CategoryBag;
                    KeyedReferenceCollection identifierBag = tmodels[0].IdentifierBag;

                    if (bag != null && identifierBag != null) {
                        KeyedReference processIdentifierType = bag.GetCategoryByName(new BusinessProcessIdentifierType().CategoryName);
                        definition.BusinessProcessIdentifierType = processIdentifierType.KeyValue;

                        foreach (KeyedReference keyref in identifierBag.GetInnerCollectionAsList()) {
                            if (keyref.KeyName == new BusinessProcessDocument().IdentifierName) {
                                definition.BusinessProcessDocument = keyref.KeyValue;
                            }
                        }
                    }*/
                }
            }
            catch (GetBusinessEntityException) {
                throw;
            }
            catch (InquiryUnexpectedException) {
                throw;
            }
            catch (Exception exp) {
                throw new ArsBusinessProcessDefinitionUnexpectedException(exp);
            }
            return definition;
        }
    
        /// <summary>
        /// Validates the embedded data, and eventually returns a structured report if validations fails
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(string EntityName, ref dk.gov.oiosi.uddi.Validation.ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;

            TModel.IsValid(_tModel, "_tModel", ref ChildFailures);

            if (ChildFailures != null)
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);

            return Failures == null;
        }
    }
}