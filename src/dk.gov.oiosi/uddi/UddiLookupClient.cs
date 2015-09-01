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
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.uddi
{

    /// <summary>
    /// Class for resolving endpoints on the UDDI-based Address Resolution Service (ARS).
    /// </summary>
    public class UddiLookupClient : IUddiLookupClient
    {
        public const string RASPREGISTRATIONCONFORMANCECLAIM = "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.1/";
        private ICache<UddiLookupKey, IList<UddiService>> uddiServiceCache;
        private ICache<UddiId, UddiTModel> uddiTModelCache;
        private ILogger logger;

        private UDDI_Inquiry_PortTypeClient uddiProxy;

        private string raspVersion = string.Empty;

        public UddiLookupClient()
        {
            this.uddiServiceCache = this.CreateUddiServiceCache();
            this.uddiTModelCache = this.CreateUddiTModelCache();
            this.logger = LoggerFactory.Create(this.GetType());
            this.raspVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Constructor used normally to do lookup for RASP endpoint types in the UDDI
        /// </summary>
        public UddiLookupClient(Uri address)
        {
            this.uddiServiceCache = this.CreateUddiServiceCache();
            this.uddiTModelCache = this.CreateUddiTModelCache();
            this.logger = LoggerFactory.Create(this.GetType());
            this.raspVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            try
            {
                this.uddiProxy = new UDDI_Inquiry_PortTypeClient("OiosiClientEndpointInquiry");
            }
            catch(Exception)                
            {
                // If this creation failed, it is possible that you are missing the app.config file in your project
                // If it still fails, the library version in in configuration file must be update to current version.
                // Se configuration/system.serviceModel/extensions/behaviorExtensions/add[name="signCustomHeaders"]
                this.logger.Error("Creation of UDDI_Inquiry_PortTypeClient failed. It is possible that you are missing the app.config file in your project, or the library version in in configuration file must be update to current version. configuration/system.serviceModel/extensions/behaviorExtensions/add[@name=signCustomHeaders)/@type=... ");
                throw;
            }

            this.uddiProxy.Endpoint.Address = new System.ServiceModel.EndpointAddress(address + "?platform=Net&raspVersion=" + raspVersion);
        }

        #region IUddiLookupClient Members

        private ICache<UddiLookupKey, IList<UddiService>> CreateUddiServiceCache()
        {
            ICache<UddiLookupKey, IList<UddiService>> uddiServiceCache = CacheFactory.Instance.UddiServiceCache;

            return uddiServiceCache;
        }

        private ICache<UddiId, UddiTModel> CreateUddiTModelCache()
        {
            ICache<UddiId, UddiTModel> uddiTModelCache = CacheFactory.Instance.UddiTModelCache;

            return uddiTModelCache;
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
        public List<UddiLookupResponse> Lookup(LookupParameters lookupParameters)
        {
            if (lookupParameters == null)
            {
                throw new ArgumentNullException("lookupParameters");
            }

            List<UddiLookupResponse> supportedResponses = new List<UddiLookupResponse>();
            IList<UddiLookupResponse> uddiLookupResponses = GetUddiResponses(lookupParameters);
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

        public List<ProcessDefinition> GetProcessDefinitions(List<UddiId> processDefinitionIds)
        {
            IList<UddiId> missingIds = new List<UddiId>();
            List<UddiTModel> foundTModels = new List<UddiTModel>();

            //Check the cache for any existing tmodels.
            foreach (UddiId processDefinitionId in processDefinitionIds)
            {
                UddiTModel tmodel = null;
                if (uddiTModelCache.TryGetValue(processDefinitionId, out tmodel))
                {
                    foundTModels.Add(tmodel);
                    continue;
                }
                missingIds.Add(processDefinitionId);
            }

            //Get the tmodels not in the cache
            IList<UddiTModel> tmodels = this.GetUddiTModels(missingIds);
            //Adds the tmodels to the cache
            foreach (UddiTModel tmodel in tmodels)
            {
                uddiTModelCache.Set(tmodel.UddiId, tmodel);
            }

            List<ProcessDefinition> processDefinitions = new List<ProcessDefinition>();
            foundTModels.AddRange(tmodels);
            foreach (UddiTModel tmodel in foundTModels)
            {
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

        private bool HasAcceptedTransportProtocol(UddiLookupResponse uddiLookupResponse, LookupParameters lookupParameters)
        {
            EndpointAddress address = uddiLookupResponse.EndpointAddress;
            bool result = lookupParameters.AcceptedTransportProtocols.Contains(address.EndpointAddressTypeCode);
            
            return result;
        }

        private IList<UddiLookupResponse> GetUddiResponses(LookupParameters lookupParameters)
        {
            bool filterResponseByProfile = lookupParameters.ProfileIds != null;

            IList<UddiLookupResponse> lookupResponses = new List<UddiLookupResponse>();
            UddiLookupKey key = new UddiLookupKey(lookupParameters.Identifier, lookupParameters.ServiceId, this.uddiProxy.Endpoint.Address.Uri, lookupParameters.ProfileConformanceClaim);

            IList<UddiService> uddiServices;
            if (!uddiServiceCache.TryGetValue(key, out uddiServices))
            {
                uddiServices = this.GetUddiServices(lookupParameters.Identifier, lookupParameters.ServiceId, lookupParameters.ProfileConformanceClaim);

                if (uddiServices.Count > 0)
                {
                    uddiServiceCache.Set(key, uddiServices);
                }
            }

            UddiLookupResponse lookupResponse;
            foreach (UddiService uddiService in uddiServices)
            {
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

        private UddiLookupResponse GetLookupResponse(LookupParameters lookupParameters, UddiService uddiService, UddiBinding uddiBinding)
        {
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

        private IList<UddiService> GetUddiServices(Identifier organizationIdentifier, UddiId serviceUddiId, string profileConformanceClaim)
        {
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
            endpointKeyType.keyValue = organizationIdentifier.KeyTypeCode;

            keyedReference endpointKey = new keyedReference();
            endpointKey.tModelKey = "uddi:e733684d-9f40-40ff-8807-1d80abc7c665";
            endpointKey.keyName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointKey/";
            endpointKey.keyValue = organizationIdentifier.GetAsString();

            keyedReference[] categories = new[] { profileConformanceClaimKeyReference, registrationConformanceClaim, endpointKeyType, endpointKey };

            categoryBag serviceCategories = new categoryBag { Items = categories };

            find_service findService = new find_service();
            findService.findQualifiers = new string[] { FindQualifers.andAllKeys.ToString() };
            if (serviceUddiId != null)
            {
                findService.tModelBag = new string[] { serviceUddiId.ID };
            }
            findService.categoryBag = serviceCategories;

            serviceList listOfServices = this.uddiProxy.find_service(findService);

            List<string> endPointUddiIds = new List<string>();

            if (listOfServices.serviceInfos == null) return new List<UddiService>();
            foreach (serviceInfo service in listOfServices.serviceInfos)
            {
                endPointUddiIds.Add(service.serviceKey);
            }

            // Har uddi-ID på service endpoint, skal finde endpoint uri
            get_serviceDetail getServiceDetail = new get_serviceDetail();
            getServiceDetail.serviceKey = endPointUddiIds.ToArray();
            serviceDetail detail = this.uddiProxy.get_serviceDetail(getServiceDetail);

            if (detail.businessService == null) return new List<UddiService>();

            IList<UddiService> uddiServices = new List<UddiService>();
            foreach (businessService businessServiceItem in detail.businessService)
            {
                List<UddiBinding> uddiBindings = new List<UddiBinding>();
                foreach (bindingTemplate bindingTemplate in businessServiceItem.bindingTemplates)
                {

                    List<string> tModelKeys = new List<string>();
                    foreach (tModelInstanceInfo tModel in bindingTemplate.tModelInstanceDetails)
                    {
                        tModelKeys.Add(tModel.tModelKey);
                    }

                    // Get the tModel details:
                    get_tModelDetail tModelDetail = new get_tModelDetail();
                    tModelDetail.tModelKey = tModelKeys.ToArray();
                    tModelDetail modelDetail = this.uddiProxy.get_tModelDetail(tModelDetail);

                    List<tModel> uddiTModels = new List<tModel>();
                    foreach (tModel tModelItem in modelDetail.tModel)
                    {
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

        private IList<UddiTModel> GetUddiTModels(IList<UddiId> uddiIds)
        {
            get_tModelDetail getTModelDetail = new get_tModelDetail();
            getTModelDetail.tModelKey = new string[uddiIds.Count];
            for (int i = 0; i < uddiIds.Count; i++)
            {
                getTModelDetail.tModelKey[i] = uddiIds[i].ID;
            }
            tModelDetail tmodelDetails = this.uddiProxy.get_tModelDetail(getTModelDetail);

            if (tmodelDetails.tModel == null) return new List<UddiTModel>();

            IList<UddiTModel> uddiTmodels = new List<UddiTModel>();
            foreach (tModel tmodel in tmodelDetails.tModel)
            {
                UddiTModel uddiTmodel = new UddiTModel(tmodel);
                uddiTmodels.Add(uddiTmodel);
            }
            return uddiTmodels;
        }
    }
}