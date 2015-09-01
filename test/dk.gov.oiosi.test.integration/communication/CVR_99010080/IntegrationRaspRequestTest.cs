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

namespace dk.gov.oiosi.test.integration.communication.CVR_99010080
{

    [TestFixture]
    public class IntegrationRaspRequestTest : AbstractIntegrationRaspRequestTest
    {

        [TestFixtureSetUp]
        public void Setup()
        {
            CertificateLoader loader = new CertificateLoader();
            this.ClientCertificate =  loader.GetCertificateFromStoreWithSerialNumber("4C 8C F7 64", StoreLocation.CurrentUser, StoreName.My);

             //CertificateUtil.InstallAndGetOces2FunctionCertificateFromCertificateStore();
            ConfigurationUtil.SetupConfiguration("Resources/RaspConfiguration.Live.xml");
        }

        [Test]
        public void OioublApplicationResponse201MustBeSendableByRaspRequest()
        {
            AssertSendable("Resources/Documents/CVR_99010080/OIOUBL_Invoice_v2p1.xml");
        }       
    }
}