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
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.uddi.Validation;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// OASIS binding registration
    /// </summary>
    public class OasisBindingRegistration : RegistrationEntity, IRegistrationEntity {

        private TModel _tModel;

        private void SetDefaultProperties() {
            // Sets the registration profile conformance claim
            _tModel.CategoryBag.SetCategory(new RegistrationConformanceClaim(
                RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());

            // Sets the wsdl entity type as default. 
            UddiOrgWsdlTypes wsdlEntityType = new UddiOrgWsdlTypes(UddiOrgWsdlTypeCode.binding);
            _tModel.CategoryBag.SetCategory(wsdlEntityType.GetAsKeyedReference());

            // Sets the SOAP 1.2 protocol as default.
            // For now it is set to soap 1.1, because no UDDI taxonomy
            // exists for indicating this 
            UddiOrgWsdlCategorizationProtocol bindingProtocol =
                new UddiOrgWsdlCategorizationProtocol(
                UddiOrgWsdlCategorizationProtocolCode.soap1_1Protocol);
            _tModel.CategoryBag.SetCategory(bindingProtocol.GetAsKeyedReference());

            // Sets the http binding as default.
            UddiOrgWsdlCategorizationTransport bindingTransport =
                new UddiOrgWsdlCategorizationTransport(
                UddiOrgWsdlCategorizationTransportCode.http);
            _tModel.CategoryBag.SetCategory(bindingTransport.GetAsKeyedReference());

            // Sets the legacy wsdl type indicator.
            UddiOrgTypes legacyWsdlIndicator = new UddiOrgTypes(UddiOrgTypesCode.wsdlSpec);
            _tModel.CategoryBag.SetCategory(legacyWsdlIndicator.GetAsKeyedReference());
        }

        /// <summary>
        /// Default constructor. Creates a new tModel entity and generates a key for it.
        /// </summary>
        public OasisBindingRegistration() {
            _tModel = new TModel();
            _tModel.CategoryBag = new CategoryBag();
            SetDefaultProperties();
        }



        /// <summary>
        /// Url to the binding registration definition WSDL document
        /// </summary>
        public OverviewDoc OverviewDocument {
            get {
                if (_tModel != null) {
                    return _tModel.OverviewDoc;
                } else {
                    return null;
                }
            }
            set {
                _tModel.OverviewDoc = value;
            }
        }


        /// <summary>
        /// Gets the uuid of the central service definition porttype
        /// </summary>
        public UddiId ID {
            get {
                if (_tModel != null && _tModel.Value.tModelKey != null) {
                    return IdentifierUtility.GetUddiIDFromString(_tModel.Value.tModelKey);
                } else {
                    return null;
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
        /// Saves the all associated UDDI entities
        /// </summary>
        public void Save() {
            SaveTModel();
        }

        private void SaveTModel() {

            TModel[] tmodels = new TModel[1];
            tmodels[0] = _tModel;

            try {

                //1. add categories to categorybag
                _tModel.CategoryBag.SetCategory(BindingNamespace.GetAsKeyedReference());
                _tModel.CategoryBag.SetCategory(PortTypeDefinitionReference.GetAsKeyedReference());

                //1. call Publication
                Publication uddiPublicationProxy = new Publication(Connection);
                tModelDetail detail = uddiPublicationProxy.Save(tmodels);
                _tModel = new TModel(detail.tModel[0]);
            } catch (SaveTModelException) {
                throw;
            } catch (PublicationUnexpectedException) {
                throw;
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Validates all associated UDDI entities
        /// </summary>
        public void Validate() {
        }

        /// <summary>
        /// Updates all associated UDDI entities
        /// </summary>
        public void Update() {
            SaveTModel();
        }

        /// <summary>
        /// Deletes all associated UDDI entities
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
                    } else {
                        throw new DeleteEntityException(_tModel.Value.tModelKey);
                    }
                } catch (DeleteTModelException) {
                    throw;
                } catch (PublicationUnexpectedException) {
                    throw;
                } catch (Exception) {
                    throw; ;
                }
            } else {
                throw new DeleteEntityException("Entity has no identifier. Probably not instantiated");
            }
        }

        /// <summary>
        /// Gets an OASIS binding registration object
        /// </summary>
        /// <param name="bindingRegistrationTModelId">uuid of the binding registration tmodel</param>
        public static OasisBindingRegistration Get(UddiId bindingRegistrationTModelId) {

            //1. create a temp tmodel
            OasisBindingRegistration tempRegistration = new OasisBindingRegistration();

            //2. create a uddi inquire instance
            Inquiry inq = new Inquiry();

            //3. call gettmodeldetail
            TModel[] tmodels;
            try {
                GetTModelDetail detail = new GetTModelDetail(bindingRegistrationTModelId.ID);
                tmodels = inq.GetDetail(detail.Value);

                if (tmodels.Length == 1) {
                    tempRegistration._tModel = tmodels[0];

                    CategoryBag bag = tmodels[0].CategoryBag;

                    KeyedReference bindingNamespace = bag.GetCategoryByName(new UddiOrgXmlNamespace().CategoryName);
                    tempRegistration.BindingNamespace = new UddiOrgXmlNamespace(new Uri(bindingNamespace.KeyValue));
                }
            } catch (GetBusinessEntityException) {
                throw;
            } catch (InquiryUnexpectedException) {
                throw;
            } catch (Exception) {
                throw;
            }
            return tempRegistration;
        }

        /// <summary>
        /// Gets or sets the name of the underlying tModel
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
        /// Description of the endpoint
        /// </summary>
        public Description Description {
            get {
                if (_tModel != null && _tModel.DescriptionCollection != null && _tModel.DescriptionCollection.Count > 0) {
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
        /// Gets or sets the namespace of the WSDL binding element
        /// </summary>
        public UddiOrgXmlNamespace BindingNamespace {
            get {
                if (_tModel == null) return null;
                if (_tModel.CategoryBag == null) return null;
                UddiOrgXmlNamespace uddiOrgXmlNs = new UddiOrgXmlNamespace();
                KeyedReference keyRef = _tModel.CategoryBag.GetCategoryByIdentifierAndKeyName(uddiOrgXmlNs.CategoryID, uddiOrgXmlNs.CategoryName);
                if (keyRef == null) return null;
                uddiOrgXmlNs.SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
                return uddiOrgXmlNs;
            }
            set {
                UddiOrgXmlNamespace test = new UddiOrgXmlNamespace();
                if (value != null) {
                    if (_tModel == null) return;
                    // Check that category ID is valid:
                    if (value.CategoryID.ToLower() != test.CategoryID.ToLower()) {
                        
                        throw new Exception("Cannot set namespace: tried to set with wrong category ID ('" +
                            value.CategoryID + "')");
                    }

                    if (_tModel.CategoryBag == null) {
                        _tModel.CategoryBag = new CategoryBag();
                    }

                    _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    if (_tModel != null && _tModel.CategoryBag != null) {
                        _tModel.CategoryBag.RemoveCategoryById(test.CategoryID);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the transport of the WSDL binding element
        /// </summary>
        public UddiOrgWsdlCategorizationTransport Transport {
            get {
                if (_tModel != null && _tModel.CategoryBag != null) {
                    UddiOrgWsdlCategorizationTransport transport = new UddiOrgWsdlCategorizationTransport();

                    KeyedReference keyRef = _tModel.CategoryBag.GetCategoryByIdentifierAndKeyName(transport.CategoryID, transport.CategoryName);
                    if (keyRef != null) {
                        transport.SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
                        return transport;
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
            set {
                UddiOrgWsdlCategorizationTransport test = new UddiOrgWsdlCategorizationTransport();
                if (value != null) {
                    if (_tModel == null) return;
                    // Check that category ID is valid:
                    if (value.CategoryID.ToLower() != test.CategoryID.ToLower()) {
                        
                        throw new Exception("Cannot set transport: tried to set with wrong category ID ('" +
                            value.CategoryID + "')");
                    }

                    if (_tModel.CategoryBag == null) {
                        _tModel.CategoryBag = new CategoryBag();
                    }

                    _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    if (_tModel != null && _tModel.CategoryBag != null) {
                        _tModel.CategoryBag.RemoveCategoryById(test.CategoryID);
                    }
                }
            }
        }



        /// <summary>
        /// Gets or sets the uddi reference to the portType definition tModel
        /// </summary>
        public UddiOrgWsdlPortTypeReference PortTypeDefinitionReference {
            get {
                if (_tModel != null && _tModel.CategoryBag != null) {
                    UddiOrgWsdlPortTypeReference portTypeRef = new UddiOrgWsdlPortTypeReference();

                    KeyedReference keyRef = _tModel.CategoryBag.GetCategoryByIdentifierAndKeyName(portTypeRef.CategoryID, portTypeRef.CategoryName);
                    if (keyRef != null) {
                        portTypeRef.SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
                        return portTypeRef;
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
            set {
                UddiOrgWsdlPortTypeReference test = new UddiOrgWsdlPortTypeReference();
                if (value != null) {
                    if (_tModel == null) return;
                    // Check that category ID is valid:

                    if (value.CategoryID.ToLower() != test.CategoryID.ToLower()) {
                        
                        throw new Exception("Cannot set namespace: tried to set with wrong category ID ('" +
                            value.CategoryID + "')");
                    }

                    if (_tModel.CategoryBag == null) {
                        _tModel.CategoryBag = new CategoryBag();
                    }

                    _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    if (_tModel != null && _tModel.CategoryBag != null) {
                        _tModel.CategoryBag.RemoveCategoryById(test.CategoryID);
                    }
                }
            }
        }




        /// <summary>
        /// Gets the registration conformance claim regarding Uddi registration model
        /// </summary>
        public RegistrationConformanceClaim RegistrationConformanceClaim {
            get {
                if (_tModel != null && _tModel.CategoryBag != null) {
                    RegistrationConformanceClaim regClaim = new RegistrationConformanceClaim();

                    KeyedReference keyRef = _tModel.CategoryBag.GetCategoryByIdentifierAndKeyName(regClaim.CategoryID, regClaim.CategoryName);
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
                    if (_tModel == null) return;
                    // Check that category ID is valid:
                    if (value.CategoryID.ToLower() != test.CategoryID.ToLower()) {
                        
                        throw new Exception("Cannot set registration conformance claim: " +
                            "Tried to set with wrong category ID ('" + value.CategoryID + "')");
                    }

                    if (_tModel.CategoryBag == null) {
                        _tModel.CategoryBag = new CategoryBag();
                    }

                    _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    if (_tModel != null && _tModel.CategoryBag != null) {
                        _tModel.CategoryBag.RemoveCategoryById(test.CategoryID);
                    }
                }
            }
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