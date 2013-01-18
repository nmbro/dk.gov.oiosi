using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron {
    /// <summary>
    /// Exception thrown when the load of the schematron stylesheet fails.
    /// </summary>
    public class FailedToLoadSchematronStylesheetException : InterceptorException {
        /// <summary>
        /// Constructor that takes the fileinfo of the schematron stylesheet file and
        /// the exception caught.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="innerException"></param>
        public FailedToLoadSchematronStylesheetException(FileInfo fileInfo, Exception innerException) : base(KeywordsFromFileInfo.GetKeywords(fileInfo), innerException) { }
    }
}
