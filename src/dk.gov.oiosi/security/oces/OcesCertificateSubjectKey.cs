using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.security.oces {
    /// <summary>
    /// Implements the OCES certificate subject key. Ensures that the text that is 
    /// used to detect certificate strings.
    /// </summary>
    public class OcesCertificateSubjectKey {
        private string _subjectKeyString;

        /// <summary>
        /// Default constructor
        /// </summary>
        public OcesCertificateSubjectKey() {
            _subjectKeyString = @"NOID";
        }

        /// <summary>
        /// Constructor that takes the subject key string as parameter.
        /// </summary>
        /// <param name="subjectKeyString"></param>
        public OcesCertificateSubjectKey(string subjectKeyString) {
            CheckSubjectKeyString(subjectKeyString);
            _subjectKeyString = subjectKeyString;
        }

        /// <summary>
        /// Gets and sets the subject key string.
        /// </summary>
        public string SubjectKeyString {
            get { return _subjectKeyString; }
            set {
                CheckSubjectKeyString(value);
                _subjectKeyString = value;
            }
        }

        private void CheckSubjectKeyString(string subjectKeyString) {
            if (string.IsNullOrEmpty(subjectKeyString)) throw new NullOrEmptyArgumentException("keyString");
            if (Regex.IsMatch(subjectKeyString, @"(\s)+"))
                throw new Exception("Invalid subject key string.");
        }
    }
}
