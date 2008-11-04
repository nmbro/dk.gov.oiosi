
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

namespace dk.gov.oiosi.extension.wcf.Interceptor.Security.Header {
    /// <summary>
    /// Represent a basic abstract header that all headers inherit to get basic 
    /// functionality.
    /// </summary>
    public abstract class Header {
        /// <summary>
        /// Gets an element value from a tag name.
        /// </summary>
        /// <param name="headerDocument"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        protected string GetElementValueFromTagName(XmlDocument headerDocument, string tagName) {
            return GetElementValueFromTagName(headerDocument.DocumentElement, tagName);
        }

        /// <summary>
        /// Gets an element value from a tag name.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        protected string GetElementValueFromTagName(XmlNode parentNode, string tagName) {
            string value = "";
            int elements = 0;
            foreach (XmlNode node in parentNode.ChildNodes) {
                string[] nodeNameSubjects = node.Name.Split(':');
                if (nodeNameSubjects[nodeNameSubjects.Length-1] == tagName)
                    foreach (XmlNode subNode in node.ChildNodes)
                        if (subNode.NodeType == XmlNodeType.Text) {
                            value = subNode.Value;
                            elements++;
                        }
            }
            if (elements < 1) throw new NoElementsFoundException(tagName);
            if (elements > 1) throw new ToManyElementsFoundException(tagName, elements);
            return value;
        }

        /// <summary>
        /// Gets an element value from a tag name.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        protected string GetAttributeValueFromTagName(XmlNode node, string tagName) {
            string value = "";
            int elements = 0;
            foreach (XmlAttribute attribute in node.Attributes) {
                string[] attributeNameSubjects = attribute.Name.Split(':');
                if (attributeNameSubjects[attributeNameSubjects.Length - 1] == tagName)
                    foreach (XmlNode subNode in attribute.ChildNodes)
                        if (subNode.NodeType == XmlNodeType.Text) {
                            value = subNode.Value;
                            elements++;
                        }
            }
            if (elements < 1) throw new NoElementsFoundException(tagName);
            if (elements > 1) throw new ToManyElementsFoundException(tagName, elements);
            return value;
        }

        /// <summary>
        /// Get List of elements from a tag name
        /// </summary>
        /// <param name="headerDocument"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        protected IEnumerable<XmlNode> GetElementsFromTagName(XmlDocument headerDocument, string tagName) {
            List<XmlNode> nodes = new List<XmlNode>();
            foreach (XmlNode node in headerDocument.DocumentElement.ChildNodes)
                if (node.Name.EndsWith(tagName))
                    nodes.Add(node);
            return nodes;
        }

        /// <summary>
        /// Does document contain the tag name
        /// </summary>
        /// <param name="headerDocument"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        protected bool ContainsElementFromTagName(XmlDocument headerDocument, string tagName) {
            XmlNodeList messageNumberElements = headerDocument.GetElementsByTagName(tagName);
            return messageNumberElements.Count > 0;
        }
    }
}
