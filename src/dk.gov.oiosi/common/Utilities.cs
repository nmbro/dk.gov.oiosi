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
  *   Jacob Mogensen, mySupply ApS
  *   Jens Madsen, Comcare
  *
  */
using System;
using System.ServiceModel.Channels;
using System.Xml;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.communication;
using dk.gov.oiosi.communication.configuration;
using dk.gov.oiosi.uddi;
using dk.gov.oiosi.xml.xpath;

namespace dk.gov.oiosi.common
{

    /// <summary>
    /// Utilities that can be used for 
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// Converts the number of milliseconds a TimeSpan into an Int32. In case the number is too large to be represented by an Int32, Int32.MaxValue is returned
        /// </summary>
        /// <param name="timeSpan">TimeSpan to convert</param>
        /// <returns>Number of milliseconds in timespan</returns>
        public static int TimeSpanInMilliseconds(TimeSpan timeSpan)
        {
            int result;
            if (timeSpan.TotalMilliseconds > (double)int.MaxValue)
            {
                result = int.MaxValue;
            }
            else
            {
                result = Convert.ToInt32(timeSpan.TotalMilliseconds);
            }

            return result;
        }

        /// <summary>
        /// From an xpath expression, gets the endpoint key
        /// </summary>
        /// <param name="xmlDoc">The xml document to search</param>
        /// <param name="xpath">The xpath expression to apply</param>
        /// <param name="keyType">The type of key to return</param>
        /// <param name="prefixedNamespaces">The list of namespaces-prefix pairs used in the xpath expression</param>
        /// <returns>Returns the endpoint key</returns>
        public static Identifier GetEndpointKeyByXpath(
            XmlDocument xmlDoc,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces,
            string keyType
        )
        {
            // 1. Get the endpoint as string:
            string endpointKeyString = DocumentXPathResolver.GetElementValueByXpath(xmlDoc, xpath, prefixedNamespaces);

            // 2. Convert to the correct IIdentifierType:
            Identifier id = IdentifierUtility.GetIdentifierFromKeyType(endpointKeyString, keyType);

            return id;
        }

        /// <summary>
        /// Extracts the body of a WCF Message and returns it as an XmlDocment.
        /// The original message will not be readable after calling this method
        /// </summary>
        /// <param name="msg">The WCF message</param>
        /// <returns>The body of the Message as an XmlDocument</returns>
        public static XmlDocument GetMessageBodyAsXmlDocument(Message msg)
        {
            return Utilities.GetMessageBodyAsXmlDocument(msg, true);
        }

        /// <summary>
        /// Extracts the body of a WCF Message and returns it as an XmlDocment
        /// </summary>
        /// <param name="msg">The WCF message</param>
        /// <param name="discardOriginalMessage">If true, the body of the original message will not be 
        /// readable after calling this method</param>
        /// <returns>The body of the Message as an XmlDocument</returns>
        public static XmlDocument GetMessageBodyAsXmlDocument(Message msg, bool discardOriginalMessage)
        {
            XmlDocument messageXml;
            if (msg.IsEmpty)
            {
                messageXml = null;
            }
            else
            {
                messageXml = new XmlDocument();

                if (!discardOriginalMessage)
                {
                    MessageBuffer bufferCopy = msg.CreateBufferedCopy(int.MaxValue);
                    XmlDictionaryReader reader = bufferCopy.CreateMessage().GetReaderAtBodyContents();
                    messageXml.Load(reader);
                    reader.Close();
                    msg = bufferCopy.CreateMessage();
                    bufferCopy.Close();
                }
                else
                {
                    XmlDictionaryReader reader = msg.GetReaderAtBodyContents();
                    messageXml.Load(reader);
                    reader.Close();
                }
            }
            return messageXml;
        }

        /// <summary>
        /// Extracts the body of a WCF Message and returns it as an string.
        /// The original message will not be readable after calling this method
        /// </summary>
        /// <param name="msg">The WCF message</param>
        /// <returns>The body of the Message as an string</returns>
        public static string GetMessageBodyAsString(Message msg)
        {
            return GetMessageBodyAsString(msg, true);
        }

