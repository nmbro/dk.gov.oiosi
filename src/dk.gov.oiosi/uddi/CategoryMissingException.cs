using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// Exception thrown if a category with a given TModelKey was expected but not found.
    /// </summary>
    public class CategoryMissingException : UddiException {
        /// <summary>
        /// Constructor that takes the categoryTModelKey that was expected to yield results.
        /// </summary>
        /// <param name="categoryTModelKey">The string value of the categorys TModel key</param>
        public CategoryMissingException(string categoryTModelKey) : base(KeywordFromString.GetKeyword("categorytmodelkey", categoryTModelKey)) { }
    }
}
