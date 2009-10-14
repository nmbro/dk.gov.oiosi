using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.uddi {
    internal class UddiCategoryBag {
        private readonly categoryBag bag;
        private readonly Dictionary<string, keyedReference> keyedReferenceBag;
        private readonly Dictionary<string, keyedReferenceGroup> keyedReferenceGroupBag;

        public UddiCategoryBag(categoryBag bag) {
            this.bag = bag;

            keyedReferenceBag = new Dictionary<string, keyedReference>();
            keyedReferenceGroupBag = new Dictionary<string, keyedReferenceGroup>();

            if (bag.Items == null) return;

            foreach (object category in bag.Items) {
                //if the category is a keyed reference group ignore it.
                if (category is keyedReference) {
                    keyedReference keyRef = (keyedReference)category;
                    keyedReferenceBag[keyRef.tModelKey.ToLower()] = keyRef;
                }
                if (category is keyedReferenceGroup) {
                    keyedReferenceGroup keyRefGroup = (keyedReferenceGroup)category;
                    keyedReferenceGroupBag[keyRefGroup.tModelKey.ToLower()] = keyRefGroup;
                }
            }
        }

        public categoryBag categoryBag {
            get { return bag; }
        }

        public bool TryGetKeyedReference(string tModelKey, out keyedReference keyedRef) {
            if (string.IsNullOrEmpty(tModelKey)) throw new ArgumentException("tModelKey");
            return keyedReferenceBag.TryGetValue(tModelKey.ToLower(), out keyedRef);
        }

        public bool TryGetKeyedReferenceGroup(string tModelKey, out keyedReferenceGroup keyedRefGroup) {
            if (string.IsNullOrEmpty(tModelKey)) throw new ArgumentException("tModelKey");
            return keyedReferenceGroupBag.TryGetValue(tModelKey.ToLower(), out keyedRefGroup);
        }
    }
}
