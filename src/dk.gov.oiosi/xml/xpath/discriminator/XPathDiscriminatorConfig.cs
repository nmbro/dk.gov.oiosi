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

namespace dk.gov.oiosi.xml.xpath.discriminator {
    /// <summary>
    /// Configuration to an XPath discrimination
    /// </summary>
    public class XPathDiscriminatorConfig : IEquatable<XPathDiscriminatorConfig> {
        private string _xpathExpression = "";
        private string _xpathExpectedResult = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public XPathDiscriminatorConfig() { }

        /// <summary>
        /// Constructor that takes the XPath expression and the expected result 
        /// as paramters.
        /// </summary>
        /// <param name="xpathExpression"></param>
        /// <param name="xpathExpectedResult"></param>
        public XPathDiscriminatorConfig(string xpathExpression, string xpathExpectedResult) {
            _xpathExpression = xpathExpression;
            _xpathExpectedResult = xpathExpectedResult;
        }

        /// <summary>
        /// Gets and sets the XPath expression used by the 
        /// XPath discriminator
        /// </summary>
        public string XPathExpression {
            get { return _xpathExpression; }
            set { _xpathExpression = value; }
        }

        /// <summary>
        /// Gets and sets the XPath expected result used by the 
        /// XPath discriminator
        /// </summary>
        public string XPathExpectedResult {
            get { return _xpathExpectedResult; }
            set { _xpathExpectedResult = value; }
        }

        #region IEquatable<XPathDiscriminatorConfig> Members

        /// <summary>
        /// Returns whether the other XPathDiscriminator given is equals
        /// to this.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(XPathDiscriminatorConfig other) {
            if (_xpathExpression != other._xpathExpression) return false;
            return _xpathExpectedResult == other._xpathExpectedResult;
        }

        #endregion
    }
}
