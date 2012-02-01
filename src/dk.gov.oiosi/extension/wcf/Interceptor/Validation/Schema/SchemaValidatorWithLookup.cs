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
using System.IO;
using System.Xml;
using System.Xml.Schema;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.schema;
using dk.gov.oiosi.logging;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schema {
 
    /// <summary>
    /// Schema validation with lookup
    /// </summary>
    public class SchemaValidatorWithLookup 
    {
        /// <summary>
        /// The Document identifyer
        /// </summary>
        private DocumentTypeConfigSearcher searcher;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public SchemaValidatorWithLookup() 
        {
            this.searcher = new DocumentTypeConfigSearcher();
            this.logger = LoggerFactory.Create(this.GetType());
        }

        /// <summary>
        /// Schema validator
        /// </summary>
        /// <param name="document">document to validate</param>
        public void Validate(XmlDocument document) 
        {
            this.logger.Trace("Schema validate xml document.");
            try
            {
                if (document == null)
                {
                    throw new SchemaValidationInterceptionEmptyBodyException();
                }

                DocumentTypeConfig documentType = searcher.FindUniqueDocumentType(document);
                SchemaStore schemaStore = new SchemaStore();
                XmlSchemaSet XmlSchemaSet = schemaStore.GetCompiledXmlSchemaSet(documentType);
                SchemaValidator schemaValidator = new SchemaValidator();
                ValidationEventHandler validationEventHandler = new ValidationEventHandler(ValidationCallBack);

                schemaValidator.SchemaValidateXmlDocument(document, XmlSchemaSet, validationEventHandler);
                
            }
            catch (Exception ex)
            {
                this.logger.Debug("Schema validate xml document.", ex);
                throw new SchemaValidateDocumentFailedException(ex);
            }

            this.logger.Trace("Schema validate xml document - Finish.");
        }

        /// <summary>
        /// Handle the callback schema error and warnings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
            {
                this.logger.Warn("Matching schema not found. No schema validation occurred");
            }
            else
            {
                this.logger.Info("Rejected a Schema invalid document.");
                throw new SchemaValidateDocumentFailedException(args.Exception);
            }
        }
    }
}