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
using System.Text;
using System.Xml.Serialization;

using dk.gov.oiosi.exception;
using dk.gov.oiosi.xml.xpath.discriminator;

namespace dk.gov.oiosi.communication.configuration {

    /// <summary>
    /// A collection of RaspDocumentTypeConfig
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = dk.gov.oiosi.configuration.RaspConfigurationHandler.RaspNamespaceUrl)]
    public class RaspDocumentTypeCollectionConfig {
        private List<RaspDocumentTypeConfig> _documentTypes = new List<RaspDocumentTypeConfig>();

        /// <summary>
        /// A list of RASP document types supported by the client (e.g. Invoices, Notifications...)
        /// </summary>
        [XmlArray("DocumentTypes")]
        public RaspDocumentTypeConfig[] DocumentTypes { 
            get { return _documentTypes.ToArray(); } 
            set { _documentTypes = new List<RaspDocumentTypeConfig>(value); } 
        }

        /// <summary>
        /// Adds a new RASP document type to the configuration
        /// </summary>
        /// <param name="documentType">documenttype to add</param>
        public void AddDocumentType(RaspDocumentTypeConfig documentType) {
            if (documentType == null)
                throw new NullArgumentException("documentType");
            if (ContainsDocumentTypeByValue(documentType))
                throw new RaspDocumentAllreadyAddedException(documentType.FriendlyName);
            _documentTypes.Add(documentType);
        }

        /// <summary>
        /// Removes a specific document type from the configuration
        /// </summary>
        /// <param name="documentType"></param>
        public void RemoveDocumentType(RaspDocumentTypeConfig documentType) {
            if (documentType == null)
                throw new NullArgumentException("documentType");
            _documentTypes.Remove(documentType);
        }

        /// <summary>
        /// Clears the document list
        /// </summary>
        public void Clear() {
            if (_documentTypes != null) {
                _documentTypes.Clear();
            }
        }

        /// <summary>
        /// Returns whether a certain document type is in the collection.
        /// The document type is in the collection if it has the same id 
        /// or has the same root name, root namespace and identifier 
        /// discriminators.
        /// </summary>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public bool ContainsDocumentTypeByValue(RaspDocumentTypeConfig documentType) {
            Predicate<RaspDocumentTypeConfig> match = delegate(RaspDocumentTypeConfig current) { 
                return current.Equals(documentType);
            };
            return _documentTypes.Exists(match);
        }

        /// <summary>
        /// Returns whether a certain document type is in the collection.
        /// The document type is in the collectio if the reference is the 
        /// same.
        /// </summary>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public bool ContainsDocumentTypeByReference(RaspDocumentTypeConfig documentType) {
            return _documentTypes.Contains(documentType);
        }

        /// <summary>
        /// Gets the documents types with a certain root name and root namespace.
        /// This does not uniqely identfies a certain document type hence multiple
        /// document types can be returned as result.
        /// </summary>
        /// <param name="rootName"></param>
        /// <param name="rootNamespace"></param>
        /// <returns></returns>
        public IEnumerable<RaspDocumentTypeConfig> GetDocumentTypes(string rootName, string rootNamespace) {
            Predicate<RaspDocumentTypeConfig> match = delegate(RaspDocumentTypeConfig current) { return current.RootName == rootName && current.RootNamespace == rootNamespace; };
            List<RaspDocumentTypeConfig> results = _documentTypes.FindAll(match);
            return results;
        }

        /// <summary>
        /// Get a document type from a root name, root namespace and a collection of 
        /// identifier expressions. 
        /// </summary>
        /// <param name="rootName"></param>
        /// <param name="rootNamespace"></param>
        /// <param name="identifierDiscriminators"></param>
        /// <returns></returns>
        public RaspDocumentTypeConfig GetDocumentType(string rootName, string rootNamespace, XpathDiscriminatorConfigCollection identifierDiscriminators) {
            if (rootName == null) throw new ArgumentNullException("rootName");
            if (rootNamespace == null) throw new ArgumentNullException("rootNamespace");
            if (identifierDiscriminators == null) throw new ArgumentNullException("identifierDiscriminators");

            RaspDocumentTypeConfig documentType = null;
            if (!TryGetDocumentType(rootName, rootNamespace, identifierDiscriminators, out documentType)) {
                throw new NoDocumentTypeFoundFromParametersException(rootName, rootNamespace, identifierDiscriminators);
            }
            return documentType;
        }

        /// <summary>
        /// Get a document type from the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RaspDocumentTypeConfig GetDocumentType(Guid id) {
            RaspDocumentTypeConfig documentType = null;
            if (!TryGetDocumentType(id, out documentType))
                throw new NoDocumentTypeFoundFromIdException(id);
            return documentType;
        }

        /// <summary>
        /// Try to get the document type from a root name, root namespace and a 
        /// collection of identifier expressions.
        /// </summary>
        /// <param name="rootName"></param>
        /// <param name="rootNamespace"></param>
        /// <param name="identifierDiscriminators"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public bool TryGetDocumentType(string rootName, string rootNamespace, XpathDiscriminatorConfigCollection identifierDiscriminators, out RaspDocumentTypeConfig documentType) {
            if (rootName == null) throw new ArgumentNullException("rootName");
            if (rootNamespace == null) throw new ArgumentNullException("rootNamespace");
            if (identifierDiscriminators == null) throw new ArgumentNullException("identifierDiscriminators");

            documentType = null;
            Predicate<RaspDocumentTypeConfig> match = delegate(RaspDocumentTypeConfig current) {
                if (rootName != current.RootName) return false;
                if (rootNamespace != current.RootNamespace) return false;
                return identifierDiscriminators == current.IdentifierDiscriminators;
            };
            List<RaspDocumentTypeConfig> documentTypes = _documentTypes.FindAll(match);
            if (documentTypes.Count < 1) return false;
            if (documentTypes.Count > 1) throw new AmbiguousDocumentTypeResultFromParametersException(rootName, rootNamespace, identifierDiscriminators);
            documentType = documentTypes[0];
            return true;
        }

        /// <summary>
        /// Try to get the document type with a certain id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public bool TryGetDocumentType(Guid id, out RaspDocumentTypeConfig documentType) {
            documentType = null;
            Predicate<RaspDocumentTypeConfig> match = delegate(RaspDocumentTypeConfig current) {
                return id == current.Id;
            };
            List<RaspDocumentTypeConfig> documentTypes = _documentTypes.FindAll(match);
            if (documentTypes.Count < 1) return false;
            if (documentTypes.Count > 1) throw new AmbiguousDocumentTypeResultFromIdException(id);
            documentType = documentTypes[0];
            return true;
        }
    }
}