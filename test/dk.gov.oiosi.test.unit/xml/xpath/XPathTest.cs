using System.Collections.Generic;
using System.Xml;
using dk.gov.oiosi.uddi;
using NUnit.Framework;

using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.common;
using dk.gov.oiosi.xml.documentType;
using dk.gov.oiosi.raspProfile;

namespace dk.gov.oiosi.test.unit.xml.xpath {
    [TestFixture]
    public class XPathTest {
        private DocumentTypeConfigSearcher _searcher;

        [TestFixtureSetUp]
        public void Start() {
            DefaultDocumentTypes documentTypes = new DefaultDocumentTypes();
            documentTypes.CleanAdd();
            _searcher = new DocumentTypeConfigSearcher();
        }

        [Test]
        public void IdentifierTest() {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary[TestConstants.PATH_APPLICATIONRESPONSE201_XML] = "5798009811578";
            dictionary[TestConstants.PATH_CATALOGUE_XML] = "5798009811578";
            dictionary[TestConstants.PATH_CATALOGUEDELETION_XML] = "5798009811578";
            dictionary[TestConstants.PATH_CATALOGUEITEMSPECIFICATIONUPDATE_XML] = "5798009811578";
            dictionary[TestConstants.PATH_CATALOGUEPRICINGUPDATE_XML] = "5798009811578";
            dictionary[TestConstants.PATH_CATALOGUEREQUEST_XML] = "5798009811578";
            dictionary[TestConstants.PATH_CREDITNOTE_XML] = "5798009811578";
            dictionary[TestConstants.PATH_INVOICE_XML] = "5798009811578";
            dictionary[TestConstants.PATH_ORDER_XML] = "5798009811578";
            dictionary[TestConstants.PATH_ORDERCANCELLATION_XML] = "5798009811578";
            dictionary[TestConstants.PATH_ORDERCHANGE_XML] = "5798009811578";
            dictionary[TestConstants.PATH_ORDERRESPONSE_XML] = "5798009811578";
            dictionary[TestConstants.PATH_ORDERRESPONSESIMPLE_XML] = "5798009811578";
            dictionary[TestConstants.PATH_REMINDER_XML] = "5798009811578";
            dictionary[TestConstants.PATH_STATEMENT_XML] = "5798009811578";
            CompareIdentifiers(dictionary);
        }


        private void CompareIdentifiers(Dictionary<string, string> dictionary) {
            foreach (KeyValuePair<string, string> pair in dictionary) {
                Identifier identifier = GetIdentifierValue(pair.Key);
                Assert.AreEqual(pair.Value, identifier.GetAsString(), "Error reading correct identifier from document using xpath specified in config: " + pair.Key);
            }
        }

        private Identifier GetIdentifierValue(string path) {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            DocumentTypeConfig config = _searcher.FindUniqueDocumentType(document);
            string keyXpath = config.EndpointType.Key.XPath;
            PrefixedNamespace[] namespaces = config.Namespaces;
            string code = "ean";
            Identifier identifier = Utilities.GetEndpointKeyByXpath(document, keyXpath, namespaces, code);
            return identifier;
        }
    }
}
