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
using dk.gov.oiosi.logging;
using System.Text;
using dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schema;

namespace dk.gov.oiosi.xml.schema {

    /// <summary>
    /// Represents a validator that can validate whether an xml document is valid for a
    /// given xml schema.
    /// </summary>
    public class SchemaValidator
    {
        private ILogger logger; 
        //private UrlToLocalFilelResolver urlResolver;

        /// <summary>
        /// Constructor that takes the directory where the schemas are located.
        /// </summary>
        /// <param name="localSchemaLocation"></param>
        public SchemaValidator()
        {
            this.logger = LoggerFactory.Create(this.GetType());    
        }

        /// <summary>
        /// Validates wheter the xml document is valid to the given xml schema. Further 
        /// it ensures that the schema and the document has the same namespace. If 
        /// the document is invalid an exception is thrown.
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="xmlSchemaSet"></param>
        public void SchemaValidateXmlDocument(XmlDocument xmlDocument, XmlSchemaSet xmlSchemaSet) 
        {
            this.SchemaValidateXmlDocument(xmlDocument, xmlSchemaSet, null);
        }

        /// <summary>
        /// Validates wheter the xml document is valid to the given xml schema. Further 
        /// it ensures that the schema and the document has the same namespace. If 
        /// the document is invalid the validation event handler is called, if no event 
        /// handler is provided, and exception is thrown
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="xmlSchemaSet"></param>
        /// <param name="validationEventHandler"></param>
        public void SchemaValidateXmlDocument(XmlDocument xmlDocument, XmlSchemaSet xmlSchemaSet, ValidationEventHandler validationEventHandler)
        {
            try
            {
                // http://msdn.microsoft.com/en-us/library/system.xml.schema.validationeventargs.severity.aspx
                using (Stream stream = new MemoryStream())
                {
                    XmlWriterSettings xmlDocumentWriterSettings = new XmlWriterSettings();
                    xmlDocumentWriterSettings.Encoding = Encoding.UTF8;
                    xmlDocumentWriterSettings.Indent = true;
                    xmlDocumentWriterSettings.IndentChars = " ";
                    xmlDocumentWriterSettings.NewLineOnAttributes = true;
                    xmlDocumentWriterSettings.OmitXmlDeclaration = false;
                    xmlDocumentWriterSettings.CloseOutput = false;

                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlDocumentWriterSettings))
                    {
                        // write document to memory stream 
                        // Note - this line is expensive in processing time
                        xmlDocument.WriteTo(xmlWriter);
                    }

                    // document is now in a memory stream
                    // set stream index to 0.
                    stream.Seek(0, SeekOrigin.Begin);

                    XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
                    xmlReaderSettings.Schemas.Add(xmlSchemaSet);
                    xmlReaderSettings.ValidationEventHandler += validationEventHandler;
                    xmlReaderSettings.ValidationType = ValidationType.Schema;

                    //Create the schema validating reader.
                    using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
                    {
                        while (xmlReader.Read()) { }
                    }
                }
            }
            catch (SchemaValidateDocumentFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SchemaValidationFailedException(xmlDocument, ex);
            }
        }

        /// <summary>
        /// Validates wheter the xml document is valid to the given xml schema. Further 
        /// it ensures that the schema and the document has the same namespace. If 
        /// the document is invalid the validation event handler is called, if no event 
        /// handler is provided, and exception is thrown
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="xmlSchemaSet"></param>
        /// <param name="validationEventHandler"></param>
        private void SchemaValidateXmlDocument2(XmlDocument xmlDocument, XmlSchemaSet xmlSchemaSet, ValidationEventHandler validationEventHandler)
        {
            //// just as fast as SchemaValidateXmlDocument - can be improved?
            try
            {
               // why is the document cloned
                XmlDocument validateDocument = null;

                if (validationEventHandler == null)
                {
                    validateDocument = xmlDocument;
                }
                else
                {
                    validateDocument = (XmlDocument)xmlDocument.Clone();
                }

                validateDocument.Schemas.Add(xmlSchemaSet);
                validateDocument.Validate(validationEventHandler);
            }
            catch (Exception ex)
            {
                throw new SchemaValidationFailedException(xmlDocument, ex);
            }
        }
    }
}