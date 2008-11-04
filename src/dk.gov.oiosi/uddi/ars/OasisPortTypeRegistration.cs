/* The contents of this file are subject to the Mozilla Public
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
    public class OasisPortTypeRegistration : RegistrationEntity, IRegistrationEntity {

        private TModel _tModel = new TModel();

        /// <summary>
        /// Initialization. Creates an empty tModel and an ID
        /// </summary>
        private void Init() {
            _tModel = new TModel();
            _tModel.CategoryBag = new CategoryBag();
        }

        private void SetDefaultProperties() {
            // Sets the registration profile conformance claim
            _tModel.CategoryBag.SetCategory(new RegistrationConformanceClaim(
                RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());

            WsdlType = new UddiOrgWsdlTypes(UddiOrgWsdlTypeCode.portType);
        }

        /// <summary>
        /// Default constructor. Constructs emtpy internal 
        /// tModel entities.
        /// </summary>
        public OasisPortTypeRegistration() {
            Init();
            SetDefaultProperties();
        }

        /// <summary>
        /// Constructor that initializes a OasisPortTypeRegistration with a specific porttype
        /// </summary>
        /// <param name="portTypeRegistrationTModelKey">porttype registration tmodel</param>
        public OasisPortTypeRegistration(UddiGuidId portTypeRegistrationTModelKey) {
            Init();
            SetDefaultProperties();

            //1. set the tmodelkey attribute on the tmodel
            _tModel.Value.tModelKey = portTypeRegistrationTModelKey.ID;
        }

        /// <summary>
        /// Gets the uuid of the port type registration
        /// </summary>
        public UddiId ID {
            get {
                if (_tModel.Value.tModelKey!=null && _tModel.Value.tModelKey.Length > 0) {
                    return IdentifierUtility.GetUddiIDFromString(_tModel.Value.tModelKey);
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    if (_tModel != null) {
                        _tModel.Value.tModelKey = value.ID;
                    }
                } else {
                    _tModel.Value.tModelKey = "";
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
        /// The name of the endpoint
        /// </summary>
        public string MyName {
            get {
                return null;
            }
            set {

            }
        }

        /// <summary>
        /// Gets or sets the service contact email
        /// </summary>
        public ServiceContactEmail ContactEmail {
            get {
                if (_tModel != null && _tModel.IdentifierBag != null) {
                    ServiceContactEmail email = new ServiceContactEmail();
                    KeyedReference keyref = _tModel.IdentifierBagAsCategoryBagObject.GetCategoryByIdentifierAndKeyName(email.IdentifierID, email.IdentifierName);
                    if (keyref != null) {
                        email.Value = keyref.KeyValue;
                        return email;
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
                        if (identifiers != null) {
                            identifiers.SetCategory(value.GetAsKeyedReference());
                            _tModel.IdentifierBag = identifiers.GetInnerCollectionAsKeyedReferenceCollection();
                        }
                    }
                } else {
                    ServiceContactEmail emailSample = new ServiceContactEmail();
                    CategoryBag identifiers = _tModel.IdentifierBagAsCategoryBagObject;
                    if (identifiers != null) {
                        identifiers.RemoveCategoryById(emailSample.IdentifierID);
                        _tModel.IdentifierBag = identifiers.GetInnerCollectionAsKeyedReferenceCollection();
                    }
                }
            }
        }


        /// <summary>
        /// Gets or sets the OIO taxonomy. 
        /// </summary>
        public OIOTaxonomy Taxonomy {
            get {
                if (_tModel != null && _tModel.CategoryBag != null) {
                    OIOTaxonomy tax = new OIOTaxonomy();
                    KeyedReference keyref = _tModel.CategoryBag.GetCategoryByIdentifierAndKeyName(tax.CategoryID, tax.CategoryName);
                    if (keyref != null) {
                        //tax.SetCategoryValue(new OIOTaxonomy(). CategoryName /*keyref.KeyName*/, keyref.KeyValue);
                        tax.SetCategoryValue(keyref.KeyName, keyref.KeyValue);
                        return tax;
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
                        CategoryBag bag = new CategoryBag();
                        bag.AddCategory(value.GetAsKeyedReference());
                        _tModel.CategoryBag = bag;
                        return;
                    } else {
                        _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                    }
                } else {
                    if (_tModel.CategoryBag != null) {
                        _tModel.CategoryBag.RemoveCategoryById(new OIOTaxonomy().CategoryID);
                    }
                }
            }
        }


        /// <summary>
        /// Gets or sets the service documentation url
        /// </summary>
        public ServiceDocumentationUrl DocumentationUrl {
            get {
                if (_tModel != null && _tModel.IdentifierBag != null) {
                    ServiceDocumentationUrl docUrl = new ServiceDocumentationUrl();
                    KeyedReference keyref = _tModel.IdentifierBagAsCategoryBagObject.GetCategoryByIdentifierAndKeyName(docUrl.IdentifierID, docUrl.IdentifierName);
                    if (keyref != null) {
                        docUrl.Value = keyref.KeyValue;
                        return docUrl;
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
                        if (identifiers != null) {
                            identifiers.SetCategory(value.GetAsKeyedReference());
                            _tModel.IdentifierBag = identifiers.GetInnerCollectionAsKeyedReferenceCollection();
                        }
                    }
                } else {
                    ServiceDocumentationUrl docUrlSample = new ServiceDocumentationUrl();
                    CategoryBag identifiers = _tModel.IdentifierBagAsCategoryBagObject;
                    if (identifiers != null) {
                        identifiers.RemoveCategoryById(docUrlSample.IdentifierID);
                        _tModel.IdentifierBag = identifiers.GetInnerCollectionAsKeyedReferenceCollection();
                    }
                }
            }
        }


        /// <summary>
        /// Gets or sets the version of the portType registration (major, minor, revision)
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
                } catch {
                    throw;
                }
                return v;
            }
            set {
                if (value != null) {
                    _tModel.CategoryBag.RemoveCategoryById(new VersionMajor().CategoryID);
                    _tModel.CategoryBag.RemoveCategoryById(new VersionMinor().CategoryID);
                    _tModel.CategoryBag.RemoveCategoryById(new VersionRevision().CategoryID);

                    SetCategory(new VersionMajor(value.Major.ToString()).GetAsKeyedReference());
                    SetCategory(new VersionMinor(value.Minor.ToString()).GetAsKeyedReference());
                    SetCategory(new VersionRevision(value.Build.ToString()).GetAsKeyedReference());
                } else {
                    _tModel.CategoryBag.RemoveCategoryById(new VersionMajor().CategoryID);
                    _tModel.CategoryBag.RemoveCategoryById(new VersionMinor().CategoryID);
                    _tModel.CategoryBag.RemoveCategoryById(new VersionRevision().CategoryID);
                }
            }
        }

        /// <summary>
        /// Sets the category in the category bag, if 'category' is not null.
        /// If the category already exists, it is overwritten. If not, it is added.
        /// </summary>
        /// <param name="category">The category to add</param>
        private void SetCategory(ArsCategory category) {
            if (category == null) return;
            SetCategory(category.GetAsKeyedReference());
        }

        /// <summary>
        /// Adds a category to the category bag
        /// </summary>
        /// <param name="keyRef">A keyed reference</param>
        private void SetCategory(KeyedReference keyRef) {
            if (keyRef != null) {
                if (_tModel.CategoryBag == null) {
                    _tModel.CategoryBag = new CategoryBag(keyRef);
                } else {
                    _tModel.CategoryBag.SetCategory(keyRef);
                }
            }
        }

        /// <summary>
        /// Removes a category from the bag.
        /// </summary>
        /// <param name="category">The category to remove</param>
        private void RemoveCategory(ArsCategory category) {
            //1. Call remove on the categorybag
            _tModel.CategoryBag.RemoveCategory(category.GetAsKeyedReference());
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
            CategoryBag bag = new CategoryBag(_tModel.Value.categoryBag);
            KeyedReference keyRef = bag.GetCategoryByIdentifierAndKeyName(category.CategoryID, category.CategoryName);
            if (keyRef == null) return null;
            category.SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
            return category;
        }





        /// <summary>
        /// Url to the porType definition WSDL document
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
        /// Gets or sets the namespace of the WSDL binding element
        /// </summary>
        public UddiOrgXmlNamespace BindingNamespace {
            get {
                if (_tModel != null && _tModel.CategoryBag != null) {
                    UddiOrgXmlNamespace uddiOrgXmlNs = new UddiOrgXmlNamespace();

                    KeyedReference keyRef = _tModel.CategoryBag.GetCategoryByIdentifierAndKeyName(uddiOrgXmlNs.CategoryID, uddiOrgXmlNs.CategoryName);
                    if (keyRef != null) {
                        uddiOrgXmlNs.SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
                        return uddiOrgXmlNs;
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            }
            set {
                if (value != null) {
                    if (_tModel == null) return;
                    // Check that category ID is valid:
                    UddiOrgXmlNamespace test = new UddiOrgXmlNamespace();
                    if (value.CategoryID.ToLower() != test.CategoryID.ToLower()) {
                        
                        throw new Exception("Cannot set namespace: tried to set with wrong category ID ('" +
                            value.CategoryID + "')");
                    }

                    if (_tModel.CategoryBag == null) {
                        _tModel.CategoryBag = new CategoryBag();
                    }

                    _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    if (_tModel.CategoryBag != null) {
                        _tModel.CategoryBag.RemoveCategoryById(new UddiOrgXmlNamespace().CategoryID);
                    }
                }
            }
        }


        /// <summary>
        /// Gets or sets the type of WSDL section that the underlying tModel represents
        /// </summary>
        private UddiOrgWsdlTypes WsdlType {
            set {
                if (value != null) {
                    if (_tModel == null) return;
                    // Check that category ID is valid:
                    UddiOrgWsdlTypes test = new UddiOrgWsdlTypes();
                    if (value.CategoryID.ToLower() != test.CategoryID.ToLower()) {
                        
                        throw new Exception("Cannot set WSDL type: tried to set with wrong category ID ('" +
                            value.CategoryID + "')");
                    }

                    if (_tModel.CategoryBag == null) {
                        _tModel.CategoryBag = new CategoryBag();
                    }

                    _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    if (_tModel.CategoryBag != null) {
                        _tModel.CategoryBag.RemoveCategoryById(new UddiOrgWsdlTypes().CategoryID);
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
                if (value != null) {
                    if (_tModel == null) return;
                    // Check that category ID is valid:
                    RegistrationConformanceClaim test = new RegistrationConformanceClaim();
                    if (value.CategoryID.ToLower() != test.CategoryID.ToLower()) {
                        
                        throw new Exception("Cannot set registration conformance claim: " +
                            "Tried to set with wrong category ID ('" + value.CategoryID + "')");
                    }

                    if (_tModel.CategoryBag == null) {
                        _tModel.CategoryBag = new CategoryBag();
                    }

                    _tModel.CategoryBag.SetCategory(value.GetAsKeyedReference());
                } else {
                    if (_tModel.CategoryBag != null) {
                        _tModel.CategoryBag.RemoveCategoryById(new RegistrationConformanceClaim().CategoryID);
                    }
                }
            }
        }




        /// <summary>
        /// Saves all underlying UDDI registrations associated with this portType
        /// </summary>
        public void Save() {

            try {
                // 1. Validate
                Validate();

                SaveTModel();
            } catch {
                throw ;
            }
        }

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
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Validates all underlying UDDI registrations associated with this portType
        /// </summary>
        public void Validate() {
        }

        /// <summary>
        /// Updates all underlying UDDI registrations associated with this portType
        /// </summary>
        public void Update() {

            try {
                // 1. Validate
                Validate();

                // 3. Update
                SaveTModel();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Deletes all underlying UDDI registrations associated with this portType
        /// NOTE: associated bindings are not deleted as part of this operation.
        /// </summary>
        public void Delete() {
            //1. check that an id exists
            if (_tModel.Value.tModelKey.Length > 0) {

                if (!BindingReferencesExists()) {
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
                }
            } else {
                throw new DeleteEntityException("Entity has no identifier. Probably not instantiated");
            }
        }

        /// <summary>
        /// Determines if other references to this porttype exists.
        /// </summary>
        /// <returns>true if references exists</returns>
        private bool BindingReferencesExists() {

            Inquiry inq = new Inquiry();
            CategoryBag bag = new CategoryBag();

            RegistrationConformanceClaim regConfClaim = new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1);
            UddiOrgWsdlTypes wsdlTypes = new UddiOrgWsdlTypes(UddiOrgWsdlTypeCode.binding);
            UddiOrgWsdlPortTypeReference portTypeReference = new UddiOrgWsdlPortTypeReference(ID);

            bag.AddCategory(regConfClaim.GetAsKeyedReference());
            bag.AddCategory(wsdlTypes.GetAsKeyedReference());
            bag.AddCategory(portTypeReference.GetAsKeyedReference());

            FindTModel find = new FindTModel(bag.GetInnerCollectionAsKeyedReferenceCollection());
            tModelList tmodels = inq.Find(find);

            if (tmodels.tModelInfos != null && tmodels.tModelInfos.Length > 0) {
                return true;
            } else
                return false;
        }

        /// <summary>
        /// Gets the UDDI registration associated with this portType
        /// </summary>
        /// <param name="portTypeRegistrationTModelId">uuid of the porttype registration tmodel</param>
        /// <returns>the porttyperegistration</returns>
        public static OasisPortTypeRegistration Get(UddiId portTypeRegistrationTModelId) {

            //1. create a temp tmodel
            OasisPortTypeRegistration tempRegistration = null;

            //2. create a uddi inquire instance
            Inquiry inq = new Inquiry();

            //3. call gettmodeldetail
            TModel[] tmodels;
            try {
                GetTModelDetail detail = new GetTModelDetail(portTypeRegistrationTModelId.ID);
                tmodels = inq.GetDetail(detail.Value);

                if (tmodels.Length == 1) {
                    tempRegistration = new OasisPortTypeRegistration();
                    tempRegistration._tModel = tmodels[0];

                    CategoryBag bag = tmodels[0].CategoryBag;

                    KeyedReference bindingNamespace = bag.GetCategoryByName(new UddiOrgXmlNamespace().CategoryName);
                    if (bindingNamespace != null) {
                        tempRegistration.BindingNamespace = new UddiOrgXmlNamespace(new Uri(bindingNamespace.KeyValue));
                    }
                }
            } catch (GetBusinessEntityException) {
                throw;
            } catch (GetTModelDetailException) {
                return null;
            } catch (InquiryUnexpectedException) {
                throw;
            } catch (Exception ex) {
                throw new InquiryUnexpectedException(ex.Message);
            }
            return tempRegistration;
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