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
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common.cache;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Class for resolving endpoints on the UDDI-based Address Resolution Service (ARS).
    /// </summary>
    public class UddiLookupClient : IUddiLookupClient {

        private static readonly ICache<UddiLookupKey, List<UddiService>> uddiCache = new TimedCache<UddiLookupKey, List<UddiService>>(new TimeSpan(14, 0, 0, 0));

        private UDDI_Inquiry_PortTypeClient _uddiProxy;

        /// <summary>
        /// Constructor
        /// </summary>
        public UddiLookupClient(Uri address) {
            //UDDI_Inquiry_PortTypeClient _uddiProxy = new UDDI_Inquiry_PortTypeClient("OiosiClientEndpointInquiry");
            _uddiProxy = new UDDI_Inquiry_PortTypeClient("OiosiClientEndpointInquiry");
            _uddiProxy.Endpoint.Address = new System.ServiceModel.EndpointAddress(address);

        }

        /// <summary>
        /// Translates a business level key ("EndpointKey", e.g. an EAN number) to an endpoint address (e.g. an URL).
        /// </summary>
        public List<UddiLookupResponse> Lookup(LookupParameters lookupParameters) {
            if (lookupParameters == null) throw new ArgumentNullException("lookupParameters");

            List<UddiLookupResponse> supportedResponses = new List<UddiLookupResponse>();
            var uddiLookupResponses = GetUddiResponses(lookupParameters);
            foreach (var uddiLookupResponse in uddiLookupResponses) {
                bool hasAcceptedTransportProtocol = HasAcceptedTransportProtocol(uddiLookupResponse, lookupParameters);
                if (hasAcceptedTransportProtocol) {
                    supportedResponses.Add(uddiLookupResponse);
                }
            }
            return supportedResponses;
        }


        private bool HasAcceptedTransportProtocol(UddiLookupResponse uddiLookupResponse, LookupParameters lookupParameters) {
            var address = uddiLookupResponse.EndpointAddress;
            return lookupParameters.AcceptedTransportProtocols.Contains(address.EndpointAddressTypeCode);
        }

        private List<UddiLookupResponse> GetUddiResponses(LookupParameters lookupParameters) {
            bool filterResponseByProfile = lookupParameters.ProfileIds != null;
            
            List<UddiLookupResponse> lookupResponses = new List<UddiLookupResponse>();
            UddiLookupKey key = new UddiLookupKey(lookupParameters.Identifier, lookupParameters.ServiceId, _uddiProxy.Endpoint.Address.Uri);

            List<UddiService> uddiServices;
            if (!uddiCache.TryGetValue(key, out uddiServices)) {
                uddiServices = GetUddiServices(lookupParameters.Identifier, lookupParameters.ServiceId);
                uddiCache.Set(key, uddiServices);
            }
            
            foreach (UddiService uddiService in uddiServices) {
                if(uddiService.IsInactiveOrExpired()) continue;

                IEnumerable<UddiBinding> supportedBindings = uddiService.Bindings;
                if (filterResponseByProfile) {
                    supportedBindings = uddiService.GetBindingsSupportingOneOrMoreProfileAndRole(lookupParameters.ProfileIds, lookupParameters.ProfileRoleIdentifier);
                }
                
                foreach (UddiBinding uddiBinding in supportedBindings) {
                    var lookupResponse = GetLookupResponse(lookupParameters, uddiService, uddiBinding);
                    lookupResponses.Add(lookupResponse);
                }
            }

            return lookupResponses;
        }

        private UddiLookupResponse GetLookupResponse(LookupParameters lookupParameters, UddiService uddiService, UddiBinding uddiBinding) {
            return new UddiLookupResponse(
                lookupParameters.Identifier,
                uddiBinding.EndpointAddress,
                uddiService.ActivationDateUTC,
                uddiService.ExpirationDateUTC,
                uddiService.CertificateSubject,
                uddiService.TermsOfUseUri,
                uddiService.ContactMail,
                uddiService.Version,
                uddiService.NewerVersion,
                uddiBinding.Processes
                );
        }

        private List<UddiService> GetUddiServices(Identifier organizationIdentifier, UddiId serviceUddiId) {

            keyedReference profileConformanceClaim = new keyedReference();
            profileConformanceClaim.tModelKey = "uddi:cc5f1df6-ae0a-4781-b24a-f30315893af7";
            profileConformanceClaim.keyName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/profileConformanceClaim/";
            profileConformanceClaim.keyValue = "http://oio.dk/profiles/OIOSI/1.0/secureReliableAsyncProfile/1.0/";
            
            keyedReference registrationConformanceClaim = new keyedReference();
            registrationConformanceClaim.tModelKey = "uddi:80496ef5-4d24-4788-a3f8-12fb54a75106";
            registrationConformanceClaim.keyName = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Categories/registrationConformanceClaim/";
            registrationConformanceClaim.keyValue = "http://oio.dk/profiles/OIOSI/1.0/UDDI/registrationModel/1.1/";

            keyedReference endpointKeyType = new keyedReference();
            endpointKeyType.tModelKey = "uddi:182a4a2b-3717-4283-b97c-55cc3b684dae";
            endpointKeyType.keyName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointKeyType/";
            endpointKeyType.keyValue = organizationIdentifier.KeyTypeValue;
            
            keyedReference endpointKey = new keyedReference();
            endpointKey.tModelKey = "uddi:e733684d-9f40-40ff-8807-1d80abc7c665";
            endpointKey.keyName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/endpointKey/";
            endpointKey.keyValue = organizationIdentifier.GetAsString();

            keyedReference[] categories = new[] {profileConformanceClaim, registrationConformanceClaim, endpointKeyType, endpointKey};

            categoryBag serviceCategories = new categoryBag {Items = categories};

            find_service findService = new find_service();
            findService.findQualifiers = new[] { FindQualifers.andAllKeys.ToString() };
            findService.tModelBag = new[] { serviceUddiId.ID };
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

            List<UddiService> uddiServices = new List<UddiService>();
            foreach (businessService businessServiceItem in detail.businessService) {
                List<UddiBinding> uddiBindings = new List<UddiBinding>();
                foreach (bindingTemplate bindingTemplate in  businessServiceItem.bindingTemplates) {
                    
                    // TODO Lav tModel opslag på alt, og vælg derefter udfra key
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
    }

    class UddiLookupKey
    {
        private Identifier identifier;
        private UddiId serviceId;
        private Uri endpoint;

        public UddiLookupKey(Identifier identifier, UddiId serviceId, Uri endpoint)
        {
            this.identifier = identifier;
            this.serviceId = serviceId;
            this.endpoint = endpoint;
        }

        public override int GetHashCode()
        {
            return identifier.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;
            UddiLookupKey other = (UddiLookupKey)obj;

            if (!identifier.Equals(other.identifier)) return false;
            
            if (!serviceId.Equals(other.serviceId)) return false;

            if (!endpoint.Equals(other.endpoint)) return false;

            return true;
        }
    }
}