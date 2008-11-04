using System;
using System.Collections.Generic;
using dk.gov.oiosi.uddi.TModels;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Factory to create UddiProcessInformation.
    /// </summary>
    public class UddiProcessInformationFactory {
        /// <summary>
        /// Attempts to parse a TModel into a UddiProcessInformation.
        /// 
        /// If the tmodel is of the correct type then the result parameter will be the 
        /// uddi process information and it will return true.
        /// 
        /// If the tmodel is not of the correct type then the result parameter will be null
        /// and it will return false.
        /// 
        /// If one of the expected categories is not present then it will throw an exception
        /// If one of the expected identifiers is not present then it will throw an exception
        /// </summary>
        /// <param name="tmodel">The tmodel to get the values from</param>
        /// <param name="result">The resulting uddi process information</param>
        /// <returns>Whether the tmodel was of the correct type</returns>
        public bool TryParseTModel(TModel tmodel, out UddiProcessInformation result) {
            const string businessProcessDefinitionCategoryName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessDefinitionReference/";
            const string businessProcessRoleTypeCategoryName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Categories/businessProcessRoleIdentifierType/";
            const string businessProcessRoleName = "http://oio.dk/profiles/OIOSI/1.0/UDDI/Identifiers/businessProcessRoleIdentifier/";

            //Find the business process definition
            KeyedReference businessProcessReference = tmodel.CategoryBag.GetCategoryByName(businessProcessDefinitionCategoryName);
            //Stop no need to do anything if the category identifier is unpresent in the tmodel.
            if (businessProcessReference == null) {
                result = null;
                return false;
            }
            string businessProcessDefinitionKeyValue = businessProcessReference.KeyValue;
            UddiId processDefinitionId = new UddiGuidId(businessProcessDefinitionKeyValue, true);
            string name = tmodel.Name.Text;
            string description = tmodel.Descriptions[0].Text;

            //Find the business process role type
            KeyedReference businessProcessRoleType = tmodel.CategoryBag.GetCategoryByName(businessProcessRoleTypeCategoryName);
            if (businessProcessRoleType == null) throw new CategoryMissingException(businessProcessRoleTypeCategoryName);
            string roleType = businessProcessRoleType.KeyValue;
            //find the business process role
            Predicate<KeyedReference> findIdentifers = delegate(KeyedReference reference) { return reference.KeyName == businessProcessRoleName; };
            List<KeyedReference> identifiers = tmodel.IdentifierBag.GetInnerCollectionAsList();
            KeyedReference businessProcessRole = identifiers.Find(findIdentifers);
            if (businessProcessRole == null) throw new IdentifierMissingException(businessProcessRoleName);
            string role = businessProcessRole.KeyValue;

            result = new UddiProcessInformation(name, description, role, roleType, processDefinitionId);
            return true;
        }
    }
}
