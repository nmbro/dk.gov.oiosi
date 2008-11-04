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
using System.Xml;
using System.Xml.Xsl;

namespace dk.gov.oiosi.xml.xslt {

    /// <summary>
    /// This utility is used for xslt transforming documents
    /// </summary>
    public class XsltUtility {

        /// <summary>
        /// Method to transform any xmldocument with a given xslt
        /// </summary>
        /// <param name="xmlDoc">The xml document to transform</param>
        /// <param name="stylesheet">The xslt to transform xml document with</param>
        /// <returns>The transformed xml document</returns>
        public XmlDocument TransformXML(XmlDocument xmlDoc, XmlDocument stylesheet) {

            XslCompiledTransform transform = new XslCompiledTransform();
            XmlDocument transformedXml = new XmlDocument();

            try {
                //document() function and embedded script blocks is disabled
                //it doesn't resolve external XML resources
                transform.Load(stylesheet, XsltSettings.Default, null);

                //no namespace-qualified arguments is used
                using (XmlWriter writer = transformedXml.CreateNavigator().AppendChild()) {
                    transform.Transform(xmlDoc, (XsltArgumentList)null, writer);
                }
            } catch (ArgumentNullException) {
                throw;
            } catch (XsltCompileException) {
                throw;
            } catch (XsltException) {
                throw;
            } catch (XmlException) {
                throw;
            } catch (Exception) {
                throw;
            }
            return transformedXml;
        }
    }
}