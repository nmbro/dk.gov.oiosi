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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using dk.gov.oiosi.exception;
using dk.gov.oiosi.xml.schematron;
using dk.gov.oiosi.xml.xpath.discriminator;

namespace dk.gov.oiosi.communication.configuration
{
    /// <summary>
    /// A RASP UBL Document type (such as an Invoice or a Notification)
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
    public class DocumentTypeConfig : IEquatable<DocumentTypeConfig>
    {
        private string _friendlyName = "";
        private string _rootNamespace = "";
        private string _rootName = "";
        private string _serviceContractTModel = "";
        private string _schemaPath = "";
        private string _stylesheetPath = "";
        private string _xsltTransformStylesheetPath = "";
        private Guid _id;
        private List<SchematronValidationConfig> _schematronValidationConfigCollection = new List<SchematronValidationConfig>();
        private List<PrefixedNamespace> _namespaces = new List<PrefixedNamespace>();
        private DocumentEndpointInformation _endpointType = new DocumentEndpointInformation();
        private XpathDiscriminatorConfigCollection _identifierDiscriminators = new XpathDiscriminatorConfigCollection();
        private CustomHeaderConfiguration _customHeaderConfiguration = new CustomHeaderConfiguration();
        private ProfileIdXPath _profileIdXPath = new ProfileIdXPath();
        private DocumentIdXPath _documentIdXPath = new DocumentIdXPath();

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentTypeConfig()
        {
        }

        /// <summary>
        /// Constructor that only takes the parameters to uniquely identify the document type.
        /// </summary>
        /// <param name="rootName"></param>
        /// <param name="rootNamespace"></param>
        /// <param name="identifierDiscriminators"></param>
        public DocumentTypeConfig(
            string rootName,
            string rootNamespace,
            XpathDiscriminatorConfigCollection identifierDiscriminators
            )
        {
            if (rootName == null) throw new NullArgumentException("rootName");
            if (rootNamespace == null) throw new NullArgumentException("rootNamespace");
            if (identifierDiscriminators == null) throw new NullArgumentException("identifierDiscriminators");
            this._id = Guid.NewGuid();
            _rootName = rootName;
            _rootNamespace = rootNamespace;
            _identifierDiscriminators = identifierDiscriminators;
        }

