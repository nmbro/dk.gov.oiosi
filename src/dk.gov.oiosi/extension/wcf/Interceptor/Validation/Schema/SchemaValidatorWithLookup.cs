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

namespace dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schema {
 
    /// <summary>
    /// Schema validation with lookup
    /// </summary>
    public class SchemaValidatorWithLookup {
        private DocumentTypeConfigSearcher searcher;

        /// <summary>
        /// Constructor
        /// </summary>
        public SchemaValidatorWithLookup() {
            searcher = new DocumentTypeConfigSearcher();
        }

        /// <summary>
        /// Schema validator
        /// </summary>
        /// <param name="document">document to validate</param>
        public void Validate(XmlDocument document) {
            try {
                if (document == null) throw new SchemaValidationInterceptionEmptyBodyException();
                DocumentTypeConfig documentType = searcher.FindUniqueDocumentType(document);
                XmlSchema schema = LoadSchema(documentType);
                DirectoryInfo localSchemaLocation = GetLocalSchemaLocation(documentType);
                SchemaValidator validator = new SchemaValidator(localSchemaLocation);
                validator.SchemaValidateXmlDocument(document, schema);
            }
            catch (Exception ex) {
                throw new SchemaValidateDocumentFailedException(ex);
            }
        }

        private DirectoryInfo GetLocalSchemaLocation(DocumentTypeConfig documentType) {
            FileInfo file = new FileInfo(documentType.SchemaPath);
            return file.Directory;
        }

        private XmlSchema LoadSchema(DocumentTypeConfig documentType) {
            FileInfo schemaFile = new FileInfo(documentType.SchemaPath);
            FileStream fs = null;
            XmlSchema schema = null;
            try {
                fs = File.OpenRead(schemaFile.FullName);
                schema = XmlSchema.Read(fs, null);
                return schema;
            }
            catch (Exception ex) {
                throw new FailedToLoadSchemaException(schemaFile, ex);
            }
            finally {
                if (fs!= null) fs.Close();
            }
        }
    }
}