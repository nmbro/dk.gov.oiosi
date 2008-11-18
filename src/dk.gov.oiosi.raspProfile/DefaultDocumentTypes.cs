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

using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.xml.xpath.discriminator;
using dk.gov.oiosi.xml.schematron;

namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// Defines all the default OIOXML/OIOUBL document type configurations
    /// </summary>
    public class DefaultDocumentTypes {
        /// <summary>
        /// OIOXML schematron error xpath
        /// </summary>
        public const string OIOXML_SCHEMATRON_ERROR_XPATH = "/schematron/error";

        /// <summary>
        /// OIOXML schematron errormessage xpath
        /// </summary>
        public const string OIOXML_SCHEMATRON_ERRORMESSAGE_XPATH = "/schematron/error";

        /// <summary>
        /// OIOUBL schematron error xpath
        /// </summary>
        public const string OIOUBL_SCHEMATRON_ERROR_XPATH = "/Schematron/Error";

        /// <summary>
        /// OIOUBL schematron errormessage xpath
        /// </summary>
        public const string OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH = "/Schematron/Error/Description";

        /// <summary>
        /// Default request action
        /// </summary>
        public const string OIO_REQUEST_ACTION = "http://rep.oio.dk/oiosi/IMessageHandler/RequestRespondRequest/";

        /// <summary>
        /// Default response action
        /// </summary>
        public const string OIO_RESPONSE_ACTION = "http://rep.oio.dk/oiosi/IMessageHandler/";

        private delegate DocumentTypeConfig GetDocumentType();

        /// <summary>
        /// Adds all the document types
        /// </summary>
        public void Add() {
            Add(new GetDocumentType(ApplicationResponse));
            Add(new GetDocumentType(CreditNote));
            Add(new GetDocumentType(Invoice));
            Add(new GetDocumentType(Order));
            Add(new GetDocumentType(OrderResponseSimple));
            Add(new GetDocumentType(Reminder));
            Add(new GetDocumentType(InvoiceV07));
            Add(new GetDocumentType(CreditNoteV07));
        }

        /// <summary>
        /// Adds all the document types from configuration, clears collection first
        /// </summary>
        public void CleanAdd() {
            DocumentTypeCollectionConfig configuration = ConfigurationHandler.GetConfigurationSection<DocumentTypeCollectionConfig>();
            configuration.Clear();
            Add();
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

        /// <summary>
        /// The application response document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig ApplicationResponse() {
            string NAME = "Applikationsmeddelse";
            string ROOTNAME = "ApplicationResponse";
            string ROOTNAMESPACE = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2";
            string PATH_APPLICATIONRESPONSE_XSD = "Resources/Schemas/OIOUBL v2.01/UBL-ApplicationResponse-2.0.xsd";
            string PATH_APPLICATIONRESPONSE_XSL = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_ApplicationResponse_Schematron.xsl";
            string PATH_APPLICATIONRESPONSE_XSL_UI = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_ApplicationResponse.xsl";
            string XPATH_APPLICATIONRESPONSE_DESTINATION_KEY = "/root:ApplicationResponse/cac:ReceiverParty/cbc:EndpointID";
            string XPATH_APPLICATIONRESPONSE_DESTINATION_FRIENDLYNAME = "/root:ApplicationResponse/cac:ReceiverParty/cac:PartyName/cbc:Name";
            string XPATH_APPLICATIONRESPONSE_SENDER_KEY = "/root:ApplicationResponse/cac:SenderParty/cbc:EndpointID";
            string XPATH_APPLICATIONRESPONSE_SENDER_FRIENDLYNAME = "/root:ApplicationResponse/cac:SenderParty/cac:PartyName/cbc:Name";
            string XPATH_APPLICATIONRESPONSE_PROFILEID = "/root:ApplicationResponse/cbc:ProfileID";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(XPATH_APPLICATIONRESPONSE_DESTINATION_FRIENDLYNAME);
            ServiceEndpointKey key = CreateKey(XPATH_APPLICATIONRESPONSE_DESTINATION_KEY);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(XPATH_APPLICATIONRESPONSE_SENDER_FRIENDLYNAME);
            ServiceEndpointKey senderKey = CreateKey(XPATH_APPLICATIONRESPONSE_SENDER_KEY);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(XPATH_APPLICATIONRESPONSE_PROFILEID);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation("http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/ApplicationResponse201Interface/SubmitApplicationResponseRequest", "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            XPathDiscriminatorConfig id = GetCustomizationIdOoiubl2_01(ROOTNAME);
            ids.Add(id);

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(PATH_APPLICATIONRESPONSE_XSL, OIOUBL_SCHEMATRON_ERROR_XPATH, OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(NAME, ROOTNAME, ROOTNAMESPACE, PATH_APPLICATIONRESPONSE_XSD, PATH_APPLICATIONRESPONSE_XSL_UI, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);
            List<PrefixedNamespace> namespaces = GetUblNamespaces();
            namespaces.Add(new PrefixedNamespace(ROOTNAMESPACE, "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = "uddi:42F92342-C3ED-46ff-8A8A-6518F55D5CD5";

            return documentType;
        }

        /// <summary>
        /// The credit note document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig CreditNote() {
            string NAME = "Kreditnota";
            string ROOTNAME = "CreditNote";
            string ROOTNAMESPACE = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2";
            string PATH_CREDITNOTE_XSD = "Resources/Schemas/OIOUBL v2.01/UBL-CreditNote-2.0.xsd";
            string PATH_CREDITNOTE_XSL = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_CreditNote_Schematron.xsl";
            string PATH_CREDITNOTE_XSL_UI = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_CreditNote.xsl";
            string XPATH_CREDITNOTE_DESTINATION_KEY = "//cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            string XPATH_CREDITNOTE_DESTINATION_FRIENDLYNAME = "//cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_CREDITNOTE_SENDER_KEY = "//cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            string XPATH_CREDITNOTE_SENDER_FRIENDLYNAME = "//cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_CREDITNOTE_PROFILEID = "/root:"+ROOTNAME+"/cbc:ProfileID";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(XPATH_CREDITNOTE_DESTINATION_FRIENDLYNAME);
            ServiceEndpointKey key = CreateKey(XPATH_CREDITNOTE_DESTINATION_KEY);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(XPATH_CREDITNOTE_SENDER_FRIENDLYNAME);
            ServiceEndpointKey senderKey = CreateKey(XPATH_CREDITNOTE_SENDER_KEY);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(XPATH_CREDITNOTE_PROFILEID);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation("http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/CreditNote201Interface/SubmitCreditNoteRequest", "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            XPathDiscriminatorConfig id = GetCustomizationIdOoiubl2_01(ROOTNAME);
            ids.Add(id);

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(PATH_CREDITNOTE_XSL, OIOUBL_SCHEMATRON_ERROR_XPATH, OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(NAME, ROOTNAME, ROOTNAMESPACE, PATH_CREDITNOTE_XSD, PATH_CREDITNOTE_XSL_UI, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);
            List<PrefixedNamespace> namespaces = GetUblNamespaces();
            namespaces.Add(new PrefixedNamespace(ROOTNAMESPACE, "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = "uddi:E4EC9613-4830-4bab-AFEE-C37AB1C67AEC";

            return documentType;
        }

        /// <summary>
        /// Settings for UBL Invoice 2.01
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig Invoice() {
            string NAME = "Faktura";
            string ROOTNAME = "Invoice";
            string ROOTNAMESPACE = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            string PATH_INVOICE_XSD = "Resources/Schemas/OIOUBL v2.01/UBL-Invoice-2.0.xsd";
            string PATH_INVOICE_XSL = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_Invoice_Schematron.xsl";
            string PATH_INVOICE_XSL_UI = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_Invoice.xsl";
            string XPATH_INVOICE_DESTINATION_KEY = "/root:Invoice/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            string XPATH_INVOICE_DESTINATION_FRIENDLYNAME = "/root:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_INVOICE_SENDER_KEY = "/root:Invoice/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            string XPATH_INVOICE_SENDER_FRIENDLYNAME = "/root:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_INVOICE_PROFILEID = "/root:" + ROOTNAME + "/cbc:ProfileID";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(XPATH_INVOICE_DESTINATION_FRIENDLYNAME);
            ServiceEndpointKey key = CreateKey(XPATH_INVOICE_DESTINATION_KEY);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(XPATH_INVOICE_SENDER_FRIENDLYNAME);
            ServiceEndpointKey senderKey = CreateKey(XPATH_INVOICE_SENDER_KEY);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(XPATH_INVOICE_PROFILEID);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation("http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice201Interface/SubmitInvoiceRequest", "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            XPathDiscriminatorConfig id = GetCustomizationIdOoiubl2_01(ROOTNAME);
            ids.Add(id);

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(PATH_INVOICE_XSL, OIOUBL_SCHEMATRON_ERROR_XPATH, OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(NAME, ROOTNAME, ROOTNAMESPACE, PATH_INVOICE_XSD, PATH_INVOICE_XSL_UI, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);
            List<PrefixedNamespace> namespaces = GetUblNamespaces();
            namespaces.Add(new PrefixedNamespace(ROOTNAMESPACE, "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = "uddi:2e0b402a-7a5e-476b-8686-b33f54fd1f47";

            return documentType;
        }

        /// <summary>
        /// The order document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig Order() {
            string NAME = "Ordre";
            string ROOTNAME = "Order";
            string ROOTNAMESPACE = "urn:oasis:names:specification:ubl:schema:xsd:Order-2";
            string PATH_ORDER_XSD = "Resources/Schemas/OIOUBL v2.01/UBL-Order-2.0.xsd";
            string PATH_ORDER_XSL = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_Order_Schematron.xsl";
            string PATH_ORDER_XSL_UI = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_Order.xsl";
            string XPATH_ORDER_DESTINATION_KEY = "/root:Order/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            string XPATH_ORDER_DESTINATION_FRIENDLYNAME = "/root:Order/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_ORDER_SENDER_KEY = "/root:Order/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            string XPATH_ORDER_SENDER_FRIENDLYNAME = "/root:Order/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_ORDER_PROFILEID = "/root:" + ROOTNAME + "/cbc:ProfileID";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(XPATH_ORDER_DESTINATION_FRIENDLYNAME);
            ServiceEndpointKey key = CreateKey(XPATH_ORDER_DESTINATION_KEY);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(XPATH_ORDER_SENDER_FRIENDLYNAME);
            ServiceEndpointKey senderKey = CreateKey(XPATH_ORDER_SENDER_KEY);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(XPATH_ORDER_PROFILEID);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation("http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Order201Interface/SubmitOrderRequest", "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            XPathDiscriminatorConfig id = GetCustomizationIdOoiubl2_01(ROOTNAME);
            ids.Add(id);

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(PATH_ORDER_XSL, OIOUBL_SCHEMATRON_ERROR_XPATH, OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(NAME, ROOTNAME, "urn:oasis:names:specification:ubl:schema:xsd:Order-2", PATH_ORDER_XSD, PATH_ORDER_XSL_UI, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);
            List<PrefixedNamespace> namespaces = GetUblNamespaces();
            namespaces.Add(new PrefixedNamespace(ROOTNAMESPACE, "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = "uddi:b138dc71-d301-42d1-8c2e-2c3a26faf56a";
            
            return documentType;
        }

        /// <summary>
        /// The order response simple document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig OrderResponseSimple() {
            string NAME = "Simpel ordrebekræftelse";
            string ROOTNAME = "OrderResponseSimple";
            string ROOTNAMESPACE = "urn:oasis:names:specification:ubl:schema:xsd:OrderResponseSimple-2";
            string PATH_ORDERRESPONSESIMPLE_XSD = "Resources/Schemas/OIOUBL v2.01/UBL-OrderResponseSimple-2.0.xsd";
            string PATH_ORDERRESPONSESIMPLE_XSL = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_OrderResponseSimple_Schematron.xsl";
            string PATH_ORDERRESPONSESIMPLE_XSL_UI = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_OrderResponseSimple.xsl";
            string XPATH_ORDERRESPONSESIMPLE_DESTINATION_KEY = "/root:OrderResponseSimple/cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            string XPATH_ORDERRESPONSESIMPLE_DESTINATION_FRIENDLYNAME = "/root:OrderResponseSimple/cac:BuyerCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_ORDERRESPONSESIMPLE_SENDER_KEY = "/root:OrderResponseSimple/cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            string XPATH_ORDERRESPONSESIMPLE_SENDER_FRIENDLYNAME = "/root:OrderResponseSimple/cac:SellerSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_ORDERRESPONSESIMPLE_PROFILEID = "/root:" + ROOTNAME + "/cbc:ProfileID";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(XPATH_ORDERRESPONSESIMPLE_DESTINATION_FRIENDLYNAME);
            ServiceEndpointKey key = CreateKey(XPATH_ORDERRESPONSESIMPLE_DESTINATION_KEY);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(XPATH_ORDERRESPONSESIMPLE_SENDER_FRIENDLYNAME);
            ServiceEndpointKey senderKey = CreateKey(XPATH_ORDERRESPONSESIMPLE_SENDER_KEY);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(XPATH_ORDERRESPONSESIMPLE_PROFILEID);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation("http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/OrderResponseSimple201Interface/SubmitOrderResponseSimpleRequest", "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            XPathDiscriminatorConfig id = GetCustomizationIdOoiubl2_01(ROOTNAME);
            ids.Add(id);

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(PATH_ORDERRESPONSESIMPLE_XSL, OIOUBL_SCHEMATRON_ERROR_XPATH, OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(NAME, ROOTNAME, ROOTNAMESPACE, PATH_ORDERRESPONSESIMPLE_XSD, PATH_ORDERRESPONSESIMPLE_XSL_UI, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);
            List<PrefixedNamespace> namespaces = GetUblNamespaces();
            namespaces.Add(new PrefixedNamespace("urn:oasis:names:specification:ubl:schema:xsd:OrderResponseSimple-2", "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = "uddi:3B0B1309-B575-4d69-9C8F-4126C53CD7B0";

            return documentType;
        }

        /// <summary>
        /// The reminder document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig Reminder() {
            string NAME = "Rykker";
            string ROOTNAME = "Reminder";
            string ROOTNAMESPACE = "urn:oasis:names:specification:ubl:schema:xsd:Reminder-2";
            string PATH_REMINDER_XSD = "Resources/Schemas/OIOUBL v2.01/UBL-Reminder-2.0.xsd";
            string PATH_REMINDER_XSL = "Resources/SchematronStylesheets/OIOUBL v2.01/OIOUBL_Reminder_Schematron.xsl";
            string PATH_REMINDER_XSL_UI = "Resources/UI/OIOUBL v2.01/Stylesheets/OIOUBL_Reminder.xsl";
            string XPATH_REMINDER_DESTINATION_KEY = "/root:Reminder/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            string XPATH_REMINDER_DESTINATION_FRIENDLYNAME = "/root:Reminder/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_REMINDER_SENDER_KEY = "/root:Reminder/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            string XPATH_REMINDER_SENDER_FRIENDLYNAME = "/root:Reminder/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name";
            string XPATH_REMINDER_PROFILEID = "/root:" + ROOTNAME + "/cbc:ProfileID";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(XPATH_REMINDER_DESTINATION_FRIENDLYNAME);
            ServiceEndpointKey key = CreateKey(XPATH_REMINDER_DESTINATION_KEY);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(XPATH_REMINDER_SENDER_FRIENDLYNAME);
            ServiceEndpointKey senderKey = CreateKey(XPATH_REMINDER_SENDER_KEY);
            ProfileIdXPath profileIdXPath = new ProfileIdXPath(XPATH_REMINDER_PROFILEID);

            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation("http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Reminder201Interface/SubmitReminderRequest", "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);

            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();
            XPathDiscriminatorConfig id = GetCustomizationIdOoiubl2_01(ROOTNAME);
            ids.Add(id);

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(PATH_REMINDER_XSL, OIOUBL_SCHEMATRON_ERROR_XPATH, OIOUBL_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(NAME, ROOTNAME, ROOTNAMESPACE, PATH_REMINDER_XSD, PATH_REMINDER_XSL_UI, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);
            List<PrefixedNamespace> namespaces = GetUblNamespaces();
            namespaces.Add(new PrefixedNamespace(ROOTNAMESPACE, "root"));
            documentType.Namespaces = namespaces.ToArray();
            documentType.ServiceContractTModel = "uddi:4FBBBDEF-0A8E-4d5e-9B9D-23C8FD98E9CE";

            return documentType;
        }

        /// <summary>
        /// The invoice 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig InvoiceV07() {
            const string NAME = "Faktura v0.7";
            const string ROOTNAME = "Invoice";
            const string ROOTNAMESPACE = "http://rep.oio.dk/ubl/xml/schemas/0p71/pie/";
            const string PATH_INVOICE_XSD = "Resources/Schemas/OIOXML v0.7/piestrict.xsd";
            const string PATH_INVOICE_XSL = "Resources/SchematronStylesheets/OIOXML v0.7/ublinvoice.xsl";
            const string PATH_INVOICE_XSL_UI = "Resources/UI/OIOXML v0.7/StyleSheets/html.xsl";
            const string XPATH_INVOICE_DESTINATION_KEY = "/root:Invoice/com:BuyersReferenceID";
            const string XPATH_INVOICE_DESTINATION_FRIENDLYNAME = "/root:Invoice/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string XPATH_INVOICE_SENDER_KEY = "/root:Invoice/com:SellerParty/com:ID";
            const string XPATH_INVOICE_SENDER_FRIENDLYNAME = "/root:Invoice/com:SellerParty/com:PartyName/com:Name";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(XPATH_INVOICE_DESTINATION_FRIENDLYNAME);
            ServiceEndpointKey key = CreateKey(XPATH_INVOICE_DESTINATION_KEY);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(XPATH_INVOICE_SENDER_FRIENDLYNAME);
            ServiceEndpointKey senderKey = CreateKey(XPATH_INVOICE_SENDER_KEY);
            ProfileIdXPath profileIdXPath = null;
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation("http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Invoice07Interface/SubmitInvoice07Request", "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);
            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(PATH_INVOICE_XSL, OIOXML_SCHEMATRON_ERROR_XPATH, OIOXML_SCHEMATRON_ERRORMESSAGE_XPATH);

            DocumentTypeConfig documentType = new DocumentTypeConfig(NAME, ROOTNAME, ROOTNAMESPACE, PATH_INVOICE_XSD, PATH_INVOICE_XSL_UI, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);

            List<PrefixedNamespace> namespaces = GetOioxmlNamespaces();
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/ubl/xml/schemas/0p71/pie/", "root"));
            documentType.Namespaces = namespaces.ToArray();

            documentType.ServiceContractTModel = "uddi:bc99bb01-80f9-4f52-89dc-edf7732c56f9";

            return documentType;
        }

        /// <summary>
        /// The credit note 0.7 document definition
        /// </summary>
        /// <returns>The document definition</returns>
        public DocumentTypeConfig CreditNoteV07() {
            const string NAME = "Kreditnota v0.7";
            const string ROOTNAME = "Invoice";
            const string ROOTNAMESPACE = "http://rep.oio.dk/ubl/xml/schemas/0p71/pcm/";
            const string PATH_INVOICE_XSD = "Resources/Schemas/OIOXML v0.7/pcmstrict.xsd";
            const string PATH_INVOICE_XSL = "Resources/SchematronStylesheets/OIOXML v0.7/ublinvoice.xsl";
            const string PATH_INVOICE_XSL_UI = "Resources/UI/OIOXML v0.7/StyleSheets/html.xsl";
            const string XPATH_INVOICE_DESTINATION_KEY = "/root:Invoice/com:BuyersReferenceID";
            const string XPATH_INVOICE_DESTINATION_FRIENDLYNAME = "/root:Invoice/com:BuyerParty/com:PartyName[count(../../com:BuyerParty)=1 or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='faktura' or translate(../com:Address/com:ID, 'FAKTUREING', 'faktureing') ='fakturering']/com:Name";
            const string XPATH_INVOICE_SENDER_KEY = "/root:Invoice/com:SellerParty/com:ID";
            const string XPATH_INVOICE_SENDER_FRIENDLYNAME = "/root:Invoice/com:SellerParty/com:PartyName/com:Name";

            ServiceEndpointFriendlyName friendlyName = new ServiceEndpointFriendlyName(XPATH_INVOICE_DESTINATION_FRIENDLYNAME);
            ServiceEndpointKey key = CreateKey(XPATH_INVOICE_DESTINATION_KEY);
            ServiceEndpointFriendlyName senderFriendlyName = new ServiceEndpointFriendlyName(XPATH_INVOICE_SENDER_FRIENDLYNAME);
            ServiceEndpointKey senderKey = CreateKey(XPATH_INVOICE_SENDER_KEY);
            ProfileIdXPath profileIdXPath = null;
            DocumentEndpointInformation endpointInformation = new DocumentEndpointInformation("http://rep.oio.dk/oiosi.ehandel.gov.dk/xml/schemas/2007/09/01/Creditnote07Interface/SubmitCreditNote07Request", "*", friendlyName, key, senderFriendlyName, senderKey, profileIdXPath);
            XpathDiscriminatorConfigCollection ids = new XpathDiscriminatorConfigCollection();

            SchematronValidationConfig schematronValidationConfig = new SchematronValidationConfig(PATH_INVOICE_XSL, OIOXML_SCHEMATRON_ERROR_XPATH, OIOXML_SCHEMATRON_ERRORMESSAGE_XPATH);
            DocumentTypeConfig documentType = new DocumentTypeConfig(NAME, ROOTNAME, ROOTNAMESPACE, PATH_INVOICE_XSD, PATH_INVOICE_XSL_UI, "", "", endpointInformation, ids, schematronValidationConfig, profileIdXPath);

            List<PrefixedNamespace> namespaces = GetOioxmlNamespaces();
            namespaces.Add(new PrefixedNamespace("http://rep.oio.dk/ubl/xml/schemas/0p71/pcm/", "root"));
            documentType.Namespaces = namespaces.ToArray();

            documentType.ServiceContractTModel = "uddi:3bbc9cf0-3c4c-11dc-98be-6976502198bd";
            
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
            mappingExpression.AddMapping(deprecatedEanMapping);
            mappingExpression.AddMapping(uppercasedDeprecatedEanMapping);
            mappingExpression.AddMapping(deprecatedCvrMapping);
            mappingExpression.AddMapping(eanMapping);
            mappingExpression.AddMapping(cvrMapping);
            mappingExpression.AddMapping(ovtMapping);
            key.AddMappingExpression(mappingExpression);
            return key;
        }

    }
}