        /// <summary>
        /// Extracts the body of a WCF Message and returns it as an string
        /// </summary>
        /// <param name="msg">The WCF message</param>
        /// <param name="discardOriginalMessage">If true, the body of the original message will not be 
        /// readable after calling this method</param>
        /// <returns>The body of the Message as an string</returns>
        public static string GetMessageBodyAsString(Message msg, bool discardOriginalMessage)
        {
            string result;
            if (msg.IsEmpty)
            {
                result = string.Empty;
            }
            else
            {
                if (!discardOriginalMessage)
                {
                    MessageBuffer bufferCopy = msg.CreateBufferedCopy(int.MaxValue);

                    using(XmlDictionaryReader reader = bufferCopy.CreateMessage().GetReaderAtBodyContents())
                    {
                        result = reader.ReadOuterXml();
                    }
                }
                else
                {
                    using (XmlDictionaryReader reader = msg.GetReaderAtBodyContents())
                    {
                        result = reader.ReadOuterXml();
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// Returns the error message, including all inner exceptions error messages, as a string
        /// </summary>
        /// <param name="e">The outer exception</param>
        /// <returns>The outer + all inner exception messages</returns>
        public static string GetFullExceptionMessage(Exception e)
        {
            string result;
            if (e == null)
            {
                result = string.Empty;
            }
            else
            {
                result = e.Message + ". " + Utilities.GetFullExceptionMessage(e.InnerException);
            }

            return result;
        }

        /// <summary>
        /// Gets the endpoint key type code from a Message and a 
        /// DocumentTypeConfig.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public static string GetEndpointKeyTypeCode(OiosiMessage message, DocumentTypeConfig documentType)
        {
            XmlDocument xmlDocument = message.MessageXml;
            string endpointKeyTypeCode = Utilities.GetEndpointKeyTypeCode(xmlDocument, documentType);

            return endpointKeyTypeCode;
        }

        /// <summary>
        /// Gets the endpoint key type code from a Message and a 
        /// DocumentTypeConfig.
        /// </summary>
        public static string GetEndpointKeyTypeCode(XmlDocument xmlDocument, DocumentTypeConfig documentType)
        {
            //Finds all mapping expressions with the name "EndpointKeyType"
            DocumentEndpointInformation endpointType = documentType.EndpointType;
            KeyTypeMappingExpression mappingExpression = endpointType.Key.GetMappingExpression("EndpointKeyType");
            //Finds the endpoint key type value from the given xpath
            string xpathExpression = mappingExpression.XPathExpression;
            string endPointKeyTypeValue =
                DocumentXPathResolver.GetElementValueByXPathNavigator(
                    xmlDocument,
                    xpathExpression,
                    documentType.Namespaces);
            KeyTypeMapping mapping = mappingExpression.GetMapping(endPointKeyTypeValue);
            string endpointKeyTypeCode = mapping.MapsTo;//Utilities.ParseKeyTypeCode(mapping.MapsTo);

            return endpointKeyTypeCode;
        }

        /// <summary>
        /// Gets the endpoint key type code from a Message and a 
        /// DocumentTypeConfig.
        /// </summary>
        public static string GetSenderKeyTypeCode(XmlDocument xmlDocument, DocumentTypeConfig documentType)
        {
            //Finds all mapping expressions with the name "EndpointKeyType"
            DocumentEndpointInformation endpointType = documentType.EndpointType;
            KeyTypeMappingExpression mappingExpression = endpointType.SenderKey.GetMappingExpression("EndpointKeyType");
            //Finds the endpoint key type value from the given xpath
            string xpathExpression = mappingExpression.XPathExpression;
            string endPointKeyTypeValue =
                DocumentXPathResolver.GetElementValueByXPathNavigator(
                    xmlDocument,
                    xpathExpression,
                    documentType.Namespaces);
            KeyTypeMapping mapping = mappingExpression.GetMapping(endPointKeyTypeValue);
            string endpointKeyTypeCode = mapping.MapsTo;

            return endpointKeyTypeCode;
        }
    }
}