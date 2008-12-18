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
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.xpath.discriminator;
using dk.gov.oiosi.xml.schematron;

namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// Defines all the default OIOXML/OIOUBL document type configurations
    /// </summary>
    public class DefaultDocumentTypes {

        private delegate DocumentTypeConfig GetDocumentType();

        /// <summary>
        /// Adds all the document types from configuration, clears collection first
        /// </summary>
        public void CleanAdd() {
            DocumentTypeCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            configuration.Clear();
            Add();
        }

        /// <summary>
        /// Adds all the document types
        /// </summary>
        public void Add() {
            Add(InvoiceV07);
            Add(CreditNoteV07);
            
            Add(ApplicationResponse);               // Applikationsmeddelelse
            Add(Catalogue);                         // Katalog
            Add(CatalogueRequest);                  // Katalogforespørgsel
            Add(CatalogueItemSpecificationUpdate);  // Opdatering af katalogelement
            Add(CataloguePricingUpdate);            // Opdatering af katalogpriser
            Add(CatalogueDeletion);                 // Sletning af katalog
            Add(CreditNote);                        // Kreditnota
            Add(Invoice);                           // Faktura
            Add(Order);                             // Ordre
            Add(OrderCancellation);                 // Ordreannulering
            Add(OrderResponse);                     // Ordrebekræftelse
            Add(OrderChange);                       // Ordreændring
            Add(OrderResponseSimple);               // Simpel ordrebekræftelse
            Add(Reminder);                          // Rykker
            Add(Statement);                         // KontoUdtog
        }

        /// <summary>
        /// The catalogue document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig Catalogue() {
            const string documentName = "Katalog";
            const string rootName = "Catalogue";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Catalogue-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-Catalogue-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_Catalogue_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:Catalogue/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Catalogue/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Catalogue/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Catalogue/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:Catalogue/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueResponse201Interface/SubmitCatalogueResponseRequest";
            const string serviceContractTModel = "uddi:b8a5a5d0-df9f-11dc-889a-1a827c218899";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The catalogue request document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig CatalogueRequest() {
            const string documentName = "Katalogforespørgsel";
            const string rootName = "CatalogueRequest";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CatalogueRequest-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-CatalogueRequest-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_CatalogueRequest_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:CatalogueRequest/cac:ProviderParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:CatalogueRequest/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:CatalogueRequest/cac:ReceiverParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:CatalogueRequest/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:CatalogueRequest/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueRequestResponse201Interface/SubmitCatalogueRequestResponseRequest";
            const string serviceContractTModel = "uddi:0cb0ff80-dfa0-11dc-889a-1a827c218899";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The statement document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig Statement() {
            const string documentName = "KontoUdtog";
            const string rootName = "Statement";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Statement-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-Statement-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_Statement_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_Statement.xsl";
            const string destinationKeyXPath = "/root:Statement/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Statement/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Statement/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Statement/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:Statement/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/StatementResponse201Interface/SubmitStatementResponseRequest";
            const string serviceContractTModel = "uddi:4e383840-bcfc-11dc-a81b-bfc65441a808";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The catalogue item specification update document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig CatalogueItemSpecificationUpdate() {
            const string documentName = "Opdatering af katalogelement";
            const string rootName = "CatalogueItemSpecificationUpdate";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CatalogueItemSpecificationUpdate-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-CatalogueItemSpecificationUpdate-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_CatalogueItemSpecificationUpdate_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:CatalogueItemSpecificationUpdate/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:CatalogueItemSpecificationUpdate/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:CatalogueItemSpecificationUpdate/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:CatalogueItemSpecificationUpdate/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:CatalogueItemSpecificationUpdate/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueItemSpecificationUpdateResponse201Interface/SubmitCatalogueItemSpecificationUpdateResponseRequest";
            const string serviceContractTModel = "uddi:63eab5c0-dfa0-11dc-889b-1a827c218899";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The catalogue pricing update document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig CataloguePricingUpdate() {
            const string documentName = "Opdatering af katalogpriser";
            const string rootName = "CataloguePricingUpdate";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CataloguePricingUpdate-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-CataloguePricingUpdate-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_CataloguePricingUpdate_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:CataloguePricingUpdate/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:CataloguePricingUpdate/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:CataloguePricingUpdate/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:CataloguePricingUpdate/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:CataloguePricingUpdate/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CataloguePricingUpdateResponse201Interface/SubmitCataloguePricingUpdateResponseRequest";
            const string serviceContractTModel = "uddi:abdb2720-dfa0-11dc-889b-1a827c218899";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The order cancellation document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig OrderCancellation() {
            const string documentName = "Ordreannulering";
            const string rootName = "OrderCancellation";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderCancellation-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-OrderCancellation-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_OrderCancellation_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_OrderCancellation.xsls";
            const string destinationKeyXPath = "/root:OrderCancellation/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:OrderCancellation/cac:Party/cac:BuyerCustomerParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:OrderCancellation/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:OrderCancellation/cac:SellerSupplierParty/cac:Party/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:OrderCancellation/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderCancellationResponse201Interface/SubmitOrderCancellationResponseRequest";
            const string serviceContractTModel = "uddi:7ba80590-dfa1-11dc-889b-1a827c218899";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The order response document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig OrderResponse() {
            const string documentName = "Ordrebekræftelse";
            const string rootName = "OrderResponse";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderResponse-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-OrderResponse-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_OrderResponse_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_OrderResponse.xsl";
            const string destinationKeyXPath = "/root:OrderResponse/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:OrderResponse/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:OrderResponse/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:OrderResponse/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:OrderResponse/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseResponse201Interface/SubmitOrderResponseResponseRequest";
            const string serviceContractTModel = "uddi:ed6d3c40-dfa1-11dc-889b-1a827c218899";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The order change document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig OrderChange() {
            const string documentName = "Ordreændring";
            const string rootName = "OrderChange";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderChange-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-OrderChange-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_OrderChange_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_OrderChange.xsl";
            const string destinationKeyXPath = "/root:OrderChange/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:OrderChange/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:OrderChange/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:OrderChange/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:OrderChange/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderChangeResponse201Interface/SubmitOrderChangeResponseRequest";
            const string serviceContractTModel = "uddi:ea4bc88f-9479-4f9b-a354-4acabdb99336";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The catalogue deletion definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig CatalogueDeletion() {
            const string documentName = "Sletning af katalog";
            const string rootName = "CatalogueDeletion";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CatalogueDeletion-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-CatalogueDeletion-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_CatalogueDeletion_Schematron.xsl";
            const string xslUIPath = "";
            const string destinationKeyXPath = "/root:CatalogueDeletion/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:CatalogueDeletion/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:CatalogueDeletion/cac:ProviderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:CatalogueDeletion/cac:ProviderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:CatalogueDeletion/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CatalogueDeletionResponse201Interface/SubmitCatalogueDeletionResponseRequest";
            const string serviceContractTModel = "uddi:40e5cbd0-dfa2-11dc-889b-1a827c218899";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The application response document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig ApplicationResponse() {
            const string documentName = "Applikationsmeddelse";
            const string rootName = "ApplicationResponse";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-ApplicationResponse-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_ApplicationResponse_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_ApplicationResponse.xsl";
            const string destinationKeyXPath = "/root:ApplicationResponse/cac:ReceiverParty/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:ApplicationResponse/cac:ReceiverParty/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:ApplicationResponse/cac:SenderParty/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:ApplicationResponse/cac:SenderParty/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:ApplicationResponse/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponse201Interface/SubmitApplicationResponseRequest";
            const string serviceContractTModel = "uddi:42F92342-C3ED-46ff-8A8A-6518F55D5CD5";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The credit note document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig CreditNote() {
            const string documentName = "Kreditnota";
            const string rootName = "CreditNote";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-CreditNote-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_CreditNote_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_ApplicationResponse.xsl";
            const string destinationKeyXPath = "//cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "//cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "//cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "//cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CreditNote201Interface/SubmitCreditNoteRequest";
            const string serviceContractTModel = "uddi:E4EC9613-4830-4bab-AFEE-C37AB1C67AEC";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// Settings for UBL Invoice 2.01
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig Invoice() {
            const string documentName = "Faktura";
            const string rootName = "Invoice";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-Invoice-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_Invoice_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_Invoice.xsl";
            const string destinationKeyXPath = "/root:Invoice/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Invoice/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice201Interface/SubmitInvoiceRequest";
            const string serviceContractTModel = "uddi:2e0b402a-7a5e-476b-8686-b33f54fd1f47";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The order document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig Order() {
            const string documentName = "Ordre";
            const string rootName = "Order";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Order-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-Order-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_Order_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_Order.xsl";
            const string destinationKeyXPath = "/root:Order/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Order/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Order/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Order/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Order201Interface/SubmitOrderRequest";
            const string serviceContractTModel = "uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The order response simple document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig OrderResponseSimple() {
            const string documentName = "Simpel ordrebekræftelse";
            const string rootName = "OrderResponseSimple";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:OrderResponseSimple-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-OrderResponseSimple-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_OrderResponseSimple_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_OrderResponseSimple.xsl";
            const string destinationKeyXPath = "/root:OrderResponseSimple/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:OrderResponseSimple/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:OrderResponseSimple/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:OrderResponseSimple/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseSimple201Interface/SubmitOrderResponseSimpleRequest";
            const string serviceContractTModel = "uddi:3B0B1309-B575-4d69-9C8F-4126C53CD7B0";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The reminder document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig Reminder() {
            const string documentName = "Rykker";
            const string rootName = "Reminder";
            const string rootNamespace = "urn:oasis:names:specification:ubl:schema:xsd:Reminder-2";
            const string xsdPath = "Resources/Schemas/OIOUBL v2.01/UBL-Reminder-2.0.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_Reminder_Schematron.xsl";
            const string xslUIPath = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_Reminder.xsl";
            const string destinationKeyXPath = "/root:Reminder/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            const string destinationFriendlyNameXPath = "/root:Reminder/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            const string senderKeyXPath = "/root:Reminder/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            const string senderFriendlyNameXPath = "/root:Reminder/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            const string profileIdXPathStr = "/root:" + rootName + "/cbc:ProfileID";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Reminder201Interface/SubmitReminderRequest";
            const string serviceContractTModel = "uddi:4FBBBDEF-0A8E-4d5e-9B9D-23C8FD98E9CE";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV2(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, profileIdXPathStr, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The invoice 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig InvoiceV07() {
            const string documentName = "Faktura v0.7";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pie/";
            const string xsdPath = "Resources/Schemas/OIOXML v0.7/piestrict.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOXML v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:Invoice/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:Invoice/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:Invoice/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:Invoice/com:SellerParty/com:PartyName/com:Name";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07Interface/SubmitInvoice07Request";
            const string serviceContractTModel = "uddi:bc99bb01-80f9-4f52-89dc-edf7732c56f9";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }

        /// <summary>
        /// The credit note 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig CreditNoteV07() {
            const string documentName = "Kreditnota v0.7";
            const string rootName = "Invoice";
            const string rootNamespace = "http://rep.oio.dk/ubl/xml/schemas/0p71/pcm/";
            const string xsdPath = "Resources/Schemas/OIOXML v0.7/pcmstrict.xsd";
            const string xslPath = "Resources/SchematronStylesheets/OIOXML v0.7/ublinvoice.xsl";
            const string xslUIPath = "Resources/UI/OIOXML v0.7/StyleSheets/html.xsl";
            const string destinationKeyXPath = "/root:Invoice/com:BuyersReferenceID";
            const string destinationFriendlyNameXPath = "/root:Invoice/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string senderKeyXPath = "/root:Invoice/com:SellerParty/com:ID";
            const string senderFriendlyNameXPath = "/root:Invoice/com:SellerParty/com:PartyName/com:Name";
            const string documentEndpointRequestAction = "http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07Interface/SubmitCreditNote07Request";
            const string serviceContractTModel = "uddi:3bbc9cf0-3c4c-11dc-98be-6976502198bd";

            DocumentTypeConfig documentTypeConfig = GetDocumentTypeConfigOioublV07(destinationFriendlyNameXPath, destinationKeyXPath, senderFriendlyNameXPath, senderKeyXPath, documentEndpointRequestAction, rootName, xslPath, documentName, rootNamespace, xsdPath, xslUIPath, serviceContractTModel);
            return documentTypeConfig;
        }


        private DocumentTypeConfig GetDocumentTypeConfigOioublV2(string destinationFriendlyNameXPath, string destinationKeyXPath, string senderFriendlyNameXPath, string senderKeyXPath, string profileIdXPathStr, string documentEndpointRequestAction, string rootName, string xslPath, string documentName, string rootNamespace, string xsdPath, string xslUIPath, string serviceContractTModel) {

            const string OIOUBL_SCHEMATRON_ERROR_XPATH = "/Schematron/Error";
            const string OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH = "/Schematron/Error/Description";
            
            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey key = CreateKey(destinationKeyXPath);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = CreateKey(senderKeyXPath);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(profileIdXPathStr);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            XPathDiscriminatorConfig id = GetCustomizationIdOoiubl2_01(rootName);
            ids.Add(id);

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, OIOUBL_SCHEMATRON_ERROR_XPATH, OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(documentName, rootName, rootNamespace, xsdPath, xslUIPath, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);
            List<PrefixedNamespace> namespaces = GetUblNamespaces();
            namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = serviceContractTModel;
            return documentType;
        }

        private DocumentTypeConfig GetDocumentTypeConfigOioublV07(string destinationFriendlyNameXPath, string destinationKeyXPath, string senderFriendlyNameXPath, string senderKeyXPath, string documentEndpointRequestAction, string rootName, string xslPath, string documentName, string rootNamespace, string xsdPath, string xslUIPath, string serviceContractTModel) {
            
            const string OIOXML_SCHEMATRON_ERROR_XPATH = "/schematron/error";
            const string OIOXML_SCHEMATRON_ERRORMESSAGE_XPATH = "/schematron/error";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(destinationFriendlyNameXPath);
            ServiceEndpointKey key = CreateKey(destinationKeyXPath);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(senderFriendlyNameXPath);
            ServiceEndpointKey senderKey = CreateKey(senderKeyXPath);
            const ProfileIdXPath profileIdXPath = null;

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation(documentEndpointRequestAction, "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(xslPath, OIOXML_SCHEMATRON_ERROR_XPATH, OIOXML_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(documentName, rootName, rootNamespace, xsdPath, xslUIPath, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);
            List<PrefixedNamespace> namespaces = GetOioxmlNamespaces();
            namespaces.Add(new PrefixedNamespace(rootNamespace, "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = serviceContractTModel;
            return documentType;
        }

        private XPathDiscriminatorConfig GetCustomizationIdOoiubl2_01(string root) {
            string expectedResult = "OIOUBL-2.01";
            string xpathExpression = "/root:"+root+"/cbc:CustomizationID";
            XPathDiscriminatorConfig id = new XPathDiscriminatorConfig(xpathExpression, expectedResult);
            return id;
        }

        private List<PrefixedNamespace> GetOioxmlNamespaces() {
            List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>();
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/ubl/xml/schemas/0p71/common/", "com"));
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/ubl/xml/schemas/0p71/maindoc/", "main"));
            return namespaces;
        }

        private List<PrefixedNamespace> GetUblNamespaces() {
            List<PrefixedNamespace> namespaces = new List<PrefixedNamespace>();
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", "cac"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", "cbc"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:CoreComponentParameters-2", "ccts"));
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:SpecializedDatatypes-2", "sdt"));
            namespaces.Add(new PrefixedNamespace("urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2", "udt"));
            return namespaces;
        }

        private ServiceEndpointKey CreateKey(string xpath) {
            ServiceEndpointKey key = new ServiceEndpointKey(xpath);
            string keyTypeXpath = xpath + "/@schemeID";
            KeyTypeMappingExpression mappingExpression = new KeyTypeMappingExpression("EndpointKeyType", keyTypeXpath);
            KeyTypeMapping deprecatedEanMapping = new KeyTypeMapping("ean", "ean");
            KeyTypeMapping uppercasedDeprecatedEanMapping = new KeyTypeMapping("EAN", "ean");
            KeyTypeMapping deprecatedCvrMapping = new KeyTypeMapping("CVR", "cvr");
            KeyTypeMapping eanMapping = new KeyTypeMapping("GLN", "ean");
            KeyTypeMapping cvrMapping = new KeyTypeMapping("DK:CVR", "cvr");
            KeyTypeMapping ovtMapping = new KeyTypeMapping("ISO 6523", "ovt");
            KeyTypeMapping pMapping = new KeyTypeMapping("DK:P", "p");
            KeyTypeMapping seMapping = new KeyTypeMapping("DK:SE", "se");
            KeyTypeMapping vansMapping = new KeyTypeMapping("DK:VANS", "vans");
            KeyTypeMapping ibanMapping = new KeyTypeMapping("IBAN", "iban");
            KeyTypeMapping dunsMapping = new KeyTypeMapping("DUNS", "duns");
            mappingExpression.AddMapping(deprecatedEanMapping);
            mappingExpression.AddMapping(uppercasedDeprecatedEanMapping);
            mappingExpression.AddMapping(deprecatedCvrMapping);
            mappingExpression.AddMapping(eanMapping);
            mappingExpression.AddMapping(cvrMapping);
            mappingExpression.AddMapping(ovtMapping);
            mappingExpression.AddMapping(pMapping);
            mappingExpression.AddMapping(seMapping);
            mappingExpression.AddMapping(vansMapping);
            mappingExpression.AddMapping(ibanMapping);
            mappingExpression.AddMapping(dunsMapping);
            key.AddMappingExpression(mappingExpression);
            return key;
        }

        /// <summary>
        /// Adds a document type definition to the collection
        /// </summary>
        /// <param name="getDocumentType"></param>
        private void Add(GetDocumentType getDocumentType) {
            DocumentTypeConfig documentType = getDocumentType();
            DocumentTypeCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            if (!configuration.ContainsDocumentTypeByValue(documentType))
                configuration.AddDocumentType(documentType);
        }

    }
}
