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
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.xpath.discriminator;
using dk.gov.oiosi.xml.schematron;

namespace dk.gov.oiosi.raspProfile
{

    /// <summary>
    /// Defines all the default OIOXML/OIOUBL document type configurations
    /// </summary>
    public class DefaultDocumentTypes
    {

        private delegate DocumentTypeConfig DocumentTypeConfigDelegate();

        /// <summary>
        /// Adds all the document types from configuration, clears collection first
        /// </summary>
        public void CleanAdd()
        {
            DocumentTypeCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            configuration.Clear();
            Add();
        }

        /// <summary>
        /// Adds all the document types
        /// </summary>
        public void Add()
        {
            this.Add(this.GetInvoiceV07);
            this.Add(this.GetCreditNoteV07);

            // Læs ind
            //this.Add(this.GetScanInvoiceV07);
            //this.Add(this.GetScanCreditNoteV07);

            this.Add(this.GetApplicationResponse);               // Applikationsmeddelelse
            this.Add(this.GetCatalogue);                         // Katalog
            this.Add(this.GetCatalogueRequest);                  // Katalogforespørgsel
            this.Add(this.GetCatalogueItemSpecificationUpdate);  // Opdatering af katalogelement
            this.Add(this.GetCataloguePricingUpdate);            // Opdatering af katalogpriser
            this.Add(this.GetCatalogueDeletion);                 // Sletning af katalog
            this.Add(this.GetCreditNote);                        // Kreditnota
            this.Add(this.GetInvoice);                           // Faktura
            this.Add(this.GetOrder);                             // Ordre
            this.Add(this.GetOrderCancellation);                 // Ordreannulering
            this.Add(this.GetOrderResponse);                     // Ordrebekræftelse
            this.Add(this.GetOrderChange);                       // Ordreændring
            this.Add(this.GetOrderResponseSimple);               // Simpel ordrebekræftelse
            this.Add(this.GetReminder);                          // Rykker
            this.Add(this.GetStatement);                         // KontoUdtog
            this.Add(this.GetUtilityStatement);                  // Forsynings specifikation
        }

