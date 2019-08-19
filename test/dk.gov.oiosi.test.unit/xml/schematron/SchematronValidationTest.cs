using System;
using System.Diagnostics;
using System.Xml;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.raspProfile;
using dk.gov.oiosi.xml.schematron;
using NUnit.Framework;

namespace dk.gov.oiosi.test.unit.xml.schematron
{
    [TestFixture]
    public class SchematronValidationTest
    {
        private DefaultDocumentTypes _defaultDocumentTypes;

        [SetUp]
        public void InitDocumentTypes()
        {
            _defaultDocumentTypes = new DefaultDocumentTypes();
        }

        [Test]
        public void OioublApplicationResponse201Validation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblApplicationResponse();
            Validate(TestConstants.PATH_APPLICATIONRESPONSE201_XML, documentType);
        }

        [Test]
        public void OioublApplicationResponse202Validation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblApplicationResponse();
            Validate(TestConstants.PATH_APPLICATIONRESPONSE202_XML, documentType);
        }

        [Test]
        public void OioublCatalogueDeletionValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblCatalogueDeletion();
            Validate(TestConstants.PATH_CATALOGUEDELETION_XML, documentType);
        }

        [Test]
        public void OioublCatalogueItemSpecificationUpdateValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblCatalogueItemSpecificationUpdate();
            Validate(TestConstants.PATH_CATALOGUEITEMSPECIFICATIONUPDATE_XML, documentType);
        }

        [Test]
        public void OioublCataloguePricingUpdateValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblCataloguePricingUpdate();
            Validate(TestConstants.PATH_CATALOGUEPRICINGUPDATE_XML, documentType);
        }

        [Test]
        public void OioublCatalogueRequestValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblCatalogueRequest();
            Validate(TestConstants.PATH_CATALOGUEREQUEST_XML, documentType);
        }

        [Test]
        public void OioublCatalogueValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblCatalogue();
            Validate(TestConstants.PATH_CATALOGUE_XML, documentType);
        }

        [Test]
        public void OioublCreditNoteValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblCreditNote();
            Validate(TestConstants.PATH_CREDITNOTE_XML, documentType);
        }

        [Test]
        public void OioublInvoiceValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblInvoice();
            Validate(TestConstants.PATH_INVOICE_XML, documentType);
        }

        [Test]
        public void OioublNoIdInvoiceValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblInvoice();
            Assert.Throws<SchematronErrorException>(() => Validate(TestConstants.PATH_INVOICENOID_XML, documentType));
        }

        [Test]
        public void OioublOrderCancellationValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblOrderCancellation();
            Validate(TestConstants.PATH_ORDERCANCELLATION_XML, documentType);
        }

        [Test]
        public void OioublOrderChangeValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblOrderChange();
            Validate(TestConstants.PATH_ORDERCHANGE_XML, documentType);
        }

        [Test]
        public void OioublOrderResponseSimpleValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblOrderResponseSimple();
            Validate(TestConstants.PATH_ORDERRESPONSESIMPLE_XML, documentType);
        }

        [Test]
        public void OioublOrderResponseValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblOrderResponse();
            Validate(TestConstants.PATH_ORDERRESPONSE_XML, documentType);
        }

        [Test]
        public void OioublOrderValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblOrder();
            Validate(TestConstants.PATH_ORDER_XML, documentType);
        }

        [Test]
        public void OioublReminderValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblReminder();
            Validate(TestConstants.PATH_REMINDER_XML, documentType);
        }

        [Test]
        public void OioublStatementValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblStatement();
            Validate(TestConstants.PATH_STATEMENT_XML, documentType);
        }

        [Test]
        public void OioublUtilityStatementValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioUblUtilityStatement();
            Validate(TestConstants.PATH_UTILITYSTATEMENT_XML, documentType);
        }

        [Test]
        public void OioxmlInvalidEanNumberInvoiceValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioXmlInvoiceV07();
             Assert.Throws<SchematronErrorException>(()=> Validate(TestConstants.PATH_INVOICEN7INVALIDEANNUMBER_XML, documentType));
        }

        [Test]
        public void OioxmlInvoiceValidation()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetOioXmlInvoiceV07();
            Validate(TestConstants.PATH_INVOICE07_XML, documentType);
        }

        // Peppol
        ////[Test]
        ////public void Peppol36aApplicationResponse()
        ////{
        ////    DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol36aApplicationResponse();
        ////    Validate(TestConstants.PATH_PEPPOL_Peppol36aApplicationResponse_XML, documentType);
        ////}

        [Test]
        public void Peppol1aApplicationResponse()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol1aApplicationResponse();
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase1_CatalogueResponse_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase2_CatalogueResponse_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase3_CatalogueResponse_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase4_CatalogueResponse_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase5_CatalogueResponse_XML, documentType);
        }

        [Test]
        public void Peppol1aCatalogue()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol1aCatalogue();
            // official test doc - all invalid
            ////Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase1_Catalogue_XML, documentType);
            ////Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase2_Catalogue_XML, documentType);
            ////Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase3_Catalogue_XML, documentType);
            ////Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase4_Catalogue_XML, documentType);
            ////Validate(TestConstants.PATH_PEPPOL_Peppol1a_UseCase5_Catalogue_XML, documentType);

            Validate(TestConstants.PATH_PEPPOL_Peppol1a_Catalogue_XML, documentType);
        }

        [Test]
        public void Peppol30aDespatchAdvice()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol30aDespatchAdvice();
            //// warning
            Validate(TestConstants.PATH_PEPPOL_Peppol30a_UserCase1_DespatchAdvice_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol30a_UserCase2_DespatchAdvice_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol30a_UserCase3_DespatchAdvice_XML, documentType);

            //ValidateFailer(TestConstants.PATH_PEPPOL_Peppol30a_UserCase4_DespatchAdvice_XML, documentType);
           // ValidateFailer(TestConstants.PATH_PEPPOL_Peppol30a_UserCase5_DespatchAdvice_XML, documentType);
        }

        [Test]
        public void Peppol3aOrder()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol3aOrder();
            Validate(TestConstants.PATH_PEPPOL_Peppol3a_UserCase1_Order_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol3a_UserCase2_Order_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol3a_UserCase3_Order_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol3a_UserCase4_Order_XML, documentType);
        }

        [Test]
        public void Peppol4aInvoice()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol4aInvoice();
            //// warning
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase1a_Invoice_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase1b_Invoice_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase2_Invoice_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase3_Invoice_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase4_Invoice_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol4a_UserCase5_Invoice_XML, documentType);
        }

        [Test]
        public void Peppol5aCreditNote()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol5aCreditNote();
            //// warning
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase1a_CreditNote_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase1b_CreditNote_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase2_CreditNote_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase3_CreditNote_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase4_CreditNote_XML, documentType);
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_UserCase5_CreditNote_XML, documentType);
        }

        [Test]
        public void Peppol5aInvoice()
        {
            DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol5aInvoice();
            //// warning
            Validate(TestConstants.PATH_PEPPOL_Peppol5a_Invocie_XML, documentType);
        }

        ////[Test]
        ////public void Peppol28aOrder()
        ////{
        ////    DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol28aOrder();
        ////    Validate(TestConstants.PATH_PEPPOL_Peppol28aOrder_XML, documentType);
        ////}

        ////[Test]
        ////public void Peppol28aOrderResponse()
        ////{
        ////    DocumentTypeConfig documentType = _defaultDocumentTypes.GetPeppol28aOrderResponse();
        ////    Validate(TestConstants.PATH_PEPPOL_Peppol28aOrderResponse_XML, documentType);
        ////}

        public void ValidateFailer(string xmlDocumentPath, DocumentTypeConfig documentType)
        {
            try
            {
                Validate(TestConstants.PATH_PEPPOL_Peppol30a_UserCase4_DespatchAdvice_XML, documentType);
                Assert.Fail("Expected schematron error");
            }
            catch (SchematronErrorException)
            {
                // as expected
            }
            catch (Exception)
            {
                Assert.Fail("Wrong type of exception");
            }
        }

        private void Validate(string xmlDocumentPath, DocumentTypeConfig documentType)
        {
            Console.WriteLine("{0} Schematron validation started ", DateTime.Now);
            SchematronValidationConfig[] SchematronValidationConfig = documentType.SchematronValidationConfigs;
            XmlDocument document = new XmlDocument();
            Console.WriteLine("{0} Loading Xml Document '{1}'", DateTime.Now, xmlDocumentPath);
            document.Load(xmlDocumentPath);
            Console.WriteLine("{0} Instanciating the validator ", DateTime.Now);
            foreach (SchematronValidationConfig schematronValidationConfig in SchematronValidationConfig)
            {
                try
                {
                    SchematronValidator validator = new SchematronValidator(schematronValidationConfig);
                    Console.WriteLine("{0} Schematron validation", DateTime.Now);
                    validator.SchematronValidateXmlDocument(document);
                }
                catch (Exception ex)
                {
                    Debug.Fail("Something is wrong:" + ex.Message);
                    throw;
                }
            }

            Console.WriteLine("{0} Schematron validation completed", DateTime.Now);
        }
    }
}