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
  *   Jacob Mogensen, mySupply
  */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.logging;
using dk.gov.oiosi.xml.xpath;
using dk.gov.oiosi.xml.xslt;
using Saxon.Api;
using dk.gov.oiosi.common;
using System.Configuration;

namespace dk.gov.oiosi.xml.schematron
{
    /// <summary>
    /// Validates schematrons
    /// </summary>
    [XmlRoot(Namespace = ConfigurationHandler.RaspNamespaceUrl)]
    public class SchematronValidator
    {
        private string errorXPath;
        private string errorMessageXPath;
        private CompiledXslt compiledXsltEntry;
        private XsltUtility xlstUtil;

        private ILogger logger;

        /// <summary>
        /// Constructs a new schematron validator
        /// </summary>
        public SchematronValidator()
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.xlstUtil = new XsltUtility();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SchematronValidator(string errorXPath, string errorMessageXPath)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.xlstUtil = new XsltUtility();
            this.errorXPath = errorXPath;
            this.errorMessageXPath = errorMessageXPath;
        }

        /// <summary>
        /// Constructor that takes the configuration as a parameter
        /// </summary>
        /// <param name="config"></param>
        public SchematronValidator(SchematronValidationConfig config)
        {
            this.logger = LoggerFactory.Create(this.GetType());
            this.xlstUtil = new XsltUtility();
            this.errorXPath = config.ErrorXPath;
            this.errorMessageXPath = config.ErrorMessageXPath;

            string stylesheetPath;
            string basePath = ConfigurationManager.AppSettings["ResourceBasePath"];
            if (string.IsNullOrEmpty(basePath))
            {
                stylesheetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.SchematronDocumentPath);
            }
            else
            {
                stylesheetPath = Path.Combine(basePath, config.SchematronDocumentPath);
            }

