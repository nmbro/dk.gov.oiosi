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
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.Services;
using dk.gov.oiosi.uddi.ars;
using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.common.validation;
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Represents an ARS-profiled UDDI bindingTemplate
    /// </summary>
    public class ArsBinding : RegistrationEntity, IRegistrationEntity {
        private BindingTemplate _bindingTemplate = null;
        private OasisServiceSingleBindingRegistration _serviceRegistration = null;
        private ArsProcessInstanceSet _processInstanceSet = new ArsProcessInstanceSet();

        #region properties

        /// <summary>
        /// Returns the underlying BindingTemplate object
        /// </summary>
        public BindingTemplate BindingTemplate {
            get { return _bindingTemplate; }
        }

        /// <summary>
        /// Gets or sets the endpoint address type
        /// </summary>
        public EndpointAddressType AddressType {
            get {
                if (_bindingTemplate == null) return null;
                if (_bindingTemplate.CategoryBag == null) return null;
                EndpointAddressType endpointAddressType = new EndpointAddressType();
                return (EndpointAddressType)GetCategory(endpointAddressType);
            }
            set {
                if (value != null) {
                    if (_bindingTemplate.CategoryBag == null) {
                        _bindingTemplate.CategoryBag = new CategoryBag(value.GetAsKeyedReference());
                    } else {
                        _bindingTemplate.CategoryBag.SetCategory(value.GetAsKeyedReference());
                    }
                } else {
                    if (_bindingTemplate.CategoryBag != null) {
                        _bindingTemplate.CategoryBag.RemoveCategoryById(new EndpointAddressType().CategoryID);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the process instance set
        /// </summary>
        public ArsProcessInstanceSet ProcessInstanceSet {
            set {
                if (value == null) return;
                if (value.Processes.Count < 1) return;
                ClearProcessInstances();
                _processInstanceSet = value;
                foreach (ArsProcessInstance instance in value.Processes) {
                    _bindingTemplate.AddTModelInstanceInfo(instance.InstanceInfo);
                }
            }
            get {
                ArsProcessInstanceSet instanceSet = new ArsProcessInstanceSet();
                if (_bindingTemplate.TModelInstanceInfos == null) return instanceSet;
                foreach (TModelInstanceInfo instanceInfo in _bindingTemplate.TModelInstanceInfos) {
                    Inquiry inq = new Inquiry();
                    get_tModelDetail tModelDetail = new get_tModelDetail();
                    tModelDetail.tModelKey = new string[1];
                    tModelDetail.tModelKey[0] = instanceInfo.Value.tModelKey;
                    TModel[] models = inq.GetDetail(tModelDetail);
                    if (models.Length > 0) {
                        CategoryBag bag = models[0].CategoryBag;
                        KeyedReference regConfClaim = null;
                        KeyedReference processRoleIdentifierType = null;
                        KeyedReference processDefinitionReference = null;
                        if (bag != null) {
                            regConfClaim = bag.GetCategoryByName(new RegistrationConformanceClaim().CategoryName);
                            processRoleIdentifierType = bag.GetCategoryByName(new BusinessProcessRoleIdentifierType().CategoryName);
                            processDefinitionReference = bag.GetCategoryByName(new BusinessProcessDefinitionReference().CategoryName);
                        }
                        if (regConfClaim != null && processRoleIdentifierType != null && processDefinitionReference != null) {
                            RegistrationConformanceClaim conformanceClaimInstance = new RegistrationConformanceClaim();
                            if (regConfClaim.KeyValue == conformanceClaimInstance.DefaultCategoryValue) {
                                instanceSet.Add(new ArsProcessInstance(models[0]));
                            }
                        }
                    }
                }
                return instanceSet;
            }
        }

        /// <summary>
        /// Gets or sets the service regsistration reference
        /// </summary>
        public OasisServiceSingleBindingRegistration ServiceRegistration {
            get {
                if (_serviceRegistration != null) return _serviceRegistration;
                OasisPortTypeRegistrationReference portTypeRef = new OasisPortTypeRegistrationReference();
                OasisBindingRegistrationReference bindingRef = new OasisBindingRegistrationReference();
                _serviceRegistration = new OasisServiceSingleBindingRegistration();

                if (_bindingTemplate.TModelInstanceInfos != null)
                {
                    foreach (TModelInstanceInfo instanceInfo in _bindingTemplate.TModelInstanceInfos)
                    {
                        Inquiry inq = new Inquiry();
                        get_tModelDetail tModelDetail = new get_tModelDetail();
                        tModelDetail.tModelKey = new string[1];
                        tModelDetail.tModelKey[0] = instanceInfo.Value.tModelKey;
                        TModel[] models = inq.GetDetail(tModelDetail);
                        if (models.Length > 0)
                        {
                            CategoryBag bag = models[0].CategoryBag;
                            KeyedReference regConfClaim = null;
                            KeyedReference wsdlTypes = null;
                            if (bag != null)
                            {
                                regConfClaim = bag.GetCategoryByName(new RegistrationConformanceClaim().CategoryName);
                                wsdlTypes = bag.GetCategoryByName(new UddiOrgWsdlTypes().CategoryName);
                            }
                            if (regConfClaim != null && wsdlTypes != null)
                            {
                                RegistrationConformanceClaim conformanceClaimInstance =
                                    new RegistrationConformanceClaim();
                                if (regConfClaim.KeyValue == conformanceClaimInstance.DefaultCategoryValue)
                                {
                                    if (wsdlTypes.KeyValue == "binding")
                                    {
                                        bindingRef = new OasisBindingRegistrationReference(instanceInfo);
                                        _serviceRegistration.BindingRegistrationRef = bindingRef;
                                    }
                                    else if (wsdlTypes.KeyValue == "portType")
                                    {
                                        portTypeRef = new OasisPortTypeRegistrationReference(new UddiGuidId(instanceInfo.Value.tModelKey, true));
                                        _serviceRegistration.PortTypeRegistrationRef = portTypeRef;
                                    }
                                }
                            }
                        }
                    }
                    
                }

                return _serviceRegistration;
            }
            set {
                if (value == null) return;
                try {
                    OasisServiceSingleBindingRegistration oldServiceRegistration = this.ServiceRegistration;
                    //1. Add all porttype registration references
                    if (value.PortTypeRegistrationRef != null) {
                        if (oldServiceRegistration.PortTypeRegistrationRef != null) {
                            _bindingTemplate.RemoveTModelInstanceInfo(oldServiceRegistration.PortTypeRegistrationRef.PortTypeReference);
                        }
                        _bindingTemplate.AddTModelInstanceInfo(value.PortTypeRegistrationRef.PortTypeReference);
                    }
                    //2. Add all binding registration references
                    if (value.BindingRegistrationRef != null ) {
                        if (oldServiceRegistration.BindingRegistrationRef != null) {
                            _bindingTemplate.RemoveTModelInstanceInfo(oldServiceRegistration.BindingRegistrationRef.BindingReference);
                        }
                        _bindingTemplate.AddTModelInstanceInfo(value.BindingRegistrationRef.BindingReference);
                    }
                } catch (Exception e) {
                    throw new ArsBindingUnexpectedException(e);
                }
            }
        }

        /// <summary>
        /// Gets or sets the access point
        /// </summary>
        public AccessPoint AccessPoint {
            get {
                if (_bindingTemplate.AccessPoint == null) return null;
                AccessPoint ap = new AccessPoint(_bindingTemplate.AccessPoint.Value);
                return ap;
            }
            set {
                try {
                    _bindingTemplate.AccessPoint = value;
                }
                catch (Exception e) {
                    throw new ArsBindingUnexpectedException(e);
                }
            }
        }

        /// <summary>
        /// Gets or sets the description. Returns null if no description exists
        /// </summary>
        public Description Description {
            get {
                if (_bindingTemplate != null){
                    return _bindingTemplate.Description;
                } else {
                    return null;
                }
            }
            set {
                _bindingTemplate.Description = value;
            }
        }

        /// <summary>
        /// Gets or sets the service key
        /// </summary>
        public UddiId ServiceKey {
            get {
                if (_bindingTemplate != null && _bindingTemplate.ServiceKey!= null && _bindingTemplate.ServiceKey.Length > 0)
                    return new UddiGuidId(_bindingTemplate.ServiceKey, true);
                else {
                    return null;
                }
            }
            set {
                try {
                    if (_bindingTemplate != null) {
                        if (value == null) _bindingTemplate.ServiceKey = "";
                        else _bindingTemplate.ServiceKey = value.ID;
                    }
                } catch (Exception e) {
                    throw new ArsBindingUnexpectedException(e);
                }
            }
        }


        /// <summary>
        /// Gets or sets the binding key. Ignored if set with null
        /// </summary>
        public UddiId BindingKey {
            get {
                if (_bindingTemplate != null && _bindingTemplate.Value != null && !String.IsNullOrEmpty(_bindingTemplate.Value.bindingKey)) {
                    return new UddiGuidId(_bindingTemplate.Value.bindingKey, true);
                }  else {
                    return null;
                }
            }
            set {
                try {
                    if (_bindingTemplate != null && _bindingTemplate.Value != null) {
                        if (value == null) {
                            _bindingTemplate.Value.bindingKey = "";
                        } else {
                            _bindingTemplate.Value.bindingKey = value.ID;
                        }
                    }
                } catch (Exception e) {
                    throw new ArsBindingUnexpectedException(e);
                }
            }
        }

        /// <summary>
        /// Gets the uuid of the binding
        /// </summary>
        public UddiId ID {
            get {
                if (_bindingTemplate != null && _bindingTemplate.Value.bindingKey != null && _bindingTemplate.Value.bindingKey.Length > 0)
                    return new UddiGuidId(_bindingTemplate.Value.bindingKey, true);
                else {
                    return null;
                }
            }
            set {
                try {
                    if (_bindingTemplate != null && _bindingTemplate.Value != null) {
                        if (value == null) _bindingTemplate.Value.bindingKey = "";
                        else _bindingTemplate.Value.bindingKey = value.ID;
                    }
                }
                catch (Exception e) {
                    throw new ArsBindingUnexpectedException(e);
                }
            }
        }

        #endregion properties

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ArsBinding() {
            try {
                _bindingTemplate = new BindingTemplate();
            }
            catch (Exception e) {
                throw new ArsBindingUnexpectedException(e);
            }
        }
        

        /// <summary>
        /// Initialize an existing arsbinding
        /// </summary>
        /// <param name="bindingTemplate">the existing arsbinding</param>
        public ArsBinding(BindingTemplate bindingTemplate) {
            //1. set the bindingtemplate
            _bindingTemplate = bindingTemplate;

            /* We don't set ArsProcessInstanceSet and OasisServiceRegistration 
             * in the release

            //2. check that the bindingtemplate is actually a arsbinding template
            if (_bindingTemplate.TModelInstanceInfos.Length == 3) {

                for (int i = 0; i < _bindingTemplate.TModelInstanceInfos.Length; i++) {

                    if (_bindingTemplate.TModelInstanceInfos[i].
                }
            }
            else {
                throw new ArgumentException("the bindingtemplate doesn't comply with the Ars profile");
            }
             * */
        }

         ///<summary>
         ///Constructor initliazing existing instanceset and serviceregistration
         ///</summary>
         ///<param name="instanceSet">Set of ARS process instances</param>
         ///<param name="serviceRegistration">Reference to the service definition</param>
         ///<param name="accessPoint">Access point of the endpoint</param>
         ///<param name="endpointAddressType">The type of the accessPoint (e.g. http or email)</param>
         ///<param name="description">The description of the endpoint binding</param>
         ///<param name="businessServiceKey">businessService key</param>
         ///<param name="bindingKey">The binding key - null if none exists</param>
        public ArsBinding(
            ArsProcessInstanceSet instanceSet,
            OasisServiceSingleBindingRegistration serviceRegistration,
            AccessPoint accessPoint,
            EndpointAddressType endpointAddressType,
            Description description,
            UddiId businessServiceKey,
            UddiId bindingKey) {

            //1. set businessentity attributes, contacts and discoveryUrls
            try {
                 SetBindingTemplateDetails(instanceSet, serviceRegistration,
                    accessPoint, endpointAddressType, description, businessServiceKey, bindingKey);
            }
            catch (Exception e) {
                throw new ArsBindingUnexpectedException(e);
            }
        }

        #endregion constructors

        #region methods

        /// <summary>
        /// This method sets all bindings
        /// </summary>
        /// <param name="instanceSet">Contains a set of process</param>
        /// <param name="serviceRegistration">Reference to porttype and bindings</param>
        /// <param name="accessPoint">The endpoint for the service</param>
        /// <param name="endpointAddressType">The type of the accessPoint (e.g. http or email)</param>
        /// <param name="description">Description of the endpoint binding</param>
        /// <param name="businessServiceKey">The UDDI key of the business service</param>
        /// <param name="bindingKey">The binding key - null if none exists</param>
        private void SetBindingTemplateDetails(
            ArsProcessInstanceSet instanceSet, 
            OasisServiceSingleBindingRegistration serviceRegistration, 
            AccessPoint accessPoint,
            EndpointAddressType endpointAddressType,
            Description description, 
            UddiId businessServiceKey,
            UddiId bindingKey) {

            //1. Set properties:
            try {
                _bindingTemplate = new BindingTemplate();
                //_processInstanceSet = instanceSet;
                ProcessInstanceSet = instanceSet;
                //_serviceRegistration = serviceRegistration;
                ServiceRegistration = serviceRegistration;
                _bindingTemplate.AccessPoint = accessPoint;
                AddressType = endpointAddressType;
                Description = description;
                ServiceKey = businessServiceKey;
                BindingKey = bindingKey;
            }
            catch (Exception e) {
                throw new ArsBindingUnexpectedException(e);
            }
        }

        /// <summary>
        /// Gets an ARS category by the category ID of the 'categoryObject' 
        /// parameter. Note that we need an actual ArsCategory descendant 
        /// object to get the category identifier. If no match is found, null is returned.
        /// </summary>
        /// <param name="category">The category object from which to get the category ID.
        /// Another object, with key and value set, is returned as a result.</param>
        /// <returns></returns>
        public ArsCategory GetCategory(ArsCategory category) {
            if (category == null) return null;

            try {
                CategoryBag bag = new CategoryBag(_bindingTemplate.Value.categoryBag);
                KeyedReference keyRef = bag.GetCategoryByIdentifierAndKeyName(category.CategoryID, category.CategoryName);
                if (keyRef == null) return null;
                category.SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
            }
            catch (Exception e) {
                throw new ArsBindingUnexpectedException(e);
            }
            return category;
        }

        /// <summary>
        /// Adds a category to the category bag
        /// </summary>
        /// <param name="uddiKeyRef">A keyed reference</param>
        public void SetCategory(keyedReference uddiKeyRef) {
            KeyedReference keyRef = new KeyedReference(uddiKeyRef);
            if (keyRef != null) {
                if (_bindingTemplate != null) {
                    if (_bindingTemplate.CategoryBag == null) {
                        _bindingTemplate.CategoryBag = new CategoryBag(keyRef);
                    }
                    else {
                        _bindingTemplate.CategoryBag.SetCategory(keyRef);
                    }
                }
                else {
                    return;
                }
            }
        }

        /// <summary>
        /// Returns the category bag
        /// </summary>
        public CategoryBag GetCategoryBag() {
            if (_bindingTemplate == null) return new CategoryBag();
            return _bindingTemplate.CategoryBag;
        }

        /// <summary>
        /// Removes the ARS process instance
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool RemoveProcessInstance(ArsProcessInstance instance) {
            if (_bindingTemplate.TModelInstanceInfos == null) return false;
            for (int iInstanceInfo = 0; iInstanceInfo < _bindingTemplate.TModelInstanceInfos.Length; iInstanceInfo++) {
                TModelInstanceInfo instanceInfo = _bindingTemplate.TModelInstanceInfos[iInstanceInfo];
                if (IdentifierUtility.GetUddiIDFromString(instanceInfo.Value.tModelKey).ID == instance.ID.ID) {
                    TModelInstanceInfo[] temp = new TModelInstanceInfo[_bindingTemplate.TModelInstanceInfos.Length - 1];
                    int iNew = 0;
                    for (int iTemp = 0; iTemp < _bindingTemplate.TModelInstanceInfos.Length; iTemp++) {
                        if (iTemp != iInstanceInfo) {
                            temp[iNew] = _bindingTemplate.TModelInstanceInfos[iTemp];
                            iNew++;
                        }
                    }
                    _bindingTemplate.TModelInstanceInfos = temp;
                    //TODO: remove this
                    //_processInstanceSet.Remove(instance);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds the ARS process instance
        /// </summary>
        /// <param name="instance"></param>
        public void AddProcessInstance(ArsProcessInstance instance) {
            _bindingTemplate.AddTModelInstanceInfo(instance.InstanceInfo);
            //TODO: remove this
            //_processInstanceSet.Add(instance);
        }

        /// <summary>
        /// Clears the process instances
        /// </summary>
        public void ClearProcessInstances() {
            foreach (ArsProcessInstance processInstance in this.ProcessInstanceSet.Processes) {
                _bindingTemplate.RemoveTModelInstanceInfo(processInstance.InstanceInfo);
            }
            _processInstanceSet.Processes.Clear();
        }

        #endregion methods        

        #region IRegistrationEntity Members

        /// <summary>
        /// Method to handle the call to SaveBinding()
        /// </summary>
        public void Save() {

            try {
                //1. Validate
                Validate();

                //2. Save the bindingtemplate to the Uddi server
                SaveBinding();
            }
            catch (Exception e) {
                throw new ArsBindingUnexpectedException(e);
            }
        }

        /// <summary>
        /// Saves the binding to the Uddi server
        /// </summary>
        private void SaveBinding() {
            BindingTemplateCollection templateCollection = new BindingTemplateCollection();
            templateCollection.Add(_bindingTemplate.Value);
            
            try {
                //1. call Publication
                Publication uddiPublicationProxy = new Publication(Connection);
                bindingDetail detail = uddiPublicationProxy.Save(templateCollection);
                _bindingTemplate = new BindingTemplate(detail.bindingTemplate[0]);
            }
            catch (SaveBindingException) {
                throw;
            }
            catch (PublicationUnexpectedException) {
                throw;
            }
            catch (Exception e) {
                throw new ArsBindingUnexpectedException(e);
            }
        }

        /// <summary>
        /// Performs validation of this entity
        /// </summary>
        public void Validate() {
            string portTypeReferenceOnBinding = "";
            string actualPortTypeReference = "";

            //1. Validate
            Inquiry inq = new Inquiry();
            GetTModelDetail getBindingTModel = new GetTModelDetail(ServiceRegistration.BindingRegistrationRef.ID.ID);
            TModel[] tmodels = inq.GetDetail(getBindingTModel.Value);

            if (tmodels != null && tmodels.Length > 0) {
                CategoryBag bag = tmodels[0].CategoryBag;
                KeyedReference keyref = bag.GetCategoryByName(new UddiOrgWsdlPortTypeReference().CategoryName);

                portTypeReferenceOnBinding = keyref.KeyValue;
                actualPortTypeReference = ServiceRegistration.PortTypeRegistrationRef.PortTypeReference.Value.tModelKey;
            }

            if (portTypeReferenceOnBinding != actualPortTypeReference) {
                throw new Exception("Validation failed for the binding. Porttype reference in the binding, does not match the actual porttype referenced");
            }
        }

        /// <summary>
        /// Updates this entity
        /// </summary>
        public void Update() {
            
            //1. Validate
            Validate();

            //2. Save
            SaveBinding();
        }

        /// <summary>
        /// Deletes the bindingtemplate
        /// </summary>
        public void Delete() {

            //1. check that an binding uuid exists
            if (_bindingTemplate.Value.bindingKey.Length > 0) {

                string[] temp = new string[1];
                temp[0] = _bindingTemplate.Value.bindingKey;

                //2. call Publication.Delete
                try {
                    Publication uddiPublicationProxy = new Publication(Connection);
                    bool deleted = uddiPublicationProxy.DeleteBindingTemplate(temp);
                    if (deleted) {
                        _bindingTemplate = new BindingTemplate();
                    }
                }
                catch (DeleteBindingException) {
                    throw;
                }
                catch (PublicationUnexpectedException) {
                    throw;
                }
                catch (Exception e) {
                    throw new ArsBindingUnexpectedException(e);
                }
            }
            else {
                throw new Exception("No bindingkey was found");
            }
        }
        #endregion
        /// <summary>
        /// Validates the embedded data, and eventually returns a structured report if validations fails
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="Failures"></param>
        /// <returns></returns>
        public bool IsValid(string EntityName, ref ValidationFailureCollection Failures) {
            ValidationFailureCollection ChildFailures = null;
            bool Valid = true;

            BindingTemplate.IsValid(_bindingTemplate, "_bindingTemplate", ref ChildFailures);

            if (ChildFailures != null) {
                ChildValidationFailure.AddFailure(ChildFailure.Message(), EntityName, this.GetType(),
                    ChildFailures, ref Failures);
                Valid = false;
            }

            return Valid;
        }

        /// <summary>
        /// Finds a binding by porttypereference and transporttype
        /// </summary>
        /// <returns>the uuid of the found binding</returns>
        public static string FindBindingGuid(UddiId porttypeUuid, UddiOrgWsdlCategorizationTransportCode transport) {

            Inquiry inq = new Inquiry();
            CategoryBag bag = new CategoryBag();

            RegistrationConformanceClaim regConClaim =
                new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1);
            UddiOrgWsdlCategorizationTransport bindingTransport = new UddiOrgWsdlCategorizationTransport(transport);
            UddiOrgWsdlTypes type = new UddiOrgWsdlTypes(UddiOrgWsdlTypeCode.binding);
            UddiOrgWsdlPortTypeReference portTypeReference = new UddiOrgWsdlPortTypeReference(porttypeUuid);

            bag.AddCategory(regConClaim.GetAsKeyedReference());
            bag.AddCategory(bindingTransport.GetAsKeyedReference());
            bag.AddCategory(type.GetAsKeyedReference());
            bag.AddCategory(portTypeReference.GetAsKeyedReference());

            FindTModel findTModel = new FindTModel(bag.GetInnerCollectionAsKeyedReferenceCollection());
            tModelList list = inq.Find(findTModel);

            if (list.tModelInfos == null) {
                return null;
            } else if (list.tModelInfos.Length == 0) {
                return null;
            } else if (list.tModelInfos != null && list.tModelInfos.Length == 1) {
                return list.tModelInfos[0].tModelKey;
            } else {
                throw new Exception("There has been an error in the registration process." +
                    " There exists more than two bindings, on the portype " + porttypeUuid.ID + ".");
            }
        }
    }
}