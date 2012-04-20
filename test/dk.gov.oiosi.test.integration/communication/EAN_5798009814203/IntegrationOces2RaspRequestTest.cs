using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.raspProfile.communication;
using dk.gov.oiosi.security;
using dk.gov.oiosi.security.ldap;
using dk.gov.oiosi.security.oces;
using dk.gov.oiosi.security.revocation;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.xml.xpath;
using NUnit.Framework;
using dk.gov.oiosi.security.lookup;

namespace dk.gov.oiosi.test.integration.communication.EAN_5798009814203
{

    [TestFixture]
    public class IntegrationOces2RaspRequestTest : AbstractIntegrationRaspRequestTest
    {

        [TestFixtureSetUp]
        public void Setup()
        {
            this.ClientCertificate = CertificateUtil.InstallAndGetOces2FunctionCertificateFromCertificateStore();
            ConfigurationUtil.SetupConfiguration("Resources/RaspConfiguration.Test.xml");
        }

        [Test]
        public void OioublApplicationResponse201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_ApplicationResponse_v2p1.xml");
        }

        [Test]
        public void OioublApplicationResponse202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_ApplicationResponse_v2p2.xml");
        }

        [Test]
        public void OioublCatalogue202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_Catalogue_v2p2.xml");
        }

        [Test]
        public void OioublCatalogueDeletion201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CatalogueDeletion_v2p1.xml");
        }

        [Test]
        public void OioublCatalogueDeletion202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CatalogueDeletion_v2p2.xml");
        }

        [Test]
        public void OioublCatalogueItemSpecificationUpdate201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CatalogueItemSpecificationUpdate_v2p1.xml");
        }

        [Test]
        public void OioublCatalogueItemSpecificationUpdate202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CatalogueItemSpecificationUpdate_v2p2.xml");
        }

        [Test]
        public void OioublCataloguePricingUpdate201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CataloguePricingUpdate_v2p1.xml");
        }

        [Test]
        public void OioublCataloguePricingUpdate202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CataloguePricingUpdate_v2p2.xml");
        }

        [Test]
        public void OioublCatalogueRequest201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CatalogueRequest_v2p1.xml");
        }

        [Test]
        public void OioublCatalogueRequest202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CatalogueRequest_v2p2.xml");
        }

        [Test]
        public void OioublCreditNote201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CreditNote_v2p1.xml");
        }

        [Test]
        public void OioublCreditNote202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_CreditNote_v2p2.xml");
        }

        [Test]
        public void OioublInvoice201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_Invoice_v2p1.xml");
        }

        [Test]
        public void OioublInvoice202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_Invoice_v2p2.xml");
        }

        [Test]
        public void OioublOrder201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_Order_v2p1.xml");
        }

        [Test]
        public void OioublOrder202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_Order_v2p2.xml");
        }

        [Test]
        public void OioublOrderCancellation201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_OrderCancellation_v2p1.xml");
        }

        [Test]
        public void OioublOrderCancellation202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_OrderCancellation_v2p2.xml");
        }

        [Test]
        public void OioublOrderChange201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_OrderChange_v2p1.xml");
        }

        [Test]
        public void OioublOrderChange202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_OrderChange_v2p2.xml");
        }

        [Test]
        public void OioublOrderResponseSimple201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_OrderResponseSimple_v2p1.xml");
        }

        [Test]
        public void OioublOrderResponseSimple202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_OrderResponseSimple_v2p2.xml");
        }

        [Test]
        public void OioublOrderResponse201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_OrderResponse_v2p1.xml");
        }

        [Test]
        public void OioublOrderResponse202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_OrderResponse_v2p2.xml");
        }

        [Test]
        public void OioublReminder201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_Reminder_v2p1.xml");
        }

        [Test]
        public void OioublReminder202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_Reminder_v2p2.xml");
        }

        [Test]
        public void OioublStatement201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_Statement_v2p1.xml");
        }

        [Test]
        public void OioublStatement202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_Statement_v2p2.xml");
        }

        [Test]
        public void OioublUtilityStatement202MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOUBL_UtilityStatement_v2p2.xml");
        }


        [Test]
        public void OioxmlCreditNoteMustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOXML_CreditNote_v0.7.xml");
        }

        [Test]
        public void OioxmlInvoiceMustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOXML_Invoice_v0.7.xml");
        }

        [Test]
        public void OioxmlInvoiceCPRSenderMustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/EAN_5798009814203/OIOXML_Invoice_v0.7_CPR_Sender.xml");
        }
    }
}