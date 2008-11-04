using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// Exception thrown if from a given indentifierTModelKey there was no indentifiers.
    /// </summary>
    public class IdentifierMissingException : UddiException {
        /// <summary>
        /// Takes the string value of the indentifier TModel key as parameter
        /// </summary>
        /// <param name="identifierTModelKey">The string value of the indentifier TModel key</param>
        public IdentifierMissingException(string identifierTModelKey) : base(KeywordFromString.GetKeyword("identifiertmodelkey", identifierTModelKey)) { }
    }
}
