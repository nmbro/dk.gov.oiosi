/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */

using System;
using System.Xml;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.xml.schematron {
    /// <summary>
    /// Consiguration to the schematron validation.
    /// </summary>
    public class SchematronValidationConfig {
        private string _schematronDocumentPath = "";
        private string _errorXPath = "";
        private string _errorMessageXPath = "";
        private XmlDocument _schematronDocument;
        private object _lockGetSchematronDocument = new object();

        /// <summary>
        /// Default constructor, should not be used.
        /// </summary>
        public SchematronValidationConfig() { }

        /// <summary>
        /// Constructor that takes the path to the schematron document, the error xpath 
        /// and the error message xpath.
        /// </summary>
        /// <param name="schematronDocumentPath">The schematron document path.</param>
        /// <param name="errorXPath">The error xpath</param>
        /// <param name="errorMessageXPath">The error message xpath</param>
        public SchematronValidationConfig(string schematronDocumentPath, string errorXPath, string errorMessageXPath) {
            if (schematronDocumentPath == null) throw new NullArgumentException("schematronDocumentPath");
            if (errorXPath == null) throw new NullArgumentException("errorXPath");
            if (errorMessageXPath == null) throw new NullArgumentException("errorMessageXPath");
            _schematronDocumentPath = schematronDocumentPath;
            _errorXPath = errorXPath;
            _errorMessageXPath = errorMessageXPath;
        }

        /// <summary>
        /// Gets and sets the schematron document path.
        /// </summary>
        public string SchematronDocumentPath {
            get { return _schematronDocumentPath; }
            set {
                if (value == null) throw new NullArgumentException("SchematronDocumentPath.value");
                _schematronDocumentPath = value; 
            }
        }

        /// <summary>
        /// Gets and sets the xpath expression that gets the error results from 
        /// the schematron result.
        /// </summary>
        public string ErrorXPath {
            get { return _errorXPath; }
            set {
                if (value == null) throw new NullArgumentException("ErrorXPath.value");
                _errorXPath = value; 
            }
        }

        /// <summary>
        /// Gets and sets the xpath expression that gets the error messages from
        /// the schematron result.
        /// </summary>
        public string ErrorMessageXPath {
            get { return _errorMessageXPath; }
            set {
                if (value == null) throw new NullArgumentException("ErrorMessageXPath.value");
                _errorMessageXPath = value; 
            }
        }

        /// <summary>
        /// Gets the schematron document.
        /// The first time this method is called then the xml document will be 
        /// loaded from disc into memory.
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetSchematronDocument() {
            lock (_lockGetSchematronDocument) {
                if (_schematronDocument == null)
                    LoadSchematronDocument();
                return _schematronDocument;
            }
        }

        private void LoadSchematronDocument() {
            try {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(_schematronDocumentPath);
                _schematronDocument = xmlDocument;
            }
            catch (Exception ex) {
                throw new Exception("Failed to load schematron document", ex);
            }
        }
    }
}
