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
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System;
using System.Xml;
using System.Xml.Xsl;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.schematron;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron {

    /// <summary>
    /// Schematron validator with lookup
    /// </summary>
    public class SchematronValidatorWithLookup {
        private DocumentTypeConfigSearcher _searcher;
        private SchematronValidator _validator;
        private DocumentTypeConfig _documentType;
        private XslCompiledTransform _compiledStylesheet;

        /// <summary>
        /// Constructor
        /// </summary>
        public SchematronValidatorWithLookup() {
            _searcher = new DocumentTypeConfigSearcher();
        }

        /// <summary>
        /// Validates a document
        /// </summary>
        /// <param name="document">the document to validate</param>
        public void Validate(XmlDocument document) {
            try {
                if (document == null) throw new SchematronValidationInterceptionEmptyBodyException();
                DocumentTypeConfig documentType = _searcher.FindUniqueDocumentType(document);
                if (_documentType == null || !documentType.Equals(_documentType)) {
                    _documentType = documentType;
                    SchematronValidationConfig schematronValidationConfig = documentType.SchematronValidationConfig;
                    SchematronStore store = SchematronStoreFactory.GetSchematronStore();
                    _compiledStylesheet = store.GetCompiledSchematron(schematronValidationConfig.SchematronDocumentPath);
                    _validator = new SchematronValidator(schematronValidationConfig.ErrorXPath, schematronValidationConfig.ErrorMessageXPath);
                }
                _validator.SchematronValidateXmlDocument(document, _compiledStylesheet);
            }
            catch (Exception ex) {
                throw new SchematronValidateDocumentFailedException(ex);
            }
        }
    }
}