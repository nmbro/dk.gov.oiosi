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
  */

using System;
using System.Collections.Generic;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.xml.schematron;
using dk.gov.oiosi.xml.xpath.discriminator;

namespace dk.gov.oiosi.raspProfile
{
    /// <summary>
    /// Defines all the default OIOXML/OIOUBL document type configurations
    /// </summary>
    public class DefaultDocumentTypes
    {
        /// <summary>
        /// delegate DocumentTypeConfig
        /// </summary>
        /// <returns></returns>
        public delegate DocumentTypeConfig DocumentTypeConfigDelegate();

        /// <summary>
        /// Adds all the document types
        /// </summary>
        public virtual void Add()
        {
            this.Add(this.GetOioXmlInvoiceV07);
            this.Add(this.GetOioXmlCreditNoteV07);

            // Læs ind
            //this.Add(this.GetScanInvoiceV07);
            //this.Add(this.GetScanCreditNoteV07);

            this.Add(this.GetOioUblApplicationResponse);               // Applikationsmeddelelse
            this.Add(this.GetOioUblCatalogue);                         // Katalog
            this.Add(this.GetOioUblCatalogueRequest);                  // Katalogforespørgsel
            this.Add(this.GetOioUblCatalogueItemSpecificationUpdate);  // Opdatering af katalogelement
            this.Add(this.GetOioUblCataloguePricingUpdate);            // Opdatering af katalogpriser
            this.Add(this.GetOioUblCatalogueDeletion);                 // Sletning af katalog
            this.Add(this.GetOioUblCreditNote);                        // Kreditnota
            this.Add(this.GetOioUblInvoice);                           // Faktura
            this.Add(this.GetOioUblOrder);                             // Ordre
            this.Add(this.GetOioUblOrderCancellation);                 // Ordreannulering
            this.Add(this.GetOioUblOrderResponse);                     // Ordrebekræftelse
            this.Add(this.GetOioUblOrderChange);                       // Ordreændring
            this.Add(this.GetOioUblOrderResponseSimple);               // Simpel ordrebekræftelse
            this.Add(this.GetOioUblReminder);                          // Rykker
            this.Add(this.GetOioUblStatement);                         // KontoUdtog
            this.Add(this.GetOioUblUtilityStatement);                  // Forsynings specifikation

            // Peppol
            //this.Add(this.GetPeppol36aApplicationResponse);            // Applikationsmeddelelse
            this.Add(this.GetPeppol1aCatalogue);                       // Faktura
            this.Add(this.GetPeppol1aApplicationResponse);             // Applikationsmeddelelse
            this.Add(this.GetPeppol5aCreditNote);                      // Kreditnota
            this.Add(this.GetPeppol5aInvoice);                         // Faktura
            this.Add(this.GetPeppol30aDespatchAdvice);                 // Forsendelsesadvis
            this.Add(this.GetPeppol4aInvoice);                         // Faktura
            this.Add(this.GetPeppol3aOrder);                           // Ordre
            //this.Add(this.GetPeppol28aOrder);                          // Ordre
            //this.Add(this.GetPeppol28aOrderResponse);                  // Ordrebekræftelse
            //this.Add(this.GetAttachedDocument);

            // NemKonto  (schemas - 2006/05/01/)
            this.Add(this.GetNKSPayment);
            this.Add(this.GetNKSReceipt0);
            this.Add(this.GetNKSReceipt1);
            this.Add(this.GetNKSResponse2);
            this.Add(this.GetNKSResponse5);
            this.Add(this.GetNKSResponse7);
            this.Add(this.GetNKSResponse8);
            this.Add(this.GetNKSResponse9);

            // NemKonto PU (schemas - 2007/10/01/)
            this.Add(this.GetNemKontoPURequest);
            this.Add(this.GetNemKontoPUResponse);
        }

        /// <summary>
        /// Adds a document type definition to the collection
        /// </summary>
        /// <param name="documentTypeConfigDelegate"></param>
        public virtual void Add(DocumentTypeConfigDelegate documentTypeConfigDelegate)
        {
            DocumentTypeConfig documentType = documentTypeConfigDelegate();
            DocumentTypeCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            if (configuration.ContainsDocumentTypeByValue(documentType))
            {
                // already added
                //Debug.Fail("already added");
                configuration.AddDocumentType(documentType);
            }
            else
            {
                configuration.AddDocumentType(documentType);
            }
        }

        /// <summary>
        /// Adds all the document types from configuration, clears collection first
        /// </summary>
        public virtual void CleanAdd()
        {
            DocumentTypeCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            configuration.Clear();
            Add();
        }

        /// <summary>
        /// CreateKey
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        [Obsolete("Rename - use CreateKeyUbl")]
        public virtual ServiceEndpointKey CreateKey(string xpath)
        {
            return this.CreateKeyUbl(xpath);
        }

        /// <summary>
        /// Create key
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public virtual ServiceEndpointKey CreateKeyUbl(string xpath)
        {
            // Note - most key mapping is handled by NHR.
            ServiceEndpointKey key = new ServiceEndpointKey(xpath);
            string keyTypeXpath = xpath + "/@schemeID";
            KeyTypeMappingExpression mappingExpression = new KeyTypeMappingExpression("EndpointKeyType", keyTypeXpath);
            key.AddMappingExpression(mappingExpression);

            //// NOTE:
            //// Old JAVaRasp requires some mappings keys, when receiving.
            //// oioxml
            ////KeyTypeMapping uppercasedEanMapping = new KeyTypeMapping("EAN", "ean");
            ////KeyTypeMapping uppercasedCVRMapping = new KeyTypeMapping("CVR", "cvr");

            //// oioubl
            KeyTypeMapping glnMapping = new KeyTypeMapping("GLN", "ean");
            KeyTypeMapping dkcvrMapping = new KeyTypeMapping("DK:CVR", "cvr");
            KeyTypeMapping dkcprMapping = new KeyTypeMapping("DK:CPR", "cpr");
            KeyTypeMapping dkseMapping = new KeyTypeMapping("DK:SE", "se");
            KeyTypeMapping dkpMapping = new KeyTypeMapping("DK:P", "p");
            KeyTypeMapping dkvansMapping = new KeyTypeMapping("DK:VANS", "vans");

            ////mappingExpression.AddMapping(uppercasedEanMapping);
            ////mappingExpression.AddMapping(uppercasedCVRMapping);
            mappingExpression.AddMapping(glnMapping);
            mappingExpression.AddMapping(dkcvrMapping);
            mappingExpression.AddMapping(dkcprMapping);
            mappingExpression.AddMapping(dkseMapping);
            mappingExpression.AddMapping(dkpMapping);
            mappingExpression.AddMapping(dkvansMapping);

            return key;
        }

        /// <summary>
        /// CreateSchematronValidationConfig OioUbl
        /// </summary>
        /// <param name="xslPath"></param>
        /// <returns></returns>
        public virtual SchematronValidationConfig CreateSchematronValidationConfig_OioUbl(string xslPath)
        {
            const string schematronErrorXPath = "/Schematron/Error";
            const string schematronErorMessageXPath = "/Schematron/Error/Description";

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, schematronErrorXPath, schematronErorMessageXPath);
            return schematronValidationConfig;
        }

