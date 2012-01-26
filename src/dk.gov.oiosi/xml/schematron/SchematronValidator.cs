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
using System.Xml.Serialization;
using System.Xml.Xsl;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.xml.xpath;
using dk.gov.oiosi.xml.xslt;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.xml.schematron {

    /// <summary>
    /// Validates schematrons
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class SchematronValidator
    {
        private string errorXPath;
        private string errorMessageXPath;
        private XmlDocument schematronDocument;
        private XsltUtility xlstUtil;

        private ILogger logger;

        /// <summary>
        /// Constructs a new schematron validator
        /// </summary>
        public SchematronValidator() 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.xlstUtil = new XsltUtility();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SchematronValidator(string errorXPath, string errorMessageXPath) 
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.xlstUtil = new XsltUtility();
            this.errorXPath = errorXPath;
            this.errorMessageXPath = errorMessageXPath;
        }

        /// <summary>
        /// Constructor that takes the configuration as a parameter
        /// </summary>
        /// <param name="config"></param>
        public SchematronValidator(SchematronValidationConfig config)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.xlstUtil = new XsltUtility();
            this.errorXPath = config.ErrorXPath;
            this.errorMessageXPath = config.ErrorMessageXPath;
            this.schematronDocument = config.GetSchematronDocument();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDocument"></param>
        public void SchematronValidateXmlDocument(XmlDocument xmlDocument)
        {
            if (this.schematronDocument == null)
            {
                throw new Exception("No schematron document is set");
            }
            if (this.errorXPath == null)
            {
                throw new Exception("No error XPath is set");
            }
            if (this.errorMessageXPath == null)
            {
                throw new Exception("No error message XPath is set");
            }

            this.SchematronValidateXmlDocument(xmlDocument, this.schematronDocument);
        }

        /// <summary>
        /// Validates a document
        /// </summary>
        /// <param name="xmlDocument">document to validate</param>
        /// <param name="xmlSchematronStylesheet">stylesheet to use</param>
        public void SchematronValidateXmlDocument(XmlDocument xmlDocument, XmlDocument xmlSchematronStylesheet) 
        {
            if (this.errorXPath == null)
            {
                throw new Exception("No error XPath is set");
            }
            if (this.errorMessageXPath == null)
            {
                throw new Exception("No error message XPath is set");
            }

            XmlDocument result = null;
            PrefixedNamespace[] prefixedNamespaces = new PrefixedNamespace[0];
            bool hasAnyErrors;
            try 
            {
                result = xlstUtil.TransformXml(xmlDocument, xmlSchematronStylesheet);
                hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(result, this.errorXPath, prefixedNamespaces);
            }
            catch (Exception ex) 
            {
                throw new SchematronValidationFailedException(xmlDocument, ex);
            }

            if (hasAnyErrors)
            {
                string firstErrorMessage = DocumentXPathResolver.GetFirstElementValueByXPath(result, this.errorMessageXPath, prefixedNamespaces);
                throw new SchematronErrorException(result, firstErrorMessage);
            }
            else
            {
                // no schematron error
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
        public void SchematronValidateXmlDocument(XmlDocument document, XslCompiledTransform schematronStylesheet)
        {
            if (this.errorXPath == null)
            {
                throw new Exception("No error XPath is set");
            }
            if (this.errorMessageXPath == null)
            {
                throw new Exception("No error message XPath is set");
            }

            XmlDocument result = null;
            PrefixedNamespace[] prefixedNamespaces = new PrefixedNamespace[0];
            bool hasAnyErrors;
            try
            {
                result = xlstUtil.TransformXml(document, schematronStylesheet);
                hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(result, this.errorXPath, prefixedNamespaces);
            }
            catch (Exception ex)
            {
                throw new SchematronValidationFailedException(document, ex);
            }

            if (hasAnyErrors)
            {
                string firstErrorMessage = DocumentXPathResolver.GetFirstElementValueByXPath(result, this.errorMessageXPath, prefixedNamespaces);
                throw new SchematronErrorException(result, firstErrorMessage);
            }
            else
            {
                // no schematron error
            }
        }
    }
}