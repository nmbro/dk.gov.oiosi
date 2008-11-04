using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;

using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.xml.documentType {

    /// <summary>
    /// Search for document with specific ID failed exception
    /// </summary>
    public class SearchForDocumentTypeFromIdFailedException : SearchForDocumentTypeFailedException {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The ID of the document</param>
        /// <param name="innerException">The inner exception</param>
        public SearchForDocumentTypeFromIdFailedException(Guid id, Exception innerException) : base(KeywordFromGuid.GetKeyword(id), innerException) { } 
    }
}
