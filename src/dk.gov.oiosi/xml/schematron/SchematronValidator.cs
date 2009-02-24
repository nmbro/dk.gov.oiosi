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
using System.Xml.Serialization;
using System.Xml.Xsl;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.xml.xpath;
using dk.gov.oiosi.xml.xslt;

namespace dk.gov.oiosi.xml.schematron {

    /// <summary>
    /// Validates schematrons
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class SchematronValidator {
        private string _errorXPath;
        private string _errorMessageXPath;
        private XmlDocument _schematronDocument;
        private XsltUtility _xlstUtil;

        /// <summary>
        /// Constructs a new schematron validator
        /// </summary>
        public SchematronValidator() {
            _xlstUtil = new XsltUtility();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SchematronValidator(string errorXPath, string errorMessageXPath) : this() {
            _errorXPath = errorXPath;
            _errorMessageXPath = errorMessageXPath;
        }

        /// <summary>
        /// Constructor that takes the configuration as a parameter
        /// </summary>
        /// <param name="config"></param>
        public SchematronValidator(SchematronValidationConfig config) : this() {
            _errorXPath = config.ErrorXPath;
            _errorMessageXPath = config.ErrorMessageXPath;
            _schematronDocument = config.GetSchematronDocument();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDocument"></param>
        public void SchematronValidateXmlDocument(XmlDocument xmlDocument) {
            if (_schematronDocument == null) throw new Exception("No schematron document is set");
            if (_errorXPath == null) throw new Exception("No error XPath is set");
            if (_errorMessageXPath == null) throw new Exception("No error message XPath is set");
            SchematronValidateXmlDocument(xmlDocument, _schematronDocument);
        }

        /// <summary>
        /// Validates a document
        /// </summary>
        /// <param name="xmlDocument">document to validate</param>
        /// <param name="xmlSchematronStylesheet">stylesheet to use</param>
        public void SchematronValidateXmlDocument(XmlDocument xmlDocument, XmlDocument xmlSchematronStylesheet) {
            if (_errorXPath == null) throw new Exception("No error XPath is set");
            if (_errorMessageXPath == null) throw new Exception("No error message XPath is set");
            XmlDocument result = null;
            PrefixedNamespace[] prefixedNamespaces = new PrefixedNamespace[0];
            bool hasAnyErrors;
            try {
                result = _xlstUtil.TransformXML(xmlDocument, xmlSchematronStylesheet);
                hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(result, _errorXPath, prefixedNamespaces);
            }
            catch (Exception ex) {
                throw new SchematronValidationFailedException(xmlDocument, ex);
            }
            if (hasAnyErrors) {
                string firstErrorMessage = DocumentXPathResolver.GetFirstElementValueByXPath(result, _errorMessageXPath, prefixedNamespaces);
                throw new SchematronErrorException(result, firstErrorMessage);
            }
        }

        /// <summary>
        /// Schematron validates a document.
        /// 
        /// If the validation process fails it throws a SchematronValidationFailedException
        /// If the document has any schematron errors it throws a SchematronErrorException
        /// </summary>
        /// <param name="document">The document to be validated</param>
        /// <param name="schematronStylesheet"></param>
        public void SchematronValidateXmlDocument(XmlDocument document, XslCompiledTransform schematronStylesheet) {
            if (_errorXPath == null) throw new Exception("No error XPath is set");
            if (_errorMessageXPath == null) throw new Exception("No error message XPath is set");
            XmlDocument result = null;
            PrefixedNamespace[] prefixedNamespaces = new PrefixedNamespace[0];
            bool hasAnyErrors;
            try {
                result = _xlstUtil.TransformXml(document, schematronStylesheet);
                hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(result, _errorXPath, prefixedNamespaces);
            }
            catch (Exception ex) {
                throw new SchematronValidationFailedException(document, ex);
            }
            if (hasAnyErrors) {
                string firstErrorMessage = DocumentXPathResolver.GetFirstElementValueByXPath(result, _errorMessageXPath, prefixedNamespaces);
                throw new SchematronErrorException(result, firstErrorMessage);
            }
        }
    }
}