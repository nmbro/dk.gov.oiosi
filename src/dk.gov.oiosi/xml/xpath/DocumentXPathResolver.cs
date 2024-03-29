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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System;
using System.Xml;
using dk.gov.oiosi.exception;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Diagnostics;

namespace dk.gov.oiosi.xml.xpath
{

    /// <summary>
    /// Resolves xpaths
    /// </summary>
    public class DocumentXPathResolver
    {

        /// <summary>
        /// Returns whether the given xpath yields any results.
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="xpath"></param>
        /// <param name="prefixedNamespaces"></param>
        /// <returns></returns>
        public static bool HasAnyElementsByXpath(
            XmlDocument xmlDocument,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces
        )
        {
            CheckParameters(xmlDocument, xpath, prefixedNamespaces);
            XmlNodeList nodes = GetNodes(xmlDocument, xpath, prefixedNamespaces);
            int nodesCount = nodes.Count;
            return nodesCount > 0;
        }

        /// <summary>
        /// Gets the value from an xpath expression.
        /// If there is no or multiple results from the xpath expression
        /// an exception is thrown.
        /// </summary>
        /// <param name="xmlDocument">The xml document to search</param>
        /// <param name="xpath">The xpath expression to apply</param>
        /// <param name="prefixedNamespaces">A list of namespace-prefixes used in the xpath expression</param>
        /// <returns>Returns the value from the xpath</returns>
        public static string GetElementValueByXpath(
            XmlDocument xmlDocument,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces
        )
        {
            CheckParameters(xmlDocument, xpath, prefixedNamespaces);
            //Get the nodes from the xpath query
            XmlNodeList nodes = GetNodes(xmlDocument, xpath, prefixedNamespaces);
            //Check whether the contrains are followed
            int nodesCount = nodes.Count;
            if (nodesCount < 1) throw new NoXPathResultsException(xpath);
            if (nodesCount > 1) throw new TooManyXpathResultsException(xpath, nodesCount);
            XmlNode node = nodes[0];
            return node.InnerText;
        }

        public static string GetElementValueByXPathNavigator(
            XmlDocument xmlDocument,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces
        )
        {
            DocumentXPathResolver.CheckParameters(xmlDocument, prefixedNamespaces);
            string result;

            if (string.IsNullOrEmpty(xpath))
            {
                result = string.Empty;
            }
            else
            {
                string[] lineIter = DocumentXPathResolver.GetXPathValues(xmlDocument, xpath, prefixedNamespaces);
                int nodesCount = lineIter.Length;
                if (nodesCount < 1) throw new NoXPathResultsException(xpath);
                if (nodesCount > 1) throw new TooManyXpathResultsException(xpath, nodesCount);
                result = lineIter[0];
            }

            return result;
        }

        /// <summary>
        /// Gets the first value from an xpath expression. 
        /// If there are no results from the xpath expression an exception is
        /// thrown.
        /// </summary>
        /// <param name="xmlDocument">The xml document to search</param>
        /// <param name="xpath">The xpath expression to apply</param>
        /// <param name="prefixedNamespaces">A list of namespace-prefixes used in the xpath expression</param>
        /// <returns>The first result value from the xpath expression</returns>
        public static string GetFirstElementValueByXPath(
            XmlDocument xmlDocument,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces
            )
        {
            CheckParameters(xmlDocument, xpath, prefixedNamespaces);
            string value = null;
            if (!TryGetFirstElementValueByXPath(xmlDocument, xpath, prefixedNamespaces, out value))
                throw new NoXPathResultsException(xpath);
            return value;
        }

        /// <summary>
        /// Tries get the first value from an xpath expression.
        /// It returns true or false whether there was any results
        /// from the xpath expression
        /// </summary>
        /// <param name="xmlDocument">The xml document to search</param>
        /// <param name="xpath">The xpath expression to apply</param>
        /// <param name="prefixedNamespaces">A list of namespace-prefixes used in the xpath expression</param>
        /// <param name="value">The result value from the xpath expression</param>
        /// <returns>a bool indicating whether the xpath gave any results</returns>
        public static bool TryGetFirstElementValueByXPath(
            XmlDocument xmlDocument,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces,
            out string value
        )
        {
            CheckParameters(xmlDocument, xpath, prefixedNamespaces);
            value = null;
            //Get the nodes from the xpath query
            XmlNodeList nodes = GetNodes(xmlDocument, xpath, prefixedNamespaces);
            int nodesCount = nodes.Count;
            if (nodesCount < 1) return false;
            value = nodes[0].InnerText;
            return true;
        }

