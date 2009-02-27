using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.uddi {
    public enum FindQualifers {
        andAllKeys,
        approximateMatch,
        caseInsensitiveSort,
        caseInsensitiveMatch,
        caseSensitiveSort,
        caseSensitiveMatch,
        exactMatch,
        orAllKeys,
        orLikeKeys,
        sortByNameAsc,
        sortByNameDesc,
        sortByDateAsc,
        sortByDateDesc,
    }
}
