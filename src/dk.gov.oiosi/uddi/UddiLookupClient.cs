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
  *   Dennis Søgaard, Accenture
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
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.security;
using System.Net.Mail;
using System.Configuration;
using dk.gov.oiosi.security.cache;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Class for resolving endpoints on the UDDI-based Address Resolution Service (ARS).
    /// </summary>
    public class UddiLookupClient : IUddiLookupClient {
        public const string RASPREGISTRATIONCONFORMANCECLAIM = "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.1/";
        private static readonly ICache<UddiLookupKey, List<UddiService>> getServiceCache = ServiceCache();
        private static readonly ICache<UddiId, UddiTModel> getTModelCache = TModelCache();


        private UDDI_Inquiry_PortTypeClient _uddiProxy;

        /// <summary>
        /// Constructor used normally to do lookup for RASP endpoint types in the UDDI
        /// </summary>
        public UddiLookupClient(Uri address) {
            _uddiProxy = new UDDI_Inquiry_PortTypeClient("OiosiClientEndpointInquiry");
            _uddiProxy.Endpoint.Address = new System.ServiceModel.EndpointAddress(address);
        }

        #region IUddiLookupClient Members

        private static ICache<UddiLookupKey, List<UddiService>> ServiceCache()
        {
            CacheConfig cacheConfig = ConfigurationHandler.GetConfigurationSection<CacheConfig>();

            TimeSpan timeSpan = cacheConfig.UddiServiceTimeSpan;
            ICache<UddiLookupKey, List<UddiService>> serviceCache = new TimedCache<UddiLookupKey, List<UddiService>>(timeSpan);

            return serviceCache;
        }

        private static ICache<UddiId, UddiTModel> TModelCache()
        {
            CacheConfig cacheConfig = ConfigurationHandler.GetConfigurationSection<CacheConfig>();

            TimeSpan timeSpan = cacheConfig.UddiTModelCache;
            ICache<UddiId, UddiTModel> tModelCache = new TimedCache<UddiId, UddiTModel>(timeSpan);

            return tModelCache;
        }

        /*private static System.TimeSpan CreateTimeSpan(string key, int days, int hours, int minutes, int seconds)
        {
            TimeSpan cacheTime;

            string value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                // not defined, using default cache time
                cacheTime = new TimeSpan(days, hours, minutes, seconds);
            }
            else
            {
                if (System.TimeSpan.TryParse(value, out cacheTime))
                {
                    // values succesfull parsed to boolean
                }
                else
                {
                    // parsing to TimeSpan failed
                    // using default cache time
                    cacheTime = new TimeSpan(days, hours, minutes, seconds);
                }
            }

            return cacheTime;
        }*/

        /// <summary>
        /// Translates a business level key ("EndpointKey", e.g. an EAN number) to an endpoint address (e.g. an URL).
        /// </summary>
        public List<UddiLookupResponse> Lookup(LookupParameters lookupParameters) {
            if (lookupParameters == null) throw new ArgumentNullException("lookupParameters");

            List<UddiLookupResponse> supportedResponses = new List<UddiLookupResponse>();
            List<UddiLookupResponse> uddiLookupResponses = GetUddiResponses(lookupParameters);
            foreach (UddiLookupResponse uddiLookupResponse in uddiLookupResponses)
            {
                bool hasAcceptedTransportProtocol = HasAcceptedTransportProtocol(uddiLookupResponse, lookupParameters);
                if (hasAcceptedTransportProtocol)
                {
                    supportedResponses.Add(uddiLookupResponse);
                }
            }
            return supportedResponses;
        }

        public List<ProcessDefinition> GetProcessDefinitions(List<UddiId> processDefinitionIds) {
            List<UddiId> missingIds = new List<UddiId>();
            List<UddiTModel> foundTModels = new List<UddiTModel>();

            //Check the cache for any existing tmodels.
            foreach (UddiId processDefinitionId in processDefinitionIds) {
                UddiTModel tmodel = null;
                if (getTModelCache.TryGetValue(processDefinitionId, out tmodel)) {
                    foundTModels.Add(tmodel);
                    continue;
                }
                missingIds.Add(processDefinitionId);
            }
            
            //Get the tmodels not in the cache
            List<UddiTModel> tmodels = this.GetUddiTModels(missingIds);
            //Adds the tmodels to the cache
            foreach (UddiTModel tmodel in tmodels) {
                getTModelCache.Set(tmodel.UddiId, tmodel);
            }

            List<ProcessDefinition> processDefinitions = new List<ProcessDefinition>();
            foundTModels.AddRange(tmodels);
            foreach (UddiTModel tmodel in foundTModels) {
                UddiId uddiId = tmodel.UddiId;
                string name = tmodel.Name;
                string description = tmodel.Description;
                string profileId = tmodel.GetProfileId();
                string profileTypeId = tmodel.GetProfileTypeId();
                string registrationConformanceClaim = tmodel.GetRegistrationConformanceClaim();

                ProcessDefinition processDefinition = new ProcessDefinition(uddiId, name, description, profileId, profileTypeId, registrationConformanceClaim); 
                processDefinitions.Add(processDefinition);
            }
            return processDefinitions;
        }

        #endregion

        private bool HasAcceptedTransportProtocol(UddiLookupResponse uddiLookupResponse, LookupParameters lookupParameters) {
            var address = uddiLookupResponse.EndpointAddress;
            return lookupParameters.AcceptedTransportProtocols.Contains(address.EndpointAddressTypeCode);
        }

        private List<UddiLookupResponse> GetUddiResponses(LookupParameters lookupParameters) {
            bool filterResponseByProfile = lookupParameters.ProfileIds != null;
            
            List<UddiLookupResponse> lookupResponses = new List<UddiLookupResponse>();
            UddiLookupKey key = new UddiLookupKey(lookupParameters.Identifier, lookupParameters.ServiceId, _uddiProxy.Endpoint.Address.Uri, lookupParameters.ProfileConformanceClaim);

            List<UddiService> uddiServices;
            if (!getServiceCache.TryGetValue(key, out uddiServices)) {
                uddiServices = GetUddiServices(lookupParameters.Identifier, lookupParameters.ServiceId, lookupParameters.ProfileConformanceClaim);
                
                if (uddiServices.Count > 0) {
                    getServiceCache.Set(key, uddiServices);
                }
            }

            UddiLookupResponse lookupResponse;
            foreach (UddiService uddiService in uddiServices) {
                if (uddiService.IsInactiveOrExpired())
                {
                    continue;
                }
                else
                {
                    IEnumerable<UddiBinding> supportedBindings = uddiService.Bindings;
                    if (filterResponseByProfile)
                    {
                        supportedBindings = uddiService.GetBindingsSupportingOneOrMoreProfileAndRole(lookupParameters.ProfileIds, lookupParameters.ProfileRoleIdentifier);
                    }

                    foreach (UddiBinding uddiBinding in supportedBindings)
                    {
                        lookupResponse = GetLookupResponse(lookupParameters, uddiService, uddiBinding);
                        lookupResponses.Add(lookupResponse);
                    }
                }
            }

            return lookupResponses;
        }

        private UddiLookupResponse GetLookupResponse(LookupParameters lookupParameters, UddiService uddiService, UddiBinding uddiBinding) {
            Identifier identifier = lookupParameters.Identifier;
            EndpointAddress endpointAddress = uddiBinding.GetEndpointAddress();
            DateTime activationDateUtc = uddiService.GetActivationDateUtc();
            DateTime expirationDateUtc = uddiService.GetExpirationDateUtc();
            CertificateSubject subject = uddiService.GetCertificateSubject();
            Uri termsOfUse = uddiService.GetTermsOfUseUrl();
            MailAddress mail = uddiService.GetContactMail();
            Version version = uddiService.GetVersion();
            UddiId newerVersion = uddiService.GetNewerVersion();
            UddiId serviceType = uddiBinding.GetPortType().UddiId;
            List<ProcessRoleDefinition> list = uddiBinding.GetProcessRoleDefinitions();
            
            UddiLookupResponse response = new UddiLookupResponse(
                identifier,
                endpointAddress,
                activationDateUtc,
                expirationDateUtc,
                subject,
                termsOfUse,
                mail,
                version,
                newerVersion,
                serviceType,
                list
                );

            return response;
        }

        private List<UddiService> GetUddiServices(Identifier organizationIdentifier, UddiId serviceUddiId, string profileConformanceClaim) {
            keyedReference profileConformanceClaimKeyReference = new keyedReference();
            profileConformanceClaimKeyReference.tModelKey = "uddi:cc5f1df6-ae0a-4781-b24a-f30315893af7";
            profileConformanceClaimKeyReference.keyName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/profileConformanceClaim/";
            profileConformanceClaimKeyReference.keyValue = profileConformanceClaim;
            
            keyedReference registrationConformanceClaim = new keyedReference();
            registrationConformanceClaim.tModelKey = "uddi:80496ef5-4d24-4788-a3f8-12fb54a75106";
            registrationConformanceClaim.keyName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/registrationConformanceClaim/";
            registrationConformanceClaim.keyValue = RASPREGISTRATIONCONFORMANCECLAIM;

            keyedReference endpointKeyType = new keyedReference();
            endpointKeyType.tModelKey = "uddi:182a4a2b-3717-4283-b97c-55cc3b684dae";
            endpointKeyType.keyName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointKeyType/";
            endpointKeyType.keyValue = organizationIdentifier.KeyTypeValue;
            
            keyedReference endpointKey = new keyedReference();
            endpointKey.tModelKey = "uddi:e733684d-9f40-40ff-8807-1d80abc7c665";
            endpointKey.keyName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointKey/";
            endpointKey.keyValue = organizationIdentifier.GetAsString();

            keyedReference[] categories = new[] {profileConformanceClaimKeyReference, registrationConformanceClaim, endpointKeyType, endpointKey};

            categoryBag serviceCategories = new categoryBag {Items = categories};

            find_service findService = new find_service();
            findService.findQualifiers = new[] { FindQualifers.andAllKeys.ToString() };
            if (serviceUddiId != null) {
                findService.tModelBag = new[] { serviceUddiId.ID };
            }
            findService.categoryBag = serviceCategories;

            serviceList listOfServices = _uddiProxy.find_service(findService);

            List<string> endPointUddiIds = new List<string>();

            if (listOfServices.serviceInfos == null) return new List<UddiService>();
            foreach (serviceInfo service in listOfServices.serviceInfos) {
                endPointUddiIds.Add(service.serviceKey);
            }

            // Har uddiid på service endpoint, skal finde endpoint uri
            get_serviceDetail getServiceDetail = new get_serviceDetail();
            getServiceDetail.serviceKey = endPointUddiIds.ToArray();
            serviceDetail detail = _uddiProxy.get_serviceDetail(getServiceDetail);

            if (detail.businessService == null) return new List<UddiService>();

            List<UddiService> uddiServices = new List<UddiService>();
            foreach (businessService businessServiceItem in detail.businessService) {
                List<UddiBinding> uddiBindings = new List<UddiBinding>();
                foreach (bindingTemplate bindingTemplate in  businessServiceItem.bindingTemplates) {
                    
                    List<string> tModelKeys = new List<string>();
                    foreach (tModelInstanceInfo tModel in bindingTemplate.tModelInstanceDetails) {
                        tModelKeys.Add(tModel.tModelKey);
                    }
                    // Get the tModel details:
                    get_tModelDetail tModelDetail = new get_tModelDetail();
                    tModelDetail.tModelKey = tModelKeys.ToArray();
                    tModelDetail modelDetail = _uddiProxy.get_tModelDetail(tModelDetail);

                    List<tModel> uddiTModels = new List<tModel>();
                    foreach (tModel tModelItem in modelDetail.tModel) {
                        uddiTModels.Add(tModelItem);
                    }
                    
                    UddiBinding uddiBinding = new UddiBinding(bindingTemplate, uddiTModels);
                    uddiBindings.Add(uddiBinding);
                }
     
                UddiService uddiService = new UddiService(businessServiceItem, uddiBindings);
                uddiServices.Add(uddiService);
            }

            return uddiServices;
        }

        private List<UddiTModel> GetUddiTModels(IList<UddiId> uddiIds) {
            get_tModelDetail getTModelDetail = new get_tModelDetail();
            getTModelDetail.tModelKey = new string[uddiIds.Count];
            for (int i=0; i<uddiIds.Count; i++) {
                getTModelDetail.tModelKey[i] = uddiIds[i].ID;
            }
            tModelDetail tmodelDetails = _uddiProxy.get_tModelDetail(getTModelDetail);

            if (tmodelDetails.tModel == null) return new List<UddiTModel>();

            List<UddiTModel> uddiTmodels = new List<UddiTModel>();
            foreach (tModel tmodel in tmodelDetails.tModel) {
                UddiTModel uddiTmodel = new UddiTModel(tmodel);
                uddiTmodels.Add(uddiTmodel);
            }
            return uddiTmodels;
        }
    }



    class UddiLookupKey {
        private Identifier identifier;
        private UddiId serviceId;
        private Uri endpoint;
        private string profileConformanceClaim;

        public UddiLookupKey(Identifier identifier, UddiId serviceId, Uri endpoint, string profileConformanceClaim) {
            this.identifier = identifier;
            this.serviceId = serviceId;
            this.endpoint = endpoint;
            this.profileConformanceClaim = profileConformanceClaim;
        }

        public override int GetHashCode() {
            return identifier.GetHashCode();
        }

        public override bool Equals(Object obj) {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;
            UddiLookupKey other = (UddiLookupKey)obj;

            if (!identifier.Equals(other.identifier)) return false;

            if (serviceId == null && other.serviceId != null) return false;
            if (serviceId != null && other.serviceId == null) return false;
            if (serviceId != null && other.serviceId != null && !serviceId.Equals(other.serviceId)) return false;

            if (!endpoint.Equals(other.endpoint)) return false;

            if (!profileConformanceClaim.Equals(other.profileConformanceClaim)) return false;

            return true;
        }
    }
}