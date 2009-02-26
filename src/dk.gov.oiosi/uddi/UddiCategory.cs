using System;
using System.Collections.Generic;

namespace dk.gov.oiosi.uddi
{
    // TODO: Could be refactored to use only one method - mandatory is only called by one other method
    internal class UddiCategory
    {
        public static keyedReference GetMandatoryCategoryByIdentifier(categoryBag bag, string categoryIdentifier) {
            return GetCategoryByIdentifier(bag, categoryIdentifier);
        }

        public static keyedReference GetOptionalCategoryByIdentifier(categoryBag bag, string categoryIdentifier) {
            try {
                var category = GetCategoryByIdentifier(bag, categoryIdentifier);
                return category;
            }
            catch(Exception ex) {
                return null;
            }
        }

        private static keyedReference GetCategoryByIdentifier(categoryBag bag, string categoryIdentifier) {
            if (bag == null) throw new ArgumentNullException("bag");
            if (bag.Items == null) throw new Exception("No category with " + categoryIdentifier + " found.");
            
            return GetOptionalCategoryByIdentifier(bag.Items, categoryIdentifier);
        }


        public static keyedReference GetOptionalCategoryByIdentifier(object[] keyedReferences, string categoryIdentifier) {
            List<keyedReference> keyedReferenceList = new List<keyedReference>();
            foreach (object category in keyedReferences) {
                //if the category is a keyed reference group ignore it.
                if (category is keyedReferenceGroup) continue;
                keyedReference keyRef = (keyedReference)category;
                if (keyRef.tModelKey.ToLower() == categoryIdentifier.ToLower()) {
                    keyedReferenceList.Add(keyRef);
                }
            }

            if (keyedReferenceList == null || keyedReferenceList.Count == 0) throw new Exception("No category with " + categoryIdentifier + " found.");
            if (keyedReferenceList.Count > 1) throw new Exception("More than one category found: " + categoryIdentifier);

            return keyedReferenceList[0];
        }
    }
}
