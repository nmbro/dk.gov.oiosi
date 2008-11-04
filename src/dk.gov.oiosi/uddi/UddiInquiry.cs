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
using System.Net.Mail;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.security;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;
using dk.gov.oiosi.uddi.Services;
using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.common;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// This class represents an inquiry to the UDDI inquiry API.
    /// It uses the Oiosi UDDI SDK to perform a set of inquiries to a UDDI.
    /// Creates a new instance per request.
    /// </summary>
    public class UddiInquiry {

        private Inquiry uddiLookupLibrary = new Inquiry();

        /// <summary>
        /// UDDI configuration
        /// </summary>
        protected UddiConfig pConfiguration;

        /// <summary>
        /// Constructor
        /// </summary>
        public UddiInquiry(UddiConfig configuration) {
            pConfiguration = configuration;
        }

        /// <summary>
        /// Performs a synchronous inquiry to the UDDI inquiry API.
        /// The endpointKey is the endpoint lookup key.
        /// Use the TranslationOptions object to supply translation filters.
        /// Returns an InquiryResult class, which contains both the resolved endpoint as well as 
        /// UDDI identifiers and other information that can later be used for e.g. caching.
        /// 
        /// Throws an exception if errors are encountered during the inquiry.
        /// </summary>
        /// <param name="endpointKey">The key of the endpoint, e.g. an EAN number</param>
        /// <param name="inquiryParameters">Parameters of the inquiry, e.g. query filters</param>
        /// <param name="lookupPolicies">The policy of the lookup, i.e. query semantics</param>
        /// <returns>Returns an InquiryResult class, which contains both the resolved endpoint as well as 
        /// UDDI identifiers and other information that can later be used for e.g. caching.</returns>
        //public UDDIInquiryResult Inquire(
        public List<UddiLookupResponse> Inquire(IIdentifier endpointKey, LookupParameters inquiryParameters, UddiLookupClientPolicy lookupPolicies) {
            List<UddiLookupResponse> result = new List<UddiLookupResponse>();

            ArsEndpointKey arsEndpointKey = new ArsEndpointKey(inquiryParameters.EndpointKey);
            // Gets the services
            serviceList serviceList = GetServices(arsEndpointKey, inquiryParameters.ServiceContractTModel.ID, inquiryParameters.EndpointKeyType, inquiryParameters.ProfileConformanceClaim, inquiryParameters.RegistrationConformanceClaim);

            if (serviceList.serviceInfos != null && serviceList.serviceInfos.Length > 0) {
                //Get servicedetails for all found services
                string[] servicekeysFound = new string[serviceList.serviceInfos.Length];
                for (int i = 0; i < serviceList.serviceInfos.Length; i++) {
                    servicekeysFound[i] = serviceList.serviceInfos[i].serviceKey;
                }
                BusinessService[] services = GetServiceDetails(servicekeysFound);

                foreach (BusinessService service in services) {
                    // We ignore this service if it is inactive (i.e. not activated yet) or expired
                    if (!IsInactiveOrExpired(service)) {
                        foreach (BindingTemplate template in service.BindingTemplates.Value) {
                            // Get tModel instances:
                            TModel[] tModels = GetTModels(template);
                            // Add endpoint if values equals lookup paramteres
                            if (!IsResponseValid(tModels, inquiryParameters, template)) continue;
                            // Also validates tModel parameters
                            AddResponse(endpointKey, tModels, service, template, result);
                        }
                    }
                }
            }
            return result;
        }

        #region methods

        /// <summary>
        /// Returns true if this service is inactive or expired, according to its UDDI registration.
        /// All registrations on the UDDI are assumed to follow danish time zone conventions
        /// </summary>
        /// <param name="service">The service to check</param>
        /// <returns>Returns true if this service is inactive or expired, according to its UDDI registration.</returns>
        private bool IsInactiveOrExpired(BusinessService service) {
            DateTime activationDateUTC = GetActivationDate(service);
            DateTime expirationDateUTC = GetExpirationDate(service);
            DateTime nowUTC = DateTime.UtcNow;

            return !(nowUTC > activationDateUTC && nowUTC < expirationDateUTC);
        }

        /// <summary>
        /// Returns true if this process instance tModel matches the criteria  specified
        /// by the user
        /// </summary>
        /// <param name="tmodel">The tModel to check</param>
        /// <param name="parameters">Lookup parameters</param>
        /// <returns>True if criteria are matched</returns>
        private bool IsAcceptableProcessInstance(TModel tmodel, LookupParameters parameters) {
            //Check whether one of the expected business processes is present
            if (parameters.HasAnyProcessConstraints) {
                bool foundProcessDefinition = false;
                foreach (UddiId processDefintion in parameters.ProcessDefintions) {
                    BusinessProcessDefinitionReference procRef = new BusinessProcessDefinitionReference();
                    KeyedReference procKeyref = tmodel.CategoryBag.GetCategoryByIdentifierAndKeyName(procRef.CategoryID, procRef.CategoryName);
                    tmodel.CategoryBag.GetCategoryByIdentifier(procRef.CategoryID);
                    if (procKeyref == null) continue;
                    if (procKeyref.KeyValue.Equals(processDefintion.ID, StringComparison.CurrentCultureIgnoreCase)) {
                        foundProcessDefinition = true;
                        break;
                    }
                }
                //The process defintion was not found
                if (!foundProcessDefinition) return false;
            }

            //Check the role identifier type, if relevant:
            if (parameters.RoleIdentifierType != null) {
                BusinessProcessRoleIdentifierType roleIdType = new BusinessProcessRoleIdentifierType();
                KeyedReference roleTypeKeyref = tmodel.CategoryBag.GetCategoryByIdentifierAndKeyName(roleIdType.CategoryID, roleIdType.CategoryName);
                if (roleTypeKeyref == null) return false;
                if (roleTypeKeyref.KeyValue != parameters.RoleIdentifierType.Value) {
                    return false;
                }
            }

            //Check the Role identifier itself, if relevant:
            if (parameters.RoleIdentifier != null) {
                BusinessProcessRoleIdentifier roleId = new BusinessProcessRoleIdentifier();
                KeyedReference roleKeyref = tmodel.IdentifierBagAsCategoryBagObject.GetCategoryByIdentifierAndKeyName(roleId.IdentifierID, roleId.IdentifierName);
                if (roleKeyref == null) return false;
                if (roleKeyref.KeyValue.ToLower() != parameters.RoleIdentifier.Value.ToLower()) {
                    return false;
                }
            }

            //All tests passed, return true
            return true;
        }

        /// <summary>
        /// Returns true if this TModel represents a valid process instance
        /// </summary>
        /// <param name="tmodel">The TModel to check</param>
        /// <param name="parameters">The lookup parameters</param>
        /// <returns>True if valid</returns>
        private bool IsTModelProcessInstance(TModel tmodel, LookupParameters parameters) {
            if (tmodel == null) return false;
            if (tmodel.CategoryBag == null) return false;
            if (tmodel.CategoryBag.Value == null) return false;
            if (tmodel.CategoryBag.Value.Items.Length < 1) return false;

            // Check registration conformance
            RegistrationConformanceClaim regConfClaim = new RegistrationConformanceClaim();
            KeyedReference confClaimKeyref = tmodel.CategoryBag.GetCategoryByIdentifierAndKeyName(regConfClaim.CategoryID, regConfClaim.CategoryName);
            if (confClaimKeyref == null) return false;
            if (confClaimKeyref.KeyValue != parameters.RegistrationConformanceClaim.Value) {
                return false;
            }

            // Check that the businessProcessDefinitionReference category exists:
            BusinessProcessDefinitionReference procRef = new BusinessProcessDefinitionReference();
            KeyedReference procKeyref = tmodel.CategoryBag.GetCategoryByIdentifierAndKeyName(procRef.CategoryID, procRef.CategoryName);

            return procKeyref != null;
        }

        /// <summary>
        /// Checks tModel parameters of a binding, e.g. if the required process-related
        /// categories are set correctly. At least one process-related tModel must 
        /// meet all the specified criteria.
        /// </summary>
        /// <param name="tmodels">The tModels for the binding to check</param>
        /// <param name="parameters">The lookup parameters specifying the criteria</param>
        /// <returns>True if criteria are met</returns>
        private bool MeetsTModelCriteria(IEnumerable<TModel> tmodels, LookupParameters parameters) {
            // Have any process-related criteria been set?
            if (!parameters.HasAnyProcessConstraints && !parameters.HasAnyProcessRoleConstraints) return true;

            foreach (TModel tm in tmodels) {
                bool isProcessInstance = IsTModelProcessInstance(tm, parameters);
                if (!isProcessInstance) continue;
                bool isAcceptableProcessInstance = IsAcceptableProcessInstance(tm, parameters);
                if (isAcceptableProcessInstance) return true;
            }
            return false;
        }

        private bool IsResponseValid(TModel[] tmodels, LookupParameters parameters, BindingTemplate binding) {
            AccessPoint accessPoint = binding.AccessPoint;
            // Check properties of associated tModels:
            if (!MeetsTModelCriteria(tmodels, parameters)) return false;
            // Make a new endpointaddress
            EndpointAddress endpointAddress = IdentifierUtility.GetEndpointAddressFromString(accessPoint.AccessEndpointString);
            // Check against address type filter:
            if (!IsAcceptableAddressType(endpointAddress, parameters)) return false;
            return true;
        }

        /// <summary>
        /// Creates endpointinformation, if the category value are as expected,
        /// and adds it to the list of results
        /// </summary>
        private void AddResponse(IIdentifier endpointKey, TModel[] tmodels, BusinessService service, BindingTemplate binding, List<UddiLookupResponse> results) {
            AccessPoint accessPoint = binding.AccessPoint;

            // Make a new endpointaddress
            EndpointAddress endpointAddress = IdentifierUtility.GetEndpointAddressFromString(accessPoint.AccessEndpointString);

            // Get endpoint activation and expiration time
            DateTime activationUTC = GetActivationDate(service);
            DateTime expirationUTC = GetExpirationDate(service);

            // Get endpoint certificate subject
            string cert = GetCertificateSubject(service);

            // Get terms of use url:
            Uri termsOfUse = GetTermsOfUseUrl(service);

            // Get contact address:
            MailAddress contactMail = GetContactMail(service);

            // Get version:
            Version version = GetVersion(service);

            // Get newer version reference:
            UddiId newerVersion = GetNewerVersion(service);

            // Make a new UddiLookupResponse
            CertificateSubject certificateSubject = new CertificateSubject(cert);
            //TODO: return the processes here
            UddiLookupResponse resp = new UddiLookupResponse(endpointKey, endpointAddress, activationUTC, expirationUTC, certificateSubject, termsOfUse, contactMail, version, newerVersion);
                
            // Add the endpointinformation to the result object
            results.Add(resp);
        }

        /// <summary>
        /// Checks if an endpoint address is of a type that is on the endpoint filter list.
        /// </summary>
        /// <param name="address">The address to check</param>
        /// <param name="parameters">The parameter object holding the filter</param>
        /// <returns>True if the address type is on the list</returns>
        private bool IsAcceptableAddressType(EndpointAddress address, LookupParameters parameters) {
            if (parameters.AddressTypeFilter == null || parameters.AddressTypeFilter.Count < 1) return true;

            if (address is EndpointAddressSMTP) {
                foreach (EndpointAddressTypeCode typecode in parameters.AddressTypeFilter) {
                    if (typecode == EndpointAddressTypeCode.email) return true;
                }
            } else if (address is EndpointAddressHttp) {
                foreach (EndpointAddressTypeCode typecode in parameters.AddressTypeFilter) {
                    if (typecode == EndpointAddressTypeCode.http) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets certificate subject from category value
        /// </summary>
        private string GetCertificateSubject(BusinessService service) {
            CategoryBag bag = service.CategoryBag;
            KeyedReference[] certificateCategories = bag.GetCategoryByIdentifier(EndpointCertificate.DEFAULTCATEGORYID);
            if (certificateCategories == null || certificateCategories.Length < 1) throw new Exception("TODO: no certificate category found.");
            if (certificateCategories.Length > 1) throw new Exception("Ambigious certificate categories found.");

            KeyedReference certificate = certificateCategories[0];
            return certificate.KeyValue;
        }

        private Uri GetTermsOfUseUrl(BusinessService service) {
            TermsOfUseUrl terms = new TermsOfUseUrl();
            KeyedReference termsRef = service.CategoryBag.GetCategoryByIdentifierAndKeyName(terms.CategoryID, terms.CategoryName);
            if (termsRef != null) {
                if (!String.IsNullOrEmpty(termsRef.KeyValue)) {
                    return new Uri(termsRef.KeyValue);
                } else {
                    return null;
                }
            } else {
                return null;
            }
        }

        private MailAddress GetContactMail(BusinessService service) {
            EndpointContactEmail mail = new EndpointContactEmail();
            KeyedReference mailRef = service.CategoryBag.GetCategoryByIdentifierAndKeyName(mail.CategoryID, mail.CategoryID);
            if (mailRef != null) {
                if (!String.IsNullOrEmpty(mailRef.KeyValue)) {
                    return new MailAddress(mailRef.KeyValue);
                } else {
                    return null;
                }
            } else {
                return null;
            }
        }

        private Version GetVersion(BusinessService service) {
            Version v = null;
            VersionMajor major = new VersionMajor();
            major = (VersionMajor)GetCategory(service, major);
            if (major == null) return null;

            VersionMinor minor = new VersionMinor();
            minor = (VersionMinor)GetCategory(service, minor);

            VersionRevision rev = new VersionRevision();
            rev = (VersionRevision)GetCategory(service, rev);

            // 2. Create the version number
            int majorInt = 0;
            int minorInt = 0;
            int revisionInt = 0;

            majorInt = Int32.Parse(major.Value);
            if (minor != null) minorInt = Int32.Parse(minor.Value);
            if (rev != null) revisionInt = Int32.Parse(rev.Value);

            v = new Version(majorInt, minorInt, revisionInt);
            return v;
        }

        private UddiId GetNewerVersion(BusinessService service) {
            NewerVersionReference newRef = new NewerVersionReference();
            KeyedReference newVersKeyref = service.CategoryBag.GetCategoryByIdentifierAndKeyName(newRef.CategoryID, newRef.CategoryName);
            //TODO: is it ok to only return null here??
            if (newVersKeyref == null) return null;
            if (String.IsNullOrEmpty(newVersKeyref.KeyValue)) return null;
            return IdentifierUtility.GetUddiIDFromString(newVersKeyref.KeyValue);
        }

        /// <summary>
        /// Gets an ARS category by the category ID of the 'categoryObject' 
        /// parameter. Note that we need an actual ArsCategory descendant 
        /// object to get the category identifier. If no match is found, null is returned.
        /// </summary>
        /// <param name="service">The business service that holds the category bag to search</param>
        /// <param name="category">The category object from which to get the category ID.
        /// Another object, with key and value set, is returned as a result.</param>
        /// <returns></returns>
        private ArsCategory GetCategory(BusinessService service, ArsCategory category) {
            if (category == null) return null;
            KeyedReference keyRef = service.CategoryBag.GetCategoryByIdentifierAndKeyName(category.CategoryID, category.CategoryName);
            if (keyRef == null) return null;
            category.SetCategoryValue(keyRef.KeyName, keyRef.KeyValue);
            return category;
        }

        /// <summary>
        /// Returns a DateTime representation of either an endpoint expiration
        /// or activation date, in UTC
        /// </summary>
        /// <param name="datestring">The date of an endpoint expiration or activation</param>
        /// <param name="getActivationDate">True if the date represents an activation date,
        /// false if expiration date. This influences which default is returned, if no date
        /// is present in the string (DateTime.MinValue for activation date, 
        /// DateTime.MaxValue for expiration time</param>
        /// <returns>
        /// Returns a DateTime representation of either an endpoint expiration
        /// or activation date, in UTC
        /// </returns>
        private DateTime GetDatetimeFromLifetimeDates(string datestring, bool getActivationDate) {
            if (String.IsNullOrEmpty(datestring)) {
                // Then set default date:
                if (getActivationDate) return DateTime.MinValue;
                return DateTime.MaxValue;
            } 
            if (datestring.Length < 20) {
                // Heuristic, treat as local date:
                return DateTime.Parse(datestring);
            }
            // Heuristic, treat as UTC date:
            return DateTime.Parse(datestring, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        }

        /// <summary>
        /// Gets endpoint activation date from category value, in UTC
        /// </summary>
        private DateTime GetActivationDate(BusinessService service) {
            CategoryBag bag = service.CategoryBag;
            KeyedReference act = bag.GetCategoryByName(new EndpointActivationDate().CategoryName);
            //There is no activation date, return minimum value
            if (act == null) return DateTime.MinValue;
            return GetDatetimeFromLifetimeDates(act.KeyValue, false);
        }

        /// <summary>
        /// Gets endpoint expiration date from category value, in UTC
        /// </summary>
        private DateTime GetExpirationDate(BusinessService service) {
            CategoryBag bag = service.CategoryBag;
            KeyedReference exp = bag.GetCategoryByName(new EndpointExpirationDate().CategoryName);
            //There is no expiration date, return max value
            if (exp == null) return DateTime.MaxValue;
            return GetDatetimeFromLifetimeDates(exp.KeyValue, false);
        }

       
        /// <summary>
        /// Gets authenticationRequired bool from category value
        /// 
        /// TODO: this is never called, WHY ???
        /// </summary>
        private bool GetAuthenticationRequired(BusinessService service) {

            bool authenticationRequired = false;

            CategoryBag bag = service.CategoryBag;
            KeyedReference autRequired = bag.GetCategoryByName(new AuthenticationRequired().CategoryName);

            if (autRequired != null) {
                if (autRequired.KeyValue == "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/authenticationRequired/") {
                    authenticationRequired = true;
                }
            }
            return authenticationRequired;
        }

        /// <summary>
        /// Calls get_serviceDetails from a list of servicekeys
        /// </summary>
        private BusinessService[] GetServiceDetails(string[] servicekeysFound) {
            //1. get_servicedetail foreach service found
            BusinessService[] services;
            try {
                GetServiceDetail serviceDetail = new GetServiceDetail(servicekeysFound);
                services = uddiLookupLibrary.GetDetail(serviceDetail.Value);
            }
            catch (Exception ex) {
                //TODO: how to handle UDDI specific exceptions
                throw new Exception("TODO: failed to get service details", ex);
            }
            //Removes any null cases
            if (services == null)
                services = new BusinessService[0];
            return services;
        }

        /// <summary>
        /// Calls find_service with relevant parameters
        /// 
        /// TODO: why does the uddi library return a serviceList? low lvl uddi?
        /// </summary>
        private serviceList GetServices(ArsEndpointKey arsEndpointKey, string centralServiceDefinitionTModelKey, EndpointKeytype endpointKeyType, ConformanceClaim profileConformanceClaim, RegistrationConformanceClaim registrationConformanceClaim) {
            //1. set categories
            CategoryBag serviceCategories = new CategoryBag();
            if (profileConformanceClaim != null) {
                serviceCategories.AddCategory(profileConformanceClaim.GetAsKeyedReference());
            }
            if (registrationConformanceClaim != null) {
                serviceCategories.AddCategory(registrationConformanceClaim.GetAsKeyedReference());
            }

            if (endpointKeyType != null) {
                serviceCategories.AddCategory(endpointKeyType.GetAsKeyedReference());
            }

            // NOTE: adding the EAN key LAST in the category bag seems to increase systinet 
            // UDDI registry performance dramatically. 
            serviceCategories.AddCategory(arsEndpointKey.GetAsKeyedReference());

            FindQualifers[] findQualifiers = new FindQualifers[1];
            findQualifiers[0] = FindQualifers.andAllKeys;
            FindService findService = new FindService(findQualifiers, serviceCategories);

            //add a reference to the central service definition tmodel
            string[] centralServiceDefinitionKey = { centralServiceDefinitionTModelKey };
            findService.Value.tModelBag = centralServiceDefinitionKey;
            
            //get services
            serviceList services;
            try {
                services = uddiLookupLibrary.Find(findService);
            }
            catch (Exception ex) {
                throw new Exception("TODO: failed to get services from UDDI", ex);
            }
            if (services == null) {
                services = new serviceList();
            }

            return services;
        }

        /// <summary>
        /// Calls get_tModelDetails from a binding template
        /// </summary>
        private TModel[] GetTModels(BindingTemplate template) {
            List<string> tModelKeys = new List<string>();

            // Run through all tmodelinstanceinfos to check tmodel
            foreach (TModelInstanceInfo instanceinfo in template.TModelInstanceInfos) {
                tModelKeys.Add(instanceinfo.Value.tModelKey);
            }

            // Get the tModel details:
            get_tModelDetail detail = new get_tModelDetail();
            detail.tModelKey = tModelKeys.ToArray();
            return uddiLookupLibrary.GetDetail(detail);
        }

        #endregion methods
    }
}