        /// <summary>
        /// Constructor that only takes the parameters to uniquely identify the document type.
        /// </summary>
        /// <param name="rootName"></param>
        /// <param name="rootNamespace"></param>
        /// <param name="identifierDiscriminators"></param>
        public DocumentTypeConfig(
            Guid id,
            string rootName,
            string rootNamespace,
            XpathDiscriminatorConfigCollection identifierDiscriminators
            )
        {
            if (rootName == null) throw new NullArgumentException("rootName");
            if (rootNamespace == null) throw new NullArgumentException("rootNamespace");
            if (identifierDiscriminators == null) throw new NullArgumentException("identifierDiscriminators");
            this._id = id;
            _rootName = rootName;
            _rootNamespace = rootNamespace;
            _identifierDiscriminators = identifierDiscriminators;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="friendlyName">Friendly name of the document</param>
        /// <param name="rootName">The document element name</param>
        /// <param name="rootNamespace">The document element namespace</param>
        /// <param name="schemaPath">Path to the schema</param>
        /// <param name="stylesheetPath">Path to the presentation stylesheet</param>
        /// <param name="serviceContractTModel">The tModel ID of the service contract</param>
        /// <param name="xsltTransformStylesheetPath">The path to the transformation stylesheet</param>
        /// <param name="endpointType">The endpoint type</param>
        /// <param name="identifierDiscriminators">Identifier discriminators</param>
        /// <param name="schematronValidationConfig">Settings to the schematron validation</param>
        /// <param name="profileIdXPath">XPath expression</param>
        public DocumentTypeConfig(
            Guid id,
            string friendlyName,
            string rootName,
            string rootNamespace,

            string schemaPath,
            string stylesheetPath,
            string serviceContractTModel,
            string xsltTransformStylesheetPath,
            DocumentEndpointInformation endpointType,
            XpathDiscriminatorConfigCollection identifierDiscriminators,
            List<SchematronValidationConfig> schematronValidationConfigCollection,
            ProfileIdXPath profileIdXPath,
            DocumentIdXPath documentIdXPath
            )
            : this(id, rootName, rootNamespace, identifierDiscriminators)
        {
            if (friendlyName == null) throw new NullArgumentException("friendlyName");
            if (schemaPath == null) throw new NullArgumentException("schemaPath");
            if (stylesheetPath == null) throw new NullArgumentException("stylesheetPath");
            if (serviceContractTModel == null) throw new NullArgumentException("serviceContractTModel");
            if (xsltTransformStylesheetPath == null) throw new NullArgumentException("xsltTransformStylesheetPath");
            if (endpointType == null) throw new NullArgumentException("endpointType");
            if (schematronValidationConfigCollection == null) throw new NullArgumentException("schematronValidationConfigs");

            _friendlyName = friendlyName;
            _schemaPath = schemaPath;
            _stylesheetPath = stylesheetPath;
            _serviceContractTModel = serviceContractTModel;
            _xsltTransformStylesheetPath = xsltTransformStylesheetPath;
            _endpointType = endpointType;
            this._schematronValidationConfigCollection = schematronValidationConfigCollection;
            _profileIdXPath = profileIdXPath;
            _documentIdXPath = documentIdXPath;
        }

        /*
        public DocumentTypeConfig(
            Guid id,
            string friendlyName,
            string rootName,
            string rootNamespace,
            string schemaPath,
            string stylesheetPath,
            string serviceContractTModel,
            string xsltTransformStylesheetPath,
            DocumentEndpointInformation endpointType,
            XpathDiscriminatorConfigCollection identifierDiscriminators,
            SchematronValidationConfig schematronValidationConfig,
            ProfileIdXPath profileIdXPath,
            DocumentIdXPath documentIdXPath
            )
            : this(id, rootName, rootNamespace, identifierDiscriminators)
        {
            if (friendlyName == null) throw new NullArgumentException("friendlyName");
            if (schemaPath == null) throw new NullArgumentException("schemaPath");
            if (stylesheetPath == null) throw new NullArgumentException("stylesheetPath");
            if (serviceContractTModel == null) throw new NullArgumentException("serviceContractTModel");
            if (xsltTransformStylesheetPath == null) throw new NullArgumentException("xsltTransformStylesheetPath");
            if (endpointType == null) throw new NullArgumentException("endpointType");
            if (schematronValidationConfig == null) throw new NullArgumentException("schematronValidationConfig");

            _friendlyName = friendlyName;
            _schemaPath = schemaPath;
            _stylesheetPath = stylesheetPath;
            _serviceContractTModel = serviceContractTModel;
            _xsltTransformStylesheetPath = xsltTransformStylesheetPath;
            _endpointType = endpointType;
            _schematronValidationConfig = schematronValidationConfig;
            _profileIdXPath = profileIdXPath;
            _documentIdXPath = documentIdXPath;
        }
        */

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="friendlyName">
        /// Friendly name of the document type e.g. for use in the UI
        /// </param>
        /// <param name="rootName">The name of the document root element</param>
        /// <param name="rootNamespace">The namespace of the document root element</param>
        /// <param name="schemaPath">Path to the schema used for validating this document type</param>
        /// <param name="stylesheetPath">
        /// Path to the stylesheet used for displaying documents of this type
        /// </param>
        /// <param name="serviceContractTModel">The Service contract TModel, used for UDDI lookups</param>
        /// <param name="xsltTransformStylesheetPath">Pthe to the xslt stylesheet</param>
        /// <param name="namespaces">Namespaces used by the document.</param>
        /// <param name="endpointType">Definition of a RASP endpoint type</param>
        /// <param name="identifierDiscriminators">
        /// XPath discriminators used for aditionel identification of document type
        /// </param>
        /// <param name="schematronValidationConfig">Settings to the schematron validation</param>
        /// <param name="profileIdXPath">Xpath expression</param>
        public DocumentTypeConfig(
            Guid id,
            string friendlyName,
            string rootName,
            string rootNamespace,
            string schemaPath,
            string stylesheetPath,
            string serviceContractTModel,
            string xsltTransformStylesheetPath,
            List<PrefixedNamespace> namespaces,
            DocumentEndpointInformation endpointType,
            XpathDiscriminatorConfigCollection identifierDiscriminators,
            List<SchematronValidationConfig> schematronValidationConfigCollection,
            ProfileIdXPath profileIdXPath,
            DocumentIdXPath documentIdXPath
        )
            : this(id, friendlyName, rootName, rootNamespace, schemaPath,
              stylesheetPath, serviceContractTModel, xsltTransformStylesheetPath,
              endpointType, identifierDiscriminators, schematronValidationConfigCollection, profileIdXPath, documentIdXPath)
        {
            if (namespaces == null) throw new NullArgumentException("namespaces");
            _namespaces = namespaces;
        }

        /// <summary>
        /// Gets and sets the ID of the documenttype.
        /// </summary>
        /// <remarks>Do not use the set property it is used for xml serialization</remarks>
        [XmlElement("Id")]
        public Guid Id
        {
            get { return this._id; }
            set
            {
                if (value == null)
                {
                    this._id = Guid.NewGuid();
                }
                else
                {
                    this._id = value;
                }
            }
        }

        /// <summary>
        /// Friendly name of the document type, e.g. for use in the UI
        /// </summary>
        [XmlElement("FriendlyName")]
        public string FriendlyName
        {
            get { return _friendlyName; }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _friendlyName = value;
            }
        }