            this.compiledXsltEntry = new CompiledXslt(new FileInfo(stylesheetPath));            
        }

        /// <summary>
        /// </summary>
        /// <param name="xmlDocument"></param>
        public void SchematronValidateXmlDocument(XmlDocument xmlDocument)
        {
            if (this.compiledXsltEntry == null)
            {
                throw new Exception("No schematron document is set");
            }
            if (this.errorXPath == null)
            {
                throw new Exception("No error XPath is set");
            }
            if (this.errorMessageXPath == null)
            {
                throw new Exception("No error message XPath is set");
            }

            this.SchematronValidateXmlDocument(xmlDocument, this.compiledXsltEntry);
        }

        /// <summary>
        /// Validates a document
        /// </summary>
        /// <param name="xmlDocument">document to validate</param>
        /// <param name="compiledXslt">stylesheet to use</param>
        public void SchematronValidateXmlDocument(XmlDocument xmlDocument, CompiledXslt compiledXslt)
        {
            if (this.errorXPath == null)
            {
                throw new Exception("No error XPath is set");
            }
            if (this.errorMessageXPath == null)
            {
                throw new Exception("No error message XPath is set");
            }

            bool documentValidated = false;
            PrefixedNamespace[] prefixedNamespaces = null;// = this.CreateDefaultNamespaceManager(xmlTextReader);
            bool hasAnyErrors = false;
            XmlDocument schematronResultXmlDocument = null;
            // First we try windows build in transformer. It handle xslt 1.0, but not xslt 2.0. Any
            // stylesheet in xslt 2.0 will fail, and some xslt 1.0, as they is too complex for the parser.
            try
            {
                // .Net build in xslt parser - only xslt 1.0
                
                if (compiledXslt != null && compiledXslt.XslCompiledTransform != null)
                {
                    schematronResultXmlDocument = xlstUtil.TransformXml(xmlDocument, compiledXslt.XslCompiledTransform);
                    byte[] schematronResultBytes = Encoding.Default.GetBytes(schematronResultXmlDocument.OuterXml);
                    using (MemoryStream schematronResultMemoryStream = new MemoryStream(schematronResultBytes))
                    {
                        XmlTextReader schematronResultXmlTextReader = new XmlTextReader(schematronResultMemoryStream);
                        prefixedNamespaces = this.CreateDefaultNamespaceManager(schematronResultXmlTextReader);

                        hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(schematronResultXmlDocument, this.errorXPath, prefixedNamespaces);
                        documentValidated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
            }

            if (documentValidated == false)
            {
                // Not xslt 1.0, or complex xslt 1.0  - so try Saxon xslt
                try
                {
                    using (MemoryStream schematronResultMemoryStream = new MemoryStream())
                    {
                        using (MemoryStream xmlSchematronStylesheetMemoryStream = compiledXslt.Stream)
                        {
                            xmlSchematronStylesheetMemoryStream.Flush();//Adjust this if you want read your data
                            xmlSchematronStylesheetMemoryStream.Position = 0;

                            Processor processor = new Processor();
                            XsltCompiler compiler = processor.NewXsltCompiler();
                            Serializer serializer = new Serializer();

                            try
                            {
                                XsltTransformer saxonTransformer = compiler.Compile(xmlSchematronStylesheetMemoryStream).Load();

                                // Load the XML document. Input to the build method is the document.
                                XdmNode docXdmNode = processor.NewDocumentBuilder().Build(xmlDocument);

                                // Set the root node of the source document to be the initial
                                // context node
                                saxonTransformer.InitialContextNode = docXdmNode;

                                // Init. the result object
                                serializer.SetOutputProperty(Serializer.INDENT, "yes");
                                serializer.SetOutputProperty(Serializer.ENCODING, Encoding.UTF8.BodyName);

                                serializer.SetOutputStream(schematronResultMemoryStream);

                                // Run the transformation with result object as input param.
                                saxonTransformer.Run(serializer);
                            }
                            catch (Exception)
                            {
                                // easy debugging
                                throw;
                            }
                            finally
                            {
                                // close/dispose
                                serializer.Close();
                            }
                        }

                        // convert the schematronResultMemoryStream, into a xmlDocument
                        schematronResultMemoryStream.Position = 0;
                        schematronResultXmlDocument = new XmlDocument();
                        schematronResultXmlDocument.Load(schematronResultMemoryStream);

                        schematronResultMemoryStream.Position = 0;
                        XmlTextReader schematronResultXmlTextReader = new XmlTextReader(schematronResultMemoryStream);
                        prefixedNamespaces = this.CreateDefaultNamespaceManager(schematronResultXmlTextReader);

                        hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(schematronResultXmlDocument, this.errorXPath, prefixedNamespaces);
                        documentValidated = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.Message);
                    throw new SchematronValidationFailedException(xmlDocument, ex);
                }
            }

            if (documentValidated == false)
            {
                throw new SchematronValidationFailedException(xmlDocument, new Exception("Failed to validate the document."));
            }

            if (hasAnyErrors)
            {
                string firstErrorMessage = DocumentXPathResolver.GetFirstElementValueByXPath(schematronResultXmlDocument, this.errorMessageXPath, prefixedNamespaces);
                throw new SchematronErrorException(schematronResultXmlDocument, firstErrorMessage);
            }
            else
            {
                // no schematron error
            }
        }

        private PrefixedNamespace[] CreateDefaultNamespaceManager(XmlTextReader reader)
        {
            PrefixedNamespace[] result;
            List<PrefixedNamespace> tmpResult = new List<PrefixedNamespace>();
            XmlNamespaceManager nameSpaceManager = new XmlNamespaceManager(new NameTable());

            while (!reader.EOF && reader.MoveToContent() != XmlNodeType.Element) ;

            if (reader.EOF)
            {
                // nothing to do
            }
            else
            {
                // Get all namespaces in the document
                IDictionary<string, string> ns = reader.GetNamespacesInScope(XmlNamespaceScope.All);

                IEnumerator<KeyValuePair<string, string>> enumera = ns.GetEnumerator();
                while (enumera.MoveNext())
                {
                    string name = enumera.Current.Key;
                    string value = enumera.Current.Value;

                    //nameSpaceManager.AddNamespace(name, value);
                    tmpResult.Add(new PrefixedNamespace(value, name));

                    if (name.CompareTo(String.Empty) == 0)
                    {
                        //Required by XPath 1.0, as it does not know about default namespaces.
                        //This means nodes with no prefix are referenced 'root:[nodename]', not '[nodename]'
                        //nameSpaceManager.AddNamespace("root", value);
                        tmpResult.Add(new PrefixedNamespace(value, "root"));
                    }
                }
            }

            result = tmpResult.ToArray();

            return result;
        }

        /////// <summary>
        /////// Schematron validates a document.
        /////// 
        /////// If the validation process fails it throws a SchematronValidationFailedException If the
        /////// document has any schematron errors it throws a SchematronErrorException
        /////// </summary>
        /////// <param name="document">The document to be validated</param>
        /////// <param name="schematronStylesheet"></param>
        ////public void SchematronValidateXmlDocument(XmlDocument document, CompiledXslt compiledXsltEntry)
        ////{
        ////    if (this.errorXPath == null)
        ////    {
        ////        throw new Exception("No error XPath is set");
        ////    }
        ////    if (this.errorMessageXPath == null)
        ////    {
        ////        throw new Exception("No error message XPath is set");
        ////    }

        ////    XmlDocument result = null;
        ////    PrefixedNamespace[] prefixedNamespaces;
        ////    bool hasAnyErrors;
        ////    try
        ////    {
        ////        // this is fast and ugly...
        ////        result = xlstUtil.TransformXml(document, schematronStylesheet);
        ////        byte[] schematronResultBytes = Encoding.Default.GetBytes(result.OuterXml);
        ////        using (MemoryStream schematronResultMemoryStream = new MemoryStream(schematronResultBytes))
        ////        {
        ////            XmlTextReader schematronResultXmlTextReader = new XmlTextReader(schematronResultMemoryStream);
        ////            prefixedNamespaces = this.CreateDefaultNamespaceManager(schematronResultXmlTextReader);
        ////            hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(result, this.errorXPath, prefixedNamespaces);
        ////        }                
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new SchematronValidationFailedException(document, ex);
        ////    }

        ////    if (hasAnyErrors)
        ////    {
        ////        string firstErrorMessage = DocumentXPathResolver.GetFirstElementValueByXPath(result, this.errorMessageXPath, prefixedNamespaces);
        ////        throw new SchematronErrorException(result, firstErrorMessage);
        ////    }
        ////    else
        ////    {
        ////        // no schematron error
        ////    }
        ////}

        /// <summary>
        /// Schematron validates a document.
        /// 
        /// If the validation process fails it throws a SchematronValidationFailedException If the
        /// document has any schematron errors it throws a SchematronErrorException
        /// </summary>
        /// <param name="documentAsString">The document to be validated</param>
        /// <param name="compiledXslt"></param>
        public void SchematronValidateXmlDocument(string documentAsString, CompiledXslt compiledXslt)
        {
            if (this.errorXPath == null)
            {
                throw new Exception("No error XPath is set");
            }
            if (this.errorMessageXPath == null)
            {
                throw new Exception("No error message XPath is set");
            }

            XmlDocument schematronResultXmlDocument = null;
            PrefixedNamespace[] prefixedNamespaces = null;
            bool hasAnyErrors = false;
            bool documentValidated = false;

            //// try
            ////{
            ////    result = xlstUtil.TransformXml(documentAsString, schematronStylesheet);
            ////    hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(result, this.errorXPath, prefixedNamespaces);
            ////}
            ////catch (Exception ex)
            ////{
            ////    XmlDocument xmlDoc = new XmlDocument();
            ////    xmlDoc.LoadXml(documentAsString);
            ////    throw new SchematronValidationFailedException(xmlDoc, ex);
            ////}

            ////if (hasAnyErrors)
            ////{
            ////    string firstErrorMessage = DocumentXPathResolver.GetFirstElementValueByXPath(result, this.errorMessageXPath, prefixedNamespaces);
            ////    throw new SchematronErrorException(result, firstErrorMessage);
            ////}
            ////else
            ////{
            ////    // no schematron error
            ////}
            // ---------
            try
            {
                // .Net build in xslt pserser - only xslt 1.0                
                if (compiledXslt != null && compiledXslt.XslCompiledTransform != null)
                {
                    schematronResultXmlDocument = xlstUtil.TransformXml(documentAsString, compiledXslt.XslCompiledTransform);
                    byte[] schematronResultBytes = Encoding.Default.GetBytes(schematronResultXmlDocument.OuterXml);
                    using (MemoryStream schematronResultMemoryStream = new MemoryStream(schematronResultBytes))
                    {
                        XmlTextReader schematronResultXmlTextReader = new XmlTextReader(schematronResultMemoryStream);
                        prefixedNamespaces = this.CreateDefaultNamespaceManager(schematronResultXmlTextReader);

                        hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(schematronResultXmlDocument, this.errorXPath, prefixedNamespaces);
                        documentValidated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
            }

            if (documentValidated == false)
            {
                // Not xslt 1.0, or complex xslt 1.0  - so try Saxon xslt
                try
                {
                    using (MemoryStream schematronResultMemoryStream = new MemoryStream())
                    {
                        using (MemoryStream xmlSchematronStylesheetMemoryStream = compiledXslt.Stream)
                        {
                            xmlSchematronStylesheetMemoryStream.Flush();//Adjust this if you want read your data
                            xmlSchematronStylesheetMemoryStream.Position = 0;

                            Processor processor = new Processor();
                            XsltCompiler compiler = processor.NewXsltCompiler();
                            Uri uri = new Uri("file://" + compiledXslt.FileInfo.Directory.FullName);
                            compiler.ErrorList = new List<object>();
                            compiler.BaseUri = uri;
                            Serializer serializer = new Serializer();

                            try
                            {
                                XsltTransformer saxonTransformer = compiler.Compile(xmlSchematronStylesheetMemoryStream).Load();

                                // Load the XML document. Input to the build method is the document.
                                DocumentBuilder docBuilder = processor.NewDocumentBuilder();
                                if (docBuilder.BaseUri == null)
                                {
                                    docBuilder.BaseUri = uri;
                                }

                                XdmNode docXdmNode = docBuilder.Build(documentAsString.ToStream());

                                // Set the root node of the source document to be the initial
                                // context node
                                saxonTransformer.InitialContextNode = docXdmNode;

                                // Init. the result object
                                serializer.SetOutputProperty(Serializer.INDENT, "yes");
                                serializer.SetOutputProperty(Serializer.ENCODING, Encoding.UTF8.BodyName);

                                serializer.SetOutputStream(schematronResultMemoryStream);

                                // Run the transformation with result object as input param.
                                saxonTransformer.Run(serializer);
                            }
                            catch (Exception)
                            {
                                // easy debugging
                                throw;
                            }
                            finally
                            {
                                // close/dispose
                                serializer.Close();
                            }
                        }

                        // convert the schematronResultMemoryStream, into a xmlDocument
                        schematronResultMemoryStream.Position = 0;
                        schematronResultXmlDocument = new XmlDocument();
                        schematronResultXmlDocument.Load(schematronResultMemoryStream);

                        schematronResultMemoryStream.Position = 0;
                        XmlTextReader schematronResultXmlTextReader = new XmlTextReader(schematronResultMemoryStream);
                        prefixedNamespaces = this.CreateDefaultNamespaceManager(schematronResultXmlTextReader);

                        hasAnyErrors = DocumentXPathResolver.HasAnyElementsByXpath(schematronResultXmlDocument, this.errorXPath, prefixedNamespaces);
                        documentValidated = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.Message);
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(documentAsString.ToStream());
                    throw new SchematronValidationFailedException(xmlDocument, ex);
                }
            }

            if (documentValidated == false)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(documentAsString.ToStream());
                throw new SchematronValidationFailedException(xmlDocument, new Exception("Failed to validate the document."));
            }

            if (hasAnyErrors)
            {
                string firstErrorMessage = DocumentXPathResolver.GetFirstElementValueByXPath(schematronResultXmlDocument, this.errorMessageXPath, prefixedNamespaces);
                throw new SchematronErrorException(schematronResultXmlDocument, firstErrorMessage);
            }
            else
            {
                // no schematron error
            }
        }        
    }
}