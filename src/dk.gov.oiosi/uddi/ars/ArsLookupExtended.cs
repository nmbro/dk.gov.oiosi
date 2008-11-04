using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.uddi.identifier;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// Extends the functionality of ArsLookup so it returns Ars specific classes instead
    /// of tmodels.
    /// </summary>
    public class ArsLookupExtended {

        /// <summary>
        /// Gets all business process definition tmodels. Wildcard "%" can be used.
        /// </summary>
        /// <param name="name">the name to use for lookup</param>
        /// <returns>a list of tmodels</returns>
        public static List<ArsBusinessProcessDefinition> FindArsProcessDefinition(string name) {
            List<ArsBusinessProcessDefinition> returnList = new List<ArsBusinessProcessDefinition>();

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

                findTModel.CategoryBag = catbag.Value;
                findTModel.IdentifierBag = identifierBag.GetKeyedReferenceCollection();

                IEnumerable<TModel> tmodels = FindAndGetDetails(findTModel);
                foreach (TModel tmodel in tmodels) {
                    ArsBusinessProcessDefinition processDefinition = new ArsBusinessProcessDefinition(tmodel);
                    returnList.Add(processDefinition);
                }
            }
            catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }

        /// <summary>
        /// Gets all business process instance tmodels that are role defintions.
        /// Wildcard "%" can be used.
        /// </summary>
        /// <returns></returns>
        public static List<ArsProcessInstance> FindArsProcessInstancesAsRoleDefinition(string name) {
            List<ArsProcessInstance> returnList = new List<ArsProcessInstance>();
            try {
                //make a FindTModel object with the name to lookup
                FindTModel findTModel = new FindTModel(name);

                //create a categorybag with the categories to look for
                CategoryBag catbag = new CategoryBag();

                KeyedReference keyedref = new BusinessProcessDefinitionReference().GetAsKeyedReference();
                keyedref.KeyValue = "%";
                KeyedReference roleReference = new KeyedReference("http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessRoleDefinition/", "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessRoleDefinition/", "uddi:bc3151a0-1144-11dd-a56f-32872391a563");

                catbag.AddCategory(new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());
                catbag.AddCategory(keyedref);
                catbag.AddCategory(roleReference);

                findTModel.CategoryBag = catbag.Value;

                IEnumerable<TModel> tmodels = FindAndGetDetails(findTModel);
                foreach (TModel tmodel in tmodels) {
                    ArsProcessInstance processInstance = new ArsProcessInstance(tmodel);
                    returnList.Add(processInstance);
                }
            }
            catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }

        /// <summary>
        /// Gets all business process instance tmodels that are role defintions and has
        /// a reference to the given process definition.
        /// </summary>
        /// <param name="processDefinition">The process definition the process instances must refer to.</param>
        /// <returns></returns>
        public static List<ArsProcessInstance> FindArsProcessInstancesAsRoleDefinition(ArsBusinessProcessDefinition processDefinition) {
            List<ArsProcessInstance> returnList = new List<ArsProcessInstance>();
            try {
                //make a FindTModel object with the name to lookup
                FindTModel findTModel = new FindTModel();

                //create a categorybag with the categories to look for
                CategoryBag catbag = new CategoryBag();

                KeyedReference keyedref = new BusinessProcessDefinitionReference().GetAsKeyedReference();
                keyedref.KeyValue = processDefinition.ID.ID;
                KeyedReference roleReference = new KeyedReference("http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessRoleDefinition/", "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessRoleDefinition/", "uddi:bc3151a0-1144-11dd-a56f-32872391a563");

                catbag.AddCategory(new RegistrationConformanceClaim(RegistrationConformanceClaimCode.oiosi1_1).GetAsKeyedReference());
                catbag.AddCategory(keyedref);
                catbag.AddCategory(roleReference);

                findTModel.CategoryBag = catbag.Value;

                IEnumerable<TModel> tmodels = FindAndGetDetails(findTModel);
                foreach (TModel tmodel in tmodels) {
                    ArsProcessInstance processInstance = new ArsProcessInstance(tmodel);
                    returnList.Add(processInstance);
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
        /// <param name="transportcode">the transport parameter used in the lookup</param>
        /// <returns>a list of tmodels</returns>
        public static List<ArsBindingType> FindArsServiceDefinition(string name, UddiOrgWsdlCategorizationTransportCode transportcode) {

            List<ArsBindingType> returnList = new List<ArsBindingType>();

            try {
                //Make a FindTModel object with the name to lookup
                FindTModel findTModel = new FindTModel(name);

                //Create a categorybag with the categories to look for
                CategoryBag catbag = new CategoryBag();

                KeyedReference uddiOrgXmlNS = new UddiOrgXmlNamespace().GetAsKeyedReference();
                uddiOrgXmlNS.KeyValue = "%";

                UddiOrgWsdlTypes uddiOrgWsdlTypes = new UddiOrgWsdlTypes(UddiOrgWsdlTypeCode.binding);

                KeyedReference uddiOrgWsdlPortType = new UddiOrgWsdlPortTypeReference().GetAsKeyedReference();
                uddiOrgWsdlPortType.KeyValue = "%";

                KeyedReference uddiOrgWsdlCategorizationProtocol = new UddiOrgWsdlCategorizationProtocol().GetAsKeyedReference();
                uddiOrgWsdlCategorizationProtocol.KeyValue = "%";

                KeyedReference uddiOrgWsdlCategorizationTransport = null;
                switch (transportcode) {
                    case UddiOrgWsdlCategorizationTransportCode.http:
                        uddiOrgWsdlCategorizationTransport = new UddiOrgWsdlCategorizationTransport(UddiOrgWsdlCategorizationTransportCode.http).GetAsKeyedReference();
                        break;
                    case UddiOrgWsdlCategorizationTransportCode.smtp:
                        uddiOrgWsdlCategorizationTransport = new UddiOrgWsdlCategorizationTransport(UddiOrgWsdlCategorizationTransportCode.smtp).GetAsKeyedReference();
                        break;
                    default:
                        throw new Exception("Transport code " + transportcode + " is not supported");
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
                IEnumerable<TModel> tmodels = FindAndGetDetails(findTModel);
                
                foreach (TModel tmodel in tmodels) {
                    ArsBindingType bindingType = new ArsBindingType(tmodel);
                    returnList.Add(bindingType);
                }
            }
            catch (Exception exp) {
                throw new ArsLookupUnexpectedException(exp);
            }
            return returnList;
        }


        private static IEnumerable<TModel> FindAndGetDetails(FindTModel findTModel) {
            List<string> tmodelKeys = new List<string>();
            //call Find method in SDK
            Inquiry inq = new Inquiry();
            tModelList list = inq.Find(findTModel);
            if (list.tModelInfos == null || list.tModelInfos.Length < 1) return new List<TModel>();;
            foreach (tModelInfo info in list.tModelInfos) {
                tmodelKeys.Add(info.tModelKey);
            }
            //call get nethod to get the details
            GetTModelDetail getTModelDetail = new GetTModelDetail(tmodelKeys.ToArray());
            TModel[] tmodels = inq.GetDetail(getTModelDetail.Value);
            if (tmodels == null || tmodels.Length < 1) return new List<TModel>();
            return tmodels;
        }
    }
}
