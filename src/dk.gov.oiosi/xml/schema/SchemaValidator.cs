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
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace dk.gov.oiosi.xml.schema {

    /// <summary>
    /// Represents a validator that can validate whether an xml document is valid for a
    /// given xml schema.
    /// </summary>
    public class SchemaValidator {
        private UrlToLocalFilelResolver urlResolver;

        /// <summary>
        /// Constructor that takes the directory where the schemas are located.
        /// </summary>
        /// <param name="localSchemaLocation"></param>
        public SchemaValidator(DirectoryInfo localSchemaLocation) {
            urlResolver = new UrlToLocalFilelResolver(localSchemaLocation);
        }

        /// <summary>
        /// Validates wheter the xml document is valid to the given xml schema. Further 
        /// it ensures that the schema and the document has the same namespace. If 
        /// either the document is invalid or the namepsaces are incorrect and an 
        /// exception is thrown.
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="xmlSchema"></param>
        public void SchemaValidateXmlDocument(XmlDocument xmlDocument, XmlSchema xmlSchema) {
            try {
                CheckNamespace(xmlDocument, xmlSchema);
                // Note: This code uses an obsolete method and should be updated.
                xmlSchema.Compile(null, urlResolver);
                XmlDocument clonedDocument = (XmlDocument)xmlDocument.Clone();
                clonedDocument.Schemas.Add(xmlSchema);
                clonedDocument.Validate(null);
            }
            catch (Exception ex) {
                throw new SchemaValidationFailedException(xmlDocument, ex);
            }
        }

        /// <summary>
        /// Validates wheter the xml document is valid to the given xml schema. Further 
        /// it ensures that the schema and the document has the same namespace. If 
        /// the document is invalid the validation event handler is called, however 
        /// if the namepsaces are incorrect and an exception is thrown.
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="xmlSchema"></param>
        /// <param name="validationEventHandler"></param>
        public void SchemaValidateXmlDocument(XmlDocument xmlDocument, XmlSchema xmlSchema, ValidationEventHandler validationEventHandler) {
            CheckNamespace(xmlDocument, xmlSchema);
            // Note: This code uses an obsolete method and should be updated.
            xmlSchema.Compile(validationEventHandler, urlResolver);
            XmlDocument clonedDocument = (XmlDocument)xmlDocument.Clone();
            clonedDocument.Schemas.Add(xmlSchema);
            clonedDocument.Validate(validationEventHandler);
        }

        /// <summary>
        /// Controls that the schema and the document has the same namespace.
        /// </summary>
        /// <exception cref="UnexpectedNamespaceException"></exception>
        /// <param name="xmlDocument"></param>
        /// <param name="xmlSchema"></param>
        private void CheckNamespace(XmlDocument xmlDocument, XmlSchema xmlSchema) {
            string documentNamespace = xmlDocument.DocumentElement.NamespaceURI;
            string expectedNamespace = xmlSchema.TargetNamespace;
            if (documentNamespace != expectedNamespace) throw new UnexpectedNamespaceException(documentNamespace, expectedNamespace);
        }
    }
}