        /// <summary>
        /// The application response document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetApplicationResponse()
        {
            const string id = "40c9fbee-ad39-48ed-9e04-c28cbbf8a38c";
            const string documentName = "Applikationsmeddelse";
            const string rootName = "ApplicationResponse";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-ApplicationResponse-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_ApplicationResponse_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/ApplicationResponseHTML.xsl";
            const string destinationKeyXPath = "/root:ApplicationResponse/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:ApplicationResponse/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:ApplicationResponse/cac:SenderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:ApplicationResponse/cac:SenderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:ApplicationResponse/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponse201Interface/SubmitApplicationResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponse201Interface/SubmitApplicationResponseResponse";
            const string serviceContractTModel = "uddi:42F92342-C3ED-46ff-8A8A-6518F55D5CD5";
            const string documentIdXPath = "/root:ApplicationResponse/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The catalogue document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetCatalogue()
        {
            const string id = "68db0c6f-ec2c-44ad-b1c3-bdfae65ee5f0";
            const string documentName = "Katalog";
            const string rootName = "Catalogue";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Catalogue-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Catalogue-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_Catalogue_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:Catalogue/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Catalogue/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Catalogue/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Catalogue/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:Catalogue/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueResponse201Interface/SubmitCatalogueResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueResponse201Interface/SubmitCatalogueResponseRequestResponse";
            const string serviceContractTModel = "uddi:b8a5a5d0-df9f-11dc-889a-1a827c218899";
            const string documentIdXPath = "/root:Catalogue/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The catalogue deletion definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetCatalogueDeletion()
        {
            const string id = "0efa1c48-b5e3-4eb8-bc5f-3fbd78daba10";
            const string documentName = "Sletning af katalog";
            const string rootName = "CatalogueDeletion";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CatalogueDeletion-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CatalogueDeletion-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_CatalogueDeletion_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:CatalogueDeletion/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:CatalogueDeletion/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:CatalogueDeletion/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:CatalogueDeletion/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:CatalogueDeletion/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueDeletionResponse201Interface/SubmitCatalogueDeletionResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueDeletionResponse201Interface/SubmitCatalogueDeletionResponseResponse";
            const string serviceContractTModel = "uddi:40e5cbd0-dfa2-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:CatalogueDeletion/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }    

        /// <summary>
        /// The catalogue item specification update document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetCatalogueItemSpecificationUpdate()
        {
            const string id = "fb3034ea-eaaf-434b-8798-0433db497e66";
            const string documentName = "Opdatering af katalogelement";
            const string rootName = "CatalogueItemSpecificationUpdate";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CatalogueItemSpecificationUpdate-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CatalogueItemSpecificationUpdate-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_CatalogueItemSpecificationUpdate_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:CatalogueItemSpecificationUpdate/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:CatalogueItemSpecificationUpdate/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:CatalogueItemSpecificationUpdate/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:CatalogueItemSpecificationUpdate/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:CatalogueItemSpecificationUpdate/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueItemSpecificationUpdateResponse201Interface/SubmitCatalogueItemSpecificationUpdateResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueItemSpecificationUpdateResponse201Interface/SubmitCatalogueItemSpecificationUpdateResponseResponse";
            const string serviceContractTModel = "uddi:63eab5c0-dfa0-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:CatalogueItemSpecificationUpdate/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        
        /// <summary>
        /// The catalogue pricing update document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetCataloguePricingUpdate()
        {
            const string id = "02092e85-c2e4-4bb2-b22d-3eec04007a36";
            const string documentName = "Opdatering af katalogpriser";
            const string rootName = "CataloguePricingUpdate";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CataloguePricingUpdate-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CataloguePricingUpdate-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_CataloguePricingUpdate_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:CataloguePricingUpdate/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:CataloguePricingUpdate/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:CataloguePricingUpdate/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:CataloguePricingUpdate/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:CataloguePricingUpdate/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CataloguePricingUpdateResponse201Interface/SubmitCataloguePricingUpdateResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CataloguePricingUpdateResponse201Interface/SubmitCataloguePricingUpdateResponseResponse";
            const string serviceContractTModel = "uddi:abdb2720-dfa0-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:CataloguePricingUpdate/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The catalogue request document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetCatalogueRequest()
        {
            const string id = "463984d1-4ba5-44d6-8903-565cc56dd4cb";
            const string documentName = "Katalogforespørgsel";
            const string rootName = "CatalogueRequest";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CatalogueRequest-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CatalogueRequest-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_CatalogueRequest_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:CatalogueRequest/cac:ProviderParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:CatalogueRequest/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:CatalogueRequest/cac:ReceiverParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:CatalogueRequest/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:CatalogueRequest/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueRequestResponse201Interface/SubmitCatalogueRequestResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueRequestResponse201Interface/SubmitCatalogueRequestResponseResponse";
            const string serviceContractTModel = "uddi:0cb0ff80-dfa0-11dc-889a-1a827c218899";
            const string documentIdXPath = "/root:CatalogueRequest/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }


        /// <summary>
        /// The credit note document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetCreditNote()
        {
            const string id = "a25f2c30-cb5b-404d-886a-9030621f7eea";
            const string documentName = "Kreditnota";
            const string rootName = "CreditNote";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-CreditNote-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_CreditNote_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/CreditNoteHTML.xsl";
            const string destinationKeyXPath = "/root:CreditNote/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:CreditNote/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CreditNote201Interface/SubmitCreditNoteRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CreditNote201Interface/SubmitCreditNoteResponse";
            const string serviceContractTModel = "uddi:E4EC9613-4830-4bab-AFEE-C37AB1C67AEC";
            const string documentIdXPath = "/root:CreditNote/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// Settings for UBL Invoice 2.01
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetInvoice()
        {
            const string id = "c0220657-c101-4d7d-9670-c9463e1559d5";
            const string documentName = "Faktura";
            const string rootName = "Invoice";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Invoice-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_Invoice_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/InvoiceHTML.xsl";
            const string destinationKeyXPath = "/root:Invoice/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Invoice/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice201Interface/SubmitInvoiceRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice201Interface/SubmitInvoiceResponse";
            const string serviceContractTModel = "uddi:2e0b402a-7a5e-476b-8686-b33f54fd1f47";
            const string documentIdXPath = "/root:Invoice/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The order document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetOrder()
        {
            const string id = "5b84d1f6-f315-4a2c-a84d-095b10cc5a2c";
            const string documentName = "Ordre";
            const string rootName = "Order";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Order-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Order-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_Order_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderHTML.xsl";
            const string destinationKeyXPath = "/root:Order/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Order/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Order/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Order/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Order201Interface/SubmitOrderRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Order201Interface/SubmitOrderResponse";
            const string serviceContractTModel = "uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a";
            const string documentIdXPath = "/root:Order/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The order cancellation document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetOrderCancellation()
        {
            const string id = "c4a09991-d038-4e51-bb06-2cdffe6c1b77";
            const string documentName = "Ordreannulering";
            const string rootName = "OrderCancellation";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderCancellation-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-OrderCancellation-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_OrderCancellation_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderCancellationHTML.xsl";
            const string destinationKeyXPath = "/root:OrderCancellation/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:OrderCancellation/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:OrderCancellation/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:OrderCancellation/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:OrderCancellation/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderCancellationResponse201Interface/SubmitOrderCancellationResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderCancellationResponse201Interface/SubmitOrderCancellationResponseResponse";
            const string serviceContractTModel = "uddi:7ba80590-dfa1-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:OrderCancellation/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The order change document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetOrderChange()
        {
            const string id = "0412fdc2-5f07-4e6f-a8fd-c0dc7d780dce";
            const string documentName = "Ordreændring";
            const string rootName = "OrderChange";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderChange-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-OrderChange-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_OrderChange_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderChangeHTML.xsl";
            const string destinationKeyXPath = "/root:OrderChange/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:OrderChange/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:OrderChange/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:OrderChange/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:OrderChange/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderChangeResponse201Interface/SubmitOrderChangeResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderChangeResponse201Interface/SubmitOrderChangeResponseResponse";
            const string serviceContractTModel = "uddi:ea4bc88f-9479-4f9b-a354-4acabdb99336";
            const string documentIdXPath = "/root:OrderChange/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }


        /// <summary>
        /// The order response document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetOrderResponse()
        {
            const string id = "ba652e7d-e8bd-4926-8bd8-9e19a5ca23e6";
            const string documentName = "Ordrebekræftelse";
            const string rootName = "OrderResponse";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderResponse-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-OrderResponse-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_OrderResponse_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderResponseHTML.xsl";
            const string destinationKeyXPath = "/root:OrderResponse/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:OrderResponse/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:OrderResponse/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:OrderResponse/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:OrderResponse/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseResponse201Interface/SubmitOrderResponseResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseResponse201Interface/SubmitOrderResponseResponseResponse";
            const string serviceContractTModel = "uddi:ed6d3c40-dfa1-11dc-889b-1a827c218899";
            const string documentIdXPath = "/root:OrderResponse/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The order response simple document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetOrderResponseSimple()
        {
            const string id = "c8577e35-7de6-49f6-926c-c061f5a7d1b6";
            const string documentName = "Simpel ordrebekræftelse";
            const string rootName = "OrderResponseSimple";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderResponseSimple-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-OrderResponseSimple-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_OrderResponseSimple_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/OrderResponseSimpleHTML.xsl";
            const string destinationKeyXPath = "/root:OrderResponseSimple/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:OrderResponseSimple/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:OrderResponseSimple/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:OrderResponseSimple/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseSimple201Interface/SubmitOrderResponseSimpleRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseSimple201Interface/SubmitOrderResponseSimpleResponse";
            const string serviceContractTModel = "uddi:3B0B1309-B575-4d69-9C8F-4126C53CD7B0";
            const string documentIdXPath = "/root:OrderResponseSimple/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The reminder document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetReminder()
        {
            const string id = "b552710b-e4c2-44f6-a89e-1b158375b5f3";
            const string documentName = "Rykker";
            const string rootName = "Reminder";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Reminder-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Reminder-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_Reminder_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/ReminderHTML.xsl";
            const string destinationKeyXPath = "/root:Reminder/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Reminder/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Reminder/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Reminder/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Reminder201Interface/SubmitReminderRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Reminder201Interface/SubmitReminderResponse";
            const string serviceContractTModel = "uddi:4FBBBDEF-0A8E-4d5e-9B9D-23C8FD98E9CE";
            const string documentIdXPath = "/root:Reminder/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The statement document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetStatement()
        {
            const string id = "76c4f0fa-e969-4360-9a04-8de3c675d4f2";
            const string documentName = "KontoUdtog";
            const string rootName = "Statement";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Statement-2";
            const string xsdPath = "Resources/Schemas/UBL_v2.0/maindoc/UBL-Statement-2.0.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_Statement_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/StatementHTML.xsl";
            const string destinationKeyXPath = "/root:Statement/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Statement/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Statement/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Statement/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:Statement/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/StatementResponse201Interface/SubmitStatementResponseRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/StatementResponse201Interface/SubmitStatementResponseResponse";
            const string serviceContractTModel = "uddi:4e383840-bcfc-11dc-a81b-bfc65441a808";
            const string documentIdXPath = "/root:Statement/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The utility statement document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetUtilityStatement()
        {
            const string id = "eee3da84-27b7-4b37-81b1-cfb9d2942a00";
            const string documentName = "Forsynings Specifikation";
            const string rootName = "UtilityStatement";
            const string rootNamespace = "urn:oioubl:names:specification:oioubl:schema:xsd:UtilityStatement-2";
            const string xsdPath = "Resources/Schemas/OIOUBL_v2.1-b/maindoc/UBL-UtilityStatement-2.1.xsd";
            const string xslPath = "Resources/Schematrons/OIOUBL_v2.0/OIOUBL_UtilityStatement_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL/Stylesheets/UtilityStatementHTML.xsl";
            const string destinationKeyXPath = "/root:" + rootName + "/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:" + rootName + "/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:" + rootName + "/cac:SenderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:" + rootName + "/cac:SenderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/UtilityStatement201Interface/SubmitUtilityStatementRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/UtilityStatement201Interface/SubmitUtilityStatementResponse";
            const string serviceContractTModel = "uddi:236f277d-a786-4724-a16e-26398b685a07";
            const string documentIdXPath = "/root:UtilityStatement/cbc:ID";

            DocumentTypeConfig documentTypeConfig = this.GetDocumentTypeConfigOioublV2(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);

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
        /// The invoice 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetInvoiceV07()
        {
            const string id = "5a15a880-eef8-40c0-80f2-bb65226f50c2";
            const string documentName = "Faktura v0.7";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pie/";
            const string xsdPath = "Resources/Schemas/OIOXML_v0.7/pieStrict.xsd";
            const string xslPath = "Resources/Schematrons/OIOXML_v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML_v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:Invoice/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:Invoice/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:Invoice/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:Invoice/com:SellerParty/com:PartyName/com:Name";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07Interface/SubmitInvoice07Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07Interface/SubmitInvoice07Response";
            const string serviceContractTModel = "uddi:bc99bb01-80f9-4f52-89dc-edf7732c56f9";
            const string documentIdXPath = "/root:Invoice/com:ID";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The scanned invoice 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetScanInvoiceV07()
        {
            const string id = "ef6f9602-7752-40a0-8b95-15440686c491";
            const string documentName = "Faktura v0.7 - Læs ind";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pip/";
            const string xsdPath = "Resources/Schemas/OIOXML_v0.7/pipStrict.xsd";
            const string xslPath = "Resources/Schematrons/OIOXML_v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML_v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:Invoice/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:Invoice/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:Invoice/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:Invoice/com:SellerParty/com:PartyName/com:Name";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07pipInterface/SubmitInvoice07pipRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07pipInterface/SubmitInvoice07pipResponse";
            const string serviceContractTModel = "uddi:bc99bb01-80f9-4f52-89dc-edf7732c56f9";
            const string documentIdXPath = "/root:Invoice/com:ID";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The credit note 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetCreditNoteV07()
        {
            const string id = "7bd520d7-6ae5-4a3c-8604-082e69414092";
            const string documentName = "Kreditnota v0.7";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pcm/";
            const string xsdPath = "Resources/Schemas/OIOXML_v0.7/pcmStrict.xsd";
            const string xslPath = "Resources/Schematrons/OIOXML_v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML_v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:Invoice/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:Invoice/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:Invoice/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:Invoice/com:SellerParty/com:PartyName/com:Name";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07Interface/SubmitCreditNote07Request";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07Interface/SubmitCreditNote07Response";
            const string serviceContractTModel = "uddi:3bbc9cf0-3c4c-11dc-98be-6976502198bd";
            const string documentIdXPath = "/root:Invoice/com:ID";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        /// <summary>
        /// The scanned credit note 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig GetScanCreditNoteV07()
        {
            const string id = "d430da7c-b4fd-4ac2-bd08-3ae77d680ffa";
            const string documentName = "Kreditnota v0.7 - Læs ind";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pcp/";
            const string xsdPath = "Resources/Schemas/OIOXML_v0.7/pcpStrict.xsd";
            const string xslPath = "Resources/Schematrons/OIOXML_v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML_v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:Invoice/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:Invoice/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:Invoice/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:Invoice/com:SellerParty/com:PartyName/com:Name";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07pcpInterface/SubmitCreditNote07pcpRequest";
            const string documentEndpointResponseAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07pcpInterface/SubmitCreditNote07pcpResponse";
            const string serviceContractTModel = "uddi:3bbc9cf0-3c4c-11dc-98be-6976502198bd";
            const string documentIdXPath = "/root:Invoice/com:ID";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(id, destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, documentEndpointRequestAction, documentEndpointResponseAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel, documentIdXPath);
            return documentTypeConfig;
        }

        private DocumentTypeConfig GetDocumentTypeConfigOioublV2(string id, string destinationFriendlyNameXPath, string destinationKeyXPath, string senderFriendlyNameXPath, string senderKeyXPath, string profileIdXPathStr, string documentEndpointRequestAction, string documentEndpointResponseAction, string rootName, string xslPath, string documentName, string rootNamespace, string xsdPath, string xslUIPath, string serviceContractTModel, string documentIdXPath)
        {

            const string OIOUBL_SCHEMATRON_ERROR_XPATH = "/Schematron/Error";
            const string OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH = "/Schematron/Error/Description";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey key = CreateKey(destinationKeyXPath);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = CreateSenderKey(senderKeyXPath);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            XPathDiscriminatorConfig xPathDiscriminatorConfig = GetCustomizationIdOoiubl2_01(rootName);
            ids.Add(xPathDiscriminatorConfig);

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, OIOUBL_SCHEMATRON_ERROR_XPATH, OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = GetUblNamespaces();
            namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = serviceContractTModel;
            return documentType;
        }

        private DocumentTypeConfig GetDocumentTypeConfigOioublV07(string id, string destinationFriendlyNameXPath, string destinationKeyXPath, string senderFriendlyNameXPath, string senderKeyXPath, string documentEndpointRequestAction, string documentEndpointResponseAction, string rootName, string xslPath, string documentName, string rootNamespace, string xsdPath, string xslUIPath, string serviceContractTModel, string documentIdXPath)
        {

            const string OIOXML_SCHEMATRON_ERROR_XPATH = "/schematron/error";
            const string OIOXML_SCHEMATRON_ERRORMESSAGE_XPATH = "/schematron/error";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey key = CreateKey(destinationKeyXPath);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = CreateSenderKey(senderKeyXPath);
            const ProfileIdXPath profileIdXPath = null;
            DocumentIdXPath docuIdXPath = new DocumentIdXPath(documentIdXPath);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, documentEndpointResponseAction, friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, OIOXML_SCHEMATRON_ERROR_XPATH, OIOXML_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(new Guid(id), documentName, rootName, rootNamespace, xsdPath, xslUIPath, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath, docuIdXPath);
            List<PrefixedNamespace> namespaces = GetOioxmlNamespaces();
            namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = serviceContractTModel;
            return documentType;
        }

        private XPathDiscriminatorConfig GetCustomizationIdOoiubl2_01(string root)
        {
            string expectedResult = "OIOUBL-2.0(1|2)";
            string xpathExpression = "/root:" + root + "/cbc:CustomizationID";
            XPathDiscriminatorConfig id = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            return id;
        }

        private List<PrefixedNamespace> GetOioxmlNamespaces()
        {
            List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>();
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/ubl/xml/schemas/0p71/common/", "com"));
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/ubl/xml/schemas/0p71/maindoc/", "main"));
            return namespaces;
        }

        private List<PrefixedNamespace> GetUblNamespaces()
        {
            List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>();
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", "cac"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", "cbc"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CoreComponentParameters-2", "ccts"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:SpecializedDatatypes-2", "sdt"));
            namespaces.Add(new PrefixedNamespace("urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2", "udt"));
            return namespaces;
        }

        private ServiceEndpointKey CreateSenderKey(string xpath)
        {
            // Why is there a differents between CreateSenderKey and CreateKey ???
            // It is not the configuration file job, to limith the use of NemHandel
            // If the xml document contain errors, the errors should be handled be schema or schematron validation

            ServiceEndpointKey key = CreateKey(xpath);

            return key;
        }

        private ServiceEndpointKey CreateKey(string xpath)
        {
            ServiceEndpointKey key = new ServiceEndpointKey(xpath);
            string keyTypeXpath = xpath + "/@schemeID";
            KeyTypeMappingExpression mappingExpression = new KeyTypeMappingExpression("EndpointKeyType", keyTypeXpath);

            KeyTypeMapping eanMapping = new KeyTypeMapping("ean", "ean");
            KeyTypeMapping uppercasedEanMapping = new KeyTypeMapping("EAN", "ean");
            KeyTypeMapping glnMapping = new KeyTypeMapping("GLN", "ean");

            KeyTypeMapping cvrMapping = new KeyTypeMapping("cvr", "cvr");
            KeyTypeMapping uppercasedCVRMapping = new KeyTypeMapping("CVR", "cvr");
            KeyTypeMapping dkcvrMapping = new KeyTypeMapping("DK:CVR", "cvr");

            KeyTypeMapping pMapping = new KeyTypeMapping("p", "p");
            KeyTypeMapping uppercasedPMapping = new KeyTypeMapping("P", "p");
            KeyTypeMapping dkpMapping = new KeyTypeMapping("DK:P", "p");

            KeyTypeMapping seMapping = new KeyTypeMapping("se", "se");
            KeyTypeMapping uppercasedSEMapping = new KeyTypeMapping("SE", "se");
            KeyTypeMapping dkseMapping = new KeyTypeMapping("DK:SE", "se");

            KeyTypeMapping cprMapping = new KeyTypeMapping("cpr", "cpr");
            KeyTypeMapping uppercasedCPRMapping = new KeyTypeMapping("CPR", "cpr");
            KeyTypeMapping dkcprMapping = new KeyTypeMapping("DK:CPR", "cpr");

            KeyTypeMapping ovtMapping = new KeyTypeMapping("ISO 6523", "ovt");

            KeyTypeMapping vansMapping = new KeyTypeMapping("VANS", "vans");

            KeyTypeMapping dkvansMapping = new KeyTypeMapping("DK:VANS", "vans");
            KeyTypeMapping ibanMapping = new KeyTypeMapping("IBAN", "iban");

            KeyTypeMapping dunsMapping = new KeyTypeMapping("DUNS", "duns");

            mappingExpression.AddMapping(eanMapping);
            mappingExpression.AddMapping(uppercasedEanMapping);
            mappingExpression.AddMapping(glnMapping);

            mappingExpression.AddMapping(cvrMapping);
            mappingExpression.AddMapping(uppercasedCVRMapping);
            mappingExpression.AddMapping(dkcvrMapping);

            mappingExpression.AddMapping(pMapping);
            mappingExpression.AddMapping(uppercasedPMapping);
            mappingExpression.AddMapping(dkpMapping);

            mappingExpression.AddMapping(seMapping);
            mappingExpression.AddMapping(uppercasedSEMapping);
            mappingExpression.AddMapping(dkseMapping);

            mappingExpression.AddMapping(cprMapping);
            mappingExpression.AddMapping(uppercasedCPRMapping);
            mappingExpression.AddMapping(dkcprMapping);

            mappingExpression.AddMapping(ovtMapping);

            mappingExpression.AddMapping(vansMapping);
            mappingExpression.AddMapping(dkvansMapping);

            mappingExpression.AddMapping(ibanMapping);
            mappingExpression.AddMapping(dunsMapping);

            key.AddMappingExpression(mappingExpression);

            return key;
        }

        /// <summary>
        /// Adds a document type definition to the collection
        /// </summary>
        /// <param name="documentTypeConfigDelegate"></param>
        private void Add(DocumentTypeConfigDelegate documentTypeConfigDelegate)
        {
            DocumentTypeConfig documentType = documentTypeConfigDelegate();
            DocumentTypeCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            if (!configuration.ContainsDocumentTypeByValue(documentType))
                configuration.AddDocumentType(documentType);
        }
    }
}
