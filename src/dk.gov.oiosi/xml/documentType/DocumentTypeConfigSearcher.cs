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
using System.Collections.Generic;
using System.Xml;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.xml.documentType {
    /// <summary>
    /// Standard way to search for document types.
    /// </summary>
    public class DocumentTypeConfigSearcher {
        private static DocumentTypeCollectionConfig _documentTypeConfig;
        private static object _documentTypeCollectionLock = new object();

        /// <summary>
        /// Default constructor
        /// </summary>
        public DocumentTypeConfigSearcher() {
            lock (_documentTypeCollectionLock) {
                if (_documentTypeConfig == null) {
                    _documentTypeConfig = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
                }
            }
        }

        /// <summary>
        /// Searches the configuration handler for a document type that have the same name
        /// and namespace as the xml document given with the parameter.
        /// </summary>
        /// <param name="document">The document to examine</param>
        /// <returns>Returns a matching DocumentTypeConfig, or throws an exception if no match was found</returns>
        public DocumentTypeConfig FindUniqueDocumentType(XmlDocument document) {
            if (document == null) throw new NullArgumentException("document");
            try {
                DocumentTypeConfig documentType;
                if (!TryFindUniqueDocumentType(document, out documentType))
                    throw new NoDocumentTypeFoundFromXmlDocumentException(document);
                return documentType;
            }
            catch (NoDocumentTypeFoundFromXmlDocumentException ex) {
                throw ex;
            }
            catch (Exception ex) {
                throw new SearchForDocumentTypeFromXmlDocumentFailedException(document, ex);
            }
        }

        /// <summary>
        /// Tries to find the xml document type from the xml document.
        /// If none can be found from the id it return false.
        /// If one is found it returns true.
        /// If more than one is found it throwns an exception.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public bool TryFindUniqueDocumentType(XmlDocument document, out DocumentTypeConfig documentType) {
            if (document == null) throw new NullArgumentException("document");
            documentType = null;
            Predicate<DocumentTypeConfig> isDocumentType =
                delegate(DocumentTypeConfig currentDocumentType) {
                    return currentDocumentType.IsDocumentOfType(document);
                };
            List<DocumentTypeConfig> allDocumentTypes = new List<DocumentTypeConfig>(_documentTypeConfig.DocumentTypes);
            List<DocumentTypeConfig> currentDocumentTypes = allDocumentTypes.FindAll(isDocumentType);
            if (currentDocumentTypes.Count > 1) throw new AmbiguousDocumentTypeFoundFromXmlDocumentException(document);
            if (currentDocumentTypes.Count < 1) return false;
            documentType = currentDocumentTypes[0];
            return true;
        }
    }
}