        /// <summary>
        /// CreateSchematronValidationConfig OioXml
        /// </summary>
        /// <param name="xslPath"></param>
        /// <returns></returns>
        public virtual SchematronValidationConfig CreateSchematronValidationConfig_OioXml(string xslPath)
        {
            const string schematronErrorXPath = "/schematron/error";
            const string schematronErorMessageXPath = "/schematron/error";

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, schematronErrorXPath, schematronErorMessageXPath);
            return schematronValidationConfig;
        }

        ////public virtual SchematronValidationConfig CreateSchematronValidationConfig_PeppolBIICore(string xslPath)
        ////{
        ////    const string schematronErrorXPath = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']";
        ////    const string schematronErorMessageXPath = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']/svrl:text";

        ////    SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, schematronErrorXPath, schematronErorMessageXPath);
        ////    return schematronValidationConfig;
        ////}

        /// <summary>
        /// CreateSchematronValidationConfig Peppol
        /// </summary>
        /// <param name="xslPath"></param>
        /// <returns></returns>
        public virtual SchematronValidationConfig CreateSchematronValidationConfig_Peppol(string xslPath)
        {
            const string schematronErrorXPath = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']";
            const string schematronErorMessageXPath = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']/svrl:text";

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, schematronErrorXPath, schematronErorMessageXPath);
            return schematronValidationConfig;
        }

        ////public virtual SchematronValidationConfig CreateSchematronValidationConfig_PeppolBIIRules(string xslPath)
        ////{
        ////    const string schematronErrorXPath = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']";
        ////    const string schematronErorMessageXPath = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']/svrl:text";

        ////    SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, schematronErrorXPath, schematronErorMessageXPath);
        ////    return schematronValidationConfig;
        ////}

        /// <summary>
        /// CreateSchematronValidationConfig NKSPU
        /// </summary>
        /// <param name="xslPath"></param>
        /// <returns></returns>
        public virtual SchematronValidationConfig CreateSchematronValidationConfig_NKSPU(string xslPath)
        {
            const string schematronErrorXPath = "/Schematron/Error";
            const string schematronErorMessageXPath = "/Schematron/Error/Description";

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, schematronErrorXPath, schematronErorMessageXPath);
            return schematronValidationConfig;
        }

        

        ////public virtual SchematronValidationConfig CreateSchematronValidationConfig_PeppolOpenPeppol(string xslPath)
        ////{
        ////    const string schematronErrorXPath = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']";
        ////    const string schematronErorMessageXPath = "/svrl:schematron-output/svrl:failed-assert[@flag='fatal']/svrl:text";

        ////    SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, schematronErrorXPath, schematronErorMessageXPath);
        ////    return schematronValidationConfig;
        ////}

        /// <summary>
        /// Create key
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public virtual ServiceEndpointKey CreateSenderKey(string xpath)
        {
            // Why is there a differents between CreateSenderKey and CreateKey ??? It is not the
            // configuration file job, to limith the use of NemHandel If the xml document contain
            // errors, the errors should be handled be schema or schematron validation

            ServiceEndpointKey key = this.CreateKeyUbl(xpath);

            return key;
        }

        /// <summary>
        /// Get CustomizationId Oioubl 2_01
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public virtual XPathDiscriminatorConfig GetCustomizationIdOioUbl2_01(string root)
        {
            string expectedResult = "OIOUBL-2.0(1|2)";
            string xpathExpression = "/root:" + root + "/cbc:CustomizationID";
            XPathDiscriminatorConfig id = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="destinationFriendlyNameXPath"></param>
        /// <param name="destinationKeyXPath"></param>
        /// <param name="senderFriendlyNameXPath"></param>
        /// <param name="senderKeyXPath"></param>
        /// <param name="profileIdXPathStr"></param>
        /// <param name="documentEndpointRequestAction"></param>
        /// <param name="documentEndpointResponseAction"></param>
        /// <param name="rootName"></param>
        /// <param name="schematronValidationConfigCollection"></param>
        /// <param name="documentName"></param>
        /// <param name="rootNamespace"></param>
        /// <param name="xsdPath"></param>
        /// <param name="xslUIPath"></param>
        /// <param name="serviceContractTModel"></param>
        /// <param name="documentIdXPath"></param>
        /// <param name="ids"></param>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        [Obsolete("use GetDocumentTypeConfigPeppol")]
        public virtual DocumentTypeConfig GetDocumentTypeConfig(string id, string destinationFriendlyNameXPath, string destinationKeyXPath, string senderFriendlyNameXPath, string senderKeyXPath, string profileIdXPathStr, string documentEndpointRequestAction, string documentEndpointResponseAction, string rootName, List<SchematronValidationConfig> schematronValidationConfigCollection, string documentName, string rootNamespace, string xsdPath, string xslUIPath, string serviceContractTModel, string documentIdXPath, XpathDiscriminatorConfigCollection ids, List<PrefixedNamespace> namespaces)
        {
            return this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, namespaces);
        }

        /// <summary>
        /// GetDocumentTypeConfigPeppol
        /// </summary>
        /// <param name="id"></param>
        /// <param name="destinationFriendlyNameXPath"></param>
        /// <param name="destinationKeyXPath"></param>
        /// <param name="senderFriendlyNameXPath"></param>
        /// <param name="senderKeyXPath"></param>
        /// <param name="profileIdXPathStr"></param>
        /// <param name="documentEndpointRequestAction"></param>
        /// <param name="documentEndpointResponseAction"></param>
        /// <param name="rootName"></param>
        /// <param name="schematronValidationConfigCollection"></param>
        /// <param name="documentName"></param>
        /// <param name="rootNamespace"></param>
        /// <param name="xsdPath"></param>
        /// <param name="xslUIPath"></param>
        /// <param name="serviceContractTModel"></param>
        /// <param name="documentIdXPath"></param>
        /// <param name="ids"></param>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        public virtual DocumentTypeConfig GetDocumentTypeConfigPeppol(string id, string destinationFriendlyNameXPath, string destinationKeyXPath, string senderFriendlyNameXPath, string senderKeyXPath, string profileIdXPathStr, string documentEndpointRequestAction, string documentEndpointResponseAction, string rootName, List<SchematronValidationConfig> schematronValidationConfigCollection, string documentName, string rootNamespace, string xsdPath, string xslUIPath, string serviceContractTModel, string documentIdXPath, XpathDiscriminatorConfigCollection ids, List<PrefixedNamespace> namespaces)
        {
            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey key = this.CreateKeyUbl(destinationKeyXPath);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = CreateSenderKey(senderKeyXPath);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, friendlyName, key, senderFriendlyName, senderKey);//, profileIdXPath);

            //SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, schematrongErrorXPath, schematronErorMessageXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);

            namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /// <summary>
        /// GetDocumentTypeConfigOioublV07
        /// </summary>
        /// <param name="id"></param>
        /// <param name="destinationFriendlyNameXPath"></param>
        /// <param name="destinationKeyXPath"></param>
        /// <param name="senderFriendlyNameXPath"></param>
        /// <param name="senderKeyXPath"></param>
        /// <param name="profileXPath"></param>
        /// <param name="documentEndpointRequestAction"></param>
        /// <param name="documentEndpointResponseAction"></param>
        /// <param name="rootName"></param>
        /// <param name="schematronValidationConfigCollection"></param>
        /// <param name="documentName"></param>
        /// <param name="rootNamespace"></param>
        /// <param name="xsdPath"></param>
        /// <param name="xslUIPath"></param>
        /// <param name="serviceContractTModel"></param>
        /// <param name="documentIdXPath"></param>
        /// <returns></returns>
        public virtual DocumentTypeConfig GetDocumentTypeConfigOioublV07(string id, string destinationFriendlyNameXPath, string destinationKeyXPath, string senderFriendlyNameXPath, string senderKeyXPath, string profileXPath, string documentEndpointRequestAction, string documentEndpointResponseAction, string rootName, List<SchematronValidationConfig> schematronValidationConfigCollection, string documentName, string rootNamespace, string xsdPath, string xslUIPath, string serviceContractTModel, string documentIdXPath)
        {
            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey key = this.CreateKeyUbl(destinationKeyXPath);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = CreateSenderKey(senderKeyXPath);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileXPath);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, friendlyName, key, senderFriendlyName, senderKey);//, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = GetOioxmlNamespaces();
            namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /// <summary>
        /// GetDocumentTypeConfigOioublV2
        /// </summary>
        /// <param name="id"></param>
        /// <param name="destinationFriendlyNameXPath"></param>
        /// <param name="destinationKeyXPath"></param>
        /// <param name="senderFriendlyNameXPath"></param>
        /// <param name="senderKeyXPath"></param>
        /// <param name="profileIdXPathStr"></param>
        /// <param name="documentEndpointRequestAction"></param>
        /// <param name="documentEndpointResponseAction"></param>
        /// <param name="rootName"></param>
        /// <param name="schematronValidationConfigCollection"></param>
        /// <param name="documentName"></param>
        /// <param name="rootNamespace"></param>
        /// <param name="xsdPath"></param>
        /// <param name="xslUIPath"></param>
        /// <param name="serviceContractTModel"></param>
        /// <param name="documentIdXPath"></param>
        /// <returns></returns>
        public virtual DocumentTypeConfig GetDocumentTypeConfigOioublV2(string id, string destinationFriendlyNameXPath, string destinationKeyXPath, string senderFriendlyNameXPath, string senderKeyXPath, string profileIdXPathStr, string documentEndpointRequestAction, string documentEndpointResponseAction, string rootName, List<SchematronValidationConfig> schematronValidationConfigCollection, string documentName, string rootNamespace, string xsdPath, string xslUIPath, string serviceContractTModel, string documentIdXPath)
        {
            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey key = this.CreateKeyUbl(destinationKeyXPath);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = CreateSenderKey(senderKeyXPath);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, friendlyName, key, senderFriendlyName, senderKey);//, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            XPathDiscriminatorConfig xPathDiscriminatorConfig = GetCustomizationIdOioUbl2_01(rootName);
            ids.Add(xPathDiscriminatorConfig);

            //SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, OIOUBL_SCHEMATRON_ERROR_XPATH, OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = GetUblNamespaces();
            namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        ////public virtual DocumentTypeConfig GetDocumentTypeConfigNKSPU(string id, string destinationFriendlyNameXPath, string destinationKeyXPath, string senderFriendlyNameXPath, string senderKeyXPath, string profileIdXPathStr, string documentEndpointRequestAction, string documentEndpointResponseAction, string rootName, List<SchematronValidationConfig> schematronValidationConfigCollection, string documentName, string rootNamespace, string xsdPath, string xslUIPath, string serviceContractTModel, string documentIdXPath)
        ////{
           
        ////}

        /// <summary>
        /// The OioUbl Application Response document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblApplicationResponse()
        {
            const string id = "40c9fbee-ad39-48ed-9e04-c28cbbf8a38c";
            const string documentName = "Applikationsmeddelse";
            const string rootName = "ApplicationResponse";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-ApplicationResponse-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_ApplicationResponse_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/ApplicationResponseHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:SenderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:SenderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponse201Interface/SubmitApplicationResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponse201Interface/SubmitApplicationResponseResponse";
            const string serviceContractTModel = "uddi:42F92342-C3ED-46ff-8A8A-6518F55D5CD5";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Catalogue document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblCatalogue()
        {
            const string id = "68db0c6f-ec2c-44ad-b1c3-bdfae65ee5f0";
            const string documentName = "Katalog";
            const string rootName = "Catalogue";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Catalogue-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Catalogue-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_Catalogue_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueResponse201Interface/SubmitCatalogueResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueResponse201Interface/SubmitCatalogueResponseRequestResponse";
            const string serviceContractTModel = "uddi:b8a5a5d0-df9f-11dc-889a-1a827c218899";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Catalogue Deletion definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblCatalogueDeletion()
        {
            const string id = "0efa1c48-b5e3-4eb8-bc5f-3fbd78daba10";
            const string documentName = "Sletning af katalog";
            const string rootName = "CatalogueDeletion";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CatalogueDeletion-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CatalogueDeletion-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_CatalogueDeletion_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueDeletionResponse201Interface/SubmitCatalogueDeletionResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueDeletionResponse201Interface/SubmitCatalogueDeletionResponseResponse";
            const string serviceContractTModel = "uddi:40e5cbd0-dfa2-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Catalogue Item Specification Update document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblCatalogueItemSpecificationUpdate()
        {
            const string id = "fb3034ea-eaaf-434b-8798-0433db497e66";
            const string documentName = "Opdatering af katalogelement";
            const string rootName = "CatalogueItemSpecificationUpdate";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CatalogueItemSpecificationUpdate-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CatalogueItemSpecificationUpdate-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_CatalogueItemSpecificationUpdate_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueItemSpecificationUpdateResponse201Interface/SubmitCatalogueItemSpecificationUpdateResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueItemSpecificationUpdateResponse201Interface/SubmitCatalogueItemSpecificationUpdateResponseResponse";
            const string serviceContractTModel = "uddi:63eab5c0-dfa0-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Catalogue Pricing Update document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblCataloguePricingUpdate()
        {
            const string id = "02092e85-c2e4-4bb2-b22d-3eec04007a36";
            const string documentName = "Opdatering af katalogpriser";
            const string rootName = "CataloguePricingUpdate";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CataloguePricingUpdate-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CataloguePricingUpdate-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_CataloguePricingUpdate_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CataloguePricingUpdateResponse201Interface/SubmitCataloguePricingUpdateResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CataloguePricingUpdateResponse201Interface/SubmitCataloguePricingUpdateResponseResponse";
            const string serviceContractTModel = "uddi:abdb2720-dfa0-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Catalogue Request document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblCatalogueRequest()
        {
            const string id = "463984d1-4ba5-44d6-8903-565cc56dd4cb";
            const string documentName = "Katalogforespørgsel";
            const string rootName = "CatalogueRequest";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CatalogueRequest-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CatalogueRequest-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_CatalogueRequest_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ProviderParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueRequestResponse201Interface/SubmitCatalogueRequestResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueRequestResponse201Interface/SubmitCatalogueRequestResponseResponse";
            const string serviceContractTModel = "uddi:0cb0ff80-dfa0-11dc-889a-1a827c218899";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Credit Note document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblCreditNote()
        {
            const string id = "a25f2c30-cb5b-404d-886a-9030621f7eea";
            const string documentName = "Kreditnota";
            const string rootName = "CreditNote";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CreditNote-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_CreditNote_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/CreditNoteHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CreditNote201Interface/SubmitCreditNoteRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CreditNote201Interface/SubmitCreditNoteResponse";
            const string serviceContractTModel = "uddi:E4EC9613-4830-4bab-AFEE-C37AB1C67AEC";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Invoice document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblInvoice()
        {
            const string id = "c0220657-c101-4d7d-9670-c9463e1559d5";
            const string documentName = "Faktura";
            const string rootName = "Invoice";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Invoice-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_Invoice_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/InvoiceHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice201Interface/SubmitInvoiceRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice201Interface/SubmitInvoiceResponse";
            const string serviceContractTModel = "uddi:2e0b402a-7a5e-476b-8686-b33f54fd1f47";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Order document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblOrder()
        {
            const string id = "5b84d1f6-f315-4a2c-a84d-095b10cc5a2c";
            const string documentName = "Ordre";
            const string rootName = "Order";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Order-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Order-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_Order_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Order201Interface/SubmitOrderRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Order201Interface/SubmitOrderResponse";
            const string serviceContractTModel = "uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Order Cancellation document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblOrderCancellation()
        {
            const string id = "c4a09991-d038-4e51-bb06-2cdffe6c1b77";
            const string documentName = "Ordreannulering";
            const string rootName = "OrderCancellation";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderCancellation-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-OrderCancellation-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_OrderCancellation_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderCancellationHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderCancellationResponse201Interface/SubmitOrderCancellationResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderCancellationResponse201Interface/SubmitOrderCancellationResponseResponse";
            const string serviceContractTModel = "uddi:7ba80590-dfa1-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Order Change document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblOrderChange()
        {
            const string id = "0412fdc2-5f07-4e6f-a8fd-c0dc7d780dce";
            const string documentName = "Ordreændring";
            const string rootName = "OrderChange";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderChange-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-OrderChange-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_OrderChange_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderChangeHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderChangeResponse201Interface/SubmitOrderChangeResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderChangeResponse201Interface/SubmitOrderChangeResponseResponse";
            const string serviceContractTModel = "uddi:ea4bc88f-9479-4f9b-a354-4acabdb99336";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Order Response document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblOrderResponse()
        {
            const string id = "ba652e7d-e8bd-4926-8bd8-9e19a5ca23e6";
            const string documentName = "Ordrebekræftelse";
            const string rootName = "OrderResponse";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderResponse-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-OrderResponse-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_OrderResponse_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderResponseHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseResponse201Interface/SubmitOrderResponseResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseResponse201Interface/SubmitOrderResponseResponseResponse";
            const string serviceContractTModel = "uddi:ed6d3c40-dfa1-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Order Response Simple document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblOrderResponseSimple()
        {
            const string id = "c8577e35-7de6-49f6-926c-c061f5a7d1b6";
            const string documentName = "Simpel ordrebekræftelse";
            const string rootName = "OrderResponseSimple";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderResponseSimple-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-OrderResponseSimple-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_OrderResponseSimple_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderResponseSimpleHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseSimple201Interface/SubmitOrderResponseSimpleRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseSimple201Interface/SubmitOrderResponseSimpleResponse";
            const string serviceContractTModel = "uddi:3B0B1309-B575-4d69-9C8F-4126C53CD7B0";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Reminder document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblReminder()
        {
            const string id = "b552710b-e4c2-44f6-a89e-1b158375b5f3";
            const string documentName = "Rykker";
            const string rootName = "Reminder";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Reminder-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Reminder-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_Reminder_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/ReminderHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Reminder201Interface/SubmitReminderRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Reminder201Interface/SubmitReminderResponse";
            const string serviceContractTModel = "uddi:4FBBBDEF-0A8E-4d5e-9B9D-23C8FD98E9CE";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Statement document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblStatement()
        {
            const string id = "76c4f0fa-e969-4360-9a04-8de3c675d4f2";
            const string documentName = "KontoUdtog";
            const string rootName = "Statement";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Statement-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Statement-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_Statement_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/StatementHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/StatementResponse201Interface/SubmitStatementResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/StatementResponse201Interface/SubmitStatementResponseResponse";
            const string serviceContractTModel = "uddi:4e383840-bcfc-11dc-a81b-bfc65441a808";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Utility Statement document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioUblUtilityStatement()
        {
            const string id = "eee3da84-27b7-4b37-81b1-cfb9d2942a00";
            const string documentName = "Forsynings Specifikation";
            const string rootName = "UtilityStatement";
            const string rootNamespace = "urn:oioubl:names:specification:oioubl:schema:xsd:UtilityStatement-2";
            const string xsdPath = "Resources/Schemas/OIOUBL_v2.1-b/maindoc/UBL-UtilityStatement-2.1.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_UtilityStatement_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/UtilityStatementHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:SenderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:SenderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/UtilityStatement201Interface/SubmitUtilityStatementRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/UtilityStatement201Interface/SubmitUtilityStatementResponse";
            const string serviceContractTModel = "uddi:236f277d-a786-4724-a16e-26398b685a07";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioUbl(xslPath));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);

            // Note - The namespace for utilityStatement is not the same for the other oioubl.
            List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>();
            namespaces.Add(new PrefixedNamespace("urn:oioubl:names:specification:oioubl:schema:xsd:CommonAggregateComponents-2", "cac"));
            namespaces.Add(new PrefixedNamespace("urn:oioubl:names:specification:oioubl:schema:xsd:CommonBasicComponents-2", "cbc"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CoreComponentParameters-2", "ccts"));
            namespaces.Add(new PrefixedNamespace("urn:oioubl:names:specification:oioubl:schema:xsd:SpecializedDatatypes-2", "sdt"));
            namespaces.Add(new PrefixedNamespace("urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2", "udt"));
            namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentTypeConfig.Namespaces = namespaces.ToArray();

            return documentTypeConfig;
        }

        /// <summary>
        /// The OioXml Credit Note 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioXmlCreditNoteV07()
        {
            const string id = "7bd520d7-6ae5-4a3c-8604-082e69414092";
            const string documentName = "Kreditnota v0.7";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pcm/";
            const string xsdPath = "Resources/Schemas/OIOXML_v0.7/pcmStrict.xsd";
            const string xslPath = "Resources/Schematrons/OIOXML_v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML_v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:" + rootName + "/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/com:SellerParty/com:PartyName/com:Name";
            const string profileIdXPathStr = "string('OIOXML elektronisk handel')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07Interface/SubmitCreditNote07Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07Interface/SubmitCreditNote07Response";
            const string serviceContractTModel = "uddi:3bbc9cf0-3c4c-11dc-98be-6976502198bd";
            const string documentIdXPath = "/root:" + rootName + "/com:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioXml(xslPath));

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioXml Invoice 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioXmlInvoiceV07()
        {
            const string id = "5a15a880-eef8-40c0-80f2-bb65226f50c2";
            const string documentName = "Faktura v0.7";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pie/";
            const string xsdPath = "Resources/Schemas/OIOXML_v0.7/pieStrict.xsd";
            const string xslPath = "Resources/Schematrons/OIOXML_v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML_v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:" + rootName + "/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/com:SellerParty/com:PartyName/com:Name";
            const string profileIdXPathStr = "string('OIOXML elektronisk handel')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07Interface/SubmitInvoice07Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07Interface/SubmitInvoice07Response";
            const string serviceContractTModel = "uddi:bc99bb01-80f9-4f52-89dc-edf7732c56f9";
            const string documentIdXPath = "/root:" + rootName + "/com:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioXml(xslPath));

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        //// DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
        /// <summary>
        /// Get name-space
        /// </summary>
        /// <returns></returns>
        public virtual List<PrefixedNamespace> GetOioxmlNamespaces()
        {
            List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>();
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/ubl/xml/schemas/0p71/common/", "com"));
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/ubl/xml/schemas/0p71/maindoc/", "main"));
            return namespaces;
        }

        /// <summary>
        /// The OioXml scanned Credit Note 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioXmlScanCreditNoteV07()
        {
            const string id = "d430da7c-b4fd-4ac2-bd08-3ae77d680ffa";
            const string documentName = "Kreditnota v0.7 - Læs ind";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pcp/";
            const string xsdPath = "Resources/Schemas/OIOXML_v0.7/pcpStrict.xsd";
            const string xslPath = "Resources/Schematrons/OIOXML_v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML_v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:" + rootName + "/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/com:SellerParty/com:PartyName/com:Name";
            const string profileIdXPathStr = "string('OIOXML elektronisk handel - læs ind')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07pcpInterface/SubmitCreditNote07pcpRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07pcpInterface/SubmitCreditNote07pcpResponse";
            const string serviceContractTModel = "uddi:599fbfe0-a894-11dc-a813-bfc65441a808";
            const string documentIdXPath = "/root:" + rootName + "/com:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioXml(xslPath));

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioXml scanned Invoice 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetOioXmlScanInvoiceV07()
        {
            const string id = "ef6f9602-7752-40a0-8b95-15440686c491";
            const string documentName = "Faktura v0.7 - Læs ind";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pip/";
            const string xsdPath = "Resources/Schemas/OIOXML_v0.7/pipStrict.xsd";
            const string xslPath = "Resources/Schematrons/OIOXML_v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML_v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:" + rootName + "/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/com:SellerParty/com:PartyName/com:Name";
            const string profileIdXPathStr = "string('OIOXML elektronisk handel - læs ind')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07pipInterface/SubmitInvoice07pipRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07pipInterface/SubmitInvoice07pipResponse";
            const string serviceContractTModel = "uddi:1867d8b0-a893-11dc-a813-bfc65441a808";
            const string documentIdXPath = "/root:" + rootName + "/com:ID";

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_OioXml(xslPath));

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The Peppol Application Response - BIS2.0-messagelevelresponse36a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol1aApplicationResponse()
        {
            const string id = "551e8437-f543-46cf-bd56-492a25e723fc";
            const string documentName = "Applikationsmeddelse (PEPPOL)";
            const string rootName = "ApplicationResponse";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-ApplicationResponse-2.1.xsd";

            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/ApplicationResponseHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:SenderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:SenderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponsePeppol1aInterface/SubmitApplicationResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponsePeppol1aInterface/SubmitApplicationResponseResponse";
            const string serviceContractTModel = "uddi:e79dd402-8f60-4811-9f59-1acb0c036d05";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns058:ver2.0:extended:urn:www.peppol.eu:bis:peppol1a:ver2.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-catalogue1a/XSLT/BIIRULES-UBL-T58.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-catalogue1a/XSLT/OPENPEPPOLCORE-UBL-T58.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-catalogue1a/XSLT/OPENPEPPOL-UBL-T58.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);
            return documentTypeConfig;
        }

        /// <summary>
        /// The Peppol Catalogue - BIS2.0-catalogue1a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol1aCatalogue()
        {
            const string id = "24750a44-9a18-46f4-85ef-50f00c90068b";
            const string documentName = "Katalog (PEPPOL)";
            const string rootName = "Catalogue";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Catalogue-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-Catalogue-2.1.xsd";

            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CataloguePeppol1aInterface/SubmitCataloguRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CataloguePeppol1aInterface/SubmitCatalogueResponse";
            const string serviceContractTModel = "uddi:6c917ef1-5143-4123-879a-471215dbd373";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns019:ver2.0:extended:urn:www.peppol.eu:bis:peppol1a:ver4.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-catalogue1a/XSLT/BIIRULES-UBL-T19.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-catalogue1a/XSLT/OPENPEPPOLCORE-UBL-T19.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-catalogue1a/XSLT/OPENPEPPOL-UBL-T19.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);
            return documentTypeConfig;
        }

        /// <summary>
        /// The Peppol Order - BIS2.0-ordering28a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol28aOrder()
        {
            const string id = "fe16f395-6d83-42ba-8ce1-cf79cc02fc08";
            const string documentName = "Ordre (PEPPOL)";
            const string rootName = "Order";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Order-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-Order-2.1.xsd";

            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderPeppol28aInterface/SubmitOrderRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderPeppol28aInterface/SubmitOrderResponse";
            const string serviceContractTModel = "uddi:f6680c9e-6d67-47cc-acde-cb97d0bce321";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns001:ver2.0:extended:urn:www.peppol.eu:bis:peppol28a:ver1.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-ordering28a/XSLT/BIIRULES-UBL-T01.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-ordering28a/XSLT/OPENPEPPOLCORE-UBL-T01.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-ordering28a/XSLT/OPENPEPPOL-UBL-T01.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);
            return documentTypeConfig;
        }

        /// <summary>
        /// The OioUbl Order Response - BIS2.0-ordering28a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol28aOrderResponse()
        {
            const string id = "1557aed8-e4e5-4cec-9824-4cf5eeaa268b";
            const string documentName = "Ordrebekræftelse (PEPPOL)";
            const string rootName = "OrderResponse";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderResponse-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-OrderResponse-2.1.xsd";

            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderResponseHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponsePeppol28aInterface/SubmitOrderResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponsePeppol28aInterface/SubmitOrderResponseResponse";
            const string serviceContractTModel = "uddi:900a898a-567e-4a05-9846-7d140fddb03c";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns076:ver2.0:extended:urn:www.peppol.eu:bis:peppol28a:ver1.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-ordering28a/XSLT/BIIRULES-UBL-T76.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-ordering28a/XSLT/OPENPEPPOLCORE-UBL-T76.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-ordering28a/XSLT/OPENPEPPOL-UBL-T76.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);
            return documentTypeConfig;
        }

        /// <summary>
        /// The Peppol DespatchAdvice - BIS2.0-despatchadvice30a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol30aDespatchAdvice()
        {
            const string id = "9e8b18e5-416e-4c41-9b9f-adadc3de6598";
            const string documentName = "Forsendelsesadvis";
            const string rootName = "DespatchAdvice";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-DespatchAdvice-2.1.xsd";

            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:DeliveryCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:DeliveryCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:DespatchSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:DespatchSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2014/09/15/DespatchAdvice10Interface/SubmitDespatchAdviceRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2014/09/15/DespatchAdvice10Interface/SubmitDespatchAdviceResponse";
            const string serviceContractTModel = "uddi:96dbec86-aa58-4f1e-ae03-ebb13079ce61";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns016:ver1.0:extended:urn:www.peppol.eu:bis:peppol30a:ver1.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-despatchadvice30a/XSLT/BIIRULES-UBL-T16.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-despatchadvice30a/XSLT/OPENPEPPOLCORE-UBL-T16.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-despatchadvice30a/XSLT/OPENPEPPOL-UBL-T16.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);

            //// Add extra namespace - Schema issue fix in updating the schema
            //List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>(documentTypeConfig.Namespaces);
            //namespaces.Add(new PrefixedNamespace("http://www.w3.org/2000/09/xmldsig#", "ds"));
            //documentTypeConfig.Namespaces = namespaces.ToArray();

            return documentTypeConfig;
        }

        /// <summary>
        /// The Peppol Application Response - BIS2.0-messagelevelresponse36a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol36aApplicationResponse()
        {
            const string id = "5b2de589-832d-4901-bbbb-8eba23e00709";
            const string documentName = "Applikationsmeddelse (PEPPOL)";
            const string rootName = "ApplicationResponse";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-ApplicationResponse-2.1.xsd";

            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/ApplicationResponseHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:SenderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:SenderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponsePeppol36aInterface/SubmitApplicationResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponsePeppol36aInterface/SubmitApplicationResponseResponse";
            const string serviceContractTModel = "uddi:c2552794-76ee-4ee8-8600-09283086492d";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns071:ver2.0:extended:urn:www.peppol.eu:bis:peppol36a:ver1.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-messagelevelresponse36a/XSLT/BIIRULES-UBL-T71.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-messagelevelresponse36a/XSLT/OPENPEPPOLCORE-UBL-T71.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-messagelevelresponse36a/XSLT/OPENPEPPOL-UBL-T71.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);
            return documentTypeConfig;
        }

        /// <summary>
        /// The Peppol Order - BIS2.0-order3a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol3aOrder()
        {
            const string id = "db2f9050-2adb-49c2-8f05-af9e440d12ca";
            const string documentName = "Ordre (PEPPOL)";
            const string rootName = "Order";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Order-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-Order-2.1.xsd";

            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderPeppol3aInterface/SubmitOrderRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderPeppol3aInterface/SubmitOrderResponse";
            const string serviceContractTModel = "uddi:873c25f6-23d2-4019-830e-89cc89386930";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns001:ver2.0:extended:urn:www.peppol.eu:bis:peppol3a:ver2.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-order3a/XSLT/BIIRULES-UBL-T01.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-order3a/XSLT/OPENPEPPOLCORE-UBL-T01.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-order3a/XSLT/OPENPEPPOL-UBL-T01.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);
            return documentTypeConfig;
        }

        /// <summary>
        /// The Peppol Invoice - BIS2.0-invoice4a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol4aInvoice()
        {
            const string id = "c1061668-0549-452c-b0cb-7d6428fdc5f7";
            const string documentName = "Faktura (PEPPOL)";
            const string rootName = "Invoice";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-Invoice-2.1.xsd";

            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/InvoiceHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/InvoicePeppol4aInterface/SubmitInvoiceRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/InvoicePeppol4aInterface/SubmitInvoiceResponse";
            const string serviceContractTModel = "uddi:e956ca42-0be2-487a-8573-580b523c248d";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns010:ver2.0:extended:urn:www.peppol.eu:bis:peppol4a:ver2.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-invoice4a/XSLT/BIIRULES-UBL-T10.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-invoice4a/XSLT/OPENPEPPOLCORE-UBL-T10.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-invoice4a/XSLT/OPENPEPPOL-UBL-T10.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);
            return documentTypeConfig;
        }

        /// <summary>
        /// The Peppol Credit Note - BIS2.0-billing5a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol5aCreditNote()
        {
            const string id = "21671b33-58a2-4ab5-96bd-42f6c4f22af6";
            const string documentName = "Kreditnota (PEPPOL)";
            const string rootName = "CreditNote";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-CreditNote-2.1.xsd";

            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/CreditNoteHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CreditNotePeppol5aInterface/SubmitCreditNoteRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CreditNotePeppol5aInterface/SubmitCreditNoteResponse";
            const string serviceContractTModel = "uddi:4db3f358-6184-4979-bbc9-5d65aee27132";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-billing5a/XSLT/BIIRULES-UBL-T14.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-billing5a/XSLT/OPENPEPPOLCORE-UBL-T14.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-billing5a/XSLT/OPENPEPPOL-UBL-T14.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);

            //// Add extra namespace - Schema issue fix in updating the schema
            //List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>(documentTypeConfig.Namespaces);
            //namespaces.Add(new PrefixedNamespace("http://www.w3.org/2000/09/xmldsig#", "ds"));
            //documentTypeConfig.Namespaces = namespaces.ToArray();

            return documentTypeConfig;
        }

        /// <summary>
        /// The Peppol Credit Note - BIS2.0-billing4a document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetPeppol5aInvoice()
        {
            const string id = "c9f45e05-8cc0-44df-ab1e-111c5167b0b5";
            const string documentName = "Faktura (PEPPOL)";
            const string rootName = "Invoice";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-Invoice-2.1.xsd";

            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/InvoiceHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/InvoicePeppol4aInterface/SubmitInvoiceRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/InvoicePeppol4aInterface/SubmitInvoiceResponse";
            const string serviceContractTModel = "uddi:ea27f0c4-cb68-4e39-b0eb-5c96af8c1759";
            const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            string expectedResult = "urn:www.cenbii.eu:transaction:biitrns010:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            List<PrefixedNamespace> prefixedNamespaceCollection = this.GetUblNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-invoice4a/XSLT/BIIRULES-UBL-T10.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-invoice4a/XSLT/OPENPEPPOLCORE-UBL-T10.xsl"));
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_Peppol("Resources/Schematrons/PEPPOL/BIS2.0-invoice4a/XSLT/OPENPEPPOL-UBL-T10.xsl"));

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigPeppol(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, schematronValidationConfigCollection, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath, ids, prefixedNamespaceCollection);
            return documentTypeConfig;
        }


        /* NemKonto*/

        /// <summary>
        /// The NKS Betalings Ordre
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNKSPayment()
        {
            const string id = "4873431b-fa84-4579-b789-2e3162d06038";
            const string documentName = "NKS Betalings Ordre";
            const string rootName = "NKSPayment";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2006/05/01/";
            const string xsdPath = "Resources/NemKonto/xsd/nemkonto/xml/schemas/2006/05/01/NKS_NKSPayment.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "//ebms:To/ebms:PartyId[count(../ebms:PartyId)]";
            const string destinationFriendlyNameXPath = "//ebms:To/ebms:PartyId[1]";
            const string senderKeyXPath = "//ebms:From/ebms:PartyId[count(../ebms:PartyId)]";
            const string senderFriendlyNameXPath = "//ebms:From/ebms:PartyId[1]";
            const string profileIdXPathStr = "string('NKS2.0')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSPaymentOrderInterface/SubmitNKSPaymentOrderRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSPaymentOrderInterface/SubmitNKSPaymentOrderResponse";
            const string serviceContractTModel = "uddi:f283bd90-a247-11dc-a80b-bfc65441a808";
            const string documentIdXPath = "";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);

            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSNamespaces();
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /// <summary>
        /// The NKS Kvittering 0
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNKSReceipt0()
        {
            const string id = "0e765453-7422-4d90-b13f-850fc0e5b309";
            const string documentName = "NKS Kvittering 0";
            const string rootName = "NKSReceipt0";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2006/05/01/";
            const string xsdPath = "Resources/NemKonto/xsd/nemkonto/xml/schemas/2006/05/01/NKS_NKSReceipt0.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "//ebms:To/ebms:PartyId[count(../ebms:PartyId)]";
            const string destinationFriendlyNameXPath = "//ebms:To/ebms:PartyId[1]";
            const string senderKeyXPath = "//ebms:From/ebms:PartyId[count(../ebms:PartyId)]";
            const string senderFriendlyNameXPath = "//ebms:From/ebms:PartyId[1]";
            const string profileIdXPathStr = "string('NKS2.0')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSReceipt0Interface/SubmitNKSReceipt0Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSReceipt0Interface/SubmitNKSReceipt0Response";
            const string serviceContractTModel = "uddi:9910d270-a242-11dc-a80a-bfc65441a808";
            const string documentIdXPath = "";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('EAN')";//"/root:NKSReceipt0/root:MessageHeader/@ebms:id";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);
            ////KeyTypeMapping destMapping = new KeyTypeMapping("Kvitteringssvar 0 NKS2C", "ean");
            ////destinationMappingExpression.AddMapping(destMapping);

            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('EAN')";//"/root:NKSReceipt0/root:MessageHeader/@ebms:id";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);
            ////KeyTypeMapping sendMapping = new KeyTypeMapping("Kvitteringssvar 0 NKS2C", "ean");
            ////senderMappingExpression.AddMapping(sendMapping);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSNamespaces();
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /// <summary>
        /// The NKS Kvittering 1
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNKSReceipt1()
        {
            const string id = "363237e6-dafc-4103-901a-36b499d0eeaa";
            const string documentName = "NKS Kvittering 1";
            const string rootName = "NKSReceipt1";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2006/05/01/";
            const string xsdPath = "Resources/NemKonto/xsd/nemkonto/xml/schemas/2006/05/01/NKS_NKSReceipt1.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "//ebms:To/ebms:PartyId[count(../ebms:PartyId)]";
            const string destinationFriendlyNameXPath = "//ebms:To/ebms:PartyId[1]";
            const string senderKeyXPath = "//ebms:From/ebms:PartyId[count(../ebms:PartyId)]";
            const string senderFriendlyNameXPath = "//ebms:From/ebms:PartyId[1]";
            const string profileIdXPathStr = "string('NKS2.0')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSReceipt1Interface/SubmitNKSReceipt1Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSReceipt1Interface/SubmitNKSReceipt1Response";
            const string serviceContractTModel = "uddi:8df531b0-a242-11dc-a80a-bfc65441a808";
            const string documentIdXPath = "";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('EAN')";//"/root:NKSReceipt1/root:MessageHeader/@ebms:id";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);
            ////KeyTypeMapping destMapping = new KeyTypeMapping("Kvitteringssvar 1 NKS2C", "ean");
            ////destinationMappingExpression.AddMapping(destMapping);

            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('EAN')";// "/root:NKSReceipt1/root:MessageHeader/@ebms:id";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);
            ////KeyTypeMapping sendMapping = new KeyTypeMapping("Kvitteringssvar 1 NKS2C", "ean");
            ////senderMappingExpression.AddMapping(sendMapping);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSNamespaces();
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /// <summary>
        /// The NKS Retursvar 2
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNKSResponse2()
        {
            const string id = "688df9d0-e564-44a1-8028-617ab625a724";
            const string documentName = "NKS Retursvar 2";
            const string rootName = "NKSResponse2";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2006/05/01/";
            const string xsdPath = "Resources/NemKonto/xsd/nemkonto/xml/schemas/2006/05/01/NKS_NKSResponse2.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "//ebms:To/ebms:PartyId[count(../ebms:PartyId)]";
            const string destinationFriendlyNameXPath = "//ebms:To/ebms:PartyId[1]";
            const string senderKeyXPath = "//ebms:From/ebms:PartyId[count(../ebms:PartyId)]";
            const string senderFriendlyNameXPath = "//ebms:From/ebms:PartyId[1]";
            const string profileIdXPathStr = "string('NKS2.0')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse2Interface/SubmitNKSResponse2Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse2Interface/SubmitNKSResponse2Response";
            const string serviceContractTModel = "uddi:41a37720-a244-11dc-a80a-bfc65441a808";
            const string documentIdXPath = "";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);
            ////KeyTypeMapping destMapping = new KeyTypeMapping("Retursvar 2 NKS2C", "ean");
            ////destinationMappingExpression.AddMapping(destMapping);

            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);
            ////KeyTypeMapping sendMapping = new KeyTypeMapping("Retursvar 2 NKS2C", "ean");
            ////senderMappingExpression.AddMapping(sendMapping);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSNamespaces();
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /// <summary>
        /// The NKS Retursvar 5
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNKSResponse5()
        {
            const string id = "3d2b9c5b-5790-4aee-999b-c26a1d66a887";
            const string documentName = "NKS Retursvar 5";
            const string rootName = "NKSResponse5";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2006/05/01/";
            const string xsdPath = "Resources/NemKonto/xsd/nemkonto/xml/schemas/2006/05/01/NKS_NKSResponse5.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "//ebms:To/ebms:PartyId[count(../ebms:PartyId)]";
            const string destinationFriendlyNameXPath = "//ebms:To/ebms:PartyId[1]";
            const string senderKeyXPath = "//ebms:From/ebms:PartyId[count(../ebms:PartyId)]";
            const string senderFriendlyNameXPath = "//ebms:From/ebms:PartyId[1]";
            const string profileIdXPathStr = "string('NKS2.0')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse5Interface/SubmitNKSResponse5Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse5Interface/SubmitNKSResponse5Response";
            const string serviceContractTModel = "uddi:65ee1590-a244-11dc-a80a-bfc65441a808";
            const string documentIdXPath = "";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);
            ////KeyTypeMapping destMapping = new KeyTypeMapping("Retursvar 5 NKS2C", "ean");
            ////destinationMappingExpression.AddMapping(destMapping);

            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);
            ////KeyTypeMapping sendMapping = new KeyTypeMapping("Retursvar 5 NKS2C", "ean");
            ////senderMappingExpression.AddMapping(sendMapping);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSNamespaces();
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /// <summary>
        /// The NKS Retursvar 7
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNKSResponse7()
        {
            const string id = "2cd298b8-eb8c-4f06-b7e8-6562cb54bc4e";
            const string documentName = "NKS Retursvar 7";
            const string rootName = "NKSResponse7";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2006/05/01/";
            const string xsdPath = "Resources/NemKonto/xsd/nemkonto/xml/schemas/2006/05/01/NKS_NKSResponse7.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "/root:NKSResponse7/root:GnlInf/swift:InitgPty/swift:OrgId/swift:PrtryId/swift:Id";
            const string destinationFriendlyNameXPath = "/root:NKSResponse7/root:GnlInf/swift:InitgPty/swift:OrgId/swift:PrtryId/swift:Issr";
            const string senderKeyXPath = "/root:NKSResponse7/root:OrgnlGrpRefInfAndSts/swift:GrpId";
            const string senderFriendlyNameXPath = "/root:NKSResponse7/root:OrgnlGrpRefInfAndSts/swift:OrgnlMsgTp";
            const string profileIdXPathStr = "string('NKS2.0')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse7Interface/SubmitNKSResponse7Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse7Interface/SubmitNKSResponse7Response";
            const string serviceContractTModel = "uddi:ec1ece70-a244-11dc-a80a-bfc65441a808";
            const string documentIdXPath = "";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);


            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSNamespaces();
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /// <summary>
        /// The NKS Retursvar 8
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNKSResponse8()
        {
            const string id = "6ba94fca-368d-49d0-9976-ae497c85eb37";
            const string documentName = "NKS Retursvar 8";
            const string rootName = "NKSResponse8";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2006/05/01/";
            const string xsdPath = "Resources/NemKonto/xsd/nemkonto/xml/schemas/2006/05/01/NKS_NKSResponse8.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "/root:NKSResponse8/root:GnlInf/swift:InitgPty/swift:OrgId/swift:PrtryId/swift:Id";
            const string destinationFriendlyNameXPath = "/root:NKSResponse8/root:GnlInf/swift:InitgPty/swift:OrgId/swift:PrtryId/swift:Issr";
            const string senderKeyXPath = "/root:NKSResponse8/root:OrgnlGrpRefInfAndSts/swift:GrpId";
            const string senderFriendlyNameXPath = "/root:NKSResponse8/root:OrgnlGrpRefInfAndSts/swift:OrgnlMsgTp";
            const string profileIdXPathStr = "string('NKS2.0')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse8Interface/SubmitNKSResponse8Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse8Interface/SubmitNKSResponse8Response";
            const string serviceContractTModel = "uddi:0bc62390-a245-11dc-a80a-bfc65441a808";
            const string documentIdXPath = "";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);


            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSNamespaces();
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /// <summary>
        /// The NKS Retursvar 9
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNKSResponse9()
        {
            const string id = "1e780d55-1779-46dc-b438-b023c5e91183";
            const string documentName = "NKS Retursvar 9";
            const string rootName = "NKSResponse9";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2006/05/01/";
            const string xsdPath = "Resources/NemKonto/xsd/nemkonto/xml/schemas/2006/05/01/NKS_NKSResponse9.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "/root:NKSResponse9/root:GnlInf/swift:InitgPty/swift:OrgId/swift:PrtryId/swift:Id";
            const string destinationFriendlyNameXPath = "/root:NKSResponse9/root:GnlInf/swift:InitgPty/swift:OrgId/swift:PrtryId/swift:Issr";
            const string senderKeyXPath = "/root:NKSResponse9/root:OrgnlGrpRefInfAndSts/swift:GrpId";
            const string senderFriendlyNameXPath = "/root:NKSResponse9/root:OrgnlGrpRefInfAndSts/swift:OrgnlMsgTp";
            const string profileIdXPathStr = "string('NKS2.0')";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse9Interface/SubmitNKSResponse9Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSResponse9Interface/SubmitNKSResponse9Response";
            const string serviceContractTModel = "uddi:2dd3ca50-a245-11dc-a80b-bfc65441a808";
            const string documentIdXPath = "";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);


            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSNamespaces();
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }

        /* NemKonto PU */

        /// <summary>
        /// The NemKonto PU Request
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNemKontoPURequest()
        {
            const string id = "76e3b26b-28a2-4cfd-bf67-27c5ee41014d";
            const string documentName = "NKS PU Request";
            const string rootName = "NemkontoPrivatUdbetalerTransporterRequest";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2007/10/01/";
            const string xsdPath = "";// "Resources/NemKonto/xsd/nemkonto-pu/xml/schemas/2007/10/01/NKSPU_Requester.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "/nkspu:" + rootName + "/nkspu:TransporterHeader/nkspu:VansHeader/nkspu:VansRecipientAddress/nkspu:VansNemkontoEnvironmentCode";
            const string destinationFriendlyNameXPath = "";//;"SimpleResult:DefaultValue=NemKonto";//"/root:" + rootName + "/root:NoneExixtingNode";
            const string senderKeyXPath = "/nkspu:" + rootName + "/nkspu:TransporterHeader/nkspu:VansHeader/nkspu:VansSenderAddress/ean:EAN13Identifier";
            const string senderFriendlyNameXPath = "";//"/root:" + rootName + "/root:NoneExixtingNode";
            const string profileIdXPathStr = "string('NKS-PU')";//"/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSPURequestInterface/SubmitNKSPURequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSPURequestInterface/SubmitNKSPUResponse";
            const string serviceContractTModel = "uddi:6b5fc8cb-5ec9-42bf-9a6f-1b3762f011ed";
            const string documentIdXPath = "";//"/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSPUNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            //schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('DK:VANS')";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);

            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSPUNamespaces();
            //namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }


        /// <summary>
        /// The NemKonto PU Response
        /// </summary>
        /// <returns>The document definition</returns>
        public virtual DocumentTypeConfig GetNemKontoPUResponse()
        {
            const string id = "1985c136-96df-4a04-add2-ca95b9de3371";
            const string documentName = "NKS PU Response";
            const string rootName = "NemkontoPrivatUdbetalerTransporterResponse";
            const string rootNamespace = "http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2007/10/01/";
            const string xsdPath = "";// "Resources/NemKonto/xsd/nemkonto-pu/xml/schemas/2007/10/01/NKSPU_NemkontoPrivatUdbetalerTransporterResponse.xsd";

            const string xslUIPath = "Resources/defaultss.xslt";
            const string destinationKeyXPath = "/nkspu:" + rootName + "/nkspu:TransporterHeader/nkspu:VansHeader/nkspu:VansRecipientAddress/ean:EAN13Identifier";
            const string destinationFriendlyNameXPath = "";//;"SimpleResult:DefaultValue=NemKonto";//"/root:" + rootName + "/root:NoneExixtingNode";
            const string senderKeyXPath = "/nkspu:" + rootName + "/nkspu:TransporterHeader/nkspu:VansHeader/nkspu:VansSenderAddress/nkspu:VansNemkontoEnvironmentCode";
            const string senderFriendlyNameXPath = "";//"/root:" + rootName + "/root:NoneExixtingNode";
            const string profileIdXPathStr = "string('NKS-PU')";//"/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSPUResponseInterface/SubmitNKSPUResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/NKSPUResponseInterface/SubmitNKSPUResponseResponse";
            const string serviceContractTModel = "uddi:34c56e2a-f920-439b-80c7-831cc4ac523b";
            const string documentIdXPath = "";//"/root:" + rootName + "/cbc:ID";

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            //string expectedResult = "urn:www.cenbii.eu:transaction:biitrns014:ver2.0:extended:urn:www.peppol.eu:bis:peppol5a:ver2.0";
            //string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
            //XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            //ids.Add(xPathDiscriminatorConfig);

            // more XPathDiscriminatorConfig ???
            //List<PrefixedNamespace> prefixedNamespaceCollection = this.GetNKSPUNamespaces();

            List<SchematronValidationConfig> schematronValidationConfigCollection = new List<SchematronValidationConfig>();
            //schematronValidationConfigCollection.Add(this.CreateSchematronValidationConfig_NKSPU("Resources/NemKonto/Schematrons/NemKonto.xsl"));

            // destination
            ServiceEndpointFriendlyName destinationFriendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey destinationKey = new ServiceEndpointKey(destinationKeyXPath);
            string destinationKeyTypeXpath = "string('EAN')";
            KeyTypeMappingExpression destinationMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", destinationKeyTypeXpath);
            destinationKey.AddMappingExpression(destinationMappingExpression);

            // sender
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = new ServiceEndpointKey(senderKeyXPath);
            string senderKeyTypeXpath = "string('DK:VANS')";
            KeyTypeMappingExpression senderMappingExpression = new KeyTypeMappingExpression("EndpointKeyType", senderKeyTypeXpath);
            senderKey.AddMappingExpression(senderMappingExpression);

            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, destinationFriendlyName, destinationKey, senderFriendlyName, senderKey);//, profileIdXPath);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, "", endpointInformation, ids, schematronValidationConfigCollection, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = this.GetNKSPUNamespaces();
            //namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentType.Namespaces = namespaces.ToArray();

            return documentType;
        }       
           
        ///// <summary>
        ///// Settings for UBL AttachedDocument 2.1
        ///// </summary>
        ///// <returns>The document definition</returns>
        ////public DocumentTypeConfig GetAttachedDocument()
        ////{
        ////    const string id = "0ec853ab-a4f0-47f3-8270-e487f79f4dc1";
        ////    const string documentName = "Indlejret dokument";
        ////    const string rootName = "AttachedDocument";
        ////    const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:AttachedDocument-2";
        ////    const string xsdPath = "Resources/Schemas/UBL_v2.1/maindoc/UBL-AttachedDocument-2.1.xsd";
        ////    const string xslPath = "Resources/Schematrons/OIOUBL/OIOUBL_AttachedDocument_Schematron.xsl";
        ////    const string xslUIPath = "";
        ////    const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
        ////    const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
        ////    const string senderKeyXPath = "/root:" + rootName + "/cac:SenderParty/cbc:EndpointID";
        ////    const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:SenderParty/cac:PartyName/cbc:Name";
        ////    const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
        ////    const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2014/09/15/AttachedDocument201Interface/SubmitAttachedDocumentRequest";
        ////    const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2014/09/15/AttachedDocument201Interface/SubmitAttachedDocumentResponse";
        ////    const string serviceContractTModel = "uddi:942f29ac-60dd-4b4d-b7d3-f5791737d084";
        ////    const string documentIdXPath = "/root:" + rootName + "/cbc:ID";

        //// const string SCHEMATRON_ERROR_XPATH = "/Schematron/Error";
        //// const string SCHEMATRON_ERRORMESSAGE_XPATH = "/Schematron/Error/Description";
        //// XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
        //// string expectedResult = "OIOUBL-2.0(1|2)";
        //// string xpathExpression = "/root:" + rootName + "/cbc:CustomizationID";
        //// XPathDiscriminatorConfig xPathDiscriminatorConfig = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
        //// ids.Add(xPathDiscriminatorConfig); // more XPathDiscriminatorConfig ???

        /// <summary>
        /// Get name-space
        /// </summary>
        /// <returns></returns>
        public virtual List<PrefixedNamespace> GetUblNamespaces()
        {
            List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>();
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", "cac"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", "cbc"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CoreComponentParameters-2", "ccts"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:SpecializedDatatypes-2", "sdt"));
            namespaces.Add(new PrefixedNamespace("urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2", "udt"));

            return namespaces;
        }

        /// <summary>
        /// Get name-space
        /// </summary>
        /// <returns></returns>
        public virtual List<PrefixedNamespace> GetNKSNamespaces()
        {
            List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>();
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/oes.dk/nemkonto.swift/xml/schemas/2006/05/01/", "swift"));
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/oes.dk/nemkonto.ebms/xml/schemas/2006/05/01/", "ebms"));
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2006/05/01/", "root"));
           
            return namespaces;
        }

        /// <summary>
        /// Get name-space
        /// </summary>
        /// <returns></returns>
        public virtual List<PrefixedNamespace> GetNKSPUNamespaces()
        {
            List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>();
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2007/10/01/", "nkspu"));
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/ean/xml/schemas/2005/01/10/", "ean"));
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/cvr.dk/xml/schemas/2005/03/22/", "cvr"));
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/cpr.dk/xml/schemas/core/2005/03/18/", "cpr"));
            ////namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/oes.dk/nemkonto/xml/schemas/2007/10/01/", "root"));
            ////namespaces.Add(new PrefixedNamespace("", "string"));
            ////namespaces.Add(new PrefixedNamespace("", ""));
           
            return namespaces;
        }
    }
}