        /// <summary>
        /// The namespace of the root node of the document
        /// </summary>
        [XmlElement("RootNamespace")]
        public string RootNamespace
        {
            get { return _rootNamespace; }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _rootNamespace = value;
            }
        }

        /// <summary>
        /// Name of the document type
        /// </summary>
        [XmlElement("RootName")]
        public string RootName
        {
            get { return _rootName; }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _rootName = value;
            }
        }

        /// <summary>
        /// Gets and sets the XpathDiscriminatorConfigCollection
        /// </summary>
        public XpathDiscriminatorConfigCollection IdentifierDiscriminators
        {
            get { return _identifierDiscriminators; }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _identifierDiscriminators = value;
            }
        }

        /// <summary>
        /// The Service contract TModel, used for UDDI lookups
        /// </summary>
        [XmlElement("ServiceContractTModel")]
        public string ServiceContractTModel
        {
            get { return _serviceContractTModel; }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _serviceContractTModel = value;
            }
        }

        /// <summary>
        /// The path to a XML schema file, defining the document type
        /// </summary>
        [XmlElement("SchemaPath")]
        public string SchemaPath
        {
            get { return _schemaPath; }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _schemaPath = value;
            }
        }

        /// <summary>
        /// The path to a stylesheet, defining how documents of this type should be presented
        /// </summary>
        [XmlElement("StylesheetPath")]
        public string StylesheetPath
        {
            get { return _stylesheetPath; }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _stylesheetPath = value;
            }
        }

        /// <summary>
        /// A list of namespaces used by this document type
        /// </summary>
        [XmlArray("Namespaces")]
        public PrefixedNamespace[] Namespaces
        {
            get { return _namespaces.ToArray(); }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _namespaces = new List<PrefixedNamespace>(value);
            }
        }

        /// <summary>
        /// A definition of what type of endpoints will receive documents of this type
        /// </summary>
        [XmlElement("EndpointType")]
        public DocumentEndpointInformation EndpointType
        {
            get { return _endpointType; }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _endpointType = value;
            }
        }

        /// <summary>
        /// The path to the XSLT transform stylesheet
        /// </summary>
        public string XsltTransformStylesheetPath
        {
            get { return _xsltTransformStylesheetPath; }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _xsltTransformStylesheetPath = value;
            }
        }

        /// <summary>
        /// Gets and sets the schematron validation configuration
        /// </summary>
        [XmlElement("SchematronValidationConfig")]
        public SchematronValidationConfig SchematronValidationConfig
        {
            get
            {
                return null;
                //throw new ArgumentException("The SchematronValidationConfig element, is now only allowed in a SchematronValidationConfigs collection.");
            }
            set
            {
                if (value != null)
                {
                    this._schematronValidationConfigCollection.Add(value);
                    logging.ILogger logger = logging.LoggerFactory.Create(this.GetType());
                    logger.Warn("In DocumentTypeConfig, the SchematronValidationConfig element is no longer allowed. Moved Into SchematronValidationConfigs collection.");
                }
            }
        }

        /// <summary>
        /// Gets and sets the schematron validation configuration
        /// </summary>
        [XmlArray("SchematronValidationConfigs")]
        public SchematronValidationConfig[] SchematronValidationConfigs
        {
            get { return this._schematronValidationConfigCollection.ToArray(); }
            set
            {
                if (value == null) throw new NullArgumentException("value");
                _schematronValidationConfigCollection = new List<SchematronValidationConfig>(value);
            }
        }

        /// <summary>
        /// Gets and sets the coniguration section for custom headers
        /// </summary>
        [XmlElement("CustomHeaderConfiguration")]
        public CustomHeaderConfiguration CustomHeaderConfiguration
        {
            get { return _customHeaderConfiguration; }
            set { _customHeaderConfiguration = value; }
        }

        /// <summary>
        /// Name of the document type
        /// </summary>
        [XmlElement("ProfileIdXPath")]
        public ProfileIdXPath ProfileIdXPath
        {
            get { return _profileIdXPath; }
            set
            {
                if (value == null) throw new NullArgumentException("ProfileIdXPath value");
                _profileIdXPath = value;
            }
        }

        /// <summary>
        /// Name of the document type
        /// </summary>
        [XmlElement("DocumentIdXPath")]
        public DocumentIdXPath DocumentIdXPath
        {
            get { return _documentIdXPath; }
            set
            {
                if (value == null) throw new NullArgumentException("_documentIdXPath value");
                _documentIdXPath = value;
            }
        }

        /// <summary>
        /// Adds a namespace
        /// </summary>
        /// <param name="ns">Namespace</param>
        /// <param name="prefix">Prefix</param>
        public void AddNamespace(string ns, string prefix)
        {
            _namespaces.Add(new PrefixedNamespace(ns, prefix));
        }

        /// <summary>
        /// Adds a namespace
        /// </summary>
        /// <param name="ns">A namespace with a prefix</param>
        public void AddNamespace(PrefixedNamespace ns)
        {
            _namespaces.Add(ns);
        }

        /// <summary>
        /// Compares RaspDocumentTypeConfig elements on the given xml document
        /// </summary>
        /// <param name="document">The document to check for</param>
        /// <returns>Returns true if name and namespace of the root element matches</returns>
        public bool IsDocumentOfType(XmlDocument document)
        {
            if (document == null) throw new NullArgumentException("document");
            XmlElement root = document.DocumentElement;
            if (root.LocalName != RootName) return false;
            if (root.NamespaceURI != RootNamespace) return false;
            return XPathDiscriminator.DiscriminateCollectionAnded(_identifierDiscriminators, document, Namespaces);
        }

        #region IEquatable<RaspDocumentTypeConfig> Members

        /// <summary>
        /// Return whether the given document type config is equal to another. They are equal if
        /// they have the same id or if they have the same root name, root namespace and identifier discriminators.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DocumentTypeConfig other)
        {
            if (_id == other._id) return true;
            if (_rootName != other._rootName) return false;
            if (_rootNamespace != other._rootNamespace) return false;
            return _identifierDiscriminators.Equals(other._identifierDiscriminators);
        }

        #endregion IEquatable<RaspDocumentTypeConfig> Members
    }
}