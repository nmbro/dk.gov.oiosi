using System;
using System.Diagnostics;
using System.IO;
using System.Text;

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
using System.Xml.XPath;
using System.Xml.Xsl;
using dk.gov.oiosi.logging;
using dk.gov.oiosi.xml.schematron;

namespace dk.gov.oiosi.xml.xslt
{
    /// <summary>
    /// This utility is used for xslt transforming documents
    /// </summary>
    public class XsltUtility
    {
        private ILogger logger;

        public XsltUtility()
        {
            this.logger = LoggerFactory.Create(this.GetType());
        }

        /// <summary>
        /// Method to transform any xmldocument with a given xslt
        /// </summary>
        /// <param name="xmlDoc">The xml document to transform</param>
        /// <param name="stylesheet">The xslt to transform xml document with</param>
        /// <returns>The transformed xml document</returns>
        public XmlDocument TransformXml(XmlDocument xmlDoc, CompiledXslt stylesheet)
        {
            XmlDocument xmlDocument = null;
            //XslCompiledTransform transform = PrecompiledStyleSheet(stylesheet);
            if (stylesheet != null && stylesheet.XslCompiledTransform != null)
            {
                xmlDocument = this.TransformXml(xmlDoc, stylesheet.XslCompiledTransform);
            }

            return xmlDocument;
        }

        /////// <summary>
        /////// Method that returns the precompiled XSLT stylesheet from the given XML document.
        /////// 
        /////// document() function and embedded script blocks is disabled it doesn't resolve external
        /////// XML resources
        /////// </summary>
        /////// <param name="stylesheet"></param>
        /////// <returns></returns>
        ////public XslCompiledTransform PrecompiledStyleSheet(XmlDocument stylesheet)
        ////{
        ////    XslCompiledTransform transform = null;

        ////    try
        ////    {
        ////        // Get XSLT version
        ////        XPathNavigator navigator = stylesheet.CreateNavigator();
        ////        XPathNodeIterator node = navigator.Select("/*/@version");
        ////        node.MoveNext();
        ////        string xsltVersionInResource = node.Current.Value;
        ////        string xsltVersion = string.Empty;
        ////        if (!string.IsNullOrEmpty(xsltVersionInResource))
        ////        {
        ////            xsltVersion = xsltVersionInResource;
        ////        }

        ////        if (xsltVersion.Equals("1.0"))
        ////        {
        ////            // The XslCompiledTransform can only handle xslt version 1.0
        ////            transform = new XslCompiledTransform(false);
        ////            transform.Load(stylesheet, XsltSettings.Default, null);
        ////        }
        ////    }
        ////    catch (System.Xml.Xsl.XsltException ex)
        ////    {
        ////        if (ex.Message.Equals("Too complex!"))
        ////        {
        ////            // To complex
        ////            // using saxon
        ////        }
        ////        else
        ////        {
        ////            //Debug.Assert(false, "XslCompiledTransform failed loading stylesheet.", ex.ToString());
        ////            throw;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Debug.Assert(false, "XslCompiledTransform failed loading stylesheet.", ex.ToString());
        ////        throw;
        ////    }

        ////    return transform;
        ////}       

        /// <summary>
        /// Method that transforms the XML document from a precompiled XSLT stylesheet
        /// 
        /// no namespace-qualified arguments is used
        /// </summary>
        /// <param name="document"></param>
        /// <param name="transform"></param>
        /// <returns></returns>
        public XmlDocument TransformXPath(XPathDocument document, XslCompiledTransform transform)
        {
            XmlDocument transformedXml = new XmlDocument();

            using (XmlWriter writer = transformedXml.CreateNavigator().AppendChild())
            {
                transform.Transform(document, writer);
            }

            return transformedXml;
        }

        public XmlDocument TransformXml(string documentAsString, XslCompiledTransform transform)
        {
            this.logger.Trace("Start TransformXml");
            XmlDocument transformedXml = new XmlDocument();

            using (Stream stream = new MemoryStream())
            {
                ////XmlWriterSettings xmlDocumentWriterSettings = new XmlWriterSettings();
                ////xmlDocumentWriterSettings.Encoding = Encoding.UTF8;
                ////xmlDocumentWriterSettings.Indent = true;
                ////xmlDocumentWriterSettings.IndentChars = " ";
                ////xmlDocumentWriterSettings.NewLineOnAttributes = true;
                ////xmlDocumentWriterSettings.OmitXmlDeclaration = false;
                ////xmlDocumentWriterSettings.CloseOutput = false;

                using (StreamWriter sw = new StreamWriter(stream))
                {
                    // write document to memory stream
                    sw.Write(documentAsString);
                    sw.Flush();

                    // document is now in a memory stream set stream index to 0.
                    stream.Seek(0, SeekOrigin.Begin);

                    // create the writer, that holds the styled xml document (schematron result).
                    XmlReaderSettings readerSettings = new XmlReaderSettings();
                    readerSettings.CloseInput = false;

                    using (XmlReader reader = XmlReader.Create(stream, readerSettings))
                    {
                        XmlWriterSettings writerSettings = new XmlWriterSettings();
                        writerSettings.Encoding = Encoding.UTF8;
                        writerSettings.Indent = true;
                        writerSettings.IndentChars = " ";
                        writerSettings.NewLineOnAttributes = true;
                        writerSettings.OmitXmlDeclaration = false;
                        writerSettings.CloseOutput = false;

                        MemoryStream resultStream = new MemoryStream();
                        using (XmlWriter schematronResultXmlWriter = XmlWriter.Create(resultStream, writerSettings))
                        {
                            // now both the reader and writer use a memory stream
                            transform.Transform(reader, schematronResultXmlWriter);
                        }

                        // create the schematron result from
                        resultStream.Seek(0, SeekOrigin.Begin);

                        transformedXml.Load(resultStream);
                    }
                }
            }

            this.logger.Trace("Finish");

            return transformedXml;
        }

