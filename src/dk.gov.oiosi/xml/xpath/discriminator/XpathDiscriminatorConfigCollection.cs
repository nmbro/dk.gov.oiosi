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

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace dk.gov.oiosi.xml.xpath.discriminator
{
    /// <summary>
    /// A collection of XPath discriminator configruations
    /// </summary>
    public class XpathDiscriminatorConfigCollection : IEnumerable<XPathDiscriminatorConfig>, IEquatable<XpathDiscriminatorConfigCollection>
    {
        private List<XPathDiscriminatorConfig> _xpathDiscriminatorConfigs;

        /// <summary>
        /// Defatult constructor that initializes the collection.
        /// </summary>
        public XpathDiscriminatorConfigCollection()
        {
            _xpathDiscriminatorConfigs = new List<XPathDiscriminatorConfig>();
        }

        /// <summary>
        /// Gets and sets the configurations of the collection
        /// </summary>
        /// <remarks>
        /// This is used by the XML serialization, used the add and remove functions instead.
        /// </remarks>
        ///[XmlElement("XPathDiscriminatorConfig")]
        public XPathDiscriminatorConfig[] XPathDiscriminatorConfigs
        {
            get { return _xpathDiscriminatorConfigs.ToArray(); }
            set { _xpathDiscriminatorConfigs = new List<XPathDiscriminatorConfig>(value); }
        }

        /// <summary>
        /// Adds an XPath discriminator configuration to the collection.
        /// </summary>
        /// <param name="xpathDiscriminatorConfig"></param>
        public void Add(XPathDiscriminatorConfig xpathDiscriminatorConfig)
        {
            _xpathDiscriminatorConfigs.Add(xpathDiscriminatorConfig);
        }

        /// <summary>
        /// Removes an XPath discriminator configuration from the collection.
        /// </summary>
        /// <param name="xpathDiscriminatorConfig"></param>
        /// <returns></returns>
        public bool Remove(XPathDiscriminatorConfig xpathDiscriminatorConfig)
        {
            return _xpathDiscriminatorConfigs.Remove(xpathDiscriminatorConfig);
        }

        #region IEnumerable<XPathDiscriminatorConfig> Members

        /// <summary>
        /// Gets an IEnumerator to traverse the collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<XPathDiscriminatorConfig> GetEnumerator()
        {
            return _xpathDiscriminatorConfigs.GetEnumerator();
        }

        #endregion IEnumerable<XPathDiscriminatorConfig> Members

        #region IEnumerable Members

        /// <summary>
        /// Gets an IEnumerator to traverse the collection
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _xpathDiscriminatorConfigs.GetEnumerator();
        }

        #endregion IEnumerable Members

        #region IEquatable<XpathDiscriminatorConfigCollection> Members

        /// <summary>
        /// Returns whether the collection is equal with another collection. This is done by value
        /// comparering and not reference.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(XpathDiscriminatorConfigCollection other)
        {
            int thisCount = _xpathDiscriminatorConfigs.Count;
            int otherCount = other._xpathDiscriminatorConfigs.Count;
            if (thisCount != otherCount) return false;
            Comparison<XPathDiscriminatorConfig> comparer =
                delegate(XPathDiscriminatorConfig x, XPathDiscriminatorConfig y)
                {
                    if (x.XPathExpression != y.XPathExpression)
                        return x.XPathExpression.CompareTo(y.XPathExpression);
                    return x.XPathExpectedResult.CompareTo(y.XPathExpectedResult);
                };
            _xpathDiscriminatorConfigs.Sort(comparer);
            other._xpathDiscriminatorConfigs.Sort(comparer);
            for (int i = 0; i < thisCount; i++)
            {
                int compare = comparer(_xpathDiscriminatorConfigs[i], other._xpathDiscriminatorConfigs[i]);
                if (compare != 0) return false;
            }
            return true;
        }

        #endregion IEquatable<XpathDiscriminatorConfigCollection> Members
    }
}