        public static bool TryGetFirstElementValueByXPathNavigator(
           XmlDocument xmlDocument,
           string xpath,
           PrefixedNamespace[] prefixedNamespaces,
           out string value
       )
        {
            DocumentXPathResolver.CheckParameters(xmlDocument, prefixedNamespaces);
            bool result;

            if (string.IsNullOrEmpty(xpath))
            {
                value = string.Empty;
                result = true;
            }
            else
            {
                string[] lineIter = DocumentXPathResolver.GetXPathValues(xmlDocument, xpath, prefixedNamespaces);
                int nodesCount = lineIter.Length;

                if (nodesCount >= 1)
                {
                    value = lineIter[0];
                    result = true;
                }
                else
                {
                    value = string.Empty;
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the values resulted from an xpath query on the xml document.
        /// </summary>
        /// <param name="xmlDocument">The xml document to search</param>
        /// <param name="xpath">The xpath expression to apply</param>
        /// <param name="prefixedNamespaces">A list of namespace-prefixes used in the xpath expression</param>
        /// <returns>A string array containing the results from the xpath expression.</returns>
        public static string[] GetElementValuesByXPath(
            XmlDocument xmlDocument,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces
            )
        {
            CheckParameters(xmlDocument, xpath, prefixedNamespaces);
            //Get the nodes from the xpath query
            XmlNodeList nodes = GetNodes(xmlDocument, xpath, prefixedNamespaces);
            int nodesCount = nodes.Count;
            string[] values = new string[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                values[i] = nodes[i].InnerText;
            }
            return values;
        }

        private static void CheckParameters(
            XmlDocument xmlDocument,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces
            )
        {
            if (String.IsNullOrEmpty(xpath))
                throw new NullOrEmptyArgumentException("xpath");
            if (xmlDocument == null)
                throw new NullArgumentException("xmlDocument");
            if (prefixedNamespaces == null)
                throw new NullArgumentException("prefixedNamespaces");
            if (xpath.Trim().Length <= 1) throw new XPathSizeTooSmallException(xpath);
        }

        private static void CheckParameters(
            XmlDocument xmlDocument,
            PrefixedNamespace[] prefixedNamespaces
            )
        {
            if (xmlDocument == null)
                throw new NullArgumentException("xmlDocument");
            if (prefixedNamespaces == null)
                throw new NullArgumentException("prefixedNamespaces");
        }

        private static XmlNodeList GetNodes(
            XmlDocument xmlDocument,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces
            )
        {
            // XmlNodeList x = new XmlNodeList();

            //Prepare xpath search
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);

            /*
             * debug code
             * string cbc = namespaceManager.LookupNamespace("cbc");

            namespaceManager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            string dns0 = namespaceManager.DefaultNamespace;
            IDictionary<string, string> nis1 = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.All);
            IDictionary<string, string> nis2 = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
            IDictionary<string, string> nis3 = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.Local);
            */

            foreach (PrefixedNamespace preNs in prefixedNamespaces)
            {
                namespaceManager.AddNamespace(preNs.Prefix, preNs.Namespace);
            }

            /*
             * debug code
             * string dns10 = namespaceManager.DefaultNamespace;
            IDictionary<string, string> nis11 = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.All);
            IDictionary<string, string> nis12 = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
            IDictionary<string, string> nis13 = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.Local);
            */

            //Xpath search
            XmlNodeList nodes = xmlDocument.SelectNodes(xpath, namespaceManager);
            return nodes;
        }

        private static string[] GetXPathValues(
            XmlDocument xmlDocument,
            string xpath,
            PrefixedNamespace[] prefixedNamespaces
            )
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);

            foreach (PrefixedNamespace preNs in prefixedNamespaces)
            {
                namespaceManager.AddNamespace(preNs.Prefix, preNs.Namespace);
            }

            XPathNavigator navigator = xmlDocument.CreateNavigator();
            XPathExpression expression = navigator.Compile(xpath);
            expression.SetContext(namespaceManager);

            List<string> collection = new List<string>();
            switch (expression.ReturnType)
            {
                case XPathResultType.NodeSet:
                    {
                        XPathNodeIterator iterator = navigator.Select(expression);
                        while (iterator.MoveNext())
                        {
                            collection.Add(iterator.Current.Value);
                        }

                        break;
                    }
                case XPathResultType.Boolean:
                case XPathResultType.Number:
                case XPathResultType.String:// Also convers XPathResultType.Navigator
                    {
                        object value = navigator.Evaluate(expression);
                        collection.Add(value.ToString());
                        break;
                    }
                case XPathResultType.Any:
                case XPathResultType.Error:
                    {
                        break;
                    }
            }

            return collection.ToArray();
        }
    }
}