        public XmlDocument TransformXml(XmlDocument document, XslCompiledTransform transform)
        {
            this.logger.Trace("Start TransformXml");
            XmlDocument transformedXml = new XmlDocument();

            using (Stream stream = new MemoryStream())
            {
                XmlWriterSettings xmlDocumentWriterSettings = new XmlWriterSettings();
                xmlDocumentWriterSettings.Encoding = Encoding.UTF8;
                xmlDocumentWriterSettings.Indent = true;
                xmlDocumentWriterSettings.IndentChars = " ";
                xmlDocumentWriterSettings.NewLineOnAttributes = true;
                xmlDocumentWriterSettings.OmitXmlDeclaration = false;
                xmlDocumentWriterSettings.CloseOutput = false;
                
                using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlDocumentWriterSettings))
                {
                    // write document to memory stream Note - this line is expensive in processing time
                    document.WriteTo(xmlWriter);
                }

                // document is now in a memory stream set stream index to 0.
                stream.Seek(0, SeekOrigin.Begin);

                // create the writer, that holds the styled xml document (schematron result).
                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.CloseInput = false;

                /*  // Note - jlm - denne linje er meget meget dyr i tid
                  //TextReader textReader = new StringReader(document.OuterXml);
                  //XmlReader xmlReader = new XmlTextReader(textReader);
                  */

                using (XmlReader reader = XmlReader.Create(stream, readerSettings))
                {
                    XmlWriterSettings writerSettings = new XmlWriterSettings();
                    writerSettings.Encoding = Encoding.UTF8;
                    writerSettings.Indent = true;
                    writerSettings.IndentChars = " ";
                    writerSettings.NewLineOnAttributes = true;
                    writerSettings.OmitXmlDeclaration = false;
                    writerSettings.CloseOutput = false;

                    MemoryStream resultStream = new MemoryStream();
                    using (XmlWriter schematronResultXmlWriter = XmlWriter.Create(resultStream, writerSettings))
                    {
                        // now both the reader and writer use a memory stream
                        transform.Transform(reader, schematronResultXmlWriter);
                    }

                    // create the schematron result from
                    resultStream.Seek(0, SeekOrigin.Begin);

                    transformedXml.Load(resultStream);
                }
            }

            this.logger.Trace("Finish");

            return transformedXml;
        }

        // just as fast - can be improved?
        private XmlDocument TransformXml2(XmlDocument document, XslCompiledTransform transform)
        {
            this.logger.Trace("Start TransformXml");
            XmlDocument transformedXml = new XmlDocument();

            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.CloseInput = false;

            // Note - this line is expensive in processing time
            StringReader stringReader = new StringReader(document.OuterXml);
            using (XmlReader reader = XmlTextReader.Create(stringReader, readerSettings))
            {
                XmlWriterSettings writerSettings = new XmlWriterSettings();
                writerSettings.Encoding = Encoding.UTF8;
                writerSettings.Indent = true;
                writerSettings.IndentChars = " ";
                writerSettings.NewLineOnAttributes = true;
                writerSettings.OmitXmlDeclaration = false;
                writerSettings.CloseOutput = false;

                MemoryStream resultStream = new MemoryStream();
                using (XmlWriter writer = XmlWriter.Create(resultStream, writerSettings))
                {
                    transform.Transform(reader, writer);
                }

                // create the schematron result from
                resultStream.Seek(0, SeekOrigin.Begin);

                transformedXml.Load(resultStream);
            }

            this.logger.Trace("Finish");

            return transformedXml;
        }

        // just as fast - can be improved?
        private XmlDocument TransformXml3(XmlDocument document, XslCompiledTransform transform)
        {
            this.logger.Trace("Start TransformXml");
            XmlDocument transformedXml = new XmlDocument();

            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.CloseInput = false;

            // Note - this line is expensive in processing time

            // XmlNodeReader xnr = new XmlNodeReader(document.DocumentElement);
            //XmlReader r = XmlTextReader.Create(
            this.logger.Trace("TransformXml1");
            XmlNode node = document.DocumentElement;
            this.logger.Trace("TransformXml2");
            using (XmlNodeReader nodeReader = new XmlNodeReader(node))
            {
                using (XmlReader reader = XmlReader.Create(nodeReader, readerSettings))
                {
                    this.logger.Trace("TransformXml3");

                    XmlWriterSettings writerSettings = new XmlWriterSettings();
                    writerSettings.Encoding = Encoding.UTF8;
                    writerSettings.Indent = true;
                    writerSettings.IndentChars = " ";
                    writerSettings.NewLineOnAttributes = true;
                    writerSettings.OmitXmlDeclaration = false;
                    writerSettings.CloseOutput = false;

                    MemoryStream resultStream = new MemoryStream();
                    this.logger.Trace("TransformXml4");
                    using (XmlWriter writer = XmlWriter.Create(resultStream, writerSettings))
                    {
                        this.logger.Trace("TransformXml4,1");
                        transform.Transform(reader, writer);
                    }
                    this.logger.Trace("TransformXml5");
                    // create the schematron result from
                    resultStream.Seek(0, SeekOrigin.Begin);

                    transformedXml.Load(resultStream);
                }
            }

            this.logger.Trace("Finish");

            return transformedXml;
        }
    }
}