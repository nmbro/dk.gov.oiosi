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
using dk.gov.oiosi.uddi.Businesses;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Utility class. Holds methods for queriying an UDDI registry following the ARS
    /// profile
    /// </summary>
    public class ArsLookup {

        /// <summary>
        /// Gets all businessentities with a given CVR number. 
        /// </summary>
        /// <param name="cvr">the cvr to use for lookup</param>
        /// <returns>a list of businessentities</returns>
        public static List<businessInfo> FindArsBusinessEntitiesByCvr(string cvr) {

            List<businessInfo> returnList = new List<businessInfo>();

            try {
                //1. make a FindBusiness object
                FindBusiness findBusiness = new FindBusiness();

                //2. create a categorybag with the categories to look for
                CategoryBag categoryBag = new CategoryBag();
                CategoryBag identifierBag = new CategoryBag();
                

                KeyedReference orgKeyType = new OrganizationKeyType().GetAsKeyedReference();
                orgKeyType.KeyValue = "http://oio.dk/profiles/OWSA/modelT/1.0/UDDI/Identifiers/cvrNumber/";

                KeyedReference orgKey = new OrganizationKey().GetAsKeyedReference();
                orgKey.KeyValue = cvr;

                categoryBag.AddCategory(new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());
                categoryBag.AddCategory(orgKeyType);
                identifierBag.AddCategory(orgKey);

                findBusiness.Value.categoryBag = categoryBag.Value;
                findBusiness.Value.identifierBag = identifierBag.GetInnerCollectionAsKeyedReferenceCollection().GetKeyedReferenceCollection();

                //3. call Find method in SDK
                Inquiry inq = new Inquiry();
                businessList list = inq.Find(findBusiness);

                if (list.businessInfos != null)
                    for (int k = 0; k < list.businessInfos.Length; k++) {
                        returnList.Add(list.businessInfos[k]);
                    }
            } catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }

        /// <summary>
        /// Gets all businessentities with a given name. Wildcard "%" can be used.
        /// </summary>
        /// <param name="name">the name to use for lookup</param>
        /// <returns>a list of businessentities</returns>
        public static List<businessInfo> FindArsBusinessEntities(string name) {

            List<businessInfo> returnList = new List<businessInfo>();

            try {
                //1. make a FindBusiness object with the name to lookup
                FindBusiness findBusiness = new FindBusiness(name);

                //2. create a categorybag with the categories to look for
                CategoryBag catbag = new CategoryBag();

                KeyedReference keyedref = new OrganizationKeyType().GetAsKeyedReference();
                keyedref.KeyValue = "%";

                catbag.AddCategory(new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());
                catbag.AddCategory(keyedref);

                findBusiness.Value.categoryBag = catbag.Value;

                //3. call Find method in SDK
                Inquiry inq = new Inquiry();
                businessList list = inq.Find(findBusiness);

                if (list.businessInfos!=null)
                    for (int k = 0; k < list.businessInfos.Length; k++)
                    {
                        returnList.Add(list.businessInfos[k]);
                    }
            }
            catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }


        private static List<tModelInfo> LookupArsServiceDefinition(string name, string transport) {

            List<tModelInfo> returnList = new List<tModelInfo>();

            try {
                //1. make a FindTModel object with the name to lookup
                FindTModel findTModel = new FindTModel(name);

                //2. create a categorybag with the categories to look for
                CategoryBag catbag = new CategoryBag();

                KeyedReference uddiOrgXmlNS = new UddiOrgXmlNamespace().GetAsKeyedReference();
                uddiOrgXmlNS.KeyValue = "%";

                UddiOrgWsdlTypes uddiOrgWsdlTypes = new UddiOrgWsdlTypes(UddiOrgWsdlTypeCode.binding);

                KeyedReference uddiOrgWsdlPortType = new UddiOrgWsdlPortTypeReference().GetAsKeyedReference();
                uddiOrgWsdlPortType.KeyValue = "%";

                KeyedReference uddiOrgWsdlCategorizationProtocol = new UddiOrgWsdlCategorizationProtocol().GetAsKeyedReference();
                uddiOrgWsdlCategorizationProtocol.KeyValue = "%";

                KeyedReference uddiOrgWsdlCategorizationTransport = null;
                if (transport.Length > 0) {
                    UddiOrgWsdlCategorizationTransportCode transportcode = (UddiOrgWsdlCategorizationTransportCode)Enum.Parse(typeof(UddiOrgWsdlCategorizationTransportCode), transport);
                    switch (transportcode) {
                        case UddiOrgWsdlCategorizationTransportCode.http:
                            uddiOrgWsdlCategorizationTransport = new UddiOrgWsdlCategorizationTransport(UddiOrgWsdlCategorizationTransportCode.http).GetAsKeyedReference();
                            break;
                        case UddiOrgWsdlCategorizationTransportCode.smtp:
                            uddiOrgWsdlCategorizationTransport = new UddiOrgWsdlCategorizationTransport(UddiOrgWsdlCategorizationTransportCode.smtp).GetAsKeyedReference();
                            break;
                        default:
                            break;
                    }
                }
                else {
                    uddiOrgWsdlCategorizationTransport = new UddiOrgWsdlCategorizationProtocol().GetAsKeyedReference();
                    uddiOrgWsdlCategorizationTransport.KeyValue = "%";
                }
                
                UddiOrgTypes uddiOrgTypes = new UddiOrgTypes(UddiOrgTypesCode.wsdlSpec);

                catbag.AddCategory(new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());
                catbag.AddCategory(uddiOrgXmlNS);
                catbag.AddCategory(uddiOrgWsdlTypes.GetAsKeyedReference());
                catbag.AddCategory(uddiOrgWsdlPortType);
                catbag.AddCategory(uddiOrgWsdlCategorizationProtocol);
                catbag.AddCategory(uddiOrgWsdlCategorizationTransport);
                catbag.AddCategory(uddiOrgTypes.GetAsKeyedReference());

                findTModel.CategoryBag = catbag.Value;

                //3. call Find method in SDK
                Inquiry inq = new Inquiry();
                tModelList list = inq.Find(findTModel);

                if (list.tModelInfos != null)
                    for (int k = 0; k < list.tModelInfos.Length; k++) {
                        returnList.Add(list.tModelInfos[k]);
                    }
            }
            catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }


        /// <summary>
        /// Gets all central service definition (binding) tmodels. Wildcard "%" can be used.
        /// </summary>
        /// <param name="name">the name to use for lookup</param>
        /// <param name="transport">the transport parameter used in the lookup</param>
        /// <returns>a list of tmodels</returns>
        public static List<tModelInfo> FindArsServiceDefinition(string name, string transport) {

            List<tModelInfo> returnList = new List<tModelInfo>();

            try {
                returnList = LookupArsServiceDefinition(name, transport);
            }
            catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }

        /// <summary>
        /// Gets all central service definition (binding) tmodels. Wildcard "%" can be used.
        /// </summary>
        /// <param name="name">the name to use for lookup</param>
        /// <returns>a list of tmodels</returns>
        public static List<tModelInfo> FindArsServiceDefinition(string name) {

            List<tModelInfo> returnList = new List<tModelInfo>();

            try
            {
                returnList = LookupArsServiceDefinition(name, "");
            }
            catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }

        /// <summary>
        /// Gets all central service definition (binding) tmodels. Wildcard "%" can be used.
        /// </summary>
        /// <param name="name">the name to use for lookup</param>
        /// <returns>a list of tmodels</returns>
        public static List<tModelInfo> FindArsServiceDefinitionByPortType(string name) {

            List<tModelInfo> returnList = new List<tModelInfo>();

            try {
                //1. make a FindTModel object with the name to lookup
                FindTModel findTModel = new FindTModel(name);

                //2. create a categorybag with the categories to look for
                CategoryBag catbag = new CategoryBag();

                UddiOrgWsdlTypes uddiOrgWsdlTypes = new UddiOrgWsdlTypes(UddiOrgWsdlTypeCode.portType);

                catbag.AddCategory(new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());
                catbag.AddCategory(uddiOrgWsdlTypes.GetAsKeyedReference());

                findTModel.CategoryBag = catbag.Value;

                //3. call Find method in SDK
                Inquiry inq = new Inquiry();
                tModelList list = inq.Find(findTModel);

                if (list.tModelInfos != null)
                    for (int k = 0; k < list.tModelInfos.Length; k++) {
                        returnList.Add(list.tModelInfos[k]);
                    }
            } catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }

        /// <summary>
        /// Gets all business process definition tmodels. Wildcard "%" can be used.
        /// </summary>
        /// <param name="name">the name to use for lookup</param>
        /// <returns>a list of tmodels</returns>
        public static List<tModelInfo> FindArsProcessDefinition(string name) {
            List<tModelInfo> returnList = new List<tModelInfo>();

            try {
                //1. make a FindTModel object with the name to lookup
                FindTModel findTModel = new FindTModel(name);

                //2. create a categorybag with the categories to look for
                CategoryBag catbag = new CategoryBag();
                KeyedReferenceCollection identifierBag = new KeyedReferenceCollection();

                //KeyedReference keyedref = new BusinessProcessIdentifierType().GetAsKeyedReference();
                KeyedReference keyedref = new BusinessProcessDocument().GetAsKeyedReference();
                keyedref.KeyValue = "%";

                catbag.AddCategory(new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());

                identifierBag.Add(keyedref);
                //catbag.AddCategory(keyedref);

                findTModel.CategoryBag = catbag.Value;
                findTModel.IdentifierBag = identifierBag.GetKeyedReferenceCollection();

                //3. call Find method in SDK
                Inquiry inq = new Inquiry();
                tModelList list = inq.Find(findTModel);

                if (list.tModelInfos!=null)
                    for (int k = 0; k < list.tModelInfos.Length; k++) {
                        returnList.Add(list.tModelInfos[k]);
                    }
            }
            catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }

        /// <summary>
        /// Gets all business process instance tmodels. Wildcard "%" can be used.
        /// </summary>
        /// <param name="name">the name to use for lookup</param>
        /// <returns>a list of tmodels</returns>
        public static List<tModelInfo> FindArsProcessInstance(string name) {

            List<tModelInfo> returnList = new List<tModelInfo>();

            try
            {
                //1. make a FindTModel object with the name to lookup
                FindTModel findTModel = new FindTModel(name);

                //2. create a categorybag with the categories to look for
                CategoryBag catbag = new CategoryBag();

                KeyedReference keyedref = new BusinessProcessDefinitionReference().GetAsKeyedReference();
                keyedref.KeyValue = "%";

                catbag.AddCategory(new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());
                catbag.AddCategory(keyedref);

                findTModel.CategoryBag = catbag.Value;

                //2. call Find method in SDK
                Inquiry inq = new Inquiry();
                tModelList list = inq.Find(findTModel);

                if (list.tModelInfos!=null)
                    for (int k = 0; k < list.tModelInfos.Length; k++)
                    {
                        returnList.Add(list.tModelInfos[k]);
                    }
            }
            catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }

    }
}