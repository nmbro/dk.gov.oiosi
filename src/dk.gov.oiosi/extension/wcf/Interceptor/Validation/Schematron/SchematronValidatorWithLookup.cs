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
  *   Jacob Mogensen, mySupply ApS
  *   Jens Madsen, Comcare
  */

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Xsl;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.logging;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.schematron;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schematron
{
    /// <summary>
    /// Schematron validator with lookup
    /// </summary>
    public class SchematronValidatorWithLookup
    {
        private DocumentTypeConfigSearcher searcher;
        private ILogger logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public SchematronValidatorWithLookup()
        {
            this.searcher = new DocumentTypeConfigSearcher();
            this.logger = LoggerFactory.Create(this.GetType());
        }

        /// <summary>
        /// Validates a document
        /// </summary>
        /// <param name="document">the document to validate</param>
        [Obsolete("It is much faster to use Validate(string documentAsString)", false)]
        public void Validate(XmlDocument document)
        {
            try
            {
                this.logger.Trace("SchematronValidation");
                if (document == null)
                {
                    throw new SchematronValidationInterceptionEmptyBodyException();
                }

                DocumentTypeConfig documentType = searcher.FindUniqueDocumentType(document);
                SchematronStore store = new SchematronStore();
                SchematronValidationConfig[] schematronValidationConfigCollection = documentType.SchematronValidationConfigs;
                foreach (SchematronValidationConfig schematronValidationConfig in schematronValidationConfigCollection)
                {
                    CompiledXslt compiledXsltEntry = store.GetCompiledSchematron(schematronValidationConfig.SchematronDocumentPath);
                    SchematronValidator validator = new SchematronValidator(schematronValidationConfig.ErrorXPath, schematronValidationConfig.ErrorMessageXPath);
                    validator.SchematronValidateXmlDocument(document, compiledXsltEntry);
                }
            }
            catch (SchematronErrorException ex)
            {
                this.logger.Info("XmlDocument rejected, as it contant at least one schematron error.");
                throw new SchematronValidateDocumentFailedException(ex);
            }
            catch (Exception ex)
            {
                this.logger.Error("Schematron validation failed", ex);
                throw new SchematronValidateDocumentFailedException(ex);
            }
        }

        /// <summary>
        /// Validates a document
        /// </summary>
        /// <param name="documentAsString">the document to validate</param>
        public void Validate(string documentAsString)
        {
            try
            {
                this.logger.Trace("SchematronValidation");
                if (documentAsString == null)
                {
                    throw new SchematronValidationInterceptionEmptyBodyException();
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(documentAsString);
                DocumentTypeConfig documentType = searcher.FindUniqueDocumentType(xmlDoc);

                SchematronValidationConfig[] schematronValidationConfigCollection = documentType.SchematronValidationConfigs;
                foreach (SchematronValidationConfig schematronValidationConfig in schematronValidationConfigCollection)
                {
                    SchematronStore store = new SchematronStore();
                    CompiledXslt compiledXsltEntry = store.GetCompiledSchematron(schematronValidationConfig.SchematronDocumentPath);
                    SchematronValidator validator = new SchematronValidator(schematronValidationConfig.ErrorXPath, schematronValidationConfig.ErrorMessageXPath);

                    validator.SchematronValidateXmlDocument(documentAsString, compiledXsltEntry);
                }
            }
            catch (SchematronErrorException ex)
            {
                this.logger.Info("XmlDocument rejected, as it contant at least one schematron error.");
                throw new SchematronValidateDocumentFailedException(ex);
            }
            catch (Exception ex)
            {
                this.logger.Error("Schematron validation failed", ex);
                throw new SchematronValidateDocumentFailedException(ex);
            }
        }
    }
}