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
  *
  */
using System.Xml;

namespace dk.gov.oiosi.xml.xpath.discriminator {
    /// <summary>
    /// Provides functions that can discriminate a document from one or more 
    /// xpath expressions with their corresponding expected result.
    /// </summary>
    public class XPathDiscriminator {
        /// <summary>
        /// Discriminates the xml document from a single configuration 
        /// element
        /// </summary>
        /// <param name="config"></param>
        /// <param name="xmlDocument"></param>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        public static bool Discriminate(XPathDiscriminatorConfig config, XmlDocument xmlDocument, PrefixedNamespace[] namespaces) {
            string xpathExpression = config.XPathExpression;
            string xpathResult = config.XPathExpectedResult;
            string result = DocumentXPathResolver.GetElementValueByXpath(xmlDocument, xpathExpression, namespaces);
            bool xPathResultIsContainedInResult = result.Contains(xpathResult);
            return xPathResultIsContainedInResult;
        }

        /// <summary>
        /// Discriminates the xml document from a collection of configuration
        /// elements. The results are anded together.
        /// </summary>
        /// <param name="configCollection"></param>
        /// <param name="xmlDocument"></param>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        public static bool DiscriminateCollectionAnded(XpathDiscriminatorConfigCollection configCollection, XmlDocument xmlDocument, PrefixedNamespace[] namespaces) {
            foreach (XPathDiscriminatorConfig config in configCollection) {
                if (!Discriminate(config, xmlDocument, namespaces))
                    return false;
            }
            return true;
        }
    }
}
