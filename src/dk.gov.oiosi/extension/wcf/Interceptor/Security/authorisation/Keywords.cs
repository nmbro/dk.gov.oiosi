using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.authorisation {
    class Keywords {
        public static Dictionary<string, string> GetKeywords(X509Certificate2 certificate, XmlDocument xmlDocument, DocumentTypeConfig documentType) {
            Dictionary<string, string> keywords = KeywordsFromX509Certificate2.GetKeywords(certificate);
            KeywordFromXmlDocument.GetKeywords(keywords, xmlDocument);
            keywords.Add("documenttypefriendlyname", documentType.FriendlyName);
            keywords.Add("documenttypeid", documentType.Id.ToString());
            return keywords;
        }
    }
}
