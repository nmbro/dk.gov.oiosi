using System;
using System.Collections.Generic;
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.common {
    /// <summary>
    /// Exception thrown if the creation of an instance fails.
    /// </summary>
    public class CreateInstanceFailedException : UtilityException {
        /// <summary>
        /// Constructor that takes the implementation class with namespace, the 
        /// implementation assembly and the exception as the reason.
        /// </summary>
        /// <param name="implementationNamespaceClass">The implementation class with namespace</param>
        /// <param name="implementationAssembly">The implementation assembly</param>
        /// <param name="innerException">The exception thrown</param>
        public CreateInstanceFailedException(string implementationNamespaceClass, string implementationAssembly, Exception innerException) : base(GetKeyword(implementationNamespaceClass, implementationAssembly), innerException) { }

        private static Dictionary<string, string> GetKeyword(string implementationNamespaceClass, string implementationAssembly) {
            Dictionary<string, string> keywords = KeywordFromString.GetKeyword("implementationnamespaceclass", implementationNamespaceClass);
            KeywordFromString.GetKeyword(keywords, "implementationassembly", implementationAssembly);
            return keywords;
        }
    }
}
