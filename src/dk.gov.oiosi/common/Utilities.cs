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
using System.Xml;
using System.ServiceModel.Channels;
using dk.gov.oiosi.xml.xpath;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.exception;
using dk.gov.oiosi.uddi.category;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;

namespace dk.gov.oiosi.common {
    
    /// <summary>
    /// Utilities that can be used for 
    /// </summary>
    public class Utilities {
        /// <summary>
        /// Converts the number of milliseconds a TimeSpan into an Int32. In case the number is too large to be represented by an Int32, Int32.MaxValue is returned
        /// </summary>
        /// <param name="timeSpan">TimeSpan to convert</param>
        /// <returns>Number of milliseconds in timespan</returns>
        public static Int32 TimeSpanInMilliseconds(TimeSpan timeSpan) {
            if (timeSpan.TotalMilliseconds > (double)int.MaxValue) return int.MaxValue;
            return Convert.ToInt32(timeSpan.TotalMilliseconds);
        }

        /// <summary>
        /// From an xpath expression, gets the endpoint key
        /// </summary>
        /// <param name="xmlDoc">The xml document to search</param>
        /// <param name="xpath">The xpath expression to apply</param>
        /// <param name="keyType">The type of key to return</param>
        /// <param name="prefixedNamespaces">The list of namespaces-prefix pairs used in the xpath expression</param>
        /// <returns>Returns the endpoint key</returns>
        public static IIdentifier GetEndpointKeyByXpath(
            XmlDocument xmlDoc,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces,
            EndpointKeyTypeCode keyType
        ) {
            // 1. Get the endpoint as string:
            string endpointKeyString = DocumentXPathResolver.GetElementValueByXpath(xmlDoc, xpath, prefixedNamespaces);

            // 2. Convert to the correct IIdentifierType:
            IIdentifier id = IdentifierUtility.GetIdentifierFromKeyType(endpointKeyString, keyType);

            return id;
        }

        /// <summary>
        /// Extracts the body of a WCF Message and returns it as an XmlDocment
        /// </summary>
        /// <param name="msg">The WCF message</param>
        /// <param name="discardOriginalMessage">If true, the body of the original message will not be 
        /// readable after calling this method</param>
        /// <returns>The body of the Message as an XmlDocument</returns>
        public static XmlDocument GetMessageBodyAsXmlDocument(Message msg, bool discardOriginalMessage) {

            if (msg.IsEmpty)
                return null;

            XmlDocument messageXml = new XmlDocument();

            if (!discardOriginalMessage) {
                MessageBuffer bufferCopy = msg.CreateBufferedCopy(int.MaxValue);
                XmlDictionaryReader reader = bufferCopy.CreateMessage().GetReaderAtBodyContents();
                messageXml.Load(reader);
                reader.Close();
                msg = bufferCopy.CreateMessage();
                bufferCopy.Close();
            }
            else {
                XmlDictionaryReader reader = msg.GetReaderAtBodyContents();
                messageXml.Load(reader);
                reader.Close();
            }
            return messageXml;
        }

        /// <summary>
        /// Extracts the body of a WCF Message and returns it as an XmlDocment.
        /// The original message will not be readable after calling this method
        /// </summary>
        /// <param name="msg">The WCF message</param>
        /// <returns>The body of the Message as an XmlDocument</returns>
        public static XmlDocument GetMessageBodyAsXmlDocument(Message msg) { 
            return GetMessageBodyAsXmlDocument(msg, true); 
        }


        /// <summary>
        /// Returns the error message, including all inner exceptions error messages, as a string
        /// </summary>
        /// <param name="e">The outer exception</param>
        /// <returns>The outer + all inner exception messages</returns>
        public static string GetFullExceptionMessage(Exception e) {
            if (e == null) return "";
            else return e.Message + ". " + GetFullExceptionMessage(e.InnerException);
        }

        /// <summary>
        /// Gets the endpoint key type code from a Message and a 
        /// DocumentTypeConfig.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public static EndpointKeyTypeCode GetEndpointKeyTypeCode(OiosiMessage message, DocumentTypeConfig documentType) {
            XmlDocument xmlDocument = message.MessageXml;
            return GetEndpointKeyTypeCode(xmlDocument, documentType);
        }

        /// <summary>
        /// Gets the endpoint key type code from a Message and a 
        /// DocumentTypeConfig.
        /// </summary>
        public static EndpointKeyTypeCode GetEndpointKeyTypeCode(XmlDocument xmlDocument, DocumentTypeConfig documentType) {
            //Finds all mapping expressions with the name "EndpointKeyType"
            DocumentEndpointInformation endpointType = documentType.EndpointType;
            KeyTypeMappingExpression mappingExpression = endpointType.Key.GetMappingExpression("EndpointKeyType");
            //Finds the endpoint key type value from the given xpath
            string xpathExpression = mappingExpression.XPathExpression;
            string endPointKeyTypeValue =
                DocumentXPathResolver.GetElementValueByXpath(
                    xmlDocument,
                    xpathExpression,
                    documentType.Namespaces);
            KeyTypeMapping mapping = mappingExpression.GetMapping(endPointKeyTypeValue);
            return ParseKeyTypeCode(mapping.MapsTo);
        }

        /// <summary>
        /// Gets the endpoint key type code from a Message and a 
        /// DocumentTypeConfig.
        /// </summary>
        public static EndpointKeyTypeCode GetSenderKeyTypeCode(XmlDocument xmlDocument, DocumentTypeConfig documentType)
        {
            //Finds all mapping expressions with the name "EndpointKeyType"
            DocumentEndpointInformation endpointType = documentType.EndpointType;
            KeyTypeMappingExpression mappingExpression = endpointType.SenderKey.GetMappingExpression("EndpointKeyType");
            //Finds the endpoint key type value from the given xpath
            string xpathExpression = mappingExpression.XPathExpression;
            string endPointKeyTypeValue =
                DocumentXPathResolver.GetElementValueByXpath(
                    xmlDocument,
                    xpathExpression,
                    documentType.Namespaces);
            KeyTypeMapping mapping = mappingExpression.GetMapping(endPointKeyTypeValue);
            return ParseKeyTypeCode(mapping.MapsTo);
        }

        private static EndpointKeyTypeCode ParseKeyTypeCode(string code) {
            switch (code)
            {
                case "cvr":
                    return EndpointKeyTypeCode.cvr;
                case "ean":
                    return EndpointKeyTypeCode.ean;
                case "ovt":
                    return EndpointKeyTypeCode.ovt;
                case "p":
                    return EndpointKeyTypeCode.p;
                case "se":
                    return EndpointKeyTypeCode.se;
                case "vans":
                    return EndpointKeyTypeCode.vans;
                case "iban":
                    return EndpointKeyTypeCode.iban;
                case "duns":
                    return EndpointKeyTypeCode.duns;
                default:
                    return EndpointKeyTypeCode.other;
            }
        